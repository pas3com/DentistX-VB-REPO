<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChqPayAccnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmChqPayAccnt))
        Me.lblTreat = New DevExpress.XtraEditors.LabelControl()
        Me.lblPayDate = New DevExpress.XtraEditors.LabelControl()
        Me.PayDate = New DevExpress.XtraEditors.DateEdit()
        Me.lblPayValue = New DevExpress.XtraEditors.LabelControl()
        Me.PayValue = New DevExpress.XtraEditors.TextEdit()
        Me.lblNotes = New DevExpress.XtraEditors.LabelControl()
        Me.NotesText = New DevExpress.XtraEditors.MemoEdit()
        Me.chqInsurTab = New DevExpress.XtraTab.XtraTabControl()
        Me.ChqPage = New DevExpress.XtraTab.XtraTabPage()
        Me.chkIsReturned = New DevExpress.XtraEditors.CheckEdit()
        Me.lblChequeScanHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblChequeScanMode = New DevExpress.XtraEditors.LabelControl()
        Me.radioChequeScanMode = New DevExpress.XtraEditors.RadioGroup()
        Me.chkSkipFields = New DevExpress.XtraEditors.CheckEdit()
        Me.ChkDrive = New DevExpress.XtraEditors.CheckEdit()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.btnResetChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScanAndPay = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScan = New DevExpress.XtraEditors.SimpleButton()
        Me.lblChequeScanSide = New DevExpress.XtraEditors.LabelControl()
        Me.radioChequeImageSide = New DevExpress.XtraEditors.RadioGroup()
        Me.txtChqOwner = New DevExpress.XtraEditors.TextEdit()
        Me.chkIsForward = New DevExpress.XtraEditors.CheckEdit()
        Me.chkIsCashed = New DevExpress.XtraEditors.CheckEdit()
        Me.dtChqDueDate = New DevExpress.XtraEditors.DateEdit()
        Me.txtForwardTo = New DevExpress.XtraEditors.TextEdit()
        Me.txtChqBank = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.txtAccountNumber = New DevExpress.XtraEditors.TextEdit()
        Me.lblForward = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqValue = New DevExpress.XtraEditors.TextEdit()
        Me.InsurePage = New DevExpress.XtraTab.XtraTabPage()
        Me.txtInsurNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.txtInureComp = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddPay = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.txtReceivedBy = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chqInsurTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chqInsurTab.SuspendLayout()
        Me.ChqPage.SuspendLayout()
        CType(Me.chkIsReturned.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioChequeScanMode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipFields.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDrive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioChequeImageSide.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsForward.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtForwardTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InsurePage.SuspendLayout()
        CType(Me.txtInsurNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInureComp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceivedBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTreat
        '
        Me.lblTreat.Appearance.Font = CType(resources.GetObject("lblTreat.Appearance.Font"), System.Drawing.Font)
        Me.lblTreat.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTreat.Appearance.Options.UseFont = True
        Me.lblTreat.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTreat, "lblTreat")
        Me.lblTreat.Name = "lblTreat"
        '
        'lblPayDate
        '
        Me.lblPayDate.Appearance.Font = CType(resources.GetObject("lblPayDate.Appearance.Font"), System.Drawing.Font)
        Me.lblPayDate.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPayDate, "lblPayDate")
        Me.lblPayDate.Name = "lblPayDate"
        '
        'PayDate
        '
        resources.ApplyResources(Me.PayDate, "PayDate")
        Me.PayDate.EnterMoveNextControl = True
        Me.PayDate.Name = "PayDate"
        Me.PayDate.Properties.Appearance.Font = CType(resources.GetObject("PayDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayDate.Properties.Appearance.Options.UseFont = True
        Me.PayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.PayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblPayValue
        '
        Me.lblPayValue.Appearance.Font = CType(resources.GetObject("lblPayValue.Appearance.Font"), System.Drawing.Font)
        Me.lblPayValue.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblPayValue.Appearance.Options.UseFont = True
        Me.lblPayValue.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblPayValue, "lblPayValue")
        Me.lblPayValue.Name = "lblPayValue"
        '
        'PayValue
        '
        resources.ApplyResources(Me.PayValue, "PayValue")
        Me.PayValue.EnterMoveNextControl = True
        Me.PayValue.Name = "PayValue"
        Me.PayValue.Properties.Appearance.Font = CType(resources.GetObject("PayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayValue.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.PayValue.Properties.Appearance.Options.UseFont = True
        Me.PayValue.Properties.Appearance.Options.UseForeColor = True
        '
        'lblNotes
        '
        Me.lblNotes.Appearance.Font = CType(resources.GetObject("lblNotes.Appearance.Font"), System.Drawing.Font)
        Me.lblNotes.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblNotes, "lblNotes")
        Me.lblNotes.Name = "lblNotes"
        '
        'NotesText
        '
        resources.ApplyResources(Me.NotesText, "NotesText")
        Me.NotesText.Name = "NotesText"
        Me.NotesText.Properties.Appearance.Font = CType(resources.GetObject("NotesText.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesText.Properties.Appearance.Options.UseFont = True
        '
        'chqInsurTab
        '
        Me.chqInsurTab.AppearancePage.Header.Font = CType(resources.GetObject("chqInsurTab.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.chqInsurTab.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.chqInsurTab, "chqInsurTab")
        Me.chqInsurTab.Name = "chqInsurTab"
        Me.chqInsurTab.SelectedTabPage = Me.ChqPage
        Me.chqInsurTab.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.ChqPage, Me.InsurePage})
        '
        'ChqPage
        '
        Me.ChqPage.Controls.Add(Me.chkIsReturned)
        Me.ChqPage.Controls.Add(Me.lblChequeScanHint)
        Me.ChqPage.Controls.Add(Me.lblChequeScanMode)
        Me.ChqPage.Controls.Add(Me.radioChequeScanMode)
        Me.ChqPage.Controls.Add(Me.chkSkipFields)
        Me.ChqPage.Controls.Add(Me.ChkDrive)
        Me.ChqPage.Controls.Add(Me.btnBrowse)
        Me.ChqPage.Controls.Add(Me.btnResetChqs)
        Me.ChqPage.Controls.Add(Me.btnScanAndPay)
        Me.ChqPage.Controls.Add(Me.btnScan)
        Me.ChqPage.Controls.Add(Me.lblChequeScanSide)
        Me.ChqPage.Controls.Add(Me.radioChequeImageSide)
        Me.ChqPage.Controls.Add(Me.txtChqOwner)
        Me.ChqPage.Controls.Add(Me.chkIsForward)
        Me.ChqPage.Controls.Add(Me.chkIsCashed)
        Me.ChqPage.Controls.Add(Me.dtChqDueDate)
        Me.ChqPage.Controls.Add(Me.txtForwardTo)
        Me.ChqPage.Controls.Add(Me.txtChqBank)
        Me.ChqPage.Controls.Add(Me.LabelControl14)
        Me.ChqPage.Controls.Add(Me.txtChqNumber)
        Me.ChqPage.Controls.Add(Me.LabelControl15)
        Me.ChqPage.Controls.Add(Me.txtAccountNumber)
        Me.ChqPage.Controls.Add(Me.lblForward)
        Me.ChqPage.Controls.Add(Me.LabelControl18)
        Me.ChqPage.Controls.Add(Me.LabelControl17)
        Me.ChqPage.Controls.Add(Me.LabelControl16)
        Me.ChqPage.Controls.Add(Me.LabelControl19)
        Me.ChqPage.Controls.Add(Me.txtChqValue)
        Me.ChqPage.Name = "ChqPage"
        resources.ApplyResources(Me.ChqPage, "ChqPage")
        '
        'chkIsReturned
        '
        Me.chkIsReturned.EnterMoveNextControl = True
        resources.ApplyResources(Me.chkIsReturned, "chkIsReturned")
        Me.chkIsReturned.Name = "chkIsReturned"
        Me.chkIsReturned.Properties.Appearance.Font = CType(resources.GetObject("chkIsReturned.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIsReturned.Properties.Appearance.Options.UseFont = True
        Me.chkIsReturned.Properties.Caption = resources.GetString("chkIsReturned.Properties.Caption")
        '
        'lblChequeScanHint
        '
        Me.lblChequeScanHint.Appearance.Font = CType(resources.GetObject("lblChequeScanHint.Appearance.Font"), System.Drawing.Font)
        Me.lblChequeScanHint.Appearance.Options.UseFont = True
        Me.lblChequeScanHint.Appearance.Options.UseTextOptions = True
        Me.lblChequeScanHint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblChequeScanHint, "lblChequeScanHint")
        Me.lblChequeScanHint.Name = "lblChequeScanHint"
        '
        'lblChequeScanMode
        '
        Me.lblChequeScanMode.Appearance.Font = CType(resources.GetObject("lblChequeScanMode.Appearance.Font"), System.Drawing.Font)
        Me.lblChequeScanMode.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChequeScanMode, "lblChequeScanMode")
        Me.lblChequeScanMode.Name = "lblChequeScanMode"
        '
        'radioChequeScanMode
        '
        resources.ApplyResources(Me.radioChequeScanMode, "radioChequeScanMode")
        Me.radioChequeScanMode.Name = "radioChequeScanMode"
        Me.radioChequeScanMode.Properties.Appearance.Font = CType(resources.GetObject("radioChequeScanMode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.radioChequeScanMode.Properties.Appearance.Options.UseFont = True
        Me.radioChequeScanMode.Properties.Columns = 2
        '
        'chkSkipFields
        '
        Me.chkSkipFields.EnterMoveNextControl = True
        resources.ApplyResources(Me.chkSkipFields, "chkSkipFields")
        Me.chkSkipFields.Name = "chkSkipFields"
        Me.chkSkipFields.Properties.Appearance.Font = CType(resources.GetObject("chkSkipFields.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkSkipFields.Properties.Appearance.ForeColor = System.Drawing.Color.BlueViolet
        Me.chkSkipFields.Properties.Appearance.Options.UseFont = True
        Me.chkSkipFields.Properties.Appearance.Options.UseForeColor = True
        Me.chkSkipFields.Properties.Caption = resources.GetString("chkSkipFields.Properties.Caption")
        '
        'ChkDrive
        '
        Me.ChkDrive.EnterMoveNextControl = True
        resources.ApplyResources(Me.ChkDrive, "ChkDrive")
        Me.ChkDrive.Name = "ChkDrive"
        Me.ChkDrive.Properties.Appearance.Font = CType(resources.GetObject("ChkDrive.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ChkDrive.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.ChkDrive.Properties.Appearance.Options.UseFont = True
        Me.ChkDrive.Properties.Appearance.Options.UseForeColor = True
        Me.ChkDrive.Properties.Caption = resources.GetString("ChkDrive.Properties.Caption")
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        Me.btnBrowse.ImageOptions.ImageKey = resources.GetString("btnBrowse.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'btnResetChqs
        '
        Me.btnResetChqs.Appearance.Font = CType(resources.GetObject("btnResetChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnResetChqs.Appearance.Options.UseFont = True
        Me.btnResetChqs.ImageOptions.ImageKey = resources.GetString("btnResetChqs.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnResetChqs, "btnResetChqs")
        Me.btnResetChqs.Name = "btnResetChqs"
        '
        'btnScanAndPay
        '
        Me.btnScanAndPay.Appearance.Font = CType(resources.GetObject("btnScanAndPay.Appearance.Font"), System.Drawing.Font)
        Me.btnScanAndPay.Appearance.Options.UseFont = True
        Me.btnScanAndPay.ImageOptions.ImageKey = resources.GetString("btnScanAndPay.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScanAndPay, "btnScanAndPay")
        Me.btnScanAndPay.Name = "btnScanAndPay"
        '
        'btnScan
        '
        Me.btnScan.Appearance.Font = CType(resources.GetObject("btnScan.Appearance.Font"), System.Drawing.Font)
        Me.btnScan.Appearance.Options.UseFont = True
        Me.btnScan.ImageOptions.ImageKey = resources.GetString("btnScan.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScan, "btnScan")
        Me.btnScan.Name = "btnScan"
        '
        'lblChequeScanSide
        '
        Me.lblChequeScanSide.Appearance.Font = CType(resources.GetObject("lblChequeScanSide.Appearance.Font"), System.Drawing.Font)
        Me.lblChequeScanSide.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChequeScanSide, "lblChequeScanSide")
        Me.lblChequeScanSide.Name = "lblChequeScanSide"
        '
        'radioChequeImageSide
        '
        resources.ApplyResources(Me.radioChequeImageSide, "radioChequeImageSide")
        Me.radioChequeImageSide.Name = "radioChequeImageSide"
        Me.radioChequeImageSide.Properties.Appearance.Font = CType(resources.GetObject("radioChequeImageSide.Properties.Appearance.Font"), System.Drawing.Font)
        Me.radioChequeImageSide.Properties.Appearance.Options.UseFont = True
        Me.radioChequeImageSide.Properties.Columns = 2
        '
        'txtChqOwner
        '
        Me.txtChqOwner.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqOwner, "txtChqOwner")
        Me.txtChqOwner.Name = "txtChqOwner"
        Me.txtChqOwner.Properties.Appearance.Font = CType(resources.GetObject("txtChqOwner.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqOwner.Properties.Appearance.Options.UseFont = True
        '
        'chkIsForward
        '
        Me.chkIsForward.EnterMoveNextControl = True
        resources.ApplyResources(Me.chkIsForward, "chkIsForward")
        Me.chkIsForward.Name = "chkIsForward"
        Me.chkIsForward.Properties.Appearance.Font = CType(resources.GetObject("chkIsForward.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIsForward.Properties.Appearance.Options.UseFont = True
        Me.chkIsForward.Properties.Caption = resources.GetString("chkIsForward.Properties.Caption")
        '
        'chkIsCashed
        '
        Me.chkIsCashed.EnterMoveNextControl = True
        resources.ApplyResources(Me.chkIsCashed, "chkIsCashed")
        Me.chkIsCashed.Name = "chkIsCashed"
        Me.chkIsCashed.Properties.Appearance.Font = CType(resources.GetObject("chkIsCashed.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIsCashed.Properties.Appearance.Options.UseFont = True
        Me.chkIsCashed.Properties.Caption = resources.GetString("chkIsCashed.Properties.Caption")
        '
        'dtChqDueDate
        '
        resources.ApplyResources(Me.dtChqDueDate, "dtChqDueDate")
        Me.dtChqDueDate.EnterMoveNextControl = True
        Me.dtChqDueDate.Name = "dtChqDueDate"
        Me.dtChqDueDate.Properties.Appearance.Font = CType(resources.GetObject("dtChqDueDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtChqDueDate.Properties.Appearance.Options.UseFont = True
        Me.dtChqDueDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtChqDueDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtChqDueDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtChqDueDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtChqDueDate.Properties.CalendarTimeProperties.MaskSettings.Set("mask", "d")
        Me.dtChqDueDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic
        Me.dtChqDueDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.dtChqDueDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtChqDueDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.dtChqDueDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtChqDueDate.Properties.ShowOk = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtChqDueDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'txtForwardTo
        '
        Me.txtForwardTo.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtForwardTo, "txtForwardTo")
        Me.txtForwardTo.Name = "txtForwardTo"
        Me.txtForwardTo.Properties.Appearance.Font = CType(resources.GetObject("txtForwardTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtForwardTo.Properties.Appearance.Options.UseFont = True
        '
        'txtChqBank
        '
        Me.txtChqBank.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqBank, "txtChqBank")
        Me.txtChqBank.Name = "txtChqBank"
        Me.txtChqBank.Properties.Appearance.Font = CType(resources.GetObject("txtChqBank.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqBank.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Name = "LabelControl14"
        '
        'txtChqNumber
        '
        Me.txtChqNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqNumber, "txtChqNumber")
        Me.txtChqNumber.Name = "txtChqNumber"
        Me.txtChqNumber.Properties.Appearance.Font = CType(resources.GetObject("txtChqNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqNumber.Properties.Appearance.Options.UseFont = True
        Me.txtChqNumber.Properties.MaxLength = 10
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.Font = CType(resources.GetObject("LabelControl15.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl15.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl15, "LabelControl15")
        Me.LabelControl15.Name = "LabelControl15"
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtAccountNumber, "txtAccountNumber")
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Properties.Appearance.Font = CType(resources.GetObject("txtAccountNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtAccountNumber.Properties.Appearance.Options.UseFont = True
        Me.txtAccountNumber.Properties.MaxLength = 10
        '
        'lblForward
        '
        Me.lblForward.Appearance.Font = CType(resources.GetObject("lblForward.Appearance.Font"), System.Drawing.Font)
        Me.lblForward.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblForward, "lblForward")
        Me.lblForward.Name = "lblForward"
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.Font = CType(resources.GetObject("LabelControl18.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl18.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl18, "LabelControl18")
        Me.LabelControl18.Name = "LabelControl18"
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.Font = CType(resources.GetObject("LabelControl17.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl17.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl17, "LabelControl17")
        Me.LabelControl17.Name = "LabelControl17"
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.Font = CType(resources.GetObject("LabelControl16.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl16.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl16, "LabelControl16")
        Me.LabelControl16.Name = "LabelControl16"
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = CType(resources.GetObject("LabelControl19.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl19.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl19.Appearance.Options.UseFont = True
        Me.LabelControl19.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl19, "LabelControl19")
        Me.LabelControl19.Name = "LabelControl19"
        '
        'txtChqValue
        '
        resources.ApplyResources(Me.txtChqValue, "txtChqValue")
        Me.txtChqValue.EnterMoveNextControl = True
        Me.txtChqValue.Name = "txtChqValue"
        Me.txtChqValue.Properties.Appearance.Font = CType(resources.GetObject("txtChqValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqValue.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtChqValue.Properties.Appearance.Options.UseFont = True
        Me.txtChqValue.Properties.Appearance.Options.UseForeColor = True
        '
        'InsurePage
        '
        Me.InsurePage.Controls.Add(Me.txtInsurNotes)
        Me.InsurePage.Controls.Add(Me.txtInureComp)
        Me.InsurePage.Controls.Add(Me.LabelControl21)
        Me.InsurePage.Controls.Add(Me.LabelControl20)
        Me.InsurePage.Name = "InsurePage"
        resources.ApplyResources(Me.InsurePage, "InsurePage")
        '
        'txtInsurNotes
        '
        resources.ApplyResources(Me.txtInsurNotes, "txtInsurNotes")
        Me.txtInsurNotes.Name = "txtInsurNotes"
        Me.txtInsurNotes.Properties.Appearance.Font = CType(resources.GetObject("txtInsurNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtInsurNotes.Properties.Appearance.Options.UseFont = True
        '
        'txtInureComp
        '
        resources.ApplyResources(Me.txtInureComp, "txtInureComp")
        Me.txtInureComp.Name = "txtInureComp"
        Me.txtInureComp.Properties.Appearance.Font = CType(resources.GetObject("txtInureComp.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtInureComp.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.Font = CType(resources.GetObject("LabelControl21.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl21.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl21, "LabelControl21")
        Me.LabelControl21.Name = "LabelControl21"
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.Font = CType(resources.GetObject("LabelControl20.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl20.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl20, "LabelControl20")
        Me.LabelControl20.Name = "LabelControl20"
        '
        'btnAddPay
        '
        Me.btnAddPay.Appearance.Font = CType(resources.GetObject("btnAddPay.Appearance.Font"), System.Drawing.Font)
        Me.btnAddPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAddPay, "btnAddPay")
        Me.btnAddPay.Name = "btnAddPay"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Name = "btnSave"
        '
        'txtReceivedBy
        '
        resources.ApplyResources(Me.txtReceivedBy, "txtReceivedBy")
        Me.txtReceivedBy.Name = "txtReceivedBy"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'FrmChqPayAccnt
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtReceivedBy)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.lblTreat)
        Me.Controls.Add(Me.lblPayDate)
        Me.Controls.Add(Me.PayDate)
        Me.Controls.Add(Me.lblPayValue)
        Me.Controls.Add(Me.PayValue)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.NotesText)
        Me.Controls.Add(Me.btnAddPay)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.chqInsurTab)
        Me.DoubleBuffered = True
        Me.Name = "FrmChqPayAccnt"
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chqInsurTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chqInsurTab.ResumeLayout(False)
        Me.ChqPage.ResumeLayout(False)
        Me.ChqPage.PerformLayout()
        CType(Me.chkIsReturned.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioChequeScanMode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipFields.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDrive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioChequeImageSide.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsForward.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtForwardTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InsurePage.ResumeLayout(False)
        Me.InsurePage.PerformLayout()
        CType(Me.txtInsurNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInureComp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceivedBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chqInsurTab As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ChqPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnResetChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnScanAndPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnScan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblChequeScanSide As DevExpress.XtraEditors.LabelControl
    Friend WithEvents radioChequeImageSide As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblChequeScanHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblChequeScanMode As DevExpress.XtraEditors.LabelControl
    Friend WithEvents radioChequeScanMode As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents txtChqOwner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkIsCashed As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtChqDueDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtChqBank As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAccountNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents InsurePage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtInsurNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtInureComp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAddPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTreat As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPayDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblPayValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblNotes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents NotesText As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ChkDrive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkIsForward As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtForwardTo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblForward As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkSkipFields As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtReceivedBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkIsReturned As DevExpress.XtraEditors.CheckEdit
End Class
