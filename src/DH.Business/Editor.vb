Imports AOS.UI

Public Class Editor
    Implements IEditor
    Private _data As EditorData
    Sub New(data As EditorData)
        _data = data
    End Sub
    Public Function CreateFont(name As String, width As Integer, height As Integer) As IEditorFont Implements IEditor.CreateFont
        Dim fontData As New FontData With
        {
            .Height = height,
            .Glyphs = New Dictionary(Of Char, GlyphData)
        }
        For character = 32 To 127
            Dim glyphData = New GlyphData With
                {
                    .Width = width,
                    .Lines = New Dictionary(Of Integer, IEnumerable(Of Integer))
                }
            For line = 0 To height - 1
                glyphData.Lines(line) = Array.Empty(Of Integer)
            Next
            fontData.Glyphs(ChrW(character)) = glyphData
        Next
        _data.Fonts(name) = fontData
        Return New EditorFont(_data, name)
    End Function

    Public Function GetFont(fontName As String) As IEditorFont Implements IEditor.GetFont
        Return New EditorFont(_data, fontName)
    End Function
End Class
