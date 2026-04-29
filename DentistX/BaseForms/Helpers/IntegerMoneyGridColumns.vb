Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Mask
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

''' <summary>
''' In-cell TextEdit repos for integer (no decimal) and money (2 decimals), plus reliable focus UX when editing.
''' </summary>
Public Module IntegerMoneyGridColumns

    Private Const IntegerPayTrtRepoName As String = "repoIntegerPayTrtValue"
    Private Const Decimal2RepoName As String = "repoDecimal2Money"

    Private Const DigitsOptionalMinusMask As String = "-?\d*"
    ''' <summary>Digits and optional dot + up to 2 fraction digits (invariant dot in mask; parse uses culture on commit).</summary>
    Private Const Decimal2RegExMask As String = "\d*(\.\d{0,2})?"

    Private ReadOnly _registrations As New ConditionalWeakTable(Of GridView, GridMoneyRegistration)()
    Private ReadOnly _shownEditorAttached As New HashSet(Of GridView)()

    Public Sub ApplyIntegerPayTrtEditors(grid As GridControl)
        If grid Is Nothing Then Return
        For Each bv As Views.Base.BaseView In grid.ViewCollection
            Dim gv = TryCast(bv, GridView)
            If gv Is Nothing Then Continue For
            ApplyIntegerMoneyGridEditors(gv, "PayValue", "TrtValue", "Discount", "Discount2")
        Next
    End Sub

    Public Sub ApplyIntegerPayTrtEditors(view As GridView)
        ApplyIntegerMoneyGridEditors(view, "PayValue", "TrtValue", "Discount", "Discount2")
    End Sub

    ''' <summary>Whole numbers: digits and optional leading minus. Display/edit f0.</summary>
    Public Sub ApplyIntegerMoneyGridEditors(view As GridView, ParamArray fieldNames As String())
        If view Is Nothing OrElse view.GridControl Is Nothing Then Return
        Dim grid = view.GridControl
        Dim repo = GetOrCreateIntegerRepo(grid)
        Dim reg = GetOrCreateRegistration(view)
        Dim any As Boolean = False
        For Each fn In fieldNames
            If String.IsNullOrWhiteSpace(fn) Then Continue For
            reg.IntegerFields.Add(fn.Trim())
            any = True
            Dim col = FindColumn(view, fn.Trim())
            If col Is Nothing Then Continue For
            col.ColumnEdit = repo
            col.DisplayFormat.FormatType = FormatType.Numeric
            col.DisplayFormat.FormatString = "f0"
        Next
        If any Then EnsureShownEditor(view)
    End Sub

    ''' <summary>Money with up to 2 decimal places. Display/edit f2.</summary>
    Public Sub ApplyDecimal2MoneyGridEditors(view As GridView, ParamArray fieldNames As String())
        If view Is Nothing OrElse view.GridControl Is Nothing Then Return
        Dim grid = view.GridControl
        Dim repo = GetOrCreateDecimal2Repo(grid)
        Dim reg = GetOrCreateRegistration(view)
        Dim any As Boolean = False
        For Each fn In fieldNames
            If String.IsNullOrWhiteSpace(fn) Then Continue For
            reg.Decimal2Fields.Add(fn.Trim())
            any = True
            Dim col = FindColumn(view, fn.Trim())
            If col Is Nothing Then Continue For
            col.ColumnEdit = repo
            col.DisplayFormat.FormatType = FormatType.Numeric
            col.DisplayFormat.FormatString = "f2"
        Next
        If any Then EnsureShownEditor(view)
    End Sub

    Private Function FindColumn(view As GridView, fieldName As String) As GridColumn
        For Each c As GridColumn In view.Columns
            If c Is Nothing Then Continue For
            If String.Equals(c.FieldName, fieldName, StringComparison.OrdinalIgnoreCase) Then Return c
        Next
        Return Nothing
    End Function

    Private Function GetOrCreateRegistration(view As GridView) As GridMoneyRegistration
        Dim reg As GridMoneyRegistration = Nothing
        If _registrations.TryGetValue(view, reg) Then Return reg
        reg = New GridMoneyRegistration()
        _registrations.Add(view, reg)
        Return reg
    End Function

    Private Sub EnsureShownEditor(view As GridView)
        If _shownEditorAttached.Contains(view) Then Return
        _shownEditorAttached.Add(view)
        AddHandler view.ShownEditor, AddressOf UnifiedMoneyGrid_ShownEditor
    End Sub

    Private Function GetOrCreateIntegerRepo(grid As GridControl) As RepositoryItemTextEdit
        For i As Integer = 0 To grid.RepositoryItems.Count - 1
            Dim ri = grid.RepositoryItems(i)
            If ri IsNot Nothing AndAlso ri.Name = IntegerPayTrtRepoName Then
                Dim existing = TryCast(ri, RepositoryItemTextEdit)
                If existing IsNot Nothing Then Return existing
            End If
        Next
        Dim r = CreateIntegerMoneyTextEdit()
        grid.RepositoryItems.Add(r)
        Return r
    End Function

    Private Function GetOrCreateDecimal2Repo(grid As GridControl) As RepositoryItemTextEdit
        For i As Integer = 0 To grid.RepositoryItems.Count - 1
            Dim ri = grid.RepositoryItems(i)
            If ri IsNot Nothing AndAlso ri.Name = Decimal2RepoName Then
                Dim existing = TryCast(ri, RepositoryItemTextEdit)
                If existing IsNot Nothing Then Return existing
            End If
        Next
        Dim r = CreateDecimal2MoneyTextEdit()
        grid.RepositoryItems.Add(r)
        Return r
    End Function

    Private Function CreateIntegerMoneyTextEdit() As RepositoryItemTextEdit
        Dim r As New RepositoryItemTextEdit()
        r.Name = IntegerPayTrtRepoName
        r.Mask.MaskType = MaskType.RegEx
        r.Mask.EditMask = DigitsOptionalMinusMask
        r.Mask.UseMaskAsDisplayFormat = False
        r.Mask.ShowPlaceHolders = False
        r.DisplayFormat.FormatType = FormatType.Numeric
        r.DisplayFormat.FormatString = "f0"
        r.EditFormat.FormatType = FormatType.Numeric
        r.EditFormat.FormatString = "f0"
        Return r
    End Function

    Private Function CreateDecimal2MoneyTextEdit() As RepositoryItemTextEdit
        Dim r As New RepositoryItemTextEdit()
        r.Name = Decimal2RepoName
        r.Mask.MaskType = MaskType.RegEx
        r.Mask.EditMask = Decimal2RegExMask
        r.Mask.UseMaskAsDisplayFormat = False
        r.Mask.ShowPlaceHolders = False
        r.DisplayFormat.FormatType = FormatType.Numeric
        r.DisplayFormat.FormatString = "f2"
        r.EditFormat.FormatType = FormatType.Numeric
        r.EditFormat.FormatString = "f2"
        Return r
    End Function

    Private Sub UnifiedMoneyGrid_ShownEditor(sender As Object, e As EventArgs)
        Dim view = TryCast(sender, GridView)
        If view Is Nothing Then Return
        Dim col = view.FocusedColumn
        If col Is Nothing Then Return
        Dim fn = col.FieldName
        If String.IsNullOrEmpty(fn) Then Return

        Dim reg As GridMoneyRegistration = Nothing
        _registrations.TryGetValue(view, reg)

        Dim isInt = False
        Dim isDec2 = False
        If reg IsNot Nothing Then
            isInt = reg.IntegerFields.Contains(fn)
            isDec2 = reg.Decimal2Fields.Contains(fn)
        End If
        If Not isInt AndAlso Not isDec2 Then
            If String.Equals(fn, "PayValue", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(fn, "TrtValue", StringComparison.OrdinalIgnoreCase) Then
                isInt = True
            End If
        End If
        If Not isInt AndAlso Not isDec2 Then Return

        Dim ed = TryCast(view.ActiveEditor, TextEdit)
        If ed Is Nothing Then Return

        Dim raw = view.GetFocusedRowCellValue(col)
        Dim d As Decimal = 0D
        If raw IsNot Nothing AndAlso Not IsDBNull(raw) Then
            Try
                d = Convert.ToDecimal(raw, CultureInfo.CurrentCulture)
            Catch
                d = 0D
            End Try
        End If

        RunDeferredGridActiveEditorNumericFocus(view, d)
    End Sub

    ''' <summary>Timer-based: zero → empty; else SelectAll repeated so DevExpress refresh does not drop selection.</summary>
    Friend Sub RunDeferredGridActiveEditorNumericFocus(view As GridView, cellValue As Decimal)
        If view Is Nothing Then Return
        Dim t As New Timer With {.Interval = 35}
        Dim pass As Integer = 0
        Dim h As EventHandler = Nothing
        h = Sub()
                pass += 1
                Dim te = TryCast(view.ActiveEditor, TextEdit)
                If cellValue = 0D Then
                    If te IsNot Nothing Then
                        te.Text = ""
                        te.SelectionStart = 0
                        te.SelectionLength = 0
                    End If
                    RemoveHandler t.Tick, h
                    t.Stop()
                    t.Dispose()
                    Return
                End If
                If te IsNot Nothing Then
                    te.SelectAll()
                End If
                If pass >= 7 Then
                    RemoveHandler t.Tick, h
                    t.Stop()
                    t.Dispose()
                End If
            End Sub
        AddHandler t.Tick, h
        t.Start()
    End Sub

End Module

Friend NotInheritable Class GridMoneyRegistration
    Friend ReadOnly IntegerFields As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
    Friend ReadOnly Decimal2Fields As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
End Class
