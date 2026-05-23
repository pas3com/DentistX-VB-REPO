Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Dapper ' Ensure Dapper is included if you're using it
'Imports DevExpress.LookAndFeel
'Imports DevExpress.Skins
Imports DevExpress.Utils
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Extensions
Imports DevExpress.Utils.Svg
Imports DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors

'Imports DevExpress.Data.Svg
'AdultJaw
Public Class AdultJaw
    Implements IJawControl
    'Implements IToothClickable

    Public Event ToothDoubleClick As EventHandler(Of ToothDoubleClickEvent) Implements IToothClickable.ToothDoubleClick


    ''' <summary>Single entry for patient loads. Constructors and TreatsUserControl must call this (not LoadPatientTreats directly) so IPatientAware / IJawControl paths stay consistent.</summary>
    Public Sub LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
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
        'sw.Start()
        Me.DoubleBuffered = True
        StoreOriginalBounds(Me)
        InitializeDefaultTeethLayout()
        'LoadPatientData(PatientID)
        LogTime("AdultJaw_New", Me.Name, sw)
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
        'Application.DoEvents() ' Forces UI to render updates before proceeding

        LoadPatientData(PatientID) ', hideDivider)
        sw.Stop()
        LogToFile("AdultJaw_NewByVal PatientID Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
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
        SetSvgImages()

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
    ' --- Treatments adult jaw (experiment scope): Mobile chart rows load from dbo.Patient_ToothTrt with LVL=9 ---
    ' --- (GetPatientTeethMobTreats / GetPatientToothMobTreats), not dbo.Patient_Mobile. Selected-tooth panel: SlctdToothCTL Source="Mobile" aligned 2026-04. ---
    ' --- If this is OK, apply the same SlctdToothCTL pattern to other jaw controls; AdultJaw data path was already ToothTrt-based. ---
    Public Property IsMobile As Boolean = False Implements IJawControl.IsMobile
    '=================================================================
    '===========================================================================================

#Region "Definitions"

    Dim clsPatientData As New PatientDATA
    Dim clsPatient As Patient
    ''' <summary>Patient bound to this chart after <see cref="LoadPatientTreats"/>; then workspace (FormManager) if needed.</summary>
    Public ReadOnly Property CurrentPatient As Patient
        Get
            If clsPatient IsNot Nothing Then Return clsPatient
            If FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.CurrentPatient IsNot Nothing Then
                Return FormManager.Instance.CurrentPatient
            End If
            Return Nothing
        End Get
    End Property
    ''' <summary>ID for chart data: loaded record / <see cref="pid"/> first so OTHER TREATS and teeth stay in sync when switching patients.</summary>
    Public ReadOnly Property PatientID As Integer
        Get
            If clsPatient IsNot Nothing Then Return clsPatient.PatientID
            If pid > 0 Then Return pid
            If FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.CurrentPatient IsNot Nothing Then
                Return FormManager.Instance.CurrentPatient.PatientID
            End If
            Return 0
        End Get
    End Property
    ''' <summary>Patient name from <see cref="CurrentPatient"/>.</summary>
    Public ReadOnly Property PatientName As String
        Get
            Dim p = CurrentPatient
            If p IsNot Nothing Then Return If(p.PatientName, "")
            Return ""
        End Get
    End Property
    'This section defines the logic for displaying treats for a given patient
    '=================================
    Dim PatientTrs As IEnumerable(Of Patient_ToothTrt)
    Dim clsTrtData As Patient_ToothTrtDATA
    Private ToothNum As Int16 = 0
    Private ToothName As String = ""


#End Region




#Region "LoadingTreatsSubs"
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) ' Handles btnRefresh.Click
        LoadPatientData(pid)
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
            PatientTrs = New List(Of Patient_ToothTrt)()
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
                Using clsTrtData As New Patient_ToothTrtDATA()
                    If Not IsMobile Then
                        PatientTrs = clsTrtData.GetPatientTeethTreats(patientId)
                        LoadTeethTreatsUsingTreatCode(PatientTrs) '  LoadTeethTreats(PatientTrs) ' 

                    Else
                        PatientTrs = clsTrtData.GetPatientTeethMobTreats(patientId)
                        LoadTeethTreatsUsingTreatCode(PatientTrs) ' LoadTeethTreats(PatientTrs) '
                        'LoadTeethTreatsUsingTreatCode(PatientTrs)
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

    Public Sub LoadTeethTreatsUsingTreatCode(patientTreats As IEnumerable(Of Patient_ToothTrt))
        Dim sw = StartTimer()

        'Me.Visible = False
        Try
            JawPanel.SuspendLayout()
            Dim displayTreats = TrtSourceHelper.ExpandWholeMouthTreatsForJawDisplay(JawPanel, patientTreats)
            For Each ct As Control In JawPanel.Controls.OfType(Of SvgImageBox)()
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                Dim name As String = svg.Name
                svg.BeginUpdate()  ' <== if SvgImageBox supports it
                TreatHelper.ProcessToothTreatments(svg, svgExternalList, displayTreats, IsMobile)
                svg.EndUpdate()
            Next
        Catch ex As Exception
        Finally
            JawPanel.ResumeLayout()
        End Try
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
    End Sub


    Public Sub LoadTeethTreats(patientTreats As IEnumerable(Of Patient_ToothTrt))
        Dim sw = StartTimer()
        JawPanel.SuspendLayout()
        Me.Visible = False
        Dim treats = TrtSourceHelper.ExpandWholeMouthTreatsForJawDisplay(JawPanel, If(patientTreats, Enumerable.Empty(Of Patient_ToothTrt)()))
        For Each svg As SvgImageBox In JawPanel.Controls.OfType(Of SvgImageBox)()
            TreatHelper.ProcessToothTreatments(svg, svgExternalList, treats, IsMobile)
        Next
        JawPanel.ResumeLayout()
        Me.Visible = True
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
    End Sub




#Region "Single Treat"
    '===========================================
    'Dim clsPatientData As New PatientDATA
    'Dim clsPatient As Patient
    Private ToothTrts As IEnumerable(Of Patient_ToothTrt)
    Private PatientTreats As IEnumerable(Of Patient_ToothTrt)
    Dim clsToothTrtData As New Patient_ToothTrtDATA
    Public Sub LoadSnglTreat(patientId As Integer, toothNum As Byte)
        If patientId <= 0 Then Return
        If clsPatientData Is Nothing Then clsPatientData = New PatientDATA()
        clsPatient = New Patient With {.PatientID = patientId}
        clsPatient = clsPatientData.Select_Record(clsPatient)

        If clsPatient IsNot Nothing Then
            ' Load tooth checks for the patient

            PatientTreats = clsToothTrtData.Select_BypID_tNum(New Patient_ToothTrt With {.PatientID = patientId, .ToothNum = toothNum})
            ' Pass the loaded PatientCheck data to the next method
            LoadPatientSnglToothTreat(PatientTreats)
        Else
            MessageBox.Show("Patient not found.")
        End If
    End Sub

    Public Sub LoadPatientSnglToothTreat(patientTreats As IEnumerable(Of Patient_ToothTrt))

        TreatHelper.ProcessToothTreatments(zSvg, svgExternalList, patientTreats, IsMobile)

    End Sub

    '==============================================

#End Region

#End Region

#Region "MouseActivity"

    'Private Sub JawPanel_MouseClick(sender As Object, e As MouseEventArgs) Handles JawPanel.MouseClick
    '    If e.Button = MouseButtons.Right Then
    '        ClearSvgSelection()
    '        UnSelect()
    '        slctdSVG = Nothing
    '        DragSource = Nothing
    '        TrtSourceHelper.ClearAddedTrtsListBound(AddedTrtsList, originalAddedTrtsTable)
    '        JawTreatmentTreeHelper.LoadEmptyJawBackgroundTreatTree(TrtsTreeView, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=False)
    '        txtSrchTrt.ResetText()
    '        If PatientID <= 0 Then Return
    '        'ComboFlyoutSearchHelper.ConfigureManualAnchoredFlyoutOnOwner(Flyout1, Me, New Point(JawPanel.Left + e.X, JawPanel.Top + e.Y), True)
    '        'Flyout1.HidePopup(False)
    '        'Me.BeginInvoke(New MethodInvoker(Sub()
    '        '    If Not Me.IsDisposed AndAlso Not Flyout1.IsDisposed Then Flyout1.ShowPopup()
    '        'End Sub))
    '    ElseIf e.Button = MouseButtons.Left Then
    '        JawPanel.Focus()
    '    End If
    'End Sub


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
    Private _handlersAttached As Boolean = False

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
        If PatientID <= 0 Then Return
        If slctdSVG Is Nothing OrElse slctdSVG.IsDisposed Then
            zoomTimer.Stop()
            Return
        End If
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
        If PatientID <= 0 Then Return
        If svgImageBox Is Nothing OrElse svgImageBox.IsDisposed Then Return
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
        Dim numberPart As String = Svg.Name.Substring(Svg.Name.Length - 1)
        Dim toothName As String = $"{baseName}{numberPart}".ToUpper
        titleItem.Text = ToothSvgQuadrantNaming.GetToothTooltipHeading(Svg.Name, Svg.Tag)

        Dim regularItem As New ToolTipItem()
        regularItem.Text = String.Empty

        Dim toothTrts As IEnumerable(Of Patient_ToothTrt) = clsToothTrtData.Select_BypID_tNum(New Patient_ToothTrt With {.PatientID = PatientID, .ToothNum = toothNum})
        Dim notesList As New List(Of String)()
        If toothTrts IsNot Nothing Then
            Dim itemId As String = If(svgItem?.Id, "")
            For Each t As Patient_ToothTrt In toothTrts
                If t.TreatNotes IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(t.TreatNotes) Then
                    If String.IsNullOrEmpty(itemId) OrElse String.Equals(t.PropertyName, itemId, StringComparison.OrdinalIgnoreCase) Then
                        notesList.Add(t.TreatNotes.Trim())
                    End If
                End If
            Next
            If notesList.Count = 0 AndAlso Not String.IsNullOrEmpty(itemId) Then
                For Each t As Patient_ToothTrt In toothTrts
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

    Private Function CreateToothTrtFromDragData(svg As SvgImageBox, dragData As DragDropData) As Patient_ToothTrt
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

            Dim nmU = svg.Name.ToUpperInvariant()
            Dim isKidSurface = nmU.Contains("OUTK") OrElse nmU.Contains("TOPK") OrElse nmU.Contains("INK")

            If isKidSurface Then
                If nmU.Contains("OUT") Then
                    outSVG = svg
                    topSVG = FindSvgDrag($"{baseName}TOPK{numberPart}")
                ElseIf nmU.Contains("TOP") Then
                    topSVG = svg
                    outSVG = FindSvgDrag($"{baseName}OUTK{numberPart}")
                ElseIf nmU.Contains("IN") Then
                    inSVG = svg
                    outSVG = FindSvgDrag($"{baseName}INK{numberPart}")
                End If
            Else
                If nmU.Contains("OUT") Then
                    outSVG = svg
                    topSVG = FindSvgDrag($"{baseName}Top{numberPart}")
                ElseIf nmU.Contains("TOP") Then
                    topSVG = svg
                    outSVG = FindSvgDrag($"{baseName}Out{numberPart}")
                ElseIf nmU.Contains("IN") Then
                    inSVG = svg
                    outSVG = FindSvgDrag($"{baseName}Out{numberPart}")
                End If
            End If

            ' Combine collections
            Dim combinedCol As IEnumerable(Of SvgImageItem) = Enumerable.Empty(Of SvgImageItem)()
            If outSVG IsNot Nothing Then combinedCol = combinedCol.Concat(outSVG.RootItems.Cast(Of SvgImageItem)())
            If topSVG IsNot Nothing Then combinedCol = combinedCol.Concat(topSVG.RootItems.Cast(Of SvgImageItem)())
            If inSVG IsNot Nothing Then combinedCol = combinedCol.Concat(inSVG.RootItems.Cast(Of SvgImageItem)())


            ' Create treatment object
            Return New Patient_ToothTrt With {
            .PatientID = PatientID,
            .ShapeID = dragData.ShapeID,
            .LVL = GetLVL(dragData.treatText),
            .ToothName = GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled),
            .ToothNum = toothNum,
            .Treat = dragData.treatText,
            .TrtValue = dragData.TrtValue,
            .TrtLoc = dragData.TrtLoc,
            .PropertyName = dragData.ShapeName,
            .TreatDate = Date.Now,
            .TreatmentType = "One Stage",
            .TreatDetails = "",
            .TreatNotes = "",
            .TreatPlan = "",
            .UserID = CurrentUser.UsID,
            .FillColor = If(String.IsNullOrEmpty(dragData.TrtColor), TrtClr, dragData.TrtColor),
            .BorderColor = dragData.TrtBrdrClr,
            .BorderThickness = dragData.TrtBrdrThick,
            .IsExternal = (grpRadioSetAs.SelectedIndex <> 0),
            .ExternalClinicName = If(grpRadioSetAs.SelectedIndex = 0, "IN House", txtExtClinic.Text),
            .ExternalTreatmentDate = Nothing,
            .IsPaid = (grpRadioSetAs.SelectedIndex <> 0),
            .ToothTrtID = 0
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

    Private Sub CommonMouseClickHandler(sender As Object, e As MouseEventArgs)
        ' Close flyout if clicking on SVG when flyout is visible (only for left clicks)
        If FlyMenu.Visible AndAlso e.Button = MouseButtons.Left Then
            FlyMenu.Visible = False
            txtSrchTrt.ResetText()
        End If

        '' Prevent multiple flyouts
        'If FlyMenu.Visible Then
        '    Return
        'End If

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
            SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
            If IsMobile Then
                If targetCount <= 1 Then
                    SetMobTreeSnglTooth(toothID, toothNum)
                Else
                    SetMobTreeMultiTeeth()
                End If
            Else
                If targetCount <= 1 Then
                    SetTreeTreatsSingleTooth(toothID, toothNum)
                Else
                    SetTrtsTreeMultiTeeth()
                End If
            End If

            ' Show panel after data is loaded
            PositionFlyoutPanel(svg)
            If Not FlyMenu.Visible Then
                BackClr = Me.BackColor
                FlyMenu.Visible = True
                PrepareFlyoutPanelForShow()
            End If

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
                    'Debug.Assert(selectedTeethList.Distinct().Count = selectedTeethList.Count)

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

        Dim superTip As New SuperToolTip With {.AllowHtmlText = DefaultBoolean.True}
        ' Create a SuperToolTipTitleItem for the title
        Dim titleItem As New ToolTipTitleItem()
        titleItem.Text = ToothSvgQuadrantNaming.GetToothTooltipHeading(Svg.Name, Svg.Tag)

        ' Create a SuperToolTipItem for the content
        Dim regularItem As New ToolTipItem()
        regularItem.Text = String.Empty ' Initialize the text property

        ' Get the base name and tooth information
        Dim baseName As String = ""
        If Svg.Name.Contains("IN") Then
            baseName = Svg.Name.Substring(0, Svg.Name.Length - 3) ' Removes "Out", "Top", or similar
        Else
            baseName = Svg.Name.Substring(0, Svg.Name.Length - 4) ' Removes "Out", "Top", or similar
        End If

        Dim numberPart As String = Svg.Name.Substring(Svg.Name.Length - 1)
        Dim toothNum As Byte = Convert.ToByte(Svg.Tag)
        Dim toothName As String = $"{baseName}{numberPart}".ToUpper

        ' Get the treatments for the tooth
        Dim Trts As List(Of String) = GetAddedTreatments(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))

        ' Ensure each treatment is added only once
        For Each trt In Trts
            ' Add each treatment to the tooltip
            Dim backColorHex As String = ColorTranslator.ToHtml(Color.YellowGreen)
            Dim textColorHex As String = ColorTranslator.ToHtml(Color.Blue) ' Default text color
            regularItem.Text += $"<br><b><backcolor={backColorHex}><color={textColorHex}>{trt}</color></backcolor></b></br>"
        Next

        ' Apply items to superTip
        superTip.Items.Add(titleItem)
        superTip.Items.Add(regularItem)
        Svg.SuperTip = superTip
        'Debug.WriteLine($"HOVERING ={Svg.Name}")
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
        RemoveToothFromAddList(svg.Name)
        ' Find and deselect all SVG views of this tooth
        For Each ctrl As Control In JawPanel.Controls
            If TypeOf ctrl Is SvgImageBox Then
                Dim toothSvg As SvgImageBox = DirectCast(ctrl, SvgImageBox)
                If Convert.ToByte(toothSvg.Tag) = toothNum AndAlso svgSelectedList.Contains(toothSvg) Then
                    toothSvg.BackColor = Color.Transparent
                    svgSelectedList.Remove(toothSvg)
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
        If Not FlyMenu.Visible Then Return
        btnEditTrts.Visible = showEditTrtBtn
        'grpSlctdTeeth.Visible = Not showEditTrtBtn
        JawFlyoutLocalization.SetJawFlyMenuDeleteButtonCaption(btnDelTrts, selectedTeethList.Count > 0)
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
               ctrl.Name.Contains("Out") AndAlso
               Convert.ToByte(ctrl.Tag) = toothNumber Then
                Return DirectCast(ctrl, SvgImageBox)
            End If
        Next
        Return Nothing
    End Function
    Private Function FindOutView(topSvgName As String) As SvgImageBox
        Dim outName As String = ""
        If topSvgName.Contains("Out") Then
            outName = topSvgName
        ElseIf topSvgName.Contains("Top") Then
            outName = topSvgName.Replace("Top", "Out")
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
        Return TrtSourceHelper.GetTrtByToothIds(query, toothNumbers)
    End Function

    Private Function GetApplicableTreatments(selectedTeeth As List(Of Integer), allTreatments As List(Of TblTRTS)) As List(Of TblTRTS)
        Return TrtSourceHelper.GetApplicableTreatments(selectedTeeth, allTreatments)
    End Function

    ' In-memory master list (full set for the mobile page)
    Private AllTrtNodes As New List(Of TrtSourceHelper.TrtNodeInfo)()

    Private Sub SetTrtsTreeMultiTeeth()
        JawTreatmentTreeHelper.LoadTreatTreeMulti(TrtsTreeView, selectedTeethList.Select(Function(t) CInt(t)).ToList(), PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=False)
        Exit Sub
        Try
            TrtsTreeView.Nodes.Clear()

            ' ------------------------
            ' 0. Determine teeth/jaw stats
            ' ------------------------
            ' Additional conditions for multi-tooth selection
            Dim hasUpperTeeth As Boolean = selectedTeethList.Any(Function(t) t >= 11 AndAlso t <= 28)
            Dim hasLowerTeeth As Boolean = selectedTeethList.Any(Function(t) t >= 31 AndAlso t <= 48)
            Dim isMixedSelection As Boolean = hasUpperTeeth AndAlso hasLowerTeeth

            ' Get current treatment levels for all selected teeth
            Dim maxToothLevel As Integer = -1
            Dim allLevels As New List(Of Integer)

            For Each ToothNum In selectedTeethList
                Dim lvls = clsToothTrtData.GetTreatLVLs(PatientID, ToothNum)
                For Each lvl In lvls
                    If Not allLevels.Contains(lvl) Then allLevels.Add(lvl)
                Next
            Next

            ' Determine the overall tooth level for filtering (similar to single tooth logic)
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

            Dim toothIDParam As String = If(selectedTeethList.Count = 1,
                       (selectedTeethList(0) Mod 10).ToString(),
                       String.Join(",", selectedTeethList.Select(Function(t) t Mod 10)))

            ' ------------------------
            ' 2. Build mobile-only query
            ' ------------------------
            Dim query As String = ""
            query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtValue], [TblTRTS].[TrtLoc], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr],  
    [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],  
    [TblTRTS].[TrtLVL], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],  
    [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt]  
    FROM [TblTRTS] LEFT JOIN [Shapes] ON [TblTRTS].[ShapeID] = [Shapes].[ShapeID]  
    WHERE  [TrtGroup] Is Not NULL   AND [TrtGroup] <> 'DIAG' 
        And (
        -- Always show ALL treatments
        [ToothID] = 'ALL' 

        -- Or show treatments that match the selection rules
        Or (
            -- Check if treatment matches any selected tooth
        (
            EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE ',' + [TblTRTS].[ToothID] + ',' LIKE '%,' + value + ',%')
            Or EXISTS(SELECT value FROM STRING_SPLIT([TblTRTS].[ToothID], ',') WHERE ',' + @ToothID + ',' LIKE '%,' + value + ',%')
)
        And (
            -- Case 1: When mixing both 1-3 And 4-8 - only show Class 1 And 5
            (
                EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('1','2','3'))
                And EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('4','5','6','7','8'))
                And [TrtGroup] In ('CLASS 1', 'CLASS 5')
            )
            Or
        -- Case 2: When only selecting from 4-8 - show Class 1, 2, And 5
        (
            Not EXISTS (SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('1','2','3'))
            And EXISTS (SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('4','5','6','7','8'))
            And [TrtGroup] IN ('CLASS 1', 'CLASS 2', 'CLASS 5')
        )
        Or
        -- Case 3: When only selecting from 1-3 - show Class 1, 3, 4, And 5
        (
            EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('1','2','3'))
            And Not EXISTS(SELECT value FROM STRING_SPLIT(@ToothID, ',') WHERE value IN ('4','5','6','7','8'))
            And [TrtGroup] In ('CLASS 1', 'CLASS 3', 'CLASS 4', 'CLASS 5')
        )
                )
            )
        ) "

            'Check if Is kid or not
            If isKid Then
                query += " AND ([KidTrt] IN (1,  2)) AND [Trt] <> 'FISSURE SEALENT' "
            Else
                query += " AND ([KidTrt] IN (0,  2)) AND [Trt] <> 'PRR' "
            End If

            ' Add level-specific conditions based on overall tooth level (same as single tooth)
            'Select Case overallToothLevel
            '    Case -1, 0, 1, 2, 3 ' Levels -1, 0, 1, 2, 3
            '        query += " AND ([TrtGroup] <> 'IMPLANT' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [TrtGroup] <> 'IMPLANT COMPONENT' AND [Trt] <> 'IMPLANT') OR [Trt] = 'EXTRACTION + IMPLANT'"

            '    Case 4 ' Level 4
            '        query += " AND [TrtLVL] > 4 AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [Trt] <> 'EXTRACTION + IMPLANT' AND [Trt] <> 'ABUTMENT' AND [Trt] <> 'HEALING CAP'"

            '    Case 5 ' Levels 5 and above
            '        query += " AND (([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR
            '                    [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 5) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND
            '                    [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT'"

            '    Case 6 ' Levels 6 and above
            '        query += $" AND ([TrtGroup] = 'EXTRACTION' OR [Trt]= 'EXTRACTION + IMPLANT' OR  [TrtGroup] = 'BRIDGE' OR
            '                        [TrtLVL] >=  {overallToothLevel} ) AND 
            '                        [TrtGroup] <> 'CROWNS ON TOOTH' AND
            '                        [TrtGroup] <> 'INDIRECT VENEERS' AND
            '                        [TrtGroup] <> 'DIRECT VENEERS' AND
            '                        [TrtGroup] <> 'IMPLANT' AND
            '                        [Trt] <> 'IMPLANT'"
            '    Case 7 ' Levels 7 and above
            '        query += $" AND ([[TrtGroup]] = 'EXTRACTION' OR [Trt]= 'EXTRACTION + IMPLANT' OR  [TrtGroup] = 'BRIDGE' OR
            '                        [TrtLVL] = {overallToothLevel}) AND
            '                        [TrtGroup] <> 'CROWNS ON TOOTH' AND
            '                        [TrtGroup] <> 'IMPLANT' AND
            '                        [TrtGroup] <> 'INDIRECT VENEERS' AND
            '                        [TrtGroup] <> 'DIRECT VENEERS' AND
            '                        [TrtGroup] <> 'CROWNS ON IMPLANT' AND
            '                        [Trt] <> 'IMPLANT'"
            '    Case 8 ' Levels 8
            '        query += $" AND ([[TrtGroup]] = 'EXTRACTION' OR [Trt]= 'EXTRACTION + IMPLANT' OR [TrtLVL] = {overallToothLevel})
            '                        AND [TrtGroup] <> 'CROWNS ON TOOTH' AND
            '                        [TrtGroup] <> 'IMPLANT' AND
            '                        [TrtGroup] <> 'INDIRECT VENEERS' AND
            '                        [TrtGroup] <> 'DIRECT VENEERS' AND
            '                        [TrtGroup] <> 'CROWNS ON IMPLANT' AND
            '                        [Trt] <> 'IMPLANT'"

            'End Select


            ' Determine the unique level groups present
            ' Determine the unique level groups present
            Dim hasMinusOne As Boolean = allLevels.Any(Function(l) l <= 3) ' -1 group
            Dim hasLevel4 As Boolean = allLevels.Contains(4)
            Dim hasLevel5 As Boolean = allLevels.Contains(5)
            Dim hasLevel6Plus As Boolean = allLevels.Any(Function(l) l >= 6) ' Levels 6,7,8

            ' Build level-specific conditions based on combinations
            If hasMinusOne AndAlso hasLevel4 AndAlso (hasLevel5 Or hasLevel6Plus) Then
                ' Case: -1,4,5+ - Only show BRIDGE
                query += " AND [TrtGroup] = 'BRIDGE'"

            ElseIf hasMinusOne AndAlso hasLevel4 AndAlso Not (hasLevel5 Or hasLevel6Plus) Then
                ' Case: -1,4 - Only show BRIDGE
                query += " AND [TrtGroup] = 'BRIDGE'"

            ElseIf hasMinusOne AndAlso (hasLevel5 Or hasLevel6Plus) AndAlso Not hasLevel4 Then
                ' Case: -1,5+ - Show EXTRACTION from level 4 and EXTRACTION+IMPLANT from level 5+
                query += " AND ([Trt] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT')  OR [TrtGroup] = 'BRIDGE'"

            ElseIf hasLevel4 AndAlso (hasLevel5 Or hasLevel6Plus) AndAlso Not hasMinusOne Then
                ' Case: 4,5+ - Only show BRIDGE
                query += " AND [TrtGroup] = 'BRIDGE'"

            ElseIf hasMinusOne AndAlso Not hasLevel4 AndAlso Not hasLevel5 AndAlso Not hasLevel6Plus Then
                ' Case: Only -1 levels
                query += " AND ([TrtGroup] <> 'IMPLANT' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [TrtGroup] <> 'IMPLANT COMPONENT' AND [Trt] <> 'IMPLANT') OR [Trt] = 'EXTRACTION + IMPLANT'"

            ElseIf hasLevel4 AndAlso Not hasMinusOne AndAlso Not hasLevel5 AndAlso Not hasLevel6Plus Then
                ' Case: Only level 4
                query += " AND [TrtLVL] > 4 AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [Trt] <> 'EXTRACTION + IMPLANT' AND [Trt] <> 'ABUTMENT' AND [Trt] <> 'HEALING CAP'"

            ElseIf hasLevel5 AndAlso Not hasMinusOne AndAlso Not hasLevel4 AndAlso Not hasLevel6Plus Then
                ' Case: Only level 5 (no 6+)
                query += " AND (([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR 
                            [TrtGroup] = 'BRIDGE' OR [TrtGroup] = 'IMPLANT COMPONENT' OR [TrtLVL] = 5) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND 
                            [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT'"

            ElseIf hasLevel6Plus AndAlso Not hasMinusOne AndAlso Not hasLevel4 AndAlso Not hasLevel5 Then
                ' Case: Only level 6+ (no level 5)
                query += " AND (([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 6) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT'"

            ElseIf hasLevel5 AndAlso hasLevel6Plus AndAlso Not hasMinusOne AndAlso Not hasLevel4 Then
                ' Case: Level 5 and 6+ combined
                query += " AND (([TrtGroup] = 'EXTRACTION' OR [Trt] = 'EXTRACTION + IMPLANT' OR [TrtGroup] = 'BRIDGE' OR [TrtLVL] >= 5) AND [TrtGroup] <> 'CROWNS ON TOOTH' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS') AND [Trt] <> 'IMPLANT'"
            End If



            ' Exclude bridge if mixed selection
            If selectedTeethList.Count > 1 AndAlso isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            End If

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            ' ------------------------
            ' 3. Execute and load into TreeView
            ' ----------------------------------------------------------
            '  STEP 1: Get list of already applied treatments for this tooth
            ' ----------------------------------------------------------
            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim connection As New SqlConnection(connectionString)

            ' List of tooth numbers you want to check
            Dim toothNums As New List(Of Byte)
            toothNums = selectedTeethList

            Dim existingTrts As New HashSet(Of Integer)

            ' Build the IN clause dynamically
            Dim toothParams As New List(Of String)
            For i As Integer = 0 To toothNums.Count - 1
                toothParams.Add("@ToothNum" & i)
            Next
            Dim inClause As String = String.Join(", ", toothParams)

            Dim sql As String = "SELECT TrtID 
                 FROM dbo.Patient_ToothTrt 
                 INNER JOIN dbo.TblTRTS 
                    ON dbo.Patient_ToothTrt.Treat = dbo.TblTRTS.Trt 
                 WHERE PatientID = @PatientID 
                   AND ToothNum IN (" & inClause & ")"

            Using checkCmd As New SqlCommand(sql, connection)
                ' Add the PatientID parameter
                checkCmd.Parameters.AddWithValue("@PatientID", PatientID)

                ' Add each ToothNum parameter
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

            ' Create command with parameterized query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", toothIDParam)
            ' Fill DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            ' ------------------------

            AllTrtNodes.Clear()

            AllTrtNodes = TrtSourceHelper.BuildTrtNodesFromDataTable(dataTable)

            BuildTreeFromList(AllTrtNodes, existingTrts, "No available treatments for the selected teeth.")

            ' ------------------------
            ' 4. Save snapshot for textbox filtering
            ' ------------------------
            SaveFullTreeSnapshot()

            ' Collapse all parent groups
            For Each n As TreeNode In TrtsTreeView.Nodes
                If n.Tag?.ToString() = "PARENT_GROUP" Then n.Collapse()
            Next

            ' Show message if empty
            If TrtsTreeView.Nodes.Count = 0 Then
                Dim msgNode As New TreeNode("No available treatments for the selected teeth.") With {
            .ForeColor = Color.Gray
        }
                TrtsTreeView.Nodes.Add(msgNode)
            End If

        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub


    Private Sub SetTreeTreatsSingleTooth(toothID As String, toothNum As Byte)
        JawTreatmentTreeHelper.LoadTreatTreeSingle(TrtsTreeView, toothID, toothNum, PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=False)
    End Sub

    ' -------------------------
    ' Build the TreeView from a list of TrtNodeInfo (used for full & filtered)
    ' -------------------------

    Private Sub BuildTreeFromList(nodeList As List(Of TrtSourceHelper.TrtNodeInfo), Optional existingTrts As HashSet(Of Integer) = Nothing, Optional emptyMessage As String = "No available treatments for current tooth status")
        TrtSourceHelper.BuildTreeFromList(TrtsTreeView, nodeList, existingTrts, emptyMessage)
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

    ' Helper: deep clone a TreeNode and its children
    Private Function CloneNode(node As TreeNode) As TreeNode
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

    ' -------------------------
    ' Filtering logic (call this from UI when you want to apply the in-memory filter)
    ' e.g. call ApplyTreeFilter() from your selection-changed event
    ' -------------------------
    Private Sub ApplyTreeFilter(filterText As String)
        TrtSourceHelper.ApplyTreeFilter(TrtsTreeView, fullTreeSnapshot, filterText)
    End Sub

    ' Recursive function to clone node if it or any child matches filter text
    Private Function FilterNode(node As TreeNode, filterText As String) As TreeNode
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

            ' Apply coloring based on whether the node matches itself
            If matchSelf Then
                ' Direct match
                clone.ForeColor = Color.Red
                clone.BackColor = Color.LightYellow
            Else
                ' No direct match, but has a matching child
                clone.ForeColor = Color.Blue ' like groupNode in your first block
                clone.BackColor = Color.Transparent
            End If

            ' Add styled children
            For Each c In matchedChildren
                ' If child directly matches, color it red/yellow
                If c.Text.ToLowerInvariant().Contains(filterText.ToLowerInvariant()) Then
                    c.ForeColor = Color.Red
                    c.BackColor = Color.LightYellow
                Else
                    c.ForeColor = Color.Green
                    c.BackColor = Color.Transparent
                End If
                clone.Nodes.Add(c)
            Next
            ' Expand if this node has results
            clone.Expand()

            Return clone
        Else
            Return Nothing
        End If
    End Function

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

        Dim lookupToothName As String = toothName
        Dim fromFdi = ToothSvgQuadrantNaming.GetAdultToothTrtLookupNameByFdi(CInt(toothNum))
        If Not String.IsNullOrWhiteSpace(fromFdi) Then
            lookupToothName = fromFdi
        End If

        ' Define the SQL query with concatenation for Treat and TreatDate
        Dim query As String = "
                                SELECT ToothTrtID, 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyy') + ')' AS DisplayTreat
                                FROM Patient_ToothTrt 
                                WHERE PatientID = @PatientID AND ToothNum = @ToothNum AND ToothName = @ToothName
                                Order By TreatDate Desc"

        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@PatientID", patientID)
            command.Parameters.AddWithValue("@ToothNum", toothNum)
            command.Parameters.AddWithValue("@ToothName", lookupToothName)

            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Set the data source for the list box
            AddedTrtsList.DataSource = dataTable
            AddedTrtsList.DisplayMember = "DisplayTreat"  ' Set the column to display Treat and TreatDate
            AddedTrtsList.ValueMember = "ToothTrtID"           ' Set the ID column as the value member
        End Using
        originalAddedTrtsTable = Nothing
    End Sub

    ''' <summary>Empty-jaw / full-mouth: Patient_ToothTrt rows stored with ToothNum = 0 (denture + whole-mouth other treats).</summary>
    Private Sub SetAddedTrtsListDataSourceFullMouth(patientID As Integer)
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        AddedTrtsList.DataSource = Nothing
        AddedTrtsList.DisplayMember = Nothing
        AddedTrtsList.ValueMember = Nothing

        Dim query As String = "
                                SELECT ToothTrtID, 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyy') + ')' AS DisplayTreat
                                FROM Patient_ToothTrt 
                                WHERE PatientID = @PatientID AND ToothNum = 0
                                Order By TreatDate Desc"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@PatientID", patientID)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            AddedTrtsList.DataSource = dataTable
            AddedTrtsList.DisplayMember = "DisplayTreat"
            AddedTrtsList.ValueMember = "ToothTrtID"
        End Using
        originalAddedTrtsTable = Nothing
    End Sub

    Private Sub SetAddedMobsListDataSourceFullMouth(patientID As Integer)
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        AddedTrtsList.DataSource = Nothing
        AddedTrtsList.DisplayMember = Nothing
        AddedTrtsList.ValueMember = Nothing

        Dim query As String = "
                                SELECT ToothTrtID, 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyy') + ')' AS DisplayTreat
                                FROM Patient_ToothTrt 
                                WHERE [dbo].[Patient_ToothTrt].LVL = 9 And PatientID = @PatientID AND ToothNum = 0
                                Order By TreatDate Desc"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@PatientID", patientID)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            AddedTrtsList.DataSource = dataTable
            AddedTrtsList.DisplayMember = "DisplayTreat"
            AddedTrtsList.ValueMember = "ToothTrtID"
        End Using
        originalAddedTrtsTable = Nothing
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
        JawTreatmentTreeHelper.LoadMobileTreeMulti(TrtsTreeView, selectedTeethList.Select(Function(t) CInt(t)).ToList(), PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=False)
        Exit Sub
        Try
            TrtsTreeView.Nodes.Clear()

            ' ------------------------
            ' 0. Determine teeth/jaw stats
            ' ------------------------
            ' Additional conditions for multi-tooth selection
            Dim hasUpperTeeth As Boolean = selectedTeethList.Any(Function(t) t >= 11 AndAlso t <= 28)
            Dim hasLowerTeeth As Boolean = selectedTeethList.Any(Function(t) t >= 31 AndAlso t <= 48)
            Dim isMixedSelection As Boolean = hasUpperTeeth AndAlso hasLowerTeeth
            ' Get current treatment levels for all selected teeth
            Dim maxToothLevel As Integer = -1

            ' Get treatment levels for each tooth
            Dim allLevels As New List(Of Integer)
            For Each ToothNum In selectedTeethList
                Dim lvls = clsToothTrtData.GetTreatLVLs(PatientID, ToothNum)
                For Each lvl In lvls
                    If Not allLevels.Contains(lvl) Then allLevels.Add(lvl)
                Next
            Next

            For Each level In allLevels
                Dim simplifiedLevel = If(level <= 3, -1, If(level = 4, 4, 5))
                maxToothLevel = Math.Max(maxToothLevel, simplifiedLevel)
            Next
            Dim toothIDParam As String = If(selectedTeethList.Count = 1,
                           (selectedTeethList(0) Mod 10).ToString(),
                           String.Join(",", selectedTeethList.Select(Function(t) t Mod 10)))


            ' ------------------------
            ' 2. Build mobile-only query
            ' ------------------------
            Dim query As String = ""
            query = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], 
            [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],
            [TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],
            [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] 
            From [TblTRTS] LEFT Join [Shapes] On [TblTRTS].[ShapeID] = [Shapes].[ShapeID] 
             WHERE ([TrtGroup] IN ('EXTRACTION', 'IMPLANT', 'DENTURES')) "


            'Check if Is kid or not
            'If isKid Then
            '    query += " AND ([KidTrt] IN (1,  2)) "
            'Else
            '    query += " AND ([KidTrt] IN (0,  2)) "
            'End If

            ' Add level-specific conditions 
            ' Exclude bridge if mixed selection
            If selectedTeethList.Count > 1 AndAlso isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            ElseIf selectedTeethList.Count = 16 AndAlso Not isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE' "
            End If


            Dim norTrt As Boolean = allLevels.Any(Function(l) l < 4)
            Dim exTrt As Boolean = allLevels.Contains(4)
            Dim impTrt As Boolean = allLevels.Any(Function(l) l >= 5 AndAlso l <= 7)
            Dim brgTrt As Boolean = allLevels.Contains(8)

            Select Case True
                Case Not norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso Not brgTrt '0000
                    ' No condition matched in original
                    query += " AND 1=1"

                Case norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso Not brgTrt '0001
                    ' Display All Treats
                    query += " AND [TrtGroup] NOT IN ('IMPLANT', 'CROWNS ON IMPLANT','IMPLANT COMPONENT')"

                Case Not norTrt AndAlso exTrt AndAlso Not impTrt AndAlso Not brgTrt '0010
                    ' Display Implant + Bridge
                    query += " AND [TrtGroup] IN ('IMPLANT', 'BRIDGE')"

                Case norTrt AndAlso exTrt AndAlso Not impTrt AndAlso Not brgTrt '0011
                    ' Display Bridge
                    query += " AND [TrtGroup] = 'BRIDGE'"

                Case Not norTrt AndAlso Not exTrt AndAlso impTrt AndAlso Not brgTrt '0100
                    ' Display Extraction + Abutment + Healing Cap + Crown on Imp + Bridge
                    query += " AND ([Trt] = 'EXTRACTION' OR [Trt] = 'ABUTMENT' OR [Trt] = 'HEALING CAP' OR [TrtGroup] = 'CROWNS ON IMPLANT' OR [TrtGroup] = 'BRIDGE')"

                Case norTrt AndAlso Not exTrt AndAlso impTrt AndAlso Not brgTrt '0101
                    ' Display Extraction + Bridge
                    query += " AND ([Trt] = 'EXTRACTION' OR [TrtGroup] = 'BRIDGE')"

                Case Not norTrt AndAlso exTrt AndAlso impTrt AndAlso Not brgTrt '0110
                    ' Display Bridge
                    query += " AND [TrtGroup] = 'BRIDGE'"

                Case norTrt AndAlso exTrt AndAlso impTrt AndAlso Not brgTrt '0111
                    ' Display Bridge
                    query += " AND [TrtGroup] = 'BRIDGE'"

                Case Not norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso brgTrt '1000
                    ' Display Extraction
                    query += " AND [Trt] = 'EXTRACTION'"

                Case norTrt AndAlso Not exTrt AndAlso Not impTrt AndAlso brgTrt '1001
                    ' Display Extraction
                    query += " AND [Trt] = 'EXTRACTION'"

                Case Not norTrt AndAlso exTrt AndAlso Not impTrt AndAlso brgTrt '1010
                    ' Display None
                    query += " AND 1=0"

                Case norTrt AndAlso exTrt AndAlso Not impTrt AndAlso brgTrt '1011
                    ' Display Extraction
                    query += " AND 1=0"

                Case Not norTrt AndAlso Not exTrt AndAlso impTrt AndAlso brgTrt '1100
                    ' Display Extraction
                    query += " AND [Trt] = 'EXTRACTION'"

                Case norTrt AndAlso Not exTrt AndAlso impTrt AndAlso brgTrt '1101
                    ' Display Extraction
                    query += " AND [Trt] = 'EXTRACTION'"

                Case Not norTrt AndAlso exTrt AndAlso impTrt AndAlso brgTrt '1110
                    ' Display Extraction
                    query += " AND 1=0"

                Case norTrt AndAlso exTrt AndAlso impTrt AndAlso brgTrt '1111
                    ' Display Bridge
                    query += " AND 1=0"
            End Select




            ' Always maintain the mixed arches restriction
            If isMixedSelection Then
                query += " AND [TrtGroup] <> 'BRIDGE'"
            End If

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            ' ------------------------
            ' 3. Execute and load into TreeView
            ' ----------------------------------------------------------
            '  STEP 1: Get list of already applied treatments for this tooth
            ' ----------------------------------------------------------
            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim connection As New SqlConnection(connectionString)

            ' List of tooth numbers you want to check
            Dim toothNums As New List(Of Byte) 'From {1, 2, 3, 4}
            toothNums = selectedTeethList

            Dim existingTrts As New HashSet(Of Integer)

            ' Build the IN clause dynamically
            Dim toothParams As New List(Of String)
            For i As Integer = 0 To toothNums.Count - 1
                toothParams.Add("@ToothNum" & i)
            Next
            Dim inClause As String = String.Join(", ", toothParams)

            Dim sql As String = "SELECT [ToothTrtID] FROM [dbo].[Patient_ToothTrt] INNER JOIN
                         dbo.TblTRTS ON [dbo].[Patient_ToothTrt].Treat = dbo.TblTRTS.Trt WHERE
                         [dbo].[Patient_ToothTrt].LVL=9 AND  PatientID = @PatientID 
                       AND ToothNum IN (" & inClause & ")"

            Using checkCmd As New SqlCommand(sql, connection)
                ' Add the PatientID parameter
                checkCmd.Parameters.AddWithValue("@PatientID", PatientID)

                ' Add each ToothNum parameter
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

            ' Create command with parameterized query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", toothIDParam)
            command.Parameters.AddWithValue("@CurrentToothLevel", maxToothLevel)
            ' Fill DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            ' ------------------------

            AllTrtNodes = TrtSourceHelper.BuildTrtNodesFromDataTable(dataTable)

            BuildTreeFromList(AllTrtNodes, existingTrts, "No available treatments for the selected teeth.")

            ' ------------------------
            ' 4. Save snapshot for textbox filtering
            ' ------------------------
            SaveFullTreeSnapshot()

            ' Collapse all parent groups
            For Each n As TreeNode In TrtsTreeView.Nodes
                If n.Tag?.ToString() = "PARENT_GROUP" Then n.Collapse()
            Next

            ' Show message if empty
            If TrtsTreeView.Nodes.Count = 0 Then
                Dim msgNode As New TreeNode("No available treatments for the selected teeth.") With {
                .ForeColor = Color.Gray
            }
                TrtsTreeView.Nodes.Add(msgNode)
            End If

        Catch ex As Exception
            MsgBox("Error loading treatments: " & ex.Message)
        End Try
    End Sub

    Private Sub SetMobTreeSnglTooth(toothID As String, toothNum As Byte)
        JawTreatmentTreeHelper.LoadMobileTreeSingle(TrtsTreeView, toothID, toothNum, PatientID, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=False)
        Exit Sub
        ' Clear existing nodes
        TrtsTreeView.Nodes.Clear()

        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Get current treatment level for this tooth
        Dim normalLVL As Integer = -1 ' All treats except Implant and Crown on Implant
        Dim extractLVL As Integer = 4 ' All treats above extract
        Dim impLVL As Integer = 5 '  Implant and extract
        Dim impComp As Integer = 6 '  Implant Components and extract
        Dim impCrwn As Integer = 7 '  Implant Crown and extract
        Dim brdgLvl As Integer = 8 '  Only extract

        Dim currentToothLevel As Integer = clsToothTrtData.GetTreatLVL(PatientID, toothNum)

        If currentToothLevel <= 3 Then
            currentToothLevel = normalLVL
        ElseIf currentToothLevel = 4 Then
            currentToothLevel = extractLVL
        ElseIf currentToothLevel = 5 Then
            currentToothLevel = impLVL
        ElseIf currentToothLevel = 6 Then
            currentToothLevel = impComp
        ElseIf currentToothLevel = 7 Then
            currentToothLevel = impCrwn
        ElseIf currentToothLevel = 8 Then
            currentToothLevel = brdgLvl
        End If

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' ----------------------------------------------------------
            '  STEP 1: Get list of already applied treatments for this tooth
            ' ----------------------------------------------------------
            Dim existingTrts As New HashSet(Of Integer)
            Using checkCmd As New SqlCommand("SELECT [ToothTrtID] FROM [dbo].[Patient_ToothTrt] INNER JOIN
                         dbo.TblTRTS ON [dbo].[Patient_ToothTrt].Treat = dbo.TblTRTS.Trt WHERE
                         [dbo].[Patient_ToothTrt].LVL=9 AND PatientID = @PatientID AND ToothNum = @ToothNum", connection)
                checkCmd.Parameters.AddWithValue("@PatientID", PatientID)
                checkCmd.Parameters.AddWithValue("@ToothNum", toothNum)
                Using rdr = checkCmd.ExecuteReader()
                    While rdr.Read()
                        existingTrts.Add(rdr.GetInt32(0))
                    End While
                End Using
            End Using

            ' ----------------------------------------------------------
            '  STEP 2: Build query for available treatments
            ' ----------------------------------------------------------
            Dim query As String = "SELECT [TblTRTS].[TrtID], [TblTRTS].[Trt], [TblTRTS].[TrtColor], [TblTRTS].[TrtBrdrClr], 
            [TblTRTS].[TrtBrdrThick], [TblTRTS].[ShapeID], [Shapes].[ShapeName], [TblTRTS].[TrtDetails],
            [TblTRTS].[TrtLVL], [TblTRTS].[TrtLoc], [TblTRTS].[ToothID], [TblTRTS].[OldTrt],
            [TblTRTS].[TrtGroup], [TblTRTS].[ParentGroup], [TblTRTS].[KidTrt] 
            From [TblTRTS] LEFT Join [Shapes] On [TblTRTS].[ShapeID] = [Shapes].[ShapeID] 
             WHERE ([TrtGroup] IN ('EXTRACTION', 'IMPLANT', 'DENTURES')) "

            ' Kid/adult condition
            'If isKid Then
            '    query += " AND ([KidTrt] IN (1,  2)) "
            'Else
            '    query += " AND ([KidTrt] IN (0,  2)) "
            'End If

            ' Level-specific conditions  
            If currentToothLevel = -1 Then
                query += " AND [TrtGroup] <> 'IMPLANT' AND [TrtGroup] <> 'CROWNS ON IMPLANT' AND [TrtGroup] <> 'IMPLANT COMPONENT' "
            ElseIf currentToothLevel = 4 Then
                query += " AND [TrtLVL] > 4 AND [TrtGroup] <> 'CROWNS ON TOOTH' " &
                     "AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT' " &
                     "AND [Trt] <> 'ABUTMENT' AND [Trt] <> 'HEALING CAP'"
            ElseIf currentToothLevel = 5 Then
                query += " AND ( ([TrtGroup] = 'EXTRACTION' OR [TrtGroup] = 'BRIDGE') OR ([TrtLVL] > @CurrentToothLevel))  AND [TrtGroup] <> 'CROWNS ON TOOTH' " &
                     "AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT' "
            ElseIf currentToothLevel = 6 Then
                query += " AND ( ([TrtGroup] = 'EXTRACTION' OR [TrtGroup] = 'BRIDGE') OR ([TrtLVL] >= @CurrentToothLevel))  AND [TrtGroup]  <> 'CROWNS ON TOOTH' " &
                     "AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS'  AND [TrtGroup] <> 'IMPLANT'"
            ElseIf currentToothLevel = 7 Then
                query += " AND ( ([TrtGroup] = 'EXTRACTION' OR [TrtGroup] = 'BRIDGE') OR ([TrtLVL] = @CurrentToothLevel)) AND [TrtGroup]  <> 'CROWNS ON TOOTH' " &
                     "AND [TrtGroup] <> 'IMPLANT' AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT'"
            ElseIf currentToothLevel = 8 Then
                query += " AND ( ([TrtGroup] = 'EXTRACTION') OR ([TrtLVL] = @CurrentToothLevel)) AND [TrtGroup]  <> 'CROWNS ON TOOTH'  AND [TrtGroup] <> 'IMPLANT' " &
                     "AND [TrtGroup] <> 'INDIRECT VENEERS' AND [TrtGroup] <> 'DIRECT VENEERS' AND [TrtGroup] <> 'CROWNS ON IMPLANT' "
            End If

            query += " ORDER BY [ParentGroup], [TrtGroup], [Trt]"

            ' Execute query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", toothID)
            command.Parameters.AddWithValue("@CurrentToothLevel", currentToothLevel)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' ----------------------------------------------------------
            ' STEP 3: Build dictionary of group nodes
            ' ----------------------------------------------------------
            Dim groupNodes As New Dictionary(Of String, TreeNode)()

            ' First pass - create all group nodes
            For Each row As DataRow In dataTable.Rows
                Dim groupName As String = row.Field(Of String)("TrtGroup")
                Dim parentGroupName As String = If(row.IsNull("ParentGroup"), Nothing, row.Field(Of String)("ParentGroup"))

                If Not groupNodes.ContainsKey(groupName) Then
                    Dim groupNode As New TreeNode(groupName) With {
                    .Name = groupName,
                    .ForeColor = Color.Blue,
                    .Tag = "GROUP"
                }
                    groupNodes.Add(groupName, groupNode)
                End If

                If Not String.IsNullOrEmpty(parentGroupName) AndAlso Not groupNodes.ContainsKey(parentGroupName) Then
                    Dim parentNode As New TreeNode(parentGroupName) With {
                    .Name = parentGroupName,
                    .ForeColor = Color.DarkBlue,
                    .Tag = "PARENT_GROUP"
                }
                    groupNodes.Add(parentGroupName, parentNode)
                End If
            Next

            ' ----------------------------------------------------------
            ' STEP 4: Second pass - build hierarchy and add treatments
            ' ----------------------------------------------------------
            For Each row As DataRow In dataTable.Rows
                Dim TrtID = row.Field(Of Integer)("TrtID")
                Dim groupName As String = row.Field(Of String)("TrtGroup")
                Dim parentGroupName As String = If(row.IsNull("ParentGroup"), Nothing, row.Field(Of String)("ParentGroup"))
                Dim groupNode = groupNodes(groupName)

                Dim treatmentNode As New TreeNode(row.Field(Of String)("Trt")) With {
                .Tag = New With {
                    Key .TrtID = TrtID,
                    Key .ShapeID = row.Field(Of Integer)("ShapeID"),
                    Key .ShapeName = row.Field(Of String)("ShapeName"),
                    Key .OldTrt = row.Field(Of String)("OldTrt"),
                    Key .TrtColor = row.Field(Of String)("TrtColor"),
                    Key .BorderColor = row.Field(Of String)("TrtBrdrClr"),
                    Key .BorderThick = row.Field(Of Byte?)("TrtBrdrThick").GetValueOrDefault(0)
                },
                .ForeColor = Color.Green
            }

                ' 🔹 Highlight if already applied
                If existingTrts.Contains(TrtID) Then
                    treatmentNode.ForeColor = Color.Red
                    treatmentNode.NodeFont = New Font(TrtsTreeView.Font, FontStyle.Strikeout)
                End If

                ' Add to appropriate parent
                If String.IsNullOrEmpty(parentGroupName) Then
                    groupNode.Nodes.Add(treatmentNode)
                Else
                    Dim parentNode = groupNodes(parentGroupName)
                    If Not parentNode.Nodes.ContainsKey(groupName) Then
                        parentNode.Nodes.Add(groupNode)
                    End If
                    groupNode.Nodes.Add(treatmentNode)
                End If
            Next

            ' ----------------------------------------------------------
            ' STEP 5: Add top-level nodes to TreeView
            ' ----------------------------------------------------------
            For Each kvp In groupNodes
                If kvp.Value.Parent Is Nothing Then
                    TrtsTreeView.Nodes.Add(kvp.Value)
                End If
            Next

            SaveFullTreeSnapshot()
            ' Collapse parent groups initially
            For Each node As TreeNode In TrtsTreeView.Nodes
                If node.Tag?.ToString() = "PARENT_GROUP" Then
                    node.Collapse()
                End If
            Next

            ' Show info if nothing is available
            If TrtsTreeView.Nodes.Count = 0 Then
                Dim infoNode As New TreeNode("No available treatments for current tooth status") With {
                .ForeColor = Color.Gray,
                .Tag = "INFO_NODE"
            }
                TrtsTreeView.Nodes.Add(infoNode)
            End If
        End Using
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
                                SELECT ToothTrtID, 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyy') + ')' AS DisplayTreat
                                FROM Patient_ToothTrt 
                                WHERE [dbo].[Patient_ToothTrt].LVL = 9 And PatientID = @PatientID AND ToothNum = @ToothNum AND ToothName = @ToothName
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
            AddedTrtsList.ValueMember = "ToothTrtID"           ' Set the ID column as the value member
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

#Region "ENTER KEY AND DOUBLE CLICK AddedTrts AND TreeView Events"


    Private quickMode As Boolean = False
    Private Function SaveTrt(mode As Boolean, toothTrtList As List(Of Patient_ToothTrt)) As DialogResult
        If clsPatient Is Nothing OrElse toothTrtList Is Nothing OrElse toothTrtList.Count = 0 Then Return DialogResult.Cancel
        If Not mode Then
            If toothTrtList.Count = 1 Then
                Using addTrt As New AddNewTrtForm(toothTrtList(0), clsPatient)
                    Return addTrt.ShowDialog(Me)
                End Using
            Else
                Using addTrt As New AddNewTrtForm(toothTrtList, clsPatient)
                    Return addTrt.ShowDialog(Me)
                End Using
            End If
        Else
            Using savTrt As SaveTreat = If(toothTrtList.Count = 1,
                                           New SaveTreat(toothTrtList(0), clsPatient),
                                           New SaveTreat(toothTrtList, clsPatient))
                Return savTrt.ShowDialog(Me)
            End Using
        End If
    End Function
    Private Sub btnQuickSrch_Toggled(sender As Object, e As EventArgs) Handles btnQuickSrch.Toggled
        If btnQuickSrch.IsOn Then
            quickMode = True
            btnQuickSrch.Properties.Appearance.BackColor = Color.Red
            'txtSrchTrt.Properties.NullValuePrompt = "Quick Search Enabled"
        Else
            quickMode = False
            btnQuickSrch.Properties.Appearance.Reset()
            'txtSrchTrt.Properties.NullValuePrompt = "Search Treatments..."
        End If
    End Sub




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

        Dim toothTrtList As New List(Of Patient_ToothTrt)
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
        If FlyMenu.Visible Then
            FlyMenu.Visible = False
        End If

        Dim result As DialogResult
        'If toothTrtList.Count = 1 Then
        '    Using addTrt As New AddNewTrtForm(toothTrtList(0), clsPatient)
        '        result = addTrt.ShowDialog(Me)
        '        Me.Focus()
        '    End Using
        'Else
        '    Using addTrt As New AddNewTrtForm(toothTrtList, clsPatient)
        '        result = addTrt.ShowDialog(Me)
        '        Me.Focus()
        '    End Using
        'End If
        'Me.Focus()
        result = SaveTrt(quickMode, toothTrtList)
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

    Private Sub TreeView_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TrtsTreeView.NodeMouseDoubleClick
        ' Basic validation
        If e.Node Is Nothing OrElse e.Node.Parent Is Nothing OrElse e.Node.Nodes.Count > 0 OrElse e.Node.Tag Is Nothing Then Exit Sub

        Dim treatmentText As String = e.Node.Text.Trim()

        If String.IsNullOrEmpty(treatmentText) Then Exit Sub

        If JawTreatmentTreeHelper.TryApplyEmptyJawTreeLeafWithoutSelection(
            e.Node, JawPanel,
            svgSelectedList IsNot Nothing AndAlso svgSelectedList.Count > 0,
            DragSource IsNot Nothing,
            isKid,
            AddressOf BuildToothTrt,
            Function(list)
                If FlyMenu.Visible Then
                    FlyMenu.Visible = False
                End If
                Dim result As DialogResult = DialogResult.Cancel
                If list.Count > 0 Then
                    result = SaveTrt(quickMode, list)
                    If result = DialogResult.OK Then
                        LoadPatientTreats(PatientID)
                        Me.Focus()
                    Else
                        Me.Focus()
                        MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    Me.Focus()
                End If
                Me.Focus()
                DragSource = Nothing
                grpRadioSetAs.SelectedIndex = 0
                Return result
            End Function) Then
            Return
        End If

        ' Collect target SVGs (single or multiple selection) — same rules as TrtsTreeView_KeyDown
        Dim targetSVGs As New List(Of SvgImageBox)
        If svgSelectedList IsNot Nothing AndAlso svgSelectedList.Count > 0 Then
            For Each s As SvgImageBox In svgSelectedList
                If s IsNot Nothing Then targetSVGs.Add(s)
            Next
        ElseIf DragSource IsNot Nothing Then
            targetSVGs.Add(DragSource)
        Else
            Exit Sub
        End If
        If targetSVGs.Count = 0 Then Exit Sub

        Dim toothTrtList As New List(Of Patient_ToothTrt)
        For Each svgBox In targetSVGs
            Dim toothTRT = BuildToothTrt(svgBox, e.Node, treatmentText)
            If toothTRT IsNot Nothing Then toothTrtList.Add(toothTRT)
        Next
        'CLOSE flyout1
        If FlyMenu.Visible Then
            FlyMenu.Visible = False
        End If
        ' Show the AddNewTrtForm
        If toothTrtList.Count > 0 Then
            Dim result As DialogResult
            'Using addTrt As AddNewTrtForm = If(toothTrtList.Count = 1,
            '                               New AddNewTrtForm(toothTrtList(0), clsPatient),
            '                               New AddNewTrtForm(toothTrtList, clsPatient))
            '    result = addTrt.ShowDialog(Me)
            'End Using
            'Me.Focus()
            result = SaveTrt(quickMode, toothTrtList)
            If result = DialogResult.OK Then
                LoadPatientTreats(PatientID)
                Me.Focus()
            Else
                Me.Focus()
                MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
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

        If JawTreatmentTreeHelper.TryApplyEmptyJawTreeLeafWithoutSelection(
            selectedNode, JawPanel,
            svgSelectedList IsNot Nothing AndAlso svgSelectedList.Count > 0,
            DragSource IsNot Nothing,
            isKid,
            AddressOf BuildToothTrt,
            Function(list)
                If FlyMenu.Visible Then
                    FlyMenu.Visible = False
                End If
                Dim result1 As DialogResult = DialogResult.Cancel
                If list.Count > 0 Then
                    result1 = SaveTrt(quickMode, list)
                    If result1 = DialogResult.OK Then
                        LoadPatientTreats(PatientID)
                        Me.Focus()
                    Else
                        Me.Focus()
                        MessageBox.Show("Nothing Changed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    Me.Focus()
                End If
                Me.Focus()
                DragSource = Nothing
                grpRadioSetAs.SelectedIndex = 0
                Return result1
            End Function) Then
            e.SuppressKeyPress = True
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

        Dim toothTrtList As New List(Of Patient_ToothTrt)
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
        If FlyMenu.Visible Then
            FlyMenu.Visible = False
        End If
        ' Show the Add form (single vs multi overload)
        Dim result As DialogResult
        'If toothTrtList.Count = 1 Then
        '    Using addTrt As New AddNewTrtForm(toothTrtList(0), clsPatient)
        '        result = addTrt.ShowDialog(Me)
        '        Me.Focus()
        '    End Using
        'Else
        '    Using addTrt As New AddNewTrtForm(toothTrtList, clsPatient)
        '        result = addTrt.ShowDialog(Me)
        '        Me.Focus()
        '    End Using
        'End If
        result = SaveTrt(quickMode, toothTrtList)
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
                Dim ToothTrtID As Integer = Convert.ToInt32(selectedRow("ToothTrtID"))
                'Dim treat As String = Convert.ToString(selectedRow("DisplayTreat"))
                'Dim x As Integer = treat.IndexOf("-")
                'treat = treat.Remove(x)
                '' Display the values or use them as needed
                ''MessageBox.Show($"Treatment: {treat}{Environment.NewLine}TrtID: {trtID}", "Treatment Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim trtData As New Patient_ToothTrtDATA
                Dim toothTrt = TrtSourceHelper.LoadToothTrtForEdit(trtData, ToothTrtID, PatientID)
                If toothTrt Is Nothing Then
                    MessageBox.Show("Failed to retrieve treatment details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                'CLOSE flyout1
                If FlyMenu.Visible Then
                    FlyMenu.Visible = False
                End If
                Dim result As DialogResult
                Using editTrt As New EditTreatFrom(toothTrt, clsPatient)
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
            Dim ToothTrtID As Integer = Convert.ToInt32(selectedRow("ToothTrtID"))
            Dim trtData As New Patient_ToothTrtDATA
            Dim toothTrt = TrtSourceHelper.LoadToothTrtForEdit(trtData, ToothTrtID, PatientID)
            If toothTrt Is Nothing Then
                MessageBox.Show("Failed to retrieve treatment details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            'CLOSE flyout1
            If FlyMenu.Visible Then
                FlyMenu.Visible = False
            End If
            Dim result As DialogResult
            Using editTrt As New EditTreatFrom(toothTrt, clsPatient)
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
            ' Extract treatment data from the node
            'Extract Data from the node's Tag
            Dim trtText As String = selectedNode.Text
            Dim trtShapeName As String = selectedNode.Tag.ShapeName
            Dim shapeID As Integer = selectedNode.Tag.ShapeID

            ' Create a custom object with all data
            Dim dragData As New DragDropData With {
                .treatText = trtText,
                .TrtValue = selectedNode.Tag.TrtValue,
                .ShapeName = trtShapeName,
                .ShapeID = shapeID,
                .TrtBrdrClr = selectedNode.Tag.TrtBrdrClr,
                .TrtBrdrThick = selectedNode.Tag.TrtBrdrThick,
                .TrtID = selectedNode.Tag.TrtID,
                .TrtGroup = selectedNode.Tag.TrtGroup,
                .ParentGroup = selectedNode.Tag.ParentGroup,
                .OldTrt = selectedNode.Tag.OldTrt,
                .TrtColor = selectedNode.Tag.TrtColor
            }

            ' Package the data into a DataObject
            Dim dataObj As New DataObject(DragDropConstants.CustomFormat, dragData)

            ' Start drag-and-drop
            TrtsTreeView.DoDragDrop(dataObj, DragDropEffects.Copy)
        End If
    End Sub


    Private Function BuildToothTrt(svg As SvgImageBox, node As TreeNode, treatmentText As String) As Patient_ToothTrt
        Try
            If svg Is Nothing OrElse node Is Nothing OrElse node.Tag Is Nothing Then Return Nothing
            If String.IsNullOrEmpty(svg.Name) Then Return Nothing
            If svg.Name.Contains("IN") Then
                If svg.Name.Length < 4 Then Return Nothing
            Else
                If svg.Name.Length < 5 Then Return Nothing
            End If

            ' Extract the base name (e.g., "LdOut1" -> "Ld1")
            Dim baseName As String
            If svg.Name.Contains("IN") Then
                baseName = svg.Name.Substring(0, svg.Name.Length - 3)
            Else
                baseName = svg.Name.Substring(0, svg.Name.Length - 4)
            End If
            Dim numberPart As String = svg.Name.Substring(svg.Name.Length - 1)

            Dim toothNum As Byte
            Try
                toothNum = Convert.ToByte(svg.Tag)
            Catch
                Return Nothing
            End Try

            Dim toothName As String = $"{baseName}{numberPart}".ToUpper

            If toothNum = 0 OrElse String.IsNullOrEmpty(toothName) Then Return Nothing

            Dim st As String
            If treatmentText.Contains("+") Then
                st = "EXTRACTION + IMPLANT"
            Else
                ' ShapeName can be Nothing; .ToString on Nothing throws NullReferenceException
                st = Convert.ToString(node.Tag.ShapeName)
            End If

            If CurrentUser Is Nothing Then Return Nothing

            Dim toothTRT As New Patient_ToothTrt With {
            .PatientID = PatientID,
            .ShapeID = node.Tag.ShapeID,
            .LVL = GetLVL(treatmentText),
            .ToothName = GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled),
            .ToothNum = toothNum,
            .Treat = treatmentText,
            .TrtValue = node.Tag.TrtValue,
            .TrtLoc = node.Tag.TrtLoc,
            .PropertyName = st,
            .FillColor = node.Tag.TrtColor,
            .BorderColor = node.Tag.TrtBrdrClr,
            .BorderThickness = node.Tag.TrtBrdrThick,
            .TreatDate = Date.Now,
            .TreatmentType = "One Stage",
            .TreatDetails = "",
            .TreatNotes = "",
            .TreatPlan = "",
            .UserID = CurrentUser.UsID,
            .IsExternal = (grpRadioSetAs.SelectedIndex <> 0),
            .ExternalClinicName = If(grpRadioSetAs.SelectedIndex = 0, "IN House", If(txtExtClinic Is Nothing, "", txtExtClinic.Text)),
            .ExternalTreatmentDate = Nothing,
            .IsPaid = (grpRadioSetAs.SelectedIndex <> 0)
        }

            EnsureCompoundLayerFills(toothTRT)
            Return toothTRT
        Catch
            Return Nothing
        End Try
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


    ' Helper: create a Patient_ToothTrt from one SvgImageBox and the selected TreeNode
    Private Function CreateToothTrtFromSvgAndNode(svg As SvgImageBox, node As TreeNode) As Patient_ToothTrt
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
            Dim toothTRT As New Patient_ToothTrt With {
            .PatientID = PatientID,
            .ShapeID = tag.ShapeID,
            .LVL = GetLVL(treatmentText),
            .ToothName = GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled),
            .ToothNum = toothNum,
            .Treat = treatmentText,
            .TrtValue = tag.TrtValue,
            .TrtLoc = tag.TrtLoc,
            .PropertyName = tag.ShapeName,
            .FillColor = If(String.IsNullOrEmpty(tag.TrtColor), TrtClr, tag.TrtColor),
            .BorderColor = tag.BorderColor,
            .BorderThickness = tag.BorderThick,
            .TreatDate = Date.Now,
            .TreatmentType = "One Stage",
            .TreatDetails = "",
            .TreatNotes = "",
            .TreatPlan = "",
            .UserID = CurrentUser.UsID,
            .IsExternal = (grpRadioSetAs.SelectedIndex <> 0),
            .ExternalClinicName = If(grpRadioSetAs.SelectedIndex = 0, "IN House", txtExtClinic.Text),
            .ExternalTreatmentDate = Nothing,
            .IsPaid = (grpRadioSetAs.SelectedIndex <> 0),
            .ToothTrtID = 0
        }

            EnsureCompoundLayerFills(toothTRT)
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


#Region "Class Adult Dependents"

#Region "Loading"


    'Public Sub AttachRotatableLabel(lbl As LabelControl)
    '    If lbl.Tag Is Nothing Then lbl.Tag = 0.0F

    '    AddHandler lbl.MouseWheel, AddressOf Label_MouseWheel
    '    AddHandler lbl.Paint, AddressOf Label_Paint
    'End Sub


    Private Sub AdultJaw_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sw = StartTimer()
        'Dim sw As New Stopwatch
        'sw.Start()
        ' Store bounds for all relevant controls inside MPanel
        ResizeControlsProportionally()

        ' Attach necessary event handlers
        If Not _handlersAttached Then
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
            _handlersAttached = True
        End If
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
        WireFlyMenuOutsideClickFilter()
        'sw.Stop()
        'LogToFile("AdultJaw_Load Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
        LogTime(NameOf(AdultJaw_Load), Me.Name, sw)
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
    Private Const OriginalPanelWidth As Integer = 1100 '1100, 650
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


    Private Sub AdultJaw_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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


#Region "FlyOutEvents"

    Private _flyMenuOutsideClickFilter As IMessageFilter
    Private _flyMenuOutsideClickFilterWired As Boolean

    Private Sub WireFlyMenuOutsideClickFilter()
        If _flyMenuOutsideClickFilterWired Then Return
        _flyMenuOutsideClickFilterWired = True
        _flyMenuOutsideClickFilter = New FlyMenuOutsideClickFilter(Me)
        Application.AddMessageFilter(_flyMenuOutsideClickFilter)
    End Sub

    Private Sub UnwireFlyMenuOutsideClickFilter()
        If Not _flyMenuOutsideClickFilterWired Then Return
        _flyMenuOutsideClickFilterWired = False
        If _flyMenuOutsideClickFilter IsNot Nothing Then
            Application.RemoveMessageFilter(_flyMenuOutsideClickFilter)
            _flyMenuOutsideClickFilter = Nothing
        End If
    End Sub

    ''' <summary>Dismiss FlyMenu when the user left-clicks outside its screen bounds (works app-wide via <see cref="IMessageFilter"/>).</summary>
    Private Sub TryHideFlyMenuFromOutsideClick(clickScreenPoint As Point)
        If Not FlyMenu.Visible Then Return
        If FlyMenu Is Nothing OrElse FlyMenu.IsDisposed Then Return
        Try
            Dim flyoutScreenRect As New Rectangle(FlyMenu.PointToScreen(Point.Empty), FlyMenu.Size)
            If Not flyoutScreenRect.Contains(clickScreenPoint) Then
                FlyMenu.Visible = False
                txtSrchTrt.ResetText()
            End If
        Catch
        End Try
    End Sub

    Friend Sub TryHideFlyMenuFromApplicationMouse(ByRef m As Message)
        Const WM_LBUTTONDOWN As Integer = &H201
        If m.Msg <> WM_LBUTTONDOWN Then Return
        If IsDisposed OrElse Not IsHandleCreated Then Return
        If Not FlyMenu.Visible Then Return
        TryHideFlyMenuFromOutsideClick(Control.MousePosition)
    End Sub

    Private NotInheritable Class FlyMenuOutsideClickFilter
        Implements IMessageFilter
        Private ReadOnly _jaw As AdultJaw

        Public Sub New(jaw As AdultJaw)
            _jaw = jaw
        End Sub

        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            _jaw.TryHideFlyMenuFromApplicationMouse(m)
            Return False
        End Function
    End Class

    Private Sub JawPanel_MouseClick(sender As Object, e As MouseEventArgs) Handles JawPanel.MouseClick
        'If e.Button = MouseButtons.Right Then
        '    If FlyMenu.Visible Then
        '        ' Check if click is outside the flyout panel
        '        Dim flyoutScreenRect As New Rectangle(FlyMenu.PointToScreen(Point.Empty), FlyMenu.Size)
        '        Dim clickScreenPoint As Point = Control.MousePosition
        '        If Not flyoutScreenRect.Contains(clickScreenPoint) Then
        '            FlyMenu.Visible = False
        '            txtSrchTrt.ResetText()
        '        End If
        '    End If
        '    '===================
        '    If PatientID <= 0 Then Exit Sub
        '    If slctdSVG Is Nothing OrElse slctdSVG.IsDisposed Then Exit Sub
        '    PositionFlyoutPanel(slctdSVG)
        '    FlyMenu.Visible = True
        '    PrepareFlyoutPanelForShow()
        'End If
        '=============
        If e.Button = MouseButtons.Right Then
            ClearSvgSelection()
            UnSelect()
            slctdSVG = Nothing
            DragSource = Nothing
            TrtSourceHelper.ClearAddedTrtsListBound(AddedTrtsList, originalAddedTrtsTable)
            JawTreatmentTreeHelper.LoadEmptyJawBackgroundTreatTree(TrtsTreeView, isKid, AllTrtNodes, fullTreeSnapshot, useDiagnosis:=False)

            '===================
            If PatientID <= 0 Then Exit Sub
            If slctdSVG Is Nothing OrElse slctdSVG.IsDisposed Then Exit Sub
            PositionFlyoutPanel(slctdSVG)
            FlyMenu.Visible = True
            PrepareFlyoutPanelForShow()
            If PatientID <= 0 Then Return

        ElseIf e.Button = MouseButtons.Left Then
            TryHideFlyMenuFromOutsideClick(Control.MousePosition)
            JawPanel.Focus()
        End If
    End Sub

    Private Sub AdultJawNewCode_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = MouseButtons.Left Then
            TryHideFlyMenuFromOutsideClick(Control.MousePosition)
        End If
    End Sub

    Private Sub PrepareFlyoutPanelForShow()
        JawFlyoutLocalization.ApplyJawTreatmentsFlyMenuLanguage(FlyMenu)
        btnDelTrts.Appearance.ForeColor = Color.Red
        JawFlyoutLocalization.SetJawFlyMenuDeleteButtonCaption(btnDelTrts, selectedTeethList.Count > 0)
        btnEditTrts.Visible = showEditTrtBtn
        'grpSlctdTeeth.Visible = Not showEditTrtBtn
        TrtsTreeView.Dock = DockStyle.Fill
        TrtsTreeView.Visible = True
        TrtsTreeView.BringToFront()
    End Sub


    Private Sub PositionFlyoutPanel(svg As SvgImageBox)
        Dim loc As Point = svg.Location
        If svg.Name.StartsWith("Ld") Then
            FlyMenu.Location = New System.Drawing.Point(loc.X - FlyMenu.Width, loc.Y + svg.Height - FlyMenu.Height)
        ElseIf svg.Name.StartsWith("Rd") Then
            FlyMenu.Location = New System.Drawing.Point(loc.X + svg.Width, loc.Y + svg.Height - FlyMenu.Height)
        ElseIf svg.Name.StartsWith("Lu") Then
            FlyMenu.Location = New System.Drawing.Point(loc.X - FlyMenu.Width, loc.Y)
        ElseIf svg.Name.StartsWith("Ru") Then
            FlyMenu.Location = New System.Drawing.Point(loc.X + svg.Width, loc.Y)
        End If
        FlyMenu.BringToFront()
    End Sub

    Private Sub btnTrtView_Click(sender As Object, e As EventArgs) Handles btnTrtView.Click
        AddedTrtsList.Visible = False
        TrtsTreeView.Dock = DockStyle.Fill
        TrtsTreeView.Visible = True
        TrtsTreeView.BringToFront()
        If slctdSVG IsNot Nothing Then
            Dim x As Int16 = ExtractDigit(slctdSVG.Name)
            If IsMobile Then
                SetMobTreeSnglTooth(x, CByte(slctdSVG.Tag))
            Else
                SetTreeTreatsSingleTooth(x, CByte(slctdSVG.Tag))
            End If
        End If
    End Sub

    Private Sub btnEditTrts_Click(sender As Object, e As EventArgs) Handles btnEditTrts.Click
        TrtsTreeView.Visible = False
        AddedTrtsList.Dock = DockStyle.Fill
        AddedTrtsList.Visible = True
        AddedTrtsList.BringToFront()

        If slctdSVG Is Nothing Then
            If PatientID <= 0 Then
                MessageBox.Show(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."), If(Eng, "Patient required", "مطلوب مريض"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not IsMobile Then
                SetAddedTrtsListDataSourceFullMouth(PatientID)
            Else
                SetAddedMobsListDataSourceFullMouth(PatientID)
            End If
        Else
            ' Extract the base name (e.g., "LdOut1" -> "Ld1")
            Dim baseName As String = slctdSVG.Name.Substring(0, slctdSVG.Name.Length - 4) ' Removes "Out", "Top", or similar
            Dim numberPart As String = slctdSVG.Name.Substring(slctdSVG.Name.Length - 1)

            Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
            Dim toothName As String = $"{baseName}{numberPart}".ToUpper
            If Not IsMobile Then
                SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
            Else
                SetAddedMobsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
            End If
        End If
    End Sub

    Private Sub btnDelTrts_Click(sender As Object, e As EventArgs) Handles btnDelTrts.Click
        ' Resolve patient for delete: on production CurrentPatient can be Nothing when jaw is hosted in TreatsUserControl
        Dim patientToUse As Patient = If(CurrentPatient, clsPatient)
        If patientToUse Is Nothing Then
            Dim c As Control = Me
            While c IsNot Nothing
                Dim treatsUC As TreatsUserControl = TryCast(c, TreatsUserControl)
                If treatsUC IsNot Nothing Then
                    patientToUse = treatsUC.CurrentPatient
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
            Dim del As New DelTreatFrom(patientToUse, selectedTeethList)
            If del.ShowDialog(Me) = DialogResult.OK Then
                LoadPatientTreats(patientToUse.PatientID)
            End If
        ElseIf slctdSVG Is Nothing Then
            Dim toothList As New List(Of Byte)
            If selectedTeethList.Count = 1 Then
                toothList.Add(selectedTeethList(0))
            Else
                toothList.Add(0) ' full mouth / chartless (Patient_ToothTrt.ToothNum = 0)
            End If
            Dim del As New DelTreatFrom(patientToUse, toothList)
            If del.ShowDialog(Me) = DialogResult.OK Then
                LoadPatientTreats(patientToUse.PatientID)
            End If
        Else
            Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
            Dim toothList As New List(Of Byte)
            toothList.Add(toothNum)
            Dim del As New DelTreatFrom(patientToUse, toothList)
            If del.ShowDialog(Me) = DialogResult.OK Then
                LoadPatientTreats(patientToUse.PatientID)
                toothList.Clear()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        FlyMenu.Visible = False
    End Sub

    ''' <summary>Same patient + tooth-list resolution as <see cref="btnDelTrts_Click"/>, then opens <see cref="EditMultiTreatFrom"/> for bulk-apply on the selected treatments.</summary>
    Private Sub btnEditMultiTrts_Click(sender As Object, e As EventArgs) Handles btnEditMultiTrts.Click
        Dim patientToUse As Patient = ResolvePatientForBulkEdit()
        If patientToUse Is Nothing Then
            MessageBox.Show(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."),
                            If(Eng, "Patient required", "مطلوب مريض"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim toothList As List(Of Byte) = BuildBulkEditToothList()

        If FlyMenu.Visible Then FlyMenu.Visible = False

        Using bulk As New EditMultiTreatFrom(patientToUse, toothList)
            If bulk.ShowDialog(Me) = DialogResult.OK AndAlso bulk.Saved Then
                LoadPatientTreats(patientToUse.PatientID)
                Me.Focus()
            End If
        End Using
    End Sub

    ''' <summary>Mirrors <see cref="btnDelTrts_Click"/>: workspace patient → TreatsUserControl → FormManager fallback.</summary>
    Private Function ResolvePatientForBulkEdit() As Patient
        Dim patientToUse As Patient = If(CurrentPatient, clsPatient)
        If patientToUse Is Nothing Then
            Dim c As Control = Me
            While c IsNot Nothing
                Dim treatsUC As TreatsUserControl = TryCast(c, TreatsUserControl)
                If treatsUC IsNot Nothing Then
                    patientToUse = treatsUC.CurrentPatient
                    Exit While
                End If
                c = c.Parent
            End While
        End If
        If patientToUse Is Nothing AndAlso FormManager.Instance.IsBasePatientFormOpen Then
            patientToUse = FormManager.Instance.GetCurrentPatient()
        End If
        Return patientToUse
    End Function

    ''' <summary>Same shape as <see cref="btnDelTrts_Click"/>: multi-tooth list → single tooth → whole-mouth (<c>{0}</c>).</summary>
    Private Function BuildBulkEditToothList() As List(Of Byte)
        If selectedTeethList IsNot Nothing AndAlso selectedTeethList.Count > 1 Then
            Return New List(Of Byte)(selectedTeethList)
        End If
        Dim toothList As New List(Of Byte)
        If slctdSVG IsNot Nothing Then
            toothList.Add(Convert.ToByte(slctdSVG.Tag))
        ElseIf selectedTeethList IsNot Nothing AndAlso selectedTeethList.Count = 1 Then
            toothList.Add(selectedTeethList(0))
        Else
            toothList.Add(0)
        End If
        Return toothList
    End Function


#End Region

#Region "FlyOutEventsOld"

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs)
        'Flyout1.Options.CloseOnOuterClick = True
        'JawFlyoutLocalization.ApplyJawTreatmentsFlyoutLanguage(Flyout1)
        'Flyout1.OptionsButtonPanel.Buttons.Item(2).Properties.Appearance.ForeColor = Color.Red
        'JawFlyoutLocalization.SetJawFlyoutDeleteButtonCaption(Flyout1, selectedTeethList.Count > 0)
        'Flyout1.OptionsButtonPanel.Buttons.Item(1).Properties.Visible = showEditTrtBtn
        'grpSlctdTeeth.Visible = Not showEditTrtBtn
        TrtsTreeView.Dock = DockStyle.Fill
        TrtsTreeView.Visible = True
        TrtsTreeView.BringToFront()
    End Sub

    Private Sub Flyout1_ButtonChecked(sender As Object, e As FlyoutPanelButtonCheckedEventArgs) 
        Select Case e.Button.GroupIndex
            Case 1

                TrtsTreeView.Visible = False
                AddedTrtsList.Dock = DockStyle.Fill
                AddedTrtsList.Visible = True
                AddedTrtsList.BringToFront()

                If slctdSVG Is Nothing Then
                    If PatientID <= 0 Then
                        MessageBox.Show(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."), If(Eng, "Patient required", "مطلوب مريض"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                    If Not IsMobile Then
                        SetAddedTrtsListDataSourceFullMouth(PatientID)
                    Else
                        SetAddedMobsListDataSourceFullMouth(PatientID)
                    End If
                Else
                    ' Extract the base name (e.g., "LdOut1" -> "Ld1")
                    Dim baseName As String = slctdSVG.Name.Substring(0, slctdSVG.Name.Length - 4) ' Removes "Out", "Top", or similar
                    Dim numberPart As String = slctdSVG.Name.Substring(slctdSVG.Name.Length - 1)

                    Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
                    Dim toothName As String = $"{baseName}{numberPart}".ToUpper
                    If Not IsMobile Then
                        SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
                    Else
                        SetAddedMobsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
                    End If
                End If

        End Select

    End Sub
    Private Sub Flyout1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs)
        'For i As Integer = 0 To Flyout1.OptionsButtonPanel.Buttons.Count - 1
        '    Dim button As DevExpress.Utils.PeekFormButton = Flyout1.OptionsButtonPanel.Buttons(i)

        '    If button.Tag = e.Button.Tag.ToString() Then
        '        ' Set the clicked button to CheckButton style and mark it as checked
        '        button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton
        '        button.Checked = True
        '    Else
        '        ' Set all other buttons to PushButton style and uncheck them
        '        button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton
        '        button.Checked = False
        '    End If
        'Next
        'Dim tag As String = e.Button.Tag.ToString()
        Select Case e.Button.GroupIndex
            Case 0
                AddedTrtsList.Visible = False
                TrtsTreeView.Dock = DockStyle.Fill
                TrtsTreeView.Visible = True
                TrtsTreeView.BringToFront()
                If slctdSVG IsNot Nothing Then
                    Dim x As Int16 = ExtractDigit(slctdSVG.Name)
                    If IsMobile Then
                        SetMobTreeSnglTooth(x, CByte(slctdSVG.Tag))
                    Else
                        SetTreeTreatsSingleTooth(x, CByte(slctdSVG.Tag))
                    End If
                End If
                e.Button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton
                e.Button.Checked = True
            Case 1
                If showEditTrtBtn Then
                    TrtsTreeView.Visible = False
                    AddedTrtsList.Dock = DockStyle.Fill
                    AddedTrtsList.Visible = True
                    AddedTrtsList.BringToFront()

                    If slctdSVG Is Nothing Then
                        If PatientID <= 0 Then
                            MessageBox.Show(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."), If(Eng, "Patient required", "مطلوب مريض"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If
                        If Not IsMobile Then
                            SetAddedTrtsListDataSourceFullMouth(PatientID)
                        Else
                            SetAddedMobsListDataSourceFullMouth(PatientID)
                        End If
                    Else
                        Dim baseName As String = slctdSVG.Name.Substring(0, slctdSVG.Name.Length - 4)
                        Dim numberPart As String = slctdSVG.Name.Substring(slctdSVG.Name.Length - 1)
                        Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
                        Dim toothName As String = $"{baseName}{numberPart}".ToUpper
                        If Not IsMobile Then
                            SetAddedTrtsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
                        Else
                            SetAddedMobsListDataSource(PatientID, toothNum, GetToothFullName(toothName, TreatsUserControl.AlternateQuadrantLabelsEnabled))
                        End If
                    End If
                End If
            Case 2
                ' Resolve patient for delete: on production CurrentPatient can be Nothing when jaw is hosted in TreatsUserControl
                Dim patientToUse As Patient = If(CurrentPatient, clsPatient)
                If patientToUse Is Nothing Then
                    Dim c As Control = Me
                    While c IsNot Nothing
                        Dim treatsUC As TreatsUserControl = TryCast(c, TreatsUserControl)
                        If treatsUC IsNot Nothing Then
                            patientToUse = treatsUC.CurrentPatient
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
                    Dim del As New DelTreatFrom(patientToUse, selectedTeethList)
                    If del.ShowDialog(Me) = DialogResult.OK Then
                        LoadPatientTreats(patientToUse.PatientID)
                    End If
                ElseIf slctdSVG Is Nothing Then
                    Dim toothList As New List(Of Byte)
                    If selectedTeethList.Count = 1 Then
                        toothList.Add(selectedTeethList(0))
                    Else
                        toothList.Add(0) ' full mouth / chartless (Patient_ToothTrt.ToothNum = 0)
                    End If
                    Dim del As New DelTreatFrom(patientToUse, toothList)
                    If del.ShowDialog(Me) = DialogResult.OK Then
                        LoadPatientTreats(patientToUse.PatientID)
                    End If
                Else
                    Dim toothNum As Byte = Convert.ToByte(slctdSVG.Tag)
                    Dim toothList As New List(Of Byte)
                    toothList.Add(toothNum)
                    Dim del As New DelTreatFrom(patientToUse, toothList)
                    If del.ShowDialog(Me) = DialogResult.OK Then
                        LoadPatientTreats(patientToUse.PatientID)
                        toothList.Clear()
                    End If
                End If
        End Select

    End Sub

    Private Sub Flyout1_Hiding(sender As Object, e As FlyoutPanelEventArgs)
        'For i As Integer = 0 To Flyout1.OptionsButtonPanel.Buttons.Count - 1
        '    Dim button As DevExpress.Utils.PeekFormButton = Flyout1.OptionsButtonPanel.Buttons(i)
        '    ' Set Edit Treats button to PushButton style and uncheck them
        '    button.Style = DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton
        '    button.Checked = False
        'Next
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

#Region "ZSVG"



    '============================================================================================================

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
        'Dim svgItem As SvgImageItem = e.Item
        'MessageBox.Show($"Mouse Entered Item: {svgItem.Id}")
        e.Item.Appearance.Normal.BorderColor = Color.FloralWhite
        e.Item.Appearance.Normal.BorderThickness = 2

        Dim svgItem As SvgImageItem = e.Item
        'MessageBox.Show($"Item Pressed: {svgItem.Id}")
        Dim superTip As New SuperToolTip With {.AllowHtmlText = DefaultBoolean.True}
        ' Create a SuperToolTipTitleItem for the title
        Dim titleItem As New ToolTipTitleItem()
        titleItem.Text = ToothSvgQuadrantNaming.GetToothTooltipHeading(zSvg.Name, zSvg.Tag)

        ' Create a SuperToolTipItem for the content
        Dim regularItem As New ToolTipItem()
        regularItem.Text = String.Empty ' Initialize the text property

        ' Get the base name and tooth information
        Dim baseName As String = ""
        If zSvg.Name.Contains("IN") Then
            baseName = zSvg.Name.Substring(0, zSvg.Name.Length - 3) ' Removes "Out", "Top", or similar
        Else
            baseName = zSvg.Name.Substring(0, zSvg.Name.Length - 4) ' Removes "Out", "Top", or similar
        End If

        Dim numberPart As String = zSvg.Name.Substring(zSvg.Name.Length - 1)
        Dim toothNum As Byte = Convert.ToByte(zSvg.Tag)
        Dim toothName As String = $"{baseName}{numberPart}".ToUpper

        'If svgItem.Id = "IMPLANT" Then

        'End If
        ' Get the treatments for the tooth
        Dim Trts As String = GetTreatment(PatientID, toothNum, svgItem.Id)

        ' Ensure each treatment is added only once
        'For Each trt In Trts
        ' Add each treatment to the tooltip
        Dim backColorHex As String = ColorTranslator.ToHtml(Color.YellowGreen)
        Dim textColorHex As String = ColorTranslator.ToHtml(Color.Blue) ' Default text color
        regularItem.Text += $"<br><b><backcolor={backColorHex}><color={textColorHex}>{Trts}</color></backcolor></b></br>"
        'Next

        ' Apply items to superTip
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
        Dim rdTop As Integer = RdTop1.Top
        Dim ruTop As Integer = RuTop1.Top
        Dim rdOutTop As Integer = RdOut1.Top
        Dim ruOutTop As Integer = RuOut1.Top

        For Each ctl As Control In JawPanel.Controls
            If ctl.Name.Contains("Top") Then
                If ctl.Name.StartsWith("Ru") OrElse ctl.Name.StartsWith("Lu") Then
                    ctl.Top = ruTop
                ElseIf ctl.Name.StartsWith("Rd") OrElse ctl.Name.StartsWith("Ld") Then
                    ctl.Top = rdTop
                End If
            ElseIf ctl.Name.Contains("Out") Then
                If ctl.Name.StartsWith("Ru") OrElse ctl.Name.StartsWith("Lu") Then
                    ctl.Top = ruOutTop
                ElseIf ctl.Name.StartsWith("Rd") OrElse ctl.Name.StartsWith("Ld") Then
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
        Dim leftToRightPrefixes As String() = {"Lu", "Ld"}
        Dim rightToLeftPrefixes As String() = {"Ru", "Rd"}

        ' Align Top views
        AlignHorizontally(leftToRightPrefixes, "Top", False)  ' Lu/Ld: Left-to-right
        AlignHorizontally(rightToLeftPrefixes, "Top", True)   ' Ru/Rd: Right-to-left

        ' Align Out views
        AlignHorizontally(leftToRightPrefixes, "Out", False)  ' Lu/Ld: Left-to-right
        AlignHorizontally(rightToLeftPrefixes, "Out", True)   ' Ru/Rd: Right-to-left
    End Sub

    Private Sub AdultJaw_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        UnwireFlyMenuOutsideClickFilter()
        controlBoundsCache.Clear()
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
        If Not _handlersAttached Then Return
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
        _handlersAttached = False
    End Sub








#End Region
#End Region


End Class
