Option Explicit On
Public Class clsControlHouseInfo
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
            .Text = "ข้อมูลบ้าน"
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
            SQL = "SELECT HOUSEID,HOUSENO,OWNERNAME,OWNERPHONE,SOI" ',HOUSEPERIOD,HOUSEPAYAMOUNT "
            'SWITCH(is_enable=True,'Yes',is_enable=False,'No')
            SQL = SQL & " ,SWITCH (HOUSETYPE =1 , 'ทาวน์เฮ้าส์ 3' , HOUSETYPE =2 , 'ทาวน์เฮ้าส์ 4' ,HOUSETYPE =3, 'บ้านแฝด' ) as HOUSETYPE "
            SQL = SQL & " FROM HOUSE "
            SQL = SQL & " WHERE ISDELETE = 'N' "
            SQL = SQL & " ORDER BY HOUSEID"
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
            .Columns("HOUSEID").Width = 0

            .Columns("HOUSENO").HeaderText = "บ้านเลขที่"
            .Columns("HOUSENO").Width = 100

            .Columns("OWNERNAME").HeaderText = "เจ้าบ้าน"
            .Columns("OWNERNAME").Width = 200

            .Columns("OWNERPHONE").HeaderText = "เบอร์โทร"
            .Columns("OWNERPHONE").Width = 200

            .Columns("HOUSETYPE").HeaderText = "ประเภทบ้าน"
            .Columns("HOUSETYPE").Width = 200

            .Columns("SOI").HeaderText = "ซอย"
            .Columns("SOI").Width = 80
        End With
    End Sub

    '' Form Events
    Private Sub mCtlForm_AddNew() Handles mCtlForm.AddNew
        Dim frmEdit As New frmEditHouseInfo
        On Error GoTo LineError
        'Begin work
        frmEdit.ModeRun = Mode.AddNew
        frmEdit.Text = "ข้อมูลบ้าน [เพิ่ม]"
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
        Dim frmEdit As New frmEditHouseInfo
        On Error GoTo LineError
        'Begin work
        frmEdit.ModeRun = Mode.Edit
        frmEdit.ID = mID
        frmEdit.Text = "ข้อมูลบ้าน [แก้ไข]"
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
            SQL = "UPDATE HOUSE SET ISDELETE ='Y' WHERE HOUSEID=" & ConvertNullToString(mID)
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
