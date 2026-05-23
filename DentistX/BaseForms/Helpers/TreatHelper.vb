

Imports DevExpress.XtraEditors

Module TreatHelper
    Public isItMobTreat As Boolean = False

    '   ProcessTooth
    '├─ EvaluateVisibilityPolicy   ← ONE place, global, authoritative
    '├─ ApplyBaseVisibility        ← blanket hide/show
    '├─ ApplyViewSpecificRules     ← Top / In / Out
    '├─ ApplySpecialCases          ← veneers, apex, etc.


    'New Helper

    Private Function ShouldSuppressLowLevels(allTreatments As List(Of Patient_ToothTrt)) As Boolean
        Return allTreatments.Any(Function(t) t.LVL = 3)
    End Function


    Public capFillClr As Color
    Public rootFillClr As Color
#Region "TreatCode"
    Public Sub LoadTeethTreatsUsingTreatCode(cntrl As Control, svgExternalList As List(Of SvgImageBox),
                                            svgDiagList As List(Of SvgImageBox), patientTreats As IEnumerable(Of Patient_ToothTrt))
        cntrl.SuspendLayout()
        For Each ct As Control In cntrl.Controls.OfType(Of SvgImageBox)()
            Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
            svg.BeginUpdate()  ' <== if SvgImageBox supports it
            'ProcessToothTreatments(svg, svgExternalList, svgDiagList, patientTreats)
            svg.EndUpdate()
        Next
        cntrl.ResumeLayout()
    End Sub
    ''' <param name="isMobileChart">When True, enables mobile-chart rules (HandleDENTURE for LVL 9, base-tooth rules in ProcessTreatmentLayers).</param>
    Public Sub ProcessToothTreatments(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox),
                                        patientTreats As IEnumerable(Of Patient_ToothTrt),
                                        Optional isMobileChart As Boolean = False)
        Dim previousMobile As Boolean = isItMobTreat
        isItMobTreat = isMobileChart
        Try
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
            HandleExternalTreatments(svg, svgExternalList, col, trtsList)
            ' If no treatments, just show base tooth and exit
            If trtsList.Count = 0 Then
                ShowBaseTooth(col)
                Return
            End If
            ' Process each treatment layer
            ProcessTreatmentLayers(svg, svgExternalList, col, trtsList)
        Finally
            isItMobTreat = previousMobile
        End Try
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
                                         trtsList As List(Of Patient_ToothTrt))
        Dim externalTrts = trtsList.Where(Function(t) t.IsExternal.HasValue AndAlso t.IsExternal.Value = True).ToList()
        If externalTrts.Any() Then
            ApplyGradientBackground(svg,
                             Color.AntiqueWhite,
                              Color.White,
                              Drawing2D.LinearGradientMode.Horizontal,
                              128)
            svgExternalList.Add(svg)
        End If
    End Sub
    Public Sub ProcessTreatmentLayers(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox),
                                       col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
        ' Show base tooth for levels 0-2
        If trtsList.Max(Function(t) t.LVL) < 4 OrElse isItMobTreat = False Then
            ShowBaseTooth(col)
        End If
        ' Process each treatment
        For Each t In trtsList
            'ProcessIndividualTreatment(svg, col, t, trtsList)
            ProcessTreatment(svg, col, t, trtsList)

        Next
        ' Handle special high-level cases
        HandleHighLevelTreatments(svg, svgExternalList, col, trtsList)
        Dim hasAnyNotes = trtsList.Any(Function(tr) tr.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrEmpty(tr.TreatNotes))
        Dim notesMark = FindSvgItemById(col, "EXCLAMATION_MARK")
        If notesMark IsNot Nothing Then
            notesMark.Visible = hasAnyNotes
            notesMark.Appearance.Normal.FillColor = BackClr ' ColorTranslator.FromHtml("#55FFC719")  '("#D81A1A")'Color.Transparent
            notesMark.Appearance.Normal.BorderColor = ColorTranslator.FromHtml("#D81A1A") '("#55FFC719") 'Color.Transparent

        End If
    End Sub

    Private Sub ProcessTreatment(svg As SvgImageBox, col As SvgImageItemCollection,
                                  treatment As Patient_ToothTrt, allTreatments As List(Of Patient_ToothTrt))


        If svg Is Nothing OrElse col Is Nothing OrElse allTreatments Is Nothing Then Return
        Dim col2 As SvgImageItemCollection = svg.RootItems
        ' ==========================================================
        ' VIEW DETECTION
        ' ==========================================================
        Dim isTopView1 As Boolean = IsTopView(svg)
        Dim isOutView1 As Boolean = IsOutView(svg)
        Dim isInView1 As Boolean = IsInView(svg)
        ' ==========================================================
        ' GROUP TREATMENTS BY SHAPE (PropertyName)
        ' ==========================================================
        Dim shapeMap =
        allTreatments.
        Where(Function(t) Not String.IsNullOrWhiteSpace(t.PropertyName)).
        GroupBy(Function(t) t.PropertyName).
        ToDictionary(Function(g) g.Key,
                     Function(g) g.ToList(),
                     StringComparer.OrdinalIgnoreCase)

        ' ==========================================================
        ' GLOBAL LVL FACTS
        ' ==========================================================
        Dim hasLvl3 As Boolean = allTreatments.Any(Function(t) t.LVL = 3)
        Dim hasLvlLessThan4 As Boolean = allTreatments.Any(Function(t) t.LVL < 4)

        ' ==========================================================
        ' HARD RESET
        ' ==========================================================
        For Each el In col
            el.Visible = False
        Next

        ' ==========================================================
        ' BASE TOOTH VISIBILITY
        ' ==========================================================
        If hasLvlLessThan4 Then
            For Each el In col
                If el.Id IsNot Nothing AndAlso
               el.Id.Contains("IMG") AndAlso
               el.Id <> "CROWN_IMG" Then
                    el.Visible = True
                End If
            Next
        End If

        ' ==========================================================
        ' APPLY TREATMENT SHAPES
        ' ==========================================================
        For Each el In col

            If el.Id Is Nothing Then Continue For

            Dim propertyName As String = el.Id

            If Not shapeMap.ContainsKey(propertyName) Then Continue For

            Dim treatments = shapeMap(propertyName)

            ' ------------------------------------------------------
            ' OUT VIEW LOGIC
            ' ------------------------------------------------------
            If isOutView1 OrElse isInView1 Then
                'For Each t In treatments
                '    If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrEmpty(t.TreatNotes) Then
                '        Dim notes = FindSvgItemById(col2, "EXCLAMATION_MARK")

                '        If notes IsNot Nothing Then
                '            notes.Visible = True
                '            'notes.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.CapFill)
                '        Else
                '            notes.Visible = False
                '        End If
                '    End If
                'Next

                ' CLASS treatments live on crown in OUT view
                ' If LVL 3 exists → hide all CLASS shapes
                If hasLvl3 AndAlso propertyName.StartsWith("CLASS_") Then
                    Continue For
                End If

                ' Otherwise show all LVL ≤ 3
                If treatments.Any(Function(t) t.LVL <= 3) Then
                    'el.Visible = True
                    For Each t In treatments

                        Select Case t.Treat
                            Case "INDIRECT PULP CAPPING"
                                Dim capFill = FindSvgItemById(col2, "INDIRECTCAP")
                                Dim rootFill = FindSvgItemById(col2, "INDIRECTROOT")
                                If capFill IsNot Nothing Then
                                    capFill.Appearance.Normal.FillColor = ResolveCompoundLayerColor(t.CapFill, t.Treat, True)
                                End If
                                If rootFill IsNot Nothing Then
                                    rootFill.Appearance.Normal.FillColor = ResolveCompoundLayerColor(t.RootFill, t.Treat, False)
                                End If
                                el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                el.Appearance.Normal.BorderThickness = t.BorderThickness
                                el.Visible = True
                            Case "DIRECT PULP CAPPING"
                                Dim capFill = FindSvgItemById(col2, "DIRECTCAP")
                                Dim rootFill = FindSvgItemById(col2, "DIRECTROOT")
                                If capFill IsNot Nothing Then
                                    capFill.Appearance.Normal.FillColor = ResolveCompoundLayerColor(t.CapFill, t.Treat, True)
                                End If
                                If rootFill IsNot Nothing Then
                                    rootFill.Appearance.Normal.FillColor = ResolveCompoundLayerColor(t.RootFill, t.Treat, False)
                                End If
                                el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                el.Appearance.Normal.BorderThickness = t.BorderThickness
                                el.Visible = True
                            Case "PULPOTOMY"
                                Dim capFill = FindSvgItemById(col2, "PULPCAP")
                                Dim rootFill = FindSvgItemById(col2, "PULPROOT")
                                If capFill IsNot Nothing Then
                                    capFill.Appearance.Normal.FillColor = ResolveCompoundLayerColor(t.CapFill, t.Treat, True)
                                End If
                                If rootFill IsNot Nothing Then
                                    rootFill.Appearance.Normal.FillColor = ResolveCompoundLayerColor(t.RootFill, t.Treat, False)
                                End If
                                el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                el.Appearance.Normal.BorderThickness = t.BorderThickness
                                el.Visible = True
                            Case Else
                                el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                el.Appearance.Normal.BorderThickness = t.BorderThickness
                                el.Visible = True
                        End Select
                    Next
                End If

                ' ------------------------------------------------------
                ' TOP VIEW LOGIC
                ' ------------------------------------------------------
            ElseIf isTopView1 Then

                If hasLvl3 Then
                    ' Only LVL 3
                    If treatments.Any(Function(t) t.LVL = 3) Then
                        For Each t In treatments
                            el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                            el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                            el.Appearance.Normal.BorderThickness = t.BorderThickness
                            el.Visible = True
                        Next
                    End If
                Else
                    ' No LVL 3 → show lower levels
                    If treatments.Any(Function(t) t.LVL < 3) Then
                        For Each t In treatments
                            el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                            el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                            el.Appearance.Normal.BorderThickness = t.BorderThickness
                            el.Visible = True
                        Next
                    End If
                End If
            ElseIf isInView1 Then

                ' IN VIEW LOGIC
                ' Similar to OUT view but may have different rules in future
                ' For now, treat it the same as OUT view
                ' CLASS treatments live on crown in IN view
                ' If LVL 3 exists → hide all CLASS shapes
                If hasLvl3 AndAlso propertyName.StartsWith("CLASS_") Then
                    Continue For
                End If
                ' Otherwise show all LVL ≤ 3
                If treatments.Any(Function(t) t.LVL <= 3) Then

                    For Each t In treatments
                        el.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                        el.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                        el.Appearance.Normal.BorderThickness = t.BorderThickness
                        el.Visible = True
                        If t.Treat.Contains("VENEERS") Then
                            ' Special handling for VENEER in IN view
                            ' e.g., adjust opacity or color
                            el.Visible = False
                            Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                        End If

                    Next
                End If
            End If

        Next

        '' EXCLAMATION_MARK: set once at end so it is not cleared by a later ProcessTreatment call.
        '' (ProcessTreatment is invoked once per treatment; the last run's visibility wins.)
        'If isOutView1 Then
        '    Dim hasAnyNotes = allTreatments.Any(Function(tr) tr.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrEmpty(tr.TreatNotes))
        '    Dim notesMark = FindSvgItemById(col2, "EXCLAMATION_MARK")
        '    If notesMark IsNot Nothing Then
        '        notesMark.Visible = hasAnyNotes
        '        notesMark.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#D81A1A") '("#55FFC719") 'Color.Transparent
        '    End If
        'End If

    End Sub

    Private Sub ProcessTreatmentWithClass(svg As SvgImageBox, col As SvgImageItemCollection,
                                  treatment As Patient_ToothTrt, allTreatments As List(Of Patient_ToothTrt))

        If svg Is Nothing OrElse col Is Nothing OrElse allTreatments Is Nothing Then Return

        ' ==========================================================
        ' VIEW DETECTION
        ' ==========================================================
        Dim isTopView1 As Boolean = IsTopView(svg)
        Dim isOutView1 As Boolean = Not isTopView1

        ' ==========================================================
        ' GROUP TREATMENTS BY SHAPE (PropertyName)
        ' ==========================================================
        Dim shapeMap =
        allTreatments.
        Where(Function(t) Not String.IsNullOrWhiteSpace(t.PropertyName)).
        GroupBy(Function(t) t.PropertyName, StringComparer.OrdinalIgnoreCase).
        ToDictionary(Function(g) g.Key,
                     Function(g) g.ToList(),
                     StringComparer.OrdinalIgnoreCase)

        ' ==========================================================
        ' GLOBAL LVL FACTS
        ' ==========================================================
        Dim hasLvl3 As Boolean = allTreatments.Any(Function(t) t.LVL = 3)
        Dim hasLvlLessThan4 As Boolean = allTreatments.Any(Function(t) t.LVL < 4)

        ' ==========================================================
        ' HARD RESET (EVERYTHING OFF)
        ' ==========================================================
        For Each el In col
            el.Visible = False
        Next

        ' ==========================================================
        ' BASE TOOTH VISIBILITY (AUTHORITATIVE RULE)
        ' ==========================================================
        If hasLvlLessThan4 Then
            For Each el In col
                If el.Id IsNot Nothing AndAlso
               el.Id.Contains("IMG") AndAlso
               el.Id <> "CROWN_IMG" Then
                    el.Visible = True
                End If
            Next
        End If

        ' ==========================================================
        ' APPLY TREATMENT SHAPES
        ' ==========================================================
        For Each el In col

            If el.Id Is Nothing Then Continue For

            Dim propertyName As String = el.Id

            If Not shapeMap.ContainsKey(propertyName) Then Continue For

            Dim treatments = shapeMap(propertyName)

            If isOutView1 Then
                ' ----------------------------------------------
                ' OUT VIEW → inclusive
                ' Show ALL treatments with LVL ≤ 3
                ' ----------------------------------------------
                If treatments.Any(Function(t) t.LVL <= 3) Then
                    el.Visible = True
                End If

            ElseIf isTopView1 Then
                ' ----------------------------------------------
                ' TOP VIEW → selective
                ' ----------------------------------------------
                If hasLvl3 Then
                    ' If any LVL 3 exists → show ONLY LVL 3
                    If treatments.Any(Function(t) t.LVL = 3) Then
                        el.Visible = True
                    ElseIf treatments.Any(Function(t) t.LVL < 3) Then
                        el.Visible = False
                    End If
                Else
                    ' No LVL 3 → show lower levels
                    If treatments.Any(Function(t) t.LVL < 3) Then
                        el.Visible = True
                    End If
                End If
            End If

        Next

    End Sub


    Private Function IsDentureTreatName(treatText As String) As Boolean
        If String.IsNullOrWhiteSpace(treatText) Then Return False
        Select Case Helpers.GetFirstTreatmentPart(treatText).ToUpperInvariant()
            Case "CD", "COMPLETE DENTURE", "RPD", "REMOVABLE PARTIAL DENTURE"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function HasDentureTreatment(trtsList As List(Of Patient_ToothTrt)) As Boolean
        Return trtsList IsNot Nothing AndAlso trtsList.Any(Function(t) IsDentureTreatName(t.Treat))
    End Function

    Public Sub HandleHighLevelTreatments(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox), col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
        ' Denture dominates: show only denture layers on top of base crown graphic, ignore other co-existing treat visuals (data unchanged).
        If HasDentureTreatment(trtsList) Then
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
                ' Denture / mobile-band chart rows use LVL=9 on Patient_ToothTrt. Always draw them when that level wins Max(LVL).
                ' (Previously gated on isItMobTreat / TreatsUserControl "Mobile", so dentures disappeared in normal jaw mode even though data loaded.)
                HandleDENTURE(svg, col, trtsList)
        End Select
    End Sub
    Public Sub HandleCrownTreatment(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
        If svg.Name.Contains("Top") Then
            ShowBaseTooth(col)
        End If
        Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
        If crownFill IsNot Nothing Then
            crownFill.Visible = True
            crownFill.Appearance.Normal.BorderThickness = 1
            Dim crownTrt = trtsList.FirstOrDefault(Function(t) t.LVL = 3)
            If crownTrt IsNot Nothing Then
                crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(crownTrt.FillColor)
            End If
        End If
    End Sub
    Public Sub HandleExtractionTreatment(svg As SvgImageBox, svgExternalList As List(Of SvgImageBox), col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
        Dim extractedId = If(svgExternalList.Contains(svg), "EXTRACTION", "EXTRACTED")
        Dim extracted = col.Find(Function(c) c.Id = extractedId)
        If extracted IsNot Nothing Then
            ' Hide all other items
            ResetSvgItemsVisibility(col)
            extracted.Appearance.Normal.FillColor = Color.Transparent
            extracted.Visible = True
        End If
    End Sub
    Public Sub HandleImplantTreatment(svg As SvgImageBox, col As SvgImageItemCollection,
                                   trtsList As List(Of Patient_ToothTrt), maxLevel As Integer)
        ' Always hide ALL layer initially
        For Each item As SvgImageItem In col
            If item.Id <> "IMPLANT" Then item.Visible = False
        Next
        ' ----------------------------
        ' Always show implant if present in treatments (unless overridden by extraction-after-implant)
        ' ----------------------------
        If trtsList.Any(Function(t) t.PropertyName.Contains("IMPLANT")) Then 'AndAlso Not (displayTreatment IsNot Nothing AndAlso displayTreatment.LVL = 4) Then
            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
            If implant IsNot Nothing Then implant.Visible = True
            ' Show healing cap if present
            If trtsList.Any(Function(t) t.PropertyName = "HEALING_CAP") Then
                Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                If heal IsNot Nothing Then heal.Visible = True
            End If
            ' Show abutment if present
            If trtsList.Any(Function(t) t.PropertyName = "ABUTMENT") Then
                Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                If abut IsNot Nothing Then abut.Visible = True
            End If
            ' ----------------------------
            ' Special handling for top view
            ' ----------------------------
            If svg.Name.Contains("Top") AndAlso maxLevel = 7 Then
                If col.Find(Function(c) c.Id = "IMPLANT") IsNot Nothing Then col.Find(Function(c) c.Id = "IMPLANT").Visible = False
                If col.Find(Function(c) c.Id = "ABUTMENT") IsNot Nothing Then col.Find(Function(c) c.Id = "ABUTMENT").Visible = False
                If col.Find(Function(c) c.Id = "HEALING_CAP") IsNot Nothing Then col.Find(Function(c) c.Id = "HEALING_CAP").Visible = False
                ShowBaseTooth(col)
            ElseIf (svg.Name.Contains("Out") OrElse svg.Name.Contains("IN")) AndAlso maxLevel = 7 Then
                If col.Find(Function(c) c.Id = "IMPLANT") IsNot Nothing Then col.Find(Function(c) c.Id = "IMPLANT").Visible = True
                If col.Find(Function(c) c.Id = "ABUTMENT") IsNot Nothing Then col.Find(Function(c) c.Id = "ABUTMENT").Visible = False
                If col.Find(Function(c) c.Id = "HEALING_CAP") IsNot Nothing Then col.Find(Function(c) c.Id = "HEALING_CAP").Visible = False
            End If
            ' ----------------------------
            ' Implant crowns with "I" ending
            ' ----------------------------
            For Each t In trtsList
                If t.LVL = 7 AndAlso t.Treat.EndsWith("I") Then
                    Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL")
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

#Region "BRIDGE"
    Public Sub HandleBridgeTreatment1(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
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
        '' 1. Handle bridge differently per view

        If svg.Name.Contains("Top") Then
            For Each item As SvgImageItem In col
                item.Visible = False
            Next
            ' Top View - style the base tooth as crown
            ShowBaseTooth(col)
            'Show BRIDGE Mark
            Dim bridgMark = col.Find(Function(c) c.Id = "BR")
            If bridgMark IsNot Nothing Then
                bridgMark.Visible = True
                bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
            End If
        ElseIf svg.Name.Contains("In") OrElse svg.Name.Contains("IN") OrElse svg.Name.Contains("Out") Then

            'if tooth not extracted, show normal treats and then bridge crown
            'normal treats levels:0,1,2,3
            'extraction level =4
            'imolant level =5,6,7
            'bridge level =8
            For Each t In trtsList
                Dim item = col.Find(Function(c) c.Id = t.PropertyName)
                If item IsNot Nothing Then
                    item.Visible = True
                    item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                    item.Appearance.Normal.BorderThickness = t.BorderThickness
                End If
            Next
            Dim trts = trtsList.Where(Function(t) t.LVL < 4).ToList()
            If trts.Any() Then
                ShowBaseTooth(col)
                For Each t In trts
                    Dim item = col.Find(Function(c) c.Id = t.PropertyName)
                    If item IsNot Nothing Then
                        item.Visible = True
                        item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                        item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                        item.Appearance.Normal.BorderThickness = t.BorderThickness
                    End If
                Next
            End If

            ' 2. Always show CROWN_IMG if IMPLANT exists
            If trtsList.Any(Function(t) t.PropertyName.Contains("IMPLANT")) Then
                Dim IMPLANT = col.Find(Function(c) c.Id = "IMPLANT")
                If IMPLANT IsNot Nothing Then IMPLANT.Visible = True
                ' Out View - show dedicated crown layer
                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                If crownImg IsNot Nothing Then
                    crownImg.Visible = True
                End If
            End If
            'Show BRIDGE Mark
            Dim bridgMark = col.Find(Function(c) c.Id = "BR")
            If bridgMark IsNot Nothing Then
                bridgMark.Visible = True
                bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
            End If
            ' Out View - show dedicated crown layer
            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
            If crownItem IsNot Nothing Then
                crownItem.Visible = True
            End If
            ' 3. Always show extraction with Yellow for bridge
            Dim EXTRACTED1 = col.Find(Function(c) c.Id = "EXTRACTED")
            If EXTRACTED1 IsNot Nothing Then EXTRACTED1.Visible = True
            EXTRACTED1.Appearance.Normal.FillColor = Color.Transparent
            EXTRACTED1.Appearance.Normal.BorderColor = Color.Yellow
            EXTRACTED1.Appearance.Normal.BorderThickness = 4
        End If
        'Show Crown Fill with bridge style
        Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
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

    Public Sub HandleBridgeTreatment(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
        ' ---------- TOP VIEW ----------
        If svg.Name.Contains("Top") Then
            For Each item As SvgImageItem In col
                item.Visible = False
            Next
            ShowBaseTooth(col)
            ShowBridgeMark(col)
            ApplyBridgeCrownFill(col, trtsList)
            Exit Sub
        End If
        ' ---------- IN / OUT VIEWS ----------
        If Not (svg.Name.IndexOf("In", StringComparison.OrdinalIgnoreCase) >= 0 _
        OrElse svg.Name.IndexOf("Out", StringComparison.OrdinalIgnoreCase) >= 0) Then
            Exit Sub
        End If
        ' ---------- CLASSIFY ----------
        Dim normalTrts = trtsList.Where(Function(t) t.LVL < 4).ToList()
        Dim hasExtraction = trtsList.Any(Function(t) t.LVL = 4)
        Dim hasImplant = trtsList.Any(Function(t) t.LVL >= 5 AndAlso t.LVL <= 7)
        Dim hasBridge = trtsList.Any(Function(t) _
        t.Treat.IndexOf("BRIDGE", StringComparison.OrdinalIgnoreCase) >= 0)
        ' Reset everything ONCE
        For Each item As SvgImageItem In col
            item.Visible = False
        Next
        ' ---------- RENDER ----------
        Select Case True
        ' ---- IMPLANT ----
            Case hasImplant
                ShowItem(col, "IMPLANT")
                ShowItem(col, "CROWN_IMG")
        ' ---- EXTRACTED ----
            Case hasExtraction
                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                If extracted IsNot Nothing Then
                    extracted.Visible = True
                    extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#55FFC719") ' Color.Transparent
                    extracted.Appearance.Normal.BorderColor = ColorTranslator.FromHtml("#55FFC719") 'Color.Yellow
                    extracted.Appearance.Normal.BorderThickness = 4
                End If
                ShowItem(col, "CROWN_IMG")
        ' ---- NORMAL or BRIDGE-ONLY ----
            Case normalTrts.Any() OrElse hasBridge
                ShowBaseTooth(col)
                ApplyTreatments(col, normalTrts)
                ShowItem(col, "CROWN_IMG")
        End Select
        ' ---------- BRIDGE VISUALS (always if bridge exists) ----------
        If hasBridge Then
            ShowBridgeMark(col)
            ApplyBridgeCrownFill(col, trtsList)
            ' ---------- BRIDGE EXTRACTION OUTLINE (always when bridge exists) ----------
            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
            If extracted IsNot Nothing Then
                extracted.Visible = True
                extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#80EDF3FA") '  Color.Transparent
                extracted.Appearance.Normal.BorderColor = Color.White 'FromArgb(12, 237, 243, 250)
                extracted.Appearance.Normal.BorderThickness = 2
            End If
        End If
    End Sub

    Private Sub ApplyTreatments(col As SvgImageItemCollection, trts As IEnumerable(Of Patient_ToothTrt))
        For Each t In trts
            Dim item = col.Find(Function(c) c.Id = t.PropertyName)
            If item Is Nothing Then Continue For
            item.Visible = True
            item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
            item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
            item.Appearance.Normal.BorderThickness = t.BorderThickness
        Next
    End Sub

    Private Sub ShowItem(col As SvgImageItemCollection, id As String)
        Dim item = col.Find(Function(c) c.Id = id)
        If item IsNot Nothing Then item.Visible = True
    End Sub

    Private Sub ShowBridgeMark(col As SvgImageItemCollection)
        Dim br = col.Find(Function(c) c.Id = "BR")
        If br IsNot Nothing Then
            br.Visible = True
            br.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
        End If
    End Sub

    Private Sub ApplyBridgeCrownFill(col As SvgImageItemCollection, trtsList As IEnumerable(Of Patient_ToothTrt))
        Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
        If crownFill Is Nothing Then Exit Sub
        Dim bridgeTrt = trtsList.FirstOrDefault(Function(t) t.Treat Like "*BRIDGE*")
        If bridgeTrt IsNot Nothing Then
            crownFill.Visible = True
            crownFill.Appearance.Normal.FillColor =
            ColorTranslator.FromHtml(bridgeTrt.FillColor)
        End If
    End Sub


#End Region




    Public Sub HandleDENTURE(svg As SvgImageBox, col As SvgImageItemCollection, trtsList As List(Of Patient_ToothTrt))
        ResetSvgItemsVisibility(col)
        Dim dentRow = trtsList.FirstOrDefault(Function(t) IsDentureTreatName(t.Treat))
        If svg.Name.Contains("Top") Then
            ShowBaseTooth(col)
            Dim dentMark = col.Find(Function(c) c.Id = "CH")
            If dentMark IsNot Nothing Then
                dentMark.Visible = True
                dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
            End If
        ElseIf svg.Name.Contains("Out") OrElse svg.Name.Contains("IN") Then
            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
            If extracted IsNot Nothing Then extracted.Visible = False
            Dim dentMark = col.Find(Function(c) c.Id = "CH")
            If dentMark IsNot Nothing Then
                dentMark.Visible = True
                dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
            End If
            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG")
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



End Module
