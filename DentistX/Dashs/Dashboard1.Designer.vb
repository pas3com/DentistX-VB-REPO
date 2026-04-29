Namespace Win_Dashboards
    Partial Public Class Dashboard1
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim Dimension1 As DevExpress.DashboardCommon.Dimension = New DevExpress.DashboardCommon.Dimension()
            Dim Measure1 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim DashboardLayoutGroup1 As DevExpress.DashboardCommon.DashboardLayoutGroup = New DevExpress.DashboardCommon.DashboardLayoutGroup()
            Dim DashboardLayoutItem1 As DevExpress.DashboardCommon.DashboardLayoutItem = New DevExpress.DashboardCommon.DashboardLayoutItem()
            Me.DashboardObjectDataSource1 = New DevExpress.DashboardCommon.DashboardObjectDataSource()
            Me.MainDS1 = New DentistX.MainDS()
            Me.PatientTableAdapter1 = New DentistX.MainDSTableAdapters.PatientTableAdapter()
            Me.PieDashboardItem1 = New DevExpress.DashboardCommon.PieDashboardItem()
            CType(Me.DashboardObjectDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MainDS1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PieDashboardItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Dimension1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'DashboardObjectDataSource1
            '
            Me.DashboardObjectDataSource1.ComponentName = "DashboardObjectDataSource1"
            Me.DashboardObjectDataSource1.DataMember = "Patient"
            Me.DashboardObjectDataSource1.DataSource = Me.MainDS1
            Me.DashboardObjectDataSource1.Name = "Object Data Source 1"
            '
            'MainDS1
            '
            Me.MainDS1.DataSetName = "MainDS"
            Me.MainDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'PatientTableAdapter1
            '
            Me.PatientTableAdapter1.ClearBeforeFill = True
            '
            'PieDashboardItem1
            '
            Dimension1.DataMember = "Sex"
            Me.PieDashboardItem1.Arguments.AddRange(New DevExpress.DashboardCommon.Dimension() {Dimension1})
            Me.PieDashboardItem1.ComponentName = "PieDashboardItem1"
            Measure1.DataMember = "PatientID"
            Measure1.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Me.PieDashboardItem1.DataItemRepository.Clear()
            Me.PieDashboardItem1.DataItemRepository.Add(Dimension1, "DataItem0")
            Me.PieDashboardItem1.DataItemRepository.Add(Measure1, "DataItem1")
            Me.PieDashboardItem1.DataSource = Me.DashboardObjectDataSource1
            Me.PieDashboardItem1.InteractivityOptions.IgnoreMasterFilters = False
            Me.PieDashboardItem1.Name = "Pies 1"
            Me.PieDashboardItem1.ShowCaption = True
            Me.PieDashboardItem1.Values.AddRange(New DevExpress.DashboardCommon.Measure() {Measure1})
            '
            'Dashboard1
            '
            Me.DataSources.AddRange(New DevExpress.DashboardCommon.IDashboardDataSource() {Me.DashboardObjectDataSource1})
            Me.Items.AddRange(New DevExpress.DashboardCommon.DashboardItem() {Me.PieDashboardItem1})
            DashboardLayoutItem1.DashboardItem = Me.PieDashboardItem1
            DashboardLayoutItem1.Weight = 100.0R
            DashboardLayoutGroup1.ChildNodes.AddRange(New DevExpress.DashboardCommon.DashboardLayoutNode() {DashboardLayoutItem1})
            DashboardLayoutGroup1.DashboardItem = Nothing
            DashboardLayoutGroup1.Orientation = DevExpress.DashboardCommon.DashboardLayoutGroupOrientation.Vertical
            DashboardLayoutGroup1.Weight = 100.0R
            Me.LayoutRoot = DashboardLayoutGroup1
            Me.Title.Text = "Dashboard"
            CType(Me.DashboardObjectDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MainDS1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Dimension1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PieDashboardItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Friend WithEvents DashboardObjectDataSource1 As DevExpress.DashboardCommon.DashboardObjectDataSource
        Friend WithEvents MainDS1 As MainDS
        Friend WithEvents PatientTableAdapter1 As MainDSTableAdapters.PatientTableAdapter
        Friend WithEvents PieDashboardItem1 As DevExpress.DashboardCommon.PieDashboardItem

#End Region
    End Class
End Namespace