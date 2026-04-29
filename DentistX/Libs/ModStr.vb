Imports System.Reflection

Module ModStr
    Friend DentConn As String = My.Settings.DentistXConnectionString





    Public Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

    Public Property PatientNames As List(Of Names)
    Public Class Names
        Implements IEnumerable
        Public Property Namep As String
        'Public Property Id As Integer



        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Throw New NotImplementedException()
        End Function


    End Class
    Public Function ConvertTableToList(dt As DataTable) As List(Of Names)

        Dim pNames As New List(Of Names)
        For Each rw As DataRow In dt.Rows
            Dim pName As New Names With
        {
          .Namep = rw.Item("PatientName")
        }
            ',
            '.Id = rw.Item("PatientID")

            pNames.Add(pName)
        Next

        Return pNames

    End Function


End Module
