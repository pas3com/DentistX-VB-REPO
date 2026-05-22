Imports System.Reflection
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.LookAndFeel

Public Class DiagUserControl
    Inherits XtraUserControl
    Implements IPatientAwareUserControl

    Friend WithEvents mainPanel As New Panel With {
        .Dock = DockStyle.Fill,
        .BackColor = BasePatientWorkspace.DiagModuleShellBack
    }
    Private frmPanel As Panel
    Private rightPanel As Panel
    Private splitContainer As SplitContainer
    ' Your existing right panel controls
    Friend WithEvents chkMobile As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkDevider As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents HealthLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpHealth As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpSvgs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FullSv As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents btnAddDiag As DevExpress.XtraEditors.SimpleButton
    ' Add to your existing control declarations
    Friend WithEvents btnShowDiag As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUpdateBack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents UpperSv As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents LowerSv As DevExpress.XtraEditors.SvgImageBox
    ' Control caching and state tracking
    Private _currentJawType As Type
    Private _currentJawControl As UserControl
    Private _jawControlCache As New Dictionary(Of Type, UserControl)
    Private _isInitialLoad As Boolean = True
    Private _SV As String = "FULL" ' Track current SVG selection
    Private _currentPatient As Patient
    Friend Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        Helpers.ResetControlBackground(Me)
        Me.BackColor = BasePatientWorkspace.DiagModuleShellBack
        UpdateStyles()
        Me.Controls.Add(mainPanel)
        Me.Controls.SetChildIndex(mainPanel, 0)
        CreateTreatsLayout()
    End Sub
    Public Sub New(ByVal filterTarget As String)
        MyBase.New()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        Helpers.ResetControlBackground(Me)
        Me.BackColor = BasePatientWorkspace.DiagModuleShellBack
        UpdateStyles()
        Me.Controls.Add(mainPanel)
        Me.Controls.SetChildIndex(mainPanel, 0)
        CreateTreatsLayout()
    End Sub
    Private Shared Sub EnableControlDoubleBuffered(ctl As Control)
        If ctl Is Nothing Then Return
        Dim pi = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        If pi IsNot Nothing AndAlso pi.CanWrite Then pi.SetValue(ctl, True, Nothing)
    End Sub

    Private Sub CreateTreatsLayout()
        Dim shell = BasePatientWorkspace.DiagModuleShellBack
        mainPanel.BackColor = shell
        mainPanel.Controls.Clear()
        Dim leftWidth As Integer = Math.Max(200, mainPanel.Width - 120)
        splitContainer = New SplitContainer With {
            .Dock = DockStyle.Fill,
            .Orientation = Orientation.Vertical,
            .SplitterDistance = leftWidth,
            .BackColor = shell
        }
        EnableControlDoubleBuffered(splitContainer)
        ' Set fixed width for right panel
        splitContainer.Panel2MinSize = 120
        splitContainer.FixedPanel = FixedPanel.Panel2
        splitContainer.Panel1.BackColor = shell
        splitContainer.Panel2.BackColor = shell
        frmPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = shell
        }
        rightPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = shell
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

        ' Adjust row percentages to accommodate taller GroupControl1
        tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 23.33F))   ' Health - slightly reduced
        tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 46))   ' SVGs - unchanged
        tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 30))   ' Options - unchanged

        tableLayout.BorderStyle = BorderStyle.Fixed3D
        tableLayout.BackColor = BasePatientWorkspace.DiagModuleShellBack

        ' Initialize controls
        InitializeRightPanelControls()

        ' Add controls to table layout rows
        tableLayout.Controls.Add(GrpHealth, 0, 0)    ' Top row - Health
        tableLayout.Controls.Add(grpSvgs, 0, 1)      ' Middle row - SVGs  
        tableLayout.Controls.Add(GroupControl1, 0, 2) ' Bottom row - Options

        ' Set dock styles
        GrpHealth.Dock = DockStyle.Fill
        grpSvgs.Dock = DockStyle.Fill
        GroupControl1.Dock = DockStyle.Fill

        ' Add table layout to right panel
        rightPanel.Controls.Add(tableLayout)
        TintRightPanelDiagShell()

        SetupFonts()

        ' Set up event handlers and initial state
        SetupEventHandlers()
        UnSelect()
        selectFULL()
        '' Create table layout with 3 equal rows
        'Dim tableLayout As New TableLayoutPanel()
        'tableLayout.Dock = DockStyle.Fill
        'tableLayout.RowCount = 3
        'tableLayout.ColumnCount = 1
        'tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 23.33F))
        'tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 53.33F))
        'tableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 23.33F))
        'tableLayout.BorderStyle = BorderStyle.Fixed3D
        '' Initialize controls
        'InitializeRightPanelControls()
        '' Add controls to table layout rows
        'tableLayout.Controls.Add(GrpHealth, 0, 0)    ' Top row - Health
        'tableLayout.Controls.Add(grpSvgs, 0, 1)      ' Middle row - SVGs  
        'tableLayout.Controls.Add(GroupControl1, 0, 2) ' Bottom row - Options
        '' Set dock styles
        'GrpHealth.Dock = DockStyle.Fill
        'grpSvgs.Dock = DockStyle.Fill
        'GroupControl1.Dock = DockStyle.Fill
        '' Add table layout to right panel
        'rightPanel.Controls.Add(tableLayout)
        'SetupFonts()
        '' Set up event handlers and initial state
        'SetupEventHandlers()
        'UnSelect()
        'selectFULL()
    End Sub
    Private Sub TintRightPanelDiagShell()
        If rightPanel Is Nothing Then Return
        ApplyDiagTintRecursive(rightPanel, BasePatientWorkspace.DiagModuleShellBack)
    End Sub

    ''' <summary>Fills nested groups, labels, checkboxes, SVGs under the diag right stack with Diag chrome (skins often ignore Appearance without Flat LF).</summary>
    Private Shared Sub ApplyDiagTintRecursive(root As Control, c As Color)
        If root Is Nothing Then Return
        Dim grp = TryCast(root, GroupControl)
        If grp IsNot Nothing Then ApplyDiagShellToGroup(grp, c)
        Dim tlp = TryCast(root, TableLayoutPanel)
        If tlp IsNot Nothing Then tlp.BackColor = c
        Dim svg = TryCast(root, SvgImageBox)
        If svg IsNot Nothing Then svg.BackColor = c

        Dim lbl = TryCast(root, LabelControl)
        If lbl IsNot Nothing Then
            lbl.LookAndFeel.UseDefaultLookAndFeel = False
            lbl.LookAndFeel.Style = LookAndFeelStyle.Flat
            lbl.Appearance.BackColor = c
            lbl.Appearance.Options.UseBackColor = True
        End If

        Dim chk = TryCast(root, CheckEdit)
        If chk IsNot Nothing Then
            chk.LookAndFeel.UseDefaultLookAndFeel = False
            chk.LookAndFeel.Style = LookAndFeelStyle.Flat
            chk.Properties.Appearance.BackColor = c
            chk.Properties.Appearance.Options.UseBackColor = True
        End If

        For Each child As Control In root.Controls
            ApplyDiagTintRecursive(child, c)
        Next
    End Sub

    Private Shared Sub ApplyDiagShellToGroup(gc As GroupControl, c As Color)
        If gc Is Nothing Then Return
        gc.LookAndFeel.UseDefaultLookAndFeel = False
        gc.LookAndFeel.Style = LookAndFeelStyle.Flat
        gc.Appearance.BackColor = c
        gc.Appearance.Options.UseBackColor = True
        gc.AppearanceCaption.BackColor = c
        gc.AppearanceCaption.Options.UseBackColor = True
    End Sub

    ''' <summary>Back-to-chart buttons are created dynamically; tint them without clearing active-state highlighting on primary actions.</summary>
    Private Shared Sub ApplyDiagShellToBackChartButton(btn As SimpleButton, c As Color)
        If btn Is Nothing Then Return
        btn.LookAndFeel.UseDefaultLookAndFeel = False
        btn.LookAndFeel.Style = LookAndFeelStyle.Flat
        btn.Appearance.BackColor = c
        btn.Appearance.Options.UseBackColor = True
    End Sub
    Private Sub SetupFonts()
        ' Set fonts for controls
        Dim boldFont As New Font("Calibri", 10, FontStyle.Bold)
        HealthLbl.Font = boldFont
        chkMobile.Font = boldFont
        chkDevider.Font = boldFont
        grpSvgs.AppearanceCaption.Font = boldFont
        GroupControl1.AppearanceCaption.Font = boldFont
        GrpHealth.AppearanceCaption.Font = boldFont
        btnAddDiag.Font = boldFont
        btnShowDiag.Font = boldFont
    End Sub
    Private Sub InitializeRightPanelControls1()


        ' GrpHealth
        GrpHealth = New DevExpress.XtraEditors.GroupControl()
        HealthLbl = New DevExpress.XtraEditors.LabelControl()
        GrpHealth.AppearanceCaption.Options.UseTextOptions = True
        GrpHealth.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GrpHealth.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        GrpHealth.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        GrpHealth.Text = If(Eng, "Patient Health", "الحالة الصحية")
        GrpHealth.Dock = DockStyle.Top
        GrpHealth.Height = 80
        GrpHealth.Width = 100
        GrpHealth.Appearance.BackColor = Color.Transparent
        'GrpHealth.StartColor = Color.AliceBlue
        'GrpHealth.EndColor = Color.BlueViolet
        'GrpHealth.gradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal
        HealthLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        HealthLbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        HealthLbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        HealthLbl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        HealthLbl.Dock = DockStyle.Fill
        HealthLbl.Text = If(Eng, "Health information will appear here", "المعلومات الصحية")
        GrpHealth.Controls.Add(HealthLbl)
        ' GroupControl1
        GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        chkMobile = New DevExpress.XtraEditors.CheckEdit()
        chkDevider = New DevExpress.XtraEditors.CheckEdit()
        btnAddDiag = New DevExpress.XtraEditors.SimpleButton()
        GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GroupControl1.Text = If(Eng, "Options", "خيارات")
        GroupControl1.Dock = DockStyle.Top
        GroupControl1.Height = 120
        GroupControl1.Width = 100

        chkMobile.Properties.Caption = If(Eng, "Mobile Treatment", "التركيبات المتحركة")
        chkMobile.Dock = DockStyle.Top
        chkMobile.Height = 25
        chkMobile.Visible = False
        chkMobile.Checked = False
        chkDevider.Properties.Caption = If(Eng, "Show Divider", "إظهار المحاور")
        chkDevider.Dock = DockStyle.Top
        chkDevider.Height = 25
        chkDevider.Checked = True
        btnAddDiag.Text = If(Eng, "Add Diagnose", "إضافة تشخيص")
        btnAddDiag.Dock = DockStyle.Top
        btnAddDiag.Height = 30

        GroupControl1.Controls.Add(btnAddDiag)
        GroupControl1.Controls.Add(chkDevider)
        GroupControl1.Controls.Add(chkMobile)
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

        ' GroupControl1 - Increase height to accommodate new button
        GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        chkMobile = New DevExpress.XtraEditors.CheckEdit()
        chkDevider = New DevExpress.XtraEditors.CheckEdit()
        btnAddDiag = New DevExpress.XtraEditors.SimpleButton()
        btnShowDiag = New DevExpress.XtraEditors.SimpleButton() ' New button
        'btnUpdateBack = New DevExpress.XtraEditors.SimpleButton() ' New button
        GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GroupControl1.Text = If(Eng, "Options", "خيارات")
        GroupControl1.Dock = DockStyle.Top
        GroupControl1.Height = 180 ' Increased from 120 to 150
        GroupControl1.Width = 100

        '' Configure buttons - Add Show Diag button first (will be at bottom due to DockStyle.Top)
        'btnUpdateBack.Text = If(Eng, "Change BackGround", "لون الخلفية")
        'btnUpdateBack.Dock = DockStyle.Top
        'btnUpdateBack.Height = 30


        btnShowDiag.Text = If(Eng, "Show Diagnoses", "عرض التشخيصات")
        btnShowDiag.Dock = DockStyle.Top
        btnShowDiag.Height = 30

        btnAddDiag.Text = If(Eng, "Add Diagnose", "إضافة تشخيص")
        btnAddDiag.Dock = DockStyle.Top
        btnAddDiag.Height = 30

        chkDevider.Properties.Caption = If(Eng, "Show Divider", "إظهار المحاور")
        chkDevider.Dock = DockStyle.Top
        chkDevider.Height = 25
        chkDevider.Checked = True

        chkMobile.Properties.Caption = If(Eng, "Mobile Treatment", "التركيبات المتحركة")
        chkMobile.Dock = DockStyle.Top
        chkMobile.Height = 25
        chkMobile.Visible = False
        chkMobile.Checked = False

        ' Add controls in reverse order (due to DockStyle.Top)
        'GroupControl1.Controls.Add(btnUpdateBack)
        GroupControl1.Controls.Add(btnShowDiag)
        GroupControl1.Controls.Add(btnAddDiag)
        GroupControl1.Controls.Add(chkDevider)
        GroupControl1.Controls.Add(chkMobile)

        ' grpSvgs - unchanged
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
        svgBox.SvgImage = GetSvgImageForJaw(tooltip)
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
        ' Add handlers for the new buttons
        AddHandler btnAddDiag.Click, AddressOf btnAddDiag_Click
        AddHandler btnShowDiag.Click, AddressOf btnShowDiag_Click
        'AddHandler btnUpdateBack.Click, AddressOf btnUpdateBack_Click
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
                jawType = If(useKid, GetType(KidUpperDiag), GetType(AdultUpperDiag))
            Case "FULL"
                jawType = If(useKid, GetType(KidDiag), GetType(AdultDiag))
            Case "LOWER"
                jawType = If(useKid, GetType(KidLowerDiag), GetType(AdultLowerDiag))
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
    Private Sub RefreshCurrentJawData()
        If _currentJawControl Is Nothing Then Return
        Dim pid = If(CurrentPatient IsNot Nothing, CurrentPatient.PatientID, -1)
        Const useMobileTree As Boolean = False
        Dim jaw = TryCast(_currentJawControl, IJawControl)
        If jaw IsNot Nothing Then
            jaw.IsMobile = useMobileTree
            jaw.LoadPatientData(pid)
        Else
            Try
                Dim isMobileProp = _currentJawControl.GetType().GetProperty("IsMobile")
                If isMobileProp IsNot Nothing Then
                    isMobileProp.SetValue(_currentJawControl, useMobileTree)
                End If
                Dim loadMethod = _currentJawControl.GetType().GetMethod("LoadPatientTreats")
                If loadMethod IsNot Nothing Then
                    loadMethod.Invoke(_currentJawControl, New Object() {pid})
                End If
            Catch ex As Exception
            End Try
        End If
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
        If Not chkMobile.Visible Then Return
        Try
            If _currentJawControl IsNot Nothing Then
                Dim pidM = If(CurrentPatient IsNot Nothing, CurrentPatient.PatientID, -1)
                Dim ws = PatientAwareHelper.TryGetPatientWorkspace(Me)
                If ws IsNot Nothing AndAlso CurrentPatient IsNot Nothing Then
                    If chkMobile.Checked = True Then
                        ws.FormType = "Mobile"
                        ws.UpdateFilterTarget("Mobile", CurrentPatient)
                        ws.UpdateFormTitle(CurrentPatient)
                    Else
                        ws.FormType = "Diag"
                        ws.UpdateFilterTarget("Diag", CurrentPatient)
                        ws.UpdateFormTitle(CurrentPatient)
                    End If
                End If
                Dim jaw = TryCast(_currentJawControl, IJawControl)
                If jaw IsNot Nothing Then
                    jaw.IsMobile = chkMobile.Checked
                    jaw.LoadPatientData(pidM)
                Else
                    Dim controlType = _currentJawControl.GetType()
                    Dim isMobileProperty = controlType.GetProperty("IsMobile")
                    If isMobileProperty IsNot Nothing AndAlso isMobileProperty.CanWrite Then
                        isMobileProperty.SetValue(_currentJawControl, chkMobile.Checked)
                    End If
                    Dim loadMethod = controlType.GetMethod("LoadPatientTreats")
                    If loadMethod IsNot Nothing Then
                        loadMethod.Invoke(_currentJawControl, New Object() {pidM})
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Error updating mobile treatment state: " & ex.Message)
        End Try
    End Sub
#End Region
    ''' <summary>No workspace patient: show diag jaws with id -1 (teeth only, no DB diagnoses).</summary>
    Private Sub ApplyNoPatientDiagView()
        CurrentPatient = Nothing
        HealthLbl.Text = If(Eng, "Select a patient to view health information.", "اختر مريضاً لعرض الحالة الصحية.")
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
            ApplyNoPatientDiagView()
            Return
        End If
        ' Sync current patient from form so we have a Patient object (same as TreatsUserControl)
        Dim ws = PatientAwareHelper.TryGetPatientWorkspace(Me)
        If ws IsNot Nothing AndAlso ws.Current_Patient IsNot Nothing AndAlso ws.Current_Patient.PatientID = patientId Then
            CurrentPatient = ws.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <> patientId Then Return
        HealthLbl.Text = $"{CurrentPatient.Health}"
        CurrentPatient.IsKid = Module1.isKid
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
            Case "AdultDiag", "KidDiag"
                newJawType = If(toKid, GetType(KidDiag), GetType(AdultDiag))
            Case "AdultUpperDiag", "KidUpperDiag"
                newJawType = If(toKid, GetType(KidUpperDiag), GetType(AdultUpperDiag))
            Case "AdultLowerDiag", "KidLowerDiag"
                newJawType = If(toKid, GetType(KidLowerDiag), GetType(AdultLowerDiag))
        End Select
        If newJawType IsNot Nothing AndAlso newJawType <> _currentJawType Then
            _currentJawType = newJawType
            ShowJawUserControl(newJawType)
        Else
            RefreshCurrentJawData()
        End If
    End Sub
    ' SVG resource methods - replace with your actual SVG resources
    Private Function GetFullJawSvg() As DevExpress.Utils.Svg.SvgImage
        Return My.Resources.Full
    End Function
    Private Function GetUpperJawSvg() As DevExpress.Utils.Svg.SvgImage
        Return My.Resources.Upper
    End Function
    Private Function GetLowerJawSvg() As DevExpress.Utils.Svg.SvgImage
        Return My.Resources.Lower
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
            Dim handler = GetType(DiagUserControl).GetMethod("HandleToothDoubleClick", BindingFlags.NonPublic Or BindingFlags.Instance)
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
                    Dim handler = GetType(DiagUserControl).GetMethod("HandleToothDoubleClick", BindingFlags.NonPublic Or BindingFlags.Instance)
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
        Dim toothDetailControl As New SlctdToothCTLDiag()
        ' Load tooth data (you'll need to implement this method in SlctdToothCTL)
        toothDetailControl.ShowSlctdToothNew(e.PatientID, e.SVG, e.Source)
        ' Replace the current jaw control with tooth detail
        frmPanel.SuspendLayout()
        frmPanel.Controls.Clear()
        toothDetailControl.Dock = DockStyle.Fill
        frmPanel.Controls.Add(toothDetailControl)
        frmPanel.ResumeLayout()
        ' Optional: Add a back button or navigation to return to jaw view
        AddBackToJawNavigation()
    End Sub
    Private Sub AddBackToJawNavigation()
        ' Example: Add a "Back to Chart" button to GroupControl1
        Dim btnBack As New SimpleButton With {
        .Text = If(Eng, "Back to Chart", "رجوع الى المخطط"),
        .Dock = DockStyle.Top,
        .Height = 30,
        .Font = New Font("Calibri", 10, FontStyle.Bold)
    }
        AddHandler btnBack.Click, AddressOf ReturnToJawView
        GroupControl1.Controls.Add(btnBack)
        GroupControl1.Controls.SetChildIndex(btnBack, 0) ' Add at top
        ApplyDiagShellToBackChartButton(btnBack, BasePatientWorkspace.DiagModuleShellBack)
    End Sub
    Private Sub ReturnToJawViewOld(sender As Object, e As EventArgs)
        ' Remove the back button first
        Dim btnBack = TryCast(sender, SimpleButton)
        If btnBack IsNot Nothing Then
            GroupControl1.Controls.Remove(btnBack)
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
        RefreshCurrentJawData()
    End Sub
#End Region

#Region "Diagnosis Button Handlers"
    ' Add this event declaration
    Public Event ReturnToJawRequested As EventHandler
    Private Sub btnAddDiag_Click(sender As Object, e As EventArgs)
        ShowAddDiagnosis()
    End Sub
    Private Sub btnShowDiag_Click(sender As Object, e As EventArgs) 'btnUpdateBack_Click
        ShowDiagnoses()
    End Sub
    'Private Sub btnUpdateBack_Click(sender As Object, e As EventArgs)

    '    Dim F As New UpdateBack
    '    F.ShowDialog()
    '    If DialogResult.OK = DialogResult.OK Then
    '        ApplyGradientWithGlass(Me, F.startColor, F.endColor,
    '                        F.gradientMode,
    '                        F.Glass)
    '    End If

    'End Sub

    Private Sub ShowAddDiagnosis()
        If CurrentPatient Is Nothing Then
            MsgBox(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."))
            Return
        End If

        Dim addDiag As New AddDiagDetails()
        addDiag.PassedPatientID = CurrentPatient.PatientID
        addDiag.PassedPatientName = CurrentPatient.PatientName

        AddHandler addDiag.ReturnToJaw, AddressOf HandleReturnFromDiagControl

        frmPanel.SuspendLayout()
        frmPanel.Controls.Clear()
        addDiag.Dock = DockStyle.Fill
        frmPanel.Controls.Add(addDiag)
        frmPanel.ResumeLayout()

        AddBackToJawNavigation("AddDiagDetails")
        UpdateButtonStates("AddDiagnosis")
    End Sub

    Private Sub ShowDiagnoses()
        If CurrentPatient Is Nothing Then
            MsgBox(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."))
            Return
        End If
        ''New code
        'Dim jawType As Type = Nothing
        'jawType = GetType(ShowDiagDetails)
        'If jawType IsNot Nothing Then
        '    _currentJawType = jawType
        '    ShowJawUserControl(jawType)
        'End If
        '' Update button states
        'UpdateButtonStates("ShowDiagnoses")

        '=================
        Dim showDiag As New ShowDiagDetails()
        showDiag.PassedPatientID = CurrentPatient.PatientID
        showDiag.PassedPatientName = CurrentPatient.PatientName
        showDiag.CurrentPatient = CurrentPatient

        AddHandler showDiag.ReturnToJaw, AddressOf HandleReturnFromDiagControl

        frmPanel.SuspendLayout()
        frmPanel.Controls.Clear()
        showDiag.Dock = DockStyle.Fill
        frmPanel.Controls.Add(showDiag)
        frmPanel.ResumeLayout()

        ' Load patient data (IPatientAwareUserControl); otherwise no data is shown
        showDiag.LoadPatientData(CurrentPatient.PatientID)
        PatientAwareHelper.SubscribeToPatientChanges(showDiag, showDiag)

        AddBackToJawNavigation("ShowDiagDetails")
        UpdateButtonStates("ShowDiagnoses")
    End Sub
    Private Sub ShowControlInMainPanel(control As UserControl, controlType As String)
        ' Store the current jaw control if we're switching from jaw view
        If _currentJawControl IsNot Nothing AndAlso frmPanel.Controls.Count > 0 AndAlso frmPanel.Controls(0) Is _currentJawControl Then
            ' We're currently showing a jaw control, store reference
            ' No need to store separately since _currentJawControl already holds it
        End If
        ' ⚡ Suspend layout updates to prevent flicker
        frmPanel.SuspendLayout()
        frmPanel.Controls.Clear()
        control.Dock = DockStyle.Fill
        frmPanel.Controls.Add(control)
        ' Subscribe to custom events if control supports them
        If TypeOf control Is AddDiagDetails Then
            AddHandler CType(control, AddDiagDetails).ReturnToJaw, AddressOf HandleReturnFromDiagControl
            ' Pass a callback method
            ShowAddDiag()
        ElseIf TypeOf control Is ShowDiagDetails Then
            AddHandler CType(control, ShowDiagDetails).ReturnToJaw, AddressOf HandleReturnFromDiagControl
        End If
        frmPanel.ResumeLayout()
        ' Add back navigation
        AddBackToJawNavigation(controlType)
    End Sub
    Private Sub ShowAddDiag()
        If CurrentPatient Is Nothing Then Return
        Dim addDiagControl As New AddDiagDetails()
        addDiagControl.PassedPatientID = CurrentPatient.PatientID
        addDiagControl.PassedPatientName = CurrentPatient.PatientName
        ' Pass a callback method
        addDiagControl.SetReturnCallback(AddressOf HandleDiagnosisControlClose)
    End Sub
    Private Sub HandleDiagnosisControlClose()
        ReturnToJawView(Nothing, EventArgs.Empty)
    End Sub
    Private Sub HandleReturnFromDiagControl(sender As Object, e As EventArgs)
        ReturnToJawView(Nothing, EventArgs.Empty)
    End Sub
    Private Sub UpdateButtonStates(activeControl As String)
        ' Highlight the active button
        Select Case activeControl
            Case "AddDiagnosis"
                btnAddDiag.BackColor = Color.LightBlue
                btnShowDiag.BackColor = SystemColors.Control
            Case "ShowDiagnoses"
                btnShowDiag.BackColor = Color.LightBlue
                btnAddDiag.BackColor = SystemColors.Control
            Case Else
                btnAddDiag.BackColor = SystemColors.Control
                btnShowDiag.BackColor = SystemColors.Control
        End Select
    End Sub
    Private Sub AddBackToJawNavigation(Optional fromControl As String = "")
        ' Clear any existing back button
        ClearExistingBackButton()
        ' Add a "Back to Chart" button to GroupControl1
        Dim btnBack As New SimpleButton With {
        .Text = If(Eng, "Back to Chart", "رجوع الى المخطط"),
        .Dock = DockStyle.Top,
        .Height = 30,
        .Font = New Font("Calibri", 10, FontStyle.Bold),
        .Tag = fromControl ' Store which control we're coming from
    }
        AddHandler btnBack.Click, AddressOf ReturnToJawView
        GroupControl1.Controls.Add(btnBack)
        GroupControl1.Controls.SetChildIndex(btnBack, 0) ' Add at top
        ApplyDiagShellToBackChartButton(btnBack, BasePatientWorkspace.DiagModuleShellBack)
    End Sub
    Private Sub ClearExistingBackButton()
        ' Find and remove any existing back button
        For i As Integer = GroupControl1.Controls.Count - 1 To 0 Step -1
            Dim ctrl As Control = GroupControl1.Controls(i)
            If TypeOf ctrl Is SimpleButton AndAlso (ctrl.Text = If(Eng, "Back to Chart", "رجوع الى المخطط") OrElse ctrl.Tag?.ToString().Contains("Back")) Then
                GroupControl1.Controls.RemoveAt(i)
                ctrl.Dispose()
            End If
        Next
    End Sub
    Private Sub ReturnToJawView(sender As Object, e As EventArgs)
        Dim btnBack = TryCast(sender, SimpleButton)
        If btnBack IsNot Nothing Then
            GroupControl1.Controls.Remove(btnBack)
            btnBack.Dispose()
        End If
        ' Reset button states
        UpdateButtonStates("")
        ClearExistingBackButton()
        ' Return to jaw view
        If _currentJawType IsNot Nothing Then
            ShowJawUserControl(_currentJawType, True) ' Force refresh
        End If
    End Sub

#End Region
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            UnsubscribeFromJawEvents() ' Clean up event subscriptions
            ' Remove event handlers for buttons
            RemoveHandler btnAddDiag.Click, AddressOf btnAddDiag_Click
            RemoveHandler btnShowDiag.Click, AddressOf btnShowDiag_Click
            For Each control In _jawControlCache.Values
                control.Dispose()
            Next
            _jawControlCache.Clear()
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DiagUserControl))
        Me.SuspendLayout()
        '
        'DiagUserControl
        '
        resources.ApplyResources(Me, "$this")
        Me.Name = "DiagUserControl"
        Me.ResumeLayout(False)
    End Sub
End Class
