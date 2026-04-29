Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' Central place for treatment / mobile TreeView loading for jaw controls.
''' Normal chart uses <see cref="LoadTreatTreeSingle"/> / <see cref="LoadTreatTreeMulti"/> (delegates to TrtSourceHelper).
''' Mobile SQL follows the same level matrix as <see cref="TrtSourceHelper.SetMobTreeSingleTooth"/> / <see cref="TrtSourceHelper.SetMobTreeMultiTeeth"/>,
''' with an expanded TrtGroup list plus dentures always allowed where level filters would hide them.
''' Cross-arch rule: strip BRIDGE rows only when selection spans upper and lower adult arches.
''' </summary>
Public NotInheritable Class JawTreatmentTreeHelper
    Private Sub New()
    End Sub

    Private Shared ReadOnly MobileTrtGroupInClause As String =
        "([TrtGroup] IN ('EXTRACTION', 'IMPLANT', 'DENTURES', 'BRIDGE', 'CROWNS ON IMPLANT', 'IMPLANT COMPONENT'))"

    ''' <summary>True when more than one tooth is selected and selection spans both adult arches (FDI).</summary>
    Public Shared Function IsCrossArchMultiSelection(fdiTeeth As IEnumerable(Of Integer)) As Boolean
        If fdiTeeth Is Nothing Then Return False
        Dim list = fdiTeeth.Distinct().ToList()
        If list.Count <= 1 Then Return False
        Dim hasUpper = list.Any(Function(t) t >= 11 AndAlso t <= 28)
        Dim hasLower = list.Any(Function(t) t >= 31 AndAlso t <= 48)
        Return hasUpper AndAlso hasLower
    End Function

    Private Shared Sub RemoveBridgeOnlyForCrossArch(dt As DataTable)
        If dt Is Nothing OrElse Not dt.Columns.Contains("TrtGroup") Then Return
        For i = dt.Rows.Count - 1 To 0 Step -1
            Dim g = If(IsDBNull(dt.Rows(i)("TrtGroup")), "", Convert.ToString(dt.Rows(i)("TrtGroup")))
            If String.Equals(g, "BRIDGE", StringComparison.OrdinalIgnoreCase) Then
                dt.Rows.RemoveAt(i)
            End If
        Next
    End Sub

    ''' <summary>General treatment chart: single tooth (excludes DIAG when not in diagnosis mode; dentures included).</summary>
    Public Shared Sub LoadTreatTreeSingle(
        treeView As TreeView,
        toothID As String,
        toothNum As Byte,
        patientId As Integer,
        isKid As Boolean,
        ByRef allTrtNodes As List(Of TrtSourceHelper.TrtNodeInfo),
        ByRef fullTreeSnapshot As List(Of TreeNode),
        Optional useDiagnosis As Boolean = False)

        If treeView Is Nothing Then Return
        If TrtSourceHelper.PatientHasDentureOnAnyTooth(patientId, New Integer() {CInt(toothNum)}) Then
            TrtSourceHelper.BuildTreeEmptyBlockedDentureTooth(treeView, useDiagnosis, multiSelect:=False, allTrtNodes, fullTreeSnapshot)
            Return
        End If
        Dim clsToothTrtData As New Patient_ToothTrtDATA
        Dim currentToothLevel = clsToothTrtData.GetTreatLVL(patientId, toothNum)
        Dim existing = If(useDiagnosis,
            TrtSourceHelper.GetExistingDiagSingleTooth(patientId, toothNum),
            TrtSourceHelper.GetExistingTrtsSingleTooth(patientId, toothNum))
        Dim results = TrtSourceHelper.GetFilteredTrtsSingleTooth(
            toothID, isKid, currentToothLevel, excludeDiagGroup:=Not useDiagnosis)
        If Not useDiagnosis Then
            results = TrtSourceHelper.ApplyToothSelectionOtherTreatAllowlist(results)
        End If
        allTrtNodes = results
        If useDiagnosis Then
            TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, existing,
                "No available diagnoses for current tooth status", "DIAGNOSES", Color.DarkRed)
        Else
            TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, existing,
                "No available treatments for current tooth status")
        End If
        fullTreeSnapshot = TrtSourceHelper.SaveFullTreeSnapshot(treeView)
    End Sub

    ''' <summary>General treatment chart: multi-tooth (SQL pipeline in TrtSourceHelper; bridge already excluded for mixed arch there).</summary>
    Public Shared Sub LoadTreatTreeMulti(
        treeView As TreeView,
        selectedFdiTeeth As IList(Of Integer),
        patientId As Integer,
        isKid As Boolean,
        ByRef allTrtNodes As List(Of TrtSourceHelper.TrtNodeInfo),
        ByRef fullTreeSnapshot As List(Of TreeNode),
        Optional useDiagnosis As Boolean = False)

        TrtSourceHelper.SetTrtsTreeMultiTeeth(
            treeView,
            selectedFdiTeeth,
            patientId,
            isKid,
            allTrtNodes,
            fullTreeSnapshot,
            useDiagnosis)
    End Sub

    ''' <summary>Mobile workflow: single tooth. Same level rules as <see cref="TrtSourceHelper.SetMobTreeSingleTooth"/> plus expanded TrtGroups; dentures always included when a level filter would drop them.</summary>
    Public Shared Sub LoadMobileTreeSingle(
        treeView As TreeView,
        toothID As String,
        toothNum As Byte,
        patientId As Integer,
        isKid As Boolean,
        ByRef allTrtNodes As List(Of TrtSourceHelper.TrtNodeInfo),
        ByRef fullTreeSnapshot As List(Of TreeNode),
        Optional useDiagnosis As Boolean = False)

        If treeView Is Nothing Then Return
        If TrtSourceHelper.PatientHasDentureOnAnyTooth(patientId, New Integer() {CInt(toothNum)}) Then
            TrtSourceHelper.BuildTreeEmptyBlockedDentureTooth(treeView, useDiagnosis, multiSelect:=False, allTrtNodes, fullTreeSnapshot)
            Return
        End If
        Dim clsToothTrtData As New Patient_ToothTrtDATA
        Try
            treeView.Nodes.Clear()
            Dim currentToothLevel As Integer = clsToothTrtData.GetTreatLVL(patientId, toothNum)

            Dim query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], 
            [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],
            [TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],
            [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] 
            From [TblTRTS] LEFT Join [Shapes] On [TblTRTS].[ShapeID] = [Shapes].[ShapeID] 
            WHERE " & MobileTrtGroupInClause & " "

            If isKid Then
                query += " AND ([KidTrt] IN (1,  2)) "
            Else
                query += " AND ([KidTrt] IN (0,  2)) "
            End If

            ' Match TrtSourceHelper.SetMobTreeSingleTooth; OR DENTURES so dentures stay available on mobile for every level.
            Select Case currentToothLevel
                Case -1, 0, 1, 2, 3
                    query += " AND ([TrtGroup] NOT IN ('IMPLANT', 'CROWNS ON IMPLANT','IMPLANT COMPONENT') OR [TrtGroup] = 'DENTURES')"
                Case 4
                    query += " AND ([TrtGroup] IN ('IMPLANT', 'BRIDGE') OR [TrtGroup] = 'DENTURES')"
                Case 5
                    query += " AND (([Trt] = 'EXTRACTION' OR [Trt] = 'ABUTMENT' OR [Trt] = 'HEALING CAP' OR [TrtGroup] = 'CROWNS ON IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'IMPLANT' OR [TrtGroup] = 'IMPLANT COMPONENT') OR [TrtGroup] = 'DENTURES')"
                Case 6, 7, 8
                    query += " AND (([Trt] = 'EXTRACTION') OR [TrtGroup] = 'DENTURES')"
            End Select

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            Dim connectionString = DentistXDATA.GetConnection.ConnectionString
            Dim existingTrts As HashSet(Of Integer) = If(useDiagnosis,
                TrtSourceHelper.GetExistingDiagSingleTooth(patientId, toothNum),
                TrtSourceHelper.GetExistingTrtsSingleTooth(patientId, toothNum))

            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ToothID", toothID)
                command.Parameters.AddWithValue("@CurrentToothLevel", currentToothLevel)
                Dim adapter As New SqlDataAdapter(command)
                Dim dataTable As New DataTable()
                adapter.Fill(dataTable)
                allTrtNodes = TrtSourceHelper.BuildTrtNodesFromDataTable(dataTable)
            End Using

            If Not useDiagnosis Then
                allTrtNodes = TrtSourceHelper.ApplyToothSelectionOtherTreatAllowlist(allTrtNodes)
            End If
            If useDiagnosis Then
                TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, existingTrts,
                    "No available diagnoses for current tooth status", "DIAGNOSES", Color.DarkRed)
            Else
                TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, existingTrts,
                    "No available treatments for current tooth status")
            End If
            fullTreeSnapshot = TrtSourceHelper.SaveFullTreeSnapshot(treeView)
        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub

    ''' <summary>Mobile workflow: multi-tooth. Same matrix as <see cref="TrtSourceHelper.SetMobTreeMultiTeeth"/> plus expanded TrtGroups and dentures preserved; cross-arch strips BRIDGE only.</summary>
    Public Shared Sub LoadMobileTreeMulti(
        treeView As TreeView,
        selectedFdiTeeth As IEnumerable(Of Integer),
        patientId As Integer,
        isKid As Boolean,
        ByRef allTrtNodes As List(Of TrtSourceHelper.TrtNodeInfo),
        ByRef fullTreeSnapshot As List(Of TreeNode),
        Optional useDiagnosis As Boolean = False)

        If treeView Is Nothing Then Return
        Dim clsToothTrtData As New Patient_ToothTrtDATA
        Dim teeth = If(selectedFdiTeeth, Enumerable.Empty(Of Integer)()).ToList()
        Try
            treeView.Nodes.Clear()

            If TrtSourceHelper.PatientHasDentureOnAnyTooth(patientId, teeth) Then
                TrtSourceHelper.BuildTreeEmptyBlockedDentureTooth(treeView, useDiagnosis, multiSelect:=True, allTrtNodes, fullTreeSnapshot)
                Return
            End If

            Dim hasUpperTeeth As Boolean = teeth.Any(Function(t) t >= 11 AndAlso t <= 28)
            Dim hasLowerTeeth As Boolean = teeth.Any(Function(t) t >= 31 AndAlso t <= 48)
            Dim isMixedSelection As Boolean = hasUpperTeeth AndAlso hasLowerTeeth
            Dim crossArchMulti = IsCrossArchMultiSelection(teeth)

            Dim maxToothLevel As Integer = -1
            Dim allLevels As New List(Of Integer)
            For Each ToothNum In teeth
                Dim lvls = clsToothTrtData.GetTreatLVLs(patientId, CByte(ToothNum))
                For Each lvl In lvls
                    If Not allLevels.Contains(lvl) Then allLevels.Add(lvl)
                Next
            Next

            For Each level In allLevels
                Dim simplifiedLevel = If(level <= 3, -1, If(level = 4, 4, 5))
                maxToothLevel = Math.Max(maxToothLevel, simplifiedLevel)
            Next

            Dim toothIDParam As String = If(teeth.Count = 1,
                (teeth(0) Mod 10).ToString(),
                String.Join(",", teeth.Select(Function(t) t Mod 10)))

            Dim query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], 
            [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],
            [TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],
            [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] 
            From [TblTRTS] LEFT Join [Shapes] On [TblTRTS].[ShapeID] = [Shapes].[ShapeID] 
             WHERE " & MobileTrtGroupInClause & " "

            If teeth.Count > 1 AndAlso isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            ElseIf teeth.Count = 16 AndAlso Not isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            End If

            Dim norTrt As Boolean = allLevels.Any(Function(l) l < 4)
            Dim exTrt As Boolean = allLevels.Contains(4)
            Dim impTrt As Boolean = allLevels.Any(Function(l) l >= 5 AndAlso l <= 7)
            Dim brgTrt As Boolean = allLevels.Contains(8)

            ' Match TrtSourceHelper.SetMobTreeMultiTeeth; OR DENTURES on restrictive arms so dentures always appear on mobile.
            Select Case True
                Case Not norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND 1=1"
                Case norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND ([TrtGroup] NOT IN ('IMPLANT', 'CROWNS ON IMPLANT','IMPLANT COMPONENT') OR [TrtGroup] = 'DENTURES')"
                Case Not norTrt AndAlso exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND ([TrtGroup] IN ('IMPLANT', 'BRIDGE') OR [TrtGroup] = 'DENTURES')"
                Case norTrt AndAlso exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND ([TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
                Case Not norTrt AndAlso Not exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND (([Trt] = 'EXTRACTION' OR [Trt] = 'ABUTMENT' OR [Trt] = 'HEALING CAP' OR [TrtGroup] = 'CROWNS ON IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'IMPLANT' OR [TrtGroup] = 'IMPLANT COMPONENT') OR [TrtGroup] = 'DENTURES')"
                Case norTrt AndAlso Not exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND (([Trt] = 'EXTRACTION' OR [TrtGroup] = 'BRIDGE') OR [TrtGroup] = 'DENTURES')"
                Case Not norTrt AndAlso exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND ([TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
                Case norTrt AndAlso exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND ([TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
                Case Not norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND (([Trt] = 'EXTRACTION') OR [TrtGroup] = 'DENTURES')"
                Case norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND (([Trt] = 'EXTRACTION') OR [TrtGroup] = 'DENTURES')"
                Case Not norTrt AndAlso exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND [TrtGroup] = 'DENTURES'"
                Case norTrt AndAlso exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND [TrtGroup] = 'DENTURES'"
                Case Not norTrt AndAlso Not exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND (([Trt] = 'EXTRACTION') OR [TrtGroup] = 'DENTURES')"
                Case norTrt AndAlso Not exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND (([Trt] = 'EXTRACTION') OR [TrtGroup] = 'DENTURES')"
                Case Not norTrt AndAlso exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND [TrtGroup] = 'DENTURES'"
                Case norTrt AndAlso exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND [TrtGroup] = 'DENTURES'"
            End Select

            If isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE'"
            End If

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            Dim connectionString = DentistXDATA.GetConnection.ConnectionString
            Dim connection As New SqlConnection(connectionString)

            Dim existingTrts As New HashSet(Of Integer)
            Dim toothParams As New List(Of String)
            For i = 0 To teeth.Count - 1
                toothParams.Add("@ToothNum" & i)
            Next
            Dim inClause = String.Join(", ", toothParams)

            Dim sql As String
            If useDiagnosis Then
                sql = "SELECT dbo.TblTRTS.TrtID FROM [dbo].[Patient_Diagnosis] INNER JOIN dbo.TblTRTS ON [dbo].[Patient_Diagnosis].Treat = dbo.TblTRTS.Trt WHERE [dbo].[Patient_Diagnosis].LVL=9 AND PatientID = @PatientID AND ToothNum IN (" & inClause & ")"
            Else
                sql = "SELECT dbo.TblTRTS.TrtID FROM [dbo].[Patient_ToothTrt] INNER JOIN dbo.TblTRTS ON [dbo].[Patient_ToothTrt].Treat = dbo.TblTRTS.Trt WHERE [dbo].[Patient_ToothTrt].LVL=9 AND PatientID = @PatientID AND ToothNum IN (" & inClause & ")"
            End If

            If teeth.Count > 0 Then
                Using checkCmd As New SqlCommand(sql, connection)
                    checkCmd.Parameters.AddWithValue("@PatientID", patientId)
                    For i = 0 To teeth.Count - 1
                        checkCmd.Parameters.AddWithValue("@ToothNum" & i, teeth(i))
                    Next
                    Try
                        connection.Open()
                        Using rdr = checkCmd.ExecuteReader()
                            While rdr.Read()
                                existingTrts.Add(rdr.GetInt32(0))
                            End While
                        End Using
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        connection.Close()
                    End Try
                End Using
            End If

            Dim dataTable As New DataTable()
            If teeth.Count > 0 Then
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ToothID", toothIDParam)
                    command.Parameters.AddWithValue("@CurrentToothLevel", maxToothLevel)
                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using
            End If

            FilterCompleteDentureTreeRowsUnlessValidFullArch(dataTable, teeth, isKid)

            If crossArchMulti Then
                RemoveBridgeOnlyForCrossArch(dataTable)
            End If

            allTrtNodes = TrtSourceHelper.BuildTrtNodesFromDataTable(dataTable)
            If Not useDiagnosis Then
                allTrtNodes = TrtSourceHelper.ApplyToothSelectionOtherTreatAllowlist(allTrtNodes)
            End If
            If useDiagnosis Then
                TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, existingTrts,
                    "No available diagnoses for the selected teeth.", "DIAGNOSES", Color.DarkRed)
            Else
                TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, existingTrts,
                    "No available treatments for the selected teeth.")
            End If
            fullTreeSnapshot = TrtSourceHelper.SaveFullTreeSnapshot(treeView)
        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub

    ''' <summary>Right-click empty jaw: COMPLETE DENTURE + OTHER subset; diagnosis mode shows a placeholder tree.</summary>
    Public Shared Sub LoadEmptyJawBackgroundTreatTree(
        treeView As TreeView,
        isKid As Boolean,
        ByRef allTrtNodes As List(Of TrtSourceHelper.TrtNodeInfo),
        ByRef fullTreeSnapshot As List(Of TreeNode),
        Optional useDiagnosis As Boolean = False)

        If treeView Is Nothing Then Return
        If useDiagnosis Then
            allTrtNodes = New List(Of TrtSourceHelper.TrtNodeInfo)()
            TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, Nothing,
                "Right-click a tooth to view diagnoses.",
                "DIAGNOSES", Color.DarkRed)
            fullTreeSnapshot = TrtSourceHelper.SaveFullTreeSnapshot(treeView)
            treeView.ExpandAll()
            Return
        End If

        allTrtNodes = TrtSourceHelper.BuildTrtNodesForEmptyJawBackgroundPanel(isKid)
        TrtSourceHelper.BuildTreeFromList(treeView, allTrtNodes, Nothing,
            "No denture/other treatments found — check TblTRTS and TblOtherTRT.")
        fullTreeSnapshot = TrtSourceHelper.SaveFullTreeSnapshot(treeView)
        treeView.ExpandAll()
    End Sub

    ''' <summary>Adult FDI: exactly 16 distinct teeth, all in one arch (upper 11–28 or lower 31–48).</summary>
    Public Shared Function IsAdultFullArchCompleteDentureSelection(fdiTeeth As IEnumerable(Of Integer)) As Boolean
        If fdiTeeth Is Nothing Then Return False
        Dim list = fdiTeeth.Distinct().ToList()
        If list.Count <> 16 Then Return False
        Dim allUpper = list.All(Function(t) t >= 11 AndAlso t <= 28)
        Dim allLower = list.All(Function(t) t >= 31 AndAlso t <= 48)
        Return allUpper OrElse allLower
    End Function

    ''' <summary>Remove COMPLETE DENTURE / CD from a treatment tree DataTable unless the multi-selection is a valid full arch (adult only).</summary>
    Public Shared Sub FilterCompleteDentureTreeRowsUnlessValidFullArch(dataTable As DataTable, selectedFdiTeeth As IEnumerable(Of Integer), isKid As Boolean)
        If dataTable Is Nothing OrElse Not dataTable.Columns.Contains("Trt") Then Return
        If isKid Then Return
        If IsAdultFullArchCompleteDentureSelection(selectedFdiTeeth) Then Return
        For i = dataTable.Rows.Count - 1 To 0 Step -1
            Dim raw = If(IsDBNull(dataTable.Rows(i)("Trt")), "", Convert.ToString(dataTable.Rows(i)("Trt")).Trim())
            Dim key = Helpers.GetFirstTreatmentPart(raw).ToUpperInvariant()
            If key = "COMPLETE DENTURE" OrElse key = "CD" Then
                dataTable.Rows.RemoveAt(i)
            End If
        Next
    End Sub

    Public Shared Function IsCompleteDentureCatalogName(treatName As String) As Boolean
        If String.IsNullOrWhiteSpace(treatName) Then Return False
        Dim key = Helpers.GetFirstTreatmentPart(treatName.Trim()).ToUpperInvariant()
        Return key = "COMPLETE DENTURE" OrElse key = "CD"
    End Function

    Private Shared Function SafeSvgToothFdi(svg As SvgImageBox) As Integer
        If svg Is Nothing Then Return 0
        Try
            Return Convert.ToInt32(svg.Tag)
        Catch
            Return 0
        End Try
    End Function

    ''' <summary>One OUT (or OUTK) surface per distinct tooth Tag, sorted by FDI / chart number.</summary>
    Public Shared Function GetOutViewSvgsPerToothSorted(jawPanel As Control) As List(Of SvgImageBox)
        Dim result As New List(Of SvgImageBox)()
        If jawPanel Is Nothing Then Return result
        Dim groups = jawPanel.Controls.OfType(Of SvgImageBox)().
            Where(Function(s) s IsNot Nothing AndAlso s.Name IsNot Nothing AndAlso
                s.Name.IndexOf("OUT", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso SafeSvgToothFdi(s) > 0).
            GroupBy(Function(s) SafeSvgToothFdi(s))
        For Each g In groups
            result.Add(g.First())
        Next
        Return result.OrderBy(Function(s) SafeSvgToothFdi(s)).ToList()
    End Function

    ''' <summary>
    ''' Empty-jaw tree: double-click / Enter with no tooth selection applies the leaf to all OUT-view teeth on this panel.
    ''' Adult COMPLETE DENTURE: one full arch (16 adult FDI on upper or lower) uses one save; combined upper+lower panel with
    ''' 16+16 OUT teeth runs two saves (upper arch then lower) — same as two separate full-arch adds. Whole-mouth OTHER treats unchanged.
    ''' </summary>
    ''' <returns>True if handled (caller skips normal tooth-target flow).</returns>
    Public Shared Function TryApplyEmptyJawTreeLeafWithoutSelection(
        node As TreeNode,
        jawPanel As Control,
        hasSelectedTeethSvgs As Boolean,
        hasDragSource As Boolean,
        isKidChart As Boolean,
        buildToothTrt As Func(Of SvgImageBox, TreeNode, String, Patient_ToothTrt),
        applySaveFlow As Func(Of List(Of Patient_ToothTrt), DialogResult)) As Boolean

        If hasSelectedTeethSvgs OrElse hasDragSource Then Return False
        If node Is Nothing OrElse node.Parent Is Nothing OrElse node.Nodes.Count > 0 Then Return False
        If node.Tag Is Nothing Then Return False
        If Not TypeOf node.Tag Is TrtSourceHelper.TrtNodeInfo Then Return False

        Dim svgs = GetOutViewSvgsPerToothSorted(jawPanel)
        If svgs.Count = 0 Then Return False

        Dim treat = node.Text.Trim()
        If String.IsNullOrEmpty(treat) Then Return False

        If Not isKidChart AndAlso IsCompleteDentureCatalogName(treat) Then
            Dim fdis = svgs.Select(Function(s) SafeSvgToothFdi(s)).Distinct().ToList()
            If IsCrossArchMultiSelection(fdis) Then
                Dim upperSvgs = svgs.Where(Function(s)
                                               Dim f = SafeSvgToothFdi(s)
                                               Return f >= 11 AndAlso f <= 28
                                           End Function).OrderBy(Function(s) SafeSvgToothFdi(s)).ToList()
                Dim lowerSvgs = svgs.Where(Function(s)
                                               Dim f = SafeSvgToothFdi(s)
                                               Return f >= 31 AndAlso f <= 48
                                           End Function).OrderBy(Function(s) SafeSvgToothFdi(s)).ToList()
                Dim upperFdi = upperSvgs.Select(Function(s) SafeSvgToothFdi(s)).Distinct().ToList()
                Dim lowerFdi = lowerSvgs.Select(Function(s) SafeSvgToothFdi(s)).Distinct().ToList()
                Dim coversBothArches = svgs.Count = upperSvgs.Count + lowerSvgs.Count
                If coversBothArches AndAlso
                   IsAdultFullArchCompleteDentureSelection(upperFdi) AndAlso
                   IsAdultFullArchCompleteDentureSelection(lowerFdi) Then
                    Dim listUpper As New List(Of Patient_ToothTrt)()
                    For Each svg In upperSvgs
                        Dim row = buildToothTrt(svg, node, treat)
                        If row IsNot Nothing Then listUpper.Add(row)
                    Next
                    If listUpper.Count = 0 Then Return False
                    Dim r1 = applySaveFlow(listUpper)
                    If r1 <> DialogResult.OK Then Return True
                    Dim listLower As New List(Of Patient_ToothTrt)()
                    For Each svg In lowerSvgs
                        Dim row = buildToothTrt(svg, node, treat)
                        If row IsNot Nothing Then listLower.Add(row)
                    Next
                    If listLower.Count = 0 Then Return False
                    applySaveFlow(listLower)
                    Return True
                End If
                MessageBox.Show(
                    "Complete denture applies to one full arch at a time. Use the upper- or lower-jaw chart, or select 16 teeth on a single arch.",
                    "Complete denture",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                Return True
            End If
            If Not IsAdultFullArchCompleteDentureSelection(fdis) Then
                MessageBox.Show(
                    "Complete denture requires exactly 16 teeth on one adult arch (upper FDI 11–28 or lower 31–48).",
                    "Complete denture",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                Return True
            End If
        End If

        ' Whole-mouth other treats: one chart row with ToothNum 0 and arch label (no per-tooth / no quadrant grid).
        If TrtSourceHelper.IsWholeMouthOtherTreat(treat) Then
            Dim template = buildToothTrt(svgs(0), node, treat)
            If template Is Nothing Then Return False
            Dim one = template.Clone()
            one.ToothNum = 0
            one.ToothName = TrtSourceHelper.FormatWholeMouthLabel(Module1.Eng)
            applySaveFlow(New List(Of Patient_ToothTrt) From {one})
            Return True
        End If

        Dim list As New List(Of Patient_ToothTrt)()
        For Each svg In svgs
            Dim row = buildToothTrt(svg, node, treat)
            If row IsNot Nothing Then list.Add(row)
        Next
        If list.Count = 0 Then Return False

        applySaveFlow(list)
        Return True
    End Function
End Class
