Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class TrtCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _TrtT As String = "Trt"

    Public WriteOnly Property SetTreatType() As String
        Set(ByVal value As String)
            _TrtT = ValidateTrtColumnIdentifier(value)
            BindTrts()
        End Set
    End Property

    Public Event TreatValueChanged(ByVal sender As Object, ByVal e As TreatIndexChangedEvent)

    Private _selectedTrtID As Integer
    Public Property TrtID As Integer
        Get
            Return _selectedTrtID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedTrtID, value) Then
                _selectedTrtID = value
                OnPropertyChanged(NameOf(TrtID))
                UpdateTreatComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedTrt As String
    Public Property Trt As String
        Get
            Return _selectedTrt
        End Get
        Set(value As String)
            If Not Equals(_selectedTrt, value) Then
                _selectedTrt = value
                OnPropertyChanged(NameOf(Trt))
            End If
        End Set
    End Property

    Private _allRows As New List(Of Treat)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean

    Private _btnAddVisible As Boolean = True
    Private _btnSearchVisible As Boolean = True

    <Browsable(True), Category("Behavior"), Description("Shows or hides the add (+) button; adjusts layout and width when Dock is None.")>
    Public Property BtnAddVisible As Boolean
        Get
            Return _btnAddVisible
        End Get
        Set(value As Boolean)
            If _btnAddVisible <> value Then
                _btnAddVisible = value
                ApplyToolbarLayout()
                OnPropertyChanged(NameOf(BtnAddVisible))
            End If
        End Set
    End Property

    <Browsable(True), Category("Behavior"), Description("Shows or hides the search (flyout) button; adjusts layout and width when Dock is None.")>
    Public Property BtnSearchVisible As Boolean
        Get
            Return _btnSearchVisible
        End Get
        Set(value As Boolean)
            If _btnSearchVisible <> value Then
                _btnSearchVisible = value
                ApplyToolbarLayout()
                OnPropertyChanged(NameOf(BtnSearchVisible))
            End If
        End Set
    End Property

    Private Sub ApplyToolbarLayout()
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddTrt, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict display names (case-insensitive). Combined with flyout search text.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyListFilter()
        End Set
    End Property

    Private Shared Function ValidateTrtColumnIdentifier(raw As String) As String
        If String.IsNullOrWhiteSpace(raw) Then Return "Trt"
        Dim sb As New StringBuilder()
        For Each c In raw.Trim()
            If Char.IsLetterOrDigit(c) OrElse c = "_"c Then sb.Append(c)
        Next
        Dim s = sb.ToString()
        If s.Length = 0 Then Return "Trt"
        If Char.IsDigit(s(0)) Then Return "Trt"
        Return s
    End Function

    Private Function BracketTrtColumn() As String
        Dim id = ValidateTrtColumnIdentifier(_TrtT)
        Return "[" & id.Replace("]", "]]") & "]"
    End Function

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Sub ClearSearchBox()
        If txtSearch Is Nothing Then Return
        _suppressSearchTextChanged = True
        Try
            txtSearch.Text = String.Empty
        Finally
            _suppressSearchTextChanged = False
        End Try
    End Sub

    Private Sub ApplyListFilter()
        If CboTrt Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.Trt, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedTrtID

        Try
            RemoveHandler CboTrt.SelectedIndexChanged, AddressOf CboTrt_SelectedIndexChanged
            CboTrt.Properties.Items.Clear()
            For Each row As Treat In filtered
                CboTrt.Properties.Items.Add(New ComboBoxItem With {.ID = row.TrtID, .Name = row.Trt})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboTrt.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboTrt.SelectedItem = keepItem
                _selectedTrtID = keepItem.ID
                _selectedTrt = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboTrt.Properties.Items.Count > 0 Then
                CboTrt.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboTrt.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedTrtID = first.ID
                    _selectedTrt = first.Name
                End If
            Else
                CboTrt.EditValue = Nothing
                _selectedTrtID = 0
                _selectedTrt = Nothing
            End If
            SetTextEditStyle(CboTrt)
        Finally
            AddHandler CboTrt.SelectedIndexChanged, AddressOf CboTrt_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindTrts()
        _allRows = GetTreats()
        ApplyListFilter()
    End Sub

    Private Sub UpdateTreatComboBoxSelection(trtId As Integer)
        ClearSearchBox()
        If trtId <= 0 Then
            CboTrt.EditValue = Nothing
            _selectedTrt = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.TrtID = trtId)
        If row Is Nothing Then
            CboTrt.EditValue = Nothing
            _selectedTrt = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboTrt.Properties.Items
            If item.ID = trtId Then
                CboTrt.SelectedItem = item
                _selectedTrt = item.Name
                Return
            End If
        Next
        CboTrt.EditValue = Nothing
        _selectedTrt = Nothing
    End Sub

    Private Sub CboTrt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboTrt.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboTrt.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            TrtID = selectedItem.ID
            Trt = selectedItem.Name
            RaiseEvent TreatValueChanged(sender, New TreatIndexChangedEvent(TrtID, Trt))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboTrt)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboTrt, txtSearch, Me)
    End Sub

    Private Sub BtnAddTrt_Click(sender As Object, e As EventArgs) Handles BtnAddTrt.Click
        BindTrts()
    End Sub

    Public Function GetTreatsTable() As DataTable
        Dim rows = GetTreats()
        Dim dt As New DataTable()
        dt.Columns.Add("TrtID", GetType(Integer))
        dt.Columns.Add("Trt", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.TrtID, If(p.Trt, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function GetTreats() As List(Of Treat)
        Dim col = BracketTrtColumn()
        Dim sql As String = "SELECT TrtID, " & col & " AS Trt FROM TblTRTS;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of Treat)(sql).ToList()
        End Using
    End Function

    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New()
        InitializeComponent()
        ApplyToolbarLayout()
        dragTimer.Enabled = False
        dragTimer.Interval = 150
        BindTrts()
    End Sub

    Private Sub HandleTreatValueChanged(ByVal sender As Object, ByVal e As TreatIndexChangedEvent) Handles Me.TreatValueChanged
        Me.TrtID = e.TrtID
        Me.Trt = e.Trt
    End Sub

    Public Sub SetSelectedTreat(id As Integer)
        Me.TrtID = id
    End Sub

#Region "Dragging"

    Private isDragging As Boolean = False
    Private startPoint As Point

    Private Sub CboTrt_MouseDown(sender As Object, e As MouseEventArgs) Handles CboTrt.MouseDown
        startPoint = e.Location
        isDragging = False
        dragTimer.Start()
    End Sub

    Private Sub CboTrt_MouseMove(sender As Object, e As MouseEventArgs) Handles CboTrt.MouseMove
        Const dragThreshold As Integer = 5

        If e.Button = MouseButtons.Left Then
            If Not isDragging AndAlso (Math.Abs(e.X - startPoint.X) > dragThreshold OrElse Math.Abs(e.Y - startPoint.Y) > dragThreshold) Then
                If CboTrt.IsPopupOpen Then
                    CboTrt.ClosePopup()
                End If

                If CboTrt.SelectedItem IsNot Nothing Then
                    isDragging = True
                    CboTrt.DoDragDrop(CboTrt.SelectedItem.ToString(), DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Sub CboTrt_MouseUp(sender As Object, e As MouseEventArgs) Handles CboTrt.MouseUp
        dragTimer.Stop()
        isDragging = False
    End Sub

    Private Sub dragTimer_Tick(sender As Object, e As EventArgs) Handles dragTimer.Tick
        Const dragThreshold As Integer = 5
        If Math.Abs(Cursor.Position.X - startPoint.X) > dragThreshold OrElse Math.Abs(Cursor.Position.Y - startPoint.Y) > dragThreshold Then
            dragTimer.Stop()

            If CboTrt.SelectedItem IsNot Nothing Then
                isDragging = True
                CboTrt.DoDragDrop(CboTrt.SelectedItem.ToString(), DragDropEffects.Copy)
            End If
        End If
    End Sub

#End Region

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class Treat
        Public Property TrtID As Integer
        Public Property Trt As String
    End Class

    Public Class TreatIndexChangedEvent
        Inherits EventArgs
        Public Property TrtID As Integer
        Public Property Trt As String
        Public Sub New(trtId As Integer, trt As String)
            Me.TrtID = trtId
            Me.Trt = trt
        End Sub
    End Class
End Class
