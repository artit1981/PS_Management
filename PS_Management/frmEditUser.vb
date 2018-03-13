Public Class frmEditUser

    Dim mMode As Mode
    Dim mID As Long
    Public Property ModeRun() As Integer
        Get
            ModeRun = mMode
        End Get
        Set(ByVal value As Integer)
            mMode = value
        End Set
    End Property

    Public Property ID() As Long
        Get
            ID = mID
        End Get
        Set(ByVal value As Long)
            mID = value
        End Set
    End Property

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim SQL As String
        Dim lBeginTX As Integer = 0
        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection
        If Not Verify() Then GoTo LineExit 'Check Data

        If mMode = Mode.AddNew Then
            Call GenNewID()
            SQL = "INSERT INTO USERS(USERID,USERNAME,PASSWORDS,DISPLAYNAME,REMARK,ISDELETE,CREATEBY,CREATEDATE,USERLEVEL)"
            SQL = SQL & " VALUES( "
            SQL = SQL & "  " & mID & ""
            SQL = SQL & " ,'" & ConvertNullToString(txtUserName.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtPassword.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtDisplayName.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtRemark.Text) & "'"
            SQL = SQL & " , 'N'  "
            SQL = SQL & " ,'" & gUserName & "'"
            SQL = SQL & " ," & formatSQLDate(Now)
            If rdoAdmin.Checked = True Then
                SQL = SQL & ",1  "
            Else
                SQL = SQL & ",2  "
            End If
            SQL = SQL & " ) "
        Else
            SQL = "UPDATE USERS SET "
            SQL = SQL & "  USERNAME='" & ConvertNullToString(txtUserName.Text) & "'"
            SQL = SQL & " ,PASSWORDS='" & ConvertNullToString(txtPassword.Text) & "'"
            SQL = SQL & " ,DISPLAYNAME='" & ConvertNullToString(txtDisplayName.Text) & "'"
            SQL = SQL & " ,REMARK='" & ConvertNullToString(txtRemark.Text) & "'"
            If rdoAdmin.Checked = True Then
                SQL = SQL & ",USERLEVEL=1  "
            Else
                SQL = SQL & ",USERLEVEL=2  "
            End If
            SQL = SQL & " ,UPDATEBY='" & gUserName & "'"
            SQL = SQL & " ,UPDATEDATE=" & formatSQLDate(Now)
            SQL = SQL & " WHERE USERID=" & mID
        End If

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

        'Call InitialForm()
        If mMode = Mode.AddNew Then

        ElseIf mMode = Mode.Edit Or mMode = Mode.Preview Then
            LoadData()
        End If
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
        SQL = "SELECT * FROM USERS"
        SQL = SQL & " WHERE USERID=" & mID

        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows.Count > 0 Then
            Me.txtUserName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("USERNAME"))
            Me.txtPassword.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("PASSWORDS"))
            Me.txtDisplayName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("DISPLAYNAME"))
            Me.txtRemark.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("REMARK"))
            If ConvertNullToZero(ds.Tables("Data").Rows(0).Item("USERLEVEL")) = 1 Then
                rdoAdmin.Checked = True
            Else
                rdoUser.Checked = True
            End If
        End If
LineExit:
    End Sub

     
    Private Function Verify() As Boolean
        Verify = False

        If ConvertNullToString(Me.txtUserName.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtUserName.Focus()
            Exit Function
        End If
        If ConvertNullToString(Me.txtPassword.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtPassword.Focus()
            Exit Function
        End If
     
        If CheckExistKey(Trim(Me.txtUserName.Text)) Then
            MsgBox("ข้อมูลนี้มีอยู่ในระบบแล้ว", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtUserName.Focus()
            Me.txtUserName.SelectAll()
            Exit Function
        End If

        Return True
    End Function


    Private Function CheckExistKey(ByVal pKey As String) As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        SQL = "SELECT USERID FROM USERS "
        SQL = SQL & " WHERE USERNAME='" & Trim(pKey) & "' and ISDELETE='N' "
        If mMode = Mode.Edit Then SQL = SQL & " AND  USERID <>" & mID

        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows.Count > 0 Then
            CheckExistKey = True
        Else
            CheckExistKey = False
        End If
LineExit:
    End Function


    Private Sub GenNewID()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        SQL = "select max(USERID) as HOUSEID from USERS "
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows(0).Item("USERID") Is DBNull.Value Then
            mID = 1
        Else
            mID = ds.Tables("Data").Rows(0).Item("USERID") + 1
        End If
LineExit:
    End Sub
     
End Class
