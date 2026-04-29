<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExchMny
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExchMny))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cboTransType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtAmount = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dtTransDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.btnAddTrans = New DevExpress.XtraEditors.SimpleButton()
        Me.ContactsCombo1 = New DentistX.ContactsCombo()
        Me.GridContactsInfo = New DevExpress.XtraGrid.GridControl()
        Me.ContactBalanceBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridViewContactInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colContactID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLoanBorrow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRepaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridLoansInfo = New DevExpress.XtraGrid.GridControl()
        Me.LoansBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridViewLoansInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colLoanID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colContactID2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCName2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOriginalAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotalRepaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRemainingBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDirection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLoanDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLoanID = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtRepayValue = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNotes = New DevExpress.XtraEditors.TextEdit()
        Me.dtRepayDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnAddPay = New DevExpress.XtraEditors.SimpleButton()
        Me.GridPayments = New DevExpress.XtraGrid.GridControl()
        Me.PaysBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridViewPayments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRepaymentID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLoanIDPay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAmountPay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRepaymentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotesPay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnEditLoan = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditPay = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelLoan = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelPay = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.cboTransType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTransDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTransDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridContactsInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContactBalanceBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewContactInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLoansInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoansBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewLoansInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRepayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRepayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRepayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PaysBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'cboTransType
        '
        resources.ApplyResources(Me.cboTransType, "cboTransType")
        Me.cboTransType.Name = "cboTransType"
        Me.cboTransType.Properties.Appearance.Font = CType(resources.GetObject("cboTransType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboTransType.Properties.Appearance.Options.UseFont = True
        Me.cboTransType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboTransType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'txtAmount
        '
        resources.ApplyResources(Me.txtAmount, "txtAmount")
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Properties.Appearance.Font = CType(resources.GetObject("txtAmount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtAmount.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'dtTransDate
        '
        resources.ApplyResources(Me.dtTransDate, "dtTransDate")
        Me.dtTransDate.Name = "dtTransDate"
        Me.dtTransDate.Properties.Appearance.Font = CType(resources.GetObject("dtTransDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTransDate.Properties.Appearance.Options.UseFont = True
        Me.dtTransDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTransDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTransDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTransDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'txtDescription
        '
        resources.ApplyResources(Me.txtDescription, "txtDescription")
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Properties.Appearance.Font = CType(resources.GetObject("txtDescription.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtDescription.Properties.Appearance.Options.UseFont = True
        '
        'btnAddTrans
        '
        Me.btnAddTrans.Appearance.Font = CType(resources.GetObject("btnAddTrans.Appearance.Font"), System.Drawing.Font)
        Me.btnAddTrans.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAddTrans, "btnAddTrans")
        Me.btnAddTrans.Name = "btnAddTrans"
        '
        'ContactsCombo1
        '
        Me.ContactsCombo1.Appearance.Font = CType(resources.GetObject("ContactsCombo1.Appearance.Font"), System.Drawing.Font)
        Me.ContactsCombo1.Appearance.Options.UseFont = True
        Me.ContactsCombo1.BtnAddVisible = True
        Me.ContactsCombo1.BtnSearchVisible = True
        Me.ContactsCombo1.ContactID = 1
        Me.ContactsCombo1.ContName = "ContactsCombo1"
        Me.ContactsCombo1.Filter = ""
        resources.ApplyResources(Me.ContactsCombo1, "ContactsCombo1")
        Me.ContactsCombo1.Name = "ContactsCombo1"
        '
        'GridContactsInfo
        '
        Me.GridContactsInfo.DataSource = Me.ContactBalanceBS
        resources.ApplyResources(Me.GridContactsInfo, "GridContactsInfo")
        Me.GridContactsInfo.MainView = Me.GridViewContactInfo
        Me.GridContactsInfo.Name = "GridContactsInfo"
        Me.GridContactsInfo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewContactInfo})
        '
        'GridViewContactInfo
        '
        Me.GridViewContactInfo.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewContactInfo.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewContactInfo.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewContactInfo.Appearance.Row.Font = CType(resources.GetObject("GridViewContactInfo.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewContactInfo.Appearance.Row.Options.UseFont = True
        Me.GridViewContactInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colContactID, Me.colName, Me.colLoanBorrow, Me.colRepaid, Me.colBal, Me.colStatus})
        Me.GridViewContactInfo.GridControl = Me.GridContactsInfo
        Me.GridViewContactInfo.Name = "GridViewContactInfo"
        '
        'colContactID
        '
        resources.ApplyResources(Me.colContactID, "colContactID")
        Me.colContactID.FieldName = "ContactID"
        Me.colContactID.Name = "colContactID"
        '
        'colName
        '
        resources.ApplyResources(Me.colName, "colName")
        Me.colName.FieldName = "CName"
        Me.colName.Name = "colName"
        '
        'colLoanBorrow
        '
        resources.ApplyResources(Me.colLoanBorrow, "colLoanBorrow")
        Me.colLoanBorrow.FieldName = "TotalLoanedOrBorrowed"
        Me.colLoanBorrow.Name = "colLoanBorrow"
        '
        'colRepaid
        '
        resources.ApplyResources(Me.colRepaid, "colRepaid")
        Me.colRepaid.FieldName = "TotalRepaid"
        Me.colRepaid.Name = "colRepaid"
        '
        'colBal
        '
        resources.ApplyResources(Me.colBal, "colBal")
        Me.colBal.FieldName = "NetBalance"
        Me.colBal.Name = "colBal"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.Name = "colStatus"
        '
        'GridLoansInfo
        '
        Me.GridLoansInfo.DataSource = Me.LoansBS
        resources.ApplyResources(Me.GridLoansInfo, "GridLoansInfo")
        Me.GridLoansInfo.MainView = Me.GridViewLoansInfo
        Me.GridLoansInfo.Name = "GridLoansInfo"
        Me.GridLoansInfo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewLoansInfo})
        '
        'LoansBS
        '
        '
        'GridViewLoansInfo
        '
        Me.GridViewLoansInfo.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewLoansInfo.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewLoansInfo.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewLoansInfo.Appearance.Row.Font = CType(resources.GetObject("GridViewLoansInfo.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewLoansInfo.Appearance.Row.Options.UseFont = True
        Me.GridViewLoansInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colLoanID, Me.colContactID2, Me.colCName2, Me.colOriginalAmount, Me.colTotalRepaid, Me.colRemainingBalance, Me.colDirection, Me.colLoanDate, Me.colDescription})
        Me.GridViewLoansInfo.GridControl = Me.GridLoansInfo
        Me.GridViewLoansInfo.Name = "GridViewLoansInfo"
        '
        'colLoanID
        '
        resources.ApplyResources(Me.colLoanID, "colLoanID")
        Me.colLoanID.FieldName = "LoanID"
        Me.colLoanID.Name = "colLoanID"
        '
        'colContactID2
        '
        resources.ApplyResources(Me.colContactID2, "colContactID2")
        Me.colContactID2.FieldName = "ContactID"
        Me.colContactID2.Name = "colContactID2"
        '
        'colCName2
        '
        resources.ApplyResources(Me.colCName2, "colCName2")
        Me.colCName2.FieldName = "CName"
        Me.colCName2.Name = "colCName2"
        '
        'colOriginalAmount
        '
        resources.ApplyResources(Me.colOriginalAmount, "colOriginalAmount")
        Me.colOriginalAmount.FieldName = "OriginalAmount"
        Me.colOriginalAmount.Name = "colOriginalAmount"
        '
        'colTotalRepaid
        '
        resources.ApplyResources(Me.colTotalRepaid, "colTotalRepaid")
        Me.colTotalRepaid.FieldName = "TotalRepaid"
        Me.colTotalRepaid.Name = "colTotalRepaid"
        '
        'colRemainingBalance
        '
        resources.ApplyResources(Me.colRemainingBalance, "colRemainingBalance")
        Me.colRemainingBalance.FieldName = "RemainingBalance"
        Me.colRemainingBalance.Name = "colRemainingBalance"
        '
        'colDirection
        '
        resources.ApplyResources(Me.colDirection, "colDirection")
        Me.colDirection.FieldName = "Direction"
        Me.colDirection.Name = "colDirection"
        '
        'colLoanDate
        '
        resources.ApplyResources(Me.colLoanDate, "colLoanDate")
        Me.colLoanDate.FieldName = "LoanDate"
        Me.colLoanDate.Name = "colLoanDate"
        '
        'colDescription
        '
        resources.ApplyResources(Me.colDescription, "colDescription")
        Me.colDescription.FieldName = "Description"
        Me.colDescription.Name = "colDescription"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
        '
        'lblLoanID
        '
        Me.lblLoanID.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.lblLoanID.Appearance.Font = CType(resources.GetObject("lblLoanID.Appearance.Font"), System.Drawing.Font)
        Me.lblLoanID.Appearance.Options.UseBorderColor = True
        Me.lblLoanID.Appearance.Options.UseFont = True
        Me.lblLoanID.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        resources.ApplyResources(Me.lblLoanID, "lblLoanID")
        Me.lblLoanID.Name = "lblLoanID"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Name = "LabelControl8"
        '
        'txtRepayValue
        '
        resources.ApplyResources(Me.txtRepayValue, "txtRepayValue")
        Me.txtRepayValue.Name = "txtRepayValue"
        Me.txtRepayValue.Properties.Appearance.Font = CType(resources.GetObject("txtRepayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtRepayValue.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Name = "LabelControl9"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Name = "LabelControl10"
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        '
        'dtRepayDate
        '
        resources.ApplyResources(Me.dtRepayDate, "dtRepayDate")
        Me.dtRepayDate.Name = "dtRepayDate"
        Me.dtRepayDate.Properties.Appearance.Font = CType(resources.GetObject("dtRepayDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtRepayDate.Properties.Appearance.Options.UseFont = True
        Me.dtRepayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtRepayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtRepayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtRepayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnAddPay
        '
        Me.btnAddPay.Appearance.Font = CType(resources.GetObject("btnAddPay.Appearance.Font"), System.Drawing.Font)
        Me.btnAddPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAddPay, "btnAddPay")
        Me.btnAddPay.Name = "btnAddPay"
        '
        'GridPayments
        '
        Me.GridPayments.DataSource = Me.PaysBS
        resources.ApplyResources(Me.GridPayments, "GridPayments")
        Me.GridPayments.MainView = Me.GridViewPayments
        Me.GridPayments.Name = "GridPayments"
        Me.GridPayments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPayments})
        '
        'GridViewPayments
        '
        Me.GridViewPayments.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewPayments.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewPayments.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewPayments.Appearance.Row.Font = CType(resources.GetObject("GridViewPayments.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewPayments.Appearance.Row.Options.UseFont = True
        Me.GridViewPayments.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRepaymentID, Me.colLoanIDPay, Me.colAmountPay, Me.colRepaymentDate, Me.colNotesPay})
        Me.GridViewPayments.GridControl = Me.GridPayments
        Me.GridViewPayments.Name = "GridViewPayments"
        '
        'colRepaymentID
        '
        resources.ApplyResources(Me.colRepaymentID, "colRepaymentID")
        Me.colRepaymentID.FieldName = "RepaymentID"
        Me.colRepaymentID.Name = "colRepaymentID"
        '
        'colLoanIDPay
        '
        resources.ApplyResources(Me.colLoanIDPay, "colLoanIDPay")
        Me.colLoanIDPay.FieldName = "LoanID"
        Me.colLoanIDPay.Name = "colLoanIDPay"
        '
        'colAmountPay
        '
        resources.ApplyResources(Me.colAmountPay, "colAmountPay")
        Me.colAmountPay.FieldName = "Amount"
        Me.colAmountPay.Name = "colAmountPay"
        '
        'colRepaymentDate
        '
        resources.ApplyResources(Me.colRepaymentDate, "colRepaymentDate")
        Me.colRepaymentDate.FieldName = "RepaymentDate"
        Me.colRepaymentDate.Name = "colRepaymentDate"
        '
        'colNotesPay
        '
        resources.ApplyResources(Me.colNotesPay, "colNotesPay")
        Me.colNotesPay.FieldName = "Notes"
        Me.colNotesPay.Name = "colNotesPay"
        '
        'btnEditLoan
        '
        Me.btnEditLoan.Appearance.Font = CType(resources.GetObject("btnEditLoan.Appearance.Font"), System.Drawing.Font)
        Me.btnEditLoan.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEditLoan, "btnEditLoan")
        Me.btnEditLoan.Name = "btnEditLoan"
        '
        'btnEditPay
        '
        Me.btnEditPay.Appearance.Font = CType(resources.GetObject("btnEditPay.Appearance.Font"), System.Drawing.Font)
        Me.btnEditPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEditPay, "btnEditPay")
        Me.btnEditPay.Name = "btnEditPay"
        '
        'btnDelLoan
        '
        Me.btnDelLoan.Appearance.Font = CType(resources.GetObject("btnDelLoan.Appearance.Font"), System.Drawing.Font)
        Me.btnDelLoan.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDelLoan, "btnDelLoan")
        Me.btnDelLoan.Name = "btnDelLoan"
        '
        'btnDelPay
        '
        Me.btnDelPay.Appearance.Font = CType(resources.GetObject("btnDelPay.Appearance.Font"), System.Drawing.Font)
        Me.btnDelPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDelPay, "btnDelPay")
        Me.btnDelPay.Name = "btnDelPay"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = CType(resources.GetObject("GroupControl1.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.btnDelLoan)
        Me.GroupControl1.Controls.Add(Me.btnEditLoan)
        Me.GroupControl1.Controls.Add(Me.btnAddTrans)
        Me.GroupControl1.Controls.Add(Me.txtDescription)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.dtTransDate)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.txtAmount)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.cboTransType)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.ContactsCombo1)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        resources.ApplyResources(Me.GroupControl1, "GroupControl1")
        Me.GroupControl1.Name = "GroupControl1"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = CType(resources.GetObject("GroupControl2.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.btnEditPay)
        Me.GroupControl2.Controls.Add(Me.btnAddPay)
        Me.GroupControl2.Controls.Add(Me.btnDelPay)
        Me.GroupControl2.Controls.Add(Me.txtNotes)
        Me.GroupControl2.Controls.Add(Me.txtRepayValue)
        Me.GroupControl2.Controls.Add(Me.LabelControl10)
        Me.GroupControl2.Controls.Add(Me.LabelControl9)
        Me.GroupControl2.Controls.Add(Me.LabelControl8)
        Me.GroupControl2.Controls.Add(Me.lblLoanID)
        Me.GroupControl2.Controls.Add(Me.LabelControl6)
        Me.GroupControl2.Controls.Add(Me.dtRepayDate)
        resources.ApplyResources(Me.GroupControl2, "GroupControl2")
        Me.GroupControl2.Name = "GroupControl2"
        '
        'FrmExchMny
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GridPayments)
        Me.Controls.Add(Me.GridLoansInfo)
        Me.Controls.Add(Me.GridContactsInfo)
        Me.Name = "FrmExchMny"
        CType(Me.cboTransType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTransDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTransDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridContactsInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContactBalanceBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewContactInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLoansInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoansBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewLoansInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRepayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRepayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRepayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PaysBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContactsCombo1 As ContactsCombo
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboTransType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtTransDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnAddTrans As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridContactsInfo As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewContactInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colContactID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLoanBorrow As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRepaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContactBalanceBS As BindingSource
    Friend WithEvents GridLoansInfo As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewLoansInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LoansBS As BindingSource
    Friend WithEvents colLoanID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colContactID2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCName2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOriginalAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalRepaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRemainingBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDirection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLoanDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLoanID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtRepayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNotes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtRepayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnAddPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridPayments As DevExpress.XtraGrid.GridControl
    Friend WithEvents PaysBS As BindingSource
    Friend WithEvents GridViewPayments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRepaymentID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLoanIDPay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAmountPay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRepaymentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotesPay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnEditLoan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelLoan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
End Class
