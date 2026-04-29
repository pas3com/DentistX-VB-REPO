Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class TblTRTSDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString


    Public Shared Function SelectAll() As List(Of TblTRTS)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            Return conn.Query(Of TblTRTS)("SELECT * FROM TblTRTS")
        End Using
    End Function
    'Public Function SelectAll() As IEnumerable(Of TblTRTS)
    '    Using conn As New SqlConnection(ConnectionString)
    '        conn.Open()
    '        Return conn.Query(Of TblTRTS)("SELECT * FROM TblTRTS")
    '    End Using
    'End Function
    Public Function SelectAll2() As IEnumerable(Of TblTRTS)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of TblTRTS)("SELECT TrtID, Trt, TrtValue FROM TblTRTS")
        End Using
    End Function

    Public Function Select_Record(ByVal clsTblTRTS As TblTRTS) As TblTRTS
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM TblTRTS WHERE TrtID = @TrtID"
            Return conn.QuerySingleOrDefault(Of TblTRTS)(sql, New With {.TrtID = clsTblTRTS.TrtID})
        End Using
    End Function

    'SelectByTrtName — duplicate Trt rows exist (e.g. Other Treats); QuerySingleOrDefault throws Sequence contains more than one element
    Public Shared Function SelectByTrtName(ByVal treat As String) As TblTRTS
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql As String = "SELECT TOP 1 * FROM TblTRTS WHERE Trt = @Trt ORDER BY TrtID"
            Return conn.QueryFirstOrDefault(Of TblTRTS)(sql, New With {.Trt = treat})
        End Using
    End Function

    Public Function Select_TrtValue(ByVal clsTblTRTS As TblTRTS) As Decimal

        Dim sql As String = "Select TrtValue FROM TblTRTS WHERE TrtID = @TrtID"

        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QuerySingleOrDefault(Of Decimal?)(sql, New With {.TrtID = clsTblTRTS.TrtID}), 0)
        End Using
    End Function

    Public Function UpdateTrtValue(ByVal oldTblTRTS As TblTRTS) As Boolean
        Using conn As New SqlConnection(ConnectionString)


            Dim affectedRows As Integer = conn.Execute("UPDATE [TblTRTS] SET [TrtValue] = @TrtValue WHERE [TrtID] = @TrtID", New With {.TrtID = oldTblTRTS.TrtID,
                                                       .TrtValue = oldTblTRTS.TrtValue})

            Return affectedRows > 0
        End Using
    End Function

    Public Function Add(ByVal clsTblTRTS As TblTRTS) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO TblTRTS 
                                      (Trt, TrtValue, ShapeID, TrtDetails, TrtLVL, TrtLoc, ToothID, ToothIDkID, 
                                       OldTrt, TrtGroup, ParentGroup, TrtColor, TrtBrdrClr, TrtBrdrThick, 
                                       KidTrt, TrtClrID, DefFillColor, DefBrdrColor, DefBrdrThick) 
                                      VALUES 
                                      (@Trt, @TrtValue, @ShapeID, @TrtDetails, @TrtLVL, @TrtLoc, @ToothID, @ToothIDkID, 
                                       @OldTrt, @TrtGroup, @ParentGroup, @TrtColor, @TrtBrdrClr, @TrtBrdrThick, 
                                       @KidTrt, @TrtClrID, @DefFillColor, @DefBrdrColor, @DefBrdrThick)"

            RowsAffected = conn.Execute(sql, New With {
                        .Trt = clsTblTRTS.Trt,
                        .TrtValue = If(clsTblTRTS.TrtValue.HasValue, clsTblTRTS.TrtValue.Value, 0),
                        .ShapeID = If(clsTblTRTS.ShapeID.HasValue, clsTblTRTS.ShapeID.Value, DBNull.Value),
                        .TrtDetails = clsTblTRTS.TrtDetails,
                        .TrtLVL = clsTblTRTS.TrtLVL,
                        .TrtLoc = clsTblTRTS.TrtLoc,
                        .ToothID = clsTblTRTS.ToothID,
                        .ToothIDkID = clsTblTRTS.ToothIDkID,
                        .OldTrt = clsTblTRTS.OldTrt,
                        .TrtGroup = clsTblTRTS.TrtGroup,
                        .ParentGroup = clsTblTRTS.ParentGroup,
                        .TrtColor = clsTblTRTS.TrtColor,
                        .TrtBrdrClr = clsTblTRTS.TrtBrdrClr,
                        .TrtBrdrThick = If(clsTblTRTS.TrtBrdrThick.HasValue, clsTblTRTS.TrtBrdrThick.Value, 0),
                        .KidTrt = clsTblTRTS.KidTrt,
                        .TrtClrID = If(clsTblTRTS.TrtClrID.HasValue, clsTblTRTS.TrtClrID.Value, DBNull.Value),
                        .DefFillColor = clsTblTRTS.DefFillColor,
                        .DefBrdrColor = clsTblTRTS.DefBrdrColor,
                        .DefBrdrThick = If(clsTblTRTS.DefBrdrThick.HasValue, clsTblTRTS.DefBrdrThick.Value, 0)
                    })

            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(ByVal oldTblTRTS As TblTRTS, ByVal newTblTRTS As TblTRTS) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                        .TrtID = oldTblTRTS.TrtID,
                        .NewTrt = newTblTRTS.Trt,
                        .NewTrtValue = If(newTblTRTS.TrtValue.HasValue, newTblTRTS.TrtValue.Value, DBNull.Value),
                        .NewShapeID = If(newTblTRTS.ShapeID.HasValue, newTblTRTS.ShapeID.Value, DBNull.Value),
                        .NewTrtDetails = newTblTRTS.TrtDetails,
                        .NewTrtLVL = newTblTRTS.TrtLVL,
                        .NewTrtLoc = newTblTRTS.TrtLoc,
                        .NewToothID = newTblTRTS.ToothID,
                        .NewToothIDkID = newTblTRTS.ToothIDkID,
                        .NewOldTrt = newTblTRTS.OldTrt,
                        .NewTrtGroup = newTblTRTS.TrtGroup,
                        .NewParentGroup = newTblTRTS.ParentGroup,
                        .NewTrtColor = newTblTRTS.TrtColor,
                        .NewTrtBrdrClr = newTblTRTS.TrtBrdrClr,
                        .NewTrtBrdrThick = If(newTblTRTS.TrtBrdrThick.HasValue, newTblTRTS.TrtBrdrThick.Value, DBNull.Value),
                        .NewKidTrt = newTblTRTS.KidTrt,
                        .NewTrtClrID = If(newTblTRTS.TrtClrID.HasValue, newTblTRTS.TrtClrID.Value, DBNull.Value),
                        .NewDefFillColor = newTblTRTS.DefFillColor,
                        .NewDefBrdrColor = newTblTRTS.DefBrdrColor,
                        .NewDefBrdrThick = If(newTblTRTS.DefBrdrThick.HasValue, newTblTRTS.DefBrdrThick.Value, DBNull.Value)
                    }

            Dim affectedRows As Integer = conn.Execute(
                        "UPDATE [TblTRTS] SET 
                         [Trt] = @NewTrt, 
                         [TrtValue] = @NewTrtValue, 
                         [ShapeID] = @NewShapeID, 
                         [TrtDetails] = @NewTrtDetails, 
                         [TrtLVL] = @NewTrtLVL, 
                         [TrtLoc] = @NewTrtLoc, 
                         [ToothID] = @NewToothID, 
                         [ToothIDkID] = @NewToothIDkID, 
                         [OldTrt] = @NewOldTrt, 
                         [TrtGroup] = @NewTrtGroup, 
                         [ParentGroup] = @NewParentGroup, 
                         [TrtColor] = @NewTrtColor, 
                         [TrtBrdrClr] = @NewTrtBrdrClr, 
                         [TrtBrdrThick] = @NewTrtBrdrThick, 
                         [KidTrt] = @NewKidTrt, 
                         [TrtClrID] = @NewTrtClrID, 
                         [DefFillColor] = @NewDefFillColor, 
                         [DefBrdrColor] = @NewDefBrdrColor, 
                         [DefBrdrThick] = @NewDefBrdrThick 
                         WHERE [TrtID] = @TrtID", parameters)

            Return affectedRows > 0
        End Using
    End Function

    Public Function AddTransactional(conn As SqlConnection, trans As SqlTransaction, clsTblTRTS As TblTRTS) As Boolean
        Try
            Dim sql As String = "INSERT INTO TblTRTS  
                                  (Trt, TrtValue, ShapeID, TrtDetails, TrtLVL, TrtLoc, ToothID, ToothIDkID, 
                                   OldTrt, TrtGroup, ParentGroup, TrtColor, TrtBrdrClr, TrtBrdrThick, 
                                   KidTrt, TrtClrID, DefFillColor, DefBrdrColor, DefBrdrThick)  
                                  VALUES  
                                  (@Trt, @TrtValue, @ShapeID, @TrtDetails, @TrtLVL, @TrtLoc, @ToothID, @ToothIDkID, 
                                   @OldTrt, @TrtGroup, @ParentGroup, @TrtColor, @TrtBrdrClr, @TrtBrdrThick, 
                                   @KidTrt, @TrtClrID, @DefFillColor, @DefBrdrColor, @DefBrdrThick)"

            Dim parameters As New List(Of SqlParameter) From {
                        New SqlParameter("@Trt", clsTblTRTS.Trt),
                        New SqlParameter("@TrtValue", If(clsTblTRTS.TrtValue.HasValue, clsTblTRTS.TrtValue.Value, DBNull.Value)),
                        New SqlParameter("@ShapeID", If(clsTblTRTS.ShapeID.HasValue, clsTblTRTS.ShapeID.Value, DBNull.Value)),
                        New SqlParameter("@TrtDetails", clsTblTRTS.TrtDetails),
                        New SqlParameter("@TrtLVL", clsTblTRTS.TrtLVL),
                        New SqlParameter("@TrtLoc", clsTblTRTS.TrtLoc),
                        New SqlParameter("@ToothID", clsTblTRTS.ToothID),
                        New SqlParameter("@ToothIDkID", clsTblTRTS.ToothIDkID),
                        New SqlParameter("@OldTrt", clsTblTRTS.OldTrt),
                        New SqlParameter("@TrtGroup", clsTblTRTS.TrtGroup),
                        New SqlParameter("@ParentGroup", clsTblTRTS.ParentGroup),
                        New SqlParameter("@TrtColor", clsTblTRTS.TrtColor),
                        New SqlParameter("@TrtBrdrClr", clsTblTRTS.TrtBrdrClr),
                        New SqlParameter("@TrtBrdrThick", If(clsTblTRTS.TrtBrdrThick.HasValue, clsTblTRTS.TrtBrdrThick.Value, DBNull.Value)),
                        New SqlParameter("@KidTrt", clsTblTRTS.KidTrt),
                        New SqlParameter("@TrtClrID", If(clsTblTRTS.TrtClrID.HasValue, clsTblTRTS.TrtClrID.Value, DBNull.Value)),
                        New SqlParameter("@DefFillColor", clsTblTRTS.DefFillColor),
                        New SqlParameter("@DefBrdrColor", clsTblTRTS.DefBrdrColor),
                        New SqlParameter("@DefBrdrThick", If(clsTblTRTS.DefBrdrThick.HasValue, clsTblTRTS.DefBrdrThick.Value, DBNull.Value))
                    }

            Dim rowsAffected As Integer = conn.Execute(sql, parameters, trans)
            Return rowsAffected > 0
        Catch ex As Exception
            Debug.WriteLine($"Error in AddTransactional for TblTRTS: {ex.ToString()}")
            Throw
        End Try
    End Function

    Public Function UpdateTransactional(conn As SqlConnection, trans As SqlTransaction, clsTblTRTS As TblTRTS) As Boolean
        Try
            Dim sql As String = "UPDATE TblTRTS SET 
                                        Trt = @Trt, 
                                        TrtValue = @TrtValue, 
                                        ShapeID = @ShapeID, 
                                        TrtDetails = @TrtDetails, 
                                        TrtLVL = @TrtLVL, 
                                        TrtLoc = @TrtLoc, 
                                        ToothID = @ToothID, 
                                        ToothIDkID = @ToothIDkID, 
                                        OldTrt = @OldTrt, 
                                        TrtGroup = @TrtGroup, 
                                        ParentGroup = @ParentGroup, 
                                        TrtColor = @TrtColor, 
                                        TrtBrdrClr = @TrtBrdrClr, 
                                        TrtBrdrThick = @TrtBrdrThick, 
                                        KidTrt = @KidTrt, 
                                        TrtClrID = @TrtClrID, 
                                        DefFillColor = @DefFillColor, 
                                        DefBrdrColor = @DefBrdrColor, 
                                        DefBrdrThick = @DefBrdrThick
                                        WHERE TrtID = @TrtID"

            Dim parameters As New List(Of SqlParameter) From {
                        New SqlParameter("@Trt", clsTblTRTS.Trt),
                        New SqlParameter("@TrtValue", If(clsTblTRTS.TrtValue.HasValue, clsTblTRTS.TrtValue.Value, DBNull.Value)),
                        New SqlParameter("@ShapeID", If(clsTblTRTS.ShapeID.HasValue, clsTblTRTS.ShapeID.Value, DBNull.Value)),
                        New SqlParameter("@TrtDetails", clsTblTRTS.TrtDetails),
                        New SqlParameter("@TrtLVL", clsTblTRTS.TrtLVL),
                        New SqlParameter("@TrtLoc", clsTblTRTS.TrtLoc),
                        New SqlParameter("@ToothID", clsTblTRTS.ToothID),
                        New SqlParameter("@ToothIDkID", clsTblTRTS.ToothIDkID),
                        New SqlParameter("@OldTrt", clsTblTRTS.OldTrt),
                        New SqlParameter("@TrtGroup", clsTblTRTS.TrtGroup),
                        New SqlParameter("@ParentGroup", clsTblTRTS.ParentGroup),
                        New SqlParameter("@TrtColor", clsTblTRTS.TrtColor),
                        New SqlParameter("@TrtBrdrClr", clsTblTRTS.TrtBrdrClr),
                        New SqlParameter("@TrtBrdrThick", If(clsTblTRTS.TrtBrdrThick.HasValue, clsTblTRTS.TrtBrdrThick.Value, DBNull.Value)),
                        New SqlParameter("@KidTrt", clsTblTRTS.KidTrt),
                        New SqlParameter("@TrtClrID", If(clsTblTRTS.TrtClrID.HasValue, clsTblTRTS.TrtClrID.Value, DBNull.Value)),
                        New SqlParameter("@DefFillColor", clsTblTRTS.DefFillColor),
                        New SqlParameter("@DefBrdrColor", clsTblTRTS.DefBrdrColor),
                        New SqlParameter("@DefBrdrThick", If(clsTblTRTS.DefBrdrThick.HasValue, clsTblTRTS.DefBrdrThick.Value, DBNull.Value)),
                        New SqlParameter("@TrtID", clsTblTRTS.TrtID)
                    }

            Dim rowsAffected As Integer = conn.Execute(sql, parameters, trans)
            Return rowsAffected > 0
        Catch ex As Exception
            Debug.WriteLine($"Error in UpdateTransactional for TblTRTS: {ex.ToString()}")
            Throw
        End Try
    End Function


    Public Function UpdateTrtClr(ByVal TrtID As Integer,
                                       ByVal NewTrtColor As String,
                                       ByVal NewTrtBrdrClr As String,
                                       ByVal NewTrtBrdrThick As Integer) As Boolean
        Try
            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Dim sql = "UPDATE [TblTRTS] SET [TrtColor] = @NewTrtColor, 
                                  [TrtBrdrClr] = @NewTrtBrdrClr, [TrtBrdrThick] = @NewTrtBrdrThick  
                                  WHERE TrtID = @TrtID"

                Dim affectedRows As Integer = conn.Execute(sql,
                            New With {
                                .TrtID = TrtID,
                                .NewTrtColor = NewTrtColor,
                                .NewTrtBrdrClr = NewTrtBrdrClr,
                                .NewTrtBrdrThick = NewTrtBrdrThick
                            })

                Return affectedRows > 0
            End Using
        Catch ex As Exception
            ' Log the error (implementation depends on your logging system)
            ' Example: Logger.Error("Error updating treatment color", ex)
            Return False
        End Try
    End Function

    Public Function UpdateDefTrtClr(ByVal TrtID As Integer,
                                       ByVal NewTrtColor As String,
                                       ByVal NewTrtBrdrClr As String,
                                       ByVal NewTrtBrdrThick As Integer) As Boolean
        Try
            Using conn As New SqlConnection(ConnectionString)
                conn.Open() '[DefFillColor],[DefBrdrColor], [DefBrdrThick]
                Dim sql = "UPDATE [TblTRTS] SET  [TrtColor] = @NewTrtColor, 
                                  [TrtBrdrClr] = @NewTrtBrdrClr, [TrtBrdrThick] = @NewTrtBrdrThick ,[DefFillColor] = @NewTrtColor, 
                                  [DefBrdrColor] = @NewTrtBrdrClr, [DefBrdrThick] = @NewTrtBrdrThick  
                                  WHERE TrtID = @TrtID"

                Dim affectedRows As Integer = conn.Execute(sql,
                            New With {
                                .TrtID = TrtID,
                                .NewTrtColor = NewTrtColor,
                                .NewTrtBrdrClr = NewTrtBrdrClr,
                                .NewTrtBrdrThick = NewTrtBrdrThick
                            })

                Return affectedRows > 0
            End Using
        Catch ex As Exception
            ' Log the error (implementation depends on your logging system)
            ' Example: Logger.Error("Error updating treatment color", ex)
            Return False
        End Try
    End Function
    Public Function UpdateTreatFillColor(fillColor As String, Treat As String) As Integer
        Dim sqlCheck As String = "SELECT TOP 1 1 FROM Patient_ToothTrt WHERE Treat = @Treat" 'SELECT  [TrtID]  FROM [dbo].[TblTRTS] WHERE Trt=
        Dim sqlUpdate As String = "UPDATE Patient_ToothTrt SET FillColor = @FillColor WHERE Treat = @Treat"

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()

            ' Check if any record exists with this treatment
            Dim exists As Boolean = conn.ExecuteScalar(Of Integer?)(sqlCheck, New With {.Treat = Treat}).HasValue

            If Not exists Then
                Return False
            End If

            ' Perform the update
            Dim affected As Integer = conn.Execute(sqlUpdate, New With {.FillColor = fillColor, .Treat = Treat})
            Return affected '> 0
        End Using
    End Function
    Public Function Delete(ByVal clsTblTRTS As TblTRTS) As Boolean
        Dim deleteStatement As String =
                        "DELETE FROM [TblTRTS] 
			            WHERE TrtID = @TrtID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.TrtID = clsTblTRTS.TrtID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class
