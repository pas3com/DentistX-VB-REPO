<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WhatsAppForm
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

    Friend WithEvents PanelQr As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PictureQr As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LblScanHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelSuccess As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblConnected As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnStartConnection As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDisconnect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents TabConnection As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabQueue As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabFailed As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridQueue As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewQueue As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnRefreshQueue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDeleteFromQueue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridFailed As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewFailed As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnRefreshFailed As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRetryFailed As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TabSend As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LblSendNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LblSendMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendMessage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BtnSendMessage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblSendFile As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendFile As DevExpress.XtraEditors.ButtonEdit

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WhatsAppForm))
        Me.PanelQr = New DevExpress.XtraEditors.PanelControl()
        Me.PictureQr = New DevExpress.XtraEditors.PictureEdit()
        Me.LblScanHint = New DevExpress.XtraEditors.LabelControl()
        Me.PanelSuccess = New DevExpress.XtraEditors.PanelControl()
        Me.LblConnected = New DevExpress.XtraEditors.LabelControl()
        Me.BtnStartConnection = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDisconnect = New DevExpress.XtraEditors.SimpleButton()
        Me.LblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.TabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.TabConnection = New DevExpress.XtraTab.XtraTabPage()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.TabQueue = New DevExpress.XtraTab.XtraTabPage()
        Me.GridQueue = New DevExpress.XtraGrid.GridControl()
        Me.ViewQueue = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnRefreshQueue = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDeleteFromQueue = New DevExpress.XtraEditors.SimpleButton()
        Me.TabFailed = New DevExpress.XtraTab.XtraTabPage()
        Me.GridFailed = New DevExpress.XtraGrid.GridControl()
        Me.ViewFailed = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnRefreshFailed = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnRetryFailed = New DevExpress.XtraEditors.SimpleButton()
        Me.TabSend = New DevExpress.XtraTab.XtraTabPage()
        Me.LblSendNumber = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LblSendMessage = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.LblSendFile = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendFile = New DevExpress.XtraEditors.ButtonEdit()
        Me.BtnSendMessage = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelQr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelQr.SuspendLayout()
        CType(Me.PictureQr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelSuccess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSuccess.SuspendLayout()
        CType(Me.TabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.TabConnection.SuspendLayout()
        Me.TabQueue.SuspendLayout()
        CType(Me.GridQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabFailed.SuspendLayout()
        CType(Me.GridFailed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewFailed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabSend.SuspendLayout()
        CType(Me.TxtSendNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSendFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelQr
        '
        Me.PanelQr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelQr.Controls.Add(Me.PictureQr)
        Me.PanelQr.Controls.Add(Me.LblScanHint)
        resources.ApplyResources(Me.PanelQr, "PanelQr")
        Me.PanelQr.Name = "PanelQr"
        '
        'PictureQr
        '
        resources.ApplyResources(Me.PictureQr, "PictureQr")
        Me.PictureQr.Name = "PictureQr"
        Me.PictureQr.Properties.NullText = resources.GetString("PictureQr.Properties.NullText")
        Me.PictureQr.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'LblScanHint
        '
        Me.LblScanHint.Appearance.Font = CType(resources.GetObject("LblScanHint.Appearance.Font"), System.Drawing.Font)
        Me.LblScanHint.Appearance.Options.UseFont = True
        Me.LblScanHint.Appearance.Options.UseTextOptions = True
        Me.LblScanHint.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LblScanHint, "LblScanHint")
        Me.LblScanHint.Name = "LblScanHint"
        '
        'PanelSuccess
        '
        Me.PanelSuccess.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelSuccess.Controls.Add(Me.LblConnected)
        resources.ApplyResources(Me.PanelSuccess, "PanelSuccess")
        Me.PanelSuccess.Name = "PanelSuccess"
        '
        'LblConnected
        '
        Me.LblConnected.Appearance.Font = CType(resources.GetObject("LblConnected.Appearance.Font"), System.Drawing.Font)
        Me.LblConnected.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LblConnected.Appearance.Options.UseFont = True
        Me.LblConnected.Appearance.Options.UseForeColor = True
        Me.LblConnected.Appearance.Options.UseTextOptions = True
        Me.LblConnected.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LblConnected, "LblConnected")
        Me.LblConnected.Name = "LblConnected"
        '
        'BtnStartConnection
        '
        Me.BtnStartConnection.Appearance.Font = CType(resources.GetObject("BtnStartConnection.Appearance.Font"), System.Drawing.Font)
        Me.BtnStartConnection.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnStartConnection, "BtnStartConnection")
        Me.BtnStartConnection.Name = "BtnStartConnection"
        '
        'BtnDisconnect
        '
        Me.BtnDisconnect.Appearance.Font = CType(resources.GetObject("BtnDisconnect.Appearance.Font"), System.Drawing.Font)
        Me.BtnDisconnect.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnDisconnect, "BtnDisconnect")
        Me.BtnDisconnect.Name = "BtnDisconnect"
        '
        'LblStatus
        '
        Me.LblStatus.Appearance.Font = CType(resources.GetObject("LblStatus.Appearance.Font"), System.Drawing.Font)
        Me.LblStatus.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LblStatus.Appearance.Options.UseFont = True
        Me.LblStatus.Appearance.Options.UseForeColor = True
        Me.LblStatus.Appearance.Options.UseTextOptions = True
        Me.LblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LblStatus, "LblStatus")
        Me.LblStatus.Name = "LblStatus"
        '
        'TabControl
        '
        Me.TabControl.AppearancePage.Header.Font = CType(resources.GetObject("TabControl.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.TabControl.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.TabControl, "TabControl")
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedTabPage = Me.TabConnection
        Me.TabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabConnection, Me.TabQueue, Me.TabFailed, Me.TabSend})
        '
        'TabConnection
        '
        Me.TabConnection.Controls.Add(Me.btnRefresh)
        Me.TabConnection.Controls.Add(Me.LblStatus)
        Me.TabConnection.Controls.Add(Me.PanelSuccess)
        Me.TabConnection.Controls.Add(Me.PanelQr)
        Me.TabConnection.Controls.Add(Me.BtnStartConnection)
        Me.TabConnection.Controls.Add(Me.BtnDisconnect)
        Me.TabConnection.Name = "TabConnection"
        resources.ApplyResources(Me.TabConnection, "TabConnection")
        '
        'btnRefresh
        '
        Me.btnRefresh.Appearance.Font = CType(resources.GetObject("btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.btnRefresh.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnRefresh, "btnRefresh")
        Me.btnRefresh.Name = "btnRefresh"
        '
        'TabQueue
        '
        Me.TabQueue.Controls.Add(Me.GridQueue)
        Me.TabQueue.Controls.Add(Me.BtnRefreshQueue)
        Me.TabQueue.Controls.Add(Me.BtnDeleteFromQueue)
        Me.TabQueue.Name = "TabQueue"
        resources.ApplyResources(Me.TabQueue, "TabQueue")
        '
        'GridQueue
        '
        resources.ApplyResources(Me.GridQueue, "GridQueue")
        Me.GridQueue.MainView = Me.ViewQueue
        Me.GridQueue.Name = "GridQueue"
        Me.GridQueue.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewQueue})
        '
        'ViewQueue
        '
        Me.ViewQueue.Appearance.HeaderPanel.Font = CType(resources.GetObject("ViewQueue.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ViewQueue.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewQueue.Appearance.Row.Font = CType(resources.GetObject("ViewQueue.Appearance.Row.Font"), System.Drawing.Font)
        Me.ViewQueue.Appearance.Row.Options.UseFont = True
        Me.ViewQueue.GridControl = Me.GridQueue
        Me.ViewQueue.Name = "ViewQueue"
        Me.ViewQueue.OptionsBehavior.Editable = False
        '
        'BtnRefreshQueue
        '
        resources.ApplyResources(Me.BtnRefreshQueue, "BtnRefreshQueue")
        Me.BtnRefreshQueue.Name = "BtnRefreshQueue"
        '
        'BtnDeleteFromQueue
        '
        resources.ApplyResources(Me.BtnDeleteFromQueue, "BtnDeleteFromQueue")
        Me.BtnDeleteFromQueue.Name = "BtnDeleteFromQueue"
        '
        'TabFailed
        '
        Me.TabFailed.Controls.Add(Me.GridFailed)
        Me.TabFailed.Controls.Add(Me.BtnRefreshFailed)
        Me.TabFailed.Controls.Add(Me.BtnRetryFailed)
        Me.TabFailed.Name = "TabFailed"
        resources.ApplyResources(Me.TabFailed, "TabFailed")
        '
        'GridFailed
        '
        resources.ApplyResources(Me.GridFailed, "GridFailed")
        Me.GridFailed.MainView = Me.ViewFailed
        Me.GridFailed.Name = "GridFailed"
        Me.GridFailed.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewFailed})
        '
        'ViewFailed
        '
        Me.ViewFailed.Appearance.HeaderPanel.Font = CType(resources.GetObject("ViewFailed.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ViewFailed.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewFailed.Appearance.Row.Font = CType(resources.GetObject("ViewFailed.Appearance.Row.Font"), System.Drawing.Font)
        Me.ViewFailed.Appearance.Row.Options.UseFont = True
        Me.ViewFailed.GridControl = Me.GridFailed
        Me.ViewFailed.Name = "ViewFailed"
        Me.ViewFailed.OptionsBehavior.Editable = False
        '
        'BtnRefreshFailed
        '
        resources.ApplyResources(Me.BtnRefreshFailed, "BtnRefreshFailed")
        Me.BtnRefreshFailed.Name = "BtnRefreshFailed"
        '
        'BtnRetryFailed
        '
        resources.ApplyResources(Me.BtnRetryFailed, "BtnRetryFailed")
        Me.BtnRetryFailed.Name = "BtnRetryFailed"
        '
        'TabSend
        '
        Me.TabSend.Controls.Add(Me.LblSendNumber)
        Me.TabSend.Controls.Add(Me.TxtSendNumber)
        Me.TabSend.Controls.Add(Me.LblSendMessage)
        Me.TabSend.Controls.Add(Me.TxtSendMessage)
        Me.TabSend.Controls.Add(Me.LblSendFile)
        Me.TabSend.Controls.Add(Me.TxtSendFile)
        Me.TabSend.Controls.Add(Me.BtnSendMessage)
        Me.TabSend.Name = "TabSend"
        resources.ApplyResources(Me.TabSend, "TabSend")
        '
        'LblSendNumber
        '
        Me.LblSendNumber.Appearance.Font = CType(resources.GetObject("LblSendNumber.Appearance.Font"), System.Drawing.Font)
        Me.LblSendNumber.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LblSendNumber, "LblSendNumber")
        Me.LblSendNumber.Name = "LblSendNumber"
        '
        'TxtSendNumber
        '
        resources.ApplyResources(Me.TxtSendNumber, "TxtSendNumber")
        Me.TxtSendNumber.Name = "TxtSendNumber"
        Me.TxtSendNumber.Properties.Appearance.Font = CType(resources.GetObject("TxtSendNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSendNumber.Properties.Appearance.Options.UseFont = True
        '
        'LblSendMessage
        '
        Me.LblSendMessage.Appearance.Font = CType(resources.GetObject("LblSendMessage.Appearance.Font"), System.Drawing.Font)
        Me.LblSendMessage.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LblSendMessage, "LblSendMessage")
        Me.LblSendMessage.Name = "LblSendMessage"
        '
        'TxtSendMessage
        '
        resources.ApplyResources(Me.TxtSendMessage, "TxtSendMessage")
        Me.TxtSendMessage.Name = "TxtSendMessage"
        Me.TxtSendMessage.Properties.Appearance.Font = CType(resources.GetObject("TxtSendMessage.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSendMessage.Properties.Appearance.Options.UseFont = True
        '
        'LblSendFile
        '
        Me.LblSendFile.Appearance.Font = CType(resources.GetObject("LblSendFile.Appearance.Font"), System.Drawing.Font)
        Me.LblSendFile.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LblSendFile, "LblSendFile")
        Me.LblSendFile.Name = "LblSendFile"
        '
        'TxtSendFile
        '
        resources.ApplyResources(Me.TxtSendFile, "TxtSendFile")
        Me.TxtSendFile.Name = "TxtSendFile"
        Me.TxtSendFile.Properties.Appearance.Font = CType(resources.GetObject("TxtSendFile.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSendFile.Properties.Appearance.Options.UseFont = True
        Me.TxtSendFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'BtnSendMessage
        '
        Me.BtnSendMessage.Appearance.Font = CType(resources.GetObject("BtnSendMessage.Appearance.Font"), System.Drawing.Font)
        Me.BtnSendMessage.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnSendMessage, "BtnSendMessage")
        Me.BtnSendMessage.Name = "BtnSendMessage"
        '
        'WhatsAppForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl)
        Me.Name = "WhatsAppForm"
        CType(Me.PanelQr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelQr.ResumeLayout(False)
        CType(Me.PictureQr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelSuccess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSuccess.ResumeLayout(False)
        CType(Me.TabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl.ResumeLayout(False)
        Me.TabConnection.ResumeLayout(False)
        Me.TabQueue.ResumeLayout(False)
        CType(Me.GridQueue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewQueue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabFailed.ResumeLayout(False)
        CType(Me.GridFailed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewFailed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabSend.ResumeLayout(False)
        Me.TabSend.PerformLayout()
        CType(Me.TxtSendNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSendFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
End Class
