Imports System.ComponentModel
Imports System.Reflection
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraEditors

''' <summary>Partial for WinForms designer; InitializeComponent must run before other setup.</summary>
Partial Public Class TreatsUserControl
    Inherits XtraUserControl
    Implements IPatientAwareUserControl

    ' Do not use "As New Panel With { ... }" on WithEvents fields — breaks CodeDom deserialization in the designer.
    Friend WithEvents mainPanel As Panel
    Private frmPanel As Panel
    Private rightPanel As Panel
    Private splitContainer As SplitContainer
    ' Your existing right panel controls
    Friend WithEvents chkNaming As DevExpress.XtraEditors.CheckEdit

    ''' <summary>When True, UI strings use UR/UL/LD/LL quarter labels and UPPER/LOWER-before-LEFT/RIGHT tooth names.</summary>
    Public Shared AlternateQuadrantLabelsEnabled As Boolean
    Friend WithEvents chkMobile As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkDevider As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents HealthLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpOptions As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpHealth As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpSvgs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FullSv As DevExpress.XtraEditors.SvgImageBox
    'Friend WithEvents btnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents UpperSv As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents LowerSv As DevExpress.XtraEditors.SvgImageBox
    ' Control caching and state tracking
    Private _currentPatient As Patient
    Friend Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property
    Private _currentJawType As Type
    Private _currentJawControl As UserControl
    Private _jawControlCache As New Dictionary(Of Type, UserControl)
    Private _isInitialLoad As Boolean = True
    ''' <summary>Stops <see cref="chkMobile"/> from calling workspace/jaw hooks during programmatic updates.</summary>
    Private _suppressChkMobileEvent As Boolean
    Private components As System.ComponentModel.IContainer
    Private _SV As String = "FULL" ' Track current SVG selection
    ' Left panel for jaw views only (color keys etc.). Visible only when frmPanel hosts a jaw control.
    'Private _jawsOnlyPanel As Panel
    'Private Const JawsOnlyPanelWidth As Integer = 220
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        InitializeRuntimeChrome()
        Me.Controls.Add(mainPanel)
        Me.Controls.SetChildIndex(mainPanel, 0)
        If LicenseManager.UsageMode = LicenseUsageMode.Designtime Then
            Return
        End If
        SetupJawsOnlyPanel()
        CreateTreatsLayout()
    End Sub

    Public Sub New(ByVal filterTarget As String)
        MyBase.New()
        InitializeComponent()
        InitializeRuntimeChrome()
        Me.Controls.Add(mainPanel)
        Me.Controls.SetChildIndex(mainPanel, 0)
        If LicenseManager.UsageMode = LicenseUsageMode.Designtime Then
            Return
        End If
        SetupJawsOnlyPanel()
        CreateTreatsLayout()
    End Sub

    Private Sub InitializeRuntimeChrome()
        mainPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent
        }
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
        ' Formerly TreatsUserControl.ar.resx ($this.RightToLeft); keep Arabic mirroring at runtime.
        If LicenseManager.UsageMode = LicenseUsageMode.Runtime AndAlso Not Eng Then
            Me.RightToLeft = RightToLeft.Yes
        End If
    End Sub
    ''' <summary>Creates the left docked panel used only when a jaw control is shown in frmPanel (AdultJaw, KidJaw, etc.).</summary>
    Private Sub SetupJawsOnlyPanel()
        '_jawsOnlyPanel = New Panel With {
        '    .Dock = DockStyle.Left,
        '    .Width = JawsOnlyPanelWidth,
        '    .BackColor = Color.White,
        '    .BorderStyle = BorderStyle.FixedSingle,
        '    .Visible = False
        '}
        '' Add left panel first so it reserves left edge; mainPanel (Fill) is added after and gets the rest.
        'Me.Controls.Add(_jawsOnlyPanel)
    End Sub
    Private Sub CreateTreatsLayout()
        mainPanel.Controls.Clear()
        splitContainer = New SplitContainer With {
            .Dock = DockStyle.Fill,
            .Orientation = Orientation.Vertical,
                 .SplitterDistance = mainPanel.Width - 100 ' frmPanel gets most width, rightPanel gets 100px
    }

        ' Set fixed width for right panel
        splitContainer.Panel2MinSize = 120
        splitContainer.FixedPanel = FixedPanel.Panel2
        frmPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent
        }
        rightPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent
        }
        splitContainer.Panel1.Controls.Add(frmPanel)
        splitContainer.Panel2.Controls.Add(rightPanel)
        mainPanel.Controls.Add(splitContainer)
        CreateRightPanelContents()
    End Sub
    Private Sub CreateRightPanelContents()
        ' Create table layout with 3 equal rows
        Dim tableLayout As New TableLayoutPanel()
        tableLayout.Dock = DockStyle.Fill
        tableLayout.RowCount = 3
        tableLayout.ColumnCount = 1
        tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 23.33F))
        tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 53.33F))
        tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 23.33F))
        tableLayout.BorderStyle = BorderStyle.Fixed3D
        ' Initialize controls
        InitializeRightPanelControls()
        ' Add controls to table layout rows
        tableLayout.Controls.Add(GrpHealth, 0, 0)    ' Top row - Health
        tableLayout.Controls.Add(grpSvgs, 0, 1)      ' Middle row - SVGs  
        tableLayout.Controls.Add(grpOptions, 0, 2) ' Bottom row - Options
        ' Set dock styles
        GrpHealth.Dock = DockStyle.Fill
        grpSvgs.Dock = DockStyle.Fill
        grpOptions.Dock = DockStyle.Fill
        ' Add table layout to right panel
        rightPanel.Controls.Add(tableLayout)
        SetupFonts()
        ApplyHealthLabelAppearance(HealthLbl.Text)
        ' Set up event handlers and initial state
        SetupEventHandlers()
        AlternateQuadrantLabelsEnabled = chkNaming.Checked
        UnSelect()
        selectFULL()
        ApplyMobileCheckboxVisibilityAndState()
    End Sub
    Private Sub SetupFonts()
        ' Set fonts for controls
        Dim boldFont As New Font("Calibri", 10, FontStyle.Bold)
        chkNaming.Font = boldFont
        chkMobile.Font = boldFont
        chkDevider.Font = boldFont
        grpSvgs.AppearanceCaption.Font = boldFont
        grpOptions.AppearanceCaption.Font = boldFont
        GrpHealth.AppearanceCaption.Font = boldFont
    End Sub

    ''' <summary>Blue Calibri bold 9 when healthy; red Calibri bold 10 otherwise.</summary>
    Private Sub ApplyHealthLabelAppearance(healthText As String)
        If HealthLbl Is Nothing Then Return
        Dim t = If(healthText, String.Empty).Trim()
        Dim isHealthy = String.Equals(t, "سليم", StringComparison.Ordinal) OrElse
            String.Equals(t, "Healthy", StringComparison.OrdinalIgnoreCase)
        HealthLbl.LookAndFeel.UseDefaultLookAndFeel = False
        HealthLbl.LookAndFeel.Style = LookAndFeelStyle.Flat
        HealthLbl.Appearance.Options.UseFont = True
        HealthLbl.Appearance.Options.UseForeColor = True
        If isHealthy Then
            HealthLbl.Appearance.ForeColor = Color.Blue
            HealthLbl.Appearance.Font = New Font("Calibri", 10.0F, FontStyle.Bold)
        Else
            HealthLbl.Appearance.ForeColor = Color.Red
            HealthLbl.Appearance.Font = New Font("Calibri", 12.0F, FontStyle.Bold)
        End If
    End Sub

    ''' <summary>Workspace/header/FormManager first, then DB by id so health loads when only id is passed.</summary>
    Private Function ResolvePatientForTreats(patientId As Integer) As Patient
        Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
        If parentWs IsNot Nothing AndAlso parentWs.Current_Patient IsNot Nothing AndAlso parentWs.Current_Patient.PatientID = patientId Then
            Return parentWs.Current_Patient
        End If
        If FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            Return FormManager.Instance.GetCurrentPatient()
        End If
        If parentWs IsNot Nothing AndAlso parentWs.HeaderControl IsNot Nothing AndAlso parentWs.HeaderControl.Current_Patient IsNot Nothing AndAlso parentWs.HeaderControl.Current_Patient.PatientID = patientId Then
            Return parentWs.HeaderControl.Current_Patient
        End If
        If patientId <= 0 Then Return Nothing
        Using pd As New PatientDATA()
            Return pd.Select_RecordByID(patientId)
        End Using
    End Function

    Private Sub InitializeRightPanelControls()
        ' GrpHealth
        GrpHealth = New DevExpress.XtraEditors.GroupControl()
        HealthLbl = New DevExpress.XtraEditors.LabelControl()
        GrpHealth.AppearanceCaption.Options.UseTextOptions = True
        GrpHealth.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GrpHealth.Text = If(Eng, "Patient Health", "الحالة الصحية")
        GrpHealth.Dock = DockStyle.Top
        GrpHealth.Height = 80
        GrpHealth.Width = 100
        HealthLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        HealthLbl.Appearance.Options.UseTextOptions = True
        HealthLbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        HealthLbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        HealthLbl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        HealthLbl.Dock = DockStyle.Fill
        HealthLbl.Text = If(Eng, "Health information will appear here", "المعلومات الصحية")
        HealthLbl.LookAndFeel.UseDefaultLookAndFeel = False
        HealthLbl.LookAndFeel.Style = LookAndFeelStyle.Flat
        GrpHealth.Controls.Add(HealthLbl)
        ' grpOptions
        grpOptions = New DevExpress.XtraEditors.GroupControl()
        chkNaming = New DevExpress.XtraEditors.CheckEdit()
        chkMobile = New DevExpress.XtraEditors.CheckEdit()
        chkDevider = New DevExpress.XtraEditors.CheckEdit()
        'btnExit = New DevExpress.XtraEditors.SimpleButton()
        grpOptions.AppearanceCaption.Options.UseTextOptions = True
        grpOptions.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        grpOptions.Text = If(Eng, "Options", "خيارات")
        grpOptions.Dock = DockStyle.Top
        grpOptions.Height = 120
        grpOptions.Width = 100
        chkNaming.Properties.Caption = If(Eng, "New Naming", "التسمية الجديدة")
        chkNaming.Dock = DockStyle.Top
        chkNaming.Height = 25
        chkNaming.Checked = My.Settings.Naming
        ' Mobile tree mode retired: dentures appear in the main tree (TrtSourceHelper); checkbox stays hidden.
        chkMobile.Properties.Caption = If(Eng, "Mobile Treatment", "التركيبات المتحركة")
        chkMobile.Dock = DockStyle.Top
        chkMobile.Height = 25
        chkMobile.Visible = False
        chkMobile.Checked = False
        chkDevider.Properties.Caption = If(Eng, "Show Divider", "إظهار المحاور")
        chkDevider.Dock = DockStyle.Top
        chkDevider.Height = 25
        chkDevider.Checked = True
        'btnExit.Text = If(Eng, "Exit", "خروج")
        'btnExit.Dock = DockStyle.Top
        'btnExit.Height = 30
        grpOptions.Controls.Add(chkNaming)
        grpOptions.Controls.Add(chkDevider)
        grpOptions.Controls.Add(chkMobile)
        ' grpSvgs
        grpSvgs = New DevExpress.XtraEditors.GroupControl()
        FullSv = New DevExpress.XtraEditors.SvgImageBox()
        UpperSv = New DevExpress.XtraEditors.SvgImageBox()
        LowerSv = New DevExpress.XtraEditors.SvgImageBox()
        grpSvgs.AppearanceCaption.Options.UseTextOptions = True
        grpSvgs.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        grpSvgs.Text = If(Eng, "Jaw Selection", "اختيار الفك")
        grpSvgs.Dock = DockStyle.Fill
        grpSvgs.Width = 100
        ' Configure and position SVGs
        ConfigureAndPositionSvgs()
        grpSvgs.Controls.Add(LowerSv)
        grpSvgs.Controls.Add(FullSv)
        grpSvgs.Controls.Add(UpperSv)
    End Sub
#Region "SVG Configuration Methods"
    Private Sub ConfigureAndPositionSvgs()
        ' Clear any existing controls first
        grpSvgs.Controls.Clear()
        ' Configure SVGs
        ConfigureSvg(UpperSv, "Upper Jaw")
        ConfigureSvg(FullSv, "Full Jaw")
        ConfigureSvg(LowerSv, "Lower Jaw")
        ' Position SVGs with proper spacing
        PositionSvg(UpperSv, 0)  ' Top
        PositionSvg(FullSv, 1)   ' Middle
        PositionSvg(LowerSv, 2)  ' Bottom
        ' Add SVGs to group control IN ORDER
        grpSvgs.Controls.Add(UpperSv)
        grpSvgs.Controls.Add(FullSv)
        grpSvgs.Controls.Add(LowerSv)
        ' Add resize handler to maintain positioning
        AddHandler grpSvgs.SizeChanged, AddressOf grpSvgs_SizeChanged
    End Sub
    Private Sub ConfigureSvg(svgBox As DevExpress.XtraEditors.SvgImageBox, tooltip As String)
        svgBox.Size = New Size(60, 60)
        Try
            svgBox.SvgImage = GetSvgImageForJaw(tooltip)
        Catch
            svgBox.SvgImage = Nothing
        End Try
        svgBox.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom
        svgBox.Tag = "None"
        svgBox.ToolTip = tooltip
    End Sub
    Private Sub PositionSvg(svgBox As DevExpress.XtraEditors.SvgImageBox, rowIndex As Integer)
        If grpSvgs.Width <= 0 Or grpSvgs.Height <= 0 Then Return
        Dim padding As Integer = 15
        Dim svgHeight As Integer = 60
        Dim titleHeight As Integer = 30 ' Safe margin for group box title
        Dim totalAvailableHeight As Integer = grpSvgs.Height - titleHeight - (padding * 2)
        Dim spaceBetweenSvgs As Integer = (totalAvailableHeight - (svgHeight * 3)) \ 2
        Dim yPosition As Integer = titleHeight + padding + (rowIndex * (svgHeight + spaceBetweenSvgs))
        Dim xPosition As Integer = (grpSvgs.Width - svgBox.Width) \ 2
        svgBox.Location = New Point(xPosition, yPosition)
    End Sub
    Private Sub grpSvgs_SizeChanged(sender As Object, e As EventArgs)
        RepositionAllSvgs()
    End Sub
    Private Sub RepositionAllSvgs()
        PositionSvg(UpperSv, 0)
        PositionSvg(FullSv, 1)
        PositionSvg(LowerSv, 2)
    End Sub
#End Region
    Private Function GetSvgImageForJaw(jawType As String) As DevExpress.Utils.Svg.SvgImage
        Select Case jawType
            Case "Full Jaw"
                Return GetFullJawSvg()
            Case "Upper Jaw"
                Return GetUpperJawSvg()
            Case "Lower Jaw"
                Return GetLowerJawSvg()
            Case Else
                Return GetFullJawSvg()
        End Select
    End Function
    Private Sub SetupEventHandlers()
        AddHandler UpperSv.Click, AddressOf UpperSv_Click
        AddHandler FullSv.Click, AddressOf FullSv_Click
        AddHandler LowerSv.Click, AddressOf LowerSv_Click
        AddHandler UpperSv.MouseEnter, AddressOf Svg_MouseEnter
        AddHandler FullSv.MouseEnter, AddressOf Svg_MouseEnter
        AddHandler LowerSv.MouseEnter, AddressOf Svg_MouseEnter
        AddHandler UpperSv.MouseLeave, AddressOf Svg_MouseLeave
        AddHandler FullSv.MouseLeave, AddressOf Svg_MouseLeave
        AddHandler LowerSv.MouseLeave, AddressOf Svg_MouseLeave
        AddHandler chkDevider.CheckedChanged, AddressOf chkDevider_CheckedChanged
        AddHandler chkMobile.CheckedChanged, AddressOf chkMobile_CheckedChanged
        AddHandler chkNaming.CheckedChanged, AddressOf chkNaming_CheckedChanged
    End Sub

    Private Sub chkNaming_CheckedChanged(sender As Object, e As EventArgs)
        AlternateQuadrantLabelsEnabled = chkNaming.Checked
        My.Settings.Naming = chkNaming.Checked
        My.Settings.Save()
    End Sub

    Private Sub PersistNamingSetting()
        If chkNaming Is Nothing Then Return
        My.Settings.Naming = chkNaming.Checked
        My.Settings.Save()
    End Sub
#Region "SVG Handling Methods"
    Private Sub selectFULL()
        UnSelect()
        UpperSv.Tag = "None"
        FullSv.Tag = "Slct"
        LowerSv.Tag = "None"
        _SV = "FULL"
        HighlightSvgCrowns(FullSv)
        ShowJawForSvgSelection()
    End Sub
    Private Sub selectLOWER()
        UnSelect()
        UpperSv.Tag = "None"
        FullSv.Tag = "None"
        LowerSv.Tag = "Slct"
        _SV = "LOWER"
        HighlightSvgCrowns(LowerSv)
        ShowJawForSvgSelection()
    End Sub
    Private Sub selectUPPER()
        UnSelect()
        UpperSv.Tag = "Slct"
        FullSv.Tag = "None"
        LowerSv.Tag = "None"
        _SV = "UPPER"
        HighlightSvgCrowns(UpperSv)
        ShowJawForSvgSelection()
    End Sub
    Private Sub HighlightSvgCrowns(svg As DevExpress.XtraEditors.SvgImageBox)
        If svg?.SvgImage Is Nothing OrElse svg.RootItems Is Nothing Then Return
        For Each item As SvgImageItem In svg.RootItems
            If Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("CROWN") Then
                item.Appearance.Normal.BorderColor = Color.Yellow
                item.Appearance.Normal.BorderThickness = 2
                item.Appearance.Normal.FillColor = Color.Wheat
            End If
        Next
    End Sub
    Private Sub Svg_MouseEnter(sender As Object, e As EventArgs)
        Dim svg As DevExpress.XtraEditors.SvgImageBox = CType(sender, DevExpress.XtraEditors.SvgImageBox)
        If svg?.SvgImage Is Nothing OrElse svg.RootItems Is Nothing Then Return
        For Each item As SvgImageItem In svg.RootItems
            If Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("CROWN") Then
                item.Appearance.Normal.BorderColor = Color.Fuchsia
                item.Appearance.Normal.BorderThickness = 3
                item.Appearance.Normal.FillColor = Color.Blue
            End If
        Next
    End Sub
    Private Sub Svg_MouseLeave(sender As Object, e As EventArgs)
        Dim svg As DevExpress.XtraEditors.SvgImageBox = CType(sender, DevExpress.XtraEditors.SvgImageBox)
        If svg?.SvgImage Is Nothing OrElse svg.RootItems Is Nothing Then Return
        For Each item As SvgImageItem In svg.RootItems
            If Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("CROWN") Then
                If svg.Tag?.ToString() = "None" Then
                    item.Appearance.Normal.BorderColor = Color.Black
                    item.Appearance.Normal.BorderThickness = 1
                    item.Appearance.Normal.FillColor = Color.Transparent
                Else
                    item.Appearance.Normal.BorderColor = Color.Yellow
                    item.Appearance.Normal.BorderThickness = 2
                    item.Appearance.Normal.FillColor = Color.Wheat
                End If
            End If
        Next
    End Sub
    Private Sub UpperSv_Click(sender As Object, e As EventArgs)
        selectUPPER()
    End Sub
    Private Sub FullSv_Click(sender As Object, e As EventArgs)
        selectFULL()
    End Sub
    Private Sub LowerSv_Click(sender As Object, e As EventArgs)
        selectLOWER()
    End Sub
    Private Sub UnSelect()
        UpperSv.Tag = "None"
        FullSv.Tag = "None"
        LowerSv.Tag = "None"
        For Each ct As Control In grpSvgs.Controls
            If TypeOf ct Is DevExpress.XtraEditors.SvgImageBox Then
                Dim sv As DevExpress.XtraEditors.SvgImageBox = CType(ct, DevExpress.XtraEditors.SvgImageBox)
                If sv?.SvgImage Is Nothing OrElse sv.RootItems Is Nothing Then Continue For
                For Each item As SvgImageItem In sv.RootItems
                    If Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("CROWN") Then
                        item.Appearance.Normal.BorderColor = Color.Black
                        item.Appearance.Normal.BorderThickness = 1
                        item.Appearance.Normal.FillColor = Color.Transparent
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub ShowJawForSvgSelection()
        Dim useKid = If(CurrentPatient IsNot Nothing, CurrentPatient.IsKid, Module1.isKid)
        Dim jawType As Type = Nothing
        Select Case _SV
            Case "UPPER"
                jawType = If(useKid, GetType(KidUpperJaw), GetType(AdultUpperJaw))
            Case "FULL"
                jawType = If(useKid, GetType(KidJaw), GetType(AdultJaw))
            Case "LOWER"
                jawType = If(useKid, GetType(KidLowerJaw), GetType(AdultLowerJaw))
        End Select
        If jawType IsNot Nothing Then
            _currentJawType = jawType
            ShowJawUserControl(jawType)
        End If
    End Sub
#End Region
#Region "Jaw Control Management"

    Private Function GetOrCreateJawControl(jawType As Type) As UserControl
        If _jawControlCache.ContainsKey(jawType) Then
            Return _jawControlCache(jawType)
        Else
            Dim newControl = DirectCast(Activator.CreateInstance(jawType), UserControl)
            Dim patientAwareControl = TryCast(newControl, IPatientAwareUserControl)
            If patientAwareControl IsNot Nothing Then
                PatientAwareHelper.SubscribeToPatientChanges(newControl, patientAwareControl)
            End If
            _jawControlCache(jawType) = newControl
            Return newControl
        End If
    End Function
    ''' <summary>Dentures and LVL=9 data use the main chart pipeline; the mobile-only tree toggle is retired (checkbox hidden).</summary>
    Private Function IsCurrentPatientMobileEligible() As Boolean
        Return False
    End Function

    Private Sub ApplyMobileCheckboxVisibilityAndState()
        If chkMobile Is Nothing Then Return
        chkMobile.Visible = False
        If chkMobile.Checked Then
            _suppressChkMobileEvent = True
            Try
                chkMobile.Checked = False
                RevertMobileWorkspaceAndJawMode()
            Finally
                _suppressChkMobileEvent = False
            End Try
        End If
    End Sub

    Private Sub RevertMobileWorkspaceAndJawMode()
        Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
        If parentWs IsNot Nothing Then
            If String.Equals(parentWs.FormType, "Mobile", StringComparison.OrdinalIgnoreCase) Then
                parentWs.FormType = "Treat"
                parentWs.UpdateFilterTarget("Treat", CurrentPatient)
                If CurrentPatient IsNot Nothing Then
                    parentWs.UpdateFormTitle(CurrentPatient)
                End If
            End If
        End If
        If _currentJawControl IsNot Nothing Then
            Dim pidRm = If(CurrentPatient IsNot Nothing, CurrentPatient.PatientID, -1)
            Dim jaw = TryCast(_currentJawControl, IJawControl)
            If jaw IsNot Nothing Then
                jaw.IsMobile = False
                jaw.LoadPatientData(pidRm)
            Else
                Try
                    Dim isMobileProp = _currentJawControl.GetType().GetProperty("IsMobile")
                    If isMobileProp IsNot Nothing AndAlso isMobileProp.CanWrite Then
                        isMobileProp.SetValue(_currentJawControl, False)
                    End If
                    Dim aware = TryCast(_currentJawControl, IPatientAwareUserControl)
                    If aware IsNot Nothing Then aware.LoadPatientData(pidRm)
                Catch
                End Try
            End If
        End If
    End Sub

    Private Sub RefreshCurrentJawData()
        If _currentJawControl Is Nothing Then Return
        Dim pid = If(CurrentPatient IsNot Nothing, CurrentPatient.PatientID, -1)
        Const mobileMode As Boolean = False
        Dim jaw = TryCast(_currentJawControl, IJawControl)
        If jaw IsNot Nothing Then
            jaw.IsMobile = mobileMode
            jaw.LoadPatientData(pid)
        Else
            Dim aware = TryCast(_currentJawControl, IPatientAwareUserControl)
            If aware IsNot Nothing Then
                Try
                    Dim isMobileProp = _currentJawControl.GetType().GetProperty("IsMobile")
                    If isMobileProp IsNot Nothing AndAlso isMobileProp.CanWrite Then
                        isMobileProp.SetValue(_currentJawControl, mobileMode)
                    End If
                Catch
                End Try
                aware.LoadPatientData(pid)
            Else
                InvokeJawLoadByReflection(_currentJawControl, pid)
            End If
        End If
    End Sub

    ''' <summary>Prefer LoadPatientData(patientId) so jaw controls keep one pipeline; fall back to LoadPatientTreats for legacy types.</summary>
    Private Shared Sub InvokeJawLoadByReflection(jawControl As UserControl, patientId As Integer)
        Try
            Dim t = jawControl.GetType()
            Dim loadData = t.GetMethod("LoadPatientData", {GetType(Integer)})
            If loadData IsNot Nothing Then
                loadData.Invoke(jawControl, {patientId})
                Return
            End If
            Dim loadTreats = t.GetMethod("LoadPatientTreats", {GetType(Integer)})
            If loadTreats IsNot Nothing Then
                loadTreats.Invoke(jawControl, {patientId})
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"InvokeJawLoadByReflection: {ex.Message}")
        End Try
    End Sub
#End Region
#Region "Checkbox Handlers"
    Private Sub chkDevider_CheckedChanged(sender As Object, e As EventArgs)
        If chkDevider.Checked = False Then
            chkDevider.Text = If(Eng, "Hide Divider", "إخفاء المحاور")
        Else
            chkDevider.Text = If(Eng, "Show Divider", "إظهار المحاور")
        End If
        ' You'll need to implement divider logic in your jaw controls
        If _currentJawControl IsNot Nothing Then
            Dim jaw = TryCast(_currentJawControl, IJawControl)
            If jaw IsNot Nothing Then
                jaw.HideShowDiv(chkDevider.Checked)
            Else
                Try
                    Dim method = _currentJawControl.GetType().GetMethod("HideShowDiv")
                    If method IsNot Nothing Then
                        method.Invoke(_currentJawControl, New Object() {chkDevider.Checked})
                    End If
                Catch ex As Exception
                    System.Diagnostics.Debug.WriteLine($"HideShowDiv not available: {ex.Message}")
                End Try
            End If
        End If
    End Sub
    Private Sub chkMobile_CheckedChanged(sender As Object, e As EventArgs)
        If _suppressChkMobileEvent Then Return
        If Not chkMobile.Visible Then Return
        Try
            If chkMobile.Checked AndAlso Not IsCurrentPatientMobileEligible() Then
                _suppressChkMobileEvent = True
                Try
                    chkMobile.Checked = False
                Finally
                    _suppressChkMobileEvent = False
                End Try
                Return
            End If
            If _currentJawControl IsNot Nothing AndAlso CurrentPatient IsNot Nothing Then
                ' Use FindForm() to get the top-level form
                Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
                If parentWs IsNot Nothing Then
                    If chkMobile.Checked = True Then
                        parentWs.FormType = "Mobile"
                        parentWs.UpdateFilterTarget("Mobile")
                        parentWs.UpdateFormTitle(CurrentPatient)
                    Else
                        parentWs.FormType = "Treat"
                        parentWs.UpdateFilterTarget("Treat")
                        parentWs.UpdateFormTitle(CurrentPatient)
                    End If
                End If
                Dim jaw = TryCast(_currentJawControl, IJawControl)
                If jaw IsNot Nothing Then
                    jaw.IsMobile = chkMobile.Checked
                    jaw.LoadPatientData(CurrentPatient.PatientID)
                Else
                    Dim aware = TryCast(_currentJawControl, IPatientAwareUserControl)
                    If aware IsNot Nothing Then
                        Dim controlType = _currentJawControl.GetType()
                        Dim isMobileProperty = controlType.GetProperty("IsMobile")
                        If isMobileProperty IsNot Nothing AndAlso isMobileProperty.CanWrite Then
                            isMobileProperty.SetValue(_currentJawControl, chkMobile.Checked)
                        End If
                        aware.LoadPatientData(CurrentPatient.PatientID)
                    Else
                        Dim controlType = _currentJawControl.GetType()
                        Dim isMobileProperty = controlType.GetProperty("IsMobile")
                        If isMobileProperty IsNot Nothing AndAlso isMobileProperty.CanWrite Then
                            isMobileProperty.SetValue(_currentJawControl, chkMobile.Checked)
                        End If
                        InvokeJawLoadByReflection(_currentJawControl, CurrentPatient.PatientID)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Error updating mobile treatment state: " & ex.Message)
        End Try
    End Sub
#End Region
    ''' <summary>Header has no patient: show jaws with id -1 (teeth only, no DB treats).</summary>
    Private Sub ApplyNoPatientTreatsView()
        CurrentPatient = Nothing
        HealthLbl.Text = If(Eng, "Select a patient to view health information.", "اختر مريضاً لعرض الحالة الصحية.")
        ApplyHealthLabelAppearance(String.Empty)
        ApplyMobileCheckboxVisibilityAndState()
        If _isInitialLoad OrElse _currentJawType Is Nothing Then
            _isInitialLoad = False
            selectFULL()
        Else
            RefreshCurrentJawData()
        End If
        RefreshCurrentJawData()
    End Sub

    Public Sub LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        If patientId <= 0 Then
            ApplyNoPatientTreatsView()
            Return
        End If
        Dim p = ResolvePatientForTreats(patientId)
        If p Is Nothing Then Return
        CurrentPatient = p
        Dim healthRaw = If(p.Health, String.Empty)
        HealthLbl.Text = healthRaw
        ApplyHealthLabelAppearance(healthRaw)
        CurrentPatient.IsKid = Module1.isKid
        ApplyMobileCheckboxVisibilityAndState()
        If _isInitialLoad OrElse _currentJawType Is Nothing Then
            _isInitialLoad = False
            selectFULL()
        Else
            If _currentJawType IsNot Nothing AndAlso ShouldConvertJawType(CurrentPatient) Then
                ConvertCurrentJawType(CurrentPatient.IsKid)
            Else
                RefreshCurrentJawData()
            End If
        End If
        RefreshCurrentJawData()
    End Sub
    Public Sub LoadPatientData(patient As Patient) 'Implements IPatientAwareUserControl.LoadPatientData
        If patient IsNot Nothing Then
            CurrentPatient = patient
            Dim healthRaw = If(patient.Health, String.Empty)
            HealthLbl.Text = healthRaw
            ApplyHealthLabelAppearance(healthRaw)
            ApplyMobileCheckboxVisibilityAndState()
            If _isInitialLoad OrElse _currentJawType Is Nothing Then
                _isInitialLoad = False
                selectFULL() ' Default to full jaw
            Else
                ' Patient changed - convert jaw type if kid status changed
                If _currentJawType IsNot Nothing AndAlso ShouldConvertJawType(patient) Then
                    ConvertCurrentJawType(patient.IsKid)
                Else
                    RefreshCurrentJawData()
                End If
            End If
        Else
            CurrentPatient = Nothing
            frmPanel.Controls.Clear()
            _currentJawControl = Nothing
            _currentJawType = Nothing
            ApplyMobileCheckboxVisibilityAndState()
            'If _jawsOnlyPanel IsNot Nothing Then _jawsOnlyPanel.Visible = False
        End If
    End Sub
    Private Function ShouldConvertJawType(newPatient As Patient) As Boolean
        If _currentJawType Is Nothing Then Return False
        Dim currentIsKidJaw = _currentJawType.Name.StartsWith("Kid")
        Dim newPatientIsKid = newPatient.IsKid
        Return currentIsKidJaw <> newPatientIsKid
    End Function
    Private Sub ConvertCurrentJawType(toKid As Boolean)
        If _currentJawType Is Nothing Then Return
        Dim currentJawName = _currentJawType.Name
        Dim newJawType As Type = Nothing
        ' Map current jaw type to appropriate kid/adult version
        Select Case currentJawName
            Case "AdultJaw", "KidJaw"
                newJawType = If(toKid, GetType(KidJaw), GetType(AdultJaw))
            Case "AdultUpperJaw", "KidUpperJaw"
                newJawType = If(toKid, GetType(KidUpperJaw), GetType(AdultUpperJaw))
            Case "AdultLowerJaw", "KidLowerJaw"
                newJawType = If(toKid, GetType(KidLowerJaw), GetType(AdultLowerJaw))
        End Select
        If newJawType IsNot Nothing AndAlso newJawType <> _currentJawType Then
            _currentJawType = newJawType
            ShowJawUserControl(newJawType)
        Else
            RefreshCurrentJawData()
        End If
    End Sub
    ' SVG resource methods - safe load so release/production does not throw (e.g. missing or locked resources)
    Private Function GetFullJawSvg() As DevExpress.Utils.Svg.SvgImage
        Try
            Return My.Resources.Full
        Catch
            Return Nothing
        End Try
    End Function
    Private Function GetUpperJawSvg() As DevExpress.Utils.Svg.SvgImage
        Try
            Return My.Resources.Upper
        Catch
            Return Nothing
        End Try
    End Function
    Private Function GetLowerJawSvg() As DevExpress.Utils.Svg.SvgImage
        Try
            Return My.Resources.Lower
        Catch
            Return Nothing
        End Try
    End Function
#Region "Tooth Double-Click Handling"
    ' Add event subscription to jaw controls
    Private Sub SubscribeToJawEvents(jawControl As UserControl)
        Dim clickable = TryCast(jawControl, IToothClickable)
        If clickable IsNot Nothing Then
            AddHandler clickable.ToothDoubleClick, AddressOf HandleToothDoubleClick
            Return
        End If
        Dim toothDoubleClickEvent = jawControl.GetType().GetEvent("ToothDoubleClick")
        If toothDoubleClickEvent IsNot Nothing Then
            Dim handler = GetType(TreatsUserControl).GetMethod("HandleToothDoubleClick", BindingFlags.NonPublic Or BindingFlags.Instance)
            If handler IsNot Nothing Then
                toothDoubleClickEvent.AddEventHandler(jawControl, [Delegate].CreateDelegate(toothDoubleClickEvent.EventHandlerType, Me, handler))
            End If
        End If
    End Sub
    Private Sub UnsubscribeFromJawEvents()
        If _currentJawControl IsNot Nothing Then
            Dim clickable = TryCast(_currentJawControl, IToothClickable)
            If clickable IsNot Nothing Then
                RemoveHandler clickable.ToothDoubleClick, AddressOf HandleToothDoubleClick
            Else
                Dim toothDoubleClickEvent = _currentJawControl.GetType().GetEvent("ToothDoubleClick")
                If toothDoubleClickEvent IsNot Nothing Then
                    Dim handler = GetType(TreatsUserControl).GetMethod("HandleToothDoubleClick", BindingFlags.NonPublic Or BindingFlags.Instance)
                    If handler IsNot Nothing Then
                        toothDoubleClickEvent.RemoveEventHandler(_currentJawControl, [Delegate].CreateDelegate(toothDoubleClickEvent.EventHandlerType, Me, handler))
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub HandleToothDoubleClick(sender As Object, e As ToothDoubleClickEvent)
        ' Show SlctdToothCTL when a tooth is double-clicked
        ShowToothDetail(e)
    End Sub
    Private Sub ShowToothDetail(e As ToothDoubleClickEvent)
        ' Create and show the tooth detail control
        Dim toothDetailControl As New SlctdToothCTL()
        ' Load tooth data (you'll need to implement this method in SlctdToothCTL)
        toothDetailControl.ShowSlctdToothNew(e.PatientID, e.SVG, e.Source)
        ' Replace the current jaw control with tooth detail (hide jaws-only panel; not a jaw view)
        'If _jawsOnlyPanel IsNot Nothing Then _jawsOnlyPanel.Visible = False
        frmPanel.SuspendLayout()
        frmPanel.Controls.Clear()
        toothDetailControl.Dock = DockStyle.Fill
        frmPanel.Controls.Add(toothDetailControl)
        frmPanel.ResumeLayout()
        ' Optional: Add a back button or navigation to return to jaw view
        AddBackToJawNavigation()
    End Sub
    Private Sub AddBackToJawNavigation()
        ' Example: Add a "Back to Jaw" button to grpOptions
        Dim btnBack As New SimpleButton With {
        .Text = If(Eng, "Back to Jaw", "رجوع الى الفك"),
        .Dock = DockStyle.Top,
        .Height = 30
    }
        AddHandler btnBack.Click, AddressOf ReturnToJawView
        grpOptions.Controls.Add(btnBack)
        grpOptions.Controls.SetChildIndex(btnBack, 0) ' Add at top
    End Sub
    Private Sub ReturnToJawView(sender As Object, e As EventArgs)
        ' Remove the back button first
        Dim btnBack = TryCast(sender, SimpleButton)
        If btnBack IsNot Nothing Then
            grpOptions.Controls.Remove(btnBack)
            btnBack.Dispose()
        End If
        ' Force return to jaw view regardless of current control
        If _currentJawType IsNot Nothing Then
            ShowJawUserControl(_currentJawType, True) ' Force refresh even if same type
        End If
    End Sub
    Private Sub ShowJawUserControl(jawType As Type, Optional forceRefresh As Boolean = False)
        ' 🛡️ Prevent flicker when patient changes but same jaw and control already active
        If Not forceRefresh AndAlso _currentJawControl IsNot Nothing AndAlso _currentJawControl.GetType() = jawType Then
            RefreshCurrentJawData()
            Return
        End If

        ' Unsubscribe from previous jaw control events
        UnsubscribeFromJawEvents()

        ' Get or create the jaw control from cache
        Dim jawControl As UserControl = GetOrCreateJawControl(jawType)

        ' 🧩 Skip visual refresh if the control is already attached and same instance
        If frmPanel.Controls.Count > 0 AndAlso frmPanel.Controls(0) Is jawControl Then
            _currentJawControl = jawControl
            RefreshCurrentJawData()
            Return
        End If

        ' ⚡ Suspend layout updates to prevent flicker
        frmPanel.SuspendLayout()

        frmPanel.Controls.Clear()
        _currentJawControl = jawControl
        _currentJawControl.Dock = DockStyle.Fill
        frmPanel.Controls.Add(_currentJawControl)

        frmPanel.ResumeLayout()

        ' Subscribe to the new jaw control's events
        SubscribeToJawEvents(jawControl)
        'If _jawsOnlyPanel IsNot Nothing Then _jawsOnlyPanel.Visible = True
        RefreshCurrentJawData()
    End Sub

#End Region
#Region "Enhanced Jaw Control Management"
    Private Sub ShowJawUserControl(jawType As Type)
        ' Check if we're already showing this control
        If _currentJawControl IsNot Nothing AndAlso _currentJawControl.GetType() = jawType Then
            RefreshCurrentJawData()
            Return
        End If
        ' Unsubscribe from previous jaw control events
        UnsubscribeFromJawEvents()
        ' Get or create the jaw control from cache
        Dim jawControl As UserControl = GetOrCreateJawControl(jawType)
        ' Only update if we have a different control
        If _currentJawControl IsNot jawControl Then
            frmPanel.SuspendLayout()
            frmPanel.Controls.Clear()
            _currentJawControl = jawControl
            _currentJawControl.Dock = DockStyle.Fill
            frmPanel.Controls.Add(_currentJawControl)
            frmPanel.ResumeLayout()
        End If
        ' Subscribe to the new jaw control's events
        SubscribeToJawEvents(jawControl)
        'If _jawsOnlyPanel IsNot Nothing Then _jawsOnlyPanel.Visible = True
        RefreshCurrentJawData()
    End Sub
#End Region
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            PersistNamingSetting()
            If components IsNot Nothing Then
                components.Dispose()
            End If
            UnsubscribeFromJawEvents() ' Clean up event subscriptions
            For Each control In _jawControlCache.Values
                control.Dispose()
            Next
            _jawControlCache.Clear()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SuspendLayout()
        Me.Name = "TreatsUserControl"
        Me.Size = New System.Drawing.Size(1214, 617)
        Me.ResumeLayout(False)
    End Sub
End Class
