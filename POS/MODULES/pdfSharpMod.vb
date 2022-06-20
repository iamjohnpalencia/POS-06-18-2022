Imports System.Xml
Imports PdfSharp
Imports PdfSharp.Drawing

Module pdfSharpMod
    Property PDF_57BrandFont As XFont = New XFont("Tahoma", 7, XFontStyle.Bold)
    Property PDF_57FontDefault As XFont = New XFont("Tahoma", 5)
    Property PDF_57FontDefaultBold As XFont = New XFont("Tahoma", 6, FontStyle.Bold)
    Property PDF_57FontDefaultLine As XFont = New XFont("Tahoma", 6, FontStyle.Bold)
    Property PDF_80BrandFont As XFont = New XFont("Tahoma", 8, XFontStyle.Bold)
    Property PDF_80FontDefault As XFont = New XFont("Tahoma", 6)
    Property PDF_80FontDefaultBold As XFont = New XFont("Tahoma", 7, FontStyle.Bold)
    Property PDF_80FontDefaultLine As XFont = New XFont("Tahoma", 7, FontStyle.Bold)


    Public Sub PDFCenterText(gfx As XGraphics, text As String, textFont As XFont, X As Integer, Y As Integer)
        Dim Format As XStringFormat = New XStringFormat()
        Dim rect As XRect = New XRect(X, Y, 250, 10)
        Dim Brush As XBrush = XBrushes.Black
        Format.Alignment = XStringAlignment.Center
        gfx.DrawString(text, textFont, Brush, rect, Format)
    End Sub

    Public Sub PDFRightToLeft(gfx As XGraphics, textLeft As String, textRight As String, textFont As XFont, X As Integer, Y As Integer)
        Dim Format As XStringFormat = New XStringFormat()
        Dim rect As XRect = New XRect(X, Y, 250, 10)
        Dim Brush As XBrush = XBrushes.Black

        Format.LineAlignment = XLineAlignment.Center
        Format.Alignment = XStringAlignment.Near
        gfx.DrawString(textLeft, textFont, Brush, rect, Format)
        Format.Alignment = XStringAlignment.Far
        Format.LineAlignment = XLineAlignment.Center
        gfx.DrawString(textRight, textFont, Brush, rect, Format)
    End Sub

    Public Sub PDFSimpleText(gfx As XGraphics, text As String, textFont As XFont, X As Integer, Y As Integer)
        Dim Format As XStringFormat = New XStringFormat()
        Dim rect As XRect = New XRect(X, Y, 250, 10)
        Dim Brush As XBrush = XBrushes.Black
        Format.LineAlignment = XLineAlignment.Center
        Format.Alignment = XStringAlignment.Near
        gfx.DrawString(text, textFont, Brush, rect, Format)
    End Sub

    Public Sub CreateXMLFile(ByVal Position As String, ByRef LoopText As String(), ByVal writer As XmlTextWriter, TextFont As Boolean)
        writer.WriteStartElement("Transaction")
        writer.WriteStartElement("Position")
        writer.WriteString(Position)
        writer.WriteEndElement()
        Dim TxtPos As Integer = 1
        For Each TxtDisp As String In LoopText
            Select Case TxtPos
                Case 1
                    writer.WriteStartElement("Textleft")
                    writer.WriteString(TxtDisp)
                    writer.WriteEndElement()
                Case 2
                    writer.WriteStartElement("TextMiddleRight")
                    writer.WriteString(TxtDisp)
                    writer.WriteEndElement()
                Case 3
                    writer.WriteStartElement("TextMiddleLeft")
                    writer.WriteString(TxtDisp)
                    writer.WriteEndElement()
                Case 4
                    writer.WriteStartElement("TextRight")
                    writer.WriteString(TxtDisp)
                    writer.WriteEndElement()
            End Select
            TxtPos += 1
        Next
        TxtPos = 1
        writer.WriteStartElement("TextFont")
        writer.WriteString(If(TextFont, "BOLD", "NORMAL"))
        writer.WriteEndElement()
        writer.WriteEndElement()
    End Sub
End Module
