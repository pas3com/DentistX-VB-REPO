<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDefColors
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDefColors))
        Me.TrtsTreeView = New System.Windows.Forms.TreeView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.searchPanel = New DevExpress.XtraEditors.SidePanel()
        Me.txtSrchTrt = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblFill = New DevExpress.XtraEditors.LabelControl()
        Me.lblBorder = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnSET = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveExit = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDefColor = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tbFillOpacity = New DevExpress.XtraEditors.TrackBarControl()
        Me.tbThick = New DevExpress.XtraEditors.TrackBarControl()
        Me.tbBrdrOpacity = New DevExpress.XtraEditors.TrackBarControl()
        Me.clrFillColor = New DevExpress.XtraEditors.ColorPickEdit()
        Me.clrBorderColor = New DevExpress.XtraEditors.ColorPickEdit()
        Me.lblTreat = New DevExpress.XtraEditors.LabelControl()
        Me.zSvg = New DevExpress.XtraEditors.SvgImageBox()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.searchPanel.SuspendLayout()
        CType(Me.txtSrchTrt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.tbFillOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFillOpacity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThick, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThick.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBrdrOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBrdrOpacity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.clrFillColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.clrBorderColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.zSvg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TrtsTreeView
        '
        resources.ApplyResources(Me.TrtsTreeView, "TrtsTreeView")
        Me.TrtsTreeView.Name = "TrtsTreeView"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TrtsTreeView)
        Me.PanelControl1.Controls.Add(Me.searchPanel)
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Name = "PanelControl1"
        '
        'searchPanel
        '
        Me.searchPanel.Controls.Add(Me.txtSrchTrt)
        resources.ApplyResources(Me.searchPanel, "searchPanel")
        Me.searchPanel.Name = "searchPanel"
        '
        'txtSrchTrt
        '
        resources.ApplyResources(Me.txtSrchTrt, "txtSrchTrt")
        Me.txtSrchTrt.Name = "txtSrchTrt"
        Me.txtSrchTrt.Properties.Appearance.Font = CType(resources.GetObject("txtSrchTrt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtSrchTrt.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = CType(resources.GetObject("LabelControl13.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl13.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl13, "LabelControl13")
        Me.LabelControl13.Name = "LabelControl13"
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.Font = CType(resources.GetObject("LabelControl22.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl22.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl22, "LabelControl22")
        Me.LabelControl22.Name = "LabelControl22"
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.Font = CType(resources.GetObject("LabelControl21.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl21.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl21, "LabelControl21")
        Me.LabelControl21.Name = "LabelControl21"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Name = "LabelControl8"
        '
        'lblFill
        '
        Me.lblFill.Appearance.Font = CType(resources.GetObject("lblFill.Appearance.Font"), System.Drawing.Font)
        Me.lblFill.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblFill.Appearance.Options.UseFont = True
        Me.lblFill.Appearance.Options.UseForeColor = True
        Me.lblFill.Appearance.Options.UseTextOptions = True
        Me.lblFill.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblFill, "lblFill")
        Me.lblFill.Name = "lblFill"
        '
        'lblBorder
        '
        Me.lblBorder.Appearance.Font = CType(resources.GetObject("lblBorder.Appearance.Font"), System.Drawing.Font)
        Me.lblBorder.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblBorder.Appearance.Options.UseFont = True
        Me.lblBorder.Appearance.Options.UseForeColor = True
        Me.lblBorder.Appearance.Options.UseTextOptions = True
        Me.lblBorder.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblBorder, "lblBorder")
        Me.lblBorder.Name = "lblBorder"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'btnSET
        '
        Me.btnSET.Appearance.Font = CType(resources.GetObject("btnSET.Appearance.Font"), System.Drawing.Font)
        Me.btnSET.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSET, "btnSET")
        Me.btnSET.Name = "btnSET"
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Appearance.Font = CType(resources.GetObject("btnSaveExit.Appearance.Font"), System.Drawing.Font)
        Me.btnSaveExit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSaveExit, "btnSaveExit")
        Me.btnSaveExit.Name = "btnSaveExit"
        '
        'lblDefColor
        '
        Me.lblDefColor.Appearance.Font = CType(resources.GetObject("lblDefColor.Appearance.Font"), System.Drawing.Font)
        Me.lblDefColor.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblDefColor.Appearance.Options.UseFont = True
        Me.lblDefColor.Appearance.Options.UseForeColor = True
        Me.lblDefColor.Appearance.Options.UseTextOptions = True
        Me.lblDefColor.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblDefColor, "lblDefColor")
        Me.lblDefColor.Name = "lblDefColor"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = CType(resources.GetObject("GroupControl1.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.tbFillOpacity)
        Me.GroupControl1.Controls.Add(Me.tbThick)
        Me.GroupControl1.Controls.Add(Me.tbBrdrOpacity)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl13)
        Me.GroupControl1.Controls.Add(Me.clrFillColor)
        Me.GroupControl1.Controls.Add(Me.LabelControl22)
        Me.GroupControl1.Controls.Add(Me.clrBorderColor)
        Me.GroupControl1.Controls.Add(Me.LabelControl21)
        Me.GroupControl1.Controls.Add(Me.LabelControl8)
        resources.ApplyResources(Me.GroupControl1, "GroupControl1")
        Me.GroupControl1.Name = "GroupControl1"
        '
        'tbFillOpacity
        '
        resources.ApplyResources(Me.tbFillOpacity, "tbFillOpacity")
        Me.tbFillOpacity.Name = "tbFillOpacity"
        Me.tbFillOpacity.Properties.AutoSize = False
        Me.tbFillOpacity.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbFillOpacity.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbFillOpacity.Properties.Maximum = 255
        Me.tbFillOpacity.Properties.ShowValueToolTip = True
        Me.tbFillOpacity.Properties.SmallChangeUseMode = DevExpress.XtraEditors.Repository.SmallChangeUseMode.ArrowKeysAndMouse
        Me.tbFillOpacity.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbFillOpacity.ShowToolTips = False
        Me.tbFillOpacity.Value = 128
        '
        'tbThick
        '
        resources.ApplyResources(Me.tbThick, "tbThick")
        Me.tbThick.Name = "tbThick"
        Me.tbThick.Properties.AutoSize = False
        Me.tbThick.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbThick.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbThick.Properties.LargeChange = 1
        Me.tbThick.Properties.Maximum = 5
        Me.tbThick.Properties.ShowValueToolTip = True
        Me.tbThick.Properties.SmallChangeUseMode = DevExpress.XtraEditors.Repository.SmallChangeUseMode.ArrowKeysAndMouse
        Me.tbThick.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbThick.ShowToolTips = False
        Me.tbThick.Value = 1
        '
        'tbBrdrOpacity
        '
        resources.ApplyResources(Me.tbBrdrOpacity, "tbBrdrOpacity")
        Me.tbBrdrOpacity.Name = "tbBrdrOpacity"
        Me.tbBrdrOpacity.Properties.AutoSize = False
        Me.tbBrdrOpacity.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbBrdrOpacity.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbBrdrOpacity.Properties.Maximum = 255
        Me.tbBrdrOpacity.Properties.ShowValueToolTip = True
        Me.tbBrdrOpacity.Properties.SmallChangeUseMode = DevExpress.XtraEditors.Repository.SmallChangeUseMode.ArrowKeysAndMouse
        Me.tbBrdrOpacity.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbBrdrOpacity.ShowToolTips = False
        Me.tbBrdrOpacity.Value = 128
        '
        'clrFillColor
        '
        resources.ApplyResources(Me.clrFillColor, "clrFillColor")
        Me.clrFillColor.EnterMoveNextControl = True
        Me.clrFillColor.Name = "clrFillColor"
        Me.clrFillColor.Properties.Appearance.Font = CType(resources.GetObject("clrFillColor.Properties.Appearance.Font"), System.Drawing.Font)
        Me.clrFillColor.Properties.Appearance.Options.UseFont = True
        Me.clrFillColor.Properties.AutomaticColor = System.Drawing.Color.Black
        Me.clrFillColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("clrFillColor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'clrBorderColor
        '
        resources.ApplyResources(Me.clrBorderColor, "clrBorderColor")
        Me.clrBorderColor.EnterMoveNextControl = True
        Me.clrBorderColor.Name = "clrBorderColor"
        Me.clrBorderColor.Properties.Appearance.Font = CType(resources.GetObject("clrBorderColor.Properties.Appearance.Font"), System.Drawing.Font)
        Me.clrBorderColor.Properties.Appearance.Options.UseFont = True
        Me.clrBorderColor.Properties.AutomaticColor = System.Drawing.Color.Black
        Me.clrBorderColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("clrBorderColor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblTreat
        '
        Me.lblTreat.Appearance.Font = CType(resources.GetObject("lblTreat.Appearance.Font"), System.Drawing.Font)
        Me.lblTreat.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblTreat.Appearance.Options.UseFont = True
        Me.lblTreat.Appearance.Options.UseForeColor = True
        Me.lblTreat.Appearance.Options.UseTextOptions = True
        Me.lblTreat.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblTreat, "lblTreat")
        Me.lblTreat.Name = "lblTreat"
        '
        'zSvg
        '
        resources.ApplyResources(Me.zSvg, "zSvg")
        Me.zSvg.Name = "zSvg"
        Me.zSvg.SvgImage = Global.DentistX.My.Resources.Resources.AllLayers
        '
        'FrmDefColors
        '
        Me.AcceptButton = Me.btnSaveExit
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblTreat)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.btnSET)
        Me.Controls.Add(Me.zSvg)
        Me.Controls.Add(Me.lblDefColor)
        Me.Controls.Add(Me.lblBorder)
        Me.Controls.Add(Me.lblFill)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "FrmDefColors"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.searchPanel.ResumeLayout(False)
        CType(Me.txtSrchTrt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.tbFillOpacity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFillOpacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThick.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThick, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBrdrOpacity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBrdrOpacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.clrFillColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.clrBorderColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.zSvg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TrtsTreeView As TreeView
    Friend WithEvents txtSrchTrt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents searchPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents tbBrdrOpacity As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents clrFillColor As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents clrBorderColor As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFill As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBorder As DevExpress.XtraEditors.LabelControl
    Friend WithEvents zSvg As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbFillOpacity As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents tbThick As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents btnSET As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSaveExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDefColor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTreat As DevExpress.XtraEditors.LabelControl
End Class
