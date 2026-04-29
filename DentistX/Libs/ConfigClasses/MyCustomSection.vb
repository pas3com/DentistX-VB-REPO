Imports System.Configuration

Public Class MyCustomSection
    Inherits ConfigurationSection


    Dim Pairs As KeyValuePair(Of String, String)

    Public Sub GetPairs()

    End Sub

    Public Function GetKeyValue(ByVal keyName As String) As String
        Dim s As String = ""

        Return s
    End Function
    <ConfigurationProperty("key", DefaultValue:="Default", IsRequired:=True)>
    <StringValidator(InvalidCharacters:="~!@#$%^&*()[]{}/;'""|\", MinLength:=1, MaxLength:=60)>
    Public Property Key() As String
        Get
            Return Me("key").ToString()
        End Get
        Set(ByVal value As String)
            Me("key") = value
        End Set
    End Property

    <ConfigurationProperty("Value", DefaultValue:="Default", IsRequired:=True)>
    <StringValidator(InvalidCharacters:="~!@#$%^&*()[]{}/;'""|\", MinLength:=1, MaxLength:=60)>
    Public Property Value() As String
        Get
            Return Me("Value").ToString()
        End Get
        Set(ByVal value As String)
            Me("Value") = value
        End Set
    End Property
End Class
'==========================
'<section name="Vault" type="System.Configuration.NameValueSectionHandler" />
Public Class TestConfigData
    Inherits ConfigurationSection

    <ConfigurationProperty("Name", IsRequired:=True)>
    Public Property Name() As String
        Get
            Return CStr(Me("Name"))
        End Get
        Set(ByVal value As String)
            Me("Name") = value
        End Set
    End Property

    <ConfigurationProperty("Data", IsRequired:=False), IntegerValidator(MinValue:=0)>
    Public Property Data() As Integer
        Get
            Return CInt(Math.Truncate(Me("Data")))
        End Get
        Set(ByVal value As Integer)
            Me("Data") = value
        End Set
    End Property
End Class
