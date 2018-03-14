Public Class frmLogin


    Private Sub frmLogin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim lDBPath As String = Application.StartupPath & "\DB\DB.MDB"
        gConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & lDBPath & " ;Jet OLEDB:Database Password=titpoo99;"

        'txtLogin.Text = "artit"
        'txtPassword.Text = "artit"
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If VerifyUser() Then

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        Finally
        End Try

    End Sub



    Private Function VerifyUser() As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        VerifyUser = False
        Try
            If txtLogin.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุข้อมูล", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLogin.Focus()
                Exit Function
            End If

            If txtPassword.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุข้อมูล", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Exit Function
            End If


            If ConnectStatus(True) Then


                SQL = "SELECT * "
                SQL = SQL & " FROM USERS"
                SQL = SQL & " where USERNAME='" & ConvertNullToString(txtLogin.Text) & "'"
                SQL = SQL & " and ISDELETE='N'"
                da = New OleDb.OleDbDataAdapter(SQL, gConnection)
                ds = New DataSet
                da.Fill(ds, "Data")
                If ds.Tables("Data").Rows.Count = 0 Then
                    MessageBox.Show("ชื่อผู้ใช้งานไม่ถูกต้อง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtLogin.Focus()
                    txtLogin.SelectAll()
                    Return False
                Else

                    If ConvertNullToString(txtPassword.Text) <> ConvertNullToString(ds.Tables("Data").Rows(0).Item("PASSWORDS")) Then
                        MessageBox.Show("รหัสไม่ถูกต้อง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtPassword.Focus()
                        txtPassword.SelectAll()
                        Return False
                    Else
                        gUserLevel = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("USERLEVEL"))
                        gUserID = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("USERID"))
                        gUserName = ConvertNullToString(ds.Tables("Data").Rows(0).Item("USERNAME"))
                        Me.Hide()
                        frmMain.Show()
                        Return True
                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        End Try
    End Function

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Chr(13) And txtPassword.Text <> "" Then
            VerifyUser()
        End If
    End Sub

    Private Sub txtLogin_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtLogin.KeyPress
        If e.KeyChar = Chr(13) And txtLogin.Text <> "" Then
            VerifyUser()
        End If
    End Sub
End Class