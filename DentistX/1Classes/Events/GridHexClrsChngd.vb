Public Class GridHexClrsChngd
    Inherits EventArgs


    Private _C1 As String
    Private _C2 As String
    Private _A As Integer
    Private _G As Byte
    Private _Reset As Integer

    Public Property C1() As String
        Get
            Return _C1
        End Get
        Set(ByVal value As String)
            _C1 = value
        End Set
    End Property

    Public Property C2() As String
        Get
            Return _C2
        End Get
        Set(ByVal value As String)
            _C2 = value
        End Set
    End Property


    Public Property G() As Byte
        Get
            Return _G
        End Get
        Set(ByVal value As Byte)
            _G = value
        End Set
    End Property


    Public Property A() As Byte
        Get
            Return _A
        End Get
        Set(ByVal value As Byte)
            _A = value
        End Set
    End Property

    Public Property Resett() As Byte
        Get
            Return _Reset
        End Get
        Set(ByVal value As Byte)
            _Reset = value
        End Set
    End Property

    Public Sub New(ByVal c1 As String, ByVal c2 As String, ByVal a As Byte, ByVal reset As Byte, ByVal g As Byte)
        Me.C1 = c1
        Me.C2 = c2
        Me.A = a
        Me.G = g
        Me.Resett = reset
    End Sub

End Class
