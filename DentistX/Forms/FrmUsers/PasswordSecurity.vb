Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows
Imports Dapper


Public Module PasswordSecurity
    Public Property CurrentUser As Users = Nothing
    Public Property CurrentDoctor As Doctors = Nothing ' Add current doctor
    Public Property CurrentSecretary As Secretaries = Nothing
    Public Property CurrentEmp As Emp = Nothing
    Public Property CurrentPatient As Patient = Nothing
    Public Property CurrentGroup As Group = Nothing


    Public Perms As PermissionService

    Public LoggedInUserID As Integer = -1
    Public LoggedInUserName As String
    Public LoggedInDoctorID As Integer = -1 ' Add doctor ID
    Public LoggedInSecID As Integer = -1
    Public LoggedInEmpID As Integer = -1
    '================================
    'Public CurrentPatient As Patient
    Public PatientID As Integer = 0, UsrID As Integer = 0, RxID As Integer = 0, TrtID As Integer = 0
    Public PatientName As String = ""
    'Public NewPatientName As String = ""

    '======================================

    ' Generate random salt
    Public Function GenerateSalt(Optional size As Integer = 16) As Byte()
        Dim salt(size - 1) As Byte
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(salt)
        End Using
        Return salt
    End Function

    ' Generate password hash and salt
    Public Sub GeneratePasswordHash(password As String, ByRef salt As Byte(), ByRef hash As Byte())
        salt = GenerateSalt()

        Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256)
            hash = pbkdf2.GetBytes(32) ' 256-bit hash
        End Using
    End Sub

    ' Hash password using known salt
    Public Function HashPassword(password As String, salt As Byte()) As Byte()
        Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256)
            Return pbkdf2.GetBytes(32) ' 256-bit hash
        End Using
    End Function

    ' Verify entered password against stored hash and salt
    Public Function VerifyPassword(password As String, salt As Byte(), storedHash As Byte()) As Boolean
        Dim computedHash As Byte() = HashPassword(password, salt)
        Return computedHash.SequenceEqual(storedHash)
    End Function

    ' Get Permissions
    Public Function GetPermission() As Dictionary(Of String, Boolean)
        Dim sql As String = "
        SELECT p.PermName,
               ISNULL(up.IsAllowed, gp.IsAllowed) AS IsAllowed
        FROM Permissions p
        LEFT JOIN GroupPermissions gp ON gp.PermID = p.PermID AND gp.GroupID = @GroupID
        LEFT JOIN UserPermissions up ON up.PermID = p.PermID AND up.UsID = @UsID
    "

        Dim permissionsDict = Conn.Query(Of PermissionResult)(sql,
        New With {.GroupID = CurrentUser.GroupID, .UsID = CurrentUser.UsID}).ToDictionary(Function(x) x.PermName, Function(x) x.IsAllowed)

        Return permissionsDict
    End Function
    'Then you can use it like:

    'Dim perms = GetPermission()
    'If perms.ContainsKey("ManagePatients") AndAlso perms("ManagePatients") Then
    '    btnAdd.Enabled = True
    'End If


    '=====================================
    ' Log audit actions
    Public Sub LogAudit(actionType As String, tableName As String, recordID As Integer, oldValue As String, newValue As String, user As String)
        Dim sql As String = "
            INSERT INTO AuditLog (ActionType, TableName, RecordID, OldValue, NewValue, ChangedBy, ChangeDate)
            VALUES (@ActionType, @TableName, @RecordID, @OldValue, @NewValue, @ChangedBy, GETDATE())"

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Execute(sql, New With {
                .ActionType = actionType,
                .TableName = tableName,
                .RecordID = recordID,
                .OldValue = oldValue,
                .NewValue = newValue,
                .ChangedBy = user
            })
        End Using
    End Sub

End Module



