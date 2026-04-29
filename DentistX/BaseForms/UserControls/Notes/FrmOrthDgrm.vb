Imports System.Collections.Concurrent





Public Class FrmOrthDgrm

        Public Sub New()
            Dim sw = StartTimer()
            ' This call is required by the designer.
            InitializeComponent()
            'Dim sw As New Stopwatch
            'sw.Start()
            StoreOriginalBounds(Me)
            ' Add any initialization after the InitializeComponent() call.
            AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        'Attach the event handler
        'AddHandler AdultKID1.ToothDoubleClick, AddressOf HandleToothDoubleClick
        Me.Icon = AppIcon
        LogTime("Public Sub New", Me.Name, sw)
        'sw.Stop()
        'LogToFile("New Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)

    End Sub

        Public Sub New(ByVal clsPatient As Patient)
            Dim sw = StartTimer()
            ' This call is required by the designer.
            InitializeComponent()
            StoreOriginalBounds(Me)
            ' Add any initialization after the InitializeComponent() call.
            AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        'Attach the event handler
        'AddHandler AdultKID1.ToothDoubleClick, AddressOf HandleToothDoubleClick
        CurrentPatient = clsPatient
            LogTime("Public Sub New", Me.Name, sw)
        Me.Icon = AppIcon
    End Sub

        ' Define a public event to bubble it up
        Public Event ToothClicked As EventHandler(Of ToothDoubleClickEvent)

        Private Sub HandleToothDoubleClick(sender As Object, e As ToothDoubleClickEvent)
            ' Raise this up to the host form (JawsForm2New)
            RaiseEvent ToothClicked(Me, e)
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

    'Dim clsPatientData As New PatientDATA
    'Dim clsPatient As Patient
    'Dim CurrentPatient As Patient
    Private Sub HandlePatientChanged(sender As Object, e As PatientChangedEventArgs) 'Handles AdultFullJaw1.ParentChanged

        Dim sw = StartTimer()
        If e.NewPatient Is Nothing Then Exit Sub
        CurrentPatient = e.NewPatient
        ' Do whatever is needed when a new patient is selected
        If CurrentPatient.PatientID > 0 Then

        End If
        LogTime(NameOf(HandlePatientChanged), Me.Name, sw)
    End Sub

        Private Sub FrmOrthDgrm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
            RemoveHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        End Sub

        'Private Sub AdultKID1_ToothDoubleClick(sender As Object, e As ToothDoubleClickEvent) Handles AdultKID1.ToothDoubleClick
        '    'JawsForm2New.SlctdToothCTL1.Visible = True
        '    'JawsForm2New.SlctdToothCTL1.BringToFront()
        '    'JawsForm2New.SlctdToothCTL1.ShowSlctdToothNew(e.PatientID, e.SVG) ', e.TreatList)

        'End Sub

        Private Sub FrmOrthDgrm_Load(sender As Object, e As EventArgs) Handles Me.Load
            If CurrentPatient Is Nothing Then Exit Sub

    End Sub

    Private Sub FrmOrthDgrm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            ResizeControlsProportionally()
        End Sub

        Private Sub FrmOrthDgrm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
            Application.DoEvents()
        End Sub
    End Class