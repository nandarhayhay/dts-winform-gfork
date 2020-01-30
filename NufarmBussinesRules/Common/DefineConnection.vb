Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.NetworkInformation
Namespace common
    Public Class DefineConnection
        Public Shared Function TestSqlServer(ByVal Address As String, ByVal Port As Integer)
            Dim result As Boolean = False
            Try

                Dim timeOut As Integer = 1500

                Using socket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    Dim asyncResult As IAsyncResult = socket.BeginConnect(Address, Port, Nothing, Nothing)
                    result = asyncResult.AsyncWaitHandle.WaitOne(timeOut, True)
                    socket.Close()
                End Using
            Catch ex As Exception
                Return False
            End Try
            Return result
        End Function
        'private bool TestSqlServer(string address, int port)
        ' {
        '     int timeout = 1500;//10 detic
        '     //if (ConfigurationManager.AppSettings["RemoteTestTimeout"] != null)
        '     //    timeout = int.Parse(ConfigurationManager.AppSettings["RemoteTestTimeout"]);
        '    bool result = false;
        '     try
        '     {

        '         using (Socket socket  = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        '         {
        '             IAsyncResult asyncResult = socket.BeginConnect(address, port, null, null);
        '             result = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
        '             socket.Close();
        '         }
        '         return result;
        '     }
        '     catch { return false; }
        ' }
        Public Shared Function pingAddress(ByVal hostName As String, ByRef IpAdd As String) As Boolean
            Try
                Dim pingSender As New Ping
                Dim options As New PingOptions()
                options.DontFragment = True
                Dim data As String = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                Dim buffer() As Byte = System.Text.Encoding.ASCII.GetBytes(data)
                Dim reply As PingReply = pingSender.Send(hostName, 128, buffer, options)
                If reply.Status = IPStatus.Success Then
                    IpAdd = Dns.GetHostAddresses(hostName)(0).ToString()
                    Return True
                End If
                Return False
            Catch ex As Exception
                Return False
            End Try
        End Function
        ' private bool PingAddress(string HostName,ref string  IpAd )
        ' {
        '     try
        '     {
        '         Ping pingSender = new Ping();
        '         PingOptions options = new PingOptions();


        '         options.DontFragment = true;


        '         string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        '         byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);

        '         PingReply reply = pingSender.Send(HostName, 128, buffer, options);

        '         if (reply.Status == IPStatus.Success)
        '         {
        '             IpAd = Dns.GetHostAddresses(HostName)[0].ToString();
        '             return true;

        '         }
        '     }
        '     catch (Exception)
        '     {

        '         return false;
        '     }

        '     return false;
        ' }
    End Class
End Namespace

