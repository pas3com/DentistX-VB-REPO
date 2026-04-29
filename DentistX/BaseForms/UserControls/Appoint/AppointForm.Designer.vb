<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppointForm
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
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpSearch = New DevExpress.XtraEditors.GroupControl()
        Me.chkAllAppts = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDoctor = New DevExpress.XtraEditors.TextEdit()
        Me.lblDoctor = New DevExpress.XtraEditors.LabelControl()
        Me.txtPatient = New DevExpress.XtraEditors.TextEdit()
        Me.lblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.gridResults = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAppID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFromTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDoctor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblToday = New DevExpress.XtraEditors.LabelControl()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.grpSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSearch.SuspendLayout()
        CType(Me.chkAllAppts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDoctor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.grpSearch)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.gridResults)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1194, 568)
        Me.SplitContainerControl1.SplitterPosition = 83
        Me.SplitContainerControl1.TabIndex = 0
        '
        'grpSearch
        '
        Me.grpSearch.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpSearch.AppearanceCaption.Options.UseFont = True
        Me.grpSearch.Controls.Add(Me.lblToday)
        Me.grpSearch.Controls.Add(Me.chkAllAppts)
        Me.grpSearch.Controls.Add(Me.txtDoctor)
        Me.grpSearch.Controls.Add(Me.lblDoctor)
        Me.grpSearch.Controls.Add(Me.txtPatient)
        Me.grpSearch.Controls.Add(Me.lblPatient)
        Me.grpSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpSearch.Location = New System.Drawing.Point(0, 0)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(1194, 83)
        Me.grpSearch.TabIndex = 0
        Me.grpSearch.Text = "Search by Patient or Doctor"
        '
        'chkAllAppts
        '
        Me.chkAllAppts.Location = New System.Drawing.Point(866, 57)
        Me.chkAllAppts.Name = "chkAllAppts"
        Me.chkAllAppts.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkAllAppts.Properties.Appearance.Options.UseFont = True
        Me.chkAllAppts.Properties.Caption = "Show all appointments."
        Me.chkAllAppts.Size = New System.Drawing.Size(166, 19)
        Me.chkAllAppts.TabIndex = 4
        '
        'txtDoctor
        '
        Me.txtDoctor.Location = New System.Drawing.Point(678, 55)
        Me.txtDoctor.Name = "txtDoctor"
        Me.txtDoctor.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtDoctor.Properties.Appearance.Options.UseFont = True
        Me.txtDoctor.Size = New System.Drawing.Size(144, 22)
        Me.txtDoctor.TabIndex = 3
        '
        'lblDoctor
        '
        Me.lblDoctor.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoctor.Appearance.Options.UseFont = True
        Me.lblDoctor.Location = New System.Drawing.Point(545, 59)
        Me.lblDoctor.Name = "lblDoctor"
        Me.lblDoctor.Size = New System.Drawing.Size(128, 15)
        Me.lblDoctor.TabIndex = 2
        Me.lblDoctor.Text = "Search By Doctor Name"
        '
        'txtPatient
        '
        Me.txtPatient.Location = New System.Drawing.Point(387, 55)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtPatient.Properties.Appearance.Options.UseFont = True
        Me.txtPatient.Size = New System.Drawing.Size(144, 22)
        Me.txtPatient.TabIndex = 1
        '
        'lblPatient
        '
        Me.lblPatient.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPatient.Appearance.Options.UseFont = True
        Me.lblPatient.Location = New System.Drawing.Point(254, 59)
        Me.lblPatient.Name = "lblPatient"
        Me.lblPatient.Size = New System.Drawing.Size(131, 15)
        Me.lblPatient.TabIndex = 0
        Me.lblPatient.Text = "Search By Patient Name"
        '
        'gridResults
        '
        Me.gridResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridResults.Location = New System.Drawing.Point(0, 0)
        Me.gridResults.MainView = Me.dgView
        Me.gridResults.Name = "gridResults"
        Me.gridResults.Size = New System.Drawing.Size(1194, 479)
        Me.gridResults.TabIndex = 0
        Me.gridResults.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colAppID, Me.colDate, Me.colFromTo, Me.colPaient, Me.colDoctor, Me.colDetail})
        Me.dgView.GridControl = Me.gridResults
        Me.dgView.Name = "dgView"
        '
        'colRowNum
        '
        Me.colRowNum.Caption = "Number"
        Me.colRowNum.FieldName = "Num"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.Visible = True
        Me.colRowNum.VisibleIndex = 0
        '
        'colAppID
        '
        Me.colAppID.Caption = "AppID"
        Me.colAppID.FieldName = "AppointmentID"
        Me.colAppID.Name = "colAppID"
        '
        'colDate
        '
        Me.colDate.Caption = "Appointment Date"
        Me.colDate.FieldName = "ApptDate"
        Me.colDate.Name = "colDate"
        Me.colDate.Visible = True
        Me.colDate.VisibleIndex = 1
        '
        'colFromTo
        '
        Me.colFromTo.Caption = "From : To"
        Me.colFromTo.FieldName = "FromTo"
        Me.colFromTo.Name = "colFromTo"
        Me.colFromTo.Visible = True
        Me.colFromTo.VisibleIndex = 2
        '
        'colPaient
        '
        Me.colPaient.Caption = "PatientName"
        Me.colPaient.FieldName = "PatientName"
        Me.colPaient.Name = "colPaient"
        Me.colPaient.Visible = True
        Me.colPaient.VisibleIndex = 3
        '
        'colDoctor
        '
        Me.colDoctor.Caption = "Doctor Name"
        Me.colDoctor.FieldName = "DoctorName"
        Me.colDoctor.Name = "colDoctor"
        Me.colDoctor.Visible = True
        Me.colDoctor.VisibleIndex = 4
        '
        'colDetail
        '
        Me.colDetail.Caption = "Details"
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.Name = "colDetail"
        Me.colDetail.Visible = True
        Me.colDetail.VisibleIndex = 5
        '
        'lblToday
        '
        Me.lblToday.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblToday.Appearance.Options.UseFont = True
        Me.lblToday.Appearance.Options.UseTextOptions = True
        Me.lblToday.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblToday.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblToday.Location = New System.Drawing.Point(347, 26)
        Me.lblToday.Name = "lblToday"
        Me.lblToday.Size = New System.Drawing.Size(501, 23)
        Me.lblToday.TabIndex = 5
        Me.lblToday.Text = "LabelControl1"
        '
        'AppointForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 568)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "AppointForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Today Appointments Form"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.grpSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.chkAllAppts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDoctor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpSearch As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkAllAppts As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDoctor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblDoctor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gridResults As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFromTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDoctor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAppID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblToday As DevExpress.XtraEditors.LabelControl
End Class
