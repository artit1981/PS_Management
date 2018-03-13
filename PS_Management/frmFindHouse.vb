Public Class frmFindHouse
    Private mKeyID As Long = 0

    Public Property KeyID() As Long
        Get
            Return mKeyID
        End Get
        Set(ByVal value As Long)
            mKeyID = value
        End Set
    End Property

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
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
            SQL = "SELECT HOUSEID,HOUSENO,OWNERNAME,OWNERPHONE,SWITCH (HOUSETYPE =1 , 'ทาวน์เฮ้าส์' , HOUSETYPE =2, 'บ้านแฝด' ) as HOUSETYPE"
            SQL = SQL & " FROM HOUSE "
            SQL = SQL & " WHERE ISDELETE = 'N' "
            If ConvertNullToString(txtHouseNo.Text) <> "" Then
                SQL = SQL & " AND HOUSENO like'%" & ConvertNullToString(txtHouseNo.Text) & "%'"
            End If
            If ConvertNullToString(txtOwnerName.Text) <> "" Then
                SQL = SQL & " AND OWNERNAME like'%" & ConvertNullToString(txtOwnerName.Text) & "%'"
            End If
            If ConvertNullToString(txtOwnerPhone.Text) <> "" Then
                SQL = SQL & " AND OWNERPHONE like'%" & ConvertNullToString(txtOwnerPhone.Text) & "%'"
            End If
            SQL = SQL & " ORDER BY HOUSEID"
            da = New OleDb.OleDbDataAdapter(SQL, gConnection)
            da.Fill(ds, "Data")
            If ds.Tables("Data").Rows.Count > 0 Then
                grdData.DataSource = ds.Tables("Data")
                Call GridStyle()
            Else
                grdData.DataSource = Nothing
            End If
        End If
    End Sub


    Private Sub GridStyle()
        With grdData
            .Columns("HOUSEID").Width = 0

            .Columns("HOUSENO").HeaderText = "บ้านเลขที่"
            .Columns("HOUSENO").Width = 100

            .Columns("OWNERNAME").HeaderText = "เจ้าบ้าน"
            .Columns("OWNERNAME").Width = 200

            .Columns("OWNERPHONE").HeaderText = "เบอร์โทร"
            .Columns("OWNERPHONE").Width = 200

            .Columns("HOUSETYPE").HeaderText = "ประเภทบ้าน"
            .Columns("HOUSETYPE").Width = 200
        End With
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'If grdData.Rows.Count = 0 Then Exit Sub
        'If grdData.CurrentRow Is Nothing Then Exit Sub
        mKeyID = ConvertNullToZero(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value.ToString())
        If mKeyID > 0 Then
            Me.Close()
        End If


    End Sub

    Private Sub frmFindHouse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub grdData_DoubleClick(sender As Object, e As System.EventArgs) Handles grdData.DoubleClick
        If grdData.RowCount > 0 Then
            mKeyID = ConvertNullToZero(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value.ToString())
            If mKeyID > 0 Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub grdData_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles grdData.KeyPress
        If e.KeyChar = Chr(13) And grdData.RowCount > 0 Then
            mKeyID = ConvertNullToZero(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value.ToString())
            If mKeyID > 0 Then
                Me.Close()
            End If
        End If
    End Sub
End Class