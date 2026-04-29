<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TreatmentsPage2
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TreatmentsPage2))
        Me.pnlFilters = New DevExpress.XtraEditors.SidePanel()
        Me.TablePanel1 = New DevExpress.Utils.Layout.TablePanel()
        Me.cboAddress = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboTrt = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.pnlBody = New DevExpress.XtraEditors.SidePanel()
        Me.pnlFilters.SuspendLayout()
        CType(Me.TablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TablePanel1.SuspendLayout()
        CType(Me.cboAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTrt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlFilters
        '
        resources.ApplyResources(Me.pnlFilters, "pnlFilters")
        Me.pnlFilters.Controls.Add(Me.TablePanel1)
        Me.pnlFilters.Name = "pnlFilters"
        '
        'TablePanel1
        '
        resources.ApplyResources(Me.TablePanel1, "TablePanel1")
        Me.TablePanel1.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 22.13!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 57.98!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 44.54!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55.46!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!)})
        Me.TablePanel1.Controls.Add(Me.cboAddress)
        Me.TablePanel1.Controls.Add(Me.cboTrt)
        Me.TablePanel1.Controls.Add(Me.LabelControl2)
        Me.TablePanel1.Controls.Add(Me.LabelControl1)
        Me.TablePanel1.Name = "TablePanel1"
        Me.TablePanel1.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TablePanel1.UseSkinIndents = True
        '
        'cboAddress
        '
        resources.ApplyResources(Me.cboAddress, "cboAddress")
        Me.TablePanel1.SetColumn(Me.cboAddress, 4)
        Me.cboAddress.Name = "cboAddress"
        Me.cboAddress.Properties.Appearance.Font = CType(resources.GetObject("cboAddress.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboAddress.Properties.Appearance.Options.UseFont = True
        Me.cboAddress.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboAddress.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TablePanel1.SetRow(Me.cboAddress, 0)
        '
        'cboTrt
        '
        resources.ApplyResources(Me.cboTrt, "cboTrt")
        Me.TablePanel1.SetColumn(Me.cboTrt, 2)
        Me.cboTrt.Name = "cboTrt"
        Me.cboTrt.Properties.Appearance.Font = CType(resources.GetObject("cboTrt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboTrt.Properties.Appearance.Options.UseFont = True
        Me.cboTrt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboTrt.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TablePanel1.SetRow(Me.cboTrt, 0)
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TablePanel1.SetColumn(Me.LabelControl2, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.TablePanel1.SetRow(Me.LabelControl2, 0)
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TablePanel1.SetColumn(Me.LabelControl1, 1)
        Me.LabelControl1.Name = "LabelControl1"
        Me.TablePanel1.SetRow(Me.LabelControl1, 0)
        '
        'pnlBody
        '
        resources.ApplyResources(Me.pnlBody, "pnlBody")
        Me.pnlBody.Name = "pnlBody"
        '
        'TreatmentsPage2
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlBody)
        Me.Controls.Add(Me.pnlFilters)
        Me.Name = "TreatmentsPage2"
        Me.pnlFilters.ResumeLayout(False)
        CType(Me.TablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TablePanel1.ResumeLayout(False)
        Me.TablePanel1.PerformLayout()
        CType(Me.cboAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTrt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlFilters As DevExpress.XtraEditors.SidePanel
    Friend WithEvents TablePanel1 As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents cboAddress As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboTrt As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlBody As DevExpress.XtraEditors.SidePanel
End Class
