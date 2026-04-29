Imports System.Collections.Generic
Imports DevExpress.XtraEditors

''' <summary>Runtime per-user form open rights (UserFormAccess + Forms). ADMINS group bypasses.</summary>
Public Module FormAccessGate

    Private ReadOnly _allowed As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
    Private _adminBypass As Boolean
    Private _initialized As Boolean

    Public Sub Clear()
        _allowed.Clear()
        _adminBypass = False
        _initialized = False
    End Sub

    Public Sub RefreshAfterLogin()
        Clear()
        If PasswordSecurity.CurrentUser Is Nothing Then Return
        _initialized = True
        If IsCurrentUserFormAccessAdmin() Then
            _adminBypass = True
            Return
        End If
        Dim dal As New UserFormAccessDATA()
        For Each name In dal.GetAllowedFormNamesForUser(PasswordSecurity.CurrentUser.UsID)
            If Not String.IsNullOrWhiteSpace(name) Then _allowed.Add(name.Trim())
        Next
    End Sub

    Public Function IsCurrentUserFormAccessAdmin() As Boolean
        If PasswordSecurity.CurrentUser Is Nothing Then Return False
        Dim g = PasswordSecurity.CurrentGroup
        If g Is Nothing Then
            g = New GroupDATA().SelectRecord(PasswordSecurity.CurrentUser.GroupID)
            PasswordSecurity.CurrentGroup = g
        End If
        If g Is Nothing Then Return False
        Return String.Equals(g.GroupName, "ADMINS", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Function CanOpenForm(formTypeName As String) As Boolean
        If String.IsNullOrWhiteSpace(formTypeName) Then Return True
        If PasswordSecurity.CurrentUser Is Nothing Then Return True
        If Not _initialized Then Return True
        If _adminBypass Then Return True
        Return _allowed.Contains(formTypeName.Trim())
    End Function

    Public Function TryEnterForm(owner As IWin32Window, formTypeName As String) As Boolean
        If CanOpenForm(formTypeName) Then Return True
        XtraMessageBox.Show(
            owner,
            If(Eng, "You are not allowed to open this screen.", "ليس لديك صلاحية فتح هذه الشاشة."),
            If(Eng, "Access denied", "صلاحيات"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)
        Return False
    End Function

End Module
