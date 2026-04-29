using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DentistX
{

    static class TreatHelper
    {
        public static bool isItMobTreat = false;

        // ProcessTooth
        // ├─ EvaluateVisibilityPolicy   ← ONE place, global, authoritative
        // ├─ ApplyBaseVisibility        ← blanket hide/show
        // ├─ ApplyViewSpecificRules     ← Top / In / Out
        // ├─ ApplySpecialCases          ← veneers, apex, etc.


        // New Helper

        private static bool ShouldSuppressLowLevels(List<DentistX.Patient_ToothTrt> allTreatments)
        {
            return allTreatments.Any(t => (int)t.LVL == 3);
        }


        public static Color capFillClr;
        public static Color rootFillClr;
        #region TreatCode
        public static void LoadTeethTreatsUsingTreatCode(Control cntrl, List<SvgImageBox> svgExternalList, List<SvgImageBox> svgDiagList, IEnumerable<DentistX.Patient_ToothTrt> patientTreats)
        {
            cntrl.SuspendLayout();
            foreach (Control ct in cntrl.Controls.OfType<SvgImageBox>())
            {
                SvgImageBox svg = (SvgImageBox)ct;
                svg.BeginUpdate();  // <== if SvgImageBox supports it
                                    // ProcessToothTreatments(svg, svgExternalList, svgDiagList, patientTreats)
                svg.EndUpdate();
            }
            cntrl.ResumeLayout();
        }
        /// <param name="isMobileChart">When True, enables mobile-chart rules (HandleDENTURE for LVL 9, base-tooth rules in ProcessTreatmentLayers).</param>
        public static void ProcessToothTreatments(SvgImageBox svg, List<SvgImageBox> svgExternalList, IEnumerable<DentistX.Patient_ToothTrt> patientTreats, bool isMobileChart = false)
        {
            bool previousMobile = isItMobTreat;
            isItMobTreat = isMobileChart;
            try
            {
                DentistX.Helpers.ClearSvgBackground(svg);
                var col = svg.RootItems;
                byte toothNum = Conversions.ToByte(svg.Tag);
                // Get treatments for this specific tooth
                // Dim orderedTrts = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderByDescending(Function(t) t.TreatDate)
                // Dim trtsList = orderedTrts.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList()
                var trtsList = patientTreats.Where(t => t.ToothNum == toothNum).OrderBy(t => t.LVL).ToList();
                // Reset all items invisible first
                ResetSvgItemsVisibility(col);
                // Handle special cases
                HandleExternalTreatments(svg, svgExternalList, col, trtsList);
                // If no treatments, just show base tooth and exit
                if (trtsList.Count == 0)
                {
                    ShowBaseTooth(col);
                    return;
                }
                // Process each treatment layer
                ProcessTreatmentLayers(svg, svgExternalList, col, trtsList);
            }
            finally
            {
                isItMobTreat = previousMobile;
            }
        }
        // =====================
        public static void ResetSvgItemsVisibility(SvgImageItemCollection col)
        {
            foreach (SvgImageItem item in col)
                item.Visible = false;
        }
        public static void ShowBaseTooth(SvgImageItemCollection col)
        {
            var baseTooth = col.Find(c => c.Id is not null && c.Id.Contains("IMG") && c.Id != "CROWN_IMG");
            if (baseTooth is not null)
                baseTooth.Visible = true;
        }
        public static void HandleExternalTreatments(SvgImageBox svg, List<SvgImageBox> svgExternalList, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            var externalTrts = trtsList.Where(t => t.IsExternal.HasValue && t.IsExternal.Value == true).ToList();
            if (externalTrts.Any())
            {
                DentistX.Helpers.ApplyGradientBackground(svg, Color.AntiqueWhite, Color.White, System.Drawing.Drawing2D.LinearGradientMode.Horizontal, 128);
                svgExternalList.Add(svg);
            }
        }
        public static void ProcessTreatmentLayers(SvgImageBox svg, List<SvgImageBox> svgExternalList, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            // Show base tooth for levels 0-2
            if ((int)trtsList.Max(t => t.LVL) < 4 || isItMobTreat == false)
            {
                ShowBaseTooth(col);
            }
            // Process each treatment
            foreach (var t in trtsList)
                // ProcessIndividualTreatment(svg, col, t, trtsList)

                ProcessTreatment(svg, col, t, trtsList);
            // Handle special high-level cases
            HandleHighLevelTreatments(svg, svgExternalList, col, trtsList);
            bool hasAnyNotes = trtsList.Any(tr => tr.TreatNotes is not null && !string.IsNullOrEmpty(tr.TreatNotes));
            var notesMark = FindSvgItemById(col, "EXCLAMATION_MARK");
            if (notesMark is not null)
            {
                notesMark.Visible = hasAnyNotes;
                notesMark.Appearance.Normal.FillColor = DentistX.Module1.BackClr; // ColorTranslator.FromHtml("#55FFC719")  '("#D81A1A")'Color.Transparent
                notesMark.Appearance.Normal.BorderColor = ColorTranslator.FromHtml("#D81A1A"); // ("#55FFC719") 'Color.Transparent

            }
        }

        private static void ProcessTreatment(SvgImageBox svg, SvgImageItemCollection col, DentistX.Patient_ToothTrt treatment, List<DentistX.Patient_ToothTrt> allTreatments)
        {


            if (svg is null || col is null || allTreatments is null)
                return;
            var col2 = svg.RootItems;
            // ==========================================================
            // VIEW DETECTION
            // ==========================================================
            bool isTopView1 = IsTopView(svg);
            bool isOutView1 = IsOutView(svg);
            bool isInView1 = IsInView(svg);
            // ==========================================================
            // GROUP TREATMENTS BY SHAPE (PropertyName)
            // ==========================================================
            var shapeMap = allTreatments.Where(t => !string.IsNullOrWhiteSpace(t.PropertyName)).GroupBy(t => t.PropertyName).ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);

            // ==========================================================
            // GLOBAL LVL FACTS
            // ==========================================================
            bool hasLvl3 = allTreatments.Any(t => (int)t.LVL == 3);
            bool hasLvlLessThan4 = allTreatments.Any(t => (int)t.LVL < 4);

            // ==========================================================
            // HARD RESET
            // ==========================================================
            foreach (var el in col)
                el.Visible = false;

            // ==========================================================
            // BASE TOOTH VISIBILITY
            // ==========================================================
            if (hasLvlLessThan4)
            {
                foreach (var el in col)
                {
                    if (el.Id is not null && el.Id.Contains("IMG") && el.Id != "CROWN_IMG")
                    {
                        el.Visible = true;
                    }
                }
            }

            // ==========================================================
            // APPLY TREATMENT SHAPES
            // ==========================================================
            foreach (var el in col)
            {

                if (el.Id is null)
                    continue;

                string propertyName = el.Id;

                if (!shapeMap.ContainsKey(propertyName))
                    continue;

                var treatments = shapeMap[propertyName];

                // ------------------------------------------------------
                // OUT VIEW LOGIC
                // ------------------------------------------------------
                if (isOutView1)
                {
                    // For Each t In treatments
                    // If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrEmpty(t.TreatNotes) Then
                    // Dim notes = FindSvgItemById(col2, "EXCLAMATION_MARK")

                    // If notes IsNot Nothing Then
                    // notes.Visible = True
                    // 'notes.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.CapFill)
                    // Else
                    // notes.Visible = False
                    // End If
                    // End If
                    // Next

                    // CLASS treatments live on crown in OUT view
                    // If LVL 3 exists → hide all CLASS shapes
                    if (hasLvl3 && propertyName.StartsWith("CLASS_"))
                    {
                        continue;
                    }

                    // Otherwise show all LVL ≤ 3
                    if (treatments.Any(t => (int)t.LVL <= 3))
                    {
                        // el.Visible = True
                        foreach (var t in treatments)
                        {

                            switch (t.Treat ?? "")
                            {
                                case "INDIRECT PULP CAPPING":
                                    {
                                        var capFill = FindSvgItemById(col2, "INDIRECT_PULP_CAPPING1");
                                        var rootFill = FindSvgItemById(col2, "INDIRECT_PULP_CAPPING2");
                                        if (capFill is not null)
                                        {
                                            capFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.CapFill);
                                        }
                                        if (rootFill is not null)
                                        {
                                            rootFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.RootFill);
                                        }
                                        el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                                        el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                                        el.Visible = true;
                                        break;
                                    }
                                case "DIRECT PULP CAPPING":
                                    {
                                        var capFill = FindSvgItemById(col2, "DIRECT_PULP_CAPPING1");
                                        var rootFill = FindSvgItemById(col2, "DIRECT_PULP_CAPPING2");
                                        if (capFill is not null)
                                        {
                                            capFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.CapFill);
                                        }
                                        if (rootFill is not null)
                                        {
                                            rootFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.RootFill);
                                        }
                                        el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                                        el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                                        el.Visible = true;
                                        break;
                                    }
                                case "PULPOTOMY":
                                    {
                                        var capFill = FindSvgItemById(col2, "PULPOTOMY1");
                                        var rootFill = FindSvgItemById(col2, "PULPOTOMY2");
                                        if (capFill is not null)
                                        {
                                            capFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.CapFill);
                                        }
                                        if (rootFill is not null)
                                        {
                                            rootFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.RootFill);
                                        }
                                        el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                                        el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                                        el.Visible = true;
                                        break;
                                    }

                                default:
                                    {
                                        el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                                        el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                                        el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                                        el.Visible = true;
                                        break;
                                    }
                            }
                        }
                    }
                }

                // ------------------------------------------------------
                // TOP VIEW LOGIC
                // ------------------------------------------------------
                else if (isTopView1)
                {

                    if (hasLvl3)
                    {
                        // Only LVL 3
                        if (treatments.Any(t => (int)t.LVL == 3))
                        {
                            foreach (var t in treatments)
                            {
                                el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                                el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                                el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                                el.Visible = true;
                            }
                        }
                    }
                    // No LVL 3 → show lower levels
                    else if (treatments.Any(t => (int)t.LVL < 3))
                    {
                        foreach (var t in treatments)
                        {
                            el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                            el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                            el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                            el.Visible = true;
                        }
                    }
                }
                else if (isInView1)
                {

                    // IN VIEW LOGIC
                    // Similar to OUT view but may have different rules in future
                    // For now, treat it the same as OUT view
                    // CLASS treatments live on crown in IN view
                    // If LVL 3 exists → hide all CLASS shapes
                    if (hasLvl3 && propertyName.StartsWith("CLASS_"))
                    {
                        continue;
                    }
                    // Otherwise show all LVL ≤ 3
                    if (treatments.Any(t => (int)t.LVL <= 3))
                    {

                        foreach (var t in treatments)
                        {
                            el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                            el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                            el.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                            el.Visible = true;
                            if (t.Treat.Contains("VENEERS"))
                            {
                                // Special handling for VENEER in IN view
                                // e.g., adjust opacity or color
                                el.Visible = false;
                                var crownFill = col.Find(c => c.Id == "CROWN_FILL");
                            }

                        }
                    }
                }

            }

            // ' EXCLAMATION_MARK: set once at end so it is not cleared by a later ProcessTreatment call.
            // ' (ProcessTreatment is invoked once per treatment; the last run's visibility wins.)
            // If isOutView1 Then
            // Dim hasAnyNotes = allTreatments.Any(Function(tr) tr.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrEmpty(tr.TreatNotes))
            // Dim notesMark = FindSvgItemById(col2, "EXCLAMATION_MARK")
            // If notesMark IsNot Nothing Then
            // notesMark.Visible = hasAnyNotes
            // notesMark.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#D81A1A") '("#55FFC719") 'Color.Transparent
            // End If
            // End If

        }

        private static void ProcessTreatmentWithClass(SvgImageBox svg, SvgImageItemCollection col, DentistX.Patient_ToothTrt treatment, List<DentistX.Patient_ToothTrt> allTreatments)
        {

            if (svg is null || col is null || allTreatments is null)
                return;

            // ==========================================================
            // VIEW DETECTION
            // ==========================================================
            bool isTopView1 = IsTopView(svg);
            bool isOutView1 = !isTopView1;

            // ==========================================================
            // GROUP TREATMENTS BY SHAPE (PropertyName)
            // ==========================================================
            var shapeMap = allTreatments.Where(t => !string.IsNullOrWhiteSpace(t.PropertyName)).GroupBy(t => t.PropertyName, StringComparer.OrdinalIgnoreCase).ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);

            // ==========================================================
            // GLOBAL LVL FACTS
            // ==========================================================
            bool hasLvl3 = allTreatments.Any(t => (int)t.LVL == 3);
            bool hasLvlLessThan4 = allTreatments.Any(t => (int)t.LVL < 4);

            // ==========================================================
            // HARD RESET (EVERYTHING OFF)
            // ==========================================================
            foreach (var el in col)
                el.Visible = false;

            // ==========================================================
            // BASE TOOTH VISIBILITY (AUTHORITATIVE RULE)
            // ==========================================================
            if (hasLvlLessThan4)
            {
                foreach (var el in col)
                {
                    if (el.Id is not null && el.Id.Contains("IMG") && el.Id != "CROWN_IMG")
                    {
                        el.Visible = true;
                    }
                }
            }

            // ==========================================================
            // APPLY TREATMENT SHAPES
            // ==========================================================
            foreach (var el in col)
            {

                if (el.Id is null)
                    continue;

                string propertyName = el.Id;

                if (!shapeMap.ContainsKey(propertyName))
                    continue;

                var treatments = shapeMap[propertyName];

                if (isOutView1)
                {
                    // ----------------------------------------------
                    // OUT VIEW → inclusive
                    // Show ALL treatments with LVL ≤ 3
                    // ----------------------------------------------
                    if (treatments.Any(t => (int)t.LVL <= 3))
                    {
                        el.Visible = true;
                    }
                }

                else if (isTopView1)
                {
                    // ----------------------------------------------
                    // TOP VIEW → selective
                    // ----------------------------------------------
                    if (hasLvl3)
                    {
                        // If any LVL 3 exists → show ONLY LVL 3
                        if (treatments.Any(t => (int)t.LVL == 3))
                        {
                            el.Visible = true;
                        }
                        else if (treatments.Any(t => (int)t.LVL < 3))
                        {
                            el.Visible = false;
                        }
                    }
                    // No LVL 3 → show lower levels
                    else if (treatments.Any(t => (int)t.LVL < 3))
                    {
                        el.Visible = true;
                    }
                }

            }

        }


        private static bool IsDentureTreatName(string treatText)
        {
            if (string.IsNullOrWhiteSpace(treatText))
                return false;
            switch (DentistX.Helpers.GetFirstTreatmentPart(treatText).ToUpperInvariant() ?? "")
            {
                case "CD":
                case "COMPLETE DENTURE":
                case "RPD":
                case "REMOVABLE PARTIAL DENTURE":
                    {
                        return true;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private static bool HasDentureTreatment(List<DentistX.Patient_ToothTrt> trtsList)
        {
            return trtsList is not null && trtsList.Any(t => TreatHelper.IsDentureTreatName(t.Treat));
        }

        public static void HandleHighLevelTreatments(SvgImageBox svg, List<SvgImageBox> svgExternalList, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            // Denture dominates: show only denture layers on top of base crown graphic, ignore other co-existing treat visuals (data unchanged).
            if (HasDentureTreatment(trtsList))
            {
                HandleDENTURE(svg, col, trtsList);
                return;
            }
            byte maxLevel = trtsList.Max(t => t.LVL);
            switch (maxLevel)
            {
                case 4: // EXTRACTED
                    {
                        HandleExtractionTreatment(svg, svgExternalList, col, trtsList);
                        break;
                    }
                case 5:
                case 6:
                case 7: // IMPLANT and stages
                    {
                        HandleImplantTreatment(svg, col, trtsList, maxLevel);
                        break;
                    }
                case 8: // BRIDGE
                    {
                        HandleBridgeTreatment(svg, col, trtsList);
                        break;
                    }
                case 9:
                    {
                        // Denture / mobile-band chart rows use LVL=9 on Patient_ToothTrt. Always draw them when that level wins Max(LVL).
                        // (Previously gated on isItMobTreat / TreatsUserControl "Mobile", so dentures disappeared in normal jaw mode even though data loaded.)
                        HandleDENTURE(svg, col, trtsList);
                        break;
                    }
            }
        }
        public static void HandleCrownTreatment(SvgImageBox svg, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            if (svg.Name.Contains("Top"))
            {
                ShowBaseTooth(col);
            }
            var crownFill = col.Find(c => c.Id == "CROWN_FILL");
            if (crownFill is not null)
            {
                crownFill.Visible = true;
                crownFill.Appearance.Normal.BorderThickness = 1f;
                var crownTrt = trtsList.FirstOrDefault(t => (int)t.LVL == 3);
                if (crownTrt is not null)
                {
                    crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor);
                }
            }
        }
        public static void HandleExtractionTreatment(SvgImageBox svg, List<SvgImageBox> svgExternalList, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            string extractedId = svgExternalList.Contains(svg) ? "EXTRACTION" : "EXTRACTED";
            var extracted = col.Find(c => (c.Id ?? "") == (extractedId ?? ""));
            if (extracted is not null)
            {
                // Hide all other items
                ResetSvgItemsVisibility(col);
                extracted.Appearance.Normal.FillColor = Color.Transparent;
                extracted.Visible = true;
            }
        }
        public static void HandleImplantTreatment(SvgImageBox svg, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList, int maxLevel)
        {
            // Always hide ALL layer initially
            foreach (SvgImageItem item in col)
            {
                if (item.Id != "IMPLANT")
                    item.Visible = false;
            }
            // ----------------------------
            // Always show implant if present in treatments (unless overridden by extraction-after-implant)
            // ----------------------------
            if (trtsList.Any(t => t.PropertyName.Contains("IMPLANT"))) // AndAlso Not (displayTreatment IsNot Nothing AndAlso displayTreatment.LVL = 4) Then
            {
                var implant = col.Find(c => c.Id == "IMPLANT");
                if (implant is not null)
                    implant.Visible = true;
                // Show healing cap if present
                if (trtsList.Any(t => t.PropertyName == "HEALING_CAP"))
                {
                    var heal = col.Find(c => c.Id == "HEALING_CAP");
                    if (heal is not null)
                        heal.Visible = true;
                }
                // Show abutment if present
                if (trtsList.Any(t => t.PropertyName == "ABUTMENT"))
                {
                    var abut = col.Find(c => c.Id == "ABUTMENT");
                    if (abut is not null)
                        abut.Visible = true;
                }
                // ----------------------------
                // Special handling for top view
                // ----------------------------
                if (svg.Name.Contains("Top") && maxLevel == 7)
                {
                    if (col.Find(c => c.Id == "IMPLANT") is not null)
                        col.Find(c => c.Id == "IMPLANT").Visible = false;
                    if (col.Find(c => c.Id == "ABUTMENT") is not null)
                        col.Find(c => c.Id == "ABUTMENT").Visible = false;
                    if (col.Find(c => c.Id == "HEALING_CAP") is not null)
                        col.Find(c => c.Id == "HEALING_CAP").Visible = false;
                    ShowBaseTooth(col);
                }
                else if ((svg.Name.Contains("Out") || svg.Name.Contains("IN")) && maxLevel == 7)
                {
                    if (col.Find(c => c.Id == "IMPLANT") is not null)
                        col.Find(c => c.Id == "IMPLANT").Visible = true;
                    if (col.Find(c => c.Id == "ABUTMENT") is not null)
                        col.Find(c => c.Id == "ABUTMENT").Visible = false;
                    if (col.Find(c => c.Id == "HEALING_CAP") is not null)
                        col.Find(c => c.Id == "HEALING_CAP").Visible = false;
                }
                // ----------------------------
                // Implant crowns with "I" ending
                // ----------------------------
                foreach (var t in trtsList)
                {
                    if ((int)t.LVL == 7 && t.Treat.EndsWith("I"))
                    {
                        var crownTooth = col.Find(c => c.Id == "CROWN_FILL");
                        if (crownTooth is not null)
                        {
                            crownTooth.Visible = true;
                            crownTooth.Appearance.Normal.BorderThickness = 1f;
                            foreach (var tc in trtsList)
                            {
                                switch (tc.Treat ?? "")
                                {
                                    case "METAL CROWN I":
                                    case "ZERCONIA CROWN I":
                                    case "PFM CROWN I":
                                    case "EMAX CROWN I":
                                    case "TEMP CROWN I":
                                    case "STAINLESS STEEL CROWN I":
                                        {
                                            crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            }
        }

        #region BRIDGE
        public static void HandleBridgeTreatment1(SvgImageBox svg, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            // 'Original Code
            // If svg.Name.Contains("Top") Then
            // ShowBaseTooth(col)

            // ' Show bridge mark
            // Dim bridgMark = col.Find(Function(c) c.Id = "BR")
            // If bridgMark IsNot Nothing Then
            // bridgMark.Visible = True
            // bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
            // End If
            // End If

            // ' Show crown fill with bridge style
            // Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
            // If crownFill IsNot Nothing Then
            // crownFill.Visible = True
            // Dim bridgeTrt = trtsList.FirstOrDefault(Function(t) t.LVL = 8)
            // If bridgeTrt IsNot Nothing Then
            // crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
            // End If
            // End If
            // 'End Original Code

            // ================
            // BRIDGE  
            // ' 1. Handle bridge differently per view

            if (svg.Name.Contains("Top"))
            {
                foreach (SvgImageItem item in col)
                    item.Visible = false;
                // Top View - style the base tooth as crown
                ShowBaseTooth(col);
                // Show BRIDGE Mark
                var bridgMark = col.Find(c => c.Id == "BR");
                if (bridgMark is not null)
                {
                    bridgMark.Visible = true;
                    bridgMark.Appearance.Normal.FillColor = DentistX.Helpers.GetCutomTrtColorByProp("BRIDGEMARK");
                }
            }
            else if (svg.Name.Contains("In") || svg.Name.Contains("IN") || svg.Name.Contains("Out"))
            {

                // if tooth not extracted, show normal treats and then bridge crown
                // normal treats levels:0,1,2,3
                // extraction level =4
                // imolant level =5,6,7
                // bridge level =8
                foreach (var t in trtsList)
                {
                    var item = col.Find(c => (c.Id ?? "") == (t.PropertyName ?? ""));
                    if (item is not null)
                    {
                        item.Visible = true;
                        item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                        item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                        item.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                    }
                }
                var trts = trtsList.Where(t => (int)t.LVL < 4).ToList();
                if (trts.Any())
                {
                    ShowBaseTooth(col);
                    foreach (var t in trts)
                    {
                        var item = col.Find(c => (c.Id ?? "") == (t.PropertyName ?? ""));
                        if (item is not null)
                        {
                            item.Visible = true;
                            item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                            item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                            item.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
                        }
                    }
                }

                // 2. Always show CROWN_IMG if IMPLANT exists
                if (trtsList.Any(t => t.PropertyName.Contains("IMPLANT")))
                {
                    var IMPLANT = col.Find(c => c.Id == "IMPLANT");
                    if (IMPLANT is not null)
                        IMPLANT.Visible = true;
                    // Out View - show dedicated crown layer
                    var crownImg = col.Find(c => c.Id == "CROWN_IMG"); // Fixed typo
                    if (crownImg is not null)
                    {
                        crownImg.Visible = true;
                    }
                }
                // Show BRIDGE Mark
                var bridgMark = col.Find(c => c.Id == "BR");
                if (bridgMark is not null)
                {
                    bridgMark.Visible = true;
                    bridgMark.Appearance.Normal.FillColor = DentistX.Helpers.GetCutomTrtColorByProp("BRIDGEMARK");
                }
                // Out View - show dedicated crown layer
                var crownItem = col.Find(c => c.Id == "CROWN_IMG"); // Fixed typo
                if (crownItem is not null)
                {
                    crownItem.Visible = true;
                }
                // 3. Always show extraction with Yellow for bridge
                var EXTRACTED1 = col.Find(c => c.Id == "EXTRACTED");
                if (EXTRACTED1 is not null)
                    EXTRACTED1.Visible = true;
                EXTRACTED1.Appearance.Normal.FillColor = Color.Transparent;
                EXTRACTED1.Appearance.Normal.BorderColor = Color.Yellow;
                EXTRACTED1.Appearance.Normal.BorderThickness = 4f;
            }
            // Show Crown Fill with bridge style
            var crownFill = col.Find(c => c.Id == "CROWN_FILL");
            if (crownFill is not null)
            {
                crownFill.Visible = true;
                foreach (var t in trtsList)
                {
                    switch (t.Treat ?? "")
                    {
                        case "METAL BRIDGE":
                        case "ZERCONIA BRIDGE":
                        case "PFM BRIDGE":
                        case "EMAX BRIDGE":
                        case "TEMP BRIDGE":
                        case "STAINLESS STEEL BRIDGE":
                            {
                                if (crownFill is not null)
                                    crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                                break;
                            }
                    }
                }
            }
        }

        public static void HandleBridgeTreatment(SvgImageBox svg, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            // ---------- TOP VIEW ----------
            if (svg.Name.Contains("Top"))
            {
                foreach (SvgImageItem item in col)
                    item.Visible = false;
                ShowBaseTooth(col);
                ShowBridgeMark(col);
                ApplyBridgeCrownFill(col, trtsList);
                return;
            }
            // ---------- IN / OUT VIEWS ----------
            if (!(svg.Name.IndexOf("In", StringComparison.OrdinalIgnoreCase) >= 0 || svg.Name.IndexOf("Out", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                return;
            }
            // ---------- CLASSIFY ----------
            var normalTrts = trtsList.Where(t => (int)t.LVL < 4).ToList();
            bool hasExtraction = trtsList.Any(t => (int)t.LVL == 4);
            bool hasImplant = trtsList.Any(t => (int)t.LVL >= 5 && (int)t.LVL <= 7);
            bool hasBridge = trtsList.Any(t => t.Treat.IndexOf("BRIDGE", StringComparison.OrdinalIgnoreCase) >= 0);
            // Reset everything ONCE
            foreach (SvgImageItem item in col)
                item.Visible = false;
            // ---------- RENDER ----------
            switch (true)
            {
                // ---- IMPLANT ----
                case object _ when hasImplant:
                    {
                        ShowItem(col, "IMPLANT");
                        ShowItem(col, "CROWN_IMG");
                        break;
                    }
                // ---- EXTRACTED ----
                case object _ when hasExtraction:
                    {
                        var extracted = col.Find(c => c.Id == "EXTRACTED");
                        if (extracted is not null)
                        {
                            extracted.Visible = true;
                            extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#55FFC719"); // Color.Transparent
                            extracted.Appearance.Normal.BorderColor = ColorTranslator.FromHtml("#55FFC719"); // Color.Yellow
                            extracted.Appearance.Normal.BorderThickness = 4f;
                        }
                        ShowItem(col, "CROWN_IMG");
                        break;
                    }
                // ---- NORMAL or BRIDGE-ONLY ----
                case object _ when normalTrts.Any() || hasBridge:
                    {
                        ShowBaseTooth(col);
                        ApplyTreatments(col, normalTrts);
                        ShowItem(col, "CROWN_IMG");
                        break;
                    }
            }
            // ---------- BRIDGE VISUALS (always if bridge exists) ----------
            if (hasBridge)
            {
                ShowBridgeMark(col);
                ApplyBridgeCrownFill(col, trtsList);
                // ---------- BRIDGE EXTRACTION OUTLINE (always when bridge exists) ----------
                var extracted = col.Find(c => c.Id == "EXTRACTED");
                if (extracted is not null)
                {
                    extracted.Visible = true;
                    extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#80EDF3FA"); // Color.Transparent
                    extracted.Appearance.Normal.BorderColor = Color.White; // FromArgb(12, 237, 243, 250)
                    extracted.Appearance.Normal.BorderThickness = 2f;
                }
            }
        }

        private static void ApplyTreatments(SvgImageItemCollection col, IEnumerable<DentistX.Patient_ToothTrt> trts)
        {
            foreach (var t in trts)
            {
                var item = col.Find(c => (c.Id ?? "") == (t.PropertyName ?? ""));
                if (item is null)
                    continue;
                item.Visible = true;
                item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor);
                item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor);
                item.Appearance.Normal.BorderThickness = (float)t.BorderThickness;
            }
        }

        private static void ShowItem(SvgImageItemCollection col, string id)
        {
            var item = col.Find(c => (c.Id ?? "") == (id ?? ""));
            if (item is not null)
                item.Visible = true;
        }

        private static void ShowBridgeMark(SvgImageItemCollection col)
        {
            var br = col.Find(c => c.Id == "BR");
            if (br is not null)
            {
                br.Visible = true;
                br.Appearance.Normal.FillColor = DentistX.Helpers.GetCutomTrtColorByProp("BRIDGEMARK");
            }
        }

        private static void ApplyBridgeCrownFill(SvgImageItemCollection col, IEnumerable<DentistX.Patient_ToothTrt> trtsList)
        {
            var crownFill = col.Find(c => c.Id == "CROWN_FILL");
            if (crownFill is null)
                return;
            var bridgeTrt = trtsList.FirstOrDefault(t => LikeOperator.LikeString(t.Treat, "*BRIDGE*", CompareMethod.Binary));
            if (bridgeTrt is not null)
            {
                crownFill.Visible = true;
                crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor);
            }
        }


        #endregion




        public static void HandleDENTURE(SvgImageBox svg, SvgImageItemCollection col, List<DentistX.Patient_ToothTrt> trtsList)
        {
            ResetSvgItemsVisibility(col);
            var dentRow = trtsList.FirstOrDefault(t => TreatHelper.IsDentureTreatName(t.Treat));
            if (svg.Name.Contains("Top"))
            {
                ShowBaseTooth(col);
                var dentMark = col.Find(c => c.Id == "CH");
                if (dentMark is not null)
                {
                    dentMark.Visible = true;
                    dentMark.Appearance.Normal.FillColor = DentistX.Helpers.GetCutomTrtColorByProp("DENTUREMARK");
                }
            }
            else if (svg.Name.Contains("Out") || svg.Name.Contains("IN"))
            {
                var extracted = col.Find(c => c.Id == "EXTRACTED");
                if (extracted is not null)
                    extracted.Visible = false;
                var dentMark = col.Find(c => c.Id == "CH");
                if (dentMark is not null)
                {
                    dentMark.Visible = true;
                    dentMark.Appearance.Normal.FillColor = DentistX.Helpers.GetCutomTrtColorByProp("DENTUREMARK");
                }
                var crownItem = col.Find(c => c.Id == "CROWN_IMG");
                if (crownItem is not null)
                {
                    crownItem.Visible = true;
                    if (dentRow is not null)
                    {
                        crownItem.Appearance.Normal.FillColor = DentistX.Helpers.GetCustomTrtColor(dentRow.Treat);
                        crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(dentRow.BorderColor);
                    }
                }
            }
        }
        #endregion
        private static bool IsOutView(SvgImageBox svg)
        {
            return svg.Name.Contains("Out") || svg.Name.Contains("OUT");
        }
        private static bool IsInView(SvgImageBox svg)
        {
            return svg.Name.Contains("In") || svg.Name.Contains("IN");
        }
        private static bool IsTopView(SvgImageBox svg)
        {
            return svg.Name.Contains("Top") || svg.Name.Contains("TOP");
        }

        private static SvgImageItem FindSvgItemById(SvgImageItemCollection items, string targetId)
        {
            foreach (SvgImageItem item in items)
            {
                // Direct hit
                if (string.Equals(item.Id, targetId, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
                // Dive into groups
                if (item.Items is not null && item.Items.Count > 0)
                {
                    var found = FindSvgItemById(item.Items, targetId);
                    if (found is not null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }



    }
}