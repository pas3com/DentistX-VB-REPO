Public NotInheritable Class JawFormFactory
    Public Shared Function CreateJawForm(formType As Type, sv As String) As IJawForm
        Return Activator.CreateInstance(formType, sv)
    End Function
End Class
