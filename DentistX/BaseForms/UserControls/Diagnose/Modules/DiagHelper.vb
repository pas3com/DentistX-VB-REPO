


Imports System.Drawing
Imports DevExpress.XtraEditors

Module DiagHelper

    Public isItMobTreat As Boolean = False

    ' Opaque warm beige ≈ midpoint of AntiqueWhite→Wheat diagonal (external diagnosis highlight); skips Svg BackgroundImage gradients.
    Friend ReadOnly ExternalTreatSvgSolidApprox As Color = Color.FromArgb(248, 233, 204)

#Region "TreatCode"
    Public Sub LoadTeethTreatsUsingTreatCode(cntrl As Control, svgExternalList As List(Of SvgImageBox),
                                            svgDiagList As List(Of SvgImageBox), patientTreats As IEnumerable(Of Patient_Diagnosis))
        cntrl.SuspendLayout()
        For Each ct As Control In cntrl.Controls.OfType(Of SvgImageBox)()
            Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
            svg.BeginUpdate()  ' <== if SvgImageBox supports it
            'ProcessToothTreatments(svg, svgExternalList, svgDiagList, patientTreats)
            svg.EndUpdate()
        Next
        cntrl.ResumeLayout()
    End Sub
    Public Sub ProcessToothTreatments1(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox),
                                       svgDiagList As List(Of SvgImageBox), patientTreats As IEnumerable(Of Patient_Diagnosis))
        ClearSvgBackground(svg)
        Dim col As SvgImageItemCollection = svg.RootItems
        Dim toothNum As Byte = CByte(svg.Tag)
        ' Get treatments for this specific tooth
        'Dim orderedTrts = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderByDescending(Function(t) t.TreatDate)
        'Dim trtsList = orderedTrts.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList()
        Dim trtsList = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList
        ' Reset all items invisible first
        ResetSvgItemsVisibility(col)
        ' Handle special cases
        HandleDiagnosTreatments(svg, svgDiagList, col, trtsList)
        HandleExternalTreatments(svg, svgExternalList, col, trtsList)
        ' If no treatments, just show base tooth and exit
        If trtsList.Count = 0 Then
            ShowBaseTooth(col)
            Return
        End If
        ' Process each treatment layer
        ProcessTreatmentLayers1(svg, svgExternalList, svgDiagList, col, trtsList)
    End Sub

    Public Sub ProcessToothTreatments(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox),
                                        patientTreats As IEnumerable(Of Patient_Diagnosis),
                                        Optional useHeavySvgGradientBackground As Boolean = True)
        ClearSvgBackground(svg)
        Dim col As SvgImageItemCollection = svg.RootItems
        Dim toothNum As Byte = CByte(svg.Tag)
        ' Get treatments for this specific tooth
        'Dim orderedTrts = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderByDescending(Function(t) t.TreatDate)
        'Dim trtsList = orderedTrts.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList()
        Dim trtsList = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList
        ' Reset all items invisible first
        ResetSvgItemsVisibility(col)
        ' Handle special cases

        HandleExternalTreatments(svg, svgExternalList, col, trtsList, useHeavySvgGradientBackground)
        ' If no treatments, just show base tooth and exit
        If trtsList.Count = 0 Then
            ShowBaseTooth(col)
            Return
        End If
        ' Process each treatment layer
        ProcessTreatmentLayers(svg, svgExternalList, col, trtsList)
    End Sub
    '=====================
    Public Sub ResetSvgItemsVisibility(col As SvgImageItemCollection)
        For Each item As SvgImageItem In col
            item.Visible = False
        Next
    End Sub
    Public Sub ShowBaseTooth(col As SvgImageItemCollection)
        Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG")
        If baseTooth IsNot Nothing Then baseTooth.Visible = True
    End Sub
    Public Sub HandleExternalTreatments(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox), col As SvgImageItemCollection,
                                         trtsList As List(Of Patient_Diagnosis), Optional useHeavySvgGradientBackground As Boolean = True)
        Dim externalTrts = trtsList.Where(Function(t) t.IsExternal.HasValue AndAlso t.IsExternal.Value = True).ToList()
        If externalTrts.Any() Then
            If useHeavySvgGradientBackground Then
                ApplyGradientBackground(svg,
                             Color.AntiqueWhite,
                              Color.Wheat,
                              Drawing2D.LinearGradientMode.ForwardDiagonal,
                              128)
            Else
                ClearSvgBackground(svg)
                svg.BackColor = ExternalTreatSvgSolidApprox
            End If
            svgExternalList.Add(svg)
        End If
    End Sub

    Public Sub HandleDiagnosTreatments(svg As SvgImageBox, svgDiagList As List(Of SvgImageBox), col As SvgImageItemCollection,
                                         trtsList As List(Of Patient_Diagnosis))
        Dim diagTrts = trtsList.Where(Function(t) t.Treat.Contains("DIAGNOSIS")).ToList()
        If diagTrts.Any() Then
            ApplyGradientBackground(svg,
                              Color.Blue,
                              Color.AliceBlue,
                              Drawing2D.LinearGradientMode.BackwardDiagonal,
                              128)
            svgDiagList.Add(svg)
        End If
    End Sub
    '======================

    Public Sub ProcessTreatmentLayers1(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox),
                                      svgDiagList As List(Of SvgImageBox), col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        ' Show base tooth for levels 0-2
        If trtsList.Max(Function(t) t.LVL) < 4 OrElse isItMobTreat = False Then
            ShowBaseTooth(col)
        End If
        ' Process each treatment
        For Each t In trtsList
            ProcessIndividualTreatment(svg, col, t, trtsList)
        Next
        ' Handle special high-level cases
        HandleDiagnosTreatments(svg, svgDiagList, col, trtsList)
        HandleHighLevelTreatments(svg, svgExternalList, col, trtsList)
    End Sub

    Public Sub ProcessTreatmentLayers(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox),
                                       col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        ' Show base tooth for levels 0-2
        If trtsList.Max(Function(t) t.LVL) < 4 OrElse isItMobTreat = False Then
            ShowBaseTooth(col)
        End If
        ' Process each treatment
        For Each t In trtsList
            ProcessIndividualTreatment(svg, col, t, trtsList)
        Next
        ' Handle special high-level cases
        HandleHighLevelTreatments(svg, svgExternalList, col, trtsList)
        Dim hasAnyNotes = trtsList.Any(Function(tr) tr.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrEmpty(tr.TreatNotes))
        Dim notesMark = FindSvgItemById(col, "EXCLAMATION_MARK")
        If notesMark IsNot Nothing Then
            notesMark.Visible = hasAnyNotes
            notesMark.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#D81A1A") '("#55FFC719") 'Color.Transparent
        End If
    End Sub

    ' Declare this at the class/scope level, not inside a method that runs often
    Public ReadOnly _clasesSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
    "CLASS 1 AMALGAM", "CLASS 1 COMPOSITE", "CLASS 1 GI", "CLASS 1 TF",
    "CLASS 2 D AMALGAM", "CLASS 2 D COMPOSITE", "CLASS 2 D GI", "CLASS 2 D TF",
    "CLASS 2 M AMALGAM", "CLASS 2 M COMPOSITE", "CLASS 2 M GI", "CLASS 2 M TF",
    "CLASS 2 MOD AMALGAM", "CLASS 2 MOD COMPOSITE", "CLASS 2 MOD GI", "CLASS 2 MOD TF",
    "CLASS 4 INCISAL", "REDO CLASS 1 AMALGAM", "REDO CLASS 1 COMPOSITE", "REDO CLASS 1 GI", "REDO CLASS 1 TF",
    "REDO CLASS 2 D AMALGAM", "REDO CLASS 2 D COMPOSITE", "REDO CLASS 2 D GI", "REDO CLASS 2 D TF",
    "REDO CLASS 2 M AMALGAM", "REDO CLASS 2 M COMPOSITE", "REDO CLASS 2 M GI", "REDO CLASS 2 M TF",
    "REDO CLASS 2 MOD AMALGAM", "REDO CLASS 2 MOD COMPOSITE", "REDO CLASS 2 MOD GI", "REDO CLASS 2 MOD TF",
    "REDO CLASS 4 INCISAL"}

    Public Sub ProcessIndividualTreatmentBad(svg As SvgImageBox, col As SvgImageItemCollection,
                                  treatment As Patient_Diagnosis, allTreatments As List(Of Patient_Diagnosis))

        Dim hasBuildUp As Boolean = allTreatments.Any(Function(t) t.LVL = 3 AndAlso t.Treat.StartsWith("BUILD"))
        Dim hasCrownT As Boolean = allTreatments.Any(Function(t) t.LVL = 3 AndAlso t.Treat.EndsWith(" T"))

        ' Find the treatment item
        'Dim item = col.Find(Function(c) c.Id = treatment.PropertyName)
        Dim item = FindSvgItemById(col, treatment.PropertyName)
        If item Is Nothing Then Return

        ' Apply basic styling to ALL items first
        item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
        item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
        item.Appearance.Normal.BorderThickness = treatment.BorderThickness

        ' ==========================================================
        ' CRITICAL FIX: Level 1 and 2 treatments should ALWAYS be visible
        ' ==========================================================
        If treatment.LVL = 1 OrElse treatment.LVL = 2 Then
            item.Visible = True
        End If

        ' ==========================================================
        ' OUT VIEW + TOP VIEW RULES
        ' ==========================================================
        If IsOutView(svg) OrElse IsTopView(svg) Then
            ' Always show root treatments
            Dim rootTreatments = allTreatments.Where(Function(t) t.TrtLoc = "ROOT" OrElse t.TrtLoc = "BOTH").ToList()
            For Each rootTrt In rootTreatments
                'Dim rootItem = col.Find(Function(c) c.Id = rootTrt.PropertyName)
                Dim rootItem = FindSvgItemById(col, rootTrt.PropertyName)
                If rootItem IsNot Nothing Then
                    rootItem.Visible = True
                    rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(rootTrt.FillColor)
                    rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(rootTrt.BorderColor)
                    rootItem.Appearance.Normal.BorderThickness = rootTrt.BorderThickness
                End If
            Next

            ' Build Up and Crown logic - but DON'T affect level 1/2 treatments
            If hasBuildUp AndAlso hasCrownT Then
                ' Hide Build Up
                For Each item1 In col
                    If item1.Id IsNot Nothing AndAlso item1.Id.StartsWith("BUILD_UP") Then item1.Visible = False
                Next
                ' Show Crown Fill
                Dim crownTrt = allTreatments.FirstOrDefault(Function(t) t.LVL = 3 AndAlso t.Treat.EndsWith(" T"))
                If crownTrt IsNot Nothing Then
                    'Dim crownItem = col.Find(Function(c) c.Id.StartsWith("CROWN") OrElse c.Id = crownTrt.PropertyName)
                    Dim crownItem = FindSvgItemById(col, "CROWN_FILL")
                    If crownItem Is Nothing Then crownItem = FindSvgItemById(col, crownTrt.PropertyName)
                    If crownItem IsNot Nothing Then
                        crownItem.Visible = True
                        crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor)
                        crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(crownTrt.BorderColor)
                        crownItem.Appearance.Normal.BorderThickness = crownTrt.BorderThickness
                    End If
                End If
            ElseIf hasBuildUp AndAlso Not hasCrownT Then
                ' Show Build Up only
                Dim buildUpTrt = allTreatments.FirstOrDefault(Function(t) t.LVL = 3 AndAlso t.Treat.StartsWith("BUILD"))
                If buildUpTrt IsNot Nothing Then
                    'Dim buildUpItem = col.Find(Function(c) c.Id.StartsWith("BUILD_UP") OrElse c.Id = buildUpTrt.PropertyName)
                    Dim buildUpItem = FindSvgItemById(col, buildUpTrt.PropertyName)
                    If buildUpItem Is Nothing Then buildUpItem = FindSvgItemById(col, "BUILD_UP")
                    If buildUpItem IsNot Nothing Then
                        buildUpItem.Visible = True
                        buildUpItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(buildUpTrt.FillColor)
                        buildUpItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(buildUpTrt.BorderColor)
                        buildUpItem.Appearance.Normal.BorderThickness = buildUpTrt.BorderThickness
                    End If
                End If
            ElseIf hasCrownT AndAlso Not hasBuildUp Then
                ' Show Crown Fill only
                Dim crownTrt = allTreatments.FirstOrDefault(Function(t) t.LVL = 3 AndAlso t.Treat.EndsWith(" T"))
                If crownTrt IsNot Nothing Then
                    'Dim crownItem = col.Find(Function(c) c.Id.StartsWith("CROWN") OrElse c.Id = crownTrt.PropertyName)
                    Dim crownItem = FindSvgItemById(col, "CROWN_FILL")
                    If crownItem Is Nothing Then crownItem = FindSvgItemById(col, crownTrt.PropertyName)
                    If crownItem IsNot Nothing Then
                        crownItem.Visible = True
                        crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor)
                        crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(crownTrt.BorderColor)
                        crownItem.Appearance.Normal.BorderThickness = crownTrt.BorderThickness
                    End If
                End If
            End If
        End If ' OUT/TOP VIEW END

        ' ==========================================================
        ' NORMAL TREATMENT DISPLAY - FIXED VERSION
        ' ==========================================================
        ' Level 1 and 2 treatments are already set to visible above
        ' For other treatments, use the original logic but don't interfere with level 1/2
        If treatment.LVL <> 1 AndAlso treatment.LVL <> 2 Then
            If treatment.TrtLoc = "ROOT" OrElse Not (hasBuildUp Or hasCrownT) Then
                item.Visible = True
                item.Appearance.Normal.FillColor = BackClr
                item.Appearance.Normal.BorderThickness = 0
            End If
        End If

        ' ==========================================================
        ' SPECIAL CASES (keep your existing code, but ensure level 1/2 visibility)
        ' ==========================================================
        If treatment.LVL = 0 AndAlso treatment.Treat.Contains("VENEERS") Then
            'Dim crownItem = col.Find(Function(c) c.Id = "CROWN")
            Dim crownItem = FindSvgItemById(col, "CROWN")
            If crownItem IsNot Nothing AndAlso (treatment.TrtLoc = "ROOT" OrElse Not (hasBuildUp Or hasCrownT)) Then
                crownItem.Visible = True
                crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
            End If
        ElseIf treatment.LVL = 0 AndAlso treatment.Treat = "APEXIFICATION" Then
            item.Visible = True
        ElseIf treatment.LVL = 0 AndAlso treatment.Treat = "DIRECT PULP CAPPING" Then
            item.Visible = True
            item.Appearance.Normal.BorderThickness = 2
            'Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
            Dim indir = FindSvgItemById(col, "INDIRECT_PULP_CAPPING")
            If indir IsNot Nothing Then
                indir.Visible = True
                indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
            End If
        ElseIf treatment.LVL = 0 AndAlso treatment.PropertyName = "CROWN_LENGTHENING" Then
            item.Visible = True
        ElseIf treatment.LVL = 1 AndAlso treatment.Treat = "APICECTOMY" Then
            ' This should already be visible from our fix above, but ensure it
            item.Visible = True
            item.Appearance.Normal.FillColor = BackClr
            item.Appearance.Normal.BorderThickness = 0
        ElseIf treatment.LVL = 2 AndAlso treatment.Treat = "HEMISECTION" Then
            ' This should already be visible from our fix above, but ensure it
            item.Visible = True
            item.Appearance.Normal.FillColor = BackClr
            item.Appearance.Normal.BorderThickness = 0
        End If

        ' ==========================================================
        ' TOP VIEW LOGIC (keep your existing code)
        ' ==========================================================
        If svg.Name.Contains("Top") Then
            Dim rct = allTreatments.Any(Function(t) t.Treat = "RCT" OrElse t.Treat.StartsWith("REDO R") OrElse t.Treat = "PULPECTOMY")
            Dim clas = allTreatments.Any(Function(t) _clasesSet.Contains(t.Treat))
            Dim post = allTreatments.Any(Function(t) t.Treat = "FIBER POST" OrElse t.Treat = "METAL POST")
            ' Always apply hide logic when Crown or BuildUp exist
            If hasBuildUp Or hasCrownT Then
                For Each t In allTreatments
                    ' Hide class
                    If _clasesSet.Contains(t.Treat) Then
                        'Dim classItem = col.Find(Function(c) c.Id = t.PropertyName)
                        Dim classItem = FindSvgItemById(col, t.PropertyName)
                        If classItem IsNot Nothing Then classItem.Visible = False
                    End If
                    ' Hide RCT, POST, PULP
                    If t.Treat.StartsWith("RC") OrElse t.Treat.StartsWith("REDO R") OrElse
                   t.Treat = "PULPECTOMY" OrElse t.Treat = "FIBER POST" OrElse t.Treat = "METAL POST" Then
                        'Dim rc = col.Find(Function(c) c.Id = "RCT")
                        'Dim postF = col.Find(Function(c) c.Id = "FIBER_POST_T")
                        'Dim postM = col.Find(Function(c) c.Id = "METAL_POST_T")
                        'Dim pulp = col.Find(Function(c) c.Id = "PULPECTOMY")
                        Dim rc = FindSvgItemById(col, "RCT")
                        Dim postF = FindSvgItemById(col, "FIBER_POST_T")
                        Dim postM = FindSvgItemById(col, "METAL_POST_T")
                        Dim pulp = FindSvgItemById(col, "PULPECTOMY")
                        If rc IsNot Nothing Then rc.Visible = False
                        If postF IsNot Nothing Then postF.Visible = False
                        If postM IsNot Nothing Then postM.Visible = False
                        If pulp IsNot Nothing Then pulp.Visible = False
                    End If
                Next
            End If
            ' Hide RCT if both RCT/Class or POST/Class coexist
            If Not hasBuildUp AndAlso Not hasCrownT Then
                If (rct AndAlso clas) OrElse (post AndAlso clas) Then
                    'Dim rc = col.Find(Function(c) c.Id = "RCT")
                    'Dim postF = col.Find(Function(c) c.Id = "FIBER_POST_T")
                    'Dim postM = col.Find(Function(c) c.Id = "METAL_POST_T")
                    'Dim pulp = col.Find(Function(c) c.Id = "PULPECTOMY")
                    Dim rc = FindSvgItemById(col, "RCT")
                    Dim postF = FindSvgItemById(col, "FIBER_POST_T")
                    Dim postM = FindSvgItemById(col, "METAL_POST_T")
                    Dim pulp = FindSvgItemById(col, "PULPECTOMY")
                    If rc IsNot Nothing Then rc.Visible = False
                    If postF IsNot Nothing Then postF.Visible = False
                    If postM IsNot Nothing Then postM.Visible = False
                    If pulp IsNot Nothing Then pulp.Visible = False
                End If
            End If
        End If
    End Sub

    Public Sub ProcessIndividualTreatment(svg As SvgImageBox, col As SvgImageItemCollection,
                                      treatment As Patient_Diagnosis, allTreatments As List(Of Patient_Diagnosis))

        ' ==========================================================
        ' MAIN VISIBILITY RULES SUMMARY
        ' ==========================================================
        ' 1. Both Build Up + Crown T → show only Crown T + Root.
        ' 2. Build Up only → show Build Up + Root.
        ' 3. Crown T only → show Crown T + Root.
        ' 4. None → normal visibility.
        ' ==========================================================

        Dim hasBuildUp As Boolean = allTreatments.Any(Function(t) t.LVL = 3 AndAlso t.Treat.StartsWith("BUILD"))
        Dim hasCrownT As Boolean = allTreatments.Any(Function(t) t.LVL = 3 AndAlso t.Treat.EndsWith(" T"))
        ' ==========================================================
        ' OUT VIEW + TOP VIEW RULES
        ' ==========================================================
        If IsOutView(svg) OrElse IsTopView(svg) Then
            ' Always show root treatments
            Dim rootTreatments = allTreatments.Where(Function(t) t.TrtLoc = "ROOT" OrElse t.TrtLoc = "BOTH").ToList()
            For Each rootTrt In rootTreatments
                'Dim rootItem = col.Find(Function(c) c.Id = rootTrt.PropertyName)
                Dim rootItem = FindSvgItemById(col, rootTrt.PropertyName)
                If rootItem IsNot Nothing Then
                    rootItem.Visible = True
                    rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(rootTrt.FillColor)
                    rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(rootTrt.BorderColor)
                    rootItem.Appearance.Normal.BorderThickness = rootTrt.BorderThickness
                End If
            Next
            ' ======================================================
            ' CASE 1: BOTH BUILD UP AND CROWN T
            ' ======================================================
            If hasBuildUp AndAlso hasCrownT Then
                ' Hide Build Up
                For Each item1 In col
                    If item1.Id IsNot Nothing AndAlso item1.Id.StartsWith("BUILD_UP") Then item1.Visible = False
                Next
                ' Show Crown Fill
                Dim crownTrt = allTreatments.FirstOrDefault(Function(t) t.LVL = 3 AndAlso t.Treat.EndsWith(" T"))
                If crownTrt IsNot Nothing Then
                    'Dim crownItem = col.Find(Function(c) c.Id.StartsWith("CROWN") OrElse c.Id = crownTrt.PropertyName)
                    Dim crownItem = FindSvgItemById(col, "CROWN_FILL")
                    If crownItem Is Nothing Then crownItem = FindSvgItemById(col, crownTrt.PropertyName)
                    If crownItem IsNot Nothing Then
                        crownItem.Visible = True
                        crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor)
                        crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(crownTrt.BorderColor)
                        crownItem.Appearance.Normal.BorderThickness = crownTrt.BorderThickness
                    End If
                End If

                If treatment.TrtLoc = "CROWN" Then Return
            End If
            ' ======================================================
            ' CASE 2: BUILD UP ONLY
            ' ======================================================
            If hasBuildUp AndAlso Not hasCrownT Then
                If IsTopView(svg) Then
                    For Each item2 In col
                        item2.Visible = False
                    Next
                    ShowBaseTooth(col)
                End If
                Dim buildUpTrt = allTreatments.FirstOrDefault(Function(t) t.LVL = 3 AndAlso t.Treat.StartsWith("BUILD"))
                If buildUpTrt IsNot Nothing Then
                    'Dim buildUpItem = col.Find(Function(c) c.Id.StartsWith("BUILD_UP") OrElse c.Id = buildUpTrt.PropertyName)
                    Dim buildUpItem = FindSvgItemById(col, buildUpTrt.PropertyName)
                    If buildUpItem Is Nothing Then buildUpItem = FindSvgItemById(col, "BUILD_UP")
                    If buildUpItem IsNot Nothing Then
                        buildUpItem.Visible = True
                        buildUpItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(buildUpTrt.FillColor)
                        buildUpItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(buildUpTrt.BorderColor)
                        buildUpItem.Appearance.Normal.BorderThickness = buildUpTrt.BorderThickness
                    End If
                End If
                ' Hide Crown visuals
                For Each item2 In col
                    If item2.Id IsNot Nothing AndAlso item2.Id.StartsWith("CROWN") Then item2.Visible = False
                Next
                If treatment.TrtLoc = "CROWN" Then Return
            End If
            ' ======================================================
            ' CASE 3: CROWN T ONLY
            ' ======================================================
            If hasCrownT AndAlso Not hasBuildUp Then
                ' Hide Build Ups
                For Each item3 In col
                    If item3.Id IsNot Nothing AndAlso item3.Id.StartsWith("BUILD_UP") Then item3.Visible = False
                Next
                ' Show Crown Fill (robust lookup)
                Dim crownTrt = allTreatments.FirstOrDefault(Function(t) t.LVL = 3 AndAlso t.Treat.EndsWith(" T"))
                If crownTrt IsNot Nothing Then
                    'Dim crownItem = col.Find(Function(c) c.Id.StartsWith("CROWN") OrElse c.Id = crownTrt.PropertyName)
                    Dim crownItem = FindSvgItemById(col, "CROWN_FILL")
                    If crownItem Is Nothing Then crownItem = FindSvgItemById(col, crownTrt.PropertyName)
                    If crownItem IsNot Nothing Then
                        crownItem.Visible = True
                        crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor)
                        crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(crownTrt.BorderColor)
                        crownItem.Appearance.Normal.BorderThickness = crownTrt.BorderThickness
                    End If
                End If
                If treatment.TrtLoc = "CROWN" Then Return
            End If
        End If ' OUT/TOP VIEW END
        ' ==========================================================
        ' NORMAL TREATMENT DISPLAY
        ' ==========================================================
        'Dim item = col.Find(Function(c) c.Id = treatment.PropertyName)
        Dim item = FindSvgItemById(col, treatment.PropertyName)
        If item Is Nothing Then Return
        item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
        item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
        item.Appearance.Normal.BorderThickness = treatment.BorderThickness
        If treatment.TrtLoc = "ROOT" OrElse Not (hasBuildUp Or hasCrownT) Then
            item.Visible = True
        End If
        ' ==========================================================
        ' SPECIAL CASES (PULP, APEX, ETC.)
        ' ==========================================================
        If treatment.LVL = 0 AndAlso treatment.Treat.Contains("VENEERS") Then
            'Dim crownItem = col.Find(Function(c) c.Id = "CROWN")
            Dim crownItem = FindSvgItemById(col, "CROWN")
            If crownItem IsNot Nothing AndAlso (treatment.TrtLoc = "ROOT" OrElse Not (hasBuildUp Or hasCrownT)) Then
                crownItem.Visible = True
                crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
            End If
        ElseIf treatment.LVL = 0 AndAlso treatment.Treat = "APEXIFICATION" Then
            item.Visible = True
        ElseIf treatment.LVL = 0 AndAlso treatment.Treat = "DIRECT PULP CAPPING" Then
            item.Visible = True
            item.Appearance.Normal.BorderThickness = 2
            'Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
            Dim indir = FindSvgItemById(col, "INDIRECT_PULP_CAPPING")
            If indir IsNot Nothing Then
                indir.Visible = True
                indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
            End If
        ElseIf treatment.LVL = 0 AndAlso treatment.PropertyName = "CROWN_LENGTHENING" Then
            item.Visible = True
        ElseIf treatment.LVL = 1 AndAlso treatment.Treat = "APICECTOMY" Then
            'Dim apic = col.Find(Function(c) c.Id = "APICECTOMY") 'APICECTOMY
            Dim apic = FindSvgItemById(col, "APICECTOMY")
            If apic IsNot Nothing Then
                apic.Appearance.Normal.FillColor = BackClr
                apic.Appearance.Normal.BorderThickness = 0
                apic.Visible = True
            End If
        ElseIf treatment.LVL = 2 AndAlso treatment.Treat = "HEMISECTION" Then
            'Dim hemi = col.Find(Function(c) c.Id = "HEMISECTION")
            Dim hemi = FindSvgItemById(col, "HEMISECTION")
            If hemi IsNot Nothing Then
                hemi.Appearance.Normal.FillColor = BackClr
                hemi.Appearance.Normal.BorderThickness = 0
                hemi.Visible = True
            End If
        End If
        ' ==========================================================
        ' TOP VIEW LOGIC (ALWAYS EXECUTES IF Top View)
        ' ==========================================================
        If svg.Name.Contains("Top") Then
            Dim rct = allTreatments.Any(Function(t) t.Treat = "RCT" OrElse t.Treat.StartsWith("REDO R") OrElse t.Treat = "PULPECTOMY")
            Dim clas = allTreatments.Any(Function(t) _clasesSet.Contains(t.Treat))
            Dim post = allTreatments.Any(Function(t) t.Treat = "FIBER POST" OrElse t.Treat = "METAL POST")
            ' Always apply hide logic when Crown or BuildUp exist
            If hasBuildUp Or hasCrownT Then
                For Each t In allTreatments
                    ' Hide class
                    If _clasesSet.Contains(t.Treat) Then
                        'Dim classItem = col.Find(Function(c) c.Id = t.PropertyName)
                        Dim classItem = FindSvgItemById(col, t.PropertyName)
                        If classItem IsNot Nothing Then classItem.Visible = False
                    End If
                    ' Hide RCT, POST, PULP
                    If t.Treat.StartsWith("RC") OrElse t.Treat.StartsWith("REDO R") OrElse
                   t.Treat = "PULPECTOMY" OrElse t.Treat = "FIBER POST" OrElse t.Treat = "METAL POST" Then
                        'Dim rc = col.Find(Function(c) c.Id = "RCT")
                        'Dim postF = col.Find(Function(c) c.Id = "FIBER_POST_T")
                        'Dim postM = col.Find(Function(c) c.Id = "METAL_POST_T")
                        'Dim pulp = col.Find(Function(c) c.Id = "PULPECTOMY")
                        Dim rc = FindSvgItemById(col, "RCT")
                        Dim postF = FindSvgItemById(col, "FIBER_POST_T")
                        Dim postM = FindSvgItemById(col, "METAL_POST_T")
                        Dim pulp = FindSvgItemById(col, "PULPECTOMY")
                        If rc IsNot Nothing Then rc.Visible = False
                        If postF IsNot Nothing Then postF.Visible = False
                        If postM IsNot Nothing Then postM.Visible = False
                        If pulp IsNot Nothing Then pulp.Visible = False
                    End If
                Next
            End If
            ' Hide RCT if both RCT/Class or POST/Class coexist
            If Not hasBuildUp AndAlso Not hasCrownT Then
                If (rct AndAlso clas) OrElse (post AndAlso clas) Then
                    'Dim rc = col.Find(Function(c) c.Id = "RCT")
                    'Dim postF = col.Find(Function(c) c.Id = "FIBER_POST_T")
                    'Dim postM = col.Find(Function(c) c.Id = "METAL_POST_T")
                    'Dim pulp = col.Find(Function(c) c.Id = "PULPECTOMY")
                    Dim rc = FindSvgItemById(col, "RCT")
                    Dim postF = FindSvgItemById(col, "FIBER_POST_T")
                    Dim postM = FindSvgItemById(col, "METAL_POST_T")
                    Dim pulp = FindSvgItemById(col, "PULPECTOMY")
                    If rc IsNot Nothing Then rc.Visible = False
                    If postF IsNot Nothing Then postF.Visible = False
                    If postM IsNot Nothing Then postM.Visible = False
                    If pulp IsNot Nothing Then pulp.Visible = False
                End If
            End If
        End If
    End Sub

    Public Sub ProcessIndividualTreatment1(svg As SvgImageBox, col As SvgImageItemCollection,
                                     treatment As Patient_Diagnosis, allTreatments As List(Of Patient_Diagnosis))
        If svg.Name.Contains("Top") OrElse svg.Name.Contains("TOP") Then
            If treatment.PropertyName = "PRR" Then
                'Dim itemPrr = col.Find(Function(c) c.Id = "FISSURE_SEALENT" OrElse c.Id = "PRR")
                Dim itemPrr = FindSvgItemById(col, "FISSURE_SEALENT")
                If itemPrr Is Nothing Then itemPrr = FindSvgItemById(col, "PRR")
                If itemPrr IsNot Nothing AndAlso treatment.PropertyName = "PRR" Then
                    If treatment.LVL = 0 AndAlso treatment.Treat = "PRR" Then
                        'Dim crownTooth = col.Find(Function(c) c.Id = "FISSURE_SEALENT")
                        Dim crownTooth = FindSvgItemById(col, "FISSURE_SEALENT")
                        If crownTooth IsNot Nothing Then
                            crownTooth.Visible = True
                            crownTooth.Appearance.Normal.BorderThickness = 1
                            crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                        End If
                    End If
                End If
                If treatment.LVL = 3 AndAlso treatment.Treat.EndsWith("T") Then
                    'Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL" OrElse c.Id = "CROWN")
                    Dim crownTooth = FindSvgItemById(col, "CROWN_FILL")
                    If crownTooth Is Nothing Then crownTooth = FindSvgItemById(col, "CROWN")
                    If crownTooth IsNot Nothing Then
                        crownTooth.Visible = True
                        crownTooth.Appearance.Normal.BorderThickness = 1
                        crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                    End If
                End If
            End If
        End If
        If svg.Name.Contains("IN") OrElse svg.Name.Contains("OUT") Then
            If treatment.PropertyName = "CROWN_IMG" Then
                If treatment.LVL = 3 AndAlso treatment.Treat.EndsWith("T") Then
                    'Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL" OrElse c.Id = "CROWN")
                    Dim crownTooth = FindSvgItemById(col, "CROWN_FILL")
                    If crownTooth Is Nothing Then crownTooth = FindSvgItemById(col, "CROWN")
                    If crownTooth IsNot Nothing Then
                        crownTooth.Visible = True
                        crownTooth.Appearance.Normal.BorderThickness = 1
                        crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                    End If
                End If
            End If
        End If
        'Dim item = col.Find(Function(c) c.Id = treatment.PropertyName)
        Dim item = FindSvgItemById(col, treatment.PropertyName)
        If item Is Nothing Then Return
        If Not item.Id = "EXTRACTION" Then
            ' Default visibility rules
            If item.Id = "APICECTOMY" OrElse item.Id = "HEMISECTION" Then
                item.Appearance.Normal.FillColor = BackClr
                item.Appearance.Normal.BorderColor = BackClr ' ColorTranslator.FromHtml(treatment.BorderColor)
                item.Appearance.Normal.BorderThickness = 0
            Else
                item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
                item.Appearance.Normal.BorderThickness = treatment.BorderThickness
            End If
            item.Visible = True
        End If
        If treatment.LVL = 3 AndAlso treatment.Treat.EndsWith("T") Then
            'Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL")
            Dim crownTooth = FindSvgItemById(col, "CROWN_FILL")
            If crownTooth IsNot Nothing Then
                crownTooth.Visible = True
                crownTooth.Appearance.Normal.BorderThickness = 1
                crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
            End If
        End If
        If treatment.LVL = 0 AndAlso treatment.Treat.Contains("VENEERS") Then
            'Dim crownItem = col.Find(Function(c) c.Id = "CROWN")
            Dim crownItem = FindSvgItemById(col, "CROWN")
            If crownItem IsNot Nothing Then
                crownItem.Visible = True
                crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
            End If
        ElseIf treatment.LVL = 0 AndAlso treatment.Treat = "DIRECT PULP CAPPING" Then
            If item IsNot Nothing Then
                'Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                Dim indir = FindSvgItemById(col, "INDIRECT_PULP_CAPPING")
                If indir IsNot Nothing Then
                    indir.Visible = True
                    indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                    indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
                    indir.Appearance.Normal.BorderThickness = treatment.BorderThickness
                End If
            End If
        ElseIf treatment.LVL = 0 AndAlso treatment.Treat = "PERIAPICAL LESION" Then
            If item IsNot Nothing Then
                'Dim peri = col.Find(Function(c) c.Id = "PERIAPICAL_LESION")
                Dim peri = FindSvgItemById(col, "PERIAPICAL_LESION")
                If peri IsNot Nothing Then
                    peri.Visible = True
                    peri.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                    peri.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
                    peri.Appearance.Normal.BorderThickness = 0
                End If
            End If
        End If
        If svg.Name.Contains("Top") Then
            If treatment.LVL = 3 AndAlso treatment.Treat.EndsWith("T") Then
                'Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL")
                Dim crownTooth = FindSvgItemById(col, "CROWN_FILL")
                If crownTooth IsNot Nothing Then
                    crownTooth.Visible = True
                    crownTooth.Appearance.Normal.BorderThickness = 1
                    crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                End If
            End If
            If Not item.Id = "EXTRACTION" Then
                ' Default visibility rules
                If item.Id = "APICECTOMY" OrElse item.Id = "HEMISECTION" Then
                    item.Appearance.Normal.FillColor = BackClr
                    item.Appearance.Normal.BorderColor = BackClr ' ColorTranslator.FromHtml(treatment.BorderColor)
                    item.Appearance.Normal.BorderThickness = 0
                Else
                    item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(treatment.FillColor)
                    item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(treatment.BorderColor)
                    item.Appearance.Normal.BorderThickness = treatment.BorderThickness
                End If
                item.Visible = True
            End If
            Dim rct, clas, post As Boolean
            ' Then, inside your loop, the check is simple and fast:
            For Each t In allTreatments
                If t.Treat.StartsWith("RC") OrElse t.Treat.StartsWith("RE") OrElse t.Treat = ("PULPECTOMY") OrElse t.Treat = ("PULPOTOMY") Then
                    rct = True
                End If
                If _clasesSet.Contains(t.Treat) Then
                    clas = True
                End If
                If t.Treat = "FIBER POST" Or t.Treat = "METAL POST" Then
                    post = True
                End If
            Next
            If rct AndAlso clas OrElse post AndAlso clas Then
                'Dim rc = col.Find(Function(c) c.Id = "RCT")
                'Dim postF = col.Find(Function(c) c.Id = "FIBER POST")
                'Dim postM = col.Find(Function(c) c.Id = "METAL POST")
                'Dim pulpe = col.Find(Function(c) c.Id = "PULPECTOMY")
                'Dim pulpo = col.Find(Function(c) c.Id = "PULPOTOMY")
                Dim rc = FindSvgItemById(col, "RCT")
                Dim postF = FindSvgItemById(col, "FIBER POST")
                Dim postM = FindSvgItemById(col, "METAL POST")
                Dim pulpe = FindSvgItemById(col, "PULPECTOMY")
                Dim pulpo = FindSvgItemById(col, "PULPOTOMY")
                If rc IsNot Nothing Then
                    rc.Visible = False
                End If
                If postF IsNot Nothing Then
                    postF.Visible = False
                End If
                If postM IsNot Nothing Then
                    postM.Visible = False
                End If
                If pulpe IsNot Nothing Then
                    pulpe.Visible = False
                End If
                If pulpo IsNot Nothing Then
                    pulpo.Visible = False
                End If
            End If
        End If
    End Sub

    Private Function IsDentureDiagTreat(treatText As String) As Boolean
        If String.IsNullOrWhiteSpace(treatText) Then Return False
        Select Case Helpers.GetFirstTreatmentPart(treatText).ToUpperInvariant()
            Case "CD", "COMPLETE DENTURE", "RPD", "REMOVABLE PARTIAL DENTURE"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function HasDentureDiagnosis(trtsList As List(Of Patient_Diagnosis)) As Boolean
        Return trtsList IsNot Nothing AndAlso trtsList.Any(Function(t) IsDentureDiagTreat(t.Treat))
    End Function

    Public Sub HandleHighLevelTreatments(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox), col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        If HasDentureDiagnosis(trtsList) Then
            HandleDENTURE(svg, col, trtsList)
            Return
        End If
        Dim maxLevel = trtsList.Max(Function(t) t.LVL)
        Select Case maxLevel
            Case 4 ' EXTRACTED
                HandleExtractionTreatment(svg, svgExternalList, col, trtsList)
            Case 5, 6, 7 ' IMPLANT and stages
                HandleImplantTreatment(svg, col, trtsList, maxLevel)
            Case 8 ' BRIDGE
                HandleBridgeTreatment(svg, col, trtsList)
            Case 9
                ' Match TreatHelper: LVL=9 diagnosis rows (dentures) render whenever present, not only when mobile UI mode is on.
                HandleDENTURE(svg, col, trtsList)
        End Select
    End Sub

    Public Sub HandleCrownTreatment(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        If svg.Name.Contains("Top") Then
            ShowBaseTooth(col)
        End If
        'Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
        Dim crownFill = FindSvgItemById(col, "CROWN_FILL")
        If crownFill IsNot Nothing Then
            crownFill.Visible = True
            crownFill.Appearance.Normal.BorderThickness = 1
            Dim crownTrt = trtsList.FirstOrDefault(Function(t) t.LVL = 3)
            If crownTrt IsNot Nothing Then
                crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor)
            End If
        End If
    End Sub

    Public Sub HandleExtractionTreatment(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox), col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        Dim extractedId = If(svgExternalList.Contains(svg), "EXTRACTION", "EXTRACTED")
        'Dim extracted = col.Find(Function(c) c.Id = extractedId)
        Dim extracted = FindSvgItemById(col, extractedId)
        If extracted IsNot Nothing Then
            ' Hide all other items
            ResetSvgItemsVisibility(col)
            extracted.Appearance.Normal.FillColor = Color.Transparent
            extracted.Visible = True
            'For Each item As SvgImageItem In col
            '    item.Visible = (item.Id = extractedId)
            'Next
        End If
    End Sub
    Public Sub HandleImplantTreatment(svg As SvgImageBox, col As SvgImageItemCollection,
                                   trtsList As List(Of Patient_Diagnosis), maxLevel As Integer)

#Region "ToDel"

        '' ----------------------------
        '' Determine latest critical event (Extraction/Implant + components)
        '' ----------------------------
        'Dim criticalTreatments = trtsList.Where(Function(t) t.LVL = 4 Or (t.LVL >= 5 AndAlso t.LVL <= 7)).ToList()

        'Dim displayTreatment As Patient_Diagnosis = Nothing
        'Dim latestImplant As Patient_Diagnosis = Nothing
        'If criticalTreatments.Any() Then
        '    ' Special rule: Extraction after Implant
        '    Dim latestExtraction = criticalTreatments.
        '    Where(Function(t) t.LVL = 4).
        '    OrderByDescending(Function(t) t.TreatDate).
        '    FirstOrDefault()

        '    latestImplant = criticalTreatments.Where(Function(t) t.LVL >= 5 AndAlso t.LVL <= 7).OrderByDescending(Function(t) t.TreatDate).FirstOrDefault()

        '    If latestExtraction IsNot Nothing AndAlso latestImplant IsNot Nothing AndAlso latestExtraction.TreatDate > latestImplant.TreatDate Then
        '        ' Extraction after implant → Extraction is the dominant treatment
        '        displayTreatment = latestExtraction
        '    Else
        '        ' Otherwise, just show latest implant-related event
        '        displayTreatment = criticalTreatments.OrderByDescending(Function(t) t.TreatDate).First()
        '    End If
        'Else
        '    ' No extraction/implant events → fallback to latest overall treatment
        '    displayTreatment = trtsList.OrderByDescending(Function(t) t.TreatDate).FirstOrDefault()
        'End If

        '' ----------------------------
        '' Apply visibility rules based on displayTreatment
        '' ----------------------------
        'If displayTreatment IsNot Nothing Then
        '    Select Case displayTreatment.LVL
        '        Case 4 ' Extraction
        '            ' Hide implant layers
        '            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
        '            If implant IsNot Nothing Then implant.Visible = False
        '            If extractedLayer IsNot Nothing Then extractedLayer.Visible = True
        '            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
        '            If heal IsNot Nothing Then heal.Visible = False
        '            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
        '            If abut IsNot Nothing Then abut.Visible = False
        '            Dim crown = col.Find(Function(c) c.Id = "CROWN_IMG")
        '            If crown IsNot Nothing Then crown.Visible = False
        '        Case 5 ' Implant base
        '            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
        '            If implant IsNot Nothing Then implant.Visible = True
        '            If extractedLayer IsNot Nothing Then extractedLayer.Visible = False

        '        Case 6 ' Healing cap
        '            If displayTreatment.TreatDate <> latestImplant.TreatDate Then
        '                If displayTreatment.PropertyName = "HEALING_CAP" Then
        '                    Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
        '                    If heal IsNot Nothing Then heal.Visible = True
        '                    If extractedLayer IsNot Nothing Then extractedLayer.Visible = False
        '                End If
        '                ' Abutment
        '                If displayTreatment.PropertyName = "ABUTMENT" Then
        '                    Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
        '                    If abut IsNot Nothing Then abut.Visible = True
        '                    If extractedLayer IsNot Nothing Then extractedLayer.Visible = False
        '                End If
        '            End If
        '        Case 7 '  crown
        '            If displayTreatment.PropertyName = "CROWN_IMG" Then
        '                If svg.Name.Contains("Top") AndAlso maxLevel = 7 Then
        '                    If col.Find(Function(c) c.Id = "IMPLANT") IsNot Nothing Then col.Find(Function(c) c.Id = "IMPLANT").Visible = False
        '                    If col.Find(Function(c) c.Id = "ABUTMENT") IsNot Nothing Then col.Find(Function(c) c.Id = "ABUTMENT").Visible = False
        '                    If col.Find(Function(c) c.Id = "HEALING_CAP") IsNot Nothing Then col.Find(Function(c) c.Id = "HEALING_CAP").Visible = False
        '                    If extractedLayer IsNot Nothing Then extractedLayer.Visible = False
        '                    ShowBaseTooth(col)

        '                ElseIf (svg.Name.Contains("Out") OrElse svg.Name.Contains("IN")) AndAlso maxLevel = 7 Then
        '                    If col.Find(Function(c) c.Id = "IMPLANT") IsNot Nothing Then col.Find(Function(c) c.Id = "IMPLANT").Visible = True
        '                    If col.Find(Function(c) c.Id = "ABUTMENT") IsNot Nothing Then col.Find(Function(c) c.Id = "ABUTMENT").Visible = False
        '                    If col.Find(Function(c) c.Id = "HEALING_CAP") IsNot Nothing Then col.Find(Function(c) c.Id = "HEALING_CAP").Visible = False
        '                    If extractedLayer IsNot Nothing Then extractedLayer.Visible = False
        '                End If

        '                Dim crown = col.Find(Function(c) c.Id = "CROWN_IMG")
        '                If crown IsNot Nothing Then crown.Visible = True
        '                If extractedLayer IsNot Nothing Then extractedLayer.Visible = False
        '                If displayTreatment.LVL = 7 AndAlso displayTreatment.Treat.EndsWith("I") Then
        '                    Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL")
        '                    If crownTooth IsNot Nothing Then
        '                        crownTooth.Visible = True
        '                        crownTooth.Appearance.Normal.BorderThickness = 1
        '                        For Each tc In trtsList
        '                            Select Case tc.Treat
        '                                Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I",
        '                                 "TEMP CROWN I", "STAINLESS STEEL CROWN I"
        '                                    crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
        '                            End Select
        '                        Next
        '                    End If
        '                End If
        '            End If
        '    End Select

        'End If


#End Region
        ' Always hide ALL layer initially
        For Each item As SvgImageItem In col
            If item.Id <> "IMPLANT" Then item.Visible = False
        Next
        ' ----------------------------
        ' Always show implant if present in treatments (unless overridden by extraction-after-implant)
        ' ----------------------------
        If trtsList.Any(Function(t) t.PropertyName.Contains("IMPLANT")) Then 'AndAlso Not (displayTreatment IsNot Nothing AndAlso displayTreatment.LVL = 4) Then
            'Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
            Dim implant = FindSvgItemById(col, "IMPLANT")
            If implant IsNot Nothing Then implant.Visible = True
            ' Show healing cap if present
            If trtsList.Any(Function(t) t.PropertyName = "HEALING_CAP") Then
                'Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                Dim heal = FindSvgItemById(col, "HEALING_CAP")
                If heal IsNot Nothing Then heal.Visible = True
            End If
            ' Show abutment if present
            If trtsList.Any(Function(t) t.PropertyName = "ABUTMENT") Then
                'Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                Dim abut = FindSvgItemById(col, "ABUTMENT")
                If abut IsNot Nothing Then abut.Visible = True
            End If
            ' ----------------------------
            ' Special handling for top view
            ' ----------------------------
            If svg.Name.Contains("Top") AndAlso maxLevel = 7 Then
                Dim impl = FindSvgItemById(col, "IMPLANT")
                If impl IsNot Nothing Then impl.Visible = False
                Dim ab = FindSvgItemById(col, "ABUTMENT")
                If ab IsNot Nothing Then ab.Visible = False
                Dim hc = FindSvgItemById(col, "HEALING_CAP")
                If hc IsNot Nothing Then hc.Visible = False
                ShowBaseTooth(col)

            ElseIf (svg.Name.Contains("Out") OrElse svg.Name.Contains("IN")) AndAlso maxLevel = 7 Then
                Dim impl = FindSvgItemById(col, "IMPLANT")
                If impl IsNot Nothing Then impl.Visible = True
                Dim ab = FindSvgItemById(col, "ABUTMENT")
                If ab IsNot Nothing Then ab.Visible = False
                Dim hc = FindSvgItemById(col, "HEALING_CAP")
                If hc IsNot Nothing Then hc.Visible = False
            End If
            ' ----------------------------
            ' Implant crowns with "I" ending
            ' ----------------------------
            For Each t In trtsList
                If t.LVL = 7 AndAlso t.Treat.EndsWith("I") Then
                    'Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL")
                    Dim crownTooth = FindSvgItemById(col, "CROWN_FILL")
                    If crownTooth IsNot Nothing Then
                        crownTooth.Visible = True
                        crownTooth.Appearance.Normal.BorderThickness = 1
                        For Each tc In trtsList
                            Select Case tc.Treat
                                Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I",
                                 "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                    crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                            End Select
                        Next
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub HandleBridgeTreatment(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        ''Original Code
        'If svg.Name.Contains("Top") Then
        '    ShowBaseTooth(col)

        '    ' Show bridge mark
        '    Dim bridgMark = col.Find(Function(c) c.Id = "BR")
        '    If bridgMark IsNot Nothing Then
        '        bridgMark.Visible = True
        '        bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
        '    End If
        'End If

        '' Show crown fill with bridge style
        'Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
        'If crownFill IsNot Nothing Then
        '    crownFill.Visible = True
        '    Dim bridgeTrt = trtsList.FirstOrDefault(Function(t) t.LVL = 8)
        '    If bridgeTrt IsNot Nothing Then
        '        crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
        '    End If
        'End If
        ''End Original Code
        '================
        ' BRIDGE  
        ' 1. Handle bridge differently per view
        If svg.Name.Contains("Top") Then
            For Each item As SvgImageItem In col
                item.Visible = False
            Next
            ' Top View - style the base tooth as crown
            ShowBaseTooth(col)
            'Show BRIDGE Mark
            'Dim bridgMark = col.Find(Function(c) c.Id = "BR")
            Dim bridgMark = FindSvgItemById(col, "BR")
            If bridgMark IsNot Nothing Then
                bridgMark.Visible = True
                bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
            End If

        ElseIf svg.Name.Contains("Out") OrElse svg.Name.Contains("IN") Then
            ' 2. Always show CON if exists
            If trtsList.Any(Function(t) t.PropertyName = "IMPLANT") Then
                'Dim IMPLANT = col.Find(Function(c) c.Id = "IMPLANT")
                Dim IMPLANT = FindSvgItemById(col, "IMPLANT")
                If IMPLANT IsNot Nothing Then IMPLANT.Visible = True
                ' Out View - show dedicated crown layer
                'Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                Dim crownImg = FindSvgItemById(col, "CROWN_IMG")
                If crownImg IsNot Nothing Then
                    crownImg.Visible = True
                End If
            ElseIf trtsList.Any(Function(t) t.PropertyName = "EXTRACTION") Then
                'Dim EXTRACTED = col.Find(Function(c) c.Id = "EXTRACTION")
                Dim EXTRACTED = FindSvgItemById(col, "EXTRACTION")
                If EXTRACTED IsNot Nothing Then EXTRACTED.Visible = True
                ' Out View - show dedicated crown layer
                'Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                Dim crownImg = FindSvgItemById(col, "CROWN_IMG")
                If crownImg IsNot Nothing Then
                    crownImg.Visible = True
                End If
            Else
                ShowBaseTooth(col)
            End If
            'Show BRIDGE Mark
            'Dim bridgMark = col.Find(Function(c) c.Id = "BR")
            Dim bridgMark2 = FindSvgItemById(col, "BR")
            If bridgMark2 IsNot Nothing Then
                bridgMark2.Visible = True
                bridgMark2.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
            End If
            ' Out View - show dedicated crown layer
            'Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG")
            Dim crownItem = FindSvgItemById(col, "CROWN_IMG")
            If crownItem IsNot Nothing Then
                crownItem.Visible = True
            End If
        End If
        'Show Crown Fill
        'Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
        Dim crownFill = FindSvgItemById(col, "CROWN_FILL")
        If crownFill IsNot Nothing Then
            crownFill.Visible = True
            For Each t In trtsList
                Select Case t.Treat
                    Case "METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE",
                        "TEMP BRIDGE", "STAINLESS STEEL BRIDGE"
                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                End Select
            Next
        End If
    End Sub

    Public Sub HandleDENTURE(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_Diagnosis))
        ResetSvgItemsVisibility(col)
        Dim dentRow = trtsList.FirstOrDefault(Function(t) IsDentureDiagTreat(t.Treat))
        If svg.Name.Contains("Top") Then
            ShowBaseTooth(col)
            Dim dentMark = FindSvgItemById(col, "CH")
            If dentMark IsNot Nothing Then
                dentMark.Visible = True
                dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
            End If
        ElseIf svg.Name.Contains("Out") OrElse svg.Name.Contains("IN") Then
            Dim extracted = FindSvgItemById(col, "EXTRACTED")
            If extracted IsNot Nothing Then extracted.Visible = False
            Dim dentMark2 = FindSvgItemById(col, "CH")
            If dentMark2 IsNot Nothing Then
                dentMark2.Visible = True
                dentMark2.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
            End If
            Dim crownItem = FindSvgItemById(col, "CROWN_IMG")
            If crownItem IsNot Nothing Then
                crownItem.Visible = True
                If dentRow IsNot Nothing Then
                    crownItem.Appearance.Normal.FillColor = GetCustomTrtColor(dentRow.Treat)
                    crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(dentRow.BorderColor)
                End If
            End If
        End If
    End Sub
#End Region

    Private Function IsOutView(svg As SvgImageBox) As Boolean
        Return svg.Name.Contains("Out") OrElse svg.Name.Contains("OUT")
    End Function
    Private Function IsInView(svg As SvgImageBox) As Boolean
        Return svg.Name.Contains("In") OrElse svg.Name.Contains("IN")
    End Function
    Private Function IsTopView(svg As SvgImageBox) As Boolean
        Return svg.Name.Contains("Top") OrElse svg.Name.Contains("TOP")
    End Function

    ' Identical to TreatHelper: find SVG item by Id, including inside groups.
    Private Function FindSvgItemById(items As SvgImageItemCollection, targetId As String) As SvgImageItem
        For Each item As SvgImageItem In items
            ' Direct hit
            If String.Equals(item.Id, targetId, StringComparison.OrdinalIgnoreCase) Then
                Return item
            End If
            ' Dive into groups
            If item.Items IsNot Nothing AndAlso item.Items.Count > 0 Then
                Dim found = FindSvgItemById(item.Items, targetId)
                If found IsNot Nothing Then
                    Return found
                End If
            End If
        Next
        Return Nothing
    End Function

#Region "DiagJawFlyoutCorners"
    ''' <summary>Anchor corner for a manual flyout in JawPanel client space (combine with JawPanel.Left/Top for host-UC client coords).</summary>
    Public Function DiagToothFlyoutCornerInJawClient(svg As SvgImageBox, flyoutWidth As Integer, flyoutHeight As Integer,
                                                       namePrefixComparison As StringComparison) As Point
        Dim loc As Point = svg.Location
        Dim n As String = svg.Name
        If n.StartsWith("Ld", namePrefixComparison) Then
            Return New Point(loc.X - flyoutWidth, loc.Y + svg.Height - flyoutHeight)
        End If
        If n.StartsWith("Rd", namePrefixComparison) Then
            Return New Point(loc.X + svg.Width, loc.Y + svg.Height - flyoutHeight)
        End If
        If n.StartsWith("Lu", namePrefixComparison) Then
            Return New Point(loc.X - flyoutWidth, loc.Y)
        End If
        If n.StartsWith("Ru", namePrefixComparison) Then
            Return New Point(loc.X + svg.Width, loc.Y)
        End If
        Return New Point(loc.X + svg.Width, loc.Y)
    End Function
#End Region

End Module

