Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LineItemEditForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LineItemEditForm))
        Me.layout = New System.Windows.Forms.TableLayoutPanel()
        Me.lblProduct = New DevExpress.XtraEditors.LabelControl()
        Me._cmbProduct = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblQty = New DevExpress.XtraEditors.LabelControl()
        Me._spinQty = New DevExpress.XtraEditors.TextEdit()
        Me.lblPrice = New DevExpress.XtraEditors.LabelControl()
        Me._spinPrice = New DevExpress.XtraEditors.TextEdit()
        Me.lblExpiry = New DevExpress.XtraEditors.LabelControl()
        Me._dateExp = New DevExpress.XtraEditors.DateEdit()
        Me.panelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.layout.SuspendLayout()
        CType(Me._cmbProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._spinQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._spinPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateExp.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'layout
        '
        resources.ApplyResources(Me.layout, "layout")
        Me.layout.Controls.Add(Me.lblProduct, 0, 0)
        Me.layout.Controls.Add(Me._cmbProduct, 1, 0)
        Me.layout.Controls.Add(Me.lblQty, 0, 1)
        Me.layout.Controls.Add(Me._spinQty, 1, 1)
        Me.layout.Controls.Add(Me.lblPrice, 0, 2)
        Me.layout.Controls.Add(Me._spinPrice, 1, 2)
        Me.layout.Controls.Add(Me.lblExpiry, 0, 3)
        Me.layout.Controls.Add(Me._dateExp, 1, 3)
        Me.layout.Name = "layout"
        '
        'lblProduct
        '
        Me.lblProduct.Appearance.Font = CType(resources.GetObject("lblProduct.Appearance.Font"), System.Drawing.Font)
        Me.lblProduct.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblProduct, "lblProduct")
        Me.lblProduct.Name = "lblProduct"
        '
        '_cmbProduct
        '
        resources.ApplyResources(Me._cmbProduct, "_cmbProduct")
        Me._cmbProduct.EnterMoveNextControl = True
        Me._cmbProduct.Name = "_cmbProduct"
        Me._cmbProduct.Properties.Appearance.Font = CType(resources.GetObject("_cmbProduct.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbProduct.Properties.Appearance.Options.UseFont = True
        Me._cmbProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_cmbProduct.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblQty
        '
        Me.lblQty.Appearance.Font = CType(resources.GetObject("lblQty.Appearance.Font"), System.Drawing.Font)
        Me.lblQty.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblQty, "lblQty")
        Me.lblQty.Name = "lblQty"
        '
        '_spinQty
        '
        resources.ApplyResources(Me._spinQty, "_spinQty")
        Me._spinQty.EnterMoveNextControl = True
        Me._spinQty.Name = "_spinQty"
        Me._spinQty.Properties.Appearance.Font = CType(resources.GetObject("_spinQty.Properties.Appearance.Font"), System.Drawing.Font)
        Me._spinQty.Properties.Appearance.Options.UseFont = True
        '
        'lblPrice
        '
        Me.lblPrice.Appearance.Font = CType(resources.GetObject("lblPrice.Appearance.Font"), System.Drawing.Font)
        Me.lblPrice.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPrice, "lblPrice")
        Me.lblPrice.Name = "lblPrice"
        '
        '_spinPrice
        '
        resources.ApplyResources(Me._spinPrice, "_spinPrice")
        Me._spinPrice.EnterMoveNextControl = True
        Me._spinPrice.Name = "_spinPrice"
        Me._spinPrice.Properties.Appearance.Font = CType(resources.GetObject("_spinPrice.Properties.Appearance.Font"), System.Drawing.Font)
        Me._spinPrice.Properties.Appearance.Options.UseFont = True
        '
        'lblExpiry
        '
        Me.lblExpiry.Appearance.Font = CType(resources.GetObject("lblExpiry.Appearance.Font"), System.Drawing.Font)
        Me.lblExpiry.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblExpiry, "lblExpiry")
        Me.lblExpiry.Name = "lblExpiry"
        '
        '_dateExp
        '
        resources.ApplyResources(Me._dateExp, "_dateExp")
        Me._dateExp.EnterMoveNextControl = True
        Me._dateExp.Name = "_dateExp"
        Me._dateExp.Properties.Appearance.Font = CType(resources.GetObject("_dateExp.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateExp.Properties.Appearance.Options.UseFont = True
        Me._dateExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateExp.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateExp.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateExp.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.btnOk)
        Me.panelButtons.Controls.Add(Me.btnCancel)
        resources.ApplyResources(Me.panelButtons, "panelButtons")
        Me.panelButtons.Name = "panelButtons"
        '
        'btnOk
        '
        Me.btnOk.Appearance.Font = CType(resources.GetObject("btnOk.Appearance.Font"), System.Drawing.Font)
        Me.btnOk.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnOk, "btnOk")
        Me.btnOk.Name = "btnOk"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'LineItemEditForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.layout)
        Me.Controls.Add(Me.panelButtons)
        Me.Name = "LineItemEditForm"
        Me.layout.ResumeLayout(False)
        Me.layout.PerformLayout()
        CType(Me._cmbProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._spinQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._spinPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateExp.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub


    Friend WithEvents _cmbProduct As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents _spinQty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents _spinPrice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents _dateExp As DevExpress.XtraEditors.DateEdit
    Friend Shadows WithEvents layout As TableLayoutPanel
    Friend WithEvents lblProduct As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPrice As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblExpiry As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton

End Class
