Public Class frmConfig

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim SQL As String
        Dim lBeginTX As Integer = 0
        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection
        If Not Verify() Then GoTo LineExit 'Check Data

        
        SQL = "UPDATE PROJECTCONFIG SET "
        SQL = SQL & "  PROJECTNAME='" & ConvertNullToString(txtProjectName.Text) & "'"
        SQL = SQL & " ,PROJECTADDRESS='" & ConvertNullToString(txtAddress.Text) & "'"
        SQL = SQL & " ,HOUSEPRICE1=" & ConvertNullToZero(HousePrice1.Text)
        SQL = SQL & " ,HOUSEPRICE2=" & ConvertNullToZero(HousePrice2.Text)
        SQL = SQL & " ,EXPIREDAY=" & ConvertNullToZero(txtExpireDay.Text)
        SQL = SQL & " ,BANKACCOUNT='" & ConvertNullToString(txtBankAccount.Text) & "'"
        SQL = SQL & " ,SIGNNAME='" & ConvertNullToString(txtSignName.Text) & "'"
        SQL = SQL & " ,SIGNPOSITION='" & ConvertNullToString(txtPosition.Text) & "'"
        SQL = SQL & " ,UPDATEBY='" & gUserName & "'"
        SQL = SQL & " ,UPDATEDATE=" & formatSQLDate(Now)
         
        Dim lTr As OleDb.OleDbTransaction
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()

        lTr = gConnection.BeginTransaction()
        lBeginTX = 1
        With lCom
            .Connection = gConnection
            .Transaction = lTr
            .CommandText = SQL
            .ExecuteNonQuery()
        End With

        lTr.Commit()
        MsgBox("การบันทึกข้อมูลเสร็จสิ้น", MsgBoxStyle.Information + MessageBoxButtons.OK, "การบันทึก")
        Me.Close()
LineExit:
        Exit Sub
LineError:
        If lBeginTX > 0 Then lTr.Rollback()
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

    End Sub

    Protected Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub frmEditHouseInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo LineError
 
        LoadData()

        Exit Sub
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        Me.Close()
    End Sub

    Private Sub LoadData()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        If Not ConnectStatus Then GoTo LineExit
        SQL = "SELECT *  FROM PROJECTCONFIG"
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")
        If ds.Tables("Data").Rows.Count > 0 Then
            Me.txtProjectName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("PROJECTNAME"))
            Me.txtAddress.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("PROJECTADDRESS"))
            Me.HousePrice1.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("HOUSEPRICE1"))
            Me.HousePrice2.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("HOUSEPRICE2"))
            Me.txtExpireDay.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("EXPIREDAY"))
            Me.txtBankAccount.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("BANKACCOUNT"))
            Me.txtSignName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("SIGNNAME"))
            Me.txtPosition.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("SIGNPOSITION"))
            
        End If
LineExit:
    End Sub

    Private Function Verify() As Boolean
        Verify = False

        If ConvertNullToString(Me.txtProjectName.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtProjectName.Focus()
            Exit Function
        End If

        If ConvertNullToString(Me.HousePrice1.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.HousePrice1.Focus()
            Exit Function
        ElseIf IsNumeric(Me.HousePrice1.Text) = False Then
            MsgBox("กรุณาระบุข้อมูลเป็นตัวเลข", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.HousePrice1.Focus()
            Exit Function
        End If

        If ConvertNullToString(Me.HousePrice2.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.HousePrice2.Focus()
            Exit Function
        ElseIf IsNumeric(Me.HousePrice2.Text) = False Then
            MsgBox("กรุณาระบุข้อมูลเป็นตัวเลข", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.HousePrice2.Focus()
            Exit Function
        End If
 
        If ConvertNullToString(Me.txtExpireDay.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtExpireDay.Focus()
            Exit Function
        ElseIf IsNumeric(Me.txtExpireDay.Text) = False Then
            MsgBox("กรุณาระบุข้อมูลเป็นตัวเลข", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtExpireDay.Focus()
            Exit Function
        End If

        Return True
    End Function

End Class
