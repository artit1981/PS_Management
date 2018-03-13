Public Class frmEditHouseInfo

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
            SQL = "INSERT INTO HOUSE(HOUSEID,HOUSENO,LANDNO,OWNERNAME,OWNERPHONE,RESIDENTNAME,RESIDENTPHONE "
            SQL = SQL & " ,HOUSETYPE,HOUSEPERIOD,REMARK,ISDELETE,CREATEBY,CREATEDATE)"
            SQL = SQL & " VALUES( "
            SQL = SQL & "  " & mID & ""
            SQL = SQL & " ,'" & ConvertNullToString(txtHouseNo.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtLandNo.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtOwnerName.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtOwnerPhone.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtResidentName.Text) & "'"
            SQL = SQL & " ,'" & ConvertNullToString(txtResidentPhone.Text) & "'"
            If rdoHouse1.Checked = True Then
                SQL = SQL & ",1  "
            Else
                SQL = SQL & ",2  "
            End If
            SQL = SQL & " ," & ConvertNullToZero(txtPeroid.Text)
            SQL = SQL & " ,'" & ConvertNullToString(txtRemark.Text) & "'"
            SQL = SQL & " , 'N'  "
            SQL = SQL & " ,'" & gUserName & "'"
            SQL = SQL & " ," & formatSQLDate(Now)
            SQL = SQL & " ) "
        Else
            SQL = "UPDATE HOUSE SET "
            SQL = SQL & "  HOUSENO='" & ConvertNullToString(txtHouseNo.Text) & "'"
            SQL = SQL & " ,LANDNO='" & ConvertNullToString(txtLandNo.Text) & "'"
            SQL = SQL & " ,OWNERNAME='" & ConvertNullToString(txtOwnerName.Text) & "'"
            SQL = SQL & " ,OWNERPHONE='" & ConvertNullToString(txtOwnerPhone.Text) & "'"
            SQL = SQL & " ,RESIDENTNAME='" & ConvertNullToString(txtResidentName.Text) & "'"
            SQL = SQL & " ,RESIDENTPHONE='" & ConvertNullToString(txtResidentPhone.Text) & "'"
            If rdoHouse1.Checked = True Then
                SQL = SQL & ",HOUSETYPE=1  "
            Else
                SQL = SQL & ",HOUSETYPE=2  "
            End If
            SQL = SQL & " ,HOUSEPERIOD=" & ConvertNullToZero(txtPeroid.Text)
            SQL = SQL & " ,REMARK='" & ConvertNullToString(txtRemark.Text) & "'"
            SQL = SQL & " ,UPDATEBY='" & gUserName & "'"
            SQL = SQL & " ,UPDATEDATE=" & formatSQLDate(Now)
            SQL = SQL & " WHERE HOUSEID=" & mID
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
            txtPeroid.Text = "1"
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
        SQL = "SELECT HOUSEID,HOUSENO,LANDNO,OWNERNAME,OWNERPHONE,RESIDENTNAME,RESIDENTPHONE,HOUSETYPE,HOUSEPERIOD,REMARK FROM HOUSE"
        SQL = SQL & " WHERE HOUSEID=" & mID

        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows.Count > 0 Then
            Me.txtHouseNo.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("HOUSENO"))
            Me.txtLandNo.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("LANDNO"))
            Me.txtOwnerName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("OWNERNAME"))
            Me.txtOwnerPhone.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("OWNERPHONE"))
            Me.txtResidentName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("RESIDENTNAME"))
            Me.txtResidentPhone.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("RESIDENTPHONE"))
            Me.txtPeroid.Text = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSEPERIOD"))
            Me.txtRemark.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("REMARK"))
            If ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSETYPE")) = 1 Then
                rdoHouse1.Checked = True
            Else
                rdoHouse2.Checked = True
            End If
        End If
LineExit:
    End Sub


    Private Sub LoadPriceData()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet


        If Not ConnectStatus Then GoTo LineExit
        SQL = "SELECT HOUSEPRICE1,HOUSEPRICE2  FROM PROJECTCONFIG"

       
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows.Count > 0 Then
            If rdoHouse1.Checked = True Then
                txtAmount.Text = Format(ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSEPRICE1")), "#,##0.00")
            Else
                txtAmount.Text = Format(ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSEPRICE2")), "#,##0.00")
            End If
        End If
LineExit:
    End Sub

    Private Function Verify() As Boolean
        Verify = False

        If ConvertNullToString(Me.txtHouseNo.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtHouseNo.Focus()
            Exit Function
        End If

        If ConvertNullToString(Me.txtPeroid.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtPeroid.Focus()
            Exit Function
        ElseIf IsNumeric(Me.txtPeroid.Text) = False Then
            MsgBox("กรุณาระบุข้อมูลเป็นตัวเลข", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtPeroid.Focus()
            Exit Function
        End If

        If ConvertNullToString(Me.txtAmount.Text) = "" Then
            MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtAmount.Focus()
            Exit Function
        ElseIf IsNumeric(Me.txtAmount.Text) = False Then
            MsgBox("กรุณาระบุข้อมูลเป็นตัวเลข", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtAmount.Focus()
            Exit Function
        End If

        If CheckExistKey(Trim(Me.txtHouseNo.Text)) Then
            MsgBox("ข้อมูลนี้มีอยู่ในระบบแล้ว", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtHouseNo.Focus()
            Me.txtHouseNo.SelectAll()
            Exit Function
        End If

        Return True
    End Function


    Private Function CheckExistKey(ByVal pKey As String) As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        SQL = "SELECT HOUSENO FROM HOUSE "
        SQL = SQL & " WHERE HOUSENO='" & Trim(pKey) & "' and ISDELETE='N' "
        If mMode = Mode.Edit Then SQL = SQL & " AND  HOUSEID <>" & mID

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

        SQL = "select max(HOUSEID) as HOUSEID from HOUSE "
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows(0).Item("HOUSEID") Is DBNull.Value Then
            mID = 1
        Else
            mID = ds.Tables("Data").Rows(0).Item("HOUSEID") + 1
        End If
LineExit:
    End Sub

    Private Sub rdoHouse1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoHouse1.CheckedChanged
        LoadPriceData()
    End Sub

    Private Sub rdoHouse2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoHouse2.CheckedChanged
        LoadPriceData()
    End Sub
End Class
