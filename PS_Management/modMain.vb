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
        MsgBox("�Դ�����Դ��Ҵ����ǡѺ�к��ҹ������ ��سҵԴ��ͼ������к�" & vbNewLine & Err.Description, MsgBoxStyle.Critical + MessageBoxButtons.OK, "Error")

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
            Case 1 : GetMonthString = IIf(gbolThai, "���Ҥ�", "January")
            Case 2 : GetMonthString = IIf(gbolThai, "����Ҿѹ��", "February")
            Case 3 : GetMonthString = IIf(gbolThai, "�չҤ�", "March")
            Case 4 : GetMonthString = IIf(gbolThai, "����¹", "April")
            Case 5 : GetMonthString = IIf(gbolThai, "����Ҥ�", "May")
            Case 6 : GetMonthString = IIf(gbolThai, "�Զع�¹", "June")
            Case 7 : GetMonthString = IIf(gbolThai, "�á�Ҥ�", "July")
            Case 8 : GetMonthString = IIf(gbolThai, "�ԧ�Ҥ�", "August")
            Case 9 : GetMonthString = IIf(gbolThai, "�ѹ��¹", "September")
            Case 10 : GetMonthString = IIf(gbolThai, "���Ҥ�", "October")
            Case 11 : GetMonthString = IIf(gbolThai, "��Ȩԡ�¹", "November")
            Case 12 : GetMonthString = IIf(gbolThai, "�ѹ�Ҥ�", "December")
        End Select
    End Function

    Public Function DaysOfMonth(ByVal pDate As Date) As Integer
        DaysOfMonth = Microsoft.VisualBasic.DateAndTime.Day((DateAdd("D", 4, DateAdd("D", -Microsoft.VisualBasic.DateAndTime.Day(DateAdd("D", 4, DateSerial(Year(pDate), Month(pDate), 28))), DateSerial(Year(pDate), Month(pDate), 28)))))
    End Function


    Public Function ChangeToThaibathWord(ByVal pAmount As Double) As String
        If pAmount = 0 Then
            Return "�ٹ��ҷ��ǹ"
        End If

        Dim _integerValue As String ' �ӹǹ���
        Dim _decimalValue As String ' �ȹ���
        Dim _integerTranslatedText As String ' �ӹǹ��� ������
        Dim _decimalTranslatedText As String ' �ȹ���������

        _integerValue = Format(pAmount, "####.00") ' �Ѵ Format ����Թ�繵���Ţ 2 ��ѡ
        _decimalValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' �ȹ���
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' �ӹǹ���

        ' �ŧ �ӹǹ��� �� ������
        _integerTranslatedText = NumberToText(CDbl(_integerValue))

        ' �ŧ �ȹ��� �� ������
        If CDbl(_decimalValue) <> 0 Then
            _decimalTranslatedText = NumberToText(CDbl(_decimalValue))
        Else
            _decimalTranslatedText = ""
        End If

        ' �������շȹ��
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "�ҷ��ǹ"
        Else
            _integerTranslatedText += "�ҷ" & _decimalTranslatedText & "ʵҧ��"
        End If

        Return _integerTranslatedText

     
    End Function


    Private Function NumberToText(ByVal pAmount As Double) As String
        ' ����ѡ��
        Dim _numberText() As String = {"", "˹��", "�ͧ", "���", "���", "���", "ˡ", "��", "Ỵ", "���", "�Ժ"}

        ' ��ѡ ˹��� �Ժ ���� �ѹ ...
        Dim _digit() As String = {"", "�Ժ", "����", "�ѹ", "����", "�ʹ", "��ҹ"}
        Dim _value As String, _aWord As String, _text As String
        Dim _numberTranslatedText As String = ""
        Dim _length, _digitPosition As Integer

        _value = pAmount.ToString
        _length = Len(_value) ' ��Ҵ�ͧ �����ŷ���ͧ����ŧ �� 122200 �բ�Ҵ ��ҡѺ 6

        For i As Integer = 0 To _length - 1 ' ǹ�ٻ ������ҡ 0 ���֧ (��Ҵ - 1)
            ' ���˹觢ͧ ��ѡ (digit) �ͧ����Ţ
            ' ��
            ' ���˹���ѡ���0 (��ѡ˹���)
            ' ���˹���ѡ���1 (��ѡ�Ժ)
            ' ���˹���ѡ���2 (��ѡ����)
            ' ����繢����� i = 7 ���˹���ѡ����ҡѺ 1 (��ѡ�Ժ)
            ' ����繢����� i = 9 ���˹���ѡ����ҡѺ 3 (��ѡ�ѹ)
            ' ����繢����� i = 13 ���˹���ѡ����ҡѺ 1 (��ѡ�Ժ)
            _digitPosition = i - (6 * ((i - 1) \ 6))
            _aWord = Mid(_value, Len(_value) - i, 1)
            _text = ""
            Select Case _digitPosition
                Case 0 ' ��ѡ˹���
                    If _aWord = "1" And _length > 1 Then
                        ' ������Ţ 1 ����բ�Ҵ�ҡ���� 1 ����դ����ҡѺ "���"
                        _text = "���"
                    ElseIf _aWord <> "0" Then
                        ' ���������Ţ 0 ����� ����ѡ�� � _numberText()
                        _text = _numberText(CInt(_aWord))
                    End If
                Case 1 ' ��ѡ�Ժ
                    If _aWord = "1" Then
                        ' ������Ţ 1 ����ͧ�� ����ѡ�� ����ա �͡�ҡ����� "�Ժ"
                        '_numberTranslatedText = "�Ժ" & _numberTranslatedText
                        _text = _digit(_digitPosition)
                    ElseIf _aWord = "2" Then
                        ' ������Ţ 2 ������ѡ�ä�� "����Ժ"
                        _text = "���" & _digit(_digitPosition)
                    ElseIf _aWord <> "0" Then
                        ' ���������Ţ 0 ����� ����ѡ�� � _numberText() �������ѡ(digit) � _digit()
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 2, 3, 4, 5 ' ��ѡ���� �֧ �ʹ
                    If _aWord <> "0" Then
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 6 ' ��ѡ��ҹ
                    If _aWord = "0" Then
                        _text = "��ҹ"
                    ElseIf _aWord = "1" And _length - 1 > i Then
                        _text = "�����ҹ"
                    Else
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
            End Select
            _numberTranslatedText = _text & _numberTranslatedText
        Next

        Return _numberTranslatedText
    End Function
End Module
