Public Class frmTXHis

    Private Sub frmAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        On Error GoTo LineError
        For i = 1 To 12
            cboMonth.Items.Add(GetMonthString(i))
        Next i

        For i = Year(Now) - 5 To Year(Now) + 5
            cboYear.Items.Add(i)
        Next i
        cboMonth.SelectedItem = GetMonthString(Month(Now))
        cboYear.Text = Year(Now)
        Call LoadData()
        Exit Sub
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")
        Me.Close()
    End Sub

    Private Sub LoadData()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet
        'Dim dr As DataRow
        'Dim llngDayMonth As Int16, i As Int16, llngID As Long
        'Dim ldtpCurrentDate As Date
        If ConnectStatus Then
            SQL = "SELECT HOUSE.HOUSEID,HOUSE.HOUSENO,HOUSE.OWNERNAME,SWITCH (HOUSETYPE =1 , 'ทาวน์เฮ้าส์' , HOUSETYPE =2, 'บ้านแฝด' ) as HOUSETYPE  ,HOUSETX.TXAMOUNT,HOUSETX.ISPAY,HOUSETX.PAYDATE "
            SQL = SQL & " FROM HOUSE,HOUSETX "
            SQL = SQL & " WHERE HOUSE.HOUSEID=HOUSETX.HOUSEID"
            SQL = SQL & " AND MONTH(HOUSETX.TXPERIOD)=" & Me.cboMonth.SelectedIndex + 1
            SQL = SQL & " AND YEAR(HOUSETX.TXPERIOD)=" & IIf(gbolThai = True, Me.cboYear.SelectedItem - 543, Me.cboYear.SelectedItem)
            SQL = SQL & " AND HOUSE.ISDELETE='N' "
            SQL = SQL & " ORDER BY HOUSE.HOUSEID"
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            da.Fill(ds, "Data")
            'If ds.Tables("Data").Rows.Count > 0 Then
            'Else
            '    ldtpCurrentDate = DateSerial(Me.cboYear.SelectedItem, Me.cboMonth.SelectedIndex + 1, 1)
            '    llngDayMonth = DaysOfMonth(ldtpCurrentDate)
            '    For i = 1 To llngDayMonth
            '        dr = ds.Tables("Data").NewRow
            '        dr.Item("ACCID") = llngID
            '        dr.Item("ACCDATE") = ldtpCurrentDate
            '        dr.Item("USERID") = gUserID
            '        llngID = llngID + 1
            '        ds.Tables("Data").Rows.Add(dr)
            '        ldtpCurrentDate = DateAdd(DateInterval.Day, 1, ldtpCurrentDate)
            '    Next i
            'End If
            grdData.DataSource = ds.Tables("Data")
            Call GridStyle()
        End If
    End Sub

    Private Sub GridStyle()
        With grdData
            .Columns("HOUSEID").Visible = False

            .Columns("HOUSENO").HeaderText = "บ้านเลขที่"
            .Columns("HOUSENO").Width = 70

            .Columns("OWNERNAME").HeaderText = "เจ้าบ้าน"
            .Columns("OWNERNAME").Width = 150

            .Columns("HOUSETYPE").HeaderText = "ประเภทบ้าน"
            .Columns("HOUSETYPE").Width = 150


            .Columns("TXAMOUNT").HeaderText = "ยอดเงิน"
            .Columns("TXAMOUNT").Width = 70
            .Columns("TXAMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TXAMOUNT").DefaultCellStyle.Format = "#,##0.00"

            .Columns("ISPAY").HeaderText = "การชำระ"
            .Columns("ISPAY").Width = 90
            .Columns("ISPAY").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("PAYDATE").HeaderText = "วันที่ชำระ"
            .Columns("PAYDATE").Width = 70
            .Columns("PAYDATE").DefaultCellStyle.Format = "dd/MM/yyyy"
        End With
    End Sub

     

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Call LoadData()
    End Sub

    Private Sub cboMonth_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMonth.SelectedValueChanged
        Call LoadData()
    End Sub

    Private Sub cboYear_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedValueChanged
        Call LoadData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class