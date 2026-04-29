Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

''' <summary>
''' Embedded patient shell: custom caption bar (title + close), navigator header, and body panel for module user controls.
''' Embedded patient shell (replaces the removed legacy top-level patient form host).
''' </summary>
Public Class BasePatientWorkspace
    Inherits DevExpress.XtraEditors.XtraUserControl

    Public Property Current_Patient As Patient
    Public Property HeaderPanel As Panel
    Public Property BodyPanel As Panel
    Public Property FormType As String = "Treat"
    Public Event PatientChanged As EventHandler(Of Patient)

    ''' <summary>When True, uses Navigator2 (&quot;old&quot; header in the UI). When False, uses Navigator (&quot;new&quot; header). Matches <c>UseHdrCheckItem</c> via MainView settings.</summary>
    Public Shared Property UseHdrTestModHeader As Boolean = True

    Private hdr As IPatientHeaderControl
    Private CreateBase As Boolean = False
    Private _shutdownDone As Boolean
    Private _titleBar As Panel
    Private _captionLabel As LabelControl
    Private _patientNameLabel As LabelControl
    Private _closeButton As SimpleButton
    Private _titleBarNormalBack As Color
    Private _titleBarNormalFore As Color

    Public ReadOnly Property HeaderControl As IPatientHeaderControl
        Get
            Return hdr
        End Get
    End Property

    Public ReadOnly Property IsKidLabelText As String
        Get
            Return If(hdr?.IsKidLabel?.Text, String.Empty)
        End Get
    End Property

    Public Sub New()
        Me.New("Treat")
    End Sub

    Public Sub New(filterTarget As String)
        InitializeComponent()
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        UpdateStyles()
        FormType = filterTarget
        LookAndFeel.UseDefaultLookAndFeel = True
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        BuildTitleBar()
        CreateLayout(filterTarget)
        ' Title bar last so Top dock paints above header/body (same as legacy form caption strip).
        Me.Controls.Add(_titleBar)
        CreateBase = True
        InitializePatientNavigator()
    End Sub

    Private Sub BuildTitleBar()
        Dim titleBack = Color.Transparent 'Color.FromArgb(45, 45, 48)
        _titleBarNormalBack = titleBack
        _titleBarNormalFore = Color.Gainsboro

        Dim isRtl As Boolean = Not Eng

        _titleBar = New Panel With {
            .Dock = DockStyle.Top,
            .Height = 30,
            .Padding = New Padding(0),
            .TabStop = False,
            .BackColor = titleBack
        }

        ' Close button owns its edge via Dock (right in English, left in Arabic) -- the
        ' caption and patient-name labels are positioned absolutely by LayoutTitleBar so
        ' (a) the caption hugs its text with no trailing gap and (b) the patient name
        ' sits at the TRUE horizontal center of the bar, not the center of whatever
        ' space happens to remain after the caption/close are laid out.
        _closeButton = New SimpleButton With {
            .Dock = If(isRtl, DockStyle.Left, DockStyle.Right),
            .Width = 40,
            .Text = "×",
            .TabIndex = 999,
            .AllowFocus = False,
            .RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
        }
        _closeButton.LookAndFeel.UseDefaultLookAndFeel = True
        _closeButton.LookAndFeel.ParentLookAndFeel = LookAndFeel
        _closeButton.ButtonStyle = BorderStyles.NoBorder
        _closeButton.Appearance.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular)
        _closeButton.Appearance.ForeColor = Color.DarkBlue
        _closeButton.Appearance.BackColor = titleBack
        _closeButton.Appearance.Options.UseBackColor = True
        _closeButton.Appearance.Options.UseForeColor = True
        AddHandler _closeButton.Click, AddressOf OnCloseClick
        AddHandler _closeButton.MouseEnter, AddressOf OnCloseButtonMouseEnter
        AddHandler _closeButton.MouseLeave, AddressOf OnCloseButtonMouseLeave

        _captionLabel = New LabelControl With {
            .AutoSizeMode = LabelAutoSizeMode.Default,
            .Padding = New Padding(10, 0, 10, 0),
            .Text = FormTitleHelper.GetModuleTitle(FormType, Eng)
        }
        _captionLabel.Appearance.ForeColor = Color.PaleVioletRed
        _captionLabel.Appearance.Font = New Font(Me.Font.FontFamily, 12.0F, FontStyle.Bold)
        _captionLabel.Appearance.TextOptions.HAlignment = If(isRtl, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.HorzAlignment.Near)
        _captionLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

        _patientNameLabel = New LabelControl With {
            .AutoSizeMode = LabelAutoSizeMode.Default,
            .Text = String.Empty
        }
        _patientNameLabel.Appearance.ForeColor = Color.Red
        _patientNameLabel.Appearance.Font = New Font(Me.Font.FontFamily, 14.0F, FontStyle.Bold)
        _patientNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        _patientNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

        _titleBar.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
        _captionLabel.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
        _patientNameLabel.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)

        _titleBar.Controls.Add(_closeButton)
        _titleBar.Controls.Add(_captionLabel)
        _titleBar.Controls.Add(_patientNameLabel)
        _patientNameLabel.BringToFront()

        AddHandler _titleBar.Resize, AddressOf LayoutTitleBar
        AddHandler _captionLabel.SizeChanged, AddressOf LayoutTitleBar
        AddHandler _captionLabel.TextChanged, AddressOf LayoutTitleBar
        AddHandler _patientNameLabel.SizeChanged, AddressOf LayoutTitleBar
        AddHandler _patientNameLabel.TextChanged, AddressOf LayoutTitleBar
        LayoutTitleBar(Nothing, EventArgs.Empty)

        Me.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
    End Sub

    ''' <summary>Positions the caption against the bar's outer edge (left in English,
    ''' right in Arabic) and the patient-name label at the geometric center of the
    ''' bar. Called whenever the bar resizes or either label changes size/text.</summary>
    Private Sub LayoutTitleBar(sender As Object, e As EventArgs)
        If _titleBar Is Nothing Then Return
        Dim barW As Integer = _titleBar.ClientSize.Width
        Dim barH As Integer = _titleBar.ClientSize.Height
        If barW <= 0 OrElse barH <= 0 Then Return
        Dim isRtl As Boolean = Not Eng

        If _captionLabel IsNot Nothing Then
            _captionLabel.Top = Math.Max(0, (barH - _captionLabel.Height) \ 2)
            If isRtl Then
                _captionLabel.Left = Math.Max(0, barW - _captionLabel.Width)
            Else
                _captionLabel.Left = 0
            End If
        End If

        If _patientNameLabel IsNot Nothing Then
            _patientNameLabel.Top = Math.Max(0, (barH - _patientNameLabel.Height) \ 2)
            _patientNameLabel.Left = Math.Max(0, (barW - _patientNameLabel.Width) \ 2)
        End If
    End Sub

    Private Sub OnCloseButtonMouseEnter(sender As Object, e As EventArgs)
        _closeButton.Appearance.BackColor = Color.FromArgb(232, 17, 35)
        _closeButton.Appearance.ForeColor = Color.DarkBlue
        _closeButton.Appearance.Options.UseBackColor = True
        _closeButton.Appearance.Options.UseForeColor = True
    End Sub

    Private Sub OnCloseButtonMouseLeave(sender As Object, e As EventArgs)
        _closeButton.Appearance.BackColor = _titleBarNormalBack
        _closeButton.Appearance.ForeColor = Color.DarkBlue ' _titleBarNormalFore
        _closeButton.Appearance.Options.UseBackColor = True
        _closeButton.Appearance.Options.UseForeColor = True
    End Sub

    Private Sub OnCloseClick(sender As Object, e As EventArgs)
        FormManager.Instance.CloseBaseForm()
    End Sub

    Private Sub CreateLayout(filterTarget As String)
        HeaderPanel = New Panel With {.Dock = DockStyle.Top, .Height = 70}
        ' UseHdrTestModHeader True = menu &quot;old&quot; style -> Navigator2 (replaced HdrTestMod). False = &quot;new&quot; style -> Navigator.

        hdr = New Navigator3(filterTarget) With {.Dock = DockStyle.Fill, .BorderStyle = BorderStyle.Fixed3D}

        HeaderPanel.Controls.Add(CType(hdr, Control))
        BodyPanel = New WorkspaceDoubleBufferedPanel With {.Dock = DockStyle.Fill}
        Me.Controls.Add(BodyPanel)
        Me.Controls.Add(HeaderPanel)
    End Sub

    Private Sub HandlePatientChanged(sender As Object, e As PatientChangedEventArgs)
        If e.NewPatient Is Nothing Then
            If CreateBase Then
                LoadPatientData(Nothing)
            End If
            Return
        End If
        e.NewPatient.IsKid = e.ShowHimAsKid
        Dim _Patient As Patient = e.NewPatient
        If Current_Patient Is Nothing OrElse Current_Patient.PatientID <> _Patient.PatientID Then
            LoadPatientData(_Patient)
        End If
        If e.ForceUpdate Then
            LoadPatientData(_Patient)
        End If
    End Sub

    Public Overridable Sub LoadPatientData(patient As Patient)
        If Not CreateBase Then Return
        Me.SuspendLayout()
        If BodyPanel.Controls.Count > 0 Then
            BodyPanel.SuspendLayout()
        End If
        Try
            Current_Patient = patient
            UpdateFormTitle(patient)

            Dim layoutSuspended As Boolean = False
            If BodyPanel.Controls.Count > 0 Then
                BodyPanel.SuspendLayout()
                layoutSuspended = True
            End If
            RaiseEvent PatientChanged(Me, patient)
            If BodyPanel.Controls.Count > 0 Then
                Dim currentControl = BodyPanel.Controls(0)
                Dim patientAwareControl = TryCast(currentControl, IPatientAwareUserControl)
                If patientAwareControl IsNot Nothing Then
                    Dim pid = If(patient IsNot Nothing, patient.PatientID, -1)
                    patientAwareControl.LoadPatientData(pid)
                End If
            End If
            If layoutSuspended Then
                BodyPanel.ResumeLayout(False)
            End If
        Finally
            If BodyPanel.Controls.Count > 0 Then
                BodyPanel.ResumeLayout()
            End If
            Me.ResumeLayout()
        End Try
    End Sub

    Public Sub UpdatePatientBalance(patient As Patient)
        If patient Is Nothing OrElse hdr Is Nothing Then Return
        hdr.LoadBal(patient.PatientID)
    End Sub

    ''' <summary>Updates header filter/patient; optionally queues a body <c>LoadPatientData</c> (skipped when the body is about to be replaced).</summary>
    Public Sub UpdateFilterTarget(filterTarget As String, Optional passedPatient As Patient = Nothing, Optional refreshEmbeddedBody As Boolean = True)
        If hdr IsNot Nothing Then
            Dim currentPatient = If(passedPatient, Current_Patient)
            hdr.UpdateFilterTarget(filterTarget, currentPatient)
            FormType = filterTarget
            If hdr.Current_Patient IsNot Nothing Then
                UpdateFormTitle(hdr.Current_Patient)
            ElseIf currentPatient IsNot Nothing Then
                UpdateFormTitle(currentPatient)
            End If

            If refreshEmbeddedBody Then
                Me.BeginInvoke(Sub()
                                   If BodyPanel.Controls.Count > 0 Then
                                       Dim currentControl = BodyPanel.Controls(0)
                                       Dim patientAwareControl = TryCast(currentControl, IPatientAwareUserControl)
                                       If patientAwareControl IsNot Nothing Then
                                           Dim pid = If(hdr.Current_Patient IsNot Nothing, hdr.Current_Patient.PatientID, -1)
                                           patientAwareControl.LoadPatientData(pid)
                                       End If
                                   End If
                               End Sub)
            End If
        End If
    End Sub

    Public Sub UpdateFormTitle(patient As Patient)
        Dim moduleTitle = FormTitleHelper.GetModuleTitle(FormType, Eng)
        If _captionLabel IsNot Nothing Then
            _captionLabel.Text = moduleTitle
        End If
        If _patientNameLabel IsNot Nothing Then
            _patientNameLabel.Text = If(patient IsNot Nothing, patient.PatientName, String.Empty)
        End If

        Dim shell = Me.FindForm()
        Dim mv3 = TryCast(shell, MainView3)
        If mv3 IsNot Nothing AndAlso mv3.stFormNameTxt IsNot Nothing Then
            mv3.stFormNameTxt.Caption = moduleTitle
        End If
        Dim mv1 = TryCast(shell, MainView1)
        If mv1 IsNot Nothing AndAlso mv1.stFormNameTxt IsNot Nothing Then
            mv1.stFormNameTxt.Caption = moduleTitle
        End If
    End Sub

    Protected Overridable Sub InitializePatientNavigator()
    End Sub

    Protected Overrides Sub OnParentChanged(e As EventArgs)
        MyBase.OnParentChanged(e)
        SyncLookAndFeelWithShell()
    End Sub

    Private Sub SyncLookAndFeelWithShell()
        Dim host = FindForm()
        If host Is Nothing Then Return
        Dim xf = TryCast(host, XtraForm)
        If xf IsNot Nothing Then
            LookAndFeel.ParentLookAndFeel = xf.LookAndFeel
        End If
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        SyncLookAndFeelWithShell()
        If Current_Patient Is Nothing AndAlso hdr IsNot Nothing AndAlso hdr.Current_Patient IsNot Nothing Then
            LoadPatientData(hdr.Current_Patient)
        ElseIf Current_Patient IsNot Nothing Then
            UpdateFormTitle(Current_Patient)
        End If
    End Sub

    Public Sub Shutdown()
        If _shutdownDone Then Return
        _shutdownDone = True
        RemoveHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        If BodyPanel IsNot Nothing Then
            For Each ctl As Control In BodyPanel.Controls
                Dim uc = TryCast(ctl, UserControl)
                If uc IsNot Nothing Then
                    PatientAwareHelper.UnsubscribeFromPatientChanges(uc)
                End If
                ctl.Dispose()
            Next
            BodyPanel.Controls.Clear()
        End If
        If hdr IsNot Nothing Then
            Dim c = TryCast(hdr, Control)
            If c IsNot Nothing Then c.Dispose()
            hdr = Nothing
        End If
        Current_Patient = Nothing
    End Sub
End Class

Friend Class WorkspaceDoubleBufferedPanel
    Inherits Panel
    Public Sub New()
        MyBase.New()
        Me.DoubleBuffered = True
    End Sub
End Class
