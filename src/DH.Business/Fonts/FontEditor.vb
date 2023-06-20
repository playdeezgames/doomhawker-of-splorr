Friend Class FontEditor
    Inherits Thingies(Of FontData, IEditorFont, String)
    Implements IFontEditor
    Private ReadOnly _data As WorldData
    Public Sub New(data As WorldData)
        MyBase.New(data, Function() data.Fonts, Function(name) New EditorFont(data, name))
        Me._data = data
    End Sub
    Const FirstCharacter = 32
    Const LastCharacter = 127
    Public Function Create(name As String, width As Integer, height As Integer) As IEditorFont Implements IFontEditor.Create
        Dim fontData As New FontData With
        {
            .Height = height,
            .Glyphs = New Dictionary(Of Char, GlyphData)
        }
        For character = FirstCharacter To LastCharacter
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

    Public Sub Export(name As String, fileName As String) Implements IFontEditor.Export
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data.Fonts(name)))
    End Sub
End Class
