Imports Microsoft.Win32
Imports System
Imports System.Windows.Forms
Namespace SettingDTS
    Public Class RegUser
        Implements IDisposable

        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            BaseRegistryKey = Nothing
        End Sub
        Private _baseRegistryKey As RegistryKey = Registry.CurrentUser
        Private subkey As String = "SOFTWARE\" & Application.ProductName.ToUpper()
        Public Property BaseRegistryKey() As RegistryKey
            Get
                Return _baseRegistryKey
            End Get
            Set(ByVal value As RegistryKey)
                _baseRegistryKey = value
            End Set
        End Property
        Public Function Read(ByVal KeyName As String) As String
            Dim rk As RegistryKey = BaseRegistryKey
            Dim sk1 As RegistryKey = rk.OpenSubKey(subkey)
            '  if (sk1 == null)
            '    {
            '        return null;
            '    }
            'Else
            '    {
            '    Try
            '        {
            '            // If the RegistryKey exists I get its value
            '            // or null is returned.
            '            return (string)sk1.GetValue(KeyName.ToUpper());
            '        }
            '        catch (Exception e)
            '        {
            '            // AAAAAAAAAAARGH, an error!
            '            ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
            '            return null;
            '        }
            '    }
            If (IsNothing(sk1)) Then : Return ""
            Else
                Try
                    Return CStr(sk1.GetValue(KeyName.ToUpper()))
                Catch ex As Exception
                    Return ""
                End Try
            End If
            Return ""
        End Function

        Public Function Write(ByVal KeyName As String, ByVal value As Object) As Boolean
            'Try
            '    {
            '        // Setting
            '        RegistryKey rk = baseRegistryKey;
            '        // I have to use CreateSubKey 
            '        // (create or open it if already exits), 
            '        // 'cause OpenSubKey open a subKey as read-only
            '        RegistryKey sk1 = rk.CreateSubKey(subKey);
            '        // Save the value
            '        sk1.SetValue(KeyName.ToUpper(), Value);

            '        return true;
            '    }
            '    catch (Exception e)
            '    {
            '        // AAAAAAAAAAARGH, an error!
            '        ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
            '        return false;
            '    }
            Try
                Dim rk As RegistryKey = BaseRegistryKey
                Dim sk1 As RegistryKey = rk.CreateSubKey(subkey)
                sk1.SetValue(KeyName.ToUpper(), value)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function DeleteKey(ByVal KeyName As String) As Boolean
            Try
                Dim rk As RegistryKey = BaseRegistryKey
                Dim sk1 As RegistryKey = rk.CreateSubKey(KeyName)
                If IsNothing(sk1) Then
                    Return True
                End If
                sk1.DeleteValue(KeyName)
                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function
        Public Function DeleteSubKeyTree() As Boolean
            Try
                Dim rk As RegistryKey = BaseRegistryKey
                Dim sk1 As RegistryKey = rk.OpenSubKey(subkey)
                If Not IsNothing(sk1) Then
                    rk.DeleteSubKeyTree(subkey)

                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function SubKeyCount() As Integer
            Try
                Dim rk As RegistryKey = BaseRegistryKey
                Dim sk1 As RegistryKey = rk.OpenSubKey(subkey)
                If Not IsNothing(sk1) Then
                    Return CInt(sk1.SubKeyCount)
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function
    End Class
End Namespace

