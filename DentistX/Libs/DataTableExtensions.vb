Imports System.Runtime.CompilerServices  ' Required for Extension

Module DataTableExtensions
    <Extension>
    Public Function ToDataTable(Of T)(data As IEnumerable(Of T)) As DataTable
        Dim dt As New DataTable()
        Dim props = GetType(T).GetProperties()
        For Each prop In props
            dt.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next
        For Each item In data
            Dim row = dt.NewRow()
            For Each prop In props
                row(prop.Name) = If(prop.GetValue(item, Nothing), DBNull.Value)
            Next
            dt.Rows.Add(row)
        Next
        Return dt
    End Function
End Module
