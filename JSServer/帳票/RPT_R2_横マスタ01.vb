Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports Microsoft.Reporting.WinForms


Public Class RPT_R2_横マスタ01

    Private report As LocalReport

    Private 帳票印刷 As 帳票印刷

    Public Sub New()

        report = New LocalReport()
        'report.ReportPath = "..\..\帳票\RPT_R2_横マスタ01.rdlc"
        report.ReportPath = "Report\RPT_R2_横マスタ03.rdlc"

        帳票印刷 = New 帳票印刷


    End Sub

    Public Sub New(言語 As String)

        report = New LocalReport()

        If 言語 = "E" Then
            'report.ReportPath = "..\..\帳票\RPT_R3_縦マスタ01_E.rdlc"
            report.ReportPath = "Report\RPT_R2_横マスタ01_E.rdlc"
        Else
            'report.ReportPath = "..\..\帳票\RPT_R3_縦マスタ01.rdlc"
            report.ReportPath = "Report\RPT_R2_横マスタ03.rdlc"
        End If


        帳票印刷 = New 帳票印刷


    End Sub



    Public Sub SetParm(Parm As RPT_Parm_H2)

        Set共通項目(Parm)


    End Sub

    Public Sub 印刷実行()

        帳票印刷.Export(report, True)  '横向き

        帳票印刷.Dispose()

    End Sub


    Private Sub Set共通項目(Parm As RPT_Parm_H2)



        Dim Parameters As New List(Of ReportParameter)
        With Parameters
            '.Add(New ReportParameter("ReportParameter1", "Ｂ級戦"))
            .Add(New ReportParameter("タイトル", Parm.タイトル))
            .Add(New ReportParameter("区分名", Parm.区分名))
            .Add(New ReportParameter("ラウンド区分1", Parm.ラウンド区分(1)))
            .Add(New ReportParameter("ラウンド区分2", Parm.ラウンド区分(2)))
            .Add(New ReportParameter("ラウンド区分3", Parm.ラウンド区分(3)))
            .Add(New ReportParameter("ラウンド区分4", Parm.ラウンド区分(4)))
            .Add(New ReportParameter("ラウンド区分5", Parm.ラウンド区分(5)))
            .Add(New ReportParameter("ラウンド区分6", Parm.ラウンド区分(6)))
            .Add(New ReportParameter("ラウンド区分7", Parm.ラウンド区分(7)))

            .Add(New ReportParameter("ラウンド区分_色1", Parm.ラウンド区分_色(1)))
            .Add(New ReportParameter("ラウンド区分_色2", Parm.ラウンド区分_色(2)))
            .Add(New ReportParameter("ラウンド区分_色3", Parm.ラウンド区分_色(3)))
            .Add(New ReportParameter("ラウンド区分_色4", Parm.ラウンド区分_色(4)))
            .Add(New ReportParameter("ラウンド区分_色5", Parm.ラウンド区分_色(5)))
            .Add(New ReportParameter("ラウンド区分_色6", Parm.ラウンド区分_色(6)))
            .Add(New ReportParameter("ラウンド区分_色7", Parm.ラウンド区分_色(7)))

            .Add(New ReportParameter("ヒート数", CStr(Parm.ヒート数)))
            .Add(New ReportParameter("出場組数", CStr(Parm.出場組数)))
            .Add(New ReportParameter("UP数", CStr(Parm.ピックアップ数)))


            '            If Parm.ヒート数 = 0 Then
            '           .Add(New ReportParameter("ヒート数", ""))
            '          Else
            '         .Add(New ReportParameter("ヒート数", Parm.ヒート数))
            '        End If
            '       .Add(New ReportParameter("出場組数", "出場  " & CStr(Parm.出場組数)))

            'If Parm.ピックアップ数 = 0 Then
            ' .Add(New ReportParameter("UP数", ""))
            ' Else
            ' .Add(New ReportParameter("UP数", Parm.ピックアップ数))
            ' End If
            .Add(New ReportParameter("審判方式", Parm.採点方式))

            .Add(New ReportParameter("種目記号1", Parm.競技種目(1)))
            .Add(New ReportParameter("種目記号2", Parm.競技種目(2)))
            .Add(New ReportParameter("種目記号3", Parm.競技種目(3)))
            .Add(New ReportParameter("種目記号4", Parm.競技種目(4)))
            .Add(New ReportParameter("種目記号5", Parm.競技種目(5)))

            .Add(New ReportParameter("種目種別1", Parm.競技種目2(1)))
            .Add(New ReportParameter("種目種別2", Parm.競技種目2(2)))
            .Add(New ReportParameter("種目種別3", Parm.競技種目2(3)))
            .Add(New ReportParameter("種目種別4", Parm.競技種目2(4)))
            .Add(New ReportParameter("種目種別5", Parm.競技種目2(5)))

            .Add(New ReportParameter("種目記号_色1", Parm.競技種目_色(1)))
            .Add(New ReportParameter("種目記号_色2", Parm.競技種目_色(2)))
            .Add(New ReportParameter("種目記号_色3", Parm.競技種目_色(3)))
            .Add(New ReportParameter("種目記号_色4", Parm.競技種目_色(4)))
            .Add(New ReportParameter("種目記号_色5", Parm.競技種目_色(5)))

            .Add(New ReportParameter("種目種別_色1", Parm.競技種目2_色(1)))
            .Add(New ReportParameter("種目種別_色2", Parm.競技種目2_色(2)))
            .Add(New ReportParameter("種目種別_色3", Parm.競技種目2_色(3)))
            .Add(New ReportParameter("種目種別_色4", Parm.競技種目2_色(4)))
            .Add(New ReportParameter("種目種別_色5", Parm.競技種目2_色(5)))

            .Add(New ReportParameter("配布先", Parm.配布先)）

            '================================

            .Add(New ReportParameter("ヒート01", Parm.ヒートText(1)）)
            .Add(New ReportParameter("ヒート02", Parm.ヒートText(2)）)
            .Add(New ReportParameter("ヒート03", Parm.ヒートText(3)）)
            .Add(New ReportParameter("ヒート04", Parm.ヒートText(4)）)
            .Add(New ReportParameter("ヒート05", Parm.ヒートText(5)）)
            .Add(New ReportParameter("ヒート06", Parm.ヒートText(6)）)
            .Add(New ReportParameter("ヒート07", Parm.ヒートText(7)）)
            .Add(New ReportParameter("ヒート08", Parm.ヒートText(8)）)
            .Add(New ReportParameter("ヒート09", Parm.ヒートText(9)）)
            .Add(New ReportParameter("ヒート10", Parm.ヒートText(10)）)

            .Add(New ReportParameter("ヒート11", Parm.ヒートText(11)）)
            .Add(New ReportParameter("ヒート12", Parm.ヒートText(12)）)
            .Add(New ReportParameter("ヒート13", Parm.ヒートText(13)）)
            .Add(New ReportParameter("ヒート14", Parm.ヒートText(14)）)
            .Add(New ReportParameter("ヒート15", Parm.ヒートText(15)）)
            .Add(New ReportParameter("ヒート16", Parm.ヒートText(16)）)
            .Add(New ReportParameter("ヒート17", Parm.ヒートText(17)）)
            .Add(New ReportParameter("ヒート18", Parm.ヒートText(18)）)
            .Add(New ReportParameter("ヒート19", Parm.ヒートText(19)）)
            .Add(New ReportParameter("ヒート20", Parm.ヒートText(20)）)

            .Add(New ReportParameter("ヒート21", Parm.ヒートText(21)）)
            .Add(New ReportParameter("ヒート22", Parm.ヒートText(22)）)
            .Add(New ReportParameter("ヒート23", Parm.ヒートText(23)）)
            .Add(New ReportParameter("ヒート24", Parm.ヒートText(24)）)




        End With

        report.SetParameters(Parameters)

    End Sub


End Class
