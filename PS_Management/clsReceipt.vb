Imports CrystalDecisions.CrystalReports.Engine

Public Class clsReceipt

    Public Sub PrintReceipt(ByVal pPaymentTXID As String, ByVal pIsPrintCoppy As String)
        Dim SQL As String, lCount As Long = 0, lSEQ As Long = 1, lPaymentID As Long = 0, lPayType As String = ""
        Dim da As OleDb.OleDbDataAdapter
        Dim dt As DataTable
        Dim ds As DataSet = New DataSet
        Dim lCom As OleDb.OleDbCommand = gConnection.CreateCommand()

        Try
            If ConnectStatus Then
                SQL = "SELECT HOUSE.HOUSEID,HOUSE.HOUSENO,PAYMENTTX.PAYMENTCODE,PAYMENTTX.PAYMENTTXID,PAYMENTTX.PAYTYPE"
                SQL = SQL & " ,HOUSE.OWNERNAME,HOUSETX.TXAMOUNT,HOUSETX.TXPERIOD,PAYMENTTX.PAYDATE,PAYMENTTX.PAYREMARK  "
                SQL = SQL & " ,HOUSETX.TXNAME,HOUSETX.QTY,HOUSETX.PRICE,HOUSE.SOI "
                SQL = SQL & " ,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION"
                SQL = SQL & " FROM PAYMENTTX,HOUSETX,HOUSE,PROJECTCONFIG "
                SQL = SQL & " WHERE HOUSETX.PAYMENTTXID=PAYMENTTX.PAYMENTTXID and HOUSE.HOUSEID=HOUSETX.HOUSEID  "
                SQL = SQL & " AND PAYMENTTX.PAYMENTTXID in ( " & pPaymentTXID & ")"
                SQL = SQL & " ORDER BY HOUSE.HOUSEID,PAYMENTTX.PAYMENTTXID,HOUSETX.TXPERIOD"
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
                        If lPaymentID <> ConvertNullToZero(ds.Tables("Data").Rows(i).Item("PAYMENTTXID")) Then
                            lSEQ = 1
                        Else
                            lSEQ = lSEQ + 1
                        End If
                        If ConvertNullToZero(ds.Tables("Data").Rows(i).Item("PAYTYPE")) = 1 Then
                            lPayType = "โอนเงินเข้าบัญชีธนาคาร"
                        Else
                            lPayType = "เงินสด"
                        End If
                        SQL = "INSERT INTO REPORTTX(HOUSEID,HOUSENO,LANDNO,OWNERNAME,TXAMOUNT,TXPERIOD,EXPIREDATE,SEQNO"
                        SQL = SQL & ",TXNAME,QTY ,PRICE,PROJECTNAME,PROJECTADDRESS,BANKACCOUNT,SIGNNAME,SIGNPOSITION,REMARK,SOI )"
                        SQL = SQL & " VALUES( "
                        SQL = SQL & "  " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("HOUSEID"))
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("HOUSENO")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("PAYMENTCODE")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("OWNERNAME")) & "'"
                        SQL = SQL & " , " & ConvertNullToZero(ds.Tables("Data").Rows(i).Item("TXAMOUNT"))
                        SQL = SQL & " ,'" & Format(ds.Tables("Data").Rows(i).Item("TXPERIOD"), "MMMM yyyy") & "'"
                        SQL = SQL & " ,'" & Format(ds.Tables("Data").Rows(i).Item("PAYDATE"), "dd MMMM yyyy") & "'"
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
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("PAYREMARK")) & "'"
                        SQL = SQL & " ,'" & ConvertNullToString(ds.Tables("Data").Rows(i).Item("SOI")) & "'"
                        SQL = SQL & " ) "
                        lPaymentID = ConvertNullToZero(ds.Tables("Data").Rows(i).Item("PAYMENTTXID"))
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
                Dim lDBPath As String = Application.StartupPath & "\rptReceipt.rpt"
                'Dim lDBPath As String = "D:\Develop\Development\PS_Management\PS_Management\rptReceipt.rpt"
                rpt.Load(lDBPath)
                rpt.SetDataSource(dt)
                rpt.SetParameterValue("Header1", "ใบเสร็จรับเงิน (ต้นฉบับ)")
                rpt.SetParameterValue("PrintDate", Format(Now, "dd MMMM yyyy  HH:mm:ss"))
                rpt.SetParameterValue("PayBy", lPayType)
                Dim lfrmReport As New frmShowReport
                lfrmReport.CrystalReportViewer1.ReportSource = rpt
                lfrmReport.CrystalReportViewer1.Refresh()
                lfrmReport.Show()

                If pIsPrintCoppy = "Y" Then
                    Dim lfrmReport2 As New frmShowReport
                    Dim rpt2 As New ReportDocument()
                    rpt2.Load(lDBPath)
                    rpt2.SetDataSource(dt)
                    rpt2.SetParameterValue("Header1", "ใบเสร็จรับเงิน (สำเนา)")
                    rpt2.SetParameterValue("PrintDate", Format(Now, "dd MMMM yyyy  HH:mm:ss"))
                    rpt2.SetParameterValue("PayBy", lPayType)
                    lfrmReport2.CrystalReportViewer1.ReportSource = rpt2
                    lfrmReport2.CrystalReportViewer1.Refresh()
                    lfrmReport2.Show()
                End If
               
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        End Try
    End Sub
End Class
