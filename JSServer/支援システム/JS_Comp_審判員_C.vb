Public Class JS_Comp_審判員_C


    Public 審判員No()
    Public 審判記号()
    Public 審判員名()
    Public 審判員表示名()
    Public 審判員担当チーム

    Const MAX審判員数 = 50


    Public Sub 初期化()

        ReDim 審判員No(MAX審判員数)
        ReDim 審判記号(MAX審判員数)
        ReDim 審判員名(MAX審判員数)
        ReDim 審判員表示名(MAX審判員数)
        ReDim 審判員担当チーム(MAX審判員数, 25)


    End Sub



    Public Sub Set_審判員No(ByVal 審判員No_, ByVal No)
        審判員No(No) = 審判員No_
    End Sub
    Public Sub Set_審判記号(ByVal 審判記号_, ByVal No)
        審判記号(No) = 審判記号_
    End Sub
    Public Sub Set_審判員名(ByVal 審判員名_, ByVal No)
        審判員名(No) = 審判員名_
    End Sub
    Public Sub Set_審判員表示名(ByVal 審判員表示名_, ByVal No)
        審判員表示名(No) = 審判員表示名_
    End Sub
    Public Sub Set_審判員担当チーム(ByVal 審判員担当チーム_, ByVal No, ByVal チームNo)
        審判員担当チーム(No, チームNo) = 審判員担当チーム_
    End Sub




End Class