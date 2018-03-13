Public Class clsControlUser
    Private mblnRunning As Boolean
    Private WithEvents mCtlForm As frmControl
    Dim mID As String

    Public Property Running() As Boolean
        Get
            Running = mblnRunning
            If mblnRunning Then mCtlForm.Focus()
        End Get
        Set(ByVal value As Boolean)
            mblnRunning = value
        End Set
    End Property

    Public Sub Execute()
        On Error GoTo LineError
        'Initial Form Sector
        Me.Running = True
        mCtlForm = New frmControl
        With mCtlForm
            .Text = "ข้อมูลผู้ใช้งาน"
            .MdiParent = frmMain
            .btnDelete.Enabled = False
            .Show()
        End With
        Call LoadData()
LineExit:
        Exit Sub
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
    End Sub

    Private Sub LoadData()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        If ConnectStatus Then
            SQL = "SELECT USERID,USERNAME,DISPLAYNAME "
            'SWITCH(is_enable=True,'Yes',is_enable=False,'No')
            SQL = SQL & " ,SWITCH (USERLEVEL =1 , 'ผู้ดูแลระบบ' , USERLEVEL =2, 'ผู้ใช้งาน' ) as USERLEVEL "
            SQL = SQL & " FROM USERS "
            SQL = SQL & " WHERE ISDELETE = 'N' "
            SQL = SQL & " ORDER BY USERID"
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                mCtlForm.grdData.DataSource = ds.Tables("Data")
                Call GridStyle()
            Else
                mCtlForm.grdData.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub GridStyle()
        With mCtlForm.grdData
            .Columns("USERID").Width = 0

            .Columns("USERNAME").HeaderText = "ชื่อผู้ใช้งาน"
            .Columns("USERNAME").Width = 100

            .Columns("DISPLAYNAME").HeaderText = "ชื่อเพื่อแสดง"
            .Columns("DISPLAYNAME").Width = 200

            .Columns("USERLEVEL").HeaderText = "ระดับการใช้งาน"
            .Columns("USERLEVEL").Width = 200
 
        End With
    End Sub

    '' Form Events
    Private Sub mCtlForm_AddNew() Handles mCtlForm.AddNew
        Dim frmEdit As New frmEditUser
        On Error GoTo LineError
        'Begin work
        frmEdit.ModeRun = Mode.AddNew
        frmEdit.Text = "ข้อมูลผู้ใช้งาน [เพิ่ม]"
        frmEdit.ShowDialog()
        Call LoadData()
LineExit:
        frmEdit = Nothing
        Exit Sub
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        Resume LineExit
    End Sub

    Private Sub mCtlForm_Edit() Handles mCtlForm.Edit
        Dim frmEdit As New frmEditUser
        On Error GoTo LineError
        'Begin work
        frmEdit.ModeRun = Mode.Edit
        frmEdit.ID = mID
        frmEdit.Text = "ข้อมูลผู้ใช้งาน [แก้ไข]"
        frmEdit.ShowDialog()
        Call LoadData()
LineExit:
        frmEdit = Nothing
        Exit Sub
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        Resume LineExit
    End Sub

    Private Sub mCtlForm_Delete() Handles mCtlForm.Delete
        Dim SQL As String
        Dim lBeginTX As Integer = 0
        Dim lTr As OleDb.OleDbTransaction
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()
        On Error GoTo LineError

        If ConvertNullToString(mID) = "" Then
            MsgBox("กรุณาเลือกข้อมูลที่ต้องการลบ", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            GoTo LineExit
        ElseIf MsgBox("ยืนยันการลบข้อมูล ?", MsgBoxStyle.Question + MessageBoxButtons.YesNo, "ยืนยัน") = MsgBoxResult.Yes Then

            If Not ConnectStatus Then GoTo LineExit 'Check Connection
            If CheckIsRelation(ConvertNullToZero(mID)) Then GoTo LineExit 'Check Relation from table Items
            SQL = "UPDATE USERS SET ISDELETE ='Y' WHERE USERID=" & ConvertNullToString(mID)
            lTr = gConnection.BeginTransaction()
            lBeginTX = 1
            With lCom
                .Connection = gConnection
                .Transaction = lTr
                .CommandText = SQL
                .ExecuteNonQuery()
            End With
            lTr.Commit()
            Call LoadData()
        End If
LineExit:
        Exit Sub
LineError:
        If lBeginTX > 0 Then lTr.Rollback()
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
    End Sub

    Private Sub mCtlForm_FormUnload() Handles mCtlForm.FormUnload
        mCtlForm = Nothing
        Me.Running = False
    End Sub


    Private Function CheckIsRelation(ByVal pKey As Long) As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        Dim lIsRelate As Boolean = False

        'If ConnectStatus Then
        '    SQL = "SELECT ITEMID,ITEMCODE,ITEMDESC FROM ITEM"
        '    SQL = SQL & " WHERE ITEMTYPE=1 "
        '    SQL = SQL & " ORDER BY ITEMCODE"
        '    da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        '    da.Fill(ds, "Data")
        '    If ds.Tables("Data").Rows.Count > 0 Then
        '        MsgBox("ไม่สามารถลบข้อมูลนี้ได้ เนื่องจากมีการเรียกใช้งานอยู่", MsgBoxStyle.Critical + MessageBoxButtons.OK, "การตรวจสอบ")
        '        lIsRelate = True
        '    End If
        'End If

        Return lIsRelate
    End Function

    Private Sub mCtlForm_SetID(ByVal pID As String) Handles mCtlForm.SetID
        mID = pID
    End Sub
End Class
