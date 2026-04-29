Imports System.Collections.Concurrent
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports Dapper
Imports DentistX
Imports DevExpress.Utils
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Svg
Imports DevExpress.XtraEditors
'Imports DevExpress.Data.Svg

Public Class KidDiag
    Implements IJawControl

Public Event ToothDoubleClick As EventHandler(Of ToothDoubleClickEvent) Implements IToothClickable.ToothDoubleClick

    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        Me.Hide()
        LoadPatientData(patientId)
        Me.Show()
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        LoadPatientTreats(patientId)
    End Sub



#Region "The Class"


    'Implements IResizableJaw
    'Implements IJawPatientBindable
    'Implements IJawControl


    '0 (Not Started)
    '1 (In Progress)
    '2 (Completed)

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Dim sw = StartTimer() 'New Stopwatch
        sw.Start()
        Me.DoubleBuffered = True
        'ApplyCtlGradientBackground(JawPanel,
        '                         Color.AliceBlue,
        '                         Color.Blue,
        '                         Drawing2D.LinearGradientMode.Horizontal, 128)
        StoreOriginalBounds(Me)
        InitializeDefaultTeethLayout()
        'Application.DoEvents() ' Forces UI to render updates before proceeding
        'LogToFile("KidDiag_New Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)

        'LoadPatientTreats(PatientID)
        LogTime("KidDiag_New", Me.Name, sw)
    End Sub

    Public Sub New(ByVal PatientID As Integer) ', hideDiv As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        Dim sw As New Stopwatch
        sw.Start()
        Me.DoubleBuffered = True
        ' Add any initialization after the InitializeComponent() call. 
        StoreOriginalBounds(Me)
        InitializeDefaultTeethLayout()
        'ApplyCtlGradientBackground(JawPanel,
        '                     Color.AliceBlue,
        '                      Color.CadetBlue,
        '                      Drawing2D.LinearGradientMode.ForwardDiagonal,
        '                      128)
        'Application.DoEvents() ' Forces UI to render updates before proceeding

        LoadPatientTreats(PatientID) ', hideDivider)
        sw.Stop()
        LogToFile("KidDiag_NewByVal PatientID Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    'Private hideDivider As Boolean = True
Public Sub HideShowDiv(hideDiv As Boolean) Implements IJawControl.HideShowDiv
        vertSep.Visible = Not hideDiv
        horSep.Visible = Not hideDiv
    End Sub
    Private Sub InitializeDefaultTeethLayout()
        ' Implementation to create default layout

        Dim sw = StartTimer()
        'InitializeSvgCache()
        SetKidSvgImages()

        For Each ct As Control In Me.JawPanel.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim sv As SvgImageBox = CType(ct, SvgImageBox)
                Dim col As SvgImageItemCollection = sv.RootItems

                ' Now set visibility only for items with a tag containing "IMG"
                For Each item As SvgImageItem In col 'sv.CustomizedItems
                    If Not item.Id Is Nothing AndAlso item.Id.ToString().Contains("IMG") AndAlso item.Id <> "CROWN_IMG" Then
                        item.Visible = True
                    Else
                        item.Visible = False
                    End If
                Next

                ' Refresh to apply changes
                sv.Refresh()
            End If
        Next
        LogTime(NameOf(InitializeDefaultTeethLayout), Me.Name, sw)
    End Sub

    Private Sub SetSvgImages()

        'Dim resourceMap = If(useKidMapping, Helpers.KidResourceMapping, Helpers.AdultResourceMapping)
        Dim resourceMap = Helpers.AdultResourceMapping

        For Each ct As Control In Me.JawPanel.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim sv As SvgImageBox = CType(ct, SvgImageBox)
                Dim baseKey As String = sv.Name.Replace("Out", "").Replace("Top", "").Replace("IN", "")

                If resourceMap.ContainsKey(baseKey) Then
                    Dim resources = resourceMap(baseKey)

                    If sv.Name.Contains("Out") Then
                        sv.SvgImage = resources.SvgOutResource
                    ElseIf sv.Name.Contains("Top") Then
                        sv.SvgImage = resources.SvgTopResource
                    ElseIf sv.Name.Contains("IN") Then
                        sv.SvgImage = resources.SvgInResource
                    Else
                        sv.SvgImage = Nothing
                    End If
                End If
            End If
        Next
        ''=====================
        'For Each ct As Control In Me.JawPanel.Controls
        '    If TypeOf ct Is SvgImageBox Then
        '        Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
        '        Dim col As SvgImageItemCollection = svg.RootItems

        '        ' Reset all items invisible
        '        For Each item As SvgImageItem In col
        '            item.Visible = False
        '        Next


        '        Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG").Visible = True
        '        'Dim chTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("CH"))
        '        'If chTooth IsNot Nothing Then chTooth.Visible = True

        '    End If
        'Next

    End Sub

    Private Sub SetKidSvgImages()

        Dim resourceMap = Helpers.KidResourceMapping

        For Each ct As Control In Me.JawPanel.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim sv As SvgImageBox = CType(ct, SvgImageBox)
                If sv.Name.Contains("K") Then
                    Dim baseKey As String = sv.Name.Replace("OUTK", "").Replace("INK", "").Replace("TOPK", "")
                    If resourceMap.ContainsKey(baseKey) Then
                        Dim resources = resourceMap(baseKey)
                        If sv.Name.Contains("OUT") Then
                            sv.SvgImage = resources.SvgOutResource
                        ElseIf sv.Name.Contains("IN") Then
                            sv.SvgImage = resources.SvgInResource
                        ElseIf sv.Name.Contains("TOP") Then
                            sv.SvgImage = resources.SvgTopResource
                        Else
                            sv.SvgImage = Nothing
                        End If
                    End If
                End If
            End If
        Next

    End Sub

    'Public Event ToothDoubleClick(ByVal sender As Object, ByVal e As ToothDoubleClickEvent)

    Private _valueFromParent As Integer
    Public Property ValueFromParent As Integer
        Get
            Return _valueFromParent
        End Get
        Set(ByVal value As Integer)
            _valueFromParent = value

            ' If PatientID > 0 Then LoadTreat(value) ' Call method to load data based on the passed PatientID
        End Set
    End Property
Public Property IsMobile As Boolean = False Implements IJawControl.IsMobile
    '=================================================================
    '===========================================================================================

#Region "Definitions"

    Dim clsPatientData As New PatientDATA
    Dim clsPatient As Patient
    'This section defines the logic for displaying treats for a given patient
    '=================================
    Dim PatientTrs As IEnumerable(Of Patient_Diagnosis)
    Dim clsTrtData As Patient_DiagnosisDATA
    Private ToothNum As Int16 = 0
    Private ToothName As String = ""


#End Region



#Region "LoadingTreatsSubs"
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) ' Handles btnRefresh.Click
        LoadPatientTreats(pid)
    End Sub
    Private svgExternalList As New List(Of SvgImageBox)
    Private svgDiagList As New List(Of SvgImageBox)
    Private pid As Integer = 0
    Public Sub LoadPatientTreats(patientId As Integer)
        SyncModuleBackClrForJawRendering(Me)
        pid = patientId
        Dim sw = StartTimer()
        If patientId <= 0 Then
            svgExternalList.Clear()
            svgDiagList.Clear()
            clsPatient = Nothing
            If clsPatientData IsNot Nothing Then
                clsPatientData.Dispose()
                clsPatientData = Nothing
            End If
            PatientTrs = New List(Of Patient_Diagnosis)()
            Me.SuspendLayout()
            Try
                LoadTeethTreatsUsingTreatCode(PatientTrs)
                Dim baseForm = FormManager.Instance.CurrentForm
                If baseForm IsNot Nothing AndAlso baseForm.HeaderControl IsNot Nothing Then
                    Dim lbl = baseForm.HeaderControl.IsKidLabel
                    If lbl IsNot Nothing AndAlso (lbl.Text.Contains("(Manual)") OrElse lbl.Text.Contains("(يدوي)")) Then
                        horSep.LineColor = Color.Red
                        vertSep.LineColor = Color.Red
                        horSep.LineStyle = DashStyle.DashDot
                        vertSep.LineStyle = DashStyle.DashDot
                    Else
                        horSep.LineColor = Color.FromArgb(0, 192, 192)
                        vertSep.LineColor = Color.FromArgb(0, 192, 192)
                        horSep.LineStyle = DashStyle.Solid
                        vertSep.LineStyle = DashStyle.Solid
                    End If
                End If
            Finally
                Me.ResumeLayout()
            End Try
            LogTime(NameOf(LoadPatientTreats), Me.Name, sw)
            Return
        End If
        'clear previous svgs
        svgExternalList.Clear()
        svgDiagList.Clear()

        ' Dispose of previous patient data if needed
        If clsPatientData IsNot Nothing Then
            clsPatientData.Dispose()
        End If
        ' Initialize fresh data context
        clsPatientData = New PatientDATA() ' Replace with your actual data context class
        ' Load patient data using proper disposal pattern
        Try
            Me.SuspendLayout()
            clsPatient = clsPatientData.Select_Record(New Patient With {.PatientID = patientId})
            If clsPatient IsNot Nothing Then
                ' Load tooth treatments with disposal
                Using clsTrtData As New Patient_DiagnosisDATA()
                    If Not IsMobile Then
                        PatientTrs = clsTrtData.GetPatientTeethDiags(patientId)
                        'LoadTeethTreats(PatientTrs)
                        LoadTeethTreatsUsingTreatCode(PatientTrs)
                    Else
                        PatientTrs = clsTrtData.GetPatientTeethMobDiags(patientId)
                        'LoadTeethTreats(PatientTrs)
                        LoadTeethTreatsUsingTreatCode(PatientTrs)
                    End If

                End Using
            Else
                MessageBox.Show("Patient not found.")
            End If
            Dim baseForm = FormManager.Instance.CurrentForm ' BasePatientWorkspace
            If baseForm IsNot Nothing AndAlso baseForm.HeaderControl IsNot Nothing Then
                Dim lbl = baseForm.HeaderControl.IsKidLabel
                If lbl IsNot Nothing AndAlso (lbl.Text.Contains("(Manual)") OrElse lbl.Text.Contains("(يدوي)")) Then
                    horSep.LineColor = Color.Red
                    vertSep.LineColor = Color.Red
                    horSep.LineStyle = DashStyle.DashDot
                    vertSep.LineStyle = DashStyle.DashDot
                Else
                    horSep.LineColor = Color.FromArgb(0, 192, 192)
                    vertSep.LineColor = Color.FromArgb(0, 192, 192)
                    horSep.LineStyle = DashStyle.Solid
                    vertSep.LineStyle = DashStyle.Solid
                End If
            End If



        Finally
            ' Dispose of main data context if needed
            If clsPatientData IsNot Nothing Then
                clsPatientData.Dispose()
            End If
            Me.ResumeLayout()
        End Try
        LogTime(NameOf(LoadPatientTreats), Me.Name, sw)
    End Sub

    Public Sub LoadTeethTreatsUsingTreatCode(patientTreats As IEnumerable(Of Patient_Diagnosis))
        Dim sw = StartTimer()

        'Me.Visible = False
        Try
            JawPanel.SuspendLayout()
            'For Each ct As Control In Me.JawPanel.Controls
            '    If TypeOf ct Is SvgImageBox Then
            '        Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
            '        Dim name As String = svg.Name
            '        ProcessToothTreatments(svg, svgExternalList, patientTreats)
            '    End If
            'Next
            For Each ct As Control In JawPanel.Controls.OfType(Of SvgImageBox)()
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                Dim name As String = svg.Name
                svg.BeginUpdate()  ' <== if SvgImageBox supports it
                DiagHelper.ProcessToothTreatments(svg, svgExternalList, patientTreats)
                svg.EndUpdate()
            Next
        Catch ex As Exception
        Finally
            JawPanel.ResumeLayout()
        End Try
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
    End Sub


    Public Sub LoadTeethTreats(patientTreats As IEnumerable(Of Patient_Diagnosis))
        Dim sw = StartTimer()
        JawPanel.SuspendLayout()
        Me.Visible = False
        Dim treats = If(patientTreats, Enumerable.Empty(Of Patient_Diagnosis)())
        For Each svg As SvgImageBox In JawPanel.Controls.OfType(Of SvgImageBox)()
            DiagHelper.ProcessToothTreatments(svg, svgExternalList, treats)
        Next
        JawPanel.ResumeLayout()
        Me.Visible = True
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
        Exit Sub

#If False Then
        Dim swLegacy = StartTimer()
        JawPanel.SuspendLayout()
        Me.Visible = False
        For Each ct As Control In Me.JawPanel.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                ClearSvgBackground(svg)
                Dim col As SvgImageItemCollection = svg.RootItems
                If Not svgSelectedList.Contains(svg) OrElse Not svgExternalList.Contains(svg) Then
                    svg.BackColor = Color.Transparent
                End If
                ' Reset all items invisible
                For Each item As SvgImageItem In col
                    item.Visible = False
                Next
                ' Get tooth number
                Dim toothNum As Byte = CByte(svg.Tag)

                Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG")
                Dim trtsList = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList
                Dim rootTrts = patientTreats.Where(Function(t) t.TrtLoc = "ROOT").OrderBy(Function(t) t.LVL).ToList
                Dim crownTrts = patientTreats.Where(Function(t) t.TrtLoc = "CROWN").OrderBy(Function(t) t.LVL).ToList

#Region "External Treats"
                '======IMPORTANT==========================================
                'External Treats
                ' Check for external treatments
                Dim externalTrts = trtsList.Where(Function(t) t.IsExternal.HasValue AndAlso t.IsExternal.Value = True).ToList()
                If externalTrts.Any() Then


                    ' 3. Deep Ocean Metallic (Horizontal)
                    ApplyGradientBackground(svg,
                                           ColorTranslator.FromHtml("#E8E9EB"),' Color.FromArgb(0, 163, 255),   ' Deep ocean blue
                                             ColorTranslator.FromHtml("#EBECF0"),'Color.FromArgb(0, 255, 188),   ' Metallic teal
                                            Drawing2D.LinearGradientMode.BackwardDiagonal,
                                            128) '200= 80% opacity

                    '    ' Apply special styling for external treatments

                    svgExternalList.Add(svg)
                    '' Add a tooltip to indicate external treatment
                    'Dim clinicNames = externalTrts.Select(Function(t) t.ExternalClinicName).Distinct()
                    'svg.ToolTip = "Treated externally at: " & String.Join(", ", clinicNames)

                    '' You could also add a small overlay icon/mark
                    'Dim mark = svg.RootItems.Find(Function(c) c.Id = "EXTRACTION")
                    'If mark IsNot Nothing Then mark.Visible = True
                End If

                'External Treats
                '================================================
#End Region

                If trtsList.Count = 0 Then
                    ' No treatments for this tooth, just show base image
                    If baseTooth IsNot Nothing Then baseTooth.Visible = True
                    Continue For
                End If
                Dim maxLevel As Integer = trtsList.Max(Function(t) t.LVL)
                ' Always show IMG placeholder if needed
                If baseTooth IsNot Nothing AndAlso maxLevel < 4 Then
                    baseTooth.Visible = True
                End If

                Select Case maxLevel
                    Case 0, 1, 2, 3
                        For Each t In trtsList
                            Dim item = col.Find(Function(c) c.Id = t.PropertyName)
                            If item IsNot Nothing Then
                                item.Visible = True
                                item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                item.Appearance.Normal.BorderThickness = t.BorderThickness
                            End If
                            If t.LVL = 0 AndAlso t.Treat = "DIRECT PULP CAPPING" Then
                                If item IsNot Nothing Then
                                    Dim dir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")

                                    item.Visible = True
                                    item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                    item.Appearance.Normal.BorderThickness = 2 ' t.BorderThickness
                                    Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                                    If indir IsNot Nothing Then
                                        indir.Visible = True
                                        indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                        indir.Appearance.Normal.BorderThickness = t.BorderThickness
                                    End If
                                End If
                            End If


                            If t.LVL = 3 AndAlso t.Treat.EndsWith("T") Then
                                Dim crownTooth = col.Find(Function(c) c.Id = "CROWN_FILL")
                                If crownTooth IsNot Nothing Then
                                    For Each itemc As SvgImageItem In col
                                        itemc.Visible = False
                                    Next
                                    DiagHelper.ShowBaseTooth(col)
                                    crownTooth.Visible = True
                                    crownTooth.Appearance.Normal.BorderThickness = 1
                                    For Each tc In trtsList
                                        Select Case tc.Treat
                                            Case "METAL CROWN T", "ZERCONIA CROWN T", "PFM CROWN T", "EMAX CROWN T", "TEMP CROWN T", "STAINLESS STEEL CROWN T"
                                                If crownTooth IsNot Nothing Then crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                        End Select
                                    Next
                                End If
                                If rootTrts.Any Then
                                    Dim rootItem = col.Find(Function(c) c.Id = t.PropertyName)
                                    If rootItem IsNot Nothing Then
                                        rootItem.Visible = True
                                        rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                        rootItem.Appearance.Normal.BorderThickness = t.BorderThickness
                                    End If
                                    If t.LVL = 0 AndAlso t.Treat = "DIRECT PULP CAPPING" Then
                                        If rootItem IsNot Nothing Then
                                            Dim dir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")

                                            rootItem.Visible = True
                                            rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                            rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                            rootItem.Appearance.Normal.BorderThickness = 2 ' t.BorderThickness
                                            Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                                            If indir IsNot Nothing Then
                                                indir.Visible = True
                                                indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                                indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                                indir.Appearance.Normal.BorderThickness = t.BorderThickness
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                    Case 4 ' EXTRACTED
                        If svgExternalList.Contains(svg) Then
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTION")
                            If extracted IsNot Nothing Then extracted.Visible = True
                            ' Ensure only extraction is visible
                            For Each item As SvgImageItem In col
                                If item.Id <> "EXTRACTION" Then item.Visible = False
                            Next
                        Else
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                            If extracted IsNot Nothing Then extracted.Visible = True
                            ' Ensure only extraction is visible
                            For Each item As SvgImageItem In col
                                If item.Id <> "EXTRACTED" Then item.Visible = False
                            Next
                        End If

                    Case 5 ' Implant
                        Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                        If implant IsNot Nothing Then implant.Visible = True
                        For Each item As SvgImageItem In col
                            If item.Id <> "IMPLANT" Then item.Visible = False
                        Next
                    Case 6 ' Implant + stages
                        ' Show implant always
                        Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                        If implant IsNot Nothing Then implant.Visible = True
                        ' Check and show Healing Cap
                        If trtsList.Any(Function(t) t.PropertyName = "HEALING_CAP" AndAlso t.LVL = 6) Then
                            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal IsNot Nothing Then heal.Visible = True
                        End If
                        ' Check and show Abutment
                        If trtsList.Any(Function(t) t.PropertyName = "ABUTMENT" AndAlso t.LVL = 6) Then
                            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut IsNot Nothing Then abut.Visible = True
                        End If
#Region "TOdel"
                        ' Show final crown and hide Healing/Abutment if present
                        If trtsList.Any(Function(t) t.PropertyName = "CROWN_IMG" AndAlso t.LVL = 6) Then
                            Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                            If crownImg IsNot Nothing Then crownImg.Visible = True
                            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal IsNot Nothing Then heal.Visible = False
                            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut IsNot Nothing Then abut.Visible = False
                            ' Optional: extra visual crown
                            If svg.Name.Contains("Top") Then
                                ' Make sure base image is shown if needed
                                Dim topBase = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG"))
                                If topBase IsNot Nothing Then topBase.Visible = True
                                ' Hide implant layer
                                Dim implantLayer = col.Find(Function(c) c.Id = "IMPLANT")
                                If implantLayer IsNot Nothing Then implantLayer.Visible = False
                                Dim heal1 = col.Find(Function(c) c.Id = "HEALING_CAP")
                                If heal1 IsNot Nothing Then heal.Visible = False
                                Dim abut1 = col.Find(Function(c) c.Id = "ABUTMENT")
                                If abut1 IsNot Nothing Then abut.Visible = False
                            End If
                            Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownFill IsNot Nothing Then
                                crownFill.Visible = True
                                crownFill.Appearance.Normal.BorderThickness = 0
                                For Each t In trtsList.Where(Function(c) c.LVL = 3)
                                    Select Case t.Treat
                                        Case "METAL CROWN", "ZERCONIA CROWN", "PFM CROWN", "EMAX CROWN", "TEMP CROWN", "STAINLESS STEEL CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    End Select
                                Next
                            End If
                        End If
#End Region
                    Case 7 'CROWN ON IMPLANT
                        'TOP VIEW
                        If svg.Name.Contains("Top") Then
                            For Each item As SvgImageItem In col
                                item.Visible = False
                            Next
                            Dim crownIMP = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownIMP IsNot Nothing Then
                                crownIMP.Visible = True
                                crownIMP.Appearance.Normal.BorderThickness = 1
                                For Each tc In trtsList
                                    Select Case tc.Treat
                                        Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I", "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                            If crownIMP IsNot Nothing Then crownIMP.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                    End Select
                                Next
                            End If
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                            End If
                        Else
                            ' Show implant always
                            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                            If implant IsNot Nothing Then implant.Visible = True
                            Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownImg IsNot Nothing Then
                                crownImg.Visible = True
                            End If
                            Dim crownIMP = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownIMP IsNot Nothing Then
                                crownIMP.Visible = True
                                crownIMP.Appearance.Normal.BorderThickness = 1
                                For Each tc In trtsList

                                    Select Case tc.Treat
                                        Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I",
                                        "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                            crownIMP.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                    End Select
                                Next
                            End If
                        End If
                    Case 8 ' BRIDGE  
                        ' 1. Handle bridge differently per view
                        If svg.Name.Contains("Top") Then
                            For Each item As SvgImageItem In col
                                item.Visible = False
                            Next
                            ' Top View - style the base tooth as crown
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                            End If
                            'Show BRIDGE Mark
                            Dim bridgMark = col.Find(Function(c) c.Id = "BR")
                            If bridgMark IsNot Nothing Then
                                bridgMark.Visible = True
                                bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
                            End If
#Region "delete"
                        ElseIf svg.Name.Contains("Out") Then
                            ' 2. Always show CON if exists
                            If trtsList.Any(Function(t) t.PropertyName = "IMPLANT") Then
                                Dim IMPLANT = col.Find(Function(c) c.Id = "IMPLANT")
                                If IMPLANT IsNot Nothing Then IMPLANT.Visible = True
                                ' Out View - show dedicated crown layer
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                If crownImg IsNot Nothing Then
                                    crownImg.Visible = True
                                End If
                            ElseIf trtsList.Any(Function(t) t.PropertyName = "EXTRACTION") Then
                                Dim EXTRACTED = col.Find(Function(c) c.Id = "EXTRACTION")
                                If EXTRACTED IsNot Nothing Then EXTRACTED.Visible = True
                                ' Out View - show dedicated crown layer
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                If crownImg IsNot Nothing Then
                                    crownImg.Visible = True
                                End If
                            Else
                                baseTooth.Visible = True
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
#End Region

                        End If
                        'Show Crown Fill
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



                End Select

            End If
        Next

        JawPanel.ResumeLayout()
        Me.Visible = True
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
  




#End If
    End Sub
#Region "Single Treat"
    '===========================================
    'Dim clsPatientData As New PatientDATA
    'Dim clsPatient As Patient
    Private ToothTrts As IEnumerable(Of Patient_Diagnosis)
    Private PatientTreats As IEnumerable(Of Patient_Diagnosis)
    Dim clsToothTrtData As New Patient_DiagnosisDATA
    Public Sub LoadSnglTreat(patientId As Integer, toothNum As Byte)
        ' Load patient data
        clsPatient = New Patient With {.PatientID = patientId}
        clsPatient = clsPatientData.Select_Record(clsPatient)

        If clsPatient IsNot Nothing Then
            ' Load tooth checks for the patient

            PatientTreats = clsToothTrtData.Select_BypID_tNum(New Patient_Diagnosis With {.PatientID = patientId, .ToothNum = toothNum})
            ' Pass the loaded PatientCheck data to the next method
            LoadPatientSnglToothTreat(PatientTreats)
        Else
            MessageBox.Show("Patient not found.")
        End If
    End Sub

    Public Sub LoadPatientSnglToothTreat(patientTreats As IEnumerable(Of Patient_Diagnosis))

        DiagHelper.ProcessToothTreatments(zSvg, svgExternalList, patientTreats)

    End Sub

    '==============================================

#End Region

#End Region



#Region "MouseActivity"

    Private Sub JawPanel_MouseClick(sender As Object, e As MouseEventArgs) Handles JawPanel.MouseClick
        If e.Button = MouseButtons.Right Then
            ClearSvgSelection()
            UnSelect()
            slctdSVG = Nothing
            DragSource = Nothing
            TrtSourceHelper.ClearAddedTrtsListBound(AddedTrtsList, originalAddedTrtsTable)
            JawTreatmentTreeHelper.LoadEmptyJawBackgroundTreatTree(TrtsTreeView, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=True)
            txtSrchTrt.ResetText()
            Flyout1.OwnerControl = Me
            Flyout1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
            Flyout1.Options.Location = New Point(JawPanel.Left + e.X, JawPanel.Top + e.Y)
            Flyout1.ShowPopup()
        ElseIf e.Button = MouseButtons.Left Then
            JawPanel.Focus()
        End If
    End Sub


    ' Common handler for UnSelect SVG
    Private Sub UnSelect()
        For Each ct As Control In Me.JawPanel.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim sv As SvgImageBox = CType(ct, SvgImageBox)
                sv.Text = ""
                If Not svgSelectedList.Contains(sv) OrElse Not svgExternalList.Contains(sv) Then
                    sv.BackColor = Color.Transparent
                End If
            End If

        Next

    End Sub

    '============================================
    ' Field to store the source of the drag operation

    Private originalSize As Size = New Size(67, 95)
    Private enlargedSize As Size = New Size(CInt(originalSize.Width * 1.12), CInt(originalSize.Height * 1.12))
    Private originalLocation As Point = New Point(100, 100) ' Adjust as needed for initial location
    Private originalWidth As Integer = 67
    Private originalHeight As Integer = 95



    Private zoomFactor As Single = 4.0F ' Initial zoom factor
    Private isZooming As Boolean = False ' Tracks if zoom logic is being executed

    'Using Timer Code
    ' Declare timer and other variables
    Private WithEvents zoomTimer As New Timer With {.Interval = 100}
    Private mouseDownTimeZoom As DateTime
    Private Const maxZoomFactor As Single = 5.0F
    Private Const zoomHoldDuration As Integer = 1000 ' Time in milliseconds (1 second)

#Region "Zooming"



    ' Initialize the timer
    Private Sub InitializeZoomTimer()
        zoomTimer.Interval = 100 ' Check every 100 ms
    End Sub

    ' MouseDown Event: Start the timer
    Private Sub SvgImageBox_MouseDown(sender As Object, e As MouseEventArgs)
        slctdSVG = CType(sender, SvgImageBox)
        originalSize = slctdSVG.Size
        originalWidth = slctdSVG.Width
        originalHeight = slctdSVG.Height
        mouseDownTimeZoom = DateTime.Now
        mouseDownTimeZoom = DateTime.Now
        isZooming = False ' Reset zooming flag
        zoomTimer.Start()
    End Sub

    ' MouseUp Event: Stop the timer and reset zoom if not held long enough
    Private Sub SvgImageBox_MouseUp(sender As Object, e As MouseEventArgs)

        Dim svg As SvgImageBox = CType(sender, SvgImageBox)
        zoomTimer.Stop()

        ' If the mouse was released before reaching the hold duration, do nothing
        If Not isZooming Then
            Console.WriteLine("Simple click detected, no zoom applied.")
            Return
        Else
            ' Reset zoom only if the mouse was released before reaching the hold duration
            If (DateTime.Now - mouseDownTimeZoom).TotalMilliseconds < zoomHoldDuration Then
                ResetZoom(svg) ' Optional: Reset zoom or perform another action
            End If
        End If

        slctdSVG = Nothing
    End Sub

    ' Timer Tick: Check if the mouse is held long enough to apply maximum zoom
    Private Sub zoomTimer_Tick(sender As Object, e As EventArgs) Handles zoomTimer.Tick
        If (DateTime.Now - mouseDownTimeZoom).TotalMilliseconds >= zoomHoldDuration Then
            zoomTimer.Stop() ' Stop the timer to prevent multiple triggers
            isZooming = True ' Mark that zoom logic is being executed
            'ApplyMaxZoom(slctdSVG) ' Zoom to the maximum factor
            ApplyZoomZ(slctdSVG)
        End If
    End Sub
    Private Sub ResetZoomZ()
        '=================================
        zSvg.Visible = False

        zSvg.SendToBack()
    End Sub

    Private Sub ApplyZoomZ(svgImageBox As DevExpress.XtraEditors.SvgImageBox)
        '' Create a new SvgImageBox for zooming
        zSvg.Visible = False
        zSvg.SvgImage = svgImageBox.SvgImage ' Copy the SVG image
        zSvg.Name = svgImageBox.Name
        zSvg.Tag = svgImageBox.Tag
        LoadSnglTreat(PatientID, CByte(svgImageBox.Tag))
        ' Center the zoomed image box in the panel
        zSvg.Location = New Point(
        (JawPanel.Width - zSvg.Width) \ 2,
        (JawPanel.Height - zSvg.Height) \ 2)

        zSvg.BringToFront()
        zSvg.Visible = True
    End Sub

    ' Apply the maximum zoom factor
    Private Sub ApplyMaxZoom(svgImageBox As DevExpress.XtraEditors.SvgImageBox)
        zoomFactor = maxZoomFactor
        ApplyZoom(svgImageBox) ' Use your existing ApplyZoom logic
        Console.WriteLine("Max zoom applied: " & zoomFactor)
    End Sub

    ' Optional: Reset zoom to default (if needed)
    Private Sub ResetZoom(svgImageBox As DevExpress.XtraEditors.SvgImageBox)
        zoomFactor = 1.0F
        ApplyZoom(svgImageBox)
        Console.WriteLine("Zoom reset to default: " & zoomFactor)
    End Sub

    Private Sub ApplyZoom(svgImageBox As DevExpress.XtraEditors.SvgImageBox)
        ' Ensure zoomFactor is clamped within valid bounds
        zoomFactor = Math.Max(1.0F, Math.Min(4.0F, zoomFactor))

        ' Get the original dimensions of the parent control and SVG image box
        Dim parentWidth As Integer = svgImageBox.Parent.Width
        Dim parentHeight As Integer = svgImageBox.Parent.Height

        ' Scale the SVG size based on the zoom factor
        svgImageBox.Width = CInt(originalWidth * zoomFactor)
        svgImageBox.Height = CInt(originalHeight * zoomFactor)

        ' Calculate the new position to center the SVG in the parent control
        Dim newX As Integer = (parentWidth - svgImageBox.Width) \ 2
        Dim newY As Integer = (parentHeight - svgImageBox.Height) \ 2

        ' Apply the new position
        svgImageBox.Left = newX
        svgImageBox.Top = newY

        ' Bring the SVG image box to the front to ensure visibility
        svgImageBox.BringToFront()

    End Sub



#End Region


    Private DragSource As SvgImageBox = Nothing
    Private slctdSVG As SvgImageBox
    Private IsInsvgList As Boolean



#Region "Svg Item Events"

    'Svg Item Events

    '========================================================
    ' Common handler for SvgItemClick
    Private Sub CommonSvgItemClick(sender As Object, e As SvgImageItemMouseEventArgs)
        'Dim svgItem As SvgImageItem = e.Item
        'Dim clickedButton As MouseButtons = e.Button
        'MessageBox.Show($"Item Clicked: {svgItem.Id}, Button: {clickedButton}")
    End Sub

    ' Common handler for SvgItemLeave
    Private Sub CommonSvgItemLeave(sender As Object, e As SvgImageItemEventArgs)
        ''Dim svgItem As SvgImageItem = e.Item
        ''MessageBox.Show($"Mouse Left Item: {svgItem.Id}")
        'e.Item.Appearance.Normal.BorderColor = Color.Black
        'If e.Item.Id.StartsWith("CH") Then
        '    e.Item.Appearance.Normal.BorderThickness = 0
        'ElseIf e.Item.Id.StartsWith("BR") Then
        '    e.Item.Appearance.Normal.BorderThickness = 0
        'Else
        '    e.Item.Appearance.Normal.BorderThickness = 1
        'End If

    End Sub

    ' Common handler for SvgItemEnter
    Private Sub CommonSvgItemEnter(sender As Object, e As SvgImageItemEventArgs)
        If PatientID <= 0 Then Return
        Dim Svg As SvgImageBox = CType(sender, SvgImageBox)
        Dim svgItem As SvgImageItem = e.Item
        Dim toothNum As Byte = 0
        Try
            toothNum = Convert.ToByte(Svg.Tag)
        Catch
            Return
        End Try

        Dim superTip As New SuperToolTip With {.AllowHtmlText = DefaultBoolean.True}
        Dim titleItem As New ToolTipTitleItem()
        Dim baseName As String = If(Svg.Name.Contains("IN"), Svg.Name.Substring(0, Svg.Name.Length - 3), Svg.Name.Substring(0, Svg.Name.Length - 4))
        titleItem.Text = $"{baseName} -->> {Svg.Tag}"

        Dim regularItem As New ToolTipItem()
        regularItem.Text = String.Empty

        Dim toothDiags As IEnumerable(Of Patient_Diagnosis) = clsToothTrtData.Select_BypID_tNum(New Patient_Diagnosis With {.PatientID = PatientID, .ToothNum = toothNum})
        Dim notesList As New List(Of String)()
        If toothDiags IsNot Nothing Then
            Dim itemId As String = If(svgItem?.Id, "")
            For Each t As Patient_Diagnosis In toothDiags
                If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(t.TreatNotes) Then
                    If String.IsNullOrEmpty(itemId) OrElse String.Equals(t.PropertyName, itemId, StringComparison.OrdinalIgnoreCase) Then
                        notesList.Add(t.TreatNotes.Trim())
                    End If
                End If
            Next
            If notesList.Count = 0 AndAlso Not String.IsNullOrEmpty(itemId) Then
                For Each t As Patient_Diagnosis In toothDiags
                    If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(t.TreatNotes) Then
                        notesList.Add(t.TreatNotes.Trim())
                    End If
                Next
            End If
        End If

        If notesList.Count > 0 Then
            Dim backColorHex As String = ColorTranslator.ToHtml(Color.LightYellow)
            Dim noteColors As Color() = {Color.DarkBlue, Color.DarkGreen, Color.DarkRed, Color.DarkOrange, Color.Purple, Color.Teal}
            Dim parts As New List(Of String)()
            For i As Integer = 0 To notesList.Count - 1
                Dim hex As String = ColorTranslator.ToHtml(noteColors(i Mod noteColors.Length))
                parts.Add($"<color={hex}><b>{System.Net.WebUtility.HtmlEncode(notesList(i))}</b></color>")
            Next
            regularItem.Text = $"<backcolor={backColorHex}>" & String.Join("<br/>", parts) & "</backcolor>"
        End If

        superTip.Items.Add(titleItem)
        superTip.Items.Add(regularItem)
        Svg.SuperTip = superTip
    End Sub

    ' Common handler for SvgItemPress
    Private Sub CommonSvgItemPress(sender As Object, e As SvgImageItemEventArgs)
        'Dim svgItem As SvgImageItem = e.Item
        ''MessageBox.Show($"Item Pressed: {svgItem.Id}")
    End Sub
    '======================================================

#End Region

    '========================================================

    ' MouseDown Event: Start the timer
    ' Common handler for MouseDown
    Private Sub CommonMouseDownHandler(sender As Object, e As MouseEventArgs)
        'Dim clickedControl As Control = CType(sender, Control)
        If e.Button = MouseButtons.Left Then
            slctdSVG = CType(sender, SvgImageBox)
            mouseDownTimeZoom = DateTime.Now
            isZooming = False ' Reset zooming flag
            zoomTimer.Start()
        End If
    End Sub

    ' Common handler for MouseUp
    ' MouseUp Event: Stop the timer and reset zoom if not held long enough
    Private Sub CommonMouseUpHandler(sender As Object, e As MouseEventArgs)
        'Dim clickedControl As Control = CType(sender, Control)
        Dim svg As SvgImageBox = CType(sender, SvgImageBox)
        zoomTimer.Stop()

        ' If the mouse was released before reaching the hold duration, do nothing
        If Not isZooming Then
            Console.WriteLine("Simple click detected, no zoom applied.")
            Return
        Else
            ' Reset zoom only if the mouse was released before reaching the hold duration
            If (DateTime.Now - mouseDownTimeZoom).TotalMilliseconds < zoomHoldDuration Then
                ResetZoom(svg) ' Optional: Reset zoom or perform another action
            End If
        End If

        slctdSVG = Nothing
    End Sub

    ' Common handler for ControlClick
    Private Sub CommonControlClick(sender As Object, e As EventArgs)

    End Sub

    Private Function EnsureRightClickToothTracked(svg As SvgImageBox, toothNum As Byte) As Integer
        Dim outView As SvgImageBox = FindOutView(CInt(toothNum))
        Dim listItemName As String = If(outView IsNot Nothing, outView.Name, svg.Name)

        If selectedTeethList Is Nothing OrElse selectedTeethList.Count <= 1 Then
            addList.Items.Clear()
            If Not String.IsNullOrWhiteSpace(listItemName) Then
                addList.Items.Add(listItemName)
            End If
        End If

        Return If(selectedTeethList Is Nothing, 0, selectedTeethList.Count)
    End Function

    ' Common handler for MouseClick
    Private Sub CommonMouseClickHandler(sender As Object, e As MouseEventArgs)
        JawPanel.Focus()
        If e.Button = MouseButtons.Right Then

            ' Set the drag source
            DragSource = DirectCast(sender, SvgImageBox)
            Dim svg As SvgImageBox = CType(sender, SvgImageBox)
            slctdSVG = CType(sender, SvgImageBox)
            DragSource.BackColor = Color.PapayaWhip
            ' Get the base name and tooth information
            Dim baseName As String = ""
            If svg.Name.Contains("IN") Then
                baseName = svg.Name.Substring(0, svg.Name.Length - 3) ' Removes "Out", "Top", or similar
            Else
                baseName = svg.Name.Substring(0, svg.Name.Length - 4) ' Removes "Out", "Top", or similar
            End If
            Dim numberPart As String = svg.Name.Substring(svg.Name.Length - 1)
            ' Apply the treatment to the specific SvgImageBox
            Dim toothNum As Byte = Convert.ToByte(svg.Tag)
            Dim toothName As String = $"{baseName}{numberPart}".ToUpper
            Dim loc As Point = svg.Location
            Dim toothID As Int16 = 0
            toothID = ExtractDigit(svg.Name)
            Dim targetCount As Integer = EnsureRightClickToothTracked(svg, toothNum)
            SetAddedTrtsListDataSource(PatientID, toothID, GetToothFullName(toothName))
            If targetCount <= 1 Then
                SetTreeTreatsSingleTooth(toothID, toothNum)
            Else
                SetTrtsTreeMultiTeeth()
            End If

            ' Same flyout positioning as AdultDiag: manual anchor, position by jaw quadrant (Ld/Lu/Rd/Ru).
            ' Kid jaws use uppercase prefixes (e.g. LDOUTK6, LUTOPK4) so use case-insensitive match so all kid jaws get the flyout.
            Flyout1.OwnerControl = Me
            Flyout1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
            If svg.Name.StartsWith("Ld", StringComparison.OrdinalIgnoreCase) Then
                Flyout1.Options.Location = New System.Drawing.Point(loc.X - Flyout1.Width, loc.Y + svg.Height - Flyout1.Height)
            ElseIf svg.Name.StartsWith("Rd", StringComparison.OrdinalIgnoreCase) Then
                Flyout1.Options.Location = New System.Drawing.Point(loc.X + svg.Width, loc.Y + svg.Height - Flyout1.Height)
            ElseIf svg.Name.StartsWith("Lu", StringComparison.OrdinalIgnoreCase) Then
                Flyout1.Options.Location = New System.Drawing.Point(loc.X - Flyout1.Width, loc.Y)
            ElseIf svg.Name.StartsWith("Ru", StringComparison.OrdinalIgnoreCase) Then
                Flyout1.Options.Location = New System.Drawing.Point(loc.X + svg.Width, loc.Y)
            End If

            BackClr = Me.BackColor

            Flyout1.ShowPopup()

            'Flyout1.ShowBeakForm()
        ElseIf e.Button = MouseButtons.Left Then
            Dim svg As SvgImageBox = CType(sender, SvgImageBox)
            ToothNum = Convert.ToByte(svg.Tag)

            If Control.ModifierKeys = Keys.Control Then
                ' Ctrl+Click handling for selection
                If selectedTeethList.Contains(ToothNum) Then
                    ' Tooth already selected - deselect all views
                    DeselectAllViewsOfTooth(svg, ToothNum)
                Else
                    ' Select this view and deselect other views of same tooth
                    SelectSingleViewOfTooth(FindOutView(svg.Name), ToothNum)
                    ' selectedTeethList must always be unique
                    Debug.Assert(selectedTeethList.Distinct().Count = selectedTeethList.Count)

                    ' LINQ version to find all selected SvgImageBox controls
                    Dim selectedSvgs = From c In JawPanel.Controls.OfType(Of SvgImageBox)()
                                       Where c.Text = "Slct"
                                       Select c

                    For Each sv In selectedSvgs
                        If sv.Name.Contains("Top") Then
                            Dim outView As SvgImageBox = FindOutView(sv.Name) ' FindOutView(CByte(sv.Tag))
                            If outView IsNot Nothing Then
                                SelectSvg(outView)
                                'SelectSingleViewOfTooth(outView, CByte(outView.Tag))
                            End If
                        Else
                            SelectSingleViewOfTooth(sv, CByte(sv.Tag))
                        End If
                    Next
                End If
            Else
                selectedTeethList.Clear()

                If svgExternalList.Count > 0 Then
                    For Each sv In svgExternalList
                        sv.BackColor = Color.Wheat
                    Next
                End If
                ClearSvgSelection()
                'grpSlctdTeeth.Visible = selectedTeethList.Count > 0
                slctdSVG = CType(sender, SvgImageBox)
                UnSelect()

                svg.Text = "Slct"
                svg.BringToFront()
                If Not svgSelectedList.Contains(svg) OrElse Not svgExternalList.Contains(svg) Then
                    'svg.BackColor = Color.FromArgb(230, 130, 202, 255)
                    svg.BackColor = ColorTranslator.FromHtml("#D5FFFF")
                End If

                Selected = False
            End If
            BackClr = Me.BackColor
        End If
    End Sub

    ' Common handler for MouseDoubleClick
    Private Sub CommonMouseDoubleClickHandler(sender As Object, e As MouseEventArgs)
        'If PatientID = 0 Then Exit Sub
        ' Identify the clicked SvgImageBox
        Dim clickedBox As SvgImageBox = CType(sender, SvgImageBox)
        RaiseEvent ToothDoubleClick(sender, New ToothDoubleClickEvent(PatientID, PatientName, clickedBox, "Treat"))
    End Sub


    ' Common handler for MouseEnter
    Private Sub CommonMouseEnterHandler(sender As Object, e As EventArgs)
        If Not isZooming Then
            Dim svg As SvgImageBox = CType(sender, SvgImageBox)
            svg.BringToFront()
            'svg.BackColor = ColorTranslator.FromHtml("#D5FFFF")
            'svg.BackColor = Color.FromArgb(230, 130, 202, 255)
            If Not svg.Text = "Slct" Then
                svg.BackColor = Color.FromArgb(170, 198, 222, 255)
            End If

            'originalLocation = svg.Location
            'originalSize = svg.Size
        End If
        If zSvg.Visible = True Then
            zSvg.BringToFront()
        End If

    End Sub

    ' Common handler for MouseHover
    Private Sub CommonMouseHoverHandler(sender As Object, e As EventArgs)
        Dim Svg As SvgImageBox = CType(sender, SvgImageBox)
        Svg.BringToFront()

        If PatientID <= 0 Then Return
        Dim toothNum As Byte = 0
        Try
            toothNum = Convert.ToByte(Svg.Tag)
        Catch
            Return
        End Try

        Dim superTip As New SuperToolTip With {.AllowHtmlText = DefaultBoolean.True}
        Dim titleItem As New ToolTipTitleItem()
        Dim baseName As String = If(Svg.Name.Contains("IN"), Svg.Name.Substring(0, Svg.Name.Length - 3), Svg.Name.Substring(0, Svg.Name.Length - 4))
        titleItem.Text = $"{baseName} -->> {Svg.Tag}"

        Dim regularItem As New ToolTipItem()
        regularItem.Text = String.Empty

        Dim toothDiags As IEnumerable(Of Patient_Diagnosis) = clsToothTrtData.Select_BypID_tNum(New Patient_Diagnosis With {.PatientID = PatientID, .ToothNum = toothNum})
        Dim notesList As New List(Of String)()
        If toothDiags IsNot Nothing Then
            For Each t As Patient_Diagnosis In toothDiags
                If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(t.TreatNotes) Then
                    notesList.Add(t.TreatNotes.Trim())
                End If
            Next
        End If

        If notesList.Count > 0 Then
            Dim backColorHex As String = ColorTranslator.ToHtml(Color.LightYellow)
            Dim noteColors As Color() = {Color.DarkBlue, Color.DarkGreen, Color.DarkRed, Color.DarkOrange, Color.Purple, Color.Teal}
            Dim parts As New List(Of String)()
            For i As Integer = 0 To notesList.Count - 1
                Dim hex As String = ColorTranslator.ToHtml(noteColors(i Mod noteColors.Length))
                parts.Add($"<color={hex}><b>{System.Net.WebUtility.HtmlEncode(notesList(i))}</b></color>")
            Next
            regularItem.Text = $"<backcolor={backColorHex}>" & String.Join("<br/>", parts) & "</backcolor>"
        End If

        superTip.Items.Add(titleItem)
        superTip.Items.Add(regularItem)
        Svg.SuperTip = superTip
    End Sub

    ' Common handler for MouseLeave
    Private Sub CommonMouseLeaveHandler(sender As Object, e As EventArgs)

        Dim svg As SvgImageBox = CType(sender, SvgImageBox)

        ' Only change appearance if not selected
        If Not svgSelectedList.Contains(svg) AndAlso Not svgExternalList.Contains(svg) Then
            svg.BackColor = Color.Transparent
            Dim col = svg.RootItems
            'Dim col As SvgImageItemCollection = svg.RootItems
            If svg.Text = "Slct" Then
                svg.BringToFront()
                'svg.BackColor = Color.FromArgb(170, 198, 222, 255) '(21, 137, 255)
                svg.BackColor = ColorTranslator.FromHtml("#D5FFFF") ' Color.FromArgb(230, 130, 202, 255)
            Else
                svg.BackColor = Color.Transparent
                svg.SendToBack()
            End If
            zoomFactor = 1.0F
            isZooming = False
        End If

        'If svgExternalList.Count > 0 Then
        '    For Each sv In svgExternalList
        '        sv.BackColor = Color.LightGray
        '    Next
        'End If
        If zSvg.Visible = True Then
            zSvg.BringToFront()
        End If



    End Sub


#Region "Select Svgs By Mouse Or Range"

#Region "Selection Variables"
    Private selectionStart As Point
    Private selectionRect As Rectangle
    Private isSelecting As Boolean = False
    Private firstSelectedTooth As Integer = -1
    Private svgSelectedList As New List(Of SvgImageBox)
    Private selectedTeethList As New List(Of Byte)
    Private showEditTrtBtn As Boolean = True
    Dim Selected As Boolean = False
    Dim senderSvg As SvgImageBox
    Private ReadOnly PhysicalToothOrder As Integer() = {
        18, 17, 16, 15, 14, 13, 12, 11,  ' RU (right to left)
        21, 22, 23, 24, 25, 26, 27, 28,   ' LU (left to right)
        38, 37, 36, 35, 34, 33, 32, 31,   ' LD (left to right)
        41, 42, 43, 44, 45, 46, 47, 48    ' RD (right to left)
    }
#End Region


    Public Sub DeselectSvg(svg As SvgImageBox)
        If svgSelectedList.Contains(svg) Then
            svg.BackColor = Color.Transparent
            svgSelectedList.Remove(svg)
            Dim toothNum As Byte = Convert.ToByte(svg.Tag)
            If selectedTeethList.Contains(toothNum) Then
                selectedTeethList.Remove(toothNum)
                RemoveToothFromAddList(toothNum)
            End If
            UpdateEditButtonVisibility()
        End If
    End Sub

#Region "Selection Methods"
    Public Sub SelectSvg(svg As SvgImageBox)
        If Not svgSelectedList.Contains(svg) Then
            svg.BackColor = Color.Yellow
            svg.Text = "Slct"
            svgSelectedList.Add(svg)

            Dim toothNum As Byte = Convert.ToByte(svg.Tag)
            If Not selectedTeethList.Contains(toothNum) Then
                selectedTeethList.Add(toothNum)

                ' add svg.Name only if it doesn't already exist
                Dim exists As Boolean = addList.Items.Cast(Of Object)().Any(Function(i) i.ToString() = svg.Name)

                If Not exists Then
                    addList.Items.Add(svg.Name)
                End If
            End If

            UpdateEditButtonVisibility()
        End If
    End Sub

    Public Sub ClearSvgSelection()
        For Each svg As SvgImageBox In svgSelectedList
            svg.BackColor = Color.Transparent
        Next
        svgSelectedList.Clear()
        selectedTeethList.Clear()
        addList.Items.Clear()
        UpdateEditButtonVisibility()
    End Sub
    Public Sub SelectSingleViewOfTooth(svg As SvgImageBox, toothNum As Byte)
        '' First deselect any other views of this tooth
        'DeselectAllViewsOfTooth(toothNum)
        '' Now select the clicked view
        'SelectSvg(svg)

        ' Only deselect other views if this tooth is NOT already selected
        If Not selectedTeethList.Contains(toothNum) Then
            DeselectAllViewsOfTooth(svg, toothNum)
        End If

        SelectSvg(svg)
    End Sub
    Public Sub DeselectAllViewsOfTooth(svg As SvgImageBox, toothNum As Byte)
        ' Remove from selected teeth list
        If selectedTeethList.Contains(toothNum) Then
            selectedTeethList.Remove(toothNum)
        End If

        ' Find and deselect all SVG views of this tooth
        For Each ctrl As Control In JawPanel.Controls
            If TypeOf ctrl Is SvgImageBox Then
                Dim toothSvg As SvgImageBox = DirectCast(ctrl, SvgImageBox)
                If Convert.ToByte(toothSvg.Tag) = toothNum AndAlso svgSelectedList.Contains(toothSvg) Then
                    toothSvg.BackColor = Color.Transparent
                    svgSelectedList.Remove(toothSvg)
                    RemoveToothFromAddList(toothSvg.Name)
                End If
            End If
        Next
        UpdateEditButtonVisibility()
    End Sub
    Public Sub RemoveToothFromAddList(svgName As String)
        ' Remove all entries for this tooth from addList
        Dim itemsToRemove As New List(Of String)
        For Each item As String In addList.Items
            If item.Contains(svgName.ToString()) Then
                itemsToRemove.Add(item)
            End If
        Next
        For Each item In itemsToRemove
            addList.Items.Remove(item)
        Next
    End Sub
    Private Sub UpdateEditButtonVisibility()
        showEditTrtBtn = selectedTeethList.Count = 0
        ' Add your code to show/hide edit button here
    End Sub
#End Region

#Region "Mouse Selection Handlers"
    Private Sub JawPanel_MouseDown(sender As Object, e As MouseEventArgs) Handles JawPanel.MouseDown
        If e.Button = MouseButtons.Left Then
            selectionStart = e.Location
            isSelecting = True
            selectionRect = New Rectangle(e.Location, Size.Empty)
            JawPanel.Invalidate()
        End If
    End Sub

    Private Sub JawPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles JawPanel.MouseMove
        If isSelecting Then
            Dim x As Integer = Math.Min(selectionStart.X, e.X)
            Dim y As Integer = Math.Min(selectionStart.Y, e.Y)
            Dim width As Integer = Math.Abs(e.X - selectionStart.X)
            Dim height As Integer = Math.Abs(e.Y - selectionStart.Y)
            selectionRect = New Rectangle(x, y, width, height)
            JawPanel.Invalidate()
        End If
    End Sub

    Private Sub JawPanel_MouseUp(sender As Object, e As MouseEventArgs) Handles JawPanel.MouseUp
        If e.Button = MouseButtons.Left AndAlso isSelecting Then
            isSelecting = False
            SelectControlsInRectangle(selectionRect)
            selectionRect = Rectangle.Empty
            JawPanel.Invalidate()
        End If
    End Sub

    Private Sub JawPanel_Paint(sender As Object, e As PaintEventArgs) Handles JawPanel.Paint
        If isSelecting AndAlso Not selectionRect.IsEmpty Then
            Using pen As New Pen(Color.Blue, 1) With {.DashStyle = Drawing2D.DashStyle.Dash}
                e.Graphics.DrawRectangle(pen, selectionRect)
            End Using
        End If
    End Sub

    Private Sub SelectControlsInRectangle(rect As Rectangle)
        ClearSvgSelection()
        Dim processedTeeth As New HashSet(Of Integer)

        For Each ctrl As Control In JawPanel.Controls
            If TypeOf ctrl Is SvgImageBox AndAlso rect.IntersectsWith(ctrl.Bounds) Then
                Dim toothNum As Integer = Convert.ToByte(ctrl.Tag)
                If Not processedTeeth.Contains(toothNum) Then
                    processedTeeth.Add(toothNum)
                    Dim outView As SvgImageBox = FindOutView(toothNum)
                    If outView IsNot Nothing Then
                        SelectSvg(outView)
                    End If
                End If
            End If
        Next
    End Sub
#End Region

#Region "Range Selection"
    Private Sub SvgImageBox_Click(sender As Object, e As EventArgs) 'Handles _
        'RuOut1.Click, RuOut2.Click, RuOut3.Click ' Add all other click handlers

        Dim clickedSvg As SvgImageBox = DirectCast(sender, SvgImageBox)
        Dim toothNum As Byte = Convert.ToByte(clickedSvg.Tag)

        ' Always find the Out view for selection
        Dim outView As SvgImageBox = FindOutView(toothNum)
        If outView Is Nothing Then Return

        If Control.ModifierKeys = Keys.Control Then
            ' Ctrl+Click behavior
            If firstSelectedTooth = -1 Then
                ' First tooth in potential range selection
                firstSelectedTooth = toothNum
                If Not selectedTeethList.Contains(toothNum) Then
                    SelectSvg(outView)
                End If
            Else
                ' Second click with Ctrl - perform range selection
                SelectToothRange(firstSelectedTooth, toothNum)
                firstSelectedTooth = -1
            End If
        Else
            ' Normal click behavior (without Ctrl)
            firstSelectedTooth = -1 ' Reset range selection
            If selectedTeethList.Contains(toothNum) Then
                ' Deselect if already selected
                DeselectAllViewsOfTooth(clickedSvg, toothNum)
            Else
                ' Select single tooth
                ClearSvgSelection()
                SelectSvg(outView)
            End If
        End If
    End Sub

    Private Sub SelectToothRange(startTooth As Integer, endTooth As Integer)
        ClearSvgSelection()

        Dim startIndex As Integer = Array.IndexOf(PhysicalToothOrder, startTooth)
        Dim endIndex As Integer = Array.IndexOf(PhysicalToothOrder, endTooth)

        If startIndex = -1 OrElse endIndex = -1 Then Return

        Dim stepValue As Integer = If(startIndex < endIndex, 1, -1)

        For i As Integer = startIndex To endIndex Step stepValue
            Dim toothNum As Integer = PhysicalToothOrder(i)
            If IsValidToothNumber(toothNum) Then
                Dim outView As SvgImageBox = FindOutView(toothNum)
                If outView IsNot Nothing Then
                    SelectSvg(outView)
                End If
            End If
        Next
    End Sub
#End Region

#Region "Helper Methods"

    Private Function FindOutView(toothNumber As Integer) As SvgImageBox
        For Each ctrl As Control In JawPanel.Controls
            If TypeOf ctrl Is SvgImageBox AndAlso
               ctrl.Name.Contains("OUT") AndAlso
               Convert.ToByte(ctrl.Tag) = toothNumber Then
                Return DirectCast(ctrl, SvgImageBox)
            End If
        Next
        Return Nothing
    End Function
    Private Function FindOutView(topSvgName As String) As SvgImageBox
        Dim outName As String = ""
        If topSvgName.Contains("OUT") Then
            outName = topSvgName
        ElseIf topSvgName.Contains("TOP") Then
            outName = topSvgName.Replace("TOP", "OUT")
        Else
            Return Nothing
        End If


        Dim ctrl As Control = JawPanel.Controls(outName)
        If TypeOf ctrl Is SvgImageBox Then
            Return DirectCast(ctrl, SvgImageBox)
        End If

        Return Nothing
    End Function

    Private Function IsValidToothNumber(toothNumber As Integer) As Boolean
        Return (toothNumber >= 11 AndAlso toothNumber <= 18) OrElse
               (toothNumber >= 21 AndAlso toothNumber <= 28) OrElse
               (toothNumber >= 31 AndAlso toothNumber <= 38) OrElse
               (toothNumber >= 41 AndAlso toothNumber <= 48)
    End Function
#End Region

#End Region

#End Region




    '============================================================================================================

#Region "Rotate"

    Private Sub SvgImageBox_Paint(sender As Object, e As PaintEventArgs)
        Dim svgControl As SvgImageBox = DirectCast(sender, SvgImageBox)

        Dim rotationAngle As Single = 0
        If svgControl.Text IsNot Nothing AndAlso Single.TryParse(CSng(Val(svgControl.Text.ToString())), rotationAngle) Then

            ' Save the current graphics state
            Dim state As Drawing2D.GraphicsState = e.Graphics.Save()

            ' Create rotation around center
            Dim center As New PointF(svgControl.Width / 2, svgControl.Height / 2)
            e.Graphics.TranslateTransform(center.X, center.Y)
            e.Graphics.RotateTransform(rotationAngle)
            e.Graphics.TranslateTransform(-center.X, -center.Y)

            ' Draw the image
            If svgControl.SvgImage IsNot Nothing Then
                Dim s As Size = New Size(svgControl.SvgImage.Width, svgControl.SvgImage.Height)
                ' Retrieve a color palette
                Dim palette = SvgPaletteHelper.GetSvgPalette(Me.LookAndFeel, ObjectState.Normal)
                svgControl.SvgImage.Render(s, palette)
                'svgControl.SvgImage.RenderToGraphics(e.Graphics, SvgPaletteHelper.GetSvgPalette(LookAndFeel, ObjectState.Normal))
            End If

            '' Restore the original graphics state
            'e.Graphics.Restore(state)


        End If
    End Sub


    Public Sub RotateSvgImageBox(ByVal controlName As String, ByVal angleDelta As Single)
        Dim svgControl As SvgImageBox = JawPanel.Controls.OfType(Of SvgImageBox)().FirstOrDefault(Function(c) c.Name = controlName)

        If svgControl Is Nothing OrElse svgControl.SvgImage Is Nothing Then Exit Sub
        Dim currentAngle As Single = If(svgControl.Text IsNot Nothing, CSng(Val(svgControl.Text)), 0)
        Dim newAngle As Single = currentAngle + angleDelta
        Dim center As New PointF(svgControl.Width / 2.0F, svgControl.Height / 2.0F)
        svgControl.Text = newAngle
        Dim x As Single = svgControl.SvgImage.Width '.Root.ViewBox.Width
        Dim y As Single = svgControl.SvgImage.Height '.Root.ViewBox.Height
        Dim bitmap = New SvgBitmap(svgControl.SvgImage)
        'bitmap.SvgImage.Render(Nothing, 1)
        center = New PointF(x / 2, y / 2)
        ' Step 1: Save visibility states by ID
        Dim visibilityMap As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
        For Each item As SvgImageItem In svgControl.RootItems
            '
            If Not String.IsNullOrEmpty(item.Id) Then
                visibilityMap(item.Id) = item.Visible
            End If
        Next
        'svgControl.SvgImage.
        ''Step 2 Calculate New rotation angle
        'Dim currentAngle As Single = If(svgControl.Text IsNot Nothing, CSng(Val(svgControl.Text)), 0)
        'Dim newAngle As Single = currentAngle + angleDelta
        'svgControl.Text = newAngle
        'Dim item As SvgImageItem
        'item = svgControl.RootItems.Find(Function(x) x.Id = "CON")
        'item.Transform.RotateAt(newAngle, center)
        ' Step 3: Apply transformation
        Using matrix As New Drawing2D.Matrix()
            'matrix.Rotate(newAngle)
            matrix.RotateAt(newAngle, center)
            svgControl.SvgImage = bitmap.SvgImage.SetTransform(matrix)
            'svgControl.RootItems.Find(Function(x) x.Id = "CON").Visible = True
            'svgControl.RootItems.Find(Function(x) x.Id = "CON").Transform.RotateAt(newAngle, center)
            'svgControl.RootItems.Find(Function(x) x.Id = "CON").Transform.Translate(-10, -10)
        End Using

        ' Step 4: Restore visibility by matching ID
        For Each item As SvgImageItem In svgControl.RootItems
            If Not String.IsNullOrEmpty(item.Id) AndAlso visibilityMap.ContainsKey(item.Id) Then
                item.Visible = visibilityMap(item.Id)
            End If
        Next

        ' Optional: Refresh to reflect restored visibility
        svgControl.Refresh()
    End Sub

    Private Sub UpdateStyle(ByVal element As SvgElement, ByVal hashtable As Hashtable)
        If element.ToString() = "DevExpress.Utils.Svg.SvgPath" Then
            hashtable(If(True, "Stroke", "Fill")) = "#660000"

        End If
    End Sub

    Public Sub RotateSvgImageBox1(ByVal controlName As String, ByVal angleDelta As Single)
        Dim svgControl As SvgImageBox = JawPanel.Controls.OfType(Of SvgImageBox)().FirstOrDefault(Function(c) c.Name = controlName)

        If svgControl IsNot Nothing AndAlso svgControl.SvgImage IsNot Nothing Then
            ' Get current rotation from Tag property
            Dim currentAngle As Single = If(svgControl.Text IsNot Nothing, CSng(Val(svgControl.Text)), 0)
            Dim newAngle As Single = currentAngle + angleDelta

            ' Store new angle in Tag
            svgControl.Text = newAngle

            Using matrix As New Drawing2D.Matrix()
                Dim center As New PointF(svgControl.Size.Width / 2, svgControl.Size.Height / 2)
                matrix.RotateAt(newAngle, center)
                svgControl.SvgImage = svgControl.SvgImage.SetTransform(matrix)
            End Using
        End If
    End Sub

    Public Sub ResetSvgRotation(ByVal controlName As String)
        Dim svgControl As SvgImageBox = JawPanel.Controls.OfType(Of SvgImageBox)().FirstOrDefault(Function(c) c.Name = controlName)

        If svgControl IsNot Nothing AndAlso svgControl.SvgImage IsNot Nothing Then
            svgControl.Text = "0"

            Using matrix As New Drawing2D.Matrix()
                svgControl.SvgImage = svgControl.SvgImage.SetTransform(matrix)
            End Using
        End If
    End Sub

    Private Sub SvgControl_MouseWheel(sender As Object, e As MouseEventArgs)
        Dim svg = TryCast(sender, SvgImageBox)
        If svg Is Nothing Then Exit Sub

        Dim delta As Integer = e.Delta / 120 ' One tick is 120
        RotateSvgImageBox(svg.Name, delta * 5)
        svg.Invalidate()
    End Sub

    '==========================
    'Code for smooth
    Private currentAngle As Single = 0
    Private WithEvents rotTimer As New Timer With {.Interval = 15}
    Private targetControl As SvgImageBox
    Private targetAngle As Single = 0

    Private Sub StartSmoothRotation(control As SvgImageBox, angleDelta As Single)
        targetControl = control
        currentAngle = CSng(Val(control.Text))
        targetAngle = currentAngle + angleDelta
        rotTimer.Start()
    End Sub

    Private Sub rotTimer_Tick(sender As Object, e As EventArgs) Handles rotTimer.Tick
        If targetControl Is Nothing Then rotTimer.Stop() : Return

        currentAngle += 3
        If currentAngle >= targetAngle Then
            currentAngle = targetAngle
            rotTimer.Stop()
        End If

        targetControl.Text = currentAngle.ToString()
        Using matrix As New Drawing2D.Matrix()
            Dim center As New PointF(targetControl.Width / 2, targetControl.Height / 2)
            matrix.RotateAt(currentAngle, center)
            targetControl.SvgImage = targetControl.SvgImage.SetTransform(matrix)
        End Using
    End Sub

    ''==============================
    ''label rotate
    'Private Sub Label_MouseWheel(sender As Object, e As MouseEventArgs)
    '    Dim lbl As LabelControl = DirectCast(sender, LabelControl)
    '    Dim angle As Single = CSng(Val(lbl.Tag))
    '    angle += Math.Sign(e.Delta) * 15
    '    lbl.Tag = angle
    '    lbl.Invalidate()
    'End Sub


    'Private Sub Label_Paint(sender As Object, e As PaintEventArgs)
    '    Dim lbl As LabelControl = DirectCast(sender, LabelControl)
    '    Dim angle As Single = CSng(Val(lbl.Tag))
    '    Dim g As Graphics = e.Graphics
    '    Dim center As New PointF(lbl.Width / 2, lbl.Height / 2)

    '    g.TranslateTransform(center.X, center.Y)
    '    g.RotateTransform(angle)
    '    g.TranslateTransform(-center.X, -center.Y)

    '    TextRenderer.DrawText(g, lbl.Text, lbl.Font, lbl.ClientRectangle, lbl.ForeColor,
    '                      TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
    'End Sub


#End Region

#Region "TrtsList and TrtsTreeView"

    Private originalTrtsTable As DataTable
    Private originalAddedTrtsTable As DataTable
    Private mouseDownLocation As Point
    Private mouseDownTime As DateTime

#Region "TreeViewSource SUBS"


#Region "New TreeViewSource"


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

    Private Function GetApplicableTreatments(selectedTeeth As List(Of Integer), allTreatments As List(Of TblTRTS)) As List(Of TblTRTS)
        ' Start with all treatments
        Dim result As IEnumerable(Of TblTRTS) = allTreatments

        ' For each selected tooth, filter down the treatments
        For Each tooth In selectedTeeth
            result = result.Where(Function(t) _
            t.ToothID = "ALL" OrElse
            t.ToothID.Split(","c).Contains(tooth.ToString()))
        Next

        Return result.ToList()
    End Function




    ' Value-type for node info (safe to store as Tag)

    ' In-memory master list (full set for the mobile page)
    Private AllTrtNodes As New List(Of TrtSourceHelper.TrtNodeInfo)()

    Private Sub SetTrtsTreeMultiTeeth()
        JawTreatmentTreeHelper.LoadTreatTreeMulti(TrtsTreeView, selectedTeethList.Select(Function(t) CInt(t)).ToList(), PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=True)
    End Sub



    Private Sub SetTreeTreatsSingleTooth(toothID As String, toothNum As Byte)
        JawTreatmentTreeHelper.LoadTreatTreeSingle(TrtsTreeView, toothID, toothNum, PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=True)
    End Sub

    Private Sub BuildTreeFromList(nodeList As List(Of TrtSourceHelper.TrtNodeInfo), Optional existingTrts As HashSet(Of Integer) = Nothing, Optional emptyMessage As String = "No available diagnoses for current tooth status")
        TrtSourceHelper.BuildTreeFromList(TrtsTreeView, nodeList, existingTrts, emptyMessage, "DIAGNOSES", Color.DarkRed)
    End Sub

    Private Sub BuildTreeFromListLastOk(nodeList As List(Of TrtSourceHelper.TrtNodeInfo), Optional existingTrts As HashSet(Of Integer) = Nothing)
        TrtsTreeView.BeginUpdate()
        Try
            TrtsTreeView.Nodes.Clear()
            Dim groupNodes As New Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)
            Dim parentNodes As New Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)

            For Each Inf In nodeList
                Dim grp = If(Inf.TrtGroup, "").ToUpperInvariant()
                Dim parent = If(Inf.ParentGroup, "")

                ' Handle IMPLANT parent group with special sub-groups
                If parent.ToUpperInvariant() = "IMPLANT" Then
                    ' For IMPLANT parent, show treatments under BOTH Immediate and Surgical
                    Dim subGroups As New List(Of String)

                    ' Check which sub-groups this treatment belongs to
                    If grp.Contains("IMMEDIATE") Or Inf.Trt.ToUpperInvariant().Contains("IMMEDIATE") Then
                        subGroups.Add("IMMEDIATE")
                    End If
                    If grp.Contains("SURGICAL") Or Inf.Trt.ToUpperInvariant().Contains("SURGICAL") Or Not subGroups.Any() Then
                        subGroups.Add("SURGICAL")
                    End If

                    ' Ensure IMPLANT parent node exists
                    If Not parentNodes.ContainsKey(parent) Then
                        Dim pnode As New TreeNode(parent) With {
                        .Name = parent,
                        .ForeColor = Color.DarkBlue,
                        .Tag = "PARENT_GROUP"
                    }
                        parentNodes.Add(parent, pnode)
                    End If

                    ' Create treatment node once (we'll add it to multiple sub-groups)
                    Dim tnode As New TreeNode(Inf.Trt) With {
                    .ForeColor = Color.Green,
                    .Tag = Inf
                }

                    ' 🔹 Highlight if already applied
                    If existingTrts IsNot Nothing AndAlso existingTrts.Contains(Inf.TrtID) Then
                        tnode.ForeColor = Color.Red
                        tnode.NodeFont = New Font(TrtsTreeView.Font, FontStyle.Strikeout)
                    End If

                    ' Add treatment to each relevant sub-group
                    For Each subGroup In subGroups
                        Dim subGroupKey = parent & "|" & subGroup

                        ' Ensure sub-group node exists under IMPLANT
                        If Not groupNodes.ContainsKey(subGroupKey) Then
                            Dim gnode As New TreeNode(subGroup) With {
                            .Name = subGroup,
                            .ForeColor = Color.Blue,
                            .Tag = "GROUP"
                        }
                            groupNodes.Add(subGroupKey, gnode)

                            ' Add sub-group to IMPLANT parent
                            parentNodes(parent).Nodes.Add(gnode)
                        End If

                        ' Clone the treatment node for this sub-group
                        Dim clonedTnode As New TreeNode(Inf.Trt) With {
                        .ForeColor = tnode.ForeColor,
                        .NodeFont = tnode.NodeFont,
                        .Tag = Inf
                    }

                        groupNodes(subGroupKey).Nodes.Add(clonedTnode)
                    Next

                Else
                    ' Original logic for non-IMPLANT groups
                    ' Ensure parent group node (if any)
                    If Not String.IsNullOrEmpty(parent) Then
                        If Not parentNodes.ContainsKey(parent) Then
                            Dim pnode As New TreeNode(parent) With {
                            .Name = parent,
                            .ForeColor = Color.DarkBlue,
                            .Tag = "PARENT_GROUP"
                        }
                            parentNodes.Add(parent, pnode)
                        End If
                    End If

                    ' Ensure group node
                    If Not groupNodes.ContainsKey(grp) Then
                        Dim gnode As New TreeNode(grp) With {
                        .Name = grp,
                        .ForeColor = Color.Blue,
                        .Tag = "GROUP"
                    }
                        groupNodes.Add(grp, gnode)
                    End If

                    ' Treatment node
                    Dim tnode As New TreeNode(Inf.Trt) With {
                    .ForeColor = Color.Green,
                    .Tag = Inf
                }

                    ' 🔹 Highlight if already applied
                    If existingTrts IsNot Nothing AndAlso existingTrts.Contains(Inf.TrtID) Then
                        tnode.ForeColor = Color.Red
                        tnode.NodeFont = New Font(TrtsTreeView.Font, FontStyle.Strikeout)
                    End If

                    ' Attach nodes: if has parent, attach group under parent (once) then treatment under group
                    If Not String.IsNullOrEmpty(parent) Then
                        Dim pnode = parentNodes(parent)
                        ' add group under parent if not already added
                        If Not pnode.Nodes.ContainsKey(grp) Then
                            pnode.Nodes.Add(groupNodes(grp))
                        End If
                        groupNodes(grp).Nodes.Add(tnode)
                    Else
                        groupNodes(grp).Nodes.Add(tnode)
                    End If
                End If
            Next

            ' Add all top-level parent nodes to TreeView
            For Each kvp In parentNodes
                If kvp.Value.Parent Is Nothing Then
                    TrtsTreeView.Nodes.Add(kvp.Value)
                End If
            Next

            ' Add any groups that don't have parents
            For Each kvp In groupNodes
                If kvp.Value.Parent Is Nothing Then
                    TrtsTreeView.Nodes.Add(kvp.Value)
                End If
            Next

            ' collapse parent groups if you want them collapsed by default
            For Each n As TreeNode In TrtsTreeView.Nodes
                If n.Tag IsNot Nothing AndAlso n.Tag.ToString() = "PARENT_GROUP" Then
                    n.Collapse()
                End If
            Next

            ' If empty, show info
            If TrtsTreeView.Nodes.Count = 0 Then
                TrtsTreeView.Nodes.Add(New TreeNode("No available treatments for current tooth status") With {.ForeColor = Color.Gray, .Tag = "INFO_NODE"})
            End If
        Finally
            TrtsTreeView.EndUpdate()
        End Try
    End Sub

    ' -------------------------
    ' Clone / Save / Restore helpers
    ' -------------------------

    ' Snapshot storage for quick restore
    '================================
    Private fullTreeSnapshot As List(Of TreeNode)
    Private Sub SaveFullTreeSnapshot()
        fullTreeSnapshot = TrtSourceHelper.SaveFullTreeSnapshot(TrtsTreeView)
    End Sub

    Private Sub ApplyTreeFilter(filterText As String)
        TrtSourceHelper.ApplyTreeFilter(TrtsTreeView, fullTreeSnapshot, filterText)
    End Sub

    Public Sub ApplyTreeFilter()
        ' Recompute selection flags from the current selectedTeethList / PatientID
        Dim hasUpperTeeth = selectedTeethList.Any(Function(t) t >= 11 AndAlso t <= 28)
        Dim hasLowerTeeth = selectedTeethList.Any(Function(t) t >= 31 AndAlso t <= 48)
        Dim isSingleJaw = Not (hasUpperTeeth AndAlso hasLowerTeeth)
        Dim is16Teeth = (selectedTeethList.Count = 16)

        Dim allLevels As New List(Of Byte)
        For Each ToothNum In selectedTeethList
            Dim lvls = clsToothTrtData.GetTreatLVLs(PatientID, ToothNum)
            For Each l In lvls
                If Not allLevels.Contains(l) Then allLevels.Add(l)
            Next
        Next

        Dim norTrt = allLevels.Any(Function(l) l < 4)
        Dim exTrt = allLevels.Contains(4)
        Dim impTrt = allLevels.Any(Function(l) l >= 5 AndAlso l <= 7)

        ' Decide allowed groups and dentures types per your rules
        Dim allowedGroups As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim allowRPD As Boolean = False
        Dim allowCD As Boolean = False

        If isSingleJaw Then
            ' Single jaw exact rules
            If norTrt AndAlso Not exTrt AndAlso Not impTrt Then
                allowedGroups.UnionWith({"EXTRACTION", "IMPLANT", "DENTURES"})
                allowRPD = True : allowCD = True
            ElseIf norTrt AndAlso exTrt AndAlso Not impTrt Then
                ' skip extraction
                allowedGroups.UnionWith({"IMPLANT", "DENTURES"})
                allowRPD = True : allowCD = True
            ElseIf norTrt AndAlso impTrt AndAlso Not exTrt Then
                ' skip implant
                allowedGroups.UnionWith({"EXTRACTION", "DENTURES"})
                allowRPD = True : allowCD = True
            ElseIf norTrt AndAlso exTrt AndAlso impTrt Then
                ' skip extraction and implant -> only dentures
                allowedGroups.Add("DENTURES")
                allowRPD = True : allowCD = True
            Else
                ' default (other combos): show mobile groups
                allowedGroups.UnionWith({"EXTRACTION", "IMPLANT", "DENTURES"})
                allowRPD = True : allowCD = True
            End If

            ' if 16 teeth, always skip RPD as you requested
            If is16Teeth Then allowRPD = False
        Else
            ' Two jaws: follow same "skip" rules but DO NOT show any dentures (RPD or CD)
            If norTrt AndAlso Not exTrt AndAlso Not impTrt Then
                allowedGroups.UnionWith({"EXTRACTION", "IMPLANT"})
            ElseIf norTrt AndAlso exTrt AndAlso Not impTrt Then
                ' skip extraction -> implant only
                allowedGroups.Add("IMPLANT")
            ElseIf norTrt AndAlso impTrt AndAlso Not exTrt Then
                ' skip implant -> extraction only
                allowedGroups.Add("EXTRACTION")
            ElseIf norTrt AndAlso exTrt AndAlso impTrt Then
                ' skip extraction and implant (then no dentures allowed on two jaws) => nothing allowed
                allowedGroups.Clear()
            Else
                ' default: show only extra
            End If
        End If
    End Sub

#End Region

    Private Sub SetAddedTrtsListDataSource(patientID As Integer, toothNum As Byte, toothName As String)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        AddedTrtsList.DataSource = Nothing
        AddedTrtsList.DisplayMember = Nothing     ' Clear the display member
        AddedTrtsList.ValueMember = Nothing       ' Clear the value member

        ' Define the SQL query with concatenation for Treat and TreatDate
        Dim query As String = "
                                SELECT DiagID, 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyy') + ')' AS DisplayTreat
                                FROM Patient_Diagnosis 
                                WHERE PatientID = @PatientID AND ToothNum = @ToothNum AND ToothName = @ToothName
                                Order By TreatDate Desc"

        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@PatientID", patientID)
            command.Parameters.AddWithValue("@ToothNum", toothNum)
            command.Parameters.AddWithValue("@ToothName", toothName)

            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Set the data source for the list box
            AddedTrtsList.DataSource = dataTable
            AddedTrtsList.DisplayMember = "DisplayTreat"  ' Set the column to display Treat and TreatDate
            AddedTrtsList.ValueMember = "DiagID"           ' Set the ID column as the value member
        End Using
    End Sub

    Private Sub FilterTrtsTreeView(searchText As String)
        TrtSourceHelper.FilterTrtsTreeView(TrtsTreeView, searchText, Module1.isKid)
    End Sub

    Private Sub FilterAddedTrtsListBox(searchText As String)
        TrtSourceHelper.FilterAddedTrtsListBox(AddedTrtsList, originalAddedTrtsTable, searchText)
    End Sub

    Private Sub txtSrchTrt_TextChanged(sender As Object, e As EventArgs) Handles txtSrchTrt.TextChanged
        Dim searchText As String = txtSrchTrt.Text.Trim().ToLower()
        'Dim searchText As String = txtSrchTrt.Text.Trim()
        ApplyTreeFilter(searchText)
        'FilterTrtsTreeView(searchText)
        FilterAddedTrtsListBox(searchText)
    End Sub

#End Region
#Region "Mob TreeViewSource SUBS"


#Region "New Mob TreeViewSource"
    ' Value-type for node info (safe to store as Tag)
    'Private Structure TrtNodeInfo
    '    Public MobID As Integer
    '    Public Trt As String
    '    Public TrtGroup As String
    '    Public ParentGroup As String
    '    Public OldTrt As String
    '    Public ShapeID As Integer
    '    Public ShapeName As String
    '    Public TrtColor As String
    '    Public BorderColor As String
    '    Public BorderThick As Byte
    'End Structure

    ' In-memory master list (full set for the mobile page)
    'Private AllTrtNodes As New List(Of TrtNodeInfo)()

    ' -------------------------
    ' Main loader (mobile page) - ONLY extraction, implant, dentures
    ' -------------------------
    Private Sub SetMobTreeMultiTeeth()
        JawTreatmentTreeHelper.LoadMobileTreeMulti(TrtsTreeView, selectedTeethList.Select(Function(t) CInt(t)).ToList(), PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=True)
    End Sub


    Private Sub SetMobTreeSnglTooth(toothID As String, toothNum As Byte)
        JawTreatmentTreeHelper.LoadMobileTreeSingle(TrtsTreeView, toothID, toothNum, PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=True)
    End Sub


    ' -------------------------


#End Region

    Private Sub SetAddedMobsListDataSource(patientID As Integer, toothNum As Byte, toothName As String)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        AddedTrtsList.DataSource = Nothing
        AddedTrtsList.DisplayMember = Nothing     ' Clear the display member
        AddedTrtsList.ValueMember = Nothing       ' Clear the value member

        ' Define the SQL query with concatenation for Treat and TreatDate
        Dim query As String = "
                                SELECT DiagID, 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyy') + ')' AS DisplayTreat
                                FROM Patient_Diagnosis 
                                WHERE [dbo].[Patient_Diagnosis].LVL = 9 And PatientID = @PatientID AND ToothNum = @ToothNum AND ToothName = @ToothName
                                Order By TreatDate Desc"

        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@PatientID", patientID)
            command.Parameters.AddWithValue("@ToothNum", toothNum)
            command.Parameters.AddWithValue("@ToothName", toothName)

            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Set the data source for the list box
            AddedTrtsList.DataSource = dataTable
            AddedTrtsList.DisplayMember = "DisplayTreat"  ' Set the column to display Treat and TreatDate
            AddedTrtsList.ValueMember = "DiagID"           ' Set the ID column as the value member
        End Using
    End Sub

    Private Sub FilterMobsTreeView(searchText As String)
        ' Rebuild the TreeView with filtered data
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim query1 As String = "SELECT TrtGroup, TrtID, Trt, ToothID FROM TblTRTS 
                                WHERE ToothID = 'ALL' OR CHARINDEX(',' + @ToothID + ',', ',' + ToothID + ',') = 0 ORDER BY TrtGroup, Trt"
        Dim query As String = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], " &
                    "[TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails], " &
                    "[TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt], " &
                    "[TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] " &
                    "FROM [TblTRTS] LEFT JOIN [Shapes] ON [TblTRTS].[ShapeID] = [Shapes].[ShapeID] " &
                    "WHERE ([KidTrt] IN (0,  2)) " &
                    "AND ([ToothID] = 'ALL' OR @ToothID = 'ALL' " &
                    "OR CHARINDEX(',' + @ToothID + ',', ',' + [ToothID] + ',') > 0 " &
                    "OR CHARINDEX(',' + [ToothID] + ',', ',' + @ToothID + ',') > 0) " &
                    "AND  ([TrtGroup] IN ('EXTRACTION', 'IMPLANT', 'DENTURES'))"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", "ALL") ' Use appropriate ToothID if available
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Clear the TreeView
            TrtsTreeView.Nodes.Clear()

            ' Group treatments by TrtGroup
            Dim groups = dataTable.AsEnumerable().
            Where(Function(row) row.Field(Of String)("Trt").ToLower().Contains(searchText)).
            GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            ' Add groups and treatments to TreeView
            For Each group In groups
                Dim groupNode As New TreeNode(group.Key)
                For Each treatment In group
                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                    .Tag = treatment.Field(Of Integer)("TrtID")
                }
                    groupNode.Nodes.Add(treatmentNode)
                Next
                TrtsTreeView.Nodes.Add(groupNode)
            Next
        End Using

        For Each groupNode As TreeNode In TrtsTreeView.Nodes
            Dim groupVisible As Boolean = False ' Track visibility for the parent node
            For Each treatmentNode As TreeNode In groupNode.Nodes
                If treatmentNode.Text.ToLower().Contains(searchText.ToLower()) Then
                    treatmentNode.ForeColor = Color.Red ' Highlight matching child node in red
                    treatmentNode.BackColor = Color.LightYellow ' Optional: Background highlight
                    treatmentNode.EnsureVisible() ' Ensure visibility of the matching node
                    groupVisible = True
                Else
                    treatmentNode.ForeColor = Color.Green ' Non-matching child nodes in green
                    treatmentNode.BackColor = Color.Transparent
                End If
            Next

            If groupVisible Then
                groupNode.ForeColor = Color.Blue ' Parent node in blue if any child is visible
                groupNode.BackColor = Color.Transparent
                groupNode.Expand() ' Expand parent if matches are found in children
            Else
                groupNode.ForeColor = Color.Transparent ' Hide parent node if no matching children
                groupNode.BackColor = Color.Transparent
            End If
        Next
    End Sub

    Private Sub FilterAddedMobsListBox(searchText As String)
        ' Assuming dataTable is the data source for TrtsList
        ' Ensure the original table is available
        If originalAddedTrtsTable Is Nothing Then
            If AddedTrtsList.DataSource IsNot Nothing Then
                originalAddedTrtsTable = DirectCast(AddedTrtsList.DataSource, DataTable).Copy() ' Make a copy to preserve the original
            End If
        End If

        ' Filter TrtsList
        If originalAddedTrtsTable IsNot Nothing Then
            Dim filteredRows = originalAddedTrtsTable.AsEnumerable().Where(Function(row) _
            Not IsDBNull(row("DisplayTreat")) AndAlso row.Field(Of String)("DisplayTreat").ToLower().Contains(searchText))
            'OrElse
            'Not IsDBNull(row("TrtAr")) AndAlso row.Field(Of String)("TrtAr").ToLower().Contains(searchText))

            If searchText = "" Then
                ' If the search box is empty, reset the DataSource to the original table
                AddedTrtsList.DataSource = originalAddedTrtsTable.Copy()
            ElseIf filteredRows.Any() Then
                ' Apply the filter
                AddedTrtsList.DataSource = filteredRows.CopyToDataTable()
            Else
                ' No matching rows
                AddedTrtsList.DataSource = Nothing
            End If
        End If
    End Sub



#End Region


#Region "eNTER KEY AND dOUBLE CLICK AddedTrts AND TreeView Events"
    Private Sub CommonDragDropHandler(sender As Object, e As DragEventArgs)
        Dim dropTarget As SvgImageBox = DirectCast(sender, SvgImageBox)

        ' Restrict to correct drop source (if you still want this rule)
        If svgSelectedList Is Nothing OrElse svgSelectedList.Count = 0 Then
            If dropTarget IsNot DragSource Then
                MessageBox.Show("You can only drop treatments on the same selected tooth that initiated the drag.", "Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        ' Get treatment data from drag
        If Not e.Data.GetDataPresent(DragDropConstants.CustomFormat) Then
            MessageBox.Show("Invalid treatment data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim dragData As DragDropData = DirectCast(e.Data.GetData(DragDropConstants.CustomFormat), DragDropData)

        ' Build target list: multi-selection or just the drop target
        Dim targets As New List(Of SvgImageBox)
        If svgSelectedList IsNot Nothing AndAlso svgSelectedList.Count > 0 Then
            targets.AddRange(svgSelectedList)
        Else
            targets.Add(dropTarget)
        End If

        Dim toothTrtList As New List(Of Patient_Diagnosis)
        For Each svgBox In targets
            Dim trt = CreateToothTrtFromDragData(svgBox, dragData)
            If trt IsNot Nothing Then
                toothTrtList.Add(trt)
            End If
        Next

        If toothTrtList.Count = 0 Then
            MessageBox.Show("No valid tooth targets found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Show Add form for single or multiple
        'CLOSE flyout1
        Flyout1.HidePopup()
        Dim result As DialogResult
        If toothTrtList.Count = 1 Then
            Using addTrt As New AddNewDiagForm(toothTrtList(0), clsPatient)
                result = addTrt.ShowDialog(Me)
                Me.Focus()
            End Using
        Else
            Using addTrt As New AddNewDiagForm(toothTrtList, clsPatient)
                result = addTrt.ShowDialog(Me)
                Me.Focus()
            End Using
        End If
        Me.Focus()
        If result = DialogResult.OK Then
            LoadPatientTreats(PatientID)
            Me.Focus()
        Else
            Me.Focus()
            MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Me.Focus()
        DragSource = Nothing
    End Sub

    Private Sub AddedTrtsList_KeyDown(sender As Object, e As KeyEventArgs) Handles AddedTrtsList.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Ensure an item is selected
            If AddedTrtsList.SelectedItem Is Nothing Then
                MessageBox.Show("Please select an item from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Cast the selected item to a DataRowView
            Dim selectedRow As DataRowView = TryCast(AddedTrtsList.SelectedItem, DataRowView)
            If selectedRow IsNot Nothing Then
                ' Retrieve the Treat and TrtID values
                Dim DiagID As Integer = Convert.ToInt32(selectedRow("DiagID"))
                'Dim treat As String = Convert.ToString(selectedRow("DisplayTreat"))
                'Dim x As Integer = treat.IndexOf("-")
                'treat = treat.Remove(x)
                '' Display the values or use them as needed
                ''MessageBox.Show($"Treatment: {treat}{Environment.NewLine}TrtID: {trtID}", "Treatment Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim trtData As New Patient_DiagnosisDATA
                Dim toothTrt As New Patient_Diagnosis With {.DiagID = DiagID, .PatientID = PatientID, .ToothNum = CByte(slctdSVG.Tag)}
                toothTrt = trtData.Select_RecordByDiagIDByTNum(toothTrt)
                'CLOSE flyout1
                Flyout1.HidePopup()
                Dim result As DialogResult
                Using editTrt As New EditDiagFrom(toothTrt, clsPatient)
                    result = editTrt.ShowDialog(Me)

                    Select Case result
                        Case DialogResult.OK
                            If editTrt.Saved Then
                                'MessageBox.Show("Saved And Existed.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                LoadPatientTreats(PatientID)
                                FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                                Me.Focus()
                            ElseIf editTrt.Deleted Then
                                'MessageBox.Show("Deleted And Existed.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                LoadPatientTreats(PatientID)
                                FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                                Me.Focus()
                            End If

                        Case DialogResult.Cancel
                            Me.Focus()
                            MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
                End Using
                Me.Focus()
            Else
                MessageBox.Show("Failed to retrieve treatment details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
    Private Sub AddedTrtsList_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AddedTrtsList.MouseDoubleClick
        ' Ensure an item is selected
        If AddedTrtsList.SelectedItem Is Nothing Then
            MessageBox.Show("Please select an item from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Cast the selected item to a DataRowView
        Dim selectedRow As DataRowView = TryCast(AddedTrtsList.SelectedItem, DataRowView)
        If selectedRow IsNot Nothing Then
            ' Retrieve the Treat and TrtID values
            Dim DiagID As Integer = Convert.ToInt32(selectedRow("DiagID"))
            Dim trtData As New Patient_DiagnosisDATA
            Dim toothTrt As New Patient_Diagnosis With {.DiagID = DiagID, .PatientID = PatientID, .ToothNum = CByte(slctdSVG.Tag)}
            toothTrt = trtData.Select_RecordByDiagIDByTNum(toothTrt)
            'CLOSE flyout1
            Flyout1.HidePopup()
            Dim result As DialogResult
            Using editTrt As New EditDiagFrom(toothTrt, clsPatient)
                result = editTrt.ShowDialog(Me)

                Select Case result
                    Case DialogResult.OK
                        If editTrt.Saved Then
                            'MessageBox.Show("Saved And Existed.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            LoadPatientTreats(PatientID)
                            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                            Me.Focus()
                        ElseIf editTrt.Deleted Then
                            'MessageBox.Show("Deleted And Existed.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            LoadPatientTreats(PatientID)
                            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                            Me.Focus()
                        End If

                    Case DialogResult.Cancel
                        Me.Focus()
                        MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select
            End Using
            Me.Focus()
        Else
            MessageBox.Show("Failed to retrieve treatment details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Private Sub TreeView_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TrtsTreeView.NodeMouseDoubleClick
        ' Basic validation
        If e.Node Is Nothing OrElse e.Node.Parent Is Nothing OrElse e.Node.Nodes.Count > 0 OrElse e.Node.Tag Is Nothing OrElse DragSource Is Nothing Then Exit Sub

        Dim treatmentText As String = e.Node.Text.Trim()

        If String.IsNullOrEmpty(treatmentText) Then Exit Sub

        ' Collect target SVGs (single or multiple selection)
        Dim targetSVGs As List(Of SvgImageBox) = If(svgSelectedList.Count > 0, svgSelectedList, New List(Of SvgImageBox) From {DragSource})

        Dim toothTrtList As New List(Of Patient_Diagnosis)
        For Each svgBox In targetSVGs
            Dim toothTRT = BuildToothTrt(svgBox, e.Node, treatmentText)
            If toothTRT IsNot Nothing Then toothTrtList.Add(toothTRT)
        Next
        'CLOSE flyout1
        Flyout1.HidePopup()

        'use SaveDiag in SaveDiagDiag module to save the treatment
        'Dim savDiag As New SaveDiag
        ' Show the AddNewDiagForm
        If toothTrtList.Count > 0 Then
            Using savDiag As SaveDiag = If(toothTrtList.Count = 1,
                                           New SaveDiag(toothTrtList(0), clsPatient),
                                           New SaveDiag(toothTrtList, clsPatient))
                Dim result As DialogResult = savDiag.ShowDialog(Me)
                Me.Focus()
                If result = DialogResult.OK Then
                    LoadPatientTreats(PatientID)
                    Me.Focus()
                Else
                    Me.Focus()
                    MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        End If


        '' Show the AddNewDiagForm
        'If toothTrtList.Count > 0 Then
        '    Dim result As DialogResult
        '    Using addTrt As AddNewDiagForm = If(toothTrtList.Count = 1,
        '                                   New AddNewDiagForm(toothTrtList(0), clsPatient),
        '                                   New AddNewDiagForm(toothTrtList, clsPatient))
        '        result = addTrt.ShowDialog(Me)
        '    End Using
        '    Me.Focus()
        '    If result = DialogResult.OK Then
        '        LoadPatientTreats(PatientID)
        '        Me.Focus()
        '    Else
        '        Me.Focus()
        '        MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If
        'End If




        Me.Focus()
        DragSource = Nothing
        grpRadioSetAs.SelectedIndex = 0
    End Sub
    Private Sub TrtsTreeView_KeyDown(sender As Object, e As KeyEventArgs) Handles TrtsTreeView.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Dim selectedNode As TreeNode = TrtsTreeView.SelectedNode
        If selectedNode Is Nothing OrElse selectedNode.Parent Is Nothing OrElse selectedNode.Nodes.Count > 0 OrElse selectedNode.Tag Is Nothing Then
            MessageBox.Show("Please select a single treatment (leaf) node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Build list of target SVG boxes: multi-selected svg boxes or the DragSource
        Dim targets As New List(Of SvgImageBox)
        If svgSelectedList IsNot Nothing AndAlso svgSelectedList.Count > 0 Then
            targets.AddRange(svgSelectedList)
        ElseIf DragSource IsNot Nothing Then
            targets.Add(DragSource)
        Else
            MessageBox.Show("Please select one or more target teeth (SvgImageBox) or set a DragSource.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim toothTrtList As New List(Of Patient_Diagnosis)
        For Each svgBox In targets
            Dim trt = CreateToothTrtFromSvgAndNode(svgBox, selectedNode)
            If trt IsNot Nothing Then
                toothTrtList.Add(trt)
            Else
                ' If you prefer, warn and continue; currently just skip invalid targets
            End If
        Next

        If toothTrtList.Count = 0 Then
            MessageBox.Show("No valid tooth targets found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        'CLOSE flyout1
        Flyout1.HidePopup()
        ' Show the Add form (single vs multi overload)
        Dim result As DialogResult
        If toothTrtList.Count = 1 Then
            Using addTrt As New AddNewDiagForm(toothTrtList(0), clsPatient)
                result = addTrt.ShowDialog(Me)
                Me.Focus()
            End Using
        Else
            Using addTrt As New AddNewDiagForm(toothTrtList, clsPatient)
                result = addTrt.ShowDialog(Me)
                Me.Focus()
            End Using
        End If

        If result = DialogResult.OK Then
            LoadPatientTreats(PatientID)
            Me.Focus()
        Else
            Me.Focus()
            MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Me.Focus()
        DragSource = Nothing
        grpRadioSetAs.SelectedIndex = 0
    End Sub

#End Region



#Region "TrtsTreeViewEvents"

#Region "TreeView Helpers"


    Dim oldTrt As String = ""
    Dim trtID As Integer = 0
    Dim treatmentText As String = ""
    Private TrtClr As String = ""
    Private TrtValue As Decimal = 0
    Private ShapeID As Integer = 0
    Private ShapeName As String = ""
    Private BorderColor As String = ""
    Private BorderThick As Byte = 0
    Private TrtGroup As String = ""
    Private ParentGroup As String = ""
    Private TrtColor As String = ""

    Private Sub TrtsTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TrtsTreeView.AfterSelect


        If e.Node.BackColor <> Color.Green Then Return
        ' Check if a treatment node is selected
        If e.Node.Tag IsNot Nothing Then
            Dim selectedData = DirectCast(e.Node.Tag, Object)
            trtID = selectedData.TrtID
            TrtValue = selectedData.TrtValue
            ShapeID = selectedData.ShapeID
            ShapeName = selectedData.ShapeName
            oldTrt = selectedData.oldTrt
            TrtClr = selectedData.TrtColor ' Assign TrtClr from the selected node's Tag
            BorderColor = selectedData.BorderColor
            BorderThick = selectedData.BorderThick
            TrtGroup = selectedData.TrtGroup
            ParentGroup = selectedData.ParentGroup
            TrtColor = selectedData.TrtColor
        End If
    End Sub

    Public Class DragDropConstants
        Public Const CustomFormat As String = "YourApp.DragDropDataFormat"
    End Class

    <Serializable()>
    Public Class DragDropData
        Public Property treatText As String
        Public Property ShapeName As String
        Public Property ShapeID As Integer
        Public Property TrtBrdrClr As String
        Public Property TrtBrdrThick As Byte
        Public Property TrtID As Integer
        Public Property Trt As String
        Public Property TrtValue As Decimal
        Public Property TrtGroup As String
        Public Property ParentGroup As String
        Public Property OldTrt As String
        Public Property TrtColor As String
        Public Property TrtLoc As String '
    End Class
    'TrtsTreeView
    Private Sub TrtsTreeView_MouseDown(sender As Object, e As MouseEventArgs) Handles TrtsTreeView.MouseDown
        ' Record the initial position and time of the mouse down
        If e.Button = MouseButtons.Left Then
            mouseDownLocation = e.Location
            mouseDownTime = DateTime.Now
        End If
    End Sub
    Private Sub TrtsTreeView_MouseMove(sender As Object, e As MouseEventArgs) Handles TrtsTreeView.MouseMove
        ' Ensure the mouse down location and time are tracked globally
        If mouseDownLocation = Point.Empty Then Exit Sub

        ' Calculate distance and time since mouse down
        Dim distance As Integer = CInt(Math.Sqrt((e.X - mouseDownLocation.X) ^ 2 + (e.Y - mouseDownLocation.Y) ^ 2))
        Dim timeHeld As TimeSpan = DateTime.Now - mouseDownTime

        ' Initiate drag if left button is held, user has moved enough, and held for over 150 ms
        If e.Button = MouseButtons.Left AndAlso distance > 5 AndAlso timeHeld.TotalMilliseconds > 150 AndAlso TrtsTreeView.SelectedNode IsNot Nothing Then
            ' Get the selected node and ensure it's valid
            Dim selectedNode As TreeNode = TrtsTreeView.SelectedNode
            ' Prevent dragging the root node
            If selectedNode.Parent Is Nothing Then
                ' Root nodes have no parent; skip dragging
                Exit Sub
            End If
            ' Only treatment nodes have TrtNodeInfo in Tag; group nodes have String ("GROUP", "PARENT_GROUP", etc.)
            If Not (TypeOf selectedNode.Tag Is TrtSourceHelper.TrtNodeInfo) Then Exit Sub
            Dim tagInfo As TrtSourceHelper.TrtNodeInfo = CType(selectedNode.Tag, TrtSourceHelper.TrtNodeInfo)
            ' Extract treatment data from the node
            Dim trtText As String = selectedNode.Text
            Dim trtShapeName As String = tagInfo.ShapeName
            Dim shapeID As Integer = tagInfo.ShapeID

            ' Create a custom object with all data
            Dim dragData As New DragDropData With {
                .treatText = trtText,
                .TrtValue = tagInfo.TrtValue,
                .ShapeName = trtShapeName,
                .ShapeID = shapeID,
                .TrtBrdrClr = tagInfo.TrtBrdrClr,
                .TrtBrdrThick = tagInfo.TrtBrdrThick,
                .TrtID = tagInfo.TrtID,
                .TrtGroup = tagInfo.TrtGroup,
                .ParentGroup = tagInfo.ParentGroup,
                .OldTrt = tagInfo.OldTrt,
                .TrtColor = tagInfo.TrtColor
            }

            ' Package the data into a DataObject
            Dim dataObj As New DataObject(DragDropConstants.CustomFormat, dragData)

            ' Start drag-and-drop
            TrtsTreeView.DoDragDrop(dataObj, DragDropEffects.Copy)
        End If
    End Sub


    Private Function BuildToothTrt(svg As SvgImageBox, node As TreeNode, treatmentText As String) As Patient_Diagnosis


        ' Extract the base name (e.g., "LdOut1" -> "Ld1")
        ' Get the base name and tooth information
        Dim baseName As String = ""
        If svg.Name.Contains("IN") Then
            baseName = svg.Name.Substring(0, svg.Name.Length - 3) ' Removes "Out", "Top", or similar
        Else
            baseName = svg.Name.Substring(0, svg.Name.Length - 4) ' Removes "Out", "Top", or similar
        End If
        Dim numberPart As String = svg.Name.Substring(svg.Name.Length - 1)

        ' Apply the treatment to the specific SvgImageBox
        ' Extract tooth info
        Dim toothNum As Byte = Convert.ToByte(svg.Tag)
        Dim toothName As String = $"{baseName}{numberPart}".ToUpper


        If toothNum = 0 OrElse String.IsNullOrEmpty(toothName) Then Return Nothing

        ' Find corresponding SVGs
        Dim outSVG As SvgImageBox = FindSvg($"{baseName}OUT{numberPart}")
        Dim topSVG As SvgImageBox = FindSvg($"{baseName}TOP{numberPart}")
        Dim inSVG As SvgImageBox = FindSvg($"{baseName}IN{numberPart}")

        Dim st As String = ""
        If treatmentText.Contains("+") Then
            st = "EXTRACTION + IMPLANT"
        Else
            st = node.Tag.ShapeName.ToString
        End If
        ' Build Patient_Diagnosis
        Dim toothTRT As New Patient_Diagnosis With {
        .PatientID = PatientID,
        .ShapeID = node.Tag.ShapeID,
        .LVL = GetLVL(treatmentText),
        .ToothName = GetToothFullName(toothName),
        .ToothNum = toothNum,
        .Treat = treatmentText,
        .TreatDate = Date.Now,
        .TrtLoc = node.Tag.TrtLoc,
        .PropertyName = st,' node.Tag.ShapeName,
        .FillColor = node.Tag.TrtColor,
        .BorderColor = node.Tag.TrtBrdrClr,
        .BorderThickness = node.Tag.TrtBrdrThick,
        .IsExternal = (grpRadioSetAs.SelectedIndex <> 0),
        .ExternalClinicName = If(grpRadioSetAs.SelectedIndex = 0, "IN House", txtExtClinic.Text),
        .ExternalTreatmentDate = Nothing,
        .IsPaid = (grpRadioSetAs.SelectedIndex <> 0)
    }

        Return toothTRT
    End Function

    Private Function GetBaseNameParts(svgName As String) As (String, String)
        If svgName.Contains("IN") Then
            Return (svgName.Substring(0, svgName.Length - 3), svgName.Substring(svgName.Length - 1))
        Else
            Return (svgName.Substring(0, svgName.Length - 4), svgName.Substring(svgName.Length - 1))
        End If
    End Function

    Private Function FindSvg(name As String) As SvgImageBox
        Dim found = Me.JawPanel.Controls.Find(name, True)
        If found.Length > 0 Then
            Return DirectCast(found(0), SvgImageBox)
        Else
            ' Optional: warn user
            Return Nothing
        End If
    End Function


    ' Helper: create a Patient_Diagnosis from one SvgImageBox and the selected TreeNode
    Private Function CreateToothTrtFromSvgAndNode(svg As SvgImageBox, node As TreeNode) As Patient_Diagnosis
        Try
            Dim treatmentText As String = node.Text.Trim()
            If String.IsNullOrEmpty(treatmentText) Then Return Nothing

            Dim svgName As String = svg.Name
            Dim baseName As String
            ' keep your original substring logic
            If svgName.Contains("IN") Then
                baseName = svgName.Substring(0, svgName.Length - 3)
            Else
                baseName = svgName.Substring(0, svgName.Length - 4)
            End If
            Dim numberPart As String = svgName.Substring(svgName.Length - 1)

            Dim toothNum As Byte = Convert.ToByte(svg.Tag)
            Dim toothName As String = $"{baseName}{numberPart}".ToUpperInvariant()
            If toothNum = 0 OrElse String.IsNullOrEmpty(toothName) Then Return Nothing

            Dim tag = node.Tag ' you used anonymous object tags earlier; late binding assumed
            Dim toothTRT As New Patient_Diagnosis With {
            .PatientID = PatientID,
            .ShapeID = tag.ShapeID,
            .LVL = GetLVL(treatmentText),
            .ToothName = GetToothFullName(toothName),
            .ToothNum = toothNum,
            .Treat = treatmentText,
            .TrtLoc = tag.TrtLoc,
            .PropertyName = tag.ShapeName,
            .FillColor = If(String.IsNullOrEmpty(tag.TrtColor), TrtClr, tag.TrtColor),
            .BorderColor = tag.BorderColor,
            .BorderThickness = tag.BorderThick,
            .IsExternal = (grpRadioSetAs.SelectedIndex <> 0),
            .ExternalClinicName = If(grpRadioSetAs.SelectedIndex = 0, "IN House", txtExtClinic.Text),
            .ExternalTreatmentDate = Date.Now,
            .IsPaid = (grpRadioSetAs.SelectedIndex <> 0),
            .DiagID = 0
        }

            Return toothTRT
        Catch ex As Exception
            ' optional: log or MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function


#End Region

#End Region

#End Region

    '============================================================================================================

#End Region

#Region "Class Kid Diag Dependents"

#Region "Loading"


    'Public Sub AttachRotatableLabel(lbl As LabelControl)
    '    If lbl.Tag Is Nothing Then lbl.Tag = 0.0F

    '    AddHandler lbl.MouseWheel, AddressOf Label_MouseWheel
    '    AddHandler lbl.Paint, AddressOf Label_Paint
    'End Sub


    Private Sub KidDiag_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sw = StartTimer()
        'Dim sw As New Stopwatch
        'sw.Start()
        ' Store bounds for all relevant controls inside MPanel
        ResizeControlsProportionally()

        ' Attach necessary event handlers
        For Each ctl As Control In JawPanel.Controls
            If TypeOf ctl Is SvgImageBox AndAlso (ctl.Name.StartsWith("L") OrElse ctl.Name.StartsWith("R") OrElse ctl.Name.StartsWith("z")) Then
                Dim svgBox As SvgImageBox = CType(ctl, SvgImageBox)
                'AddHandler ctl.Paint, AddressOf SvgImageBox_Paint
                'AddHandler ctl.MouseWheel, AddressOf SvgControl_MouseWheel
                AddHandler ctl.MouseClick, AddressOf CommonMouseClickHandler
                AddHandler ctl.MouseDoubleClick, AddressOf CommonMouseDoubleClickHandler
                AddHandler ctl.MouseEnter, AddressOf CommonMouseEnterHandler
                AddHandler ctl.MouseHover, AddressOf CommonMouseHoverHandler
                AddHandler ctl.MouseLeave, AddressOf CommonMouseLeaveHandler
                AddHandler ctl.MouseDown, AddressOf CommonMouseDownHandler
                AddHandler ctl.MouseUp, AddressOf CommonMouseUpHandler
                AddHandler ctl.DragEnter, AddressOf CommonDragEnterHandler
                AddHandler ctl.DragDrop, AddressOf CommonDragDropHandler
                AddHandler ctl.DragDrop, AddressOf CommonControlClick
                ' Attach event handlers for SvgImageBox items
                'AddHandler svgBox.ItemPress, AddressOf CommonSvgItemPress
                AddHandler svgBox.ItemEnter, AddressOf CommonSvgItemEnter
                'AddHandler svgBox.ItemLeave, AddressOf CommonSvgItemLeave
                'AddHandler svgBox.ItemClick, AddressOf CommonSvgItemClick

            End If
        Next
        'For Each ctl As Control In JawPanel.Controls
        '    If TypeOf ctl Is LabelControl Then
        '        Dim lbl As LabelControl = CType(ctl, LabelControl)
        '        '====================
        '        'label rotate
        '        ' Attach the mouse wheel and paint events
        '        AttachRotatableLabel(lbl)
        '        'If lbl.Tag Is Nothing Then lbl.Tag = 0.0F
        '        'AddHandler lbl.MouseWheel, AddressOf Label_MouseWheel
        '        'AddHandler lbl.Paint, AddressOf Label_Paint
        '    End If
        'Next
        ' Avoid redundant resizing calls
        If Me.Size <> originalPanelSize Then
            ResizeControls()
        End If
        'sw.Stop()
        'LogToFile("KidDiag_Load Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
        LogTime(NameOf(KidDiag_Load), Me.Name, sw)
    End Sub

    Private Sub ResizeControls()
        If originalPanelSize.Width = 0 OrElse originalPanelSize.Height = 0 Then Exit Sub
        ResizeControlsProportionally()
    End Sub




    'Private Sub ResizeControls() Implements IResizableJaw.ResizeControls
    '    If originalPanelSize.Width = 0 OrElse originalPanelSize.Height = 0 Then Exit Sub
    '    ResizeControlsProportionally()
    'End Sub


    ' ✅ Only Call `ResizeControls` When Needed

    '1152, 643
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1100 '1107
    Private Const OriginalPanelHeight As Integer = 670 ' 654
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)
    Private isResizing As Boolean = False
    ' ✅ CON Resize Logic to Prevent Flickering
    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub

    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return

        Dim widthRatio As Double = Me.Width / OriginalPanelWidth
        Dim heightRatio As Double = Me.Height / OriginalPanelHeight

        SuspendLayout()
        Try
            For Each kvp In controlBoundsCache
                With kvp.Value
                    Dim newX As Integer = CInt(Math.Round(.X * widthRatio))
                    Dim newY As Integer = CInt(Math.Round(.Y * heightRatio))
                    Dim newWidth As Integer = CInt(Math.Round(.Width * widthRatio))
                    Dim newHeight As Integer = CInt(Math.Round(.Height * heightRatio))

                    kvp.Key.SetBounds(newX, newY, newWidth, newHeight)
                End With
            Next

            ' For centered controls, consider using Anchor or Dock properties instead
            zSvg.Location = New Point(
            CInt(Math.Round((JawPanel.Width - zSvg.Width) / 2)),
            CInt(Math.Round((JawPanel.Height - zSvg.Height) / 2)))
        Finally

            ResumeLayout(True)
        End Try
    End Sub


    Private Sub KidDiag_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
        ALIGNS()
    End Sub

    Private Sub JawPanel_Click(sender As Object, e As EventArgs) Handles JawPanel.Click
        UnSelect()
        ResetZoomZ()
        svgSelectedList.Clear()
        If svgExternalList.Count > 0 Then
            'For Each sv In svgExternalList
            '    sv.BackColor = Color.LightGray
            'Next
        End If
        ALIGNS()
    End Sub
#End Region



#Region "DragDrop"

    'DragDrop
    ' Common handler for DragEnter
    Private Sub CommonDragEnterHandler(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DragDropConstants.CustomFormat) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' Common handler for DragDrop

#Region "New Drag Code"

    Private Function CreateToothTrtFromDragData(svg As SvgImageBox, dragData As DragDropData) As Patient_Diagnosis
        Try
            Dim baseName As String
            If svg.Name.Contains("IN") Then
                baseName = svg.Name.Substring(0, svg.Name.Length - 3)
            Else
                baseName = svg.Name.Substring(0, svg.Name.Length - 4)
            End If
            Dim numberPart As String = svg.Name.Substring(svg.Name.Length - 1)

            Dim toothNum As Byte = Convert.ToByte(svg.Tag)
            Dim toothName As String = $"{baseName}{numberPart}".ToUpperInvariant()
            If toothNum = 0 OrElse String.IsNullOrEmpty(toothName) Then Return Nothing

            ' Collect OUT/TOP/IN shapes if relevant
            Dim outSVG As SvgImageBox = Nothing
            Dim topSVG As SvgImageBox = Nothing
            Dim inSVG As SvgImageBox = Nothing

            If svg.Name.Contains("OUT") Then
                outSVG = svg
                topSVG = FindSvgDrag($"{baseName}TOPK{numberPart}")
            ElseIf svg.Name.Contains("TOP") Then
                topSVG = svg
                outSVG = FindSvgDrag($"{baseName}OUTK{numberPart}")
            ElseIf svg.Name.Contains("IN") Then
                inSVG = svg
                outSVG = FindSvgDrag($"{baseName}INK{numberPart}")
            End If

            ' Combine collections
            Dim combinedCol As IEnumerable(Of SvgImageItem) = Enumerable.Empty(Of SvgImageItem)()
            If outSVG IsNot Nothing Then combinedCol = combinedCol.Concat(outSVG.RootItems.Cast(Of SvgImageItem)())
            If topSVG IsNot Nothing Then combinedCol = combinedCol.Concat(topSVG.RootItems.Cast(Of SvgImageItem)())
            If inSVG IsNot Nothing Then combinedCol = combinedCol.Concat(inSVG.RootItems.Cast(Of SvgImageItem)())

            ' Create treatment object
            Return New Patient_Diagnosis With {
            .PatientID = PatientID,
            .ShapeID = dragData.ShapeID,
            .LVL = GetLVL(dragData.treatText),
            .ToothName = GetToothFullName(toothName),
            .ToothNum = toothNum,
            .Treat = dragData.treatText,
             .TrtLoc = dragData.TrtLoc,
            .PropertyName = dragData.ShapeName,
            .FillColor = If(String.IsNullOrEmpty(dragData.TrtColor), TrtClr, dragData.TrtColor),
            .BorderColor = dragData.TrtBrdrClr,
            .BorderThickness = dragData.TrtBrdrThick,
            .IsExternal = (grpRadioSetAs.SelectedIndex <> 0),
            .ExternalClinicName = If(grpRadioSetAs.SelectedIndex = 0, "IN House", txtExtClinic.Text),
            .ExternalTreatmentDate = Date.Now,
            .IsPaid = (grpRadioSetAs.SelectedIndex <> 0),
            .DiagID = 0
        }
        Catch
            Return Nothing
        End Try
    End Function

    Private Function FindSvgDrag(name As String) As SvgImageBox
        Dim foundControls = Me.JawPanel.Controls.Find(name, True)
        If foundControls.Length > 0 Then
            Return DirectCast(foundControls(0), SvgImageBox)
        End If
        Return Nothing
    End Function

#End Region

#End Region

    '========================================================


#Region "FlyOutEvents"



    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        Flyout1.OptionsButtonPanel.Buttons.Item(2).Properties.Appearance.ForeColor = Color.Red
        If selectedTeethList.Count > 0 Then
            Flyout1.OptionsButtonPanel.Buttons.Item(2).Properties.Caption = If(Eng, "Delete All Treats On Selected Teeth", "حذف كل العلاجات على الأسنان المحددة")
        Else
            Flyout1.OptionsButtonPanel.Buttons.Item(2).Properties.Caption = If(Eng, "Delete All Treats On Selected Tooth", "حذف كل العلاجات على السن المحدد")
        End If
        Flyout1.OptionsButtonPanel.Buttons.Item(1).Properties.Visible = showEditTrtBtn
        'grpSlctdTeeth.Visible = Not showEditTrtBtn
        TrtsTreeView.Dock = DockStyle.Fill
        TrtsTreeView.Visible = True
        TrtsTreeView.BringToFront()
    End Sub

    Private Sub Flyout1_ButtonChecked(sender As Object, e As FlyoutPanelButtonCheckedEventArgs) Handles Flyout1.ButtonChecked
        Select Case e.Button.GroupIndex
            Case 1

                TrtsTreeView.Visible = False
                AddedTrtsList.Dock = DockStyle.Fill
                AddedTrtsList.Visible = True
                AddedTrtsList.BringToFront()

                ' Extract the base name (e.g., "LdOut1" -> "Ld1")
                Dim baseName As String = slctdSVG.Name.Substring(0, slctdSVG.Name.Length - 4) ' Removes "Out", "Top", or similar
                Dim numberPart As String = slctdSVG.Name.Substring(slctdSVG.Name.Length - 1)

                ' Apply the treatment to the specific SvgImageBox

                Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
                Dim toothName As String = $"{baseName}{numberPart}".ToUpper
                If Not IsMobile Then
                    SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName))
                Else
                    SetAddedMobsListDataSource(PatientID, toothNum, GetToothFullName(toothName))
                End If

        End Select

    End Sub
    Private Sub Flyout1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs) Handles Flyout1.ButtonClick
        For i As Integer = 0 To Flyout1.OptionsButtonPanel.Buttons.Count - 1
            Dim button As DevExpress.Utils.PeekFormButton = Flyout1.OptionsButtonPanel.Buttons(i)

            If button.Tag = e.Button.Tag.ToString() Then
                ' Set the clicked button to CheckButton style and mark it as checked
                button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton
                button.Checked = True
            Else
                ' Set all other buttons to PushButton style and uncheck them
                button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton
                button.Checked = False
            End If
        Next
        'Dim tag As String = e.Button.Tag.ToString()
        Select Case e.Button.GroupIndex
            Case 0
                AddedTrtsList.Visible = False
                TrtsTreeView.Dock = DockStyle.Fill
                TrtsTreeView.Visible = True
                TrtsTreeView.BringToFront()
                Dim x As Int16 = 0
                x = ExtractDigit(slctdSVG.Name)
                'SetTrtsListDataSource(x)
                SetTreeTreatsSingleTooth(x, CByte(slctdSVG.Tag))
                e.Button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton
                e.Button.Checked = True
            Case 1
                If showEditTrtBtn Then
                    TrtsTreeView.Visible = False
                    AddedTrtsList.Dock = DockStyle.Fill
                    AddedTrtsList.Visible = True
                    AddedTrtsList.BringToFront()

                    ' Extract the base name (e.g., "LdOut1" -> "Ld1")
                    Dim baseName As String = slctdSVG.Name.Substring(0, slctdSVG.Name.Length - 4) ' Removes "Out", "Top", or similar
                    Dim numberPart As String = slctdSVG.Name.Substring(slctdSVG.Name.Length - 1)

                    ' Apply the treatment to the specific SvgImageBox

                    Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
                    Dim toothName As String = $"{baseName}{numberPart}".ToUpper
                    'SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName))
                    If Not IsMobile Then
                        SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName))
                    Else
                        SetAddedMobsListDataSource(PatientID, toothNum, GetToothFullName(toothName))
                    End If
                End If
            Case 2
                ' Resolve patient for delete: on production CurrentPatient can be Nothing when jaw is hosted in DiagUserControl
                Dim patientToUse As Patient = If(CurrentPatient, clsPatient)
                If patientToUse Is Nothing Then
                    Dim c As Control = Me
                    While c IsNot Nothing
                        Dim diag As DiagUserControl = TryCast(c, DiagUserControl)
                        If diag IsNot Nothing Then
                            patientToUse = diag.CurrentPatient
                            Exit While
                        End If
                        c = c.Parent
                    End While
                End If
                If patientToUse Is Nothing AndAlso FormManager.Instance.IsBasePatientFormOpen Then
                    patientToUse = FormManager.Instance.GetCurrentPatient()
                End If
                If patientToUse Is Nothing Then
                    MessageBox.Show(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."), If(Eng, "Patient required", "مطلوب مريض"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                If selectedTeethList.Count > 1 Then
                    Dim del As New DelDiagForm(patientToUse, selectedTeethList)
                    If del.ShowDialog(Me) = DialogResult.OK Then
                        LoadPatientTreats(patientToUse.PatientID)
                    End If
                Else
                    Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
                    Dim toothList As New List(Of Byte)
                    toothList.Add(toothNum)
                    Dim del As New DelDiagForm(patientToUse, toothList)
                    If del.ShowDialog(Me) = DialogResult.OK Then
                        LoadPatientTreats(patientToUse.PatientID)
                        toothList.Clear()
                    End If
                End If
        End Select

    End Sub

    Private Sub Flyout1_Hiding(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Hiding
        For i As Integer = 0 To Flyout1.OptionsButtonPanel.Buttons.Count - 1
            Dim button As DevExpress.Utils.PeekFormButton = Flyout1.OptionsButtonPanel.Buttons(i)
            ' Set Edit Treats button to PushButton style and uncheck them
            button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton
            button.Checked = False
        Next
        txtSrchTrt.ResetText()
    End Sub

#End Region

    Private Sub grpRadioSetAs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grpRadioSetAs.SelectedIndexChanged
        If grpRadioSetAs.SelectedIndex = 0 Then
            txtExtClinic.Visible = False
        Else
            txtExtClinic.Visible = True
        End If
    End Sub


    '============================================================================================================


    '============================================================================================================


#Region "ZSVG"

#Region "Svg Item Events"

    'Svg Item Events

    '========================================================
    ' Common handler for SvgItemClick
    Private Sub zSvg_ItemClick(sender As Object, e As SvgImageItemMouseEventArgs) Handles zSvg.ItemClick
        'Dim svgItem As SvgImageItem = e.Item
        'Dim clickedButton As MouseButtons = e.Button
        'MessageBox.Show($"Item Clicked: {svgItem.Id}, Button: {clickedButton}")
    End Sub

    ' Common handler for SvgItemLeave
    Private Sub zSvg_ItemLeave(sender As Object, e As SvgImageItemEventArgs) Handles zSvg.ItemLeave
        ''Dim svgItem As SvgImageItem = e.Item
        ''MessageBox.Show($"Mouse Left Item: {svgItem.Id}")
        'e.Item.Appearance.Normal.BorderColor = Color.Black
        'If e.Item.Id.StartsWith("CH") Then
        '    e.Item.Appearance.Normal.BorderThickness = 0
        'ElseIf e.Item.Id.StartsWith("BR") Then
        e.Item.Appearance.Normal.BorderThickness = 0
        'Else
        '    e.Item.Appearance.Normal.BorderThickness = 1
        'End If

    End Sub

    ' Common handler for SvgItemEnter
    Private Sub zSvg_ItemEnter(sender As Object, e As SvgImageItemEventArgs) Handles zSvg.ItemEnter
        e.Item.Appearance.Normal.BorderColor = Color.FloralWhite
        e.Item.Appearance.Normal.BorderThickness = 2

        If PatientID <= 0 Then Return
        Dim svgItem As SvgImageItem = e.Item
        Dim toothNum As Byte = 0
        Try
            toothNum = Convert.ToByte(zSvg.Tag)
        Catch
            Return
        End Try

        Dim superTip As New SuperToolTip With {.AllowHtmlText = DefaultBoolean.True}
        Dim titleItem As New ToolTipTitleItem()
        Dim baseName As String = If(zSvg.Name.Contains("IN"), zSvg.Name.Substring(0, zSvg.Name.Length - 3), zSvg.Name.Substring(0, zSvg.Name.Length - 4))
        titleItem.Text = $"{baseName} -->> {zSvg.Tag}"

        Dim regularItem As New ToolTipItem()
        regularItem.Text = String.Empty

        Dim toothDiags As IEnumerable(Of Patient_Diagnosis) = clsToothTrtData.Select_BypID_tNum(New Patient_Diagnosis With {.PatientID = PatientID, .ToothNum = toothNum})
        Dim notesList As New List(Of String)()
        If toothDiags IsNot Nothing Then
            Dim itemId As String = If(svgItem?.Id, "")
            For Each t As Patient_Diagnosis In toothDiags
                If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(t.TreatNotes) Then
                    If String.IsNullOrEmpty(itemId) OrElse String.Equals(t.PropertyName, itemId, StringComparison.OrdinalIgnoreCase) Then
                        notesList.Add(t.TreatNotes.Trim())
                    End If
                End If
            Next
            If notesList.Count = 0 AndAlso Not String.IsNullOrEmpty(itemId) Then
                For Each t As Patient_Diagnosis In toothDiags
                    If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(t.TreatNotes) Then
                        notesList.Add(t.TreatNotes.Trim())
                    End If
                Next
            End If
        End If

        If notesList.Count > 0 Then
            Dim backColorHex As String = ColorTranslator.ToHtml(Color.LightYellow)
            Dim noteColors As Color() = {Color.DarkBlue, Color.DarkGreen, Color.DarkRed, Color.DarkOrange, Color.Purple, Color.Teal}
            Dim parts As New List(Of String)()
            For i As Integer = 0 To notesList.Count - 1
                Dim hex As String = ColorTranslator.ToHtml(noteColors(i Mod noteColors.Length))
                parts.Add($"<color={hex}><b>{System.Net.WebUtility.HtmlEncode(notesList(i))}</b></color>")
            Next
            regularItem.Text = $"<backcolor={backColorHex}>" & String.Join("<br/>", parts) & "</backcolor>"
        End If

        superTip.Items.Add(titleItem)
        superTip.Items.Add(regularItem)
        zSvg.SuperTip = superTip
    End Sub

    ' Common handler for SvgItemPress
    Private Sub zSvg_ItemPress(sender As Object, e As SvgImageItemEventArgs) Handles zSvg.ItemPress
        'Dim svgItem As SvgImageItem = e.Item
        ''MessageBox.Show($"Item Pressed: {svgItem.Id}")
    End Sub

    '======================================================

#End Region
#End Region

#Region "Aligning"
    Public Sub AlignTops()
        ' Get reference tops (assuming these are the "base" controls)
        Dim rdTop As Integer = RDTOPK1.Top
        Dim ruTop As Integer = RUTOPK1.Top
        Dim rdOutTop As Integer = RDOUTK1.Top
        Dim ruOutTop As Integer = RUOUTK1.Top

        For Each ctl As Control In JawPanel.Controls
            If ctl.Name.Contains("TOP") Then
                If ctl.Name.StartsWith("RU") OrElse ctl.Name.StartsWith("LU") Then
                    ctl.Top = ruTop
                ElseIf ctl.Name.StartsWith("RD") OrElse ctl.Name.StartsWith("LD") Then
                    ctl.Top = rdTop
                End If
            ElseIf ctl.Name.Contains("OUT") Then
                If ctl.Name.StartsWith("RU") OrElse ctl.Name.StartsWith("LU") Then
                    ctl.Top = ruOutTop
                ElseIf ctl.Name.StartsWith("RD") OrElse ctl.Name.StartsWith("LD") Then
                    ctl.Top = rdOutTop
                End If
            End If
        Next
    End Sub

    Private Sub AlignHorizontally(prefixes As String(), viewType As String, isRightToLeft As Boolean)
        For Each prefix In prefixes
            For i As Integer = 2 To 8
                Dim currentCtrl As Control = JawPanel.Controls($"{prefix}{viewType}{i}")
                Dim prevCtrl As Control = JawPanel.Controls($"{prefix}{viewType}{i - 1}")

                If currentCtrl IsNot Nothing AndAlso prevCtrl IsNot Nothing Then
                    If isRightToLeft Then
                        ' Right-to-left alignment (Ru/Rd)
                        currentCtrl.Left = prevCtrl.Left - currentCtrl.Width
                    Else
                        ' Left-to-right alignment (Lu/Ld)
                        currentCtrl.Left = prevCtrl.Right
                    End If
                End If
            Next
        Next
    End Sub

    Public Sub ALIGNS()
        ' Align vertical positions
        AlignTops()

        ' Align horizontal positions
        Dim leftToRightPrefixes As String() = {"LU", "LD"}
        Dim rightToLeftPrefixes As String() = {"RU", "RD"}

        ' Align Top views
        AlignHorizontally(leftToRightPrefixes, "TOPK", False)  ' Lu/Ld: Left-to-right
        AlignHorizontally(rightToLeftPrefixes, "TOPK", True)   ' Ru/Rd: Right-to-left

        ' Align Out views
        AlignHorizontally(leftToRightPrefixes, "OUTK", False)  ' Lu/Ld: Left-to-right
        AlignHorizontally(rightToLeftPrefixes, "OUTK", True)   ' Ru/Rd: Right-to-left
    End Sub

    Private Sub KidDiag_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        DetachJawHandlers()
        Try
            zoomTimer.Stop()
            zoomTimer.Dispose()
        Catch
        End Try
        Try
            rotTimer.Stop()
            rotTimer.Dispose()
        Catch
        End Try
    End Sub

    Private Sub DetachJawHandlers()
        For Each ctl As Control In JawPanel.Controls
            If TypeOf ctl Is SvgImageBox AndAlso (ctl.Name.StartsWith("L") OrElse ctl.Name.StartsWith("R") OrElse ctl.Name.StartsWith("z")) Then
                RemoveHandler ctl.MouseClick, AddressOf CommonMouseClickHandler
                RemoveHandler ctl.MouseDoubleClick, AddressOf CommonMouseDoubleClickHandler
                RemoveHandler ctl.MouseEnter, AddressOf CommonMouseEnterHandler
                RemoveHandler ctl.MouseHover, AddressOf CommonMouseHoverHandler
                RemoveHandler ctl.MouseLeave, AddressOf CommonMouseLeaveHandler
                RemoveHandler ctl.MouseDown, AddressOf CommonMouseDownHandler
                RemoveHandler ctl.MouseUp, AddressOf CommonMouseUpHandler
                RemoveHandler ctl.DragEnter, AddressOf CommonDragEnterHandler
                RemoveHandler ctl.DragDrop, AddressOf CommonDragDropHandler
                RemoveHandler ctl.DragDrop, AddressOf CommonControlClick
            End If
        Next
    End Sub
#End Region

#End Region



End Class

