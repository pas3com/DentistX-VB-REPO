<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DayControlOld
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DayNumLabel = New DevExpress.XtraEditors.LabelControl()
        Me.VisitPanel = New DevExpress.XtraEditors.PanelControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnAddNewVisit = New DevExpress.XtraBars.BarButtonItem()
        Me.btnEditVisit = New DevExpress.XtraBars.BarButtonItem()
        Me.btnDeleteVisit = New DevExpress.XtraBars.BarButtonItem()
        Me.btnListVisits = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuVisitsMenu = New DevExpress.XtraBars.BarSubItem()
        Me.BarVisitsList = New DevExpress.XtraBars.BarListItem()
        Me.RadialMenu1 = New DevExpress.XtraBars.Ribbon.RadialMenu(Me.components)
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        CType(Me.VisitPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadialMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DayNumLabel
        '
        Me.DayNumLabel.Appearance.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.DayNumLabel.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.DayNumLabel.Appearance.Options.UseFont = True
        Me.DayNumLabel.Appearance.Options.UseForeColor = True
        Me.DayNumLabel.Appearance.Options.UseTextOptions = True
        Me.DayNumLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DayNumLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DayNumLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.DayNumLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.DayNumLabel.Location = New System.Drawing.Point(0, 0)
        Me.DayNumLabel.Name = "DayNumLabel"
        Me.DayNumLabel.Size = New System.Drawing.Size(100, 35)
        Me.DayNumLabel.TabIndex = 0
        Me.DayNumLabel.Text = "Label"
        '
        'VisitPanel
        '
        Me.VisitPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VisitPanel.Location = New System.Drawing.Point(0, 35)
        Me.VisitPanel.Name = "VisitPanel"
        Me.VisitPanel.Size = New System.Drawing.Size(100, 65)
        Me.VisitPanel.TabIndex = 1
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnAddNewVisit, Me.btnEditVisit, Me.btnDeleteVisit, Me.btnListVisits, Me.mnuVisitsMenu, Me.BarVisitsList})
        Me.BarManager1.MaxItemId = 5
        Me.BarManager1.ShowFullMenus = True
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(100, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 100)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(100, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 100)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(100, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 100)
        '
        'btnAddNewVisit
        '
        Me.btnAddNewVisit.Caption = "Add New Visit"
        Me.btnAddNewVisit.Id = 0
        Me.btnAddNewVisit.Name = "btnAddNewVisit"
        '
        'btnEditVisit
        '
        Me.btnEditVisit.Caption = "Edit Visit"
        Me.btnEditVisit.Id = 1
        Me.btnEditVisit.Name = "btnEditVisit"
        '
        'btnDeleteVisit
        '
        Me.btnDeleteVisit.Caption = "Delete Visit"
        Me.btnDeleteVisit.Id = 2
        Me.btnDeleteVisit.Name = "btnDeleteVisit"
        '
        'btnListVisits
        '
        Me.btnListVisits.Caption = "List Visits"
        Me.btnListVisits.Id = 3
        Me.btnListVisits.Name = "btnListVisits"
        '
        'mnuVisitsMenu
        '
        Me.mnuVisitsMenu.Caption = "Visits Menu"
        Me.mnuVisitsMenu.Id = 4
        Me.mnuVisitsMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarVisitsList)})
        Me.mnuVisitsMenu.Name = "mnuVisitsMenu"
        '
        'BarVisitsList
        '
        Me.BarVisitsList.Caption = "Visits List"
        Me.BarVisitsList.Id = 5
        Me.BarVisitsList.Name = "BarVisitsList"
        '
        'RadialMenu1
        '
        Me.RadialMenu1.AutoExpand = True
        Me.RadialMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnAddNewVisit), New DevExpress.XtraBars.LinkPersistInfo(Me.btnEditVisit), New DevExpress.XtraBars.LinkPersistInfo(Me.btnDeleteVisit), New DevExpress.XtraBars.LinkPersistInfo(Me.btnListVisits), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuVisitsMenu)})
        Me.RadialMenu1.Manager = Me.BarManager1
        Me.RadialMenu1.Name = "RadialMenu1"
        '
        'ToolTipController1
        '
        Me.ToolTipController1.ShowBeak = True
        '
        'DayControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.VisitPanel)
        Me.Controls.Add(Me.DayNumLabel)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "DayControl"
        Me.Size = New System.Drawing.Size(100, 100)
        CType(Me.VisitPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadialMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DayNumLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VisitPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnAddNewVisit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnEditVisit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDeleteVisit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnListVisits As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents mnuVisitsMenu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarVisitsList As DevExpress.XtraBars.BarListItem
    Friend WithEvents RadialMenu1 As DevExpress.XtraBars.Ribbon.RadialMenu
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
End Class
