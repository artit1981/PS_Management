Public Class frmPayment
    Private mTXList As List(Of Long)
    Dim mMode As Mode
    Dim mIDs As Long
    Dim mHouseID As Long = 0
    
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
            ID = mIDs
        End Get
        Set(ByVal value As Long)
            mIDs = value
        End Set
    End Property

    Private Sub frmPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo LineError

        'Dim i As Integer
        'For i = 1 To 12
        '    cboMonth.Items.Add(GetMonthString(i))
        'Next i

        'For i = Year(Now) - 5 To Year(Now) + 5
        '    cboYear.Items.Add(i)
        'Next i
        'cboMonth.SelectedItem = GetMonthString(Month(Now))
        'cboYear.Text = Year(Now)
        Call GenNewID()
        btnPrint.Enabled = False

        mTXList = Nothing
        mTXList = New List(Of Long)

        Exit Sub
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        Me.Close()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim SQL As String = ""
        Dim lBeginTX As Integer = 0
        Dim lTr As OleDb.OleDbTransaction = Nothing
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()

        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection
        If Not Verify() Then GoTo LineExit 'Check Data

        If mMode = Mode.AddNew Then
            'Call GenNewID()
            lTr = gConnection.BeginTransaction()
            lBeginTX = 1

            SQL = "INSERT INTO PAYMENTTX(PAYMENTTXID,PAYMENTCODE,HOUSEID,PAYDATE,TOTALAMOUNT,PAYTYPE,PAYREMARK,CREATEBY,CREATEDATE)" '1=Bank   2=Cash
            SQL = SQL & " VALUES( "
            SQL = SQL & "  " & mIDs
            SQL = SQL & " ,'" & ConvertNullToString(txtPaymentCode.Text) & "'"
            SQL = SQL & " ," & mHouseID
            SQL = SQL & " ," & formatSQLDate(PAYDATE.Value)
            SQL = SQL & " ," & ConvertNullToZero(PAYAMOUNT.Text) 
            If rdoBank.Checked = True Then
                SQL = SQL & ",1  "
            Else
                SQL = SQL & ",2  "
            End If
            SQL = SQL & " ,'" & ConvertNullToString(PAYREMARK.Text) & "'"
            SQL = SQL & " ,'" & gUserName & "'"
            SQL = SQL & " ," & formatSQLDate(Now)
            SQL = SQL & " ) "
            With lCom
                .Connection = gConnection
                .Transaction = lTr
                .CommandText = SQL
                .ExecuteNonQuery()
            End With

            For Each pHouseTXId In mTXList
                If pHouseTXId > 0 Then
                    SQL = " update HOUSETX Set "
                    SQL = SQL & " ISPAY='Y' "
                    SQL = SQL & " ,PAYMENTTXID=" & mIDs
                    SQL = SQL & " ,PAYDATE=" & formatSQLDate(PAYDATE.Value)
                    SQL = SQL & " WHERE HOUSETXID=" & pHouseTXId
                    With lCom
                        .Connection = gConnection
                        .Transaction = lTr
                        .CommandText = SQL
                        .ExecuteNonQuery()
                    End With
                End If
            Next
        End If
        lTr.Commit()
        MsgBox("การบันทึกข้อมูลเสร็จสิ้น", MsgBoxStyle.Information + MessageBoxButtons.OK, "การบันทึก")
        btnPrint.Enabled = True
        btnSave.Enabled = False
        btnFind.Enabled = False
        'If MsgBox("การบันทึกข้อมูลเสร็จสิ้น ต้องการพิมพ์ใบเสร็จหรือไม่", MsgBoxStyle.Information + MessageBoxButtons.YesNo, "การบันทึก") = MsgBoxResult.Yes Then
        '    Me.Cursor = Cursors.WaitCursor
        '    Dim lclsPrint As New clsReceipt
        '    lclsPrint.PrintReceipt(mIDs)
        '    Me.Cursor = Cursors.Default
        'End If
        'Me.Close()
LineExit:
        Exit Sub
LineError:
        If lBeginTX > 0 Then lTr.Rollback()
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

    End Sub

    Protected Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        LoadHouseData(0, txtHouseNo.Text, True)
    End Sub

    Private Sub txtHouseNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHouseNo.KeyPress
        If e.KeyChar = Chr(13) Then
            If ConvertNullToString(txtHouseNo.Text) = "" Then
                LoadHouseData(0, txtHouseNo.Text, True)
            Else
                LoadHouseData(0, txtHouseNo.Text, False)
            End If

        End If
    End Sub


    Private Function Verify() As Boolean
        Verify = False
        If mTXList.Count = 0 Then
            MsgBox("ไม่พบรายการชำระ", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.PAYAMOUNT.Focus()
            Exit Function
        End If

        If ConvertNullToString(Me.txtPaymentCode.Text) = "" Then
            MsgBox("กรุณาระบุเลขที่ใบเสร็จ", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtPaymentCode.Focus()
            Exit Function
        ElseIf CheckExistKey(Trim(Me.txtPaymentCode.Text)) Then
            MsgBox("เลขที่ใบเสร็จซ้ำ", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.txtPaymentCode.Focus()
            Me.txtPaymentCode.SelectAll()
            Exit Function
        End If
        If ConvertNullToString(Me.PAYAMOUNT.Text) = "" Then
            MsgBox("กรุณาระบุยอดชำระ", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.PAYAMOUNT.Focus()
            Exit Function
        ElseIf IsNumeric(Me.PAYAMOUNT.Text) = False Then
            MsgBox("กรุณาระบุข้อมูลเป็นตัวเลข", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.PAYAMOUNT.Focus()
            Exit Function
        ElseIf ConvertNullToZero(Me.PAYAMOUNT.Text) <= 0 Then
            MsgBox("กรุณาระบุยอดชำระ", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Me.PAYAMOUNT.Focus()
            Exit Function
        End If


        Return True
    End Function


    Private Function CheckExistKey(ByVal pKey As String) As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        SQL = "SELECT PAYMENTCODE FROM PAYMENTTX "
        SQL = SQL & " WHERE PAYMENTCODE='" & Trim(pKey) & "' "
        
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

        SQL = "select max(PAYMENTTXID) as PAYMENTTXID from PAYMENTTX "
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows(0).Item("PAYMENTTXID") Is DBNull.Value Then
            mIDs = 1
        Else
            mIDs = ds.Tables("Data").Rows(0).Item("PAYMENTTXID") + 1
        End If

        txtPaymentCode.Text = Format(Now, "yy") & "-" & Format(mIDs, "000000")
LineExit:
    End Sub

    Private Sub LoadHouseData(ByVal pHouseID As Long, ByVal pHouseNo As String, ByVal pForceShowFind As Boolean)
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        If Not ConnectStatus Then GoTo LineExit

        If pForceShowFind = True Then
            pHouseNo = "XXXXX"
        End If
        SQL = "SELECT HOUSEID,HOUSENO,OWNERNAME "
        SQL = SQL & " FROM HOUSE "
        SQL = SQL & " WHERE ISDELETE = 'N' "
        If pHouseNo <> "" Then
            SQL = SQL & " AND HOUSENO = '" & pHouseNo & "' "
        End If
        If pHouseID > 0 Then
            SQL = SQL & " AND HOUSEID = " & pHouseID
        End If

        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows.Count > 0 Then
            txtName.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("OWNERNAME"))
            txtHouseNo.Text = ConvertNullToString(ds.Tables("Data").Rows(0).Item("HOUSENO"))
            mHouseID = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSEID"))
            grdData.DataSource = Nothing
            CalcAmount()
        Else
            pHouseID = ShowFormFind()
            If pHouseID > 0 Then
                Call LoadHouseData(pHouseID, "", False)
            End If
        End If

LineExit:
    End Sub

    Private Sub LoadHouseTX()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        Dim lHouseTXList As String = ""

        If Not ConnectStatus Then GoTo LineExit
        If mTXList.Count > 0 Then
            For Each pTxID As Long In mTXList
                If lHouseTXList = "" Then
                    lHouseTXList = pTxID.ToString
                Else
                    lHouseTXList = lHouseTXList & "," & pTxID.ToString
                End If
            Next

            SQL = "SELECT  HOUSETXID,TXPERIOD,TXAMOUNT FROM HOUSETX"
            SQL = SQL & " WHERE ISPAY='N' "
            SQL = SQL & " AND HOUSETXID in (" & lHouseTXList & ")"
            SQL = SQL & " Order by TXPERIOD"

            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                grdData.DataSource = ds.Tables("Data")
                Call GridStyle()
            Else
                grdData.DataSource = Nothing
            End If
        End If
        CalcAmount()
LineExit:
    End Sub

    Private Sub CalcAmount()
        Dim lTotal As Decimal = 0, lRow As Long = 0
        Try
            mTXList = New List(Of Long)
              
            For lRow = 0 To grdData.Rows.Count - 1
                lTotal = lTotal + ConvertNullToZero(grdData.Rows(lRow).Cells("TXAMOUNT").Value)
                mTXList.Add(ConvertNullToZero(grdData.Rows(lRow).Cells("HOUSETXID").Value))
            Next
            PAYAMOUNT.Text = Format(lTotal, "#,##0.00")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridStyle()
        With grdData
            .Columns("HOUSETXID").Visible = False

            .Columns("TXPERIOD").HeaderText = "งวดชำระ"
            .Columns("TXPERIOD").Width = 150
            .Columns("TXPERIOD").DefaultCellStyle.Format = "MMMM yyyy"
            .Columns("TXPERIOD").ReadOnly = True

            .Columns("TXAMOUNT").HeaderText = "ยอดเงิน"
            .Columns("TXAMOUNT").Width = 180
            .Columns("TXAMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TXAMOUNT").DefaultCellStyle.Format = "#,##0.00"
            .Columns("TXAMOUNT").ReadOnly = True
 
        End With
    End Sub
    Private Function ShowFormFind() As Long
        Dim lfrmFind As New frmFindHouse
        lfrmFind.ShowDialog()
        Return lfrmFind.KeyID
LineExit:
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click


        Dim lResult As MsgBoxResult, lIsPrintCoppy As String = ""

        lResult = MsgBox("ต้องการพิมพ์สำเนาใบเสร็จหรือไม่", MsgBoxStyle.Information + MessageBoxButtons.YesNoCancel, "Information")
        If lResult = MsgBoxResult.Yes Then
            lIsPrintCoppy = "Y"
        ElseIf lResult = MsgBoxResult.No Then
            lIsPrintCoppy = "N"
        End If

        If lIsPrintCoppy <> "" Then
            Me.Cursor = Cursors.WaitCursor
            Dim lclsPrint As New clsReceipt
            lclsPrint.PrintReceipt(mIDs, lIsPrintCoppy)
        End If

        Me.Cursor = Cursors.Default

    End Sub
     
    Private Sub btnFindHouseTX_Click(sender As System.Object, e As System.EventArgs) Handles btnFindHouseTX.Click
        Dim lfrmTX As New frmFindHouseTX

        mTXList = Nothing
        mTXList = New List(Of Long)
        If mHouseID > 0 Then
            lfrmTX.HouseNo = txtHouseNo.Text
            lfrmTX.HouseID = mHouseID
            lfrmTX.ShowDialog()

            mTXList = lfrmTX.KeyID
            If Not IsNothing(mTXList) Then
                LoadHouseTX()
            End If

        Else
            MessageBox.Show("กรุณาระบุบ้านเลือกที่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            grdData.DataSource = Nothing
        End If

    End Sub
End Class
