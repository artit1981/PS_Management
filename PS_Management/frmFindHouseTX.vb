Public Class frmFindHouseTX
    Private mKeyID As List(Of Long)
    Private mHouseID As Long = 0
    Private mHouseNo As String = ""

    Public Property HouseID() As Long
        Get
            Return mHouseID
        End Get
        Set(ByVal value As Long)
            mHouseID = value
        End Set
    End Property

    Public Property HouseNo() As String
        Get
            Return mHouseNo
        End Get
        Set(ByVal value As String)
            mHouseNo = value
        End Set
    End Property

    Public Property KeyID() As List(Of Long)
        Get
            Return mKeyID
        End Get
        Set(ByVal value As List(Of Long))
            mKeyID = value
        End Set
    End Property

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error GoTo LineError

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
            SQL = "SELECT  HOUSETXID,TXPERIOD,TXAMOUNT FROM HOUSETX"
            'SQL = SQL & " WHERE month(TXPERIOD)= " & Me.cboMonth.SelectedIndex + 1
            'SQL = SQL & " AND year(TXPERIOD)= " & Me.cboYear.SelectedItem
            SQL = SQL & " WHERE ISPAY='N' "
            If mHouseID > 0 Then
                SQL = SQL & " AND HOUSEID = " & mHouseID
            End If
            SQL = SQL & " Order by TXPERIOD"

            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                grdData.DataSource = ds.Tables("Data")
                Call GridStyle()
                'Add Select column
                Dim AddColumn As New DataGridViewCheckBoxColumn
                With AddColumn
                    .HeaderText = "เลือก"
                    .Name = "ISSELECT"
                    .Width = 80
                End With
                grdData.Columns.Insert(0, AddColumn)
            Else
                grdData.DataSource = Nothing
            End If

        End If
       
    End Sub

    Private Sub GridStyle()
        With grdData
            .Columns("HOUSETXID").Visible = False

            .Columns("TXPERIOD").HeaderText = "งวดชำระ"
            .Columns("TXPERIOD").Width = 150
            .Columns("TXPERIOD").DefaultCellStyle.Format = "MMMM yyyy"
            .Columns("TXPERIOD").ReadOnly = True
            .Columns("TXPERIOD").DefaultCellStyle.BackColor = Color.LightYellow

            .Columns("TXAMOUNT").HeaderText = "ยอดเงิน"
            .Columns("TXAMOUNT").Width = 180
            .Columns("TXAMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TXAMOUNT").DefaultCellStyle.Format = "#,##0.00"
            .Columns("TXAMOUNT").ReadOnly = True
            .Columns("TXAMOUNT").DefaultCellStyle.BackColor = Color.LightYellow
        End With
    End Sub

    Private Function GetKey(ByVal pRowIndex As Long) As Boolean
        Dim lHouseTXID As Long = 0
        mKeyID = Nothing
        mKeyID = New List(Of Long)

        If grdData.RowCount > 0 Then
            If pRowIndex >= 0 Then
                lHouseTXID = ConvertNullToZero(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value)
                If lHouseTXID > 0 Then
                    mKeyID.Add(lHouseTXID)
                End If
            Else
                For lRow = 0 To grdData.RowCount - 1
                    If grdData.Rows(lRow).Cells(0).Value = True Then
                        mKeyID.Add(ConvertNullToZero(grdData.Rows(lRow).Cells("HOUSETXID").Value))
                    End If
                Next
            End If
        End If

        Return mKeyID.Count > 0
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If GetKey(-1) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmFindHouse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
        txtHouseNo.Text = mHouseNo
    End Sub

    'Private Sub grdData_DoubleClick(sender As Object, e As System.EventArgs) Handles grdData.DoubleClick
    '    If grdData.RowCount > 0 Then
    '        mKeyID = ConvertNullToZero(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value.ToString())
    '        If mKeyID > 0 Then
    '            Me.Close()
    '        End If
    '    End If
    'End Sub

    'Private Sub grdData_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles grdData.KeyPress
    '    If e.KeyChar = Chr(13) And grdData.RowCount > 0 Then
    '        mKeyID = ConvertNullToZero(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value.ToString())
    '        If mKeyID > 0 Then
    '            Me.Close()
    '        End If
    '    End If
    'End Sub


End Class