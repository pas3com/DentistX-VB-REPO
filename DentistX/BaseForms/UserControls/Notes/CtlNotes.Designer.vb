<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtlNotes
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtlNotes))
        Me.NotesGroup = New DevExpress.XtraEditors.GroupControl()
        Me.NotesDate = New DevExpress.XtraEditors.DateEdit()
        Me.btAddNotes = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.btEditNotes = New DevExpress.XtraEditors.SimpleButton()
        Me.UltraLabel6 = New DevExpress.XtraEditors.LabelControl()
        Me.NotesNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButton33 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel7 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton34 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton35 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton36 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox7 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton37 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton38 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.NotesSave = New System.Windows.Forms.ToolStripButton()
        Me.UltraLabel9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.NotesGrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNoteID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNoteDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNote = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.NotesGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NotesGroup.SuspendLayout()
        CType(Me.NotesDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NotesNavigator.SuspendLayout()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NotesGroup
        '
        Me.NotesGroup.AppearanceCaption.Font = CType(resources.GetObject("NotesGroup.AppearanceCaption.Font"), System.Drawing.Font)
        Me.NotesGroup.AppearanceCaption.ForeColor = System.Drawing.Color.Blue
        Me.NotesGroup.AppearanceCaption.Options.UseFont = True
        Me.NotesGroup.AppearanceCaption.Options.UseForeColor = True
        Me.NotesGroup.Controls.Add(Me.NotesDate)
        Me.NotesGroup.Controls.Add(Me.btAddNotes)
        Me.NotesGroup.Controls.Add(Me.btnDel)
        Me.NotesGroup.Controls.Add(Me.btEditNotes)
        Me.NotesGroup.Controls.Add(Me.UltraLabel6)
        Me.NotesGroup.Controls.Add(Me.UltraLabel9)
        Me.NotesGroup.Controls.Add(Me.txtNotes)
        Me.NotesGroup.Controls.Add(Me.NotesNavigator)
        resources.ApplyResources(Me.NotesGroup, "NotesGroup")
        Me.NotesGroup.Name = "NotesGroup"
        '
        'NotesDate
        '
        resources.ApplyResources(Me.NotesDate, "NotesDate")
        Me.NotesDate.Name = "NotesDate"
        Me.NotesDate.Properties.Appearance.Font = CType(resources.GetObject("NotesDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesDate.Properties.Appearance.Options.UseFont = True
        Me.NotesDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("NotesDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.NotesDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("NotesDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btAddNotes
        '
        Me.btAddNotes.Appearance.Font = CType(resources.GetObject("btAddNotes.Appearance.Font"), System.Drawing.Font)
        Me.btAddNotes.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btAddNotes, "btAddNotes")
        Me.btAddNotes.Name = "btAddNotes"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = CType(resources.GetObject("btnDel.Appearance.Font"), System.Drawing.Font)
        Me.btnDel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDel, "btnDel")
        Me.btnDel.Name = "btnDel"
        '
        'btEditNotes
        '
        Me.btEditNotes.Appearance.Font = CType(resources.GetObject("btEditNotes.Appearance.Font"), System.Drawing.Font)
        Me.btEditNotes.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btEditNotes, "btEditNotes")
        Me.btEditNotes.Name = "btEditNotes"
        '
        'UltraLabel6
        '
        Me.UltraLabel6.Appearance.Font = CType(resources.GetObject("UltraLabel6.Appearance.Font"), System.Drawing.Font)
        Me.UltraLabel6.Appearance.Options.UseFont = True
        Me.UltraLabel6.LineVisible = True
        resources.ApplyResources(Me.UltraLabel6, "UltraLabel6")
        Me.UltraLabel6.Name = "UltraLabel6"
        '
        'NotesNavigator
        '
        Me.NotesNavigator.AddNewItem = Me.ToolStripButton33
        Me.NotesNavigator.CountItem = Me.ToolStripLabel7
        Me.NotesNavigator.DeleteItem = Me.ToolStripButton34
        resources.ApplyResources(Me.NotesNavigator, "NotesNavigator")
        Me.NotesNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton35, Me.ToolStripButton36, Me.ToolStripSeparator17, Me.ToolStripTextBox7, Me.ToolStripLabel7, Me.ToolStripSeparator18, Me.ToolStripButton37, Me.ToolStripButton38, Me.ToolStripSeparator19, Me.ToolStripButton33, Me.ToolStripButton34, Me.NotesSave})
        Me.NotesNavigator.MoveFirstItem = Me.ToolStripButton35
        Me.NotesNavigator.MoveLastItem = Me.ToolStripButton38
        Me.NotesNavigator.MoveNextItem = Me.ToolStripButton37
        Me.NotesNavigator.MovePreviousItem = Me.ToolStripButton36
        Me.NotesNavigator.Name = "NotesNavigator"
        Me.NotesNavigator.PositionItem = Me.ToolStripTextBox7
        '
        'ToolStripButton33
        '
        Me.ToolStripButton33.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton33, "ToolStripButton33")
        Me.ToolStripButton33.Name = "ToolStripButton33"
        '
        'ToolStripLabel7
        '
        Me.ToolStripLabel7.Name = "ToolStripLabel7"
        resources.ApplyResources(Me.ToolStripLabel7, "ToolStripLabel7")
        '
        'ToolStripButton34
        '
        Me.ToolStripButton34.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton34, "ToolStripButton34")
        Me.ToolStripButton34.Name = "ToolStripButton34"
        '
        'ToolStripButton35
        '
        Me.ToolStripButton35.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton35, "ToolStripButton35")
        Me.ToolStripButton35.Name = "ToolStripButton35"
        '
        'ToolStripButton36
        '
        Me.ToolStripButton36.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton36, "ToolStripButton36")
        Me.ToolStripButton36.Name = "ToolStripButton36"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        resources.ApplyResources(Me.ToolStripSeparator17, "ToolStripSeparator17")
        '
        'ToolStripTextBox7
        '
        resources.ApplyResources(Me.ToolStripTextBox7, "ToolStripTextBox7")
        Me.ToolStripTextBox7.Name = "ToolStripTextBox7"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        resources.ApplyResources(Me.ToolStripSeparator18, "ToolStripSeparator18")
        '
        'ToolStripButton37
        '
        Me.ToolStripButton37.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton37, "ToolStripButton37")
        Me.ToolStripButton37.Name = "ToolStripButton37"
        '
        'ToolStripButton38
        '
        Me.ToolStripButton38.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton38, "ToolStripButton38")
        Me.ToolStripButton38.Name = "ToolStripButton38"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        resources.ApplyResources(Me.ToolStripSeparator19, "ToolStripSeparator19")
        '
        'NotesSave
        '
        Me.NotesSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.NotesSave, "NotesSave")
        Me.NotesSave.Name = "NotesSave"
        '
        'UltraLabel9
        '
        Me.UltraLabel9.Appearance.Font = CType(resources.GetObject("UltraLabel9.Appearance.Font"), System.Drawing.Font)
        Me.UltraLabel9.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.UltraLabel9, "UltraLabel9")
        Me.UltraLabel9.Name = "UltraLabel9"
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        '
        'NotesGrid
        '
        resources.ApplyResources(Me.NotesGrid, "NotesGrid")
        Me.NotesGrid.MainView = Me.GridView1
        Me.NotesGrid.Name = "NotesGrid"
        Me.NotesGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FixedLine.Font = CType(resources.GetObject("GridView1.Appearance.FixedLine.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.FixedLine.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Preview.Font = CType(resources.GetObject("GridView1.Appearance.Preview.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Preview.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView1.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.ColNoteID, Me.ColPatientID, Me.ColNoteDate, Me.ColNote})
        Me.GridView1.GridControl = Me.NotesGrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowRowSizing = True
        Me.GridView1.OptionsDetail.SmartDetailHeight = True
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.RowAutoHeight = True
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'ColNoteID
        '
        resources.ApplyResources(Me.ColNoteID, "ColNoteID")
        Me.ColNoteID.FieldName = "NoteID"
        Me.ColNoteID.Name = "ColNoteID"
        Me.ColNoteID.UnboundDataType = GetType(Integer)
        '
        'ColPatientID
        '
        resources.ApplyResources(Me.ColPatientID, "ColPatientID")
        Me.ColPatientID.FieldName = "PatientID"
        Me.ColPatientID.Name = "ColPatientID"
        Me.ColPatientID.UnboundDataType = GetType(Integer)
        '
        'ColNoteDate
        '
        resources.ApplyResources(Me.ColNoteDate, "ColNoteDate")
        Me.ColNoteDate.FieldName = "NoteDate"
        Me.ColNoteDate.Name = "ColNoteDate"
        Me.ColNoteDate.UnboundDataType = GetType(Date)
        '
        'ColNote
        '
        resources.ApplyResources(Me.ColNote, "ColNote")
        Me.ColNote.FieldName = "Note"
        Me.ColNote.Name = "ColNote"
        Me.ColNote.UnboundDataType = GetType(String)
        '
        'CtlNotes
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.NotesGrid)
        Me.Controls.Add(Me.NotesGroup)
        Me.Name = "CtlNotes"
        CType(Me.NotesGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NotesGroup.ResumeLayout(False)
        Me.NotesGroup.PerformLayout()
        CType(Me.NotesDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NotesNavigator.ResumeLayout(False)
        Me.NotesNavigator.PerformLayout()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents NotesGroup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents NotesGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColNoteID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNoteDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NotesDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btAddNotes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEditNotes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents UltraLabel6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents UltraLabel9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents NotesNavigator As BindingNavigator
    Friend WithEvents ToolStripButton33 As ToolStripButton
    Friend WithEvents ToolStripLabel7 As ToolStripLabel
    Friend WithEvents ToolStripButton34 As ToolStripButton
    Friend WithEvents ToolStripButton35 As ToolStripButton
    Friend WithEvents ToolStripButton36 As ToolStripButton
    Friend WithEvents ToolStripSeparator17 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox7 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator18 As ToolStripSeparator
    Friend WithEvents ToolStripButton37 As ToolStripButton
    Friend WithEvents ToolStripButton38 As ToolStripButton
    Friend WithEvents ToolStripSeparator19 As ToolStripSeparator
    Friend WithEvents NotesSave As ToolStripButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
End Class
