<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockHubForm
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

    Friend WithEvents btnDashboard As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSuppliers As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnProducts As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBuy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPayments As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTracking As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExpenses As DevExpress.XtraEditors.SimpleButton

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockHubForm))
        Me.panel = New DevExpress.XtraEditors.PanelControl()
        Me.btnDashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSuppliers = New DevExpress.XtraEditors.SimpleButton()
        Me.btnProducts = New DevExpress.XtraEditors.SimpleButton()
        Me.btnInvoices = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBuy = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPayments = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTracking = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSupplierAccountStatement = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExpenses = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel
        '
        resources.ApplyResources(Me.panel, "panel")
        Me.panel.Controls.Add(Me.btnDashboard)
        Me.panel.Controls.Add(Me.btnSuppliers)
        Me.panel.Controls.Add(Me.btnProducts)
        Me.panel.Controls.Add(Me.btnInvoices)
        Me.panel.Controls.Add(Me.btnBuy)
        Me.panel.Controls.Add(Me.btnPayments)
        Me.panel.Controls.Add(Me.btnTracking)
        Me.panel.Controls.Add(Me.btnSupplierAccountStatement)
        Me.panel.Controls.Add(Me.btnExpenses)
        Me.panel.Name = "panel"
        '
        'btnDashboard
        '
        resources.ApplyResources(Me.btnDashboard, "btnDashboard")
        Me.btnDashboard.Appearance.Font = CType(resources.GetObject("btnDashboard.Appearance.Font"), System.Drawing.Font)
        Me.btnDashboard.Appearance.Options.UseFont = True
        Me.btnDashboard.ImageOptions.ImageKey = resources.GetString("btnDashboard.ImageOptions.ImageKey")
        Me.btnDashboard.Name = "btnDashboard"
        '
        'btnSuppliers
        '
        resources.ApplyResources(Me.btnSuppliers, "btnSuppliers")
        Me.btnSuppliers.Appearance.Font = CType(resources.GetObject("btnSuppliers.Appearance.Font"), System.Drawing.Font)
        Me.btnSuppliers.Appearance.Options.UseFont = True
        Me.btnSuppliers.ImageOptions.ImageKey = resources.GetString("btnSuppliers.ImageOptions.ImageKey")
        Me.btnSuppliers.Name = "btnSuppliers"
        '
        'btnProducts
        '
        resources.ApplyResources(Me.btnProducts, "btnProducts")
        Me.btnProducts.Appearance.Font = CType(resources.GetObject("btnProducts.Appearance.Font"), System.Drawing.Font)
        Me.btnProducts.Appearance.Options.UseFont = True
        Me.btnProducts.ImageOptions.ImageKey = resources.GetString("btnProducts.ImageOptions.ImageKey")
        Me.btnProducts.Name = "btnProducts"
        '
        'btnInvoices
        '
        resources.ApplyResources(Me.btnInvoices, "btnInvoices")
        Me.btnInvoices.Appearance.Font = CType(resources.GetObject("btnInvoices.Appearance.Font"), System.Drawing.Font)
        Me.btnInvoices.Appearance.Options.UseFont = True
        Me.btnInvoices.ImageOptions.ImageKey = resources.GetString("btnInvoices.ImageOptions.ImageKey")
        Me.btnInvoices.Name = "btnInvoices"
        '
        'btnBuy
        '
        resources.ApplyResources(Me.btnBuy, "btnBuy")
        Me.btnBuy.Appearance.Font = CType(resources.GetObject("btnBuy.Appearance.Font"), System.Drawing.Font)
        Me.btnBuy.Appearance.Options.UseFont = True
        Me.btnBuy.ImageOptions.ImageKey = resources.GetString("btnBuy.ImageOptions.ImageKey")
        Me.btnBuy.Name = "btnBuy"
        '
        'btnPayments
        '
        resources.ApplyResources(Me.btnPayments, "btnPayments")
        Me.btnPayments.Appearance.Font = CType(resources.GetObject("btnPayments.Appearance.Font"), System.Drawing.Font)
        Me.btnPayments.Appearance.Options.UseFont = True
        Me.btnPayments.ImageOptions.ImageKey = resources.GetString("btnPayments.ImageOptions.ImageKey")
        Me.btnPayments.Name = "btnPayments"
        '
        'btnTracking
        '
        resources.ApplyResources(Me.btnTracking, "btnTracking")
        Me.btnTracking.Appearance.Font = CType(resources.GetObject("btnTracking.Appearance.Font"), System.Drawing.Font)
        Me.btnTracking.Appearance.Options.UseFont = True
        Me.btnTracking.ImageOptions.ImageKey = resources.GetString("btnTracking.ImageOptions.ImageKey")
        Me.btnTracking.Name = "btnTracking"
        '
        'btnSupplierAccountStatement
        '
        resources.ApplyResources(Me.btnSupplierAccountStatement, "btnSupplierAccountStatement")
        Me.btnSupplierAccountStatement.Appearance.Font = CType(resources.GetObject("btnSupplierAccountStatement.Appearance.Font"), System.Drawing.Font)
        Me.btnSupplierAccountStatement.Appearance.Options.UseFont = True
        Me.btnSupplierAccountStatement.ImageOptions.ImageKey = resources.GetString("btnSupplierAccountStatement.ImageOptions.ImageKey")
        Me.btnSupplierAccountStatement.Name = "btnSupplierAccountStatement"
        '
        'btnExpenses
        '
        resources.ApplyResources(Me.btnExpenses, "btnExpenses")
        Me.btnExpenses.Appearance.Font = CType(resources.GetObject("btnExpenses.Appearance.Font"), System.Drawing.Font)
        Me.btnExpenses.Appearance.Options.UseFont = True
        Me.btnExpenses.ImageOptions.ImageKey = resources.GetString("btnExpenses.ImageOptions.ImageKey")
        Me.btnExpenses.Name = "btnExpenses"
        '
        'StockHubForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.panel)
        Me.Name = "StockHubForm"
        CType(Me.panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnSupplierAccountStatement As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnInvoices As DevExpress.XtraEditors.SimpleButton
End Class

