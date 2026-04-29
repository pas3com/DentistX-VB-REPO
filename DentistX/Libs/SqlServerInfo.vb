#Region "Namespace references"
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms
#End Region

Namespace Util
    ''' <summary>
    ''' Class SqlServerInfo <p/>
    ''' Provides information about a MS SQL server instance.
    ''' </summary>
    ''' <example><pre>
    '''    SqlServerInfo[] servs = SqlServerInfo.Seek();
    '''    foreach(SqlServerInfo inst in servs)
    '''    {
    '''         Console.WriteLine("Server: {0}, InstanceName: {1}, Version: {2}", 
    '''                         serv.ServerName, serv.InstanceName, serv.Version);
    '''         foreach(string db in serv.Catalogs)
    '''         {
    '''             Console.WriteLine("      Database: {0}", db);
    '''         }
    '''    }
    '''</pre></example>
    '''<remarks>
    '''Copyright &#169; 2005, James M. Curran. <br/>
    ''' First published on CodeProject.com, Nov 2005 <br/>
    '''May be used freely.
    '''</remarks>
    Public Class SqlServerInfo
#Region "Fields"
        Private oServerName As String
        Private oInstanceName As String
        Private oIsClustered As Boolean
        Private oVersion As String
        Private otcpPort As Integer
        Private oNp As String
        Private oRpc As String
        Private oIP As IPAddress
        Private oCatalogs As StringCollection
        Private oUserId As String
        Private oPassword As String
        Private oIntegratedSecurity As Boolean = True
        Private oTimeOut As Integer = 2
#End Region

#Region "Constructors"
        ''' <summary>
        ''' Initializes a new instance of the <see cref="SqlServerInfo"/> class.
        ''' </summary>
        Private Sub New()

        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SqlServerInfo"/> class.
        ''' </summary>
        ''' <param name="ip">The ip.</param>
        ''' <param name="info">The info.</param>
        Public Sub New(ByVal ip As IPAddress, ByVal info() As Byte)
            Me.New(ip, System.Text.ASCIIEncoding.ASCII.GetString(info, 3, BitConverter.ToInt16(info, 1)))
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SqlServerInfo"/> class.
        ''' </summary>
        ''' <param name="ip">The ip address.</param>
        ''' <param name="info">The info.</param>
        Public Sub New(ByVal ip As IPAddress, ByVal info As String)
            oIP = ip
            Dim nvs() As String = info.Split(";"c)
            For i As Integer = 0 To nvs.Length - 1 Step 2
                Select Case nvs(i).ToLower()
                    Case "servername"
                        Me.oServerName = nvs(i + 1)

                    Case "instancename"
                        Me.oInstanceName = nvs(i + 1)

                    Case "isclustered"
                        Me.oIsClustered = (nvs(i + 1).ToLower() = "yes") 'bool.Parse(nvs[i+1]);

                    Case "version"
                        Me.oVersion = nvs(i + 1)

                    Case "tcp"
                        Me.otcpPort = Integer.Parse(nvs(i + 1))

                    Case "np"
                        Me.oNp = nvs(i + 1)

                    Case "rpc"
                        Me.oRpc = nvs(i + 1)

                End Select
            Next i
        End Sub

#End Region

#Region "Public Properties"

        ''' <summary>
        ''' Gets the IP address.
        ''' </summary>
        ''' <value>The address.</value>
        ''' <remarks>Presently, this is not implemented and will always return null,</remarks>
        Public ReadOnly Property Address() As IPAddress
            Get
                Return oIP
            End Get
        End Property
        ''' <summary>
        ''' Gets the name of the server.
        ''' </summary>
        ''' <value>The name of the server.</value>
        Public ReadOnly Property ServerName() As String
            Get
                Return oServerName
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the instance.
        ''' </summary>
        ''' <value>The name of the instance.</value>
        Public ReadOnly Property InstanceName() As String
            Get
                Return oInstanceName
            End Get
        End Property
        ''' <summary>
        ''' Gets a value indicating whether this instance is clustered.
        ''' </summary>
        ''' <value>
        ''' 	<see langword="true"/> if this instance is clustered; otherwise, <see langword="false"/>.
        ''' </value>
        Public ReadOnly Property IsClustered() As Boolean
            Get
                Return oIsClustered
            End Get
        End Property
        ''' <summary>
        ''' Gets the version.
        ''' </summary>
        ''' <value>The version.</value>
        Public ReadOnly Property Version() As String
            Get
                Return oVersion
            End Get
        End Property
        ''' <summary>
        ''' Gets the TCP port.
        ''' </summary>
        ''' <value>The TCP port.</value>
        Public ReadOnly Property TcpPort() As Integer
            Get
                Return otcpPort
            End Get
        End Property
        ''' <summary>
        ''' Gets the named pipe.
        ''' </summary>
        ''' <value>The named pipe.</value>
        Public ReadOnly Property NamedPipe() As String
            Get
                Return oNp
            End Get
        End Property

        ''' <summary>
        ''' Gets the catalogs.
        ''' </summary>
        ''' <value>The catalogs.</value>
        Public ReadOnly Property Catalogs() As StringCollection
            Get
                If oCatalogs Is Nothing Then
                    oCatalogs = GetCatalogs()
                End If
                Return oCatalogs
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the user id.
        ''' </summary>
        ''' <value>The user id.</value>
        Public Property UserId() As String
            Get
                Return oUserId
            End Get
            Set(ByVal value As String)
                oUserId = value
                oIntegratedSecurity = False
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the password.
        ''' </summary>
        ''' <value>The password.</value>
        Public Property Password() As String
            Get
                Return oPassword
            End Get
            Set(ByVal value As String)
                oPassword = value
                oIntegratedSecurity = False
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [integrated security].
        ''' </summary>
        ''' <value>
        ''' 	<see langword="true"/> if [integrated security]; otherwise, <see langword="false"/>.
        ''' </value>
        Public Property IntegratedSecurity() As Boolean
            Get
                Return oIntegratedSecurity
            End Get
            Set(ByVal value As Boolean)
                oIntegratedSecurity = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the time out.
        ''' </summary>
        ''' <value>The time out.</value>
        Public Property TimeOut() As Integer
            Get
                Return oTimeOut
            End Get
            Set(ByVal value As Integer)
                oTimeOut = value
            End Set
        End Property

#End Region

#Region "Public Methods"
        ''' <summary>
        ''' Tests the connection.
        ''' </summary>
        ''' <returns></returns>
        Public Function TestConnection() As Boolean
            Dim conn As OleDbConnection = Me.GetConnection()
            Dim success As Boolean = False
            Try
                conn.Open()
                conn.Close()
                success = True
            Catch
            End Try
            Return success
        End Function


        ''' <summary>
        ''' Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        ''' </returns>
        Public Overrides Function ToString() As String
            If Me.InstanceName Is Nothing OrElse Me.InstanceName = "MSSQLSERVER" Then
                Return Me.ServerName
            Else
                Return Me.ServerName & "\" & Me.InstanceName
            End If
        End Function
#End Region

#Region "Private Methods"
        Private Function GetCatalogs() As StringCollection
            'INSTANT VB NOTE: The variable catalogs was renamed since Visual Basic does not handle local variables named the same as class members well:
            Dim catalogs_Renamed As New StringCollection()

            Try
                Dim myConnection As OleDbConnection = Me.GetConnection()
                myConnection.Open()
                Dim schemaTable As DataTable = myConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, Nothing)
                myConnection.Close()
                For Each dr As DataRow In schemaTable.Rows
                    catalogs_Renamed.Add(TryCast(dr(0), String))
                Next dr
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Return catalogs_Renamed
        End Function

        Private Function GetConnection() As OleDbConnection
            Dim myConnString As String = If(Me.IntegratedSecurity, String.Format("Provider=SQLOLEDB;Data Source={0};Integrated Security=SSPI;Connect Timeout={1}", Me, Me.TimeOut), String.Format("Provider=SQLOLEDB;Data Source={0};User Id={1};Password={2};Connect Timeout={3}", Me, Me.UserId, Me.Password, Me.TimeOut))

            Return New OleDbConnection(myConnString)
        End Function

#End Region

#Region "Public Static Method - Seek"
        ''' <summary>
        ''' Seeks SQL servers on this network.
        ''' </summary>
        ''' <returns>An array of SqlServerInfo objects describing Sql Servers on this network</returns>
        Public Shared Function Seek() As SqlServerInfo()
            Dim socket As New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)

            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1)
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000)

            '  For .Net v 2.0 it's a bit simpler
            '  socket.EnableBroadcast = true;	// for .Net v2.0
            '  socket.ReceiveTimeout = 3000;	// for .Net v2.0

            Dim servers As New ArrayList()
            Try
                Dim msg() As Byte = {&H2}
                Dim ep As New IPEndPoint(IPAddress.Broadcast, 1434)
                socket.SendTo(msg, ep)

                Dim cnt As Integer = 0
                Dim bytBuffer(1023) As Byte
                Do
                    cnt = socket.Receive(bytBuffer)
                    servers.Add(New SqlServerInfo(Nothing, bytBuffer))
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 300)
                Loop While cnt <> 0
            Catch socex As SocketException
                Const WSAETIMEDOUT As Integer = 10060 ' Connection timed out.
                Const WSAEHOSTUNREACH As Integer = 10065 ' No route to host.

                ' Re-throw if it's not a timeout.
                If socex.ErrorCode = WSAETIMEDOUT OrElse socex.ErrorCode = WSAEHOSTUNREACH Then
                    ' DO nothing......
                Else
                    '					Console.WriteLine("{0} {1}", socex.ErrorCode, socex.Message);
                    Throw
                End If
            Finally
                socket.Close()
            End Try
            '=============MyCode================


            '=============MyCode================
            ' Copy from the untyped but expandable ArrayList, to a
            ' type-safe but fixed array of SqlServerInfos.

            Dim aServers(servers.Count - 1) As SqlServerInfo
            servers.CopyTo(aServers)
            Return aServers
        End Function
#End Region

        Private Function GetIPv4Address(ByVal cb As ComboBox) As String
            cb.Items.Clear()
            GetIPv4Address = String.Empty
            Dim strHostName As String = System.Net.Dns.GetHostName()
            Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

            For Each ipheal As System.Net.IPAddress In iphe.AddressList
                If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    GetIPv4Address = ipheal.ToString()
                    cb.Items.Add(GetIPv4Address)
                End If
            Next

        End Function

        'Using The Class=================
        '==================================
        'Shared Sub Main(ByVal args() As String)
        '    Dim servs() As SqlServerInfo = SqlServerInfo.Seek()
        '    For Each serv As SqlServerInfo In servs
        '        Console.WriteLine(serv)

        '        ' For this to display the Catalogs,
        '        ' You must be able to connect using 
        '        ' Integrated (Windows) Security.
        '        ' If not, fill in the following lines:
        '        ' serv.UserId = "......";
        '        ' serv.Password = ".....";
        '        For Each catalog As String In serv.Catalogs
        '            Console.WriteLine(vbTab & "{0}", catalog)

        '        Next catalog

        '    Next serv
        '    Console.ReadLine()
        'End Sub

    End Class
End Namespace
