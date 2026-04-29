Public Module DapperExtensions
    Public Function ToNullableDateParam(value As Date?) As Object
        Return If(value.HasValue, value.Value, DBNull.Value)
    End Function

    Public Function FromDBNullDate(value As Object) As Date?
        Return If(value Is DBNull.Value, Nothing, CType(value, Date?))
    End Function
End Module
