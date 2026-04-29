Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports Dapper
Imports DevExpress.Charts.Native
Imports DevExpress.DashboardWin.Forms.Export
Imports DevExpress.Xpo.DB.Helpers

Public Class UsersDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Users)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Users)("SELECT * FROM Users")
        End Using
    End Function

    Public Function Select_ByGroupID(ByVal grpID As Integer) As IEnumerable(Of Users)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM Users WHERE GroupID = @GroupID"
            Return conn.Query(Of Users)(sql, New With {.GroupID = grpID})
        End Using
    End Function


    Public Function Select_Record(ByVal clsUsers As Users) As Users
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Users WHERE UsID = @UsID"
            Return conn.QuerySingleOrDefault(Of Users)(sql, New With {.UsID = clsUsers.UsID})
        End Using
    End Function

    Public Function GetUsersCount() As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select Count(*) FROM Users "
            'Return conn.ExecuteScalar(Of Integer)(sql)
            Dim result As Object = conn.ExecuteScalar(sql)

            If result IsNot Nothing Then
                Dim count = Convert.ToInt32(result) ' Set the TrtID from the result
                Return count > 0
            Else
                Return False
            End If
        End Using
    End Function
    Public Function GetUserByUsername(ByVal username As String) As Users
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT UsID, UsName, [UsPassHash],[UsSalt], UsLvl, UsGrp, GroupID FROM Users WHERE UsName = @UsName"
            Return conn.QueryFirstOrDefault(Of Users)(sql, New With {.UsName = username})
        End Using
    End Function

    Public Function GetDoctorByUserID(ByVal UsID As Integer) As Doctors
        Using conn As New SqlConnection(ConnectionString)
            ' First, get the DrID from USERS table
            Dim getDrIdSql As String = "SELECT DrID FROM USERS WHERE UsID = @UsID"
            Dim drId As Integer? = conn.ExecuteScalar(Of Integer)(getDrIdSql, New With {.UsID = UsID})

            ' If DrID was found, get the full doctor record
            If drId.HasValue AndAlso drId.Value > 0 Then
                Dim getDoctorSql As String = "SELECT * FROM Doctors WHERE DrID = @DrID"
                Return conn.QueryFirstOrDefault(Of Doctors)(getDoctorSql, New With {.DrID = drId.Value})
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Function GetSecretaryByUserID(ByVal UsID As Integer) As Secretaries
        Using conn As New SqlConnection(ConnectionString)
            ' First, get the DrID from USERS table
            Dim getSecIdSql As String = "SELECT SecID FROM USERS WHERE UsID = @UsID"
            Dim SecID As Integer? = conn.ExecuteScalar(Of Integer)(getSecIdSql, New With {.UsID = UsID})

            ' If DrID was found, get the full doctor record
            If SecID.HasValue AndAlso SecID.Value > 0 Then
                Dim getSecretariesSql As String = "SELECT * FROM Secretaries WHERE SecID = @SecID"
                Return conn.QueryFirstOrDefault(Of Secretaries)(getSecretariesSql, New With {.SecID = SecID.Value})
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Function GetGroupByUserGrpID(ByVal grpID As String) As Group
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT GroupID, GroupName  FROM Groups WHERE GroupID = @GroupID"
            Return conn.QueryFirstOrDefault(Of Group)(sql, New With {.GroupID = grpID})
        End Using
    End Function

    Public Function UpdatePassword(ByVal user As Users) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "UPDATE Users SET UsPassHash = @UsPassHash, UsSalt = @UsSalt WHERE UsID = @UsID"
            Dim rowsAffected As Integer = conn.Execute(sql, New With {
            .UsPassHash = user.UsPassHash,
            .UsSalt = user.UsSalt,
            .UsID = user.UsID
        })
            Return rowsAffected > 0
        End Using
    End Function

    Public Function Add(ByVal clsUSERS As USERS) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO USERS (UsName, UsPassHash, UsSalt, GroupID, UsLvl, UsGrp, DrID, SecID,EmpID) VALUES (@UsName, @UsPassHash, @UsSalt, @GroupID, @UsLvl, @UsGrp, @DrID, @SecID, @EmpID)"
            RowsAffected = conn.Execute(sql, New With {.UsName = clsUSERS.UsName, .UsPassHash = clsUSERS.UsPassHash, .UsSalt = clsUSERS.UsSalt, .GroupID = clsUSERS.GroupID, .UsLvl = clsUSERS.UsLvl, .UsGrp = clsUSERS.UsGrp, .DrID = clsUSERS.DrID, .SecID = clsUSERS.SecID, .EmpID = clsUSERS.EmpID})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldUSERS As USERS, newUSERS As USERS) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
            .NewUsName = newUSERS.UsName,
            .NewGroupID = newUSERS.GroupID,
            .NewUsLvl = newUSERS.UsLvl,
            .NewUsGrp = newUSERS.UsGrp,
            .NewDrID = If(newUSERS.DrID, DBNull.Value),
            .NewSecID = If(newUSERS.SecID, DBNull.Value),
            .NewEmpID = If(newUSERS.EmpID, DBNull.Value),
            .OldUsName = oldUSERS.UsName,
            .OldGroupID = oldUSERS.GroupID,
            .OldUsLvl = oldUSERS.UsLvl,
            .OldUsGrp = oldUSERS.UsGrp,
            .OldDrID = If(oldUSERS.DrID, DBNull.Value),
            .OldSecID = If(oldUSERS.SecID, DBNull.Value),
            .OldEmpID = If(oldUSERS.EmpID, DBNull.Value)
        }
            '.NewUsSalt = newUSERS.UsSalt,
            '.NewUsPassHash = newUSERS.UsPassHash,
            '.OldUsPassHash = oldUSERS.UsPassHash,
            '.OldUsSalt = oldUSERS.UsSalt,
            Dim query As String = "UPDATE [USERS] SET 
                              [UsName] = @NewUsName, 
                              [GroupID] = @NewGroupID, 
                              [UsLvl] = @NewUsLvl, 
                              [UsGrp] = @NewUsGrp, 
                              [DrID] = @NewDrID, 
                              [SecID] = @NewSecID,
                              [EmpID] = @NewEmpID
                              WHERE [UsName] = @OldUsName 
                              AND [GroupID] = @OldGroupID 
                              AND [UsLvl] = @OldUsLvl 
                              AND [UsGrp] = @OldUsGrp 
                              AND (([DrID] = @OldDrID) OR ([DrID] IS NULL AND @OldDrID IS NULL))
                              AND (([SecID] = @OldSecID) OR ([SecID] IS NULL AND @OldSecID IS NULL))
                              AND (([EmpID] = @OldEmpID) OR ([EmpID] IS NULL AND @OldEmpID IS NULL))"
            '[UsPassHash] = @NewUsPassHash, 
            '[UsSalt] = @NewUsSalt, 
            'AND [UsPassHash] = @OldUsPassHash 
            'AND [UsSalt] = @OldUsSalt 

            Dim affectedRows As Integer = conn.Execute(query, parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Update1(oldUSERS As USERS, newUSERS As USERS) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewUsName = newUSERS.UsName, .OldUsName = oldUSERS.UsName, .NewUsPassHash = newUSERS.UsPassHash, .OldUsPassHash = oldUSERS.UsPassHash, .NewUsSalt = newUSERS.UsSalt, .OldUsSalt = oldUSERS.UsSalt, .NewGroupID = newUSERS.GroupID, .OldGroupID = oldUSERS.GroupID, .NewUsLvl = newUSERS.UsLvl, .OldUsLvl = oldUSERS.UsLvl, .NewUsGrp = newUSERS.UsGrp, .OldUsGrp = oldUSERS.UsGrp, .NewDrID = newUSERS.DrID, .OldDrID = oldUSERS.DrID, .NewSecID = newUSERS.SecID, .OldSecID = oldUSERS.SecID
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [USERS] SET [UsName] = @NewUsName, [UsPassHash] = @NewUsPassHash, [UsSalt] = @NewUsSalt, [GroupID] = @NewGroupID, [UsLvl] = @NewUsLvl, [UsGrp] = @NewUsGrp, [DrID] = @NewDrID, [SecID] = @NewSecID WHERE [UsName] = @OldUsName AND [UsPassHash] = @OldUsPassHash AND [UsSalt] = @OldUsSalt AND [GroupID] = @OldGroupID AND [UsLvl] = @OldUsLvl AND [UsGrp] = @OldUsGrp AND [DrID] = @OldDrID AND [SecID] = @OldSecID", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function AddDoctorUser(ByVal doctorUser As USERS) As Boolean
        If doctorUser.DrID Is Nothing Then
            MsgBox("DoctorID must be provided for Doctor users.")
        End If
        doctorUser.GroupID = 2
        doctorUser.UsGrp = "Doctors"
        Return Add(doctorUser)
    End Function

    Public Function AddSecretaryUser(ByVal secretaryUser As USERS) As Boolean
        If secretaryUser.SecID Is Nothing Then
            MsgBox("SecID must be provided for Secretary users.")
        End If
        secretaryUser.GroupID = 3
        secretaryUser.UsGrp = "Secretaries"
        Return Add(secretaryUser)
    End Function


    Public Function Delete(ByVal clsUsers As Users) As Boolean
        Dim deletedUser = Conn.QueryFirstOrDefault(Of Users)("SELECT * FROM Users WHERE UsID = @UsID", New With {.UsID = clsUsers.UsID})

        Dim deleteStatement As String =
            "DELETE FROM [Users] 
			WHERE UsID = @UsID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.UsID = clsUsers.UsID})
            If affectedRows > 0 Then
                ' Log this action into the Audit Log
                LogAudit("Delete", "Users", deletedUser.UsID, $"{deletedUser.UsName}, {deletedUser.UsPassHash}, {deletedUser.UsLvl}, {deletedUser.UsGrp}", Nothing, LoggedInUserName) ' Replace "CurrentUser" with the actual user
            End If
            Return affectedRows > 0
        End Using
    End Function

    'Methods to get parents and childs
    Public Function GetGroup(ByVal GroupID As Integer) As Groups
        Dim parent As Groups = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Groups] WHERE [GroupID] = @GroupID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Groups)(query, New With {.GroupID = GroupID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

    Public Function GetDoctor(ByVal DrID As Integer) As Doctors
        Dim parent As Doctors = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Doctors] WHERE [DrID] = @DrID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Doctors)(query, New With {.DrID = DrID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

    Public Function GetSecretary(ByVal SecID As Integer) As Secretaries
        Dim parent As Secretaries = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Secretaries] WHERE [SecID] = @SecID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Secretaries)(query, New With {.SecID = SecID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function
End Class
