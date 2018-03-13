Option Explicit On

Public Class frmControl
    Public Event AddNew()
    Public Event Edit()
    Public Event Preview()
    Public Event Delete()
    Public Event FormUnload()
    Public Event SetID(ByVal pID As String)

    Private Sub frmControl_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RaiseEvent FormUnload()
    End Sub

    Private Sub frmControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.WindowState = FormWindowState.Maximized
        'Call CheckPrivilege()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        RaiseEvent AddNew()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        RaiseEvent Preview()
    End Sub

    Private Sub grdData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdData.DoubleClick
        RaiseEvent Preview()
    End Sub

    Private Sub grdData_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdData.SelectionChanged
        If grdData.Rows.Count = 0 Then Exit Sub
        If grdData.CurrentRow Is Nothing Then Exit Sub
        'Send ID of record to Parent class when click on Grid
        RaiseEvent SetID(ConvertNullToString(grdData.Rows(grdData.CurrentCell.RowIndex).Cells(0).Value.ToString()))
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        RaiseEvent Edit()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        RaiseEvent Delete()
    End Sub

    Private Sub CheckPrivilege()
        With Me
            If gUserLevel = UserLevel.Admin Then
                .btnAddNew.Enabled = True
                .btnEdit.Enabled = True
                .btnDelete.Enabled = True
            Else
                .btnAddNew.Enabled = False
                .btnEdit.Enabled = False
                .btnDelete.Enabled = False
            End If
        End With
    End Sub


End Class