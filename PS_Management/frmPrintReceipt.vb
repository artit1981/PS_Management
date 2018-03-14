Public Class frmPrintReceipt
    Dim mHouseID As Long = 0
    Dim lFirstLoad As Boolean = True

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

        mHouseID = 0

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
        If mHouseID > 0 Then
            LoadHouseTX()
        End If

LineExit:
    End Sub


    Private Sub LoadHouseTX()
        Dim SQL As String
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet = New DataSet

        If Not ConnectStatus Then GoTo LineExit
        SQL = "SELECT PAYMENTTX.PAYMENTTXID,PAYMENTTX.PAYMENTCODE"
        SQL = SQL & " ,PAYMENTTX.PAYDATE,TOTALAMOUNT,PAYMENTTX.PAYREMARK  "
        SQL = SQL & " FROM PAYMENTTX"
        SQL = SQL & " WHERE HOUSEID = " & mHouseID
        SQL = SQL & " Order by PAYMENTTX.PAYMENTTXID desc"

        da = New OleDb.OleDbDataAdapter(SQL, gConnection)
        da.Fill(ds, "Data")
        If ds.Tables("Data").Rows.Count > 0 Then
            grdData.DataSource = ds.Tables("Data")
            Call GridStyle()
            If lFirstLoad = True Then
                'Add Select column
                Dim AddColumn As New DataGridViewCheckBoxColumn
                With AddColumn
                    .HeaderText = "เลือก"
                    .Name = "ISSELECT"
                    .Width = 80
                End With
                grdData.Columns.Insert(0, AddColumn)
                lFirstLoad = False
            End If
        Else
            grdData.DataSource = Nothing
        End If

LineExit:
    End Sub


    Private Sub GridStyle()
        With grdData
            .Columns("PAYMENTTXID").Visible = False

            .Columns("PAYMENTCODE").HeaderText = "เลขที่"
            .Columns("PAYMENTCODE").Width = 110

            .Columns("PAYDATE").HeaderText = "วันที่ชำระ"
            .Columns("PAYDATE").Width = 100
            .Columns("PAYDATE").DefaultCellStyle.Format = "dd/MM/yyyy"
            .Columns("PAYDATE").ReadOnly = True

            .Columns("TOTALAMOUNT").HeaderText = "ยอดชำระ"
            .Columns("TOTALAMOUNT").Width = 100
            .Columns("TOTALAMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TOTALAMOUNT").DefaultCellStyle.Format = "#,##0.00"
            .Columns("TOTALAMOUNT").ReadOnly = True

            .Columns("PAYREMARK").HeaderText = "รายละเอียดการชำระ"
            .Columns("PAYREMARK").Width = 150

        End With
    End Sub

    Private Function ShowFormFind() As Long
        Dim lfrmFind As New frmFindHouse
        lfrmFind.ShowDialog()
        Return lfrmFind.KeyID
LineExit:
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim lResult As MsgBoxResult, lIsPrintCoppy As String = "", lPaymentIDList As String = ""
        Try
            lPaymentIDList = GetPaymentList()
            If lPaymentIDList = "" Then
                MsgBox("กรุณาระบุข้อมูล", MsgBoxStyle.Information + MessageBoxButtons.OK, "การตรวจสอบ")
            Else
                lResult = MsgBox("ต้องการพิมพ์สำเนาใบเสร็จหรือไม่", MsgBoxStyle.Information + MessageBoxButtons.YesNoCancel, "Information")
                If lResult = MsgBoxResult.Yes Then
                    lIsPrintCoppy = "Y"
                ElseIf lResult = MsgBoxResult.No Then
                    lIsPrintCoppy = "N"
                End If

                If lIsPrintCoppy <> "" Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim lclsPrint As New clsReceipt
                    lclsPrint.PrintReceipt(lPaymentIDList, lIsPrintCoppy)
                End If
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function GetPaymentList() As String
        Dim lList As String = ""
        Try
            grdData.EndEdit()
            grdData.Refresh()
            For lRow = 0 To grdData.Rows.Count - 1
                If grdData.Rows(lRow).Cells(0).Value = True Then
                    If lList = "" Then
                        lList = ConvertNullToZero(grdData.Rows(lRow).Cells("PAYMENTTXID").Value) & ""
                    Else
                        lList = lList & "," & ConvertNullToZero(grdData.Rows(lRow).Cells("PAYMENTTXID").Value)
                    End If
                End If
            Next

        Catch ex As Exception

        End Try

        Return lList
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class