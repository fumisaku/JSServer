Public Class C_ラウンド

    Public 区分番号 As String
    Public ラウンド番号 As String
    Public 採点方式 As String
    Public 担当審判グループ As Integer
    'Public 出場組数 As Integer
    Public ヒート数 As Integer
    Public UP予定数 As Integer
    Public CaliMax As Double
    Public CaliMin As Double

    Public リアルタイムFLAG As String

    Public ヒート割方式 As String


    Public Function Get_採点方式名()

        Dim rc As String = 採点方式

        If 採点方式 = "PDJ10A" Then

            rc = "AJS3.0J(Type D)"

        ElseIf 採点方式 = "PDJ10B" Then

            rc = "AJS3.0J"

        ElseIf 採点方式 = "PDJ20A" Then

            rc = "AJS3.1J(Type D)"

        ElseIf 採点方式 = "PDJ20B" Then

            rc = "AJS3.1J"

        End If


        Return rc

    End Function

End Class
