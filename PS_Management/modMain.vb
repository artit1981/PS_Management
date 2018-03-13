Option Explicit On
Module modMain
    'Public gConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & "D:\Investment\Account\Account\bin\Debug\DB\DB.MDB ;Exclusive=1;Uid=admin;Pwd=qwerty"
    Public gConnString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & "D:\Investment\Account\Account\bin\Debug\DB\DB.MDB ;Jet OLEDB:Database Password=qwerty;"
    Public gConnection As OleDb.OleDbConnection
    Public gMode As Mode
    Public gUserLevel As UserLevel
    Public gUserID As Integer
    Public gUserName As String
    Public mblnConnectStatus As Boolean
    Public gbolThai As Boolean

    Public Enum Mode
        AddNew = 1
        Edit = 2
        Preview = 3
        Delete = 4
    End Enum

    Public Enum UserLevel
        Admin = 1
        Guest = 2
    End Enum

    Public Enum TxType
        Receive = 0
        Pay = 1
    End Enum

    Public Sub Connect()
        On Error GoTo LineError
        If gConnection Is Nothing Then
            gConnection = New OleDb.OleDbConnection
        End If
        gConnection.ConnectionString = gConnString
        If gConnection.State = ConnectionState.Closed Then
            gConnection.Open()
        End If

        mblnConnectStatus = True
LineExit:
        Exit Sub
LineError:
        MsgBox("เกิดความผิดพลาดเกี่ยวกับระบบฐานข้อมูล กรุณาติดต่อผู้ดูแลระบบ" & vbNewLine & Err.Description, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

        mblnConnectStatus = False
    End Sub

    Public Sub DisConnect()
        On Error Resume Next
        gConnection.Close()
        mblnConnectStatus = False
        gConnection = Nothing
    End Sub

    Public Property ConnectStatus(Optional ByVal pReconnect As Boolean = True) As Boolean

        Get
            If gConnection Is Nothing Then
                gConnection = New OleDb.OleDbConnection
            End If

            If mblnConnectStatus Or gConnection.State = ConnectionState.Open Then
                ConnectStatus = mblnConnectStatus
            Else
                If pReconnect Then Call Connect() 'Re Connect
                ConnectStatus = mblnConnectStatus
            End If
        End Get
        Set(ByVal value As Boolean)

        End Set

    End Property

    Public Function ConvertNullToZero(ByVal pField As Object) As Object
        On Error GoTo LineError
        If pField Is DBNull.Value OrElse Trim(pField) = "" Then
            ConvertNullToZero = 0
        Else
            ConvertNullToZero = CDec(pField)
        End If
LineExit:
        Exit Function
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, vbInformation + vbOKCancel, "Error")
    End Function

    Public Function ConvertNullToString(ByVal pField As Object) As String
        On Error GoTo LineError
        If pField Is DBNull.Value Then
            ConvertNullToString = ""
        Else
            ConvertNullToString = Trim(pField)
        End If
LineExit:
        Exit Function
LineError:
        MsgBox(Err.Number & ":" & Err.Description & vbNewLine & "in " & Err.Source, vbInformation + vbOKCancel, "Error")
    End Function

    Public Function ChkKeyDecimal(ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal pChkDot As Boolean = True _
                        , Optional ByVal pChkTel As Boolean = False) As Boolean
        'Return True if not in [0,1,2,3,4,5,6,7,8,9,.,-]
        ChkKeyDecimal = False
        If e.KeyChar < "0" Or e.KeyChar > "9" Then
            ChkKeyDecimal = True
            If pChkDot Then
                If e.KeyChar = "." Then ChkKeyDecimal = False
            End If
            If pChkTel Then
                If e.KeyChar = "-" Then ChkKeyDecimal = False
            End If
            If Asc(e.KeyChar) = 8 Then ChkKeyDecimal = False
        End If
    End Function

    Public Function formatSQLDate(ByVal pDate As Date) As String
        If gbolThai = True Then 'Thai format
            formatSQLDate = "#" & Format(pDate, "MM/d/") & (Format(pDate, "yyyy") - 543) & "#"
        Else 'English
            formatSQLDate = "#" & Format(pDate, "MM/d/") & Year(pDate) & "#"
        End If
    End Function

    Public Function formatSQLDateTime(ByVal pDate As Date) As String
        formatSQLDateTime = Format(pDate, "yyyy-MM-dd HH:mm:ss")
    End Function

    Public Function GetMonthString(ByVal intMonth As Integer) As String
        GetMonthString = "???"
        If intMonth < 1 Or intMonth > 12 Then Exit Function
        Select Case intMonth
            Case 1 : GetMonthString = IIf(gbolThai, "มกราคม", "January")
            Case 2 : GetMonthString = IIf(gbolThai, "กุมภาพันธ์", "February")
            Case 3 : GetMonthString = IIf(gbolThai, "มีนาคม", "March")
            Case 4 : GetMonthString = IIf(gbolThai, "เมษายน", "April")
            Case 5 : GetMonthString = IIf(gbolThai, "พฤษภาคม", "May")
            Case 6 : GetMonthString = IIf(gbolThai, "มิถุนายน", "June")
            Case 7 : GetMonthString = IIf(gbolThai, "กรกฎาคม", "July")
            Case 8 : GetMonthString = IIf(gbolThai, "สิงหาคม", "August")
            Case 9 : GetMonthString = IIf(gbolThai, "กันยายน", "September")
            Case 10 : GetMonthString = IIf(gbolThai, "ตุลาคม", "October")
            Case 11 : GetMonthString = IIf(gbolThai, "พฤศจิกายน", "November")
            Case 12 : GetMonthString = IIf(gbolThai, "ธันวาคม", "December")
        End Select
    End Function

    Public Function DaysOfMonth(ByVal pDate As Date) As Integer
        DaysOfMonth = Microsoft.VisualBasic.DateAndTime.Day((DateAdd("D", 4, DateAdd("D", -Microsoft.VisualBasic.DateAndTime.Day(DateAdd("D", 4, DateSerial(Year(pDate), Month(pDate), 28))), DateSerial(Year(pDate), Month(pDate), 28)))))
    End Function


    Public Function ChangeToThaibathWord(ByVal pAmount As Double) As String
        If pAmount = 0 Then
            Return "ศูนย์บาทถ้วน"
        End If

        Dim _integerValue As String ' จำนวนเต็ม
        Dim _decimalValue As String ' ทศนิยม
        Dim _integerTranslatedText As String ' จำนวนเต็ม ภาษาไทย
        Dim _decimalTranslatedText As String ' ทศนิยมภาษาไทย

        _integerValue = Format(pAmount, "####.00") ' จัด Format ค่าเงินเป็นตัวเลข 2 หลัก
        _decimalValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' ทศนิยม
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' จำนวนเต็ม

        ' แปลง จำนวนเต็ม เป็น ภาษาไทย
        _integerTranslatedText = NumberToText(CDbl(_integerValue))

        ' แปลง ทศนิยม เป็น ภาษาไทย
        If CDbl(_decimalValue) <> 0 Then
            _decimalTranslatedText = NumberToText(CDbl(_decimalValue))
        Else
            _decimalTranslatedText = ""
        End If

        ' ถ้าไม่มีทศนิม
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "บาทถ้วน"
        Else
            _integerTranslatedText += "บาท" & _decimalTranslatedText & "สตางค์"
        End If

        Return _integerTranslatedText

     
    End Function


    Private Function NumberToText(ByVal pAmount As Double) As String
        ' ตัวอักษร
        Dim _numberText() As String = {"", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ"}

        ' หลัก หน่วย สิบ ร้อย พัน ...
        Dim _digit() As String = {"", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน"}
        Dim _value As String, _aWord As String, _text As String
        Dim _numberTranslatedText As String = ""
        Dim _length, _digitPosition As Integer

        _value = pAmount.ToString
        _length = Len(_value) ' ขนาดของ ข้อมูลที่ต้องการแปลง เช่น 122200 มีขนาด เท่ากับ 6

        For i As Integer = 0 To _length - 1 ' วนลูป เริ่มจาก 0 จนถึง (ขนาด - 1)
            ' ตำแหน่งของ หลัก (digit) ของตัวเลข
            ' เช่น
            ' ตำแหน่งหลักที่0 (หลักหน่วย)
            ' ตำแหน่งหลักที่1 (หลักสิบ)
            ' ตำแหน่งหลักที่2 (หลักร้อย)
            ' ถ้าเป็นข้อมูล i = 7 ตำแหน่งหลักจะเท่ากับ 1 (หลักสิบ)
            ' ถ้าเป็นข้อมูล i = 9 ตำแหน่งหลักจะเท่ากับ 3 (หลักพัน)
            ' ถ้าเป็นข้อมูล i = 13 ตำแหน่งหลักจะเท่ากับ 1 (หลักสิบ)
            _digitPosition = i - (6 * ((i - 1) \ 6))
            _aWord = Mid(_value, Len(_value) - i, 1)
            _text = ""
            Select Case _digitPosition
                Case 0 ' หลักหน่วย
                    If _aWord = "1" And _length > 1 Then
                        ' ถ้าเป็นเลข 1 และมีขนาดมากกว่า 1 ให้มีค่าเท่ากับ "เอ็ด"
                        _text = "เอ็ด"
                    ElseIf _aWord <> "0" Then
                        ' ถ้าไม่ใช่เลข 0 ให้หา ตัวอักษร ใน _numberText()
                        _text = _numberText(CInt(_aWord))
                    End If
                Case 1 ' หลักสิบ
                    If _aWord = "1" Then
                        ' ถ้าเป็นเลข 1 ไม่ต้องมี ตัวอักษร อื่นอีก นอกจากคำว่า "สิบ"
                        '_numberTranslatedText = "สิบ" & _numberTranslatedText
                        _text = _digit(_digitPosition)
                    ElseIf _aWord = "2" Then
                        ' ถ้าเป็นเลข 2 ให้ตัวอักษรคือ "ยี่สิบ"
                        _text = "ยี่" & _digit(_digitPosition)
                    ElseIf _aWord <> "0" Then
                        ' ถ้าไม่ใช่เลข 0 ให้หา ตัวอักษร ใน _numberText() และหาหลัก(digit) ใน _digit()
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 2, 3, 4, 5 ' หลักร้อย ถึง แสน
                    If _aWord <> "0" Then
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 6 ' หลักล้าน
                    If _aWord = "0" Then
                        _text = "ล้าน"
                    ElseIf _aWord = "1" And _length - 1 > i Then
                        _text = "เอ็ดล้าน"
                    Else
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
            End Select
            _numberTranslatedText = _text & _numberTranslatedText
        Next

        Return _numberTranslatedText
    End Function
End Module
