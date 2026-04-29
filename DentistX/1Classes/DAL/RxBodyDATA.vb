Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class RxBodyDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of RxBody)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of RxBody)("SELECT * FROM RxBody")
        End Using
    End Function


    Public Function Select_Record(ByVal clsRxBody As RxBody) As RxBody
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM RxBody WHERE RxBdyID = @RxBdyID"
            Return conn.QuerySingleOrDefault(Of RxBody)(sql, New With {.RxBdyID = clsRxBody.RxBdyID})
        End Using
    End Function

    Public Function Add(ByVal clsRxBody As RxBody) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO RxBody (RxBdyID, ArHdrName, ArHdrAdres, EnHdrName, EnHdrAdres, Logo, Detail, ArFtr, EnFtr, WtrImg, WtrText, UseWtrImg, UseWtrText, DrName) VALUES (@RxBdyID, @ArHdrName, @ArHdrAdres, @EnHdrName, @EnHdrAdres, @Logo, @Detail, @ArFtr, @EnFtr, @WtrImg, @WtrText, @UseWtrImg, @UseWtrText, @DrName)"
            RowsAffected = conn.Execute(sql, New With {.RxBdyID = clsRxBody.RxBdyID, .ArHdrName = clsRxBody.ArHdrName, .ArHdrAdres = clsRxBody.ArHdrAdres, .EnHdrName = clsRxBody.EnHdrName, .EnHdrAdres = clsRxBody.EnHdrAdres, .Logo = clsRxBody.Logo, .Detail = clsRxBody.Detail, .ArFtr = clsRxBody.ArFtr, .EnFtr = clsRxBody.EnFtr, .WtrImg = clsRxBody.WtrImg, .WtrText = clsRxBody.WtrText, .UseWtrImg = clsRxBody.UseWtrImg, .UseWtrText = clsRxBody.UseWtrText, .DrName = clsRxBody.DrName})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldRxBody As RxBody, newRxBody As RxBody) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                        .NewRxBdyID = newRxBody.RxBdyID,
                        .NewArHdrName = newRxBody.ArHdrName,
                        .NewArHdrAdres = newRxBody.ArHdrAdres,
                        .NewEnHdrName = newRxBody.EnHdrName,
                        .NewEnHdrAdres = newRxBody.EnHdrAdres,
                        .NewLogo = newRxBody.Logo,
                        .NewDetail = newRxBody.Detail,
                        .NewArFtr = newRxBody.ArFtr,
                        .NewEnFtr = newRxBody.EnFtr,
                        .NewWtrImg = newRxBody.WtrImg,
                        .NewWtrText = newRxBody.WtrText,
                        .NewUseWtrImg = newRxBody.UseWtrImg,
                        .NewUseWtrText = newRxBody.UseWtrText,
                        .NewDrName = newRxBody.DrName,
                        .RxBdyID = oldRxBody.RxBdyID ' Use only the primary key or unique identifier
                    }

            Dim sql As String = "
                        UPDATE [RxBody]
                        SET [RxBdyID] = @NewRxBdyID,
                            [ArHdrName] = @NewArHdrName,
                            [ArHdrAdres] = @NewArHdrAdres,
                            [EnHdrName] = @NewEnHdrName,
                            [EnHdrAdres] = @NewEnHdrAdres,
                            [Logo] = @NewLogo,
                            [Detail] = @NewDetail,
                            [ArFtr] = @NewArFtr,
                            [EnFtr] = @NewEnFtr,
                            [WtrImg] = @NewWtrImg,
                            [WtrText] = @NewWtrText,
                            [UseWtrImg] = @NewUseWtrImg,
                            [UseWtrText] = @NewUseWtrText,
                            [DrName] = @NewDrName
                        WHERE [RxBdyID] = @RxBdyID
                    "

            Dim affectedRows As Integer = conn.Execute(sql, parameters)
            Return affectedRows > 0
        End Using
    End Function


    Public Function Update1(oldRxBody As RxBody, newRxBody As RxBody) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                                .NewRxBdyID = newRxBody.RxBdyID, .OldRxBdyID = oldRxBody.RxBdyID, .NewArHdrName = newRxBody.ArHdrName, .OldArHdrName = oldRxBody.ArHdrName, .NewArHdrAdres = newRxBody.ArHdrAdres, .OldArHdrAdres = oldRxBody.ArHdrAdres, .NewEnHdrName = newRxBody.EnHdrName, .OldEnHdrName = oldRxBody.EnHdrName, .NewEnHdrAdres = newRxBody.EnHdrAdres, .OldEnHdrAdres = oldRxBody.EnHdrAdres, .NewLogo = newRxBody.Logo, .OldLogo = oldRxBody.Logo, .NewDetail = newRxBody.Detail, .OldDetail = oldRxBody.Detail, .NewArFtr = newRxBody.ArFtr, .OldArFtr = oldRxBody.ArFtr, .NewEnFtr = newRxBody.EnFtr, .OldEnFtr = oldRxBody.EnFtr, .NewWtrImg = newRxBody.WtrImg, .OldWtrImg = oldRxBody.WtrImg, .NewWtrText = newRxBody.WtrText, .OldWtrText = oldRxBody.WtrText, .NewUseWtrImg = newRxBody.UseWtrImg, .OldUseWtrImg = oldRxBody.UseWtrImg, .NewUseWtrText = newRxBody.UseWtrText, .OldUseWtrText = oldRxBody.UseWtrText, .NewDrName = newRxBody.DrName, .OldDrName = oldRxBody.DrName
                                                      }
            Dim affectedRows As Integer = conn.Execute("UPDATE [RxBody] SET [RxBdyID] = @NewRxBdyID, [ArHdrName] = @NewArHdrName, [ArHdrAdres] = @NewArHdrAdres, [EnHdrName] = @NewEnHdrName, [EnHdrAdres] = @NewEnHdrAdres, [Logo] = @NewLogo, [Detail] = @NewDetail, [ArFtr] = @NewArFtr, [EnFtr] = @NewEnFtr, [WtrImg] = @NewWtrImg, [WtrText] = @NewWtrText, [UseWtrImg] = @NewUseWtrImg, [UseWtrText] = @NewUseWtrText, [DrName] = @NewDrName WHERE [RxBdyID] = @OldRxBdyID AND [ArHdrName] = @OldArHdrName AND [ArHdrAdres] = @OldArHdrAdres AND [EnHdrName] = @OldEnHdrName AND [EnHdrAdres] = @OldEnHdrAdres AND [Logo] = @OldLogo AND [Detail] = @OldDetail AND [ArFtr] = @OldArFtr AND [EnFtr] = @OldEnFtr AND [WtrImg] = @OldWtrImg AND [WtrText] = @OldWtrText AND [UseWtrImg] = @OldUseWtrImg AND [UseWtrText] = @OldUseWtrText AND [DrName] = @OldDrName", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsRxBody As RxBody) As Boolean
        Dim deleteStatement As String =
                        "DELETE FROM [RxBody] 
			            WHERE RxBdyID = @RxBdyID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.RxBdyID = clsRxBody.RxBdyID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class
