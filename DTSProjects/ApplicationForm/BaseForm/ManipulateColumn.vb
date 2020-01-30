Public Class ManipulateColumn
#Region "Deklarasi"
    Friend grid As Janus.Windows.GridEX.GridEX
    Friend ManipulateColumnName As String
#End Region
    Friend Sub FillcomboColumn()
        Me.cmbColumn.Items.Clear()
        For i As Integer = 0 To grid.RootTable.Columns.Count - 1
            Me.cmbColumn.Items.Add(Me.grid.RootTable.Columns(i).DataMember)
        Next

    End Sub

    Private Sub txtManipulate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManipulate.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Me.Button1_Click(Me.Button1, New System.EventArgs)
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.cmbColumn.Text = "" Then
            MessageBox.Show("Please Define Column Name!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        '     Try
        '{
        '         Switch(this.ManipulateColumnName)
        '	{
        '		case "Hide" :
        '		{
        '			
        '		}
        '		case "Show" :
        '		{
        '			if(this.cmbColumn.Text == "")
        '			{
        '				MessageBox.Show("Tentukan nama kolomnya","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        '				return;
        '			}
        '			this.Grid.RootTable.Columns[this.cmbColumn.Text].Visible = true;
        '			if(this.WidtColumn != 0 || this.WidtColumn == -1)
        '			{
        '				this.Grid.RootTable.Columns[this.cmbColumn.Text].Width = this.WidtColumn;
        '			}
        '                     Else
        '			{
        '			this.Grid.RootTable.Columns[this.cmbColumn.Text].Width = 100;
        '			}
        '			break;
        '		}
        '		case "Rename" :
        '		{

        '		break;
        '		}
        '	}
        '}
        'catch(System.Exception Ex)
        '{
        'MessageBox.Show(Ex.Message);
        '}
        Try
            Select Case Me.ManipulateColumnName
                Case "Rename"
                   
                    If Me.txtManipulate.Text = "" Then
                        MessageBox.Show("Please type the column name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    Me.grid.RootTable.Columns(Me.cmbColumn.Text).Caption = Me.txtManipulate.Text
                Case "Hide"
                    '			if(this.Grid != null)
                    '			{
                    '				this.WidtColumn = this.Grid.RootTable.Columns[this.cmbColumn.Text].Width; 
                    '				this.Grid.RootTable.Columns[this.cmbColumn.Text].Visible = false;

                    '			}
                    '			break;

                    If Not IsNothing(Me.grid) Then
                        Me.grid.RootTable.Columns(Me.cmbColumn.Text).Visible = False
                    End If

                Case "Show"
                    If Not IsNothing(Me.grid) Then
                        Me.grid.RootTable.Columns(Me.cmbColumn.Text).Visible = True
                        Me.grid.RootTable.Columns(Me.cmbColumn.Text).Width = 100
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ManipulateColumn_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose(True)
    End Sub

    Private Sub cmbColumn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbColumn.TextChanged
        Try
            If Me.cmbColumn.Text <> "" Or Me.cmbColumn.SelectedIndex <> -1 Then
                If Me.ManipulateColumnName = "Rename" Then
                    Me.grpRename.Enabled = True
                    Me.txtManipulate.Enabled = True
                Else
                    Me.grpRename.Enabled = False
                    Me.txtManipulate.Enabled = False
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class