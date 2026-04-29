Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class TblTrtClrDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of TblTrtClr)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of TblTrtClr)("SELECT * FROM TblTrtClr")
        End Using
    End Function


    Public Function Select_Record(ByVal clsTblTrtClr As TblTrtClr) As TblTrtClr
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM TblTrtClr WHERE TrtClrID = @TrtClrID"
            Return conn.QuerySingleOrDefault(Of TblTrtClr)(sql, New With {.TrtClrID = clsTblTrtClr.TrtClrID})
        End Using
    End Function
    'GetTrtClrIDByTrtID
    Public Function Select_RecordTRTID(ByVal clsTblTrtClr As TblTrtClr) As TblTrtClr
        Using conn As New SqlConnection(ConnectionString)
            Dim sql1 As String = "SELECT  dbo.TblTrtClr.TrtClrID, dbo.TblTrtClr.FillColor, dbo.TblTrtClr.BorderColor, dbo.TblTrtClr.BorderThick,
		                                  dbo.TblTrtClr.FillColorDef, dbo.TblTRTS.TrtID
                                  FROM    dbo.TblTrtClr INNER JOIN
                                         dbo.TblTRTS ON dbo.TblTrtClr.TrtID = dbo.TblTRTS.TrtID
                                 WHERE dbo.TblTrtClr.TrtID = @TrtID"


            Dim sql As String = "Select * FROM TblTrtClr WHERE TrtID = @TrtID"
            Return conn.QuerySingleOrDefault(Of TblTrtClr)(sql, New With {.TrtID = clsTblTrtClr.TrtID})
        End Using
    End Function

    Public Function GetTrtClrIDByTrtID(trtID As Integer) As Integer
        Dim query = "SELECT [TrtClrID] FROM [TblTrtClr] WHERE TrtID = @TrtID "
        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QuerySingleOrDefault(Of Integer?)(query, New With {.TrtID = trtID}), 0)
        End Using

    End Function
    Public Function Add(ByVal clsTblTrtClr As TblTrtClr) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO TblTrtClr (TrtID, FillColor, BorderColor, BorderThick, FillColorDef) VALUES (@TrtID, @FillColor, @BorderColor, @BorderThick, @FillColorDef)"
            RowsAffected = conn.Execute(sql, New With {.TrtID = clsTblTrtClr.TrtID, .FillColor = clsTblTrtClr.FillColor, .BorderColor = clsTblTrtClr.BorderColor, .BorderThick = clsTblTrtClr.BorderThick, .FillColorDef = clsTblTrtClr.FillColorDef})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldTblTrtClr As TblTrtClr, newTblTrtClr As TblTrtClr) As Boolean

        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
            .NewTrtID = newTblTrtClr.TrtID, .OldTrtID = oldTblTrtClr.TrtID,
            .NewFillColor = newTblTrtClr.FillColor, .OldFillColor = oldTblTrtClr.FillColor,
            .NewBorderColor = newTblTrtClr.BorderColor, .OldBorderColor = oldTblTrtClr.BorderColor,
            .NewBorderThick = newTblTrtClr.BorderThick, .OldBorderThick = oldTblTrtClr.BorderThick,
            .NewFillColorDef = newTblTrtClr.FillColorDef, .OldFillColorDef = oldTblTrtClr.FillColorDef,
            .TrtClrID = oldTblTrtClr.TrtClrID ' This line was missing
        }

            Dim sql As String = "
            UPDATE [TblTrtClr] SET 
                [TrtID] = @NewTrtID,
                [FillColor] = @NewFillColor,
                [BorderColor] = @NewBorderColor,
                [BorderThick] = @NewBorderThick,
                [FillColorDef] =@NewFillColorDef
            WHERE TrtClrID = @TrtClrID
        "

            Dim affectedRows As Integer = conn.Execute(sql, parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsTblTrtClr As TblTrtClr) As Boolean
        Dim deleteStatement As String =
                        "DELETE FROM [TblTrtClr] 
			            WHERE TrtClrID = @TrtClrID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.TrtClrID = clsTblTrtClr.TrtClrID})
            Return affectedRows > 0
        End Using
        'Query to sync colors from TblTRTS into TblTrtClr
        Dim sql As String = "
MERGE INTO [dbo].[TblTrtClr] AS target
USING (
    SELECT 
        [TrtID],
        [TrtColor]
    FROM 
        [dbo].[TblTRTS]
) AS source
ON (target.[TrtID] = source.[TrtID])
WHEN MATCHED THEN
    UPDATE SET 
        target.[FillColor] = source.[TrtColor],
        target.[FillColorDef] = source.[TrtColor],
        target.[BorderColor] = '#00000000',
        target.[BorderThick] = 0
WHEN NOT MATCHED THEN
    INSERT (
        [TrtID],
        [FillColor],
        [BorderColor],
        [BorderThick],
        [FillColorDef]
    )
    VALUES (
        source.[TrtID],
        source.[TrtColor],
        '#00000000',
        0,
        source.[TrtColor]
    );"
    End Function


    'Methods to get parents and childs
End Class
