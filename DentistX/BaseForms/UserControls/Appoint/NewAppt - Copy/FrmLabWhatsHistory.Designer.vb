<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLabWhatsHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLabWhatsHistory))
        Me.splitMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gridHistory = New DevExpress.XtraGrid.GridControl()
        Me.viewHistory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.panelDetail = New DevExpress.XtraEditors.PanelControl()
        Me.memoMessageBody = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.listDetails = New DevExpress.XtraEditors.ListBoxControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.memoNote = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtReceiveDate = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtMsgDate = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSubject = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPatient = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtClinicLab = New DevExpress.XtraEditors.TextEdit()
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.dtTo = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dtFrom = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lookUpSubject = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lookUpLab = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        CType(Me.splitMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        CType(Me.gridHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelDetail.SuspendLayout()
        CType(Me.memoMessageBody.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memoNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceiveDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMsgDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClinicLab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTop.SuspendLayout()
        CType(Me.dtTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpLab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitMain
        '
        resources.ApplyResources(Me.splitMain, "splitMain")
        Me.splitMain.CaptionImageOptions.ImageKey = resources.GetString("splitMain.CaptionImageOptions.ImageKey")
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        resources.ApplyResources(Me.splitMain.Panel1, "splitMain.Panel1")
        Me.splitMain.Panel1.Controls.Add(Me.gridHistory)
        '
        'splitMain.Panel2
        '
        resources.ApplyResources(Me.splitMain.Panel2, "splitMain.Panel2")
        Me.splitMain.Panel2.Controls.Add(Me.panelDetail)
        Me.splitMain.SplitterPosition = 676
        '
        'gridHistory
        '
        resources.ApplyResources(Me.gridHistory, "gridHistory")
        Me.gridHistory.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridHistory.EmbeddedNavigator.AccessibleDescription")
        Me.gridHistory.EmbeddedNavigator.AccessibleName = resources.GetString("gridHistory.EmbeddedNavigator.AccessibleName")
        Me.gridHistory.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridHistory.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridHistory.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridHistory.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridHistory.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridHistory.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridHistory.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridHistory.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridHistory.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridHistory.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridHistory.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridHistory.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridHistory.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridHistory.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridHistory.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridHistory.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridHistory.EmbeddedNavigator.ToolTip = resources.GetString("gridHistory.EmbeddedNavigator.ToolTip")
        Me.gridHistory.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridHistory.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridHistory.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridHistory.EmbeddedNavigator.ToolTipTitle")
        Me.gridHistory.MainView = Me.viewHistory
        Me.gridHistory.Name = "gridHistory"
        Me.gridHistory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewHistory})
        '
        'viewHistory
        '
        resources.ApplyResources(Me.viewHistory, "viewHistory")
        Me.viewHistory.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewHistory.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewHistory.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewHistory.Appearance.Row.Font = CType(resources.GetObject("viewHistory.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewHistory.Appearance.Row.Options.UseFont = True
        Me.viewHistory.GridControl = Me.gridHistory
        Me.viewHistory.Name = "viewHistory"
        Me.viewHistory.OptionsBehavior.Editable = False
        Me.viewHistory.OptionsFind.AlwaysVisible = True
        Me.viewHistory.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewHistory.OptionsView.ShowGroupPanel = False
        '
        'panelDetail
        '
        resources.ApplyResources(Me.panelDetail, "panelDetail")
        Me.panelDetail.Controls.Add(Me.memoMessageBody)
        Me.panelDetail.Controls.Add(Me.LabelControl11)
        Me.panelDetail.Controls.Add(Me.listDetails)
        Me.panelDetail.Controls.Add(Me.LabelControl10)
        Me.panelDetail.Controls.Add(Me.memoNote)
        Me.panelDetail.Controls.Add(Me.LabelControl9)
        Me.panelDetail.Controls.Add(Me.txtReceiveDate)
        Me.panelDetail.Controls.Add(Me.LabelControl8)
        Me.panelDetail.Controls.Add(Me.txtMsgDate)
        Me.panelDetail.Controls.Add(Me.LabelControl7)
        Me.panelDetail.Controls.Add(Me.txtSubject)
        Me.panelDetail.Controls.Add(Me.LabelControl6)
        Me.panelDetail.Controls.Add(Me.txtPatient)
        Me.panelDetail.Controls.Add(Me.LabelControl5)
        Me.panelDetail.Controls.Add(Me.txtClinicLab)
        Me.panelDetail.Name = "panelDetail"
        '
        'memoMessageBody
        '
        resources.ApplyResources(Me.memoMessageBody, "memoMessageBody")
        Me.memoMessageBody.Name = "memoMessageBody"
        Me.memoMessageBody.Properties.Appearance.Font = CType(resources.GetObject("memoMessageBody.Properties.Appearance.Font"), System.Drawing.Font)
        Me.memoMessageBody.Properties.Appearance.Options.UseFont = True
        Me.memoMessageBody.Properties.ReadOnly = True
        '
        'LabelControl11
        '
        resources.ApplyResources(Me.LabelControl11, "LabelControl11")
        Me.LabelControl11.Appearance.Font = CType(resources.GetObject("LabelControl11.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Name = "LabelControl11"
        '
        'listDetails
        '
        resources.ApplyResources(Me.listDetails, "listDetails")
        Me.listDetails.Appearance.Font = CType(resources.GetObject("listDetails.Appearance.Font"), System.Drawing.Font)
        Me.listDetails.Appearance.Options.UseFont = True
        Me.listDetails.Name = "listDetails"
        '
        'LabelControl10
        '
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Name = "LabelControl10"
        '
        'memoNote
        '
        resources.ApplyResources(Me.memoNote, "memoNote")
        Me.memoNote.Name = "memoNote"
        Me.memoNote.Properties.Appearance.Font = CType(resources.GetObject("memoNote.Properties.Appearance.Font"), System.Drawing.Font)
        Me.memoNote.Properties.Appearance.Options.UseFont = True
        Me.memoNote.Properties.ReadOnly = True
        '
        'LabelControl9
        '
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Name = "LabelControl9"
        '
        'txtReceiveDate
        '
        resources.ApplyResources(Me.txtReceiveDate, "txtReceiveDate")
        Me.txtReceiveDate.Name = "txtReceiveDate"
        Me.txtReceiveDate.Properties.Appearance.Font = CType(resources.GetObject("txtReceiveDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtReceiveDate.Properties.Appearance.Options.UseFont = True
        Me.txtReceiveDate.Properties.ReadOnly = True
        '
        'LabelControl8
        '
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Name = "LabelControl8"
        '
        'txtMsgDate
        '
        resources.ApplyResources(Me.txtMsgDate, "txtMsgDate")
        Me.txtMsgDate.Name = "txtMsgDate"
        Me.txtMsgDate.Properties.Appearance.Font = CType(resources.GetObject("txtMsgDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtMsgDate.Properties.Appearance.Options.UseFont = True
        Me.txtMsgDate.Properties.ReadOnly = True
        '
        'LabelControl7
        '
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Name = "LabelControl7"
        '
        'txtSubject
        '
        resources.ApplyResources(Me.txtSubject, "txtSubject")
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Properties.Appearance.Font = CType(resources.GetObject("txtSubject.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtSubject.Properties.Appearance.Options.UseFont = True
        Me.txtSubject.Properties.ReadOnly = True
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Name = "LabelControl6"
        '
        'txtPatient
        '
        resources.ApplyResources(Me.txtPatient, "txtPatient")
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Properties.Appearance.Font = CType(resources.GetObject("txtPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPatient.Properties.Appearance.Options.UseFont = True
        Me.txtPatient.Properties.ReadOnly = True
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'txtClinicLab
        '
        resources.ApplyResources(Me.txtClinicLab, "txtClinicLab")
        Me.txtClinicLab.Name = "txtClinicLab"
        Me.txtClinicLab.Properties.Appearance.Font = CType(resources.GetObject("txtClinicLab.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtClinicLab.Properties.Appearance.Options.UseFont = True
        Me.txtClinicLab.Properties.ReadOnly = True
        '
        'panelTop
        '
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Controls.Add(Me.btnClose)
        Me.panelTop.Controls.Add(Me.btnClear)
        Me.panelTop.Controls.Add(Me.btnSearch)
        Me.panelTop.Controls.Add(Me.dtTo)
        Me.panelTop.Controls.Add(Me.LabelControl4)
        Me.panelTop.Controls.Add(Me.dtFrom)
        Me.panelTop.Controls.Add(Me.LabelControl3)
        Me.panelTop.Controls.Add(Me.lookUpSubject)
        Me.panelTop.Controls.Add(Me.LabelControl2)
        Me.panelTop.Controls.Add(Me.lookUpLab)
        Me.panelTop.Controls.Add(Me.LabelControl1)
        Me.panelTop.Name = "panelTop"
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.ImageOptions.ImageKey = resources.GetString("btnClose.ImageOptions.ImageKey")
        Me.btnClose.Name = "btnClose"
        '
        'btnClear
        '
        resources.ApplyResources(Me.btnClear, "btnClear")
        Me.btnClear.Appearance.Font = CType(resources.GetObject("btnClear.Appearance.Font"), System.Drawing.Font)
        Me.btnClear.Appearance.Options.UseFont = True
        Me.btnClear.ImageOptions.ImageKey = resources.GetString("btnClear.ImageOptions.ImageKey")
        Me.btnClear.Name = "btnClear"
        '
        'btnSearch
        '
        resources.ApplyResources(Me.btnSearch, "btnSearch")
        Me.btnSearch.Appearance.Font = CType(resources.GetObject("btnSearch.Appearance.Font"), System.Drawing.Font)
        Me.btnSearch.Appearance.Options.UseFont = True
        Me.btnSearch.ImageOptions.ImageKey = resources.GetString("btnSearch.ImageOptions.ImageKey")
        Me.btnSearch.Name = "btnSearch"
        '
        'dtTo
        '
        resources.ApplyResources(Me.dtTo, "dtTo")
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Properties.Appearance.Font = CType(resources.GetObject("dtTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTo.Properties.Appearance.Options.UseFont = True
        Me.dtTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'dtFrom
        '
        resources.ApplyResources(Me.dtFrom, "dtFrom")
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Properties.Appearance.Font = CType(resources.GetObject("dtFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtFrom.Properties.Appearance.Options.UseFont = True
        Me.dtFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'lookUpSubject
        '
        resources.ApplyResources(Me.lookUpSubject, "lookUpSubject")
        Me.lookUpSubject.Name = "lookUpSubject"
        Me.lookUpSubject.Properties.Appearance.Font = CType(resources.GetObject("lookUpSubject.Properties.Appearance.Font"), System.Drawing.Font)
        Me.lookUpSubject.Properties.Appearance.Options.UseFont = True
        Me.lookUpSubject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("lookUpSubject.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.lookUpSubject.Properties.NullText = resources.GetString("lookUpSubject.Properties.NullText")
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'lookUpLab
        '
        resources.ApplyResources(Me.lookUpLab, "lookUpLab")
        Me.lookUpLab.Name = "lookUpLab"
        Me.lookUpLab.Properties.Appearance.Font = CType(resources.GetObject("lookUpLab.Properties.Appearance.Font"), System.Drawing.Font)
        Me.lookUpLab.Properties.Appearance.Options.UseFont = True
        Me.lookUpLab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("lookUpLab.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.lookUpLab.Properties.NullText = resources.GetString("lookUpLab.Properties.NullText")
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'FrmLabWhatsHistory
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "FrmLabWhatsHistory"
        CType(Me.splitMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.Panel1.ResumeLayout(False)
        CType(Me.splitMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        CType(Me.gridHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelDetail.ResumeLayout(False)
        Me.panelDetail.PerformLayout()
        CType(Me.memoMessageBody.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memoNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceiveDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMsgDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClinicLab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        Me.panelTop.PerformLayout()
        CType(Me.dtTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpLab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lookUpSubject As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lookUpLab As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents splitMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gridHistory As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewHistory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents panelDetail As DevExpress.XtraEditors.PanelControl
    Friend WithEvents memoMessageBody As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents listDetails As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents memoNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtReceiveDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtMsgDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSubject As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtClinicLab As DevExpress.XtraEditors.TextEdit
End Class
