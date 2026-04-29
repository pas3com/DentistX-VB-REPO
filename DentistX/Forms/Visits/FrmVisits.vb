
Imports System.Collections.Concurrent

Public Class FrmVisits

    Public Sub New()
        Dim sw = StartTimer()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        StoreOriginalBounds(Me)
        ' Add any initialization after the InitializeComponent() call.
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged

        LogTime("Public Sub New", Me.Name, sw)


    End Sub

    Public Sub New(ByVal clsPatient As Patient)
        Dim sw = StartTimer()
        ' This call is required by the designer.
        InitializeComponent()
        StoreOriginalBounds(Me)
        ' Add any initialization after the InitializeComponent() call.
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        'Attach the event handler
        CurrentPatient = clsPatient
        LogTime("Public Sub New", Me.Name, sw)

    End Sub

    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1156
    Private Const OriginalPanelHeight As Integer = 654
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()

        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
        sw.Stop()
        'LogToFile("StoreOriginalBounds Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)

    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
        sw.Stop()
    End Sub

    Private Sub HandlePatientChanged(sender As Object, e As PatientChangedEventArgs) 'Handles AdultFullJaw1.ParentChanged
        ' Update JawsForm2 based on new patient selection
        Dim sw = StartTimer()
        If e.NewPatient Is Nothing Then Exit Sub
        'Dim sw As New Stopwatch
        'sw.Start()
        CurrentPatient = e.NewPatient
        ' Do whatever is needed when a new patient is selected
        If CurrentPatient.PatientID > 0 Then
            VisitCalendarDayControl1.FilterPatient = CurrentPatient.PatientName
            Me.Text = CurrentPatient.PatientName
        End If

        'sw.Stop()
        'LogToFile("HandlePatientChanged Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
        LogTime(NameOf(HandlePatientChanged), Me.Name, sw)
    End Sub

    Private Sub FrmVisits_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
    End Sub

    Private Sub FrmVisits_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CurrentPatient Is Nothing Then Exit Sub
        Me.Text = CurrentPatient.PatientName
    End Sub

    Private Sub FrmVisits_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeControlsProportionally()
    End Sub

    Private Sub FrmVisits_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.DoEvents()
    End Sub

    Private Sub FrmVisits_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    End Sub



End Class