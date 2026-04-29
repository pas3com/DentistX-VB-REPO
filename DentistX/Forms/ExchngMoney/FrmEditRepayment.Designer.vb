Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmEditRepayment
    Inherits XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim uiFont As New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmount = New LabelControl()
        Me.lblDate = New LabelControl()
        Me.lblNotes = New LabelControl()
        Me.txtAmount = New TextEdit()
        Me.dtRepaymentDate = New DateEdit()
        Me.txtNotes = New MemoEdit()
        Me.btnSave = New SimpleButton()
        Me.btnCancel = New SimpleButton()
        CType(Me.txtAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRepaymentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRepaymentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblAmount
        '
        Me.lblAmount.Appearance.Font = uiFont
        Me.lblAmount.Appearance.Options.UseFont = True
        Me.lblAmount.AutoSizeMode = LabelAutoSizeMode.None
        Me.lblAmount.Location = New System.Drawing.Point(12, 15)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(104, 18)
        Me.lblAmount.TabIndex = 0
        Me.lblAmount.Text = "Amount:"
        '
        'lblDate
        '
        Me.lblDate.Appearance.Font = uiFont
        Me.lblDate.Appearance.Options.UseFont = True
        Me.lblDate.AutoSizeMode = LabelAutoSizeMode.None
        Me.lblDate.Location = New System.Drawing.Point(12, 47)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(104, 18)
        Me.lblDate.TabIndex = 1
        Me.lblDate.Text = "Date:"
        '
        'lblNotes
        '
        Me.lblNotes.Appearance.Font = uiFont
        Me.lblNotes.Appearance.Options.UseFont = True
        Me.lblNotes.AutoSizeMode = LabelAutoSizeMode.None
        Me.lblNotes.Location = New System.Drawing.Point(12, 79)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(104, 18)
        Me.lblNotes.TabIndex = 2
        Me.lblNotes.Text = "Notes:"
        '
        'txtAmount
        '
        Me.txtAmount.EditValue = ""
        Me.txtAmount.EnterMoveNextControl = True
        Me.txtAmount.Location = New System.Drawing.Point(122, 12)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Properties.Appearance.Font = uiFont
        Me.txtAmount.Properties.Appearance.Options.UseFont = True
        Me.txtAmount.Size = New System.Drawing.Size(278, 24)
        Me.txtAmount.TabIndex = 3
        '
        'dtRepaymentDate
        '
        Me.dtRepaymentDate.EnterMoveNextControl = True
        Me.dtRepaymentDate.Location = New System.Drawing.Point(122, 44)
        Me.dtRepaymentDate.Name = "dtRepaymentDate"
        Me.dtRepaymentDate.Properties.Appearance.Font = uiFont
        Me.dtRepaymentDate.Properties.Appearance.Options.UseFont = True
        Me.dtRepaymentDate.Properties.Buttons.AddRange(New EditorButton() {New EditorButton(ButtonPredefines.Combo)})
        Me.dtRepaymentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New EditorButton() {New EditorButton(ButtonPredefines.Combo)})
        Me.dtRepaymentDate.Size = New System.Drawing.Size(278, 24)
        Me.dtRepaymentDate.TabIndex = 4
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(122, 76)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = uiFont
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        Me.txtNotes.Size = New System.Drawing.Size(278, 110)
        Me.txtNotes.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = uiFont
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Location = New System.Drawing.Point(218, 198)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 30)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = uiFont
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(310, 198)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 30)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        '
        'FrmEditRepayment
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(420, 244)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.dtRepaymentDate)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblAmount)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmEditRepayment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Repayment"
        CType(Me.txtAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRepaymentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRepaymentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblAmount As LabelControl
    Friend WithEvents lblDate As LabelControl
    Friend WithEvents lblNotes As LabelControl
    Friend WithEvents txtAmount As TextEdit
    Friend WithEvents dtRepaymentDate As DateEdit
    Friend WithEvents txtNotes As MemoEdit
    Friend WithEvents btnSave As SimpleButton
    Friend WithEvents btnCancel As SimpleButton
End Class
