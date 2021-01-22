Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPrintBill
    Dim mHouseID As Long = 0

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        InsertData()
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
        Else
            pHouseID = ShowFormFind()
            If pHouseID > 0 Then
                Call LoadHouseData(pHouseID, "", False)
            End If
        End If

LineExit:
    End Sub

    Private Function ShowFormFind() As Long
        Dim lfrmFind As New frmFindHouse
        lfrmFind.ShowDialog()
        Return lfrmFind.KeyID
LineExit:
    End Function
    Protected Sub InsertData()
        Dim SQL As String, lCount As Long = 0, lSEQ As Long = 1, lHouseID As Long = 0
        Dim da As OleDb.OleDbDataAdapter
        Dim dt As DataTable
        Dim ds As DataSet = New DataSet
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()
        Dim ldtpCurrentDate As Date = DateSerial(Me.cboYear.SelectedItem, Me.cboMonth.SelectedIndex + 1, 1)
        Dim lstrPeriod As String = "", lstrExprire As String = "", lTxDate As Date, lConDate As Date
        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection

        'lConDate = DateSerial(Year(ds.Tables("Data").Rows(i).Item("TXPERIOD")), Month(ds.Tables("Data").Rows(i).Item("TXPERIOD")), 1)

        If CheckIsProceeded() = True Then
            Me.Cursor = Cursors.WaitCursor
            SQL = "SELECT HOUSE.HOUSEID,HOUSE.HOUSENO,HOUSE.LANDNO,HOUSE.SOI"
            SQL = SQL & " ,HOUSE.OWNERNAME,HOUSETX.TXAMOUNT,HOUSETX.SEQNO "
            SQL = SQL & " ,HOUSETX.TXNAME as TXNAME,HOUSETX.QTY,HOUSETX.PRICE "
            SQL = SQL & " ,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION"
            SQL = SQL & " ,HOUSETX.TXPERIOD as TXPERIOD ,HOUSETX.EXPIREDATE as EXPIREDATE,PROJECTCONFIG.EXPIREDAY "
            SQL = SQL & " FROM HOUSE,HOUSETX,PROJECTCONFIG "
            SQL = SQL & " WHERE HOUSE.HOUSEID=HOUSETX.HOUSEID"
            'SQL = SQL & " AND MONTH(HOUSETX.TXPERIOD)<=" & Me.cboMonth.SelectedIndex + 1
            SQL = SQL & " AND YEAR(HOUSETX.TXPERIOD)<=" & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
            SQL = SQL & " AND HOUSE.ISDELETE='N'  AND HOUSETX.ISPAY='N'"
            If chkAll.CheckState = CheckState.Unchecked Then
                SQL = SQL & " AND HOUSE.HOUSEID=" & mHouseID
            End If
            'SQL = SQL & " GROUP BY HOUSE.HOUSEID,HOUSE.HOUSENO,HOUSETX.TXNAME,HOUSE.LANDNO,HOUSE.OWNERNAME,HOUSETX.TXAMOUNT"
            'SQL = SQL & " ,HOUSETX.SEQNO ,HOUSETX.QTY,HOUSETX.PRICE,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION"
            SQL = SQL & " ORDER BY HOUSE.HOUSEID,HOUSETX.TXPERIOD "
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "Data")
            lCount = ds.Tables(0).Rows.Count
            If lCount > 0 Then
                SQL = "DELETE FROM REPORTTX "
                With lCom
                    .Connection = gConnection
                    .CommandText = SQL
                    .ExecuteNonQuery()
                End With

                lstrPeriod = Format(ldtpCurrentDate, "MMMM yyyy")
                lstrExprire = Format(DateAdd(DateInterval.Day, ConvertNullToZero(ds.Tables("Data").Rows(0).Item("EXPIREDAY")), ldtpCurrentDate), "dd MMMM yyyy")

                For i = 0 To lCount - 1
                    lTxDate = DateSerial(Year(ds.Tables("Data").Rows(i).Item("TXPERIOD")), Month(ds.Tables("Data").Rows(i).Item("TXPERIOD")), 1)

                    If lTxDate <= ldtpCurrentDate Then
                        If ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSEID")) <> lHouseID Then
                            lSEQ = 1
                        Else
                            lSEQ = lSEQ + 1
                        End If

                        SQL = "INSERT INTO REPORTTX(HOUSEID,HOUSENO,LANDNO,OWNERNAME,TXAMOUNT,TXPERIOD,EXPIREDATE,SEQNO"
                        SQL = SQL & ",TXNAME,QTY ,PRICE,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION,REMARK,SOI )"
                        SQL = SQL & " VALUES( "
                        SQL = SQL & "  " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSEID"))
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("HOUSENO")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("LANDNO")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("OWNERNAME")) & "'"
                        SQL = SQL & " , " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("TXAMOUNT"))
                        SQL = SQL & " ,'" & Format(ds.Tables("Data").Rows(i).Item("TXPERIOD"), "MMMM yyyy") & "'"
                        SQL = SQL & " ,'" & Format(ds.Tables("Data").Rows(i).Item("EXPIREDATE"), "dd MMMM yyyy") & "'"
                        SQL = SQL & " , " & lSEQ
                        'SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("TXNAME")) & "'"
                        SQL = SQL & " ,'ค่าใช้จ่ายส่วนกลาง เดือน" & Format(ds.Tables("Data").Rows(i).Item("TXPERIOD"), "MMMM yyyy") & "'"
                        SQL = SQL & " , " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("QTY"))
                        SQL = SQL & " , " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("PRICE"))
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("PROJECTNAME")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("PROJECTADDRESS")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("BANKACCOUNT")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("SIGNNAME")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("SIGNPOSITION")) & "'"
                        SQL = SQL & " ,''"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("SOI")) & "'"
                        SQL = SQL & " ) "
                        lHouseID = ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSEID"))
                        With lCom
                            .Connection = gConnection
                            .CommandText = SQL
                            .ExecuteNonQuery()
                        End With
                    End If
                Next
            End If

            SQL = "SELECT HOUSEID,HOUSENO,LANDNO,OWNERNAME,TXAMOUNT,TXPERIOD,EXPIREDATE,SEQNO"
            SQL = SQL & ",TXNAME,QTY ,PRICE,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION,REMARK,SOI FROM REPORTTX "
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "DataTable")
            dt = ds.Tables(0)
            Dim rpt As New ReportDocument()
            Dim lDBPath As String = Application.StartupPath & "\rptBilling2.rpt"
            'Dim lDBPath As String = "D:\Develop\Development\PS_Management\PS_Management\rptBilling2.rpt"
            rpt.Load(lDBPath)
            rpt.SetDataSource(dt)
            rpt.SetParameterValue("Period", lstrPeriod)
            rpt.SetParameterValue("Expire", lstrExprire)
            frmShowReport.CrystalReportViewer1.ReportSource = rpt
            frmShowReport.CrystalReportViewer1.Refresh()
            Me.Cursor = Cursors.Default
            'frmShowReport.MdiParent = frmMain
            frmShowReport.Show()

        End If
LineExit:
        Exit Sub
LineError:
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

    End Sub

    Private Function CheckIsProceeded() As Boolean
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        Dim lBeginTX As Integer = 0

        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()

        Dim lIsRelate As Boolean = True
        Dim ldtpCurrentDate As Date = DateSerial(Me.cboYear.SelectedItem, Me.cboMonth.SelectedIndex + 1, 1)
        If ConnectStatus Then
            'Check IS PAY
            SQL = "SELECT HOUSEID FROM HOUSETX"
            'SQL = SQL & " WHERE month(TXPERIOD)<= " & Me.cboMonth.SelectedIndex + 1
            SQL = SQL & " WHERE year(TXPERIOD)<= " & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
            SQL = SQL & " AND ISPAY='N' "
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            ds = New DataSet
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count = 0 Then
                MsgBox(Me.cboMonth.SelectedIndex + 1 & ":" & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem))
                MsgBox("ไม่พบพบข้อมูลการประมวลผลของ งวดเดือน" & Format(ldtpCurrentDate, "MMMM yyyy"), MsgBoxStyle.Critical + MessageBoxButtons.OK, "การตรวจสอบ")
                Return False
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
     
        Dim i As Integer
        For i = 1 To 12
            cboMonth.Items.Add(GetMonthString(i))
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

        grpCond.Enabled = False
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckStateChanged
        grpCond.Enabled = chkAll.CheckState = CheckState.Unchecked
    End Sub

    
End Class