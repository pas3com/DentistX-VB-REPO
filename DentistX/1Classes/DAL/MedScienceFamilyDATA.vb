Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class MedScienceFamilyDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString
    Public Function SelectAll() As IEnumerable(Of MedScienceFamily)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of MedScienceFamily)("SELECT * FROM MedScienceFamily")
        End Using
    End Function
    Public Function Select_Record(ByVal clsMedScienceFamily As MedScienceFamily) As MedScienceFamily
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM MedScienceFamily WHERE ScincID = @ScincID"
            Return conn.QuerySingleOrDefault(Of MedScienceFamily)(sql, New With {.ScincID = clsMedScienceFamily.ScincID})
        End Using
    End Function

    Public Function SelectByScincID(ScincID As Integer) As IEnumerable(Of MedScienceFamily)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of MedScienceFamily)("SELECT * FROM MedScienceFamily WHERE ScincID = @ScincID", New With {.ScincID = ScincID})
        End Using
    End Function

    Public Function Add(ByVal clsMedScienceFamily As MedScienceFamily) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO MedScienceFamily (SubCatID, ScienceName) VALUES (@SubCatID, @ScienceName)"
            RowsAffected = conn.Execute(sql, New With {.SubCatID = clsMedScienceFamily.SubCatID, .ScienceName = clsMedScienceFamily.ScienceName})
            Return RowsAffected > 0
        End Using
    End Function
    Public Function Update(oldMedScienceFamily As MedScienceFamily, newMedScienceFamily As MedScienceFamily) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewSubCatID = newMedScienceFamily.SubCatID, .OldSubCatID = oldMedScienceFamily.SubCatID, .NewScienceName = newMedScienceFamily.ScienceName, .OldScienceName = oldMedScienceFamily.ScienceName
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [MedScienceFamily] SET [SubCatID] = @NewSubCatID, [ScienceName] = @NewScienceName WHERE [SubCatID] = @OldSubCatID AND [ScienceName] = @OldScienceName", parameters)
            Return affectedRows > 0
        End Using
    End Function
    Public Function Delete(ByVal clsMedScienceFamily As MedScienceFamily) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [MedScienceFamily] 
			WHERE ScincID = @ScincID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.ScincID = clsMedScienceFamily.ScincID})
            Return affectedRows > 0
        End Using
    End Function
    'Methods to get parents and childs
    Public Function GetMedicineFamily(ByVal SubCatID As Integer) As MedicineFamily
        Dim parent As MedicineFamily = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [MedicineFamily] WHERE [SubCatID] = @SubCatID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of MedicineFamily)(query, New With {.SubCatID = SubCatID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function
    Public Function GetMedicineItems(ByVal clsMedScienceFamily As MedScienceFamily) As IEnumerable(Of MedicineItems)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [MedicineItems] WHERE [ScincID] = @ScincID"
            Return conn.Query(Of MedicineItems)(query, New With {.ScincID = clsMedScienceFamily.ScincID})
        End Using
    End Function
End Class
