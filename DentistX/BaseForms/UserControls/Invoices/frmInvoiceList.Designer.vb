<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInvoiceList
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceList))
        Me.dgvInvoices = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnViewDetails = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvInvoices
        '
        resources.ApplyResources(Me.dgvInvoices, "dgvInvoices")
        Me.dgvInvoices.EmbeddedNavigator.AccessibleDescription = resources.GetString("dgvInvoices.EmbeddedNavigator.AccessibleDescription")
        Me.dgvInvoices.EmbeddedNavigator.AccessibleName = resources.GetString("dgvInvoices.EmbeddedNavigator.AccessibleName")
        Me.dgvInvoices.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.dgvInvoices.EmbeddedNavigator.Anchor = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.dgvInvoices.EmbeddedNavigator.AutoSize = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.AutoSize"), Boolean)
        Me.dgvInvoices.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.dgvInvoices.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.dgvInvoices.EmbeddedNavigator.ImeMode = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.dgvInvoices.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.dgvInvoices.EmbeddedNavigator.TextLocation = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.dgvInvoices.EmbeddedNavigator.ToolTip = resources.GetString("dgvInvoices.EmbeddedNavigator.ToolTip")
        Me.dgvInvoices.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("dgvInvoices.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.dgvInvoices.EmbeddedNavigator.ToolTipTitle = resources.GetString("dgvInvoices.EmbeddedNavigator.ToolTipTitle")
        Me.dgvInvoices.MainView = Me.GridView1
        Me.dgvInvoices.Name = "dgvInvoices"
        Me.dgvInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.dgvInvoices
        Me.GridView1.Name = "GridView1"
        '
        'btnViewDetails
        '
        resources.ApplyResources(Me.btnViewDetails, "btnViewDetails")
        Me.btnViewDetails.Appearance.Font = CType(resources.GetObject("btnViewDetails.Appearance.Font"), System.Drawing.Font)
        Me.btnViewDetails.Appearance.Options.UseFont = True
        Me.btnViewDetails.ImageOptions.ImageKey = resources.GetString("btnViewDetails.ImageOptions.ImageKey")
        Me.btnViewDetails.Name = "btnViewDetails"
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.ImageOptions.ImageKey = resources.GetString("btnClose.ImageOptions.ImageKey")
        Me.btnClose.Name = "btnClose"
        '
        'frmInvoiceList
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnViewDetails)
        Me.Controls.Add(Me.dgvInvoices)
        Me.Name = "frmInvoiceList"
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnViewDetails As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
End Class
