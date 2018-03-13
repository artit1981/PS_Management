Public Class frmProcessMonthly

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        InsertData()
    End Sub

    Protected Sub InsertData()
        Dim SQL As String
        Dim lBeginTX As Integer = 0, i As Long = 0, lCount As Long = 0, lTxId As Long = 0, lSEQ As Integer = 1
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        Dim lPrice1 As Decimal = 0, lPrice2 As Decimal = 0, lExpireDay As Long = 0, lExpireDate As Date
        Dim lTr As OleDb.OleDbTransaction = Nothing
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()
        Dim ldtpCurrentDate As Date = DateSerial(Me.cboYear.SelectedItem, Me.cboMonth.SelectedIndex + 1, 1)
        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection

        If CheckIsProceeded() = False Then


            'Get Price
            SQL = "SELECT HOUSEPRICE1,HOUSEPRICE2,EXPIREDAY  FROM PROJECTCONFIG"
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                lPrice1 = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSEPRICE1"))
                lPrice2 = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("HOUSEPRICE2"))
                lExpireDay = ConvertNullToZero(ds.Tables("Data").Rows(0).Item("EXPIREDAY"))
            End If

            'Get House
            SQL = "SELECT HOUSEID,HOUSENO,OWNERNAME,OWNERPHONE,RESIDENTNAME,RESIDENTPHONE,HOUSETYPE,HOUSEPERIOD,REMARK FROM HOUSE"
            SQL = SQL & " WHERE ISDELETE = 'N' "
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "Data")
            lCount = ds.Tables("Data").Rows.Count
            If lCount > 0 Then
                lTxId = GenNewID()

                'Begin Trans.
                lTr = gConnection.BeginTransaction()
                lBeginTX = 1

                ProgressBar1.Visible = True
                lExpireDate = DateAdd(DateInterval.Day, lExpireDay, ldtpCurrentDate)
                For i = 0 To lCount - 1
                    RunProgress(i + 1, lCount)
                    SQL = "INSERT INTO HOUSETX(HOUSETXID,HOUSEID,TXPERIOD,SEQNO,TXNAME,QTY,PRICE,TXAMOUNT,EXPIREDATE,ISPAY,PAYMENTTXID,PAYDATE,CREATEBY,CREATEDATE )"
                    SQL = SQL & " VALUES( "
                    SQL = SQL & "  " & lTxId
                    SQL = SQL & " ," & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSEID"))
                    SQL = SQL & " ," & formatSQLDate(ldtpCurrentDate)
                    SQL = SQL & " ," & lSEQ
                    SQL = SQL & " ,'ค่าใช้จ่ายส่วนกลาง เดือน" & Format(ldtpCurrentDate, "MMMM yyyy") & "'"
                    SQL = SQL & " ,1" 'QTY
                    If ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSETYPE")) = 1 Then
                        SQL = SQL & " ," & lPrice1
                    Else
                        SQL = SQL & " ," & lPrice2
                    End If
                    If ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSETYPE")) = 1 Then
                        SQL = SQL & " ," & lPrice1
                    Else
                        SQL = SQL & " ," & lPrice2
                    End If
                    SQL = SQL & " ," & formatSQLDate(lExpireDate)
                    SQL = SQL & " ,'N' "    'ISPAY
                    SQL = SQL & " ,0 "      'PAYMENTTXID
                    SQL = SQL & " ,Null "   'PAYDATE
                    SQL = SQL & " ,'" & gUserName & "'"
                    SQL = SQL & " ," & formatSQLDate(Now)
                    SQL = SQL & " ) "

                    With lCom
                        .Connection = gConnection
                        .Transaction = lTr
                        .CommandText = SQL
                        .ExecuteNonQuery()
                    End With
                    lTxId = lTxId + 1
                Next
                lTr.Commit()
            End If


            MsgBox("การประมวลผลข้อมูลเสร็จสิ้น", MsgBoxStyle.Information + MessageBoxButtons.OK, "การประมวลผล")
            ProgressBar1.Visible = False
        End If
LineExit:
        Exit Sub
LineError:
        If lBeginTX > 0 Then lTr.Rollback()
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

    End Sub

    Private Function CheckIsProceeded() As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        Dim lBeginTX As Integer = 0
        Dim lTr As OleDb.OleDbTransaction
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()

        Dim lIsRelate As Boolean = False
        Dim ldtpCurrentDate As Date = DateSerial(Me.cboYear.SelectedItem, Me.cboMonth.SelectedIndex + 1, 1)
        If ConnectStatus Then
            'Check IS PAY
            SQL = "SELECT HOUSEID FROM HOUSETX"
            SQL = SQL & " WHERE month(TXPERIOD)= " & Me.cboMonth.SelectedIndex + 1
            SQL = SQL & " AND year(TXPERIOD)= " & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
            SQL = SQL & " AND ISPAY='Y' "
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                MsgBox("พบข้อมูลการประมวลผลของ งวดเดือน" & Format(ldtpCurrentDate, "MMMM yyyy") & " มีการชำระเงินแล้ว ไม่สามารถประมวลผลใหม่ได้", MsgBoxStyle.Critical + MessageBoxButtons.OK, "การตรวจสอบ")
                Return True
            End If

            SQL = "SELECT HOUSEID FROM HOUSETX"
            SQL = SQL & " WHERE month(TXPERIOD)= " & Me.cboMonth.SelectedIndex + 1
            SQL = SQL & " AND year(TXPERIOD)= " & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                If MsgBox("พบข้อมูลการประมวลผลของ งวดเดือน" & Format(ldtpCurrentDate, "MMMM yyyy") & " ต้องการดำเนินการต่อหรือไม่", MsgBoxStyle.Question + MessageBoxButtons.YesNo, "การตรวจสอบ") = MsgBoxResult.No Then
                    lIsRelate = True
                Else
                    SQL = "DELETE FROM HOUSETX   "
                    SQL = SQL & " WHERE month(TXPERIOD)= " & Me.cboMonth.SelectedIndex + 1
                    SQL = SQL & " AND year(TXPERIOD)= " & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
                    lTr = gConnection.BeginTransaction()
                    lBeginTX = 1
                    With lCom
                        .Connection = gConnection
                        .Transaction = lTr
                        .CommandText = SQL
                        .ExecuteNonQuery()
                    End With
                    lTr.Commit()
                End If
            End If
        End If

        Return lIsRelate
    End Function



    Private Function GenNewID() As Long
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        SQL = "select max(HOUSETXID) as HOUSETXID from HOUSETX "
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")

        If ds.Tables("Data").Rows(0).Item("HOUSETXID") Is DBNull.Value Then
            GenNewID = 1
        Else
            GenNewID = ds.Tables("Data").Rows(0).Item("HOUSETXID") + 1
        End If
LineExit:
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmProcessMonthly_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ProgressBar1.Visible = False

        Dim i As Integer
        For i = 1 To 12
            cboMonth.Items.Add(GetMonthString(i ))
        Next i

        For i = Year(Now) - 5 To Year(Now) + 5
            cboYear.Items.Add(i)
        Next i

        Dim lMonth As Integer = Month(Now)
        If lMonth < 12 Then
            lMonth = lMonth + 1
        End If

        cboMonth.SelectedItem = GetMonthString(lMonth)
        cboYear.Text = Year(Now)
    End Sub

    Private Sub RunProgress(ByVal pProgress As Long, ByVal pCount As Long)
        Dim lCurrent As Long = ((pProgress * 100) / pCount)
        ProgressBar1.Value = lCurrent
    End Sub
End Class