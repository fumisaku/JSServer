Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports Microsoft.Reporting.WinForms


Public Class RPT_R1_ヒート表マスタ

    Private report As LocalReport

    Private 帳票印刷 As 帳票印刷

    Public Sub New()

        report = New LocalReport()
        'report.ReportPath = "..\..\帳票\RPT_R1_ヒート表マスタ.rdlc"
        report.ReportPath = "Report\RPT_R1_ヒート表マスタ.rdlc"

        帳票印刷 = New 帳票印刷


    End Sub

    Public Sub SetParm(Parm As RPT_Parm_H1)

        Set共通項目(Parm)


    End Sub

    Public Sub 印刷実行()

        帳票印刷.Export(report, True)  '横向き

        帳票印刷.Dispose()

    End Sub


    Private Sub Set共通項目(Parm As RPT_Parm_H1)



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



            'If Parm.ヒート数 = 0 Then
            ' .Add(New ReportParameter("ヒート数", ""))
            'Else
            '.Add(New ReportParameter("ヒート数", Parm.ヒート数))
            'End If
            '.Add(New ReportParameter("出場組数", "出場  " & CStr(Parm.出場組数)))

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

            .Add(New ReportParameter("背番号1_01", Parm.ヒート(1).背番号(1)))
            .Add(New ReportParameter("背番号1_02", Parm.ヒート(1).背番号(2)))
            .Add(New ReportParameter("背番号1_03", Parm.ヒート(1).背番号(3)))
            .Add(New ReportParameter("背番号1_04", Parm.ヒート(1).背番号(4)))
            .Add(New ReportParameter("背番号1_05", Parm.ヒート(1).背番号(5)))
            .Add(New ReportParameter("背番号1_06", Parm.ヒート(1).背番号(6)))
            .Add(New ReportParameter("背番号1_07", Parm.ヒート(1).背番号(7)))
            .Add(New ReportParameter("背番号1_08", Parm.ヒート(1).背番号(8)))
            .Add(New ReportParameter("背番号1_09", Parm.ヒート(1).背番号(9)))
            .Add(New ReportParameter("背番号1_10", Parm.ヒート(1).背番号(10)))

            .Add(New ReportParameter("背番号1_11", Parm.ヒート(1).背番号(11)))
            .Add(New ReportParameter("背番号1_12", Parm.ヒート(1).背番号(12)))
            .Add(New ReportParameter("背番号1_13", Parm.ヒート(1).背番号(13)))
            .Add(New ReportParameter("背番号1_14", Parm.ヒート(1).背番号(14)))
            .Add(New ReportParameter("背番号1_15", Parm.ヒート(1).背番号(15)))
            .Add(New ReportParameter("背番号1_16", Parm.ヒート(1).背番号(16)))
            .Add(New ReportParameter("背番号1_17", Parm.ヒート(1).背番号(17)))
            .Add(New ReportParameter("背番号1_18", Parm.ヒート(1).背番号(18)))
            .Add(New ReportParameter("背番号1_19", Parm.ヒート(1).背番号(19)))
            .Add(New ReportParameter("背番号1_20", Parm.ヒート(1).背番号(20)))

            .Add(New ReportParameter("選手名1_01", Parm.ヒート(1).選手名(1)))
            .Add(New ReportParameter("選手名1_02", Parm.ヒート(1).選手名(2)))
            .Add(New ReportParameter("選手名1_03", Parm.ヒート(1).選手名(3)))
            .Add(New ReportParameter("選手名1_04", Parm.ヒート(1).選手名(4)))
            .Add(New ReportParameter("選手名1_05", Parm.ヒート(1).選手名(5)))
            .Add(New ReportParameter("選手名1_06", Parm.ヒート(1).選手名(6)))
            .Add(New ReportParameter("選手名1_07", Parm.ヒート(1).選手名(7)))
            .Add(New ReportParameter("選手名1_08", Parm.ヒート(1).選手名(8)))
            .Add(New ReportParameter("選手名1_09", Parm.ヒート(1).選手名(9)))
            .Add(New ReportParameter("選手名1_10", Parm.ヒート(1).選手名(10)))

            .Add(New ReportParameter("選手名1_11", Parm.ヒート(1).選手名(11)))
            .Add(New ReportParameter("選手名1_12", Parm.ヒート(1).選手名(12)))
            .Add(New ReportParameter("選手名1_13", Parm.ヒート(1).選手名(13)))
            .Add(New ReportParameter("選手名1_14", Parm.ヒート(1).選手名(14)))
            .Add(New ReportParameter("選手名1_15", Parm.ヒート(1).選手名(15)))
            .Add(New ReportParameter("選手名1_16", Parm.ヒート(1).選手名(16)))
            .Add(New ReportParameter("選手名1_17", Parm.ヒート(1).選手名(17)))
            .Add(New ReportParameter("選手名1_18", Parm.ヒート(1).選手名(18)))
            .Add(New ReportParameter("選手名1_19", Parm.ヒート(1).選手名(19)))
            .Add(New ReportParameter("選手名1_20", Parm.ヒート(1).選手名(20)))

            '2 Heat
            .Add(New ReportParameter("背番号2_01", Parm.ヒート(2).背番号(1)))
            .Add(New ReportParameter("背番号2_02", Parm.ヒート(2).背番号(2)))
            .Add(New ReportParameter("背番号2_03", Parm.ヒート(2).背番号(3)))
            .Add(New ReportParameter("背番号2_04", Parm.ヒート(2).背番号(4)))
            .Add(New ReportParameter("背番号2_05", Parm.ヒート(2).背番号(5)))
            .Add(New ReportParameter("背番号2_06", Parm.ヒート(2).背番号(6)))
            .Add(New ReportParameter("背番号2_07", Parm.ヒート(2).背番号(7)))
            .Add(New ReportParameter("背番号2_08", Parm.ヒート(2).背番号(8)))
            .Add(New ReportParameter("背番号2_09", Parm.ヒート(2).背番号(9)))
            .Add(New ReportParameter("背番号2_10", Parm.ヒート(2).背番号(10)))

            .Add(New ReportParameter("背番号2_11", Parm.ヒート(2).背番号(11)))
            .Add(New ReportParameter("背番号2_12", Parm.ヒート(2).背番号(12)))
            .Add(New ReportParameter("背番号2_13", Parm.ヒート(2).背番号(13)))
            .Add(New ReportParameter("背番号2_14", Parm.ヒート(2).背番号(14)))
            .Add(New ReportParameter("背番号2_15", Parm.ヒート(2).背番号(15)))
            .Add(New ReportParameter("背番号2_16", Parm.ヒート(2).背番号(16)))
            .Add(New ReportParameter("背番号2_17", Parm.ヒート(2).背番号(17)))
            .Add(New ReportParameter("背番号2_18", Parm.ヒート(2).背番号(18)))
            .Add(New ReportParameter("背番号2_19", Parm.ヒート(2).背番号(19)))
            .Add(New ReportParameter("背番号2_20", Parm.ヒート(2).背番号(20)))

            .Add(New ReportParameter("選手名2_01", Parm.ヒート(2).選手名(1)))
            .Add(New ReportParameter("選手名2_02", Parm.ヒート(2).選手名(2)))
            .Add(New ReportParameter("選手名2_03", Parm.ヒート(2).選手名(3)))
            .Add(New ReportParameter("選手名2_04", Parm.ヒート(2).選手名(4)))
            .Add(New ReportParameter("選手名2_05", Parm.ヒート(2).選手名(5)))
            .Add(New ReportParameter("選手名2_06", Parm.ヒート(2).選手名(6)))
            .Add(New ReportParameter("選手名2_07", Parm.ヒート(2).選手名(7)))
            .Add(New ReportParameter("選手名2_08", Parm.ヒート(2).選手名(8)))
            .Add(New ReportParameter("選手名2_09", Parm.ヒート(2).選手名(9)))
            .Add(New ReportParameter("選手名2_10", Parm.ヒート(2).選手名(10)))

            .Add(New ReportParameter("選手名2_11", Parm.ヒート(2).選手名(11)))
            .Add(New ReportParameter("選手名2_12", Parm.ヒート(2).選手名(12)))
            .Add(New ReportParameter("選手名2_13", Parm.ヒート(2).選手名(13)))
            .Add(New ReportParameter("選手名2_14", Parm.ヒート(2).選手名(14)))
            .Add(New ReportParameter("選手名2_15", Parm.ヒート(2).選手名(15)))
            .Add(New ReportParameter("選手名2_16", Parm.ヒート(2).選手名(16)))
            .Add(New ReportParameter("選手名2_17", Parm.ヒート(2).選手名(17)))
            .Add(New ReportParameter("選手名2_18", Parm.ヒート(2).選手名(18)))
            .Add(New ReportParameter("選手名2_19", Parm.ヒート(2).選手名(19)))
            .Add(New ReportParameter("選手名2_20", Parm.ヒート(2).選手名(20)))

            '3heat
            .Add(New ReportParameter("背番号3_01", Parm.ヒート(3).背番号(1)))
            .Add(New ReportParameter("背番号3_02", Parm.ヒート(3).背番号(2)))
            .Add(New ReportParameter("背番号3_03", Parm.ヒート(3).背番号(3)))
            .Add(New ReportParameter("背番号3_04", Parm.ヒート(3).背番号(4)))
            .Add(New ReportParameter("背番号3_05", Parm.ヒート(3).背番号(5)))
            .Add(New ReportParameter("背番号3_06", Parm.ヒート(3).背番号(6)))
            .Add(New ReportParameter("背番号3_07", Parm.ヒート(3).背番号(7)))
            .Add(New ReportParameter("背番号3_08", Parm.ヒート(3).背番号(8)))
            .Add(New ReportParameter("背番号3_09", Parm.ヒート(3).背番号(9)))
            .Add(New ReportParameter("背番号3_10", Parm.ヒート(3).背番号(10)))

            .Add(New ReportParameter("背番号3_11", Parm.ヒート(3).背番号(11)))
            .Add(New ReportParameter("背番号3_12", Parm.ヒート(3).背番号(12)))
            .Add(New ReportParameter("背番号3_13", Parm.ヒート(3).背番号(13)))
            .Add(New ReportParameter("背番号3_14", Parm.ヒート(3).背番号(14)))
            .Add(New ReportParameter("背番号3_15", Parm.ヒート(3).背番号(15)))
            .Add(New ReportParameter("背番号3_16", Parm.ヒート(3).背番号(16)))
            .Add(New ReportParameter("背番号3_17", Parm.ヒート(3).背番号(17)))
            .Add(New ReportParameter("背番号3_18", Parm.ヒート(3).背番号(18)))
            .Add(New ReportParameter("背番号3_19", Parm.ヒート(3).背番号(19)))
            .Add(New ReportParameter("背番号3_20", Parm.ヒート(3).背番号(20)))

            .Add(New ReportParameter("選手名3_01", Parm.ヒート(3).選手名(1)))
            .Add(New ReportParameter("選手名3_02", Parm.ヒート(3).選手名(2)))
            .Add(New ReportParameter("選手名3_03", Parm.ヒート(3).選手名(3)))
            .Add(New ReportParameter("選手名3_04", Parm.ヒート(3).選手名(4)))
            .Add(New ReportParameter("選手名3_05", Parm.ヒート(3).選手名(5)))
            .Add(New ReportParameter("選手名3_06", Parm.ヒート(3).選手名(6)))
            .Add(New ReportParameter("選手名3_07", Parm.ヒート(3).選手名(7)))
            .Add(New ReportParameter("選手名3_08", Parm.ヒート(3).選手名(8)))
            .Add(New ReportParameter("選手名3_09", Parm.ヒート(3).選手名(9)))
            .Add(New ReportParameter("選手名3_10", Parm.ヒート(3).選手名(10)))

            .Add(New ReportParameter("選手名3_11", Parm.ヒート(3).選手名(11)))
            .Add(New ReportParameter("選手名3_12", Parm.ヒート(3).選手名(12)))
            .Add(New ReportParameter("選手名3_13", Parm.ヒート(3).選手名(13)))
            .Add(New ReportParameter("選手名3_14", Parm.ヒート(3).選手名(14)))
            .Add(New ReportParameter("選手名3_15", Parm.ヒート(3).選手名(15)))
            .Add(New ReportParameter("選手名3_16", Parm.ヒート(3).選手名(16)))
            .Add(New ReportParameter("選手名3_17", Parm.ヒート(3).選手名(17)))
            .Add(New ReportParameter("選手名3_18", Parm.ヒート(3).選手名(18)))
            .Add(New ReportParameter("選手名3_19", Parm.ヒート(3).選手名(19)))
            .Add(New ReportParameter("選手名3_20", Parm.ヒート(3).選手名(20)))

            '4 heat
            .Add(New ReportParameter("背番号4_01", Parm.ヒート(4).背番号(1)))
            .Add(New ReportParameter("背番号4_02", Parm.ヒート(4).背番号(2)))
            .Add(New ReportParameter("背番号4_03", Parm.ヒート(4).背番号(3)))
            .Add(New ReportParameter("背番号4_04", Parm.ヒート(4).背番号(4)))
            .Add(New ReportParameter("背番号4_05", Parm.ヒート(4).背番号(5)))
            .Add(New ReportParameter("背番号4_06", Parm.ヒート(4).背番号(6)))
            .Add(New ReportParameter("背番号4_07", Parm.ヒート(4).背番号(7)))
            .Add(New ReportParameter("背番号4_08", Parm.ヒート(4).背番号(8)))
            .Add(New ReportParameter("背番号4_09", Parm.ヒート(4).背番号(9)))
            .Add(New ReportParameter("背番号4_10", Parm.ヒート(4).背番号(10)))

            .Add(New ReportParameter("背番号4_11", Parm.ヒート(4).背番号(11)))
            .Add(New ReportParameter("背番号4_12", Parm.ヒート(4).背番号(12)))
            .Add(New ReportParameter("背番号4_13", Parm.ヒート(4).背番号(13)))
            .Add(New ReportParameter("背番号4_14", Parm.ヒート(4).背番号(14)))
            .Add(New ReportParameter("背番号4_15", Parm.ヒート(4).背番号(15)))
            .Add(New ReportParameter("背番号4_16", Parm.ヒート(4).背番号(16)))
            .Add(New ReportParameter("背番号4_17", Parm.ヒート(4).背番号(17)))
            .Add(New ReportParameter("背番号4_18", Parm.ヒート(4).背番号(18)))
            .Add(New ReportParameter("背番号4_19", Parm.ヒート(4).背番号(19)))
            .Add(New ReportParameter("背番号4_20", Parm.ヒート(4).背番号(20)))

            .Add(New ReportParameter("選手名4_01", Parm.ヒート(4).選手名(1)))
            .Add(New ReportParameter("選手名4_02", Parm.ヒート(4).選手名(2)))
            .Add(New ReportParameter("選手名4_03", Parm.ヒート(4).選手名(3)))
            .Add(New ReportParameter("選手名4_04", Parm.ヒート(4).選手名(4)))
            .Add(New ReportParameter("選手名4_05", Parm.ヒート(4).選手名(5)))
            .Add(New ReportParameter("選手名4_06", Parm.ヒート(4).選手名(6)))
            .Add(New ReportParameter("選手名4_07", Parm.ヒート(4).選手名(7)))
            .Add(New ReportParameter("選手名4_08", Parm.ヒート(4).選手名(8)))
            .Add(New ReportParameter("選手名4_09", Parm.ヒート(4).選手名(9)))
            .Add(New ReportParameter("選手名4_10", Parm.ヒート(4).選手名(10)))

            .Add(New ReportParameter("選手名4_11", Parm.ヒート(4).選手名(11)))
            .Add(New ReportParameter("選手名4_12", Parm.ヒート(4).選手名(12)))
            .Add(New ReportParameter("選手名4_13", Parm.ヒート(4).選手名(13)))
            .Add(New ReportParameter("選手名4_14", Parm.ヒート(4).選手名(14)))
            .Add(New ReportParameter("選手名4_15", Parm.ヒート(4).選手名(15)))
            .Add(New ReportParameter("選手名4_16", Parm.ヒート(4).選手名(16)))
            .Add(New ReportParameter("選手名4_17", Parm.ヒート(4).選手名(17)))
            .Add(New ReportParameter("選手名4_18", Parm.ヒート(4).選手名(18)))
            .Add(New ReportParameter("選手名4_19", Parm.ヒート(4).選手名(19)))
            .Add(New ReportParameter("選手名4_20", Parm.ヒート(4).選手名(20)))


            '5 heat
            .Add(New ReportParameter("背番号5_01", Parm.ヒート(5).背番号(1)))
            .Add(New ReportParameter("背番号5_02", Parm.ヒート(5).背番号(2)))
            .Add(New ReportParameter("背番号5_03", Parm.ヒート(5).背番号(3)))
            .Add(New ReportParameter("背番号5_04", Parm.ヒート(5).背番号(4)))
            .Add(New ReportParameter("背番号5_05", Parm.ヒート(5).背番号(5)))
            .Add(New ReportParameter("背番号5_06", Parm.ヒート(5).背番号(6)))
            .Add(New ReportParameter("背番号5_07", Parm.ヒート(5).背番号(7)))
            .Add(New ReportParameter("背番号5_08", Parm.ヒート(5).背番号(8)))
            .Add(New ReportParameter("背番号5_09", Parm.ヒート(5).背番号(9)))
            .Add(New ReportParameter("背番号5_10", Parm.ヒート(5).背番号(10)))

            .Add(New ReportParameter("背番号5_11", Parm.ヒート(5).背番号(11)))
            .Add(New ReportParameter("背番号5_12", Parm.ヒート(5).背番号(12)))
            .Add(New ReportParameter("背番号5_13", Parm.ヒート(5).背番号(13)))
            .Add(New ReportParameter("背番号5_14", Parm.ヒート(5).背番号(14)))
            .Add(New ReportParameter("背番号5_15", Parm.ヒート(5).背番号(15)))
            .Add(New ReportParameter("背番号5_16", Parm.ヒート(5).背番号(16)))
            .Add(New ReportParameter("背番号5_17", Parm.ヒート(5).背番号(17)))
            .Add(New ReportParameter("背番号5_18", Parm.ヒート(5).背番号(18)))
            .Add(New ReportParameter("背番号5_19", Parm.ヒート(5).背番号(19)))
            .Add(New ReportParameter("背番号5_20", Parm.ヒート(5).背番号(20)))

            .Add(New ReportParameter("選手名5_01", Parm.ヒート(5).選手名(1)))
            .Add(New ReportParameter("選手名5_02", Parm.ヒート(5).選手名(2)))
            .Add(New ReportParameter("選手名5_03", Parm.ヒート(5).選手名(3)))
            .Add(New ReportParameter("選手名5_04", Parm.ヒート(5).選手名(4)))
            .Add(New ReportParameter("選手名5_05", Parm.ヒート(5).選手名(5)))
            .Add(New ReportParameter("選手名5_06", Parm.ヒート(5).選手名(6)))
            .Add(New ReportParameter("選手名5_07", Parm.ヒート(5).選手名(7)))
            .Add(New ReportParameter("選手名5_08", Parm.ヒート(5).選手名(8)))
            .Add(New ReportParameter("選手名5_09", Parm.ヒート(5).選手名(9)))
            .Add(New ReportParameter("選手名5_10", Parm.ヒート(5).選手名(10)))

            .Add(New ReportParameter("選手名5_11", Parm.ヒート(5).選手名(11)))
            .Add(New ReportParameter("選手名5_12", Parm.ヒート(5).選手名(12)))
            .Add(New ReportParameter("選手名5_13", Parm.ヒート(5).選手名(13)))
            .Add(New ReportParameter("選手名5_14", Parm.ヒート(5).選手名(14)))
            .Add(New ReportParameter("選手名5_15", Parm.ヒート(5).選手名(15)))
            .Add(New ReportParameter("選手名5_16", Parm.ヒート(5).選手名(16)))
            .Add(New ReportParameter("選手名5_17", Parm.ヒート(5).選手名(17)))
            .Add(New ReportParameter("選手名5_18", Parm.ヒート(5).選手名(18)))
            .Add(New ReportParameter("選手名5_19", Parm.ヒート(5).選手名(19)))
            .Add(New ReportParameter("選手名5_20", Parm.ヒート(5).選手名(20)))

            .Add(New ReportParameter("ヒート番号1", Parm.ヒート番号(1)))
            .Add(New ReportParameter("ヒート番号2", Parm.ヒート番号(2)))
            .Add(New ReportParameter("ヒート番号3", Parm.ヒート番号(3)))
            .Add(New ReportParameter("ヒート番号4", Parm.ヒート番号(4)))
            .Add(New ReportParameter("ヒート番号5", Parm.ヒート番号(5)))

        End With

        report.SetParameters(Parameters)

    End Sub


End Class
