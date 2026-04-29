<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HistoryViewerForm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistoryViewerForm))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIcon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnResetLayout = New DevExpress.XtraEditors.SimpleButton()
        Me.DateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.DateTo = New DevExpress.XtraEditors.DateEdit()
        Me.CboAction = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtSearchPatient = New DevExpress.XtraEditors.TextEdit()
        Me.ImgActions = New DevExpress.Utils.ImageCollection(Me.components)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.DateFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboAction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSearchPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.GridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleDescription")
        Me.GridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleName")
        Me.GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridControl1.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridControl1.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridControl1.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridControl1.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridControl1.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridControl1.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridControl1.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridControl1.EmbeddedNavigator.ToolTip = resources.GetString("GridControl1.EmbeddedNavigator.ToolTip")
        Me.GridControl1.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridControl1.EmbeddedNavigator.ToolTipTitle")
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIcon})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colIcon
        '
        resources.ApplyResources(Me.colIcon, "colIcon")
        Me.colIcon.ImageOptions.ImageKey = resources.GetString("colIcon.ImageOptions.ImageKey")
        Me.colIcon.Name = "colIcon"
        '
        'PanelControl1
        '
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.BtnResetLayout)
        Me.PanelControl1.Controls.Add(Me.DateFrom)
        Me.PanelControl1.Controls.Add(Me.DateTo)
        Me.PanelControl1.Controls.Add(Me.CboAction)
        Me.PanelControl1.Controls.Add(Me.BtnRefresh)
        Me.PanelControl1.Controls.Add(Me.TxtSearchPatient)
        Me.PanelControl1.Name = "PanelControl1"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'BtnResetLayout
        '
        resources.ApplyResources(Me.BtnResetLayout, "BtnResetLayout")
        Me.BtnResetLayout.Appearance.Font = CType(resources.GetObject("BtnResetLayout.Appearance.Font"), System.Drawing.Font)
        Me.BtnResetLayout.Appearance.Options.UseFont = True
        Me.BtnResetLayout.ImageOptions.ImageKey = resources.GetString("BtnResetLayout.ImageOptions.ImageKey")
        Me.BtnResetLayout.Name = "BtnResetLayout"
        '
        'DateFrom
        '
        resources.ApplyResources(Me.DateFrom, "DateFrom")
        Me.DateFrom.Name = "DateFrom"
        Me.DateFrom.Properties.Appearance.Font = CType(resources.GetObject("DateFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DateFrom.Properties.Appearance.Options.UseFont = True
        Me.DateFrom.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "d"
        Me.DateFrom.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '
        'DateTo
        '
        resources.ApplyResources(Me.DateTo, "DateTo")
        Me.DateTo.Name = "DateTo"
        Me.DateTo.Properties.Appearance.Font = CType(resources.GetObject("DateTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DateTo.Properties.Appearance.Options.UseFont = True
        '
        'CboAction
        '
        resources.ApplyResources(Me.CboAction, "CboAction")
        Me.CboAction.Name = "CboAction"
        Me.CboAction.Properties.Appearance.Font = CType(resources.GetObject("CboAction.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboAction.Properties.Appearance.Options.UseFont = True
        Me.CboAction.Properties.Items.AddRange(New Object() {resources.GetString("CboAction.Properties.Items"), resources.GetString("CboAction.Properties.Items1"), resources.GetString("CboAction.Properties.Items2"), resources.GetString("CboAction.Properties.Items3"), resources.GetString("CboAction.Properties.Items4")})
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Appearance.Font = CType(resources.GetObject("BtnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.BtnRefresh.Appearance.Options.UseFont = True
        Me.BtnRefresh.ImageOptions.ImageKey = resources.GetString("BtnRefresh.ImageOptions.ImageKey")
        Me.BtnRefresh.Name = "BtnRefresh"
        '
        'TxtSearchPatient
        '
        resources.ApplyResources(Me.TxtSearchPatient, "TxtSearchPatient")
        Me.TxtSearchPatient.Name = "TxtSearchPatient"
        Me.TxtSearchPatient.Properties.Appearance.Font = CType(resources.GetObject("TxtSearchPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSearchPatient.Properties.Appearance.Options.UseFont = True
        Me.TxtSearchPatient.Properties.NullValuePrompt = resources.GetString("TxtSearchPatient.Properties.NullValuePrompt")
        '
        'ImgActions
        '
        Me.ImgActions.ImageStream = CType(resources.GetObject("ImgActions.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImgActions.Images.SetKeyName(0, "Insert")
        Me.ImgActions.Images.SetKeyName(1, "Update")
        Me.ImgActions.Images.SetKeyName(2, "Delete")
        Me.ImgActions.Images.SetKeyName(3, "View")
        '
        'HistoryViewerForm
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "HistoryViewerForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.DateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboAction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSearchPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboAction As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtSearchPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ImgActions As DevExpress.Utils.ImageCollection
    Friend WithEvents colIcon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BtnResetLayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
End Class
