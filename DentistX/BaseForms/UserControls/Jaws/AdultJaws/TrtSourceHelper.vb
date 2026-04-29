Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Public Class TrtSourceHelper

    ''' <summary>When tooth has bridge and is not extracted, these treatments are allowed in the tree (single and multi select). Includes RCT group.</summary>
    Private Shared ReadOnly BridgeNotExtractedAllowedTrts As String() = {
        "ABSCESS DRAINAGE", "APEXIFICATION", "APICECTOMY", "EXTRACTION", "RCM", "RCC", "RCO", "REDO RCT", "TF",
        "RCC TF", "RCO TF", "RCT (ELSE WHERE)", "REDO (ELSE WHERE-NERVE)", "RCF GI", "REDO AMALGAM", "REDO COMPOSITE", "REDO GI", "REDO (IN CLINIC)", "RCM TF"
    }

    Private Shared ReadOnly CompleteDentureCatalogName As String = "COMPLETE DENTURE"

    ''' <summary>OTHER TREATS for empty jaw / full-mouth context (must exist in TblOtherTRT when that table is non-empty).</summary>
    Private Shared ReadOnly EmptyJawBackgroundOtherTrtNames As String() = {
        "Scaling and Polishing",
        "Fluoride Varnish",
        "Laser Bleaching (at clinic)",
        "Home Bleaching",
        "Diastema Closure",
        "Gingivectomy",
        "Labial Frenectomy",
        "Buccal Frenectomy",
        "Lingual Frenotomy",
        "Lingual Frenectomy"
    }

    ''' <summary>OTHER TREATS allowed on tooth right-click; other TblOtherTRT-only rows are hidden in that context.</summary>
    Private Shared ReadOnly ToothSelectionOtherTrtAllowNames As String() = {
        "Crown Lengthening",
        "Cyst Enucleation",
        "Cyst Marsupialisation",
        "Operculectomy",
        "Space Maintainer",
        "Root Planning"
    }

    Private Shared Function OtherTreatNameInList(treatName As String, names As String()) As Boolean
        If String.IsNullOrWhiteSpace(treatName) OrElse names Is Nothing Then Return False
        Dim key = treatName.Trim()
        For Each n In names
            If String.Equals(key, n, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    ''' <summary>Full-mouth / empty-jaw other treats (no FDI on save — see whole-mouth save path).</summary>
    Public Shared Function IsWholeMouthOtherTreat(treatName As String) As Boolean
        Return OtherTreatNameInList(treatName, EmptyJawBackgroundOtherTrtNames)
    End Function

    ''' <summary>Single Patient_ToothTrt row with ToothNum = 0 and arch label only (no quadrant chart cell).</summary>
    Public Shared Function IsWholeMouthChartlessSave(treatName As String, toothNum As Byte) As Boolean
        Return IsWholeMouthOtherTreat(treatName) AndAlso toothNum = 0
    End Function

    ''' <summary>
    ''' Tooth-level "other" treats: TrtGroupID, Patient_TrtInfo, one Patient_Trts parent when saved for more than one tooth (bridges/dentures pattern).
    ''' </summary>
    Public Shared Function IsArchGroupedOtherTreat(treatName As String) As Boolean
        Return OtherTreatNameInList(treatName, ToothSelectionOtherTrtAllowNames)
    End Function

    ''' <summary>Patient_Trts.Detail suffix for grouped multi-tooth accounting (upper/lower arch labels).</summary>
    Public Shared Function FormatAccountingArchSummaryFromFdi(fdiTeeth As IEnumerable(Of Integer), useEnglish As Boolean) As String
        If fdiTeeth Is Nothing Then Return If(useEnglish, "arch (unspecified)", "الفك (غير محدد)")
        Dim nums = fdiTeeth.Distinct().ToList()
        If nums.Count = 0 Then Return If(useEnglish, "arch (unspecified)", "الفك (غير محدد)")
        Dim allAdultUpper = nums.All(Function(t) t >= 11 AndAlso t <= 28)
        Dim allAdultLower = nums.All(Function(t) t >= 31 AndAlso t <= 48)
        If allAdultUpper Then Return If(useEnglish, "Upper Arch", "الفك العلوي")
        If allAdultLower Then Return If(useEnglish, "Lower Arch", "الفك السفلي")
        Return String.Join(", ", nums)
    End Function

    ''' <summary>Label for whole-mouth other treats (Patient_ToothTrt.ToothName / accounting detail).</summary>
    Public Shared Function FormatWholeMouthLabel(useEnglish As Boolean) As String
        Return If(useEnglish, "Full Mouth", "الفم بالكامل")
    End Function

    ''' <summary>
    ''' Whole-mouth other treats are stored with ToothNum = 0. Chart layer rendering (<see cref="TreatHelper.ProcessToothTreatments"/>)
    ''' matches rows to each SvgImageBox by Tag (FDI). This expands each such row into clones with ToothNum set for every FDI on the panel.
    ''' DB and accounting stay a single row per procedure; this list is for display only.
    ''' </summary>
    Public Shared Function ExpandWholeMouthTreatsForJawDisplay(jawPanel As Control, patientTreats As IEnumerable(Of Patient_ToothTrt)) As List(Of Patient_ToothTrt)
        Dim list As New List(Of Patient_ToothTrt)()
        If patientTreats IsNot Nothing Then list.AddRange(patientTreats)
        If jawPanel Is Nothing OrElse list.Count = 0 Then Return list

        Dim fdis As New HashSet(Of Byte)()
        For Each svg As SvgImageBox In jawPanel.Controls.OfType(Of SvgImageBox)()
            If svg Is Nothing OrElse svg.Tag Is Nothing Then Continue For
            Try
                Dim v = Convert.ToInt32(svg.Tag)
                If v > 0 AndAlso v <= 255 Then fdis.Add(CByte(v))
            Catch
            End Try
        Next
        If fdis.Count = 0 Then Return list

        Dim inject As New List(Of Patient_ToothTrt)()
        For i = list.Count - 1 To 0 Step -1
            Dim t = list(i)
            If Not IsWholeMouthChartlessSave(t.Treat, t.ToothNum) Then Continue For
            list.RemoveAt(i)
            For Each fdi In fdis
                Dim c = t.Clone()
                c.ToothNum = fdi
                inject.Add(c)
            Next
        Next
        list.AddRange(inject)
        Return list
    End Function

    ''' <summary>
    ''' Load the canonical <see cref="Patient_ToothTrt"/> row for edit/delete from the AddedTrts list.
    ''' Whole-mouth rows are stored with <see cref="Patient_ToothTrt.ToothNum"/> = 0 but expanded per FDI for chart display;
    ''' <c>Select_RecordByTrtIDByTNum</c> with the selected SVG tooth Tag would not match the database row.
    ''' </summary>
    Public Shared Function LoadToothTrtForEdit(trtData As Patient_ToothTrtDATA, toothTrtID As Integer, patientID As Integer) As Patient_ToothTrt
        If trtData Is Nothing OrElse toothTrtID <= 0 OrElse patientID <= 0 Then Return Nothing
        Dim row = trtData.SelectByID(New Patient_ToothTrt With {.ToothTrtID = toothTrtID})
        If row Is Nothing OrElse row.PatientID <> patientID Then Return Nothing
        Return row
    End Function

    ''' <summary>True when the row is persisted as full-mouth (ToothNum = 0): catalog whole-mouth other treats or full-mouth tooth name in EN/AR.</summary>
    Public Shared Function IsStoredWholeMouthPatientRecord(t As Patient_ToothTrt) As Boolean
        If t Is Nothing Then Return False
        If IsWholeMouthChartlessSave(t.Treat, t.ToothNum) Then Return True
        If t.ToothNum <> 0 Then Return False
        Dim nm = If(t.ToothName, "").Trim()
        Return nm.Equals(FormatWholeMouthLabel(True), StringComparison.OrdinalIgnoreCase) OrElse
            nm.Equals(FormatWholeMouthLabel(False), StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared _tblOtherTrtNamesCache As HashSet(Of String)
    Private Shared ReadOnly _tblOtherTrtNamesLock As New Object

    Public Shared Function GetTblOtherTrtNameSet() As HashSet(Of String)
        If _tblOtherTrtNamesCache IsNot Nothing Then Return _tblOtherTrtNamesCache
        SyncLock _tblOtherTrtNamesLock
            If _tblOtherTrtNamesCache Is Nothing Then
                Dim hs As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                Try
                    Dim dal As New TblOtherTRTDATA()
                    For Each row In dal.SelectAll()
                        If row.Trt Is Nothing OrElse String.IsNullOrWhiteSpace(row.Trt) Then Continue For
                        hs.Add(row.Trt.Trim())
                    Next
                Catch
                End Try
                _tblOtherTrtNamesCache = hs
            End If
        End SyncLock
        Return _tblOtherTrtNamesCache
    End Function

    Private Shared Function PassesCatalogKidFilter(isKid As Boolean, trtRow As TblTRTS) As Boolean
        If isKid Then
            If String.Equals(trtRow.Trt, "FISSURE SEALENT", StringComparison.OrdinalIgnoreCase) Then Return False
            Return trtRow.KidTrt = 1 OrElse trtRow.KidTrt = 2
        End If
        If String.Equals(trtRow.Trt, "PRR", StringComparison.OrdinalIgnoreCase) Then Return False
        Return trtRow.KidTrt = 0 OrElse trtRow.KidTrt = 2
    End Function

    Private Shared Function ResolveShapeName(trtRow As TblTRTS, shapes As List(Of Shapes)) As String
        If shapes Is Nothing OrElse Not trtRow.ShapeID.HasValue Then Return Nothing
        Dim sid = trtRow.ShapeID.Value
        Dim sh = shapes.FirstOrDefault(Function(s) s.ShapeID = sid)
        If sh Is Nothing Then Return Nothing
        Return sh.ShapeName
    End Function

    Private Shared Function TrtNodeInfoFromCatalog(trtRow As TblTRTS, shapeName As String) As TrtNodeInfo
        Dim brd As Byte = 0
        If trtRow.TrtBrdrThick.HasValue Then brd = trtRow.TrtBrdrThick.Value
        Dim sid As Integer = 0
        If trtRow.ShapeID.HasValue Then sid = CInt(trtRow.ShapeID.Value)
        Return New TrtNodeInfo With {
            .TrtID = trtRow.TrtID,
            .Trt = trtRow.Trt,
            .TrtValue = If(trtRow.TrtValue.HasValue, trtRow.TrtValue.Value, 0D),
            .ParentGroup = trtRow.ParentGroup,
            .TrtGroup = trtRow.TrtGroup,
            .TrtColor = trtRow.TrtColor,
            .TrtLoc = trtRow.TrtLoc,
            .TrtBrdrClr = trtRow.TrtBrdrClr,
            .ShapeName = shapeName,
            .ShapeID = sid,
            .TrtBrdrThick = brd,
            .OldTrt = trtRow.OldTrt
        }
    End Function

    ''' <summary>DENTURES (COMPLETE DENTURE only) + OTHER TREATS subset for empty jaw right-click.</summary>
    Public Shared Function BuildTrtNodesForEmptyJawBackgroundPanel(isKid As Boolean) As List(Of TrtNodeInfo)
        Dim acc As New List(Of TrtNodeInfo)()
        Dim seenIds As New HashSet(Of Integer)()
        Dim allTrts = TblTRTSDATA.SelectAll()
        Dim shapes = ShapesDATA.SelectAll()
        Dim otherCatalog = GetTblOtherTrtNameSet()

        Dim dentureRow = allTrts.FirstOrDefault(Function(t) t.Trt IsNot Nothing AndAlso
            t.Trt.Trim().Equals(CompleteDentureCatalogName, StringComparison.OrdinalIgnoreCase) AndAlso
            t.TrtGroup IsNot Nothing AndAlso t.TrtGroup.Trim().Equals("DENTURES", StringComparison.OrdinalIgnoreCase) AndAlso
            PassesCatalogKidFilter(isKid, t))
        If dentureRow IsNot Nothing AndAlso seenIds.Add(dentureRow.TrtID) Then
            acc.Add(TrtNodeInfoFromCatalog(dentureRow, ResolveShapeName(dentureRow, shapes)))
        End If

        For Each name In EmptyJawBackgroundOtherTrtNames
            If otherCatalog.Count > 0 AndAlso Not otherCatalog.Contains(name) Then Continue For
            Dim row = allTrts.FirstOrDefault(Function(t) t.Trt IsNot Nothing AndAlso t.Trt.Trim().Equals(name, StringComparison.OrdinalIgnoreCase) AndAlso PassesCatalogKidFilter(isKid, t))
            If row IsNot Nothing AndAlso seenIds.Add(row.TrtID) Then
                acc.Add(TrtNodeInfoFromCatalog(row, ResolveShapeName(row, shapes)))
            End If
        Next

        Return acc.OrderBy(Function(n) n.ParentGroup).ThenBy(Function(n) n.TrtGroup).ThenBy(Function(n) n.Trt).ToList()
    End Function

    ''' <summary>Removes TblOtherTRT-catalog rows not in the tooth-context allowlist; all other rows stay.</summary>
    Public Shared Function ApplyToothSelectionOtherTreatAllowlist(nodes As List(Of TrtNodeInfo)) As List(Of TrtNodeInfo)
        If nodes Is Nothing Then Return New List(Of TrtNodeInfo)()
        If nodes.Count = 0 Then Return nodes
        Dim otherCatalog = GetTblOtherTrtNameSet()
        If otherCatalog.Count = 0 Then Return nodes

        Dim allow As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each s In ToothSelectionOtherTrtAllowNames
            allow.Add(s)
        Next

        Dim result As New List(Of TrtNodeInfo)(nodes.Count)
        For Each n In nodes
            Dim trtKey = If(n.Trt, String.Empty).Trim()
            If otherCatalog.Contains(trtKey) AndAlso Not allow.Contains(trtKey) Then
                Continue For
            End If
            result.Add(n)
        Next
        Return result
    End Function

    Public Structure TrtNodeInfo
        Public TrtID As Integer
        Public Trt As String
        Public TrtValue As Decimal
        Public TrtGroup As String
        Public ParentGroup As String
        Public OldTrt As String
        Public ShapeID As Integer
        Public ShapeName As String
        Public TrtColor As String
        Public TrtBrdrClr As String
        Public TrtBrdrThick As Byte
        Public TrtLoc As String
    End Structure

    Public Shared Function GetExistingTrtsSingleTooth(patientId As Integer, toothNum As Byte) As HashSet(Of Integer)
        Dim existingTrts As New HashSet(Of Integer)()
        Dim appliedTrts As List(Of Patient_ToothTrt) = Patient_ToothTrtDATA.SelectByPatientAndTooth(patientId, toothNum)
        For Each pt In appliedTrts
            Dim trt As TblTRTS = TblTRTSDATA.SelectByTrtName(pt.Treat)
            If trt IsNot Nothing Then
                existingTrts.Add(trt.TrtID)
            End If
        Next
        Return existingTrts
    End Function

    ''' <summary>Same as GetExistingTrtsSingleTooth but for diagnosis tree: uses Patient_Diagnosis so "already applied" highlights diagnoses.</summary>
    Public Shared Function GetExistingDiagSingleTooth(patientId As Integer, toothNum As Byte) As HashSet(Of Integer)
        Dim existingTrts As New HashSet(Of Integer)()
        Dim appliedDiag As List(Of Patient_Diagnosis) = Patient_DiagnosisDATA.SelectByPatientAndTooth(patientId, toothNum)
        For Each pt In appliedDiag
            Dim trt As TblTRTS = TblTRTSDATA.SelectByTrtName(pt.Treat)
            If trt IsNot Nothing Then
                existingTrts.Add(trt.TrtID)
            End If
        Next
        Return existingTrts
    End Function

    ''' <summary>True if any selected FDI tooth already has an applied treatment whose catalog <c>TrtGroup</c> is DENTURES.</summary>
    Public Shared Function PatientHasDentureOnAnyTooth(patientId As Integer, fdiTeeth As IEnumerable(Of Integer)) As Boolean
        If fdiTeeth Is Nothing Then Return False
        For Each toothNum In fdiTeeth.Distinct()
            Dim appliedTrts As List(Of Patient_ToothTrt) = Patient_ToothTrtDATA.SelectByPatientAndTooth(patientId, toothNum)
            For Each pt In appliedTrts
                Dim trt As TblTRTS = TblTRTSDATA.SelectByTrtName(pt.Treat)
                If trt IsNot Nothing AndAlso String.Equals(trt.TrtGroup, "DENTURES", StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    ''' <summary>Empty tree when a selected tooth already has a denture (no further treatments or diagnoses on that tooth).</summary>
    Public Shared Sub BuildTreeEmptyBlockedDentureTooth(
        treeView As TreeView,
        useDiagnosis As Boolean,
        multiSelect As Boolean,
        ByRef allTrtNodes As List(Of TrtNodeInfo),
        ByRef fullTreeSnapshot As List(Of TreeNode))

        allTrtNodes = New List(Of TrtNodeInfo)()
        Dim msg As String
        If useDiagnosis Then
            msg = If(multiSelect, "No diagnoses: a selected tooth has a denture.", "No diagnoses: this tooth has a denture.")
        Else
            msg = If(multiSelect, "No treatments available: a selected tooth has a denture.", "No treatments available: this tooth has a denture.")
        End If
        If useDiagnosis Then
            BuildTreeFromList(treeView, allTrtNodes, Nothing, msg, "DIAGNOSES", Color.DarkRed)
        Else
            BuildTreeFromList(treeView, allTrtNodes, Nothing, msg)
        End If
        fullTreeSnapshot = SaveFullTreeSnapshot(treeView)
    End Sub

    Public Shared Function GetFilteredTrtsSingleTooth(toothID As String, isKid As Boolean, currentToothLevel As Integer, Optional excludeDiagGroup As Boolean = True) As List(Of TrtNodeInfo)
        Dim allTrts As List(Of TblTRTS) = TblTRTSDATA.SelectAll()
        Dim allShapes As List(Of Shapes) = ShapesDATA.SelectAll()

        Dim q = From t In allTrts
                Group Join s In allShapes On t.ShapeID Equals s.ShapeID Into sj = Group
                From s In sj.DefaultIfEmpty()
                Where (t.ToothID = "ALL" OrElse
                       toothID = "ALL" OrElse
                       ("," & t.ToothID & ",").Contains("," & toothID & ",") OrElse
                       ("," & toothID & ",").Contains("," & t.ToothID & ",")) AndAlso
                      t.TrtGroup IsNot Nothing AndAlso (Not excludeDiagGroup OrElse t.TrtGroup <> "DIAG")
                Select New With {
                    t.TrtID,
                    t.Trt,
                    t.TrtValue,
                    t.TrtLoc,
                    t.TrtColor,
                    t.TrtBrdrClr,
                    t.TrtBrdrThick,
                    t.ShapeID,
                    .ShapeName = If(s Is Nothing, Nothing, s.ShapeName),
                    t.TrtDetails,
                    t.TrtLVL,
                    t.ToothID,
                    t.OldTrt,
                    t.TrtGroup,
                    t.ParentGroup,
                    t.KidTrt
                }

        If isKid Then
            q = q.Where(Function(r) (r.KidTrt = 1 OrElse r.KidTrt = 2) AndAlso r.Trt <> "FISSURE SEALENT")
        Else
            q = q.Where(Function(r) (r.KidTrt = 0 OrElse r.KidTrt = 2) AndAlso r.Trt <> "PRR")
        End If

        Select Case currentToothLevel
            Case -1, 0, 1, 2, 3
                q = q.Where(Function(r) (r.TrtGroup <> "IMPLANT" AndAlso
                                r.TrtGroup <> "CROWNS ON IMPLANT" AndAlso
                                r.TrtGroup <> "IMPLANT COMPONENT" AndAlso
                                r.Trt <> "IMPLANT" OrElse
                                r.Trt = "EXTRACTION + IMPLANT") OrElse r.TrtGroup = "DENTURES")
            Case 4
                q = q.Where(Function(r) (r.TrtLVL > 4 AndAlso
                                r.TrtGroup <> "CROWNS ON TOOTH" AndAlso
                                r.TrtGroup <> "INDIRECT VENEERS" AndAlso
                                r.TrtGroup <> "DIRECT VENEERS" AndAlso
                                r.TrtGroup <> "CROWNS ON IMPLANT" AndAlso
                                r.Trt <> "EXTRACTION + IMPLANT" AndAlso
                                r.Trt <> "ABUTMENT" AndAlso
                                r.Trt <> "HEALING CAP") OrElse r.TrtGroup = "DENTURES")
            Case 5
                q = q.Where(Function(r) ((r.TrtGroup = "EXTRACTION" OrElse r.Trt = "EXTRACTION + IMPLANT" OrElse r.TrtGroup = "BRIDGE" OrElse r.TrtLVL > currentToothLevel) AndAlso
                                r.TrtGroup <> "CROWNS ON TOOTH" AndAlso
                                r.TrtGroup <> "INDIRECT VENEERS" AndAlso
                                r.TrtGroup <> "DIRECT VENEERS" AndAlso
                                r.TrtGroup <> "CROWNS ON IMPLANT" AndAlso
                                r.Trt <> "IMPLANT") OrElse r.TrtGroup = "DENTURES")
            Case 6
                q = q.Where(Function(r) ((r.TrtGroup = "EXTRACTION" OrElse r.Trt = "EXTRACTION + IMPLANT" OrElse r.TrtGroup = "BRIDGE" OrElse r.TrtLVL >= currentToothLevel) AndAlso
                                r.TrtGroup <> "CROWNS ON TOOTH" AndAlso
                                r.TrtGroup <> "INDIRECT VENEERS" AndAlso
                                r.TrtGroup <> "DIRECT VENEERS" AndAlso
                                r.TrtGroup <> "IMPLANT" AndAlso
                                r.Trt <> "IMPLANT") OrElse r.TrtGroup = "DENTURES")
            Case 7
                q = q.Where(Function(r) ((r.TrtGroup = "EXTRACTION" OrElse r.Trt = "EXTRACTION + IMPLANT" OrElse r.TrtGroup = "BRIDGE" OrElse r.TrtLVL = currentToothLevel) AndAlso
                                r.TrtGroup <> "CROWNS ON TOOTH" AndAlso
                                r.TrtGroup <> "IMPLANT" AndAlso
                                r.TrtGroup <> "INDIRECT VENEERS" AndAlso
                                r.TrtGroup <> "DIRECT VENEERS" AndAlso
                                r.TrtGroup <> "CROWNS ON IMPLANT" AndAlso
                                r.Trt <> "IMPLANT") OrElse r.TrtGroup = "DENTURES")
            Case 8
                ' Bridge level: allow EXTRACTION, EXTRACTION+IMPLANT, TrtLVL=8, or bridge-not-extracted list (Abscess drainage, Apexification, etc.)
                q = q.Where(Function(r) (((r.TrtGroup = "EXTRACTION" OrElse r.Trt = "EXTRACTION + IMPLANT" OrElse r.TrtLVL = currentToothLevel) OrElse
                                (r.Trt IsNot Nothing AndAlso BridgeNotExtractedAllowedTrts.Contains(r.Trt.Trim().ToUpperInvariant()))) AndAlso
                                r.TrtGroup <> "CROWNS ON TOOTH" AndAlso
                                r.TrtGroup <> "IMPLANT" AndAlso
                                r.TrtGroup <> "INDIRECT VENEERS" AndAlso
                                r.TrtGroup <> "DIRECT VENEERS" AndAlso
                                r.TrtGroup <> "CROWNS ON IMPLANT" AndAlso
                                r.Trt <> "IMPLANT") OrElse r.TrtGroup = "DENTURES")
        End Select

        q = q.OrderBy(Function(r) r.ParentGroup).
            ThenBy(Function(r) r.TrtGroup).
            ThenBy(Function(r) r.Trt)

        Dim results As New List(Of TrtNodeInfo)()
        For Each r In q.ToList()
            Dim brdThick As Byte = 0
            Try
                brdThick = Convert.ToByte(r.TrtBrdrThick)
            Catch
            End Try

            Dim shapeId As Integer = 0
            Try
                shapeId = Convert.ToInt32(r.ShapeID)
            Catch
            End Try

            results.Add(New TrtNodeInfo With {
                .TrtID = r.TrtID,
                .Trt = r.Trt,
                .TrtValue = If(r.TrtValue Is Nothing, 0D, r.TrtValue),
                .ParentGroup = r.ParentGroup,
                .TrtGroup = r.TrtGroup,
                .TrtColor = r.TrtColor,
                .TrtLoc = r.TrtLoc,
                .TrtBrdrClr = r.TrtBrdrClr,
                .ShapeName = r.ShapeName,
                .ShapeID = shapeId,
                .TrtBrdrThick = brdThick,
                .OldTrt = r.OldTrt
            })
        Next

        Return results
    End Function

    Public Shared Function BuildTrtNodesFromDataTable(dataTable As DataTable) As List(Of TrtNodeInfo)
        Dim nodes As New List(Of TrtNodeInfo)()
        If dataTable Is Nothing Then Return nodes

        For Each row As DataRow In dataTable.Rows
            Dim shapeId As Integer = 0
            If Not IsDBNull(row("ShapeID")) Then
                Integer.TryParse(row("ShapeID").ToString(), shapeId)
            End If
            '                .TrtValue = row.Field(Of Decimal?)("TrtValue").GetValueOrDefault(0D),
            nodes.Add(New TrtNodeInfo With {
                .TrtID = Convert.ToInt32(row("TrtID")),
                .Trt = row("Trt").ToString(),
                .ParentGroup = row("ParentGroup").ToString(),
                .TrtGroup = row("TrtGroup").ToString(),
                .TrtColor = row.Field(Of String)("TrtColor"),
                .TrtBrdrClr = row.Field(Of String)("TrtBrdrClr"),
                .ShapeName = row.Field(Of String)("ShapeName"),
                .ShapeID = shapeId,
                .TrtBrdrThick = row.Field(Of Byte?)("TrtBrdrThick").GetValueOrDefault(0),
                .TrtLoc = row.Field(Of String)("TrtLoc")
            })
        Next

        Return nodes
    End Function

    Public Shared Sub BuildTreeFromList(treeView As TreeView, nodeList As List(Of TrtNodeInfo), Optional existingTrts As HashSet(Of Integer) = Nothing, Optional emptyMessage As String = "No available treatments for current tooth status", Optional rootNodeText As String = "TREATS", Optional rootNodeForeColor As Color = Nothing)
        If treeView Is Nothing Then Return

        Dim rootColor As Color = If(rootNodeForeColor = Nothing, Color.DarkGreen, rootNodeForeColor)
        Dim treatsRoot As TreeNode = Nothing
        treeView.BeginUpdate()
        Try
            treeView.Nodes.Clear()
            Dim groupNodes As New Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)
            Dim parentNodes As New Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)

            treatsRoot = New TreeNode(rootNodeText) With {
                .Name = rootNodeText,
                .ForeColor = rootColor,
                .Tag = "ROOT_PARENT",
                .NodeFont = New Font(treeView.Font, FontStyle.Bold)
            }

            Dim safeList As List(Of TrtNodeInfo) = If(nodeList, New List(Of TrtNodeInfo)())
            For Each Inf In safeList
                Dim grp = If(Inf.TrtGroup, "").ToUpperInvariant()
                Dim parent = If(Inf.ParentGroup, "")

                If parent.ToUpperInvariant() = "SURGERY" AndAlso grp = "SURGICAL,IMMEDIATE" Then
                    Dim subGroups As New List(Of String)
                    If grp.Contains("IMMEDIATE") Or Inf.Trt.ToUpperInvariant().Contains("IMMEDIATE") Then
                        subGroups.Add("IMMEDIATE")
                    End If
                    If grp.Contains("SURGICAL") Or Inf.Trt.ToUpperInvariant().Contains("SURGICAL") OrElse Not subGroups.Any() Then
                        subGroups.Add("SURGICAL")
                    End If

                    If Not parentNodes.ContainsKey(parent) Then
                        Dim pnode As New TreeNode(parent) With {
                            .Name = parent,
                            .ForeColor = Color.DarkBlue,
                            .Tag = "PARENT_GROUP"
                        }
                        parentNodes.Add(parent, pnode)
                        treatsRoot.Nodes.Add(pnode)
                    End If

                    Dim surgeryImplantKey = parent & "|IMPLANT"
                    If Not groupNodes.ContainsKey(surgeryImplantKey) Then
                        Dim implantNode As New TreeNode("IMPLANT") With {
                            .Name = "IMPLANT",
                            .ForeColor = Color.Blue,
                            .Tag = "GROUP"
                        }
                        groupNodes.Add(surgeryImplantKey, implantNode)
                        parentNodes(parent).Nodes.Add(implantNode)
                    End If

                    Dim tnode As New TreeNode(Inf.Trt) With {
                        .ForeColor = Color.Green,
                        .Tag = Inf
                    }
                    If existingTrts IsNot Nothing AndAlso existingTrts.Contains(Inf.TrtID) Then
                        tnode.ForeColor = Color.Red
                        tnode.NodeFont = New Font(treeView.Font, FontStyle.Strikeout)
                    End If

                    For Each subGroup In subGroups
                        Dim subGroupKey = parent & "|IMPLANT|" & subGroup
                        If Not groupNodes.ContainsKey(subGroupKey) Then
                            Dim gnode As New TreeNode(subGroup) With {
                                .Name = subGroup,
                                .ForeColor = Color.Blue,
                                .Tag = "GROUP"
                            }
                            groupNodes.Add(subGroupKey, gnode)
                            groupNodes(surgeryImplantKey).Nodes.Add(gnode)
                        End If
                        Dim clonedTnode As New TreeNode(Inf.Trt) With {
                            .ForeColor = tnode.ForeColor,
                            .NodeFont = tnode.NodeFont,
                            .Tag = Inf
                        }
                        groupNodes(subGroupKey).Nodes.Add(clonedTnode)
                    Next
                    Continue For
                End If

                If parent.ToUpperInvariant() = "IMPLANT" Then
                    Dim subGroups As New List(Of String)
                    If grp.Contains("IMMEDIATE") Or Inf.Trt.ToUpperInvariant().Contains("IMMEDIATE") Then
                        subGroups.Add("IMMEDIATE")
                    End If
                    If grp.Contains("SURGICAL") Or Inf.Trt.ToUpperInvariant().Contains("SURGICAL") Or Not subGroups.Any() Then
                        subGroups.Add("SURGICAL")
                    End If

                    If Not parentNodes.ContainsKey(parent) Then
                        Dim pnode As New TreeNode(parent) With {
                            .Name = parent,
                            .ForeColor = Color.DarkBlue,
                            .Tag = "PARENT_GROUP"
                        }
                        parentNodes.Add(parent, pnode)
                        treatsRoot.Nodes.Add(pnode)
                    End If

                    Dim tnode As New TreeNode(Inf.Trt) With {
                        .ForeColor = Color.Green,
                        .Tag = Inf
                    }
                    If existingTrts IsNot Nothing AndAlso existingTrts.Contains(Inf.TrtID) Then
                        tnode.ForeColor = Color.Red
                        tnode.NodeFont = New Font(treeView.Font, FontStyle.Strikeout)
                    End If

                    For Each subGroup In subGroups
                        Dim subGroupKey = parent & "|" & subGroup
                        If Not groupNodes.ContainsKey(subGroupKey) Then
                            Dim gnode As New TreeNode(subGroup) With {
                                .Name = subGroup,
                                .ForeColor = Color.Blue,
                                .Tag = "GROUP"
                            }
                            groupNodes.Add(subGroupKey, gnode)
                            parentNodes(parent).Nodes.Add(gnode)
                        End If
                        Dim clonedTnode As New TreeNode(Inf.Trt) With {
                            .ForeColor = tnode.ForeColor,
                            .NodeFont = tnode.NodeFont,
                            .Tag = Inf
                        }
                        groupNodes(subGroupKey).Nodes.Add(clonedTnode)
                    Next
                Else
                    If Not String.IsNullOrEmpty(parent) Then
                        If Not parentNodes.ContainsKey(parent) Then
                            Dim pnode As New TreeNode(parent) With {
                                .Name = parent,
                                .ForeColor = Color.DarkBlue,
                                .Tag = "PARENT_GROUP"
                            }
                            parentNodes.Add(parent, pnode)
                            treatsRoot.Nodes.Add(pnode)
                        End If
                    End If

                    If Not groupNodes.ContainsKey(grp) Then
                        Dim gnode As New TreeNode(grp) With {
                            .Name = grp,
                            .ForeColor = Color.Blue,
                            .Tag = "GROUP"
                        }
                        groupNodes.Add(grp, gnode)
                    End If

                    Dim tnode As New TreeNode(Inf.Trt) With {
                        .ForeColor = Color.Green,
                        .Tag = Inf
                    }
                    If existingTrts IsNot Nothing AndAlso existingTrts.Contains(Inf.TrtID) Then
                        tnode.ForeColor = Color.Red
                        tnode.NodeFont = New Font(treeView.Font, FontStyle.Strikeout)
                    End If

                    If Not String.IsNullOrEmpty(parent) Then
                        Dim pnode = parentNodes(parent)
                        If Not pnode.Nodes.ContainsKey(grp) Then
                            pnode.Nodes.Add(groupNodes(grp))
                        End If
                        groupNodes(grp).Nodes.Add(tnode)
                    Else
                        If groupNodes(grp).Parent Is Nothing Then
                            treatsRoot.Nodes.Add(groupNodes(grp))
                        End If
                        groupNodes(grp).Nodes.Add(tnode)
                    End If
                End If
            Next

            treeView.Nodes.Add(treatsRoot)
            For Each kvp In groupNodes
                If kvp.Value.Parent Is Nothing Then
                    treatsRoot.Nodes.Add(kvp.Value)
                End If
            Next

            If treatsRoot.Nodes.Count = 0 AndAlso Not String.IsNullOrWhiteSpace(emptyMessage) Then
                treatsRoot.Nodes.Add(New TreeNode(emptyMessage) With {
                    .ForeColor = Color.Gray,
                    .Tag = "INFO_NODE"
                })
            End If
        Finally
            treeView.EndUpdate()
        End Try

        ' Expand root after EndUpdate (BeginUpdate suppresses paint; RTL often needs a second pass once mirrored layout is applied).
        If treatsRoot IsNot Nothing Then
            treatsRoot.Expand()
            If treeView.IsMirrored OrElse treeView.RightToLeft = RightToLeft.Yes Then
                Dim r = treatsRoot
                treeView.BeginInvoke(New MethodInvoker(Sub()
                    If r.TreeView IsNot Nothing Then r.Expand()
                End Sub))
            End If
        End If
    End Sub

    Public Shared Function GetTrtByToothIds(ByVal query As IQueryable(Of TblTRTS), ByVal toothNumbers As String) As IQueryable(Of TblTRTS)
        Dim numbers = toothNumbers.Split({","}, StringSplitOptions.RemoveEmptyEntries).Select(Function(s) s.Trim()).ToList()
        Dim group1 = New List(Of String) From {"1", "2", "3"}
        Dim group2 = New List(Of String) From {"4", "5", "6", "7", "8"}

        Dim hasGroup1 As Boolean = numbers.All(Function(n) group1.Contains(n)) AndAlso numbers.Any()
        Dim hasGroup2 As Boolean = numbers.All(Function(n) group2.Contains(n)) AndAlso numbers.Any()
        Dim mixedGroups As Boolean = numbers.Any(Function(n) group1.Contains(n)) AndAlso numbers.Any(Function(n) group2.Contains(n))

        If mixedGroups Then
            query = query.Where(Function(t) t.ToothID = "ALL")
        ElseIf hasGroup1 Then
            query = query.Where(Function(t) t.ToothID = "ALL" OrElse group1.Any(Function(num) ("," & t.ToothID & ",").Contains("," & num & ",")))
        ElseIf hasGroup2 Then
            query = query.Where(Function(t) t.ToothID = "ALL" OrElse group2.Any(Function(num) ("," & t.ToothID & ",").Contains("," & num & ",")))
        Else
            query = query.Where(Function(t) t.ToothID = "ALL")
        End If

        Return query
    End Function

    Public Shared Function GetApplicableTreatments(selectedTeeth As List(Of Integer), allTreatments As List(Of TblTRTS)) As List(Of TblTRTS)
        Dim result As IEnumerable(Of TblTRTS) = allTreatments
        For Each tooth In selectedTeeth
            result = result.Where(Function(t) _
                t.ToothID = "ALL" OrElse
                t.ToothID.Split(","c).Contains(tooth.ToString()))
        Next
        Return result.ToList()
    End Function

    Public Shared Function SaveFullTreeSnapshot(treeView As TreeView) As List(Of TreeNode)
        Dim snapshot As New List(Of TreeNode)()
        If treeView Is Nothing Then Return snapshot
        For Each node As TreeNode In treeView.Nodes
            snapshot.Add(CloneNode(node))
        Next
        Return snapshot
    End Function

    Public Shared Sub ApplyTreeFilter(treeView As TreeView, fullTreeSnapshot As List(Of TreeNode), filterText As String)
        If treeView Is Nothing Then Return

        treeView.BeginUpdate()
        treeView.Nodes.Clear()

        Dim safeSnapshot As List(Of TreeNode) = If(fullTreeSnapshot, New List(Of TreeNode)())
        If String.IsNullOrWhiteSpace(filterText) Then
            For Each node In safeSnapshot
                treeView.Nodes.Add(CloneNode(node))
            Next
        Else
            Dim filteredNodes As New List(Of TreeNode)
            For Each node In safeSnapshot
                Dim filteredNode = FilterNode(node, filterText.ToLowerInvariant())
                If filteredNode IsNot Nothing Then
                    filteredNodes.Add(filteredNode)
                End If
            Next
            For Each node In filteredNodes
                treeView.Nodes.Add(node)
            Next
        End If

        treeView.EndUpdate()
    End Sub

    Public Shared Sub FilterTrtsTreeView(treeView As TreeView, searchText As String, Optional forKidTreatments As Boolean = False)
        If treeView Is Nothing Then Return

        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim kidClause = If(forKidTreatments, "([KidTrt] IN (1,  2)) ", "([KidTrt] IN (0,  2)) ")
        Dim query As String = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], " &
                    "[TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails], " &
                    "[TblTRTS].[TrtLVL], [TblTRTS].[TrtValue], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt], " &
                    "[TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] " &
                    "FROM [TblTRTS] LEFT JOIN [Shapes] ON [TblTRTS].[ShapeID] = [Shapes].[ShapeID] " &
                    "WHERE " & kidClause &
                    "AND ([ToothID] = 'ALL' OR @ToothID = 'ALL' " &
                    "OR CHARINDEX(',' + @ToothID + ',', ',' + [ToothID] + ',') > 0 " &
                    "OR CHARINDEX(',' + [ToothID] + ',', ',' + @ToothID + ',') > 0) " &
                    "AND [TrtGroup] IS NOT NULL"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", "ALL")
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            treeView.Nodes.Clear()

            Dim groups = dataTable.AsEnumerable().
                Where(Function(row) row.Field(Of String)("Trt").ToLower().Contains(searchText)).
                GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            For Each group In groups
                Dim groupNode As New TreeNode(group.Key)
                For Each treatment In group
                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                        .Tag = treatment.Field(Of Integer)("TrtID")
                    }
                    groupNode.Nodes.Add(treatmentNode)
                Next
                treeView.Nodes.Add(groupNode)
            Next
        End Using

        For Each groupNode As TreeNode In treeView.Nodes
            Dim groupVisible As Boolean = False
            For Each treatmentNode As TreeNode In groupNode.Nodes
                If treatmentNode.Text.ToLower().Contains(searchText.ToLower()) Then
                    treatmentNode.ForeColor = Color.Red
                    treatmentNode.BackColor = Color.LightYellow
                    treatmentNode.EnsureVisible()
                    groupVisible = True
                Else
                    treatmentNode.ForeColor = Color.Green
                    treatmentNode.BackColor = Color.Transparent
                End If
            Next

            If groupVisible Then
                groupNode.ForeColor = Color.Blue
                groupNode.BackColor = Color.Transparent
                groupNode.Expand()
            Else
                groupNode.ForeColor = Color.Transparent
                groupNode.BackColor = Color.Transparent
            End If
        Next
    End Sub

    Public Shared Sub ClearAddedTrtsListBound(addedTrtsList As ListBoxControl, ByRef originalAddedTrtsTable As DataTable)
        If addedTrtsList Is Nothing Then Return
        addedTrtsList.DataSource = Nothing
        addedTrtsList.DisplayMember = Nothing
        addedTrtsList.ValueMember = Nothing
        originalAddedTrtsTable = Nothing
    End Sub

    Public Shared Sub FilterAddedTrtsListBox(addedTrtsList As ListBoxControl, ByRef originalAddedTrtsTable As DataTable, searchText As String)
        If addedTrtsList Is Nothing Then Return

        If originalAddedTrtsTable Is Nothing Then
            If addedTrtsList.DataSource IsNot Nothing Then
                originalAddedTrtsTable = DirectCast(addedTrtsList.DataSource, DataTable).Copy()
            End If
        End If

        If originalAddedTrtsTable IsNot Nothing Then
            Dim filteredRows = originalAddedTrtsTable.AsEnumerable().Where(Function(row) _
                Not IsDBNull(row("DisplayTreat")) AndAlso row.Field(Of String)("DisplayTreat").ToLower().Contains(searchText))

            If searchText = "" Then
                addedTrtsList.DataSource = originalAddedTrtsTable.Copy()
            ElseIf filteredRows.Any() Then
                addedTrtsList.DataSource = filteredRows.CopyToDataTable()
            Else
                addedTrtsList.DataSource = originalAddedTrtsTable.Clone()
            End If
        End If
    End Sub

    Public Shared Sub SetTrtsTreeMultiTeeth(treeView As TreeView, selectedTeethList As IEnumerable(Of Integer), patientId As Integer, isKid As Boolean, ByRef allTrtNodes As List(Of TrtNodeInfo), ByRef fullTreeSnapshot As List(Of TreeNode), Optional useDiagnosis As Boolean = False)
        If treeView Is Nothing Then Return
        Dim clsToothTrtData As New Patient_ToothTrtDATA
        Dim teeth = If(selectedTeethList, Enumerable.Empty(Of Integer)()).ToList()
        Try
            treeView.Nodes.Clear()

            If PatientHasDentureOnAnyTooth(patientId, teeth) Then
                BuildTreeEmptyBlockedDentureTooth(treeView, useDiagnosis, multiSelect:=True, allTrtNodes, fullTreeSnapshot)
                Return
            End If

            Dim hasUpperTeeth As Boolean = teeth.Any(Function(t) t >= 11 AndAlso t <= 28)
            Dim hasLowerTeeth As Boolean = teeth.Any(Function(t) t >= 31 AndAlso t <= 48)
            Dim isMixedSelection As Boolean = hasUpperTeeth AndAlso hasLowerTeeth

            Dim maxToothLevel As Integer = -1
            Dim allLevels As New List(Of Integer)

            For Each ToothNum In teeth
                Dim lvls = clsToothTrtData.GetTreatLVLs(patientId, CByte(ToothNum))
                For Each lvl In lvls
                    If Not allLevels.Contains(lvl) Then allLevels.Add(lvl)
                Next
            Next

            Dim overallToothLevel As Integer = -1
            If allLevels.Any(Function(l) l > 7) Then
                overallToothLevel = 8
            ElseIf allLevels.Any(Function(l) l > 5 AndAlso l < 8) Then
                overallToothLevel = 6
            ElseIf allLevels.Any(Function(l) l = 5) Then
                overallToothLevel = 5
            ElseIf allLevels.Any(Function(l) l = 4) Then
                overallToothLevel = 4
            ElseIf allLevels.Any(Function(l) l <= 3 AndAlso l >= 0) Then
                overallToothLevel = -1
            End If

            Dim toothIDParam As String = If(teeth.Count = 1,
                       (teeth(0) Mod 10).ToString(),
                       String.Join(",", teeth.Select(Function(t) t Mod 10)))

            Dim query As String = ""
            query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtValue], [TblTRTS].[TrtLoc], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr],  
    [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],  
    [TblTRTS].[TrtLVL], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],  
    [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt]  
    FROM [TblTRTS] LEFT JOIN [Shapes] ON [TblTRTS].[ShapeID] = [Shapes].[ShapeID]  
    WHERE  [TrtGroup] Is Not NULL " & If(useDiagnosis, "", "  AND [TrtGroup] <> 'DIAG' ") & "
        And (
        [ToothID] = 'ALL' 
        Or (
        (
            EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE ',' + [TblTRTS].[ToothID] + ',' LIKE '%,' + value + ',%')
            Or EXISTS(SELECT value FROM STRING_SPLIT([TblTRTS].[ToothID], ',') WHERE ',' + @ToothID + ',' LIKE '%,' + value + ',%')
)
        And (
            (
                EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('1','2','3'))
                And EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('4','5','6','7','8'))
                And [TrtGroup] In ('CLASS 1', 'CLASS 5')
            )
            Or
        (
            Not EXISTS (SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('1','2','3'))
            And EXISTS (SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('4','5','6','7','8'))
            And [TrtGroup] IN ('CLASS 1', 'CLASS 2', 'CLASS 5')
        )
        Or
        (
            EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('1','2','3'))
            And Not EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('4','5','6','7','8'))
            And [TrtGroup] In ('CLASS 1', 'CLASS 3', 'CLASS 4', 'CLASS 5')
        )
                )
            )
        ) "

            If isKid Then
                query += " AND ([KidTrt] IN (1,  2)) AND [Trt] <> 'FISSURE SEALENT' "
            Else
                query += " AND ([KidTrt] IN (0,  2)) AND [Trt] <> 'PRR' "
            End If

            Dim hasMinusOne As Boolean = allLevels.Any(Function(l) l <= 3)
            Dim hasLevel4 As Boolean = allLevels.Contains(4)
            Dim hasLevel5 As Boolean = allLevels.Contains(5)
            Dim hasLevel6Plus As Boolean = allLevels.Any(Function(l) l >= 6)
            Dim hasLevel8 As Boolean = allLevels.Contains(8)
            Dim bridgeNotExtracted As Boolean = hasLevel8 AndAlso Not hasLevel4

            If hasMinusOne AndAlso hasLevel4 AndAlso (hasLevel5 Or hasLevel6Plus) Then
                query += " AND ([TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
            ElseIf hasMinusOne AndAlso hasLevel4 AndAlso Not (hasLevel5 Or hasLevel6Plus) Then
                query += " AND ([TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
            ElseIf hasMinusOne AndAlso (hasLevel5 Or hasLevel6Plus) AndAlso Not hasLevel4 Then
                query += " AND (([Trt] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT') OR [TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
            ElseIf hasLevel4 AndAlso (hasLevel5 Or hasLevel6Plus) AndAlso Not hasMinusOne Then
                query += " AND ([TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'DENTURES')"
            ElseIf hasMinusOne AndAlso Not hasLevel4 AndAlso Not hasLevel5 AndAlso Not hasLevel6Plus Then
                query += " AND (([TrtGroup] <> 'IMPLANT' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [TrtGroup] <> 'IMPLANT COMPONENT' AND [Trt] <> 'IMPLANT') OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'DENTURES')"
            ElseIf hasLevel4 AndAlso Not hasMinusOne AndAlso Not hasLevel5 AndAlso Not hasLevel6Plus Then
                query += " AND (([TrtLVL] > 4 AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [Trt] <> 'EXTRACTION + IMPLANT' AND [Trt] <> 'ABUTMENT' AND [Trt] <> 'HEALING CAP') OR [TrtGroup] = 'DENTURES')"
            ElseIf hasLevel5 AndAlso Not hasMinusOne AndAlso Not hasLevel4 AndAlso Not hasLevel6Plus Then
                query += " AND ((([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR 
                            [TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'IMPLANT COMPONENT' OR [TrtLVL] = 5) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND 
                            [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT' OR [TrtGroup] = 'DENTURES')"
            ElseIf hasLevel6Plus AndAlso Not hasMinusOne AndAlso Not hasLevel4 AndAlso Not hasLevel5 Then
                If bridgeNotExtracted Then
                    query += " AND ((((([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 6) OR [Trt] IN ('ABSCESS DRAINAGE', 'APEXIFICATION', 'APICECTOMY', 'EXTRACTION', 'RCM', 'RCC', 'RCO', 'REDO RCT', 'TF', 'RCC TF', 'RCO TF', 'RCT (ELSE WHERE)', 'REDO (ELSE WHERE-NERVE)', 'RCF GI', 'REDO AMALGAM', 'REDO COMPOSITE', 'REDO GI', 'REDO (IN CLINIC)', 'RCM TF')) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT') OR [TrtGroup] = 'DENTURES')"
                Else
                    query += " AND (((([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 6) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT') OR [TrtGroup] = 'DENTURES')"
                End If
            ElseIf hasLevel5 AndAlso hasLevel6Plus AndAlso Not hasMinusOne AndAlso Not hasLevel4 Then
                If bridgeNotExtracted Then
                    query += " AND ((((([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 5) OR [Trt] IN ('ABSCESS DRAINAGE', 'APEXIFICATION', 'APICECTOMY', 'EXTRACTION', 'RCM', 'RCC', 'RCO', 'REDO RCT', 'TF', 'RCC TF', 'RCO TF', 'RCT (ELSE WHERE)', 'REDO (ELSE WHERE-NERVE)', 'RCF GI', 'REDO AMALGAM', 'REDO COMPOSITE', 'REDO GI', 'REDO (IN CLINIC)', 'RCM TF')) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT') OR [TrtGroup] = 'DENTURES')"
                Else
                    query += " AND (((([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 5) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT') OR [TrtGroup] = 'DENTURES')"
                End If
            End If

            If teeth.Count > 1 AndAlso isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            End If

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim connection As New SqlConnection(connectionString)

            Dim toothNums As New List(Of Integer)
            toothNums = teeth

            Dim existingTrts As New HashSet(Of Integer)

            Dim toothParams As New List(Of String)
            For i As Integer = 0 To toothNums.Count - 1
                toothParams.Add("@ToothNum" & i)
            Next
            Dim inClause As String = String.Join(", ", toothParams)

            Dim sql As String
            If useDiagnosis Then
                sql = "SELECT dbo.TblTRTS.TrtID FROM dbo.Patient_Diagnosis INNER JOIN dbo.TblTRTS ON dbo.Patient_Diagnosis.Treat = dbo.TblTRTS.Trt WHERE PatientID = @PatientID AND ToothNum IN (" & inClause & ")"
            Else
                sql = "SELECT TrtID FROM dbo.Patient_ToothTrt INNER JOIN dbo.TblTRTS ON dbo.Patient_ToothTrt.Treat = dbo.TblTRTS.Trt WHERE PatientID = @PatientID AND ToothNum IN (" & inClause & ")"
            End If

            Using checkCmd As New SqlCommand(sql, connection)
                checkCmd.Parameters.AddWithValue("@PatientID", patientId)
                For i As Integer = 0 To toothNums.Count - 1
                    checkCmd.Parameters.AddWithValue("@ToothNum" & i, toothNums(i))
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

            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", toothIDParam)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            JawTreatmentTreeHelper.FilterCompleteDentureTreeRowsUnlessValidFullArch(dataTable, teeth, isKid)

            allTrtNodes = BuildTrtNodesFromDataTable(dataTable)
            If Not useDiagnosis Then
                allTrtNodes = ApplyToothSelectionOtherTreatAllowlist(allTrtNodes)
            End If
            If useDiagnosis Then
                BuildTreeFromList(treeView, allTrtNodes, existingTrts, "No available diagnoses for the selected teeth.", "DIAGNOSES", Color.DarkRed)
            Else
                BuildTreeFromList(treeView, allTrtNodes, existingTrts, "No available treatments for the selected teeth.")
            End If
            fullTreeSnapshot = SaveFullTreeSnapshot(treeView)
        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub SetMobTreeMultiTeeth(treeView As TreeView, selectedTeethList As IEnumerable(Of Integer), patientId As Integer, isKid As Boolean, ByRef allTrtNodes As List(Of TrtNodeInfo), ByRef fullTreeSnapshot As List(Of TreeNode), Optional useDiagnosis As Boolean = False)
        If treeView Is Nothing Then Return
        Dim clsToothTrtData As New Patient_ToothTrtDATA
        Dim teeth = If(selectedTeethList, Enumerable.Empty(Of Integer)()).ToList()
        Try
            treeView.Nodes.Clear()

            If PatientHasDentureOnAnyTooth(patientId, teeth) Then
                BuildTreeEmptyBlockedDentureTooth(treeView, useDiagnosis, multiSelect:=True, allTrtNodes, fullTreeSnapshot)
                Return
            End If

            Dim hasUpperTeeth As Boolean = teeth.Any(Function(t) t >= 11 AndAlso t <= 28)
            Dim hasLowerTeeth As Boolean = teeth.Any(Function(t) t >= 31 AndAlso t <= 48)
            Dim isMixedSelection As Boolean = hasUpperTeeth AndAlso hasLowerTeeth
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

            Dim query As String = ""
            query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], 
            [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],
            [TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],
            [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] 
            From [TblTRTS] LEFT Join [Shapes] On [TblTRTS].[ShapeID] = [Shapes].[ShapeID] 
             WHERE ([TrtGroup] IN ('EXTRACTION', 'IMPLANT', 'DENTURES')) "

            If teeth.Count > 1 AndAlso isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            ElseIf teeth.Count = 16 AndAlso Not isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            End If

            Dim norTrt As Boolean = allLevels.Any(Function(l) l < 4)
            Dim exTrt As Boolean = allLevels.Contains(4)
            Dim impTrt As Boolean = allLevels.Any(Function(l) l >= 5 AndAlso l <= 7)
            Dim brgTrt As Boolean = allLevels.Contains(8)

            Select Case True
                Case Not norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND 1=1"
                Case norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND [TrtGroup] NOT IN ('IMPLANT', 'CROWNS ON IMPLANT','IMPLANT COMPONENT')"
                Case Not norTrt AndAlso exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND [TrtGroup] IN ('IMPLANT', 'BRIDGE')"
                Case norTrt AndAlso exTrt AndAlso Not impTrt AndAlso Not brgTrt
                    query += " AND [TrtGroup] = 'BRIDGE'"
                Case Not norTrt AndAlso Not exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND ([Trt] = 'EXTRACTION' OR [Trt] = 'ABUTMENT' OR [Trt] = 'HEALING CAP' OR [TrtGroup] = 'CROWNS ON IMPLANT' OR [TrtGroup] = 'BRIDGE')"
                Case norTrt AndAlso Not exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND ([Trt] = 'EXTRACTION' OR [TrtGroup] = 'BRIDGE')"
                Case Not norTrt AndAlso exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND [TrtGroup] = 'BRIDGE'"
                Case norTrt AndAlso exTrt AndAlso impTrt AndAlso Not brgTrt
                    query += " AND [TrtGroup] = 'BRIDGE'"
                Case Not norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND [Trt] = 'EXTRACTION'"
                Case norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND [Trt] = 'EXTRACTION'"
                Case Not norTrt AndAlso exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND 1=0"
                Case norTrt AndAlso exTrt AndAlso Not impTrt AndAlso brgTrt
                    query += " AND 1=0"
                Case Not norTrt AndAlso Not exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND [Trt] = 'EXTRACTION'"
                Case norTrt AndAlso Not exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND [Trt] = 'EXTRACTION'"
                Case Not norTrt AndAlso exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND 1=0"
                Case norTrt AndAlso exTrt AndAlso impTrt AndAlso brgTrt
                    query += " AND 1=0"
            End Select

            If isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE'"
            End If

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim connection As New SqlConnection(connectionString)

            Dim toothNums As New List(Of Integer)
            toothNums = teeth

            Dim existingTrts As New HashSet(Of Integer)

            Dim toothParams As New List(Of String)
            For i As Integer = 0 To toothNums.Count - 1
                toothParams.Add("@ToothNum" & i)
            Next
            Dim inClause As String = String.Join(", ", toothParams)

            Dim sql As String
            If useDiagnosis Then
                sql = "SELECT dbo.TblTRTS.TrtID FROM [dbo].[Patient_Diagnosis] INNER JOIN dbo.TblTRTS ON [dbo].[Patient_Diagnosis].Treat = dbo.TblTRTS.Trt WHERE [dbo].[Patient_Diagnosis].LVL=9 AND PatientID = @PatientID AND ToothNum IN (" & inClause & ")"
            Else
                sql = "SELECT dbo.TblTRTS.TrtID FROM [dbo].[Patient_ToothTrt] INNER JOIN dbo.TblTRTS ON [dbo].[Patient_ToothTrt].Treat = dbo.TblTRTS.Trt WHERE [dbo].[Patient_ToothTrt].LVL=9 AND PatientID = @PatientID AND ToothNum IN (" & inClause & ")"
            End If

            Using checkCmd As New SqlCommand(sql, connection)
                checkCmd.Parameters.AddWithValue("@PatientID", patientId)
                For i As Integer = 0 To toothNums.Count - 1
                    checkCmd.Parameters.AddWithValue("@ToothNum" & i, toothNums(i))
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

            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", toothIDParam)
            command.Parameters.AddWithValue("@CurrentToothLevel", maxToothLevel)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            JawTreatmentTreeHelper.FilterCompleteDentureTreeRowsUnlessValidFullArch(dataTable, toothNums, isKid)

            allTrtNodes = BuildTrtNodesFromDataTable(dataTable)
            If Not useDiagnosis Then
                allTrtNodes = ApplyToothSelectionOtherTreatAllowlist(allTrtNodes)
            End If
            If useDiagnosis Then
                BuildTreeFromList(treeView, allTrtNodes, existingTrts, "No available diagnoses for the selected teeth.", "DIAGNOSES", Color.DarkRed)
            Else
                BuildTreeFromList(treeView, allTrtNodes, existingTrts, "No available treatments for the selected teeth.")
            End If
            fullTreeSnapshot = SaveFullTreeSnapshot(treeView)
        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub SetMobTreeSingleTooth(treeView As TreeView, toothID As String, toothNum As Byte, patientId As Integer, isKid As Boolean, ByRef allTrtNodes As List(Of TrtNodeInfo), ByRef fullTreeSnapshot As List(Of TreeNode), Optional useDiagnosis As Boolean = False)
        If treeView Is Nothing Then Return
        Dim clsToothTrtData As New Patient_ToothTrtDATA
        Try
            treeView.Nodes.Clear()
            If PatientHasDentureOnAnyTooth(patientId, New Integer() {CInt(toothNum)}) Then
                BuildTreeEmptyBlockedDentureTooth(treeView, useDiagnosis, multiSelect:=False, allTrtNodes, fullTreeSnapshot)
                Return
            End If
            Dim currentToothLevel As Integer = clsToothTrtData.GetTreatLVL(patientId, toothNum)

            Dim query As String = ""
            query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], 
            [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],
            [TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],
            [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] 
            From [TblTRTS] LEFT Join [Shapes] On [TblTRTS].[ShapeID] = [Shapes].[ShapeID] 
            WHERE ([TrtGroup] IN ('EXTRACTION', 'IMPLANT', 'DENTURES')) "

            If isKid Then
                query += " AND ([KidTrt] IN (1,  2)) "
            Else
                query += " AND ([KidTrt] IN (0,  2)) "
            End If

            Select Case currentToothLevel
                Case -1, 0, 1, 2, 3
                    query += " AND [TrtGroup] NOT IN ('IMPLANT', 'CROWNS ON IMPLANT','IMPLANT COMPONENT')"
                Case 4
                    query += " AND [TrtGroup] IN ('IMPLANT', 'BRIDGE')"
                Case 5
                    query += " AND ([Trt] = 'EXTRACTION' OR [Trt] = 'ABUTMENT' OR [Trt] = 'HEALING CAP' OR [TrtGroup] = 'CROWNS ON IMPLANT' OR [TrtGroup] = 'BRIDGE')"
                Case 6, 7, 8
                    query += " AND [Trt] = 'EXTRACTION'"
            End Select

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim connection As New SqlConnection(connectionString)

            Dim existingTrts As HashSet(Of Integer) = If(useDiagnosis, GetExistingDiagSingleTooth(patientId, toothNum), GetExistingTrtsSingleTooth(patientId, toothNum))

            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", toothID)
            command.Parameters.AddWithValue("@CurrentToothLevel", currentToothLevel)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            allTrtNodes = BuildTrtNodesFromDataTable(dataTable)
            If Not useDiagnosis Then
                allTrtNodes = ApplyToothSelectionOtherTreatAllowlist(allTrtNodes)
            End If
            If useDiagnosis Then
                BuildTreeFromList(treeView, allTrtNodes, existingTrts, "No available diagnoses for current tooth status", "DIAGNOSES", Color.DarkRed)
            Else
                BuildTreeFromList(treeView, allTrtNodes, existingTrts, "No available treatments for current tooth status")
            End If
            fullTreeSnapshot = SaveFullTreeSnapshot(treeView)
        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub

    Private Shared Function CloneNode(node As TreeNode) As TreeNode
        Dim newNode As New TreeNode(node.Text) With {
            .Name = node.Name,
            .Tag = node.Tag,
            .ForeColor = node.ForeColor,
            .BackColor = node.BackColor,
            .NodeFont = node.NodeFont
        }
        For Each child As TreeNode In node.Nodes
            newNode.Nodes.Add(CloneNode(child))
        Next
        Return newNode
    End Function

    Private Shared Function FilterNode(node As TreeNode, filterText As String) As TreeNode
        Dim matchSelf As Boolean = node.Text.ToLowerInvariant().Contains(filterText.ToLowerInvariant())
        Dim matchedChildren As New List(Of TreeNode)

        For Each child As TreeNode In node.Nodes
            Dim filteredChild = FilterNode(child, filterText)
            If filteredChild IsNot Nothing Then
                matchedChildren.Add(filteredChild)
            End If
        Next

        If matchSelf OrElse matchedChildren.Count > 0 Then
            Dim clone As TreeNode = CloneNode(node)
            clone.Nodes.Clear()

            If matchSelf Then
                clone.ForeColor = Color.Red
                clone.BackColor = Color.LightYellow
            Else
                clone.ForeColor = Color.Blue
                clone.BackColor = Color.Transparent
            End If

            For Each c In matchedChildren
                If c.Text.ToLowerInvariant().Contains(filterText.ToLowerInvariant()) Then
                    c.ForeColor = Color.Red
                    c.BackColor = Color.LightYellow
                Else
                    c.ForeColor = Color.Green
                    c.BackColor = Color.Transparent
                End If
                clone.Nodes.Add(c)
            Next
            clone.Expand()

            Return clone
        Else
            Return Nothing
        End If
    End Function
End Class
