Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports Microsoft.Reporting.WinForms

Public Class 帳票印刷

    Implements IDisposable


    Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))

        'ev.Graphics.DrawImage(pageImage, ev.PageBounds)

        'ev.PageSettings.Landscape = True   '横

        '中身を描画
        ev.Graphics.DrawImage(pageImage, ev.PageSettings.Bounds)

        '1ページしか印刷しないので、以下はコメントアウト
        'm_currentPageIndex += 1
        'ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
    End Sub

    Private m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)

    Private Function CreateStream(ByVal name As String,
       ByVal fileNameExtension As String,
       ByVal encoding As Encoding, ByVal mimeType As String,
       ByVal willSeek As Boolean) As Stream
        ' Dim stream As Stream =
        '     New FileStream("..\..\" +
        'name + "." + fileNameExtension, FileMode.Create)
        Dim stream As Stream =
            New FileStream(name + "." + fileNameExtension, FileMode.Create)
        m_streams.Add(stream)
        Return stream
    End Function


    '印刷実行

    Public Sub Export(ByVal report As LocalReport, ByVal 横FLAG As Boolean)

        '   "  <PageWidth>11in</PageWidth>" +


        Dim deviceInfo As String =
          "<DeviceInfo>" +
          "  <OutputFormat>EMF</OutputFormat>" +
          "  <PageHeight>8.5in</PageHeight>" +
          "  <MarginTop>0.25in</MarginTop>" +
          "  <MarginLeft>0.25in</MarginLeft>" +
          "  <MarginRight>0.25in</MarginRight>" +
          "  <MarginBottom>0.25in</MarginBottom>" +
          "</DeviceInfo>"
        Dim warnings() As Warning = Nothing
        m_streams = New List(Of Stream)()


        report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)

        Dim stream As Stream
        For Each stream In m_streams
            stream.Position = 0
        Next


        m_currentPageIndex = 0
        Print(横FLAG)


    End Sub


    Private Sub Print(ByVal 横FLAG As Boolean)
        'Const printerName As String = "Microsoft Office Document Image Writer"
        'Const printerName As String = "Adobe PDF"

        'PrintDocumentの作成
        Dim pd As New System.Drawing.Printing.PrintDocument
        'プリンタ名の取得 デフォルト
        Dim defaultPrinterName As String = pd.PrinterSettings.PrinterName

        Dim printerName As String = defaultPrinterName

        If m_streams Is Nothing Or m_streams.Count = 0 Then
            Return
        End If

        Dim pps As PaperSize
        pps = New PaperSize
        pps.PaperName = "A4"


        Dim printDoc As New PrintDocument()
        printDoc.PrinterSettings.PrinterName = printerName
        printDoc.PrinterSettings.DefaultPageSettings.PaperSize = pps

        If 横FLAG = True Then
            printDoc.DefaultPageSettings.Landscape = True   ''これだ！横向き
        Else
            printDoc.DefaultPageSettings.Landscape = False   ''縦向き

        End If


        If Not printDoc.PrinterSettings.IsValid Then
            Dim msg As String = String.Format(
                "Can't find printer ""{0}"".", printerName)
            Console.WriteLine(msg)
            Return
        End If
        AddHandler printDoc.PrintPage, AddressOf PrintPage     'ここで中身を印刷

        Try
            printDoc.Print()
        Catch ex As Exception
            MsgBox("印刷に失敗しました。")
        End Try
    End Sub


    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        If Not (m_streams Is Nothing) Then
            Dim stream As Stream
            For Each stream In m_streams
                stream.Close()
            Next
            m_streams = Nothing
        End If
    End Sub

End Class
