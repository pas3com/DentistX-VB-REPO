<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Navigator3
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Navigator3))
        Me.PatientBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.btNewPatient = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLast = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFirst = New DevExpress.XtraEditors.SimpleButton()
        Me.PopupPatient = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.SurgeryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelBal = New DevExpress.XtraEditors.LabelControl()
        Me.txtCount = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.SplashScreenManager1 = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Global.DentistX.WaitForm1), True, True, GetType(System.Windows.Forms.UserControl))
        Me.posTxt = New DevExpress.XtraEditors.TextEdit()
        Me.btnAdultChart = New DevExpress.XtraEditors.CheckButton()
        Me.LabelAge = New DevExpress.XtraEditors.LabelControl()
        Me.IsKidLabel = New DevExpress.XtraEditors.LabelControl()
        Me.btnResetKid = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelAddress = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientNum = New DevExpress.XtraEditors.LabelControl()
        Me.Side1 = New DevExpress.XtraEditors.SidePanel()
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.lblTxtTrts = New DevExpress.XtraEditors.LabelControl()
        Me.lblTrts = New DevExpress.XtraEditors.LabelControl()
        Me.lblTxtPays = New DevExpress.XtraEditors.LabelControl()
        Me.lblPays = New DevExpress.XtraEditors.LabelControl()
        Me.lbltxtBal = New DevExpress.XtraEditors.LabelControl()
        Me.txtPatientName = New System.Windows.Forms.TextBox()
        Me.btnSearchResults = New DevExpress.XtraEditors.SimpleButton()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PatientBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SurgeryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.posTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Side1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PatientBS
        '
        Me.PatientBS.DataSource = GetType(DentistX.Patient)
        '
        'btNewPatient
        '
        resources.ApplyResources(Me.btNewPatient, "btNewPatient")
        Me.btNewPatient.Appearance.Font = CType(resources.GetObject("btNewPatient.Appearance.Font"), System.Drawing.Font)
        Me.btNewPatient.Appearance.Options.UseFont = True
        Me.btNewPatient.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btNewPatient.Name = "btNewPatient"
        '
        'btnLast
        '
        resources.ApplyResources(Me.btnLast, "btnLast")
        Me.btnLast.Appearance.Font = CType(resources.GetObject("btnLast.Appearance.Font"), System.Drawing.Font)
        Me.btnLast.Appearance.Options.UseFont = True
        Me.btnLast.Name = "btnLast"
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseFont = True
        Me.btnNext.Name = "btnNext"
        '
        'btnPrev
        '
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseFont = True
        Me.btnPrev.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Name = "btnPrev"
        '
        'btnFirst
        '
        Me.btnFirst.Appearance.Font = CType(resources.GetObject("btnFirst.Appearance.Font"), System.Drawing.Font)
        Me.btnFirst.Appearance.Options.UseFont = True
        Me.btnFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.btnFirst, "btnFirst")
        Me.btnFirst.Name = "btnFirst"
        '
        'PopupPatient
        '
        resources.ApplyResources(Me.PopupPatient, "PopupPatient")
        Me.PopupPatient.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "PatientName", True))
        Me.PopupPatient.Name = "PopupPatient"
        Me.PopupPatient.Properties.Appearance.BackColor = System.Drawing.Color.SeaShell
        Me.PopupPatient.Properties.Appearance.Font = CType(resources.GetObject("PopupPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PopupPatient.Properties.Appearance.Options.UseBackColor = True
        Me.PopupPatient.Properties.Appearance.Options.UseFont = True
        Me.PopupPatient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PopupPatient.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.PopupPatient.Properties.NullText = resources.GetString("PopupPatient.Properties.NullText")
        Me.PopupPatient.Properties.NullValuePrompt = resources.GetString("PopupPatient.Properties.NullValuePrompt")
        Me.PopupPatient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        '
        'LabelBal
        '
        Me.LabelBal.Appearance.Font = CType(resources.GetObject("LabelBal.Appearance.Font"), System.Drawing.Font)
        Me.LabelBal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelBal, "LabelBal")
        Me.LabelBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "Balance", True))
        Me.LabelBal.Name = "LabelBal"
        '
        'txtCount
        '
        resources.ApplyResources(Me.txtCount, "txtCount")
        Me.txtCount.Name = "txtCount"
        Me.txtCount.Properties.Appearance.BackColor = System.Drawing.Color.Honeydew
        Me.txtCount.Properties.Appearance.Font = CType(resources.GetObject("txtCount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtCount.Properties.Appearance.Options.UseBackColor = True
        Me.txtCount.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl8
        '
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Name = "LabelControl8"
        '
        'SplashScreenManager1
        '
        Me.SplashScreenManager1.ClosingDelay = 500
        '
        'posTxt
        '
        resources.ApplyResources(Me.posTxt, "posTxt")
        Me.posTxt.Name = "posTxt"
        Me.posTxt.Properties.Appearance.BackColor = System.Drawing.Color.MintCream
        Me.posTxt.Properties.Appearance.Font = CType(resources.GetObject("posTxt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.posTxt.Properties.Appearance.Options.UseBackColor = True
        Me.posTxt.Properties.Appearance.Options.UseFont = True
        '
        'btnAdultChart
        '
        Me.btnAdultChart.Appearance.Font = CType(resources.GetObject("btnAdultChart.Appearance.Font"), System.Drawing.Font)
        Me.btnAdultChart.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAdultChart, "btnAdultChart")
        Me.btnAdultChart.Name = "btnAdultChart"
        Me.btnAdultChart.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[True]
        '
        'LabelAge
        '
        Me.LabelAge.Appearance.Font = CType(resources.GetObject("LabelAge.Appearance.Font"), System.Drawing.Font)
        Me.LabelAge.Appearance.Options.UseFont = True
        Me.LabelAge.Appearance.Options.UseTextOptions = True
        Me.LabelAge.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelAge.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelAge, "LabelAge")
        Me.LabelAge.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "Age", True))
        Me.LabelAge.Name = "LabelAge"
        '
        'IsKidLabel
        '
        Me.IsKidLabel.Appearance.Font = CType(resources.GetObject("IsKidLabel.Appearance.Font"), System.Drawing.Font)
        Me.IsKidLabel.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.IsKidLabel.Appearance.Options.UseFont = True
        Me.IsKidLabel.Appearance.Options.UseForeColor = True
        Me.IsKidLabel.Appearance.Options.UseTextOptions = True
        Me.IsKidLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IsKidLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.IsKidLabel, "IsKidLabel")
        Me.IsKidLabel.Name = "IsKidLabel"
        '
        'btnResetKid
        '
        Me.btnResetKid.Appearance.Font = CType(resources.GetObject("btnResetKid.Appearance.Font"), System.Drawing.Font)
        Me.btnResetKid.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnResetKid, "btnResetKid")
        Me.btnResetKid.Name = "btnResetKid"
        '
        'LabelAddress
        '
        Me.LabelAddress.Appearance.Font = CType(resources.GetObject("LabelAddress.Appearance.Font"), System.Drawing.Font)
        Me.LabelAddress.Appearance.Options.UseFont = True
        Me.LabelAddress.Appearance.Options.UseTextOptions = True
        Me.LabelAddress.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelAddress, "LabelAddress")
        Me.LabelAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "Address", True))
        Me.LabelAddress.Name = "LabelAddress"
        '
        'lblPatientNum
        '
        Me.lblPatientNum.Appearance.Font = CType(resources.GetObject("lblPatientNum.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientNum.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientNum, "lblPatientNum")
        Me.lblPatientNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "PatientNumber", True))
        Me.lblPatientNum.Name = "lblPatientNum"
        '
        'Side1
        '
        Me.Side1.Controls.Add(Me.lblPatientName)
        Me.Side1.Controls.Add(Me.lblTxtTrts)
        Me.Side1.Controls.Add(Me.lblTrts)
        Me.Side1.Controls.Add(Me.lblTxtPays)
        Me.Side1.Controls.Add(Me.lblPays)
        Me.Side1.Controls.Add(Me.lbltxtBal)
        Me.Side1.Controls.Add(Me.LabelBal)
        Me.Side1.Controls.Add(Me.LabelAddress)
        Me.Side1.Controls.Add(Me.LabelAge)
        resources.ApplyResources(Me.Side1, "Side1")
        Me.Side1.Name = "Side1"
        '
        'lblPatientName
        '
        Me.lblPatientName.Appearance.Font = CType(resources.GetObject("lblPatientName.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientName.Appearance.ForeColor = System.Drawing.Color.Magenta
        Me.lblPatientName.Appearance.Options.UseFont = True
        Me.lblPatientName.Appearance.Options.UseForeColor = True
        Me.lblPatientName.Appearance.Options.UseTextOptions = True
        Me.lblPatientName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblPatientName, "lblPatientName")
        Me.lblPatientName.Name = "lblPatientName"
        '
        'lblTxtTrts
        '
        Me.lblTxtTrts.Appearance.Font = CType(resources.GetObject("lblTxtTrts.Appearance.Font"), System.Drawing.Font)
        Me.lblTxtTrts.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTxtTrts.Appearance.Options.UseFont = True
        Me.lblTxtTrts.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTxtTrts, "lblTxtTrts")
        Me.lblTxtTrts.Name = "lblTxtTrts"
        '
        'lblTrts
        '
        Me.lblTrts.Appearance.Font = CType(resources.GetObject("lblTrts.Appearance.Font"), System.Drawing.Font)
        Me.lblTrts.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTrts.Appearance.Options.UseFont = True
        Me.lblTrts.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTrts, "lblTrts")
        Me.lblTrts.Name = "lblTrts"
        '
        'lblTxtPays
        '
        Me.lblTxtPays.Appearance.Font = CType(resources.GetObject("lblTxtPays.Appearance.Font"), System.Drawing.Font)
        Me.lblTxtPays.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTxtPays.Appearance.Options.UseFont = True
        Me.lblTxtPays.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTxtPays, "lblTxtPays")
        Me.lblTxtPays.Name = "lblTxtPays"
        '
        'lblPays
        '
        Me.lblPays.Appearance.Font = CType(resources.GetObject("lblPays.Appearance.Font"), System.Drawing.Font)
        Me.lblPays.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPays.Appearance.Options.UseFont = True
        Me.lblPays.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblPays, "lblPays")
        Me.lblPays.Name = "lblPays"
        '
        'lbltxtBal
        '
        Me.lbltxtBal.Appearance.Font = CType(resources.GetObject("lbltxtBal.Appearance.Font"), System.Drawing.Font)
        Me.lbltxtBal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lbltxtBal, "lbltxtBal")
        Me.lbltxtBal.Name = "lbltxtBal"
        '
        'txtPatientName
        '
        Me.txtPatientName.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtPatientName, "txtPatientName")
        Me.txtPatientName.Name = "txtPatientName"
        '
        'btnSearchResults
        '
        Me.btnSearchResults.Appearance.Font = CType(resources.GetObject("btnSearchResults.Appearance.Font"), System.Drawing.Font)
        Me.btnSearchResults.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSearchResults, "btnSearchResults")
        Me.btnSearchResults.Name = "btnSearchResults"
        Me.btnSearchResults.TabStop = False
        '
        'btnWhatsSend
        '
        Me.btnWhatsSend.ImageOptions.ImageKey = resources.GetString("btnWhatsSend.ImageOptions.ImageKey")
        Me.btnWhatsSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(20, 20)
        resources.ApplyResources(Me.btnWhatsSend, "btnWhatsSend")
        Me.btnWhatsSend.Name = "btnWhatsSend"
        '
        'Navigator3
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnSearchResults)
        Me.Controls.Add(Me.txtPatientName)
        Me.Controls.Add(Me.lblPatientNum)
        Me.Controls.Add(Me.btnResetKid)
        Me.Controls.Add(Me.btnWhatsSend)
        Me.Controls.Add(Me.IsKidLabel)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnAdultChart)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.posTxt)
        Me.Controls.Add(Me.txtCount)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.PopupPatient)
        Me.Controls.Add(Me.btNewPatient)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.Side1)
        Me.Name = "Navigator3"
        CType(Me.PatientBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SurgeryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.posTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Side1.ResumeLayout(False)
        Me.Side1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btNewPatient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLast As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFirst As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PopupPatient As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LabelBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SurgeryBindingSource As Windows.Forms.BindingSource
    Friend WithEvents PatientBS As Windows.Forms.BindingSource
    Friend WithEvents SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager
    Friend WithEvents posTxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAdultChart As DevExpress.XtraEditors.CheckButton
    Friend WithEvents LabelAge As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IsKidLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnResetKid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelAddress As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientNum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Side1 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents txtPatientName As System.Windows.Forms.TextBox
    Friend WithEvents btnWhatsSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTxtTrts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTrts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTxtPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbltxtBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSearchResults As DevExpress.XtraEditors.SimpleButton
End Class
