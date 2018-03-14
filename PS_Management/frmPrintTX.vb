Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPrintTX
    Dim mHouseID As Long = 0

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        InsertData()
    End Sub

     
    Protected Sub InsertData()
        Dim SQL As String, lCount As Long = 0
        Dim da As OleDb.OleDbDataAdapter
        Dim dt As DataTable
        Dim ds As DataSet = New DataSet
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()
        Dim lPay1 As Integer = 0, lPay2 As Integer = 0, lPay3 As Integer = 0, lPay4 As Integer = 0, lPay5 As Integer = 0, lPay6 As Integer = 0, lPayAll As Integer = 0
        Dim lNotPay1 As Integer = 0, lNotPay2 As Integer = 0, lNotPay3 As Integer = 0, lNotPay4 As Integer = 0 _
                , lNotPay5 As Integer = 0, lNotPay6 As Integer = 0, lNotPayAll As Integer = 0
        Dim lTotal1 As Integer = 0, lTotal2 As Integer = 0, lTotal3 As Integer = 0, lTotal4 As Integer = 0, lTotal5 As Integer = 0, lTotal6 As Integer = 0, lTotalAll As Integer = 0

        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection
         
        Me.Cursor = Cursors.WaitCursor

        Dim ldtpCurrentDate As Date = DateSerial(Me.cboYear.SelectedItem, Me.cboMonth.SelectedIndex + 1, 1)
        Dim lstrPeriod As String = Format(ldtpCurrentDate, "MMMM yyyy")


        SQL = "SELECT HOUSETX.HOUSETXID,HOUSE.HOUSENO ,HOUSE.OWNERNAME,SWITCH (HOUSETYPE =1 , 'ทาวน์เฮ้าส์ 3' ,HOUSETYPE =2 , 'ทาวน์เฮ้าส์ 4' , HOUSETYPE =3, 'บ้านแฝด' ) as HOUSETYPE  "
        SQL = SQL & " ,HOUSETX.TXAMOUNT,HOUSETX.ISPAY,HOUSETX.PAYDATE,HOUSE.SOI , PAYMENTTX.PAYMENTCODE"
        SQL = SQL & " FROM ( HOUSE  "
        SQL = SQL & " INNER JOIN HOUSETX ON HOUSE.HOUSEID=HOUSETX.HOUSEID )"
        SQL = SQL & " LEFT JOIN PAYMENTTX ON PAYMENTTX.PAYMENTTXID=HOUSETX.PAYMENTTXID   "
        SQL = SQL & " WHERE YEAR(HOUSETX.TXPERIOD)=" & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
        SQL = SQL & " AND MONTH(HOUSETX.TXPERIOD)=" & Me.cboMonth.SelectedIndex + 1
        SQL = SQL & " AND HOUSE.ISDELETE='N' "
        'SQL = SQL & " AND HOUSE.HOUSEID=HOUSETX.HOUSEID"
        SQL = SQL & " ORDER BY HOUSE.HOUSEID"
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

            For i = 0 To lCount - 1

                Select Case ConvertNullToZero(ds.Tables("Data").Rows(i).Item("SOI"))
                    Case 1
                        If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                            lPay1 = lPay1 + 1
                            lPayAll = lPayAll + 1
                        Else
                            lNotPay1 = lNotPay1 + 1
                            lNotPayAll = lNotPayAll + 1
                        End If
                        lTotal1 = lTotal1 + 1
                    Case 2
                        If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                            lPay2 = lPay2 + 1
                            lPayAll = lPayAll + 1
                        Else
                            lNotPay2 = lNotPay2 + 1
                            lNotPayAll = lNotPayAll + 1
                        End If
                        lTotal2 = lTotal2 + 1
                    Case 3
                        If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                            lPay3 = lPay3 + 1
                            lPayAll = lPayAll + 1
                        Else
                            lNotPay3 = lNotPay3 + 1
                            lNotPayAll = lNotPayAll + 1
                        End If
                        lTotal3 = lTotal3 + 1
                    Case 4
                        If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                            lPay4 = lPay4 + 1
                            lPayAll = lPayAll + 1
                        Else
                            lNotPay4 = lNotPay4 + 1
                            lNotPayAll = lNotPayAll + 1
                        End If
                        lTotal4 = lTotal4 + 1
                    Case 5
                        If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                            lPay5 = lPay5 + 1
                            lPayAll = lPayAll + 1
                        Else
                            lNotPay5 = lNotPay5 + 1
                            lNotPayAll = lNotPayAll + 1
                        End If
                        lTotal5 = lTotal5 + 1
                    Case 6
                        If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                            lPay6 = lPay6 + 1
                            lPayAll = lPayAll + 1
                        Else
                            lNotPay6 = lNotPay6 + 1
                            lNotPayAll = lNotPayAll + 1
                        End If
                        lTotal6 = lTotal6 + 1
                End Select

                lTotalAll = lTotalAll + 1

                SQL = "INSERT INTO REPORTTX(HOUSEID,HOUSENO,LANDNO,OWNERNAME,TXAMOUNT,TXPERIOD,EXPIREDATE,SEQNO"
                SQL = SQL & ",TXNAME,QTY ,PRICE,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION,REMARK,SOI )"
                SQL = SQL & " VALUES( "
                SQL = SQL & "  " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSETXID"))
                SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("HOUSENO")) & "'"
                SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("HOUSETYPE")) & "'"          'LANDNO
                SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("OWNERNAME")) & "'"
                SQL = SQL & " , " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("TXAMOUNT"))
                SQL = SQL & " ,'" & lstrPeriod & "'"
                If IsDBNull(ds.Tables("Data").Rows(i).Item("PAYDATE")) Then
                    SQL = SQL & " ,''"
                Else
                    SQL = SQL & " ,'" & Format(ds.Tables("Data").Rows(i).Item("PAYDATE"), "dd/MM/yyyy") & "'"         'EXPIREDATE
                End If

                SQL = SQL & " , " & i + 1                                                                           'SEQNO
                If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                    SQL = SQL & " ,'ชำระ'"
                Else
                    SQL = SQL & " ,''"
                End If
                SQL = SQL & " , 1"
                If ConvertNullToString(ds.Tables("Data").Rows(i).Item("ISPAY")) = "Y" Then
                    SQL = SQL & " , " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("TXAMOUNT"))
                Else
                    SQL = SQL & " ,0 "
                End If
                SQL = SQL & " ,''"
                SQL = SQL & " ,''"
                SQL = SQL & " ,''"
                SQL = SQL & " ,''"
                SQL = SQL & " ,''"
                SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("PAYMENTCODE")) & "'"
                SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("SOI")) & "'"
                SQL = SQL & " ) "

                With lCom
                    .Connection = gConnection
                    .CommandText = SQL
                    .ExecuteNonQuery()
                End With
            Next
        End If

    
        SQL = "SELECT HOUSEID,HOUSENO,LANDNO,OWNERNAME,TXAMOUNT,TXPERIOD,EXPIREDATE,SEQNO"
        SQL = SQL & ",TXNAME,QTY ,PRICE,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION,REMARK,SOI FROM REPORTTX "
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        ds = New DataSet
        da.Fill(ds, "DataTable")
        dt = ds.Tables(0)
        Dim rpt As New ReportDocument()
        Dim lDBPath As String = Application.StartupPath & "\rptPrintTx.rpt"
        'Dim lDBPath As String = "D:\Develop\Development\PS_Management\PS_Management\rptPrintTx.rpt"
        rpt.Load(lDBPath)
        rpt.SetDataSource(dt)
        rpt.SetParameterValue("Period", lstrPeriod)
        rpt.SetParameterValue("Pay1", lPay1)
        rpt.SetParameterValue("Pay2", lPay2)
        rpt.SetParameterValue("Pay3", lPay3)
        rpt.SetParameterValue("Pay4", lPay4)
        rpt.SetParameterValue("Pay5", lPay5)
        rpt.SetParameterValue("Pay6", lPay6)
        rpt.SetParameterValue("PayAll", lPayAll)
        rpt.SetParameterValue("NotPay1", lNotPay1)
        rpt.SetParameterValue("NotPay2", lNotPay2)
        rpt.SetParameterValue("NotPay3", lNotPay3)
        rpt.SetParameterValue("NotPay4", lNotPay4)
        rpt.SetParameterValue("NotPay5", lNotPay5)
        rpt.SetParameterValue("NotPay6", lNotPay6)
        rpt.SetParameterValue("NotPayAll", lNotPayAll)
        rpt.SetParameterValue("Total1", lTotal1)
        rpt.SetParameterValue("Total2", lTotal2)
        rpt.SetParameterValue("Total3", lTotal3)
        rpt.SetParameterValue("Total4", lTotal4)
        rpt.SetParameterValue("Total5", lTotal5)
        rpt.SetParameterValue("Total6", lTotal6)
        rpt.SetParameterValue("TotalAll", lTotalAll)
        frmShowReport.CrystalReportViewer1.ReportSource = rpt
        frmShowReport.CrystalReportViewer1.Refresh()
        Me.Cursor = Cursors.Default
        frmShowReport.Show()
         
LineExit:
        Exit Sub
LineError:
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

    End Sub
     
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
        'If lMonth < 12 Then
        '    lMonth = lMonth + 1
        'End If
        cboMonth.SelectedItem = GetMonthString(lMonth)
        cboYear.Text = Year(Now)
 
    End Sub

  

End Class