Public Class frmMain
    Private mToClose As Boolean = True
    Private m_ChildFormNumber As Integer = 0

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If mToClose = True Then
            Global.System.Windows.Forms.Application.Exit()
            Call DisConnect()
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not ConnectStatus(True) Then
            mToClose = True
            Me.Close()
        Else
            ToolStripUserName.Text = "ผู้ใช้งาน : " & gUserName
            If Val(Format(Now, "yyyy")) > 2500 Then
                gbolThai = True
            Else
                gbolThai = False
            End If

            If gUserLevel = UserLevel.Guest Then
                mnuUser.Enabled = False
            End If
        End If
    End Sub
 


    Private Sub mnuHouseInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHouseInfo.Click
        Static lcls As clsControlHouseInfo
        On Error GoTo LineError
        If lcls Is Nothing Then lcls = New clsControlHouseInfo
        If Not lcls.Running Then Call lcls.Execute()
LineExit:
        Exit Sub
LineError:
        Resume LineExit

    End Sub

    Private Sub tbnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbnExit.Click
        If MsgBox("ต้องการออกจากระบบ ?", MsgBoxStyle.Question + MessageBoxButtons.YesNo, "ยืนยันการออกจากระบบ") = vbYes Then
            mToClose = True
            Me.Close()
        End If
    End Sub

    Private Sub MnuloseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuloseAll.Click
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub cmdData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdData.Click
        frmProcessMonthly.MdiParent = Me
        frmProcessMonthly.Show()
    End Sub
     

   
    Private Sub mnuConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfig.Click
        frmConfig.MdiParent = Me
        frmConfig.Show()
    End Sub
     
     
    Private Sub cmdPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPay.Click
        frmPayment.MdiParent = Me
        frmPayment.ModeRun = 1
        frmPayment.Show()
    End Sub

    Private Sub cmdHis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHis.Click
        frmTXHis.MdiParent = Me
        frmTXHis.Show()
    End Sub

    Private Sub mnuReportBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportBill.Click
        frmPrintBill.MdiParent = Me
        frmPrintBill.Show()
    End Sub

    Private Sub MnuPrintReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuPrintReceipt.Click
        frmPrintReceipt.MdiParent = Me
        frmPrintReceipt.Show()
    End Sub
End Class
