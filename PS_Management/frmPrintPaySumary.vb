Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPrintPaySumary
    Dim mHouseID As Long = 0

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        InsertData()
    End Sub


    Protected Sub InsertData()
        Dim SQL As String, lMonthNo As Long, lstrMonthNo As String
        Dim da As OleDb.OleDbDataAdapter
        Dim dt As DataTable, ldtPay As DataTable
        Dim ds As DataSet = New DataSet
        On Error GoTo LineError

        If Not ConnectStatus Then GoTo LineExit 'Check Connection

        Me.Cursor = Cursors.WaitCursor

        SQL = "SELECT HOUSE.HOUSEID,HOUSE.HOUSENO,HOUSE.SOI,HOUSE.OWNERNAME ,HOUSETX.TXAMOUNT,HOUSETX.ISPAY,HOUSETX.PAYDATE,HOUSETX.TXPERIOD AS MONTHNO "
        SQL = SQL & " FROM HOUSE,HOUSETX "
        SQL = SQL & " WHERE HOUSE.HOUSEID=HOUSETX.HOUSEID"
        SQL = SQL & " AND YEAR(HOUSETX.TXPERIOD)=" & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
        SQL = SQL & " AND HOUSE.ISDELETE='N' "
        SQL = SQL & " ORDER BY HOUSE.HOUSEID,MONTH(HOUSETX.TXPERIOD)"
        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        ds = New DataSet
        da.Fill(ds, "DataTable2")

        ldtPay = InitailTable()
        For Each pRow As DataRow In ds.Tables("DataTable2").Rows
            lMonthNo = Month(pRow("MONTHNO"))
            lstrMonthNo = "M" & lMonthNo

            Dim foundRows() As Data.DataRow
            foundRows = ldtPay.[Select]("HOUSEID='" & pRow("HOUSEID") & "'")
            If foundRows.Length = 0 Then
                Dim R As DataRow = ldtPay.NewRow
                R("HOUSEID") = pRow("HOUSEID")
                R("HOUSENO") = ConvertNullToString(pRow("HOUSENO"))
                R("OWNERNAME") = ConvertNullToString(pRow("OWNERNAME"))
                R("SOI") = ConvertNullToString(pRow("SOI"))
                R(lstrMonthNo) = IIf(pRow("ISPAY") = "N", "", "/")
                ldtPay.Rows.Add(R)
            Else
                foundRows(0)(lstrMonthNo) = IIf(pRow("ISPAY") = "N", "", "/")
            End If
        Next


        Dim rpt As New ReportDocument()
        Dim lDBPath As String = Application.StartupPath & "\rptPaySumary.rpt"
        rpt.Load(lDBPath)
        rpt.SetDataSource(ldtPay)
        rpt.SetParameterValue("Years", cboYear.Text)
    
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

    Private Function InitailTable() As DataTable
        Dim lTable As New DataTable

        Try
            lTable.Columns.Add("HOUSEID")
            lTable.Columns.Add("HOUSENO")
            lTable.Columns.Add("OWNERNAME")
            lTable.Columns.Add("SOI")
            lTable.Columns.Add("M1")
            lTable.Columns.Add("M2")
            lTable.Columns.Add("M3")
            lTable.Columns.Add("M4")
            lTable.Columns.Add("M5")
            lTable.Columns.Add("M6")
            lTable.Columns.Add("M7")
            lTable.Columns.Add("M8")
            lTable.Columns.Add("M9")
            lTable.Columns.Add("M10")
            lTable.Columns.Add("M11")
            lTable.Columns.Add("M12")
            Dim primaryKey(1) As DataColumn
            primaryKey(1) = lTable.Columns("HOUSEID")
            lTable.PrimaryKey = primaryKey

            Return lTable
        Catch ex As Exception
            Return lTable
        Finally

        End Try
    End Function



    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmProcessMonthly_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer
       

        For i = Year(Now) - 5 To Year(Now) + 5
            cboYear.Items.Add(i)
        Next i


        cboYear.Text = Year(Now)

    End Sub



End Class