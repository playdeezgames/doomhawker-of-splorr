Public Class Editor
    Implements IEditor
    Private _data As EditorData
    Sub New(data As EditorData)
        _data = data
    End Sub
    Const FirstCharacter = 32
    Const LastCharacter = 127

    Public ReadOnly Property FontNames As IEnumerable(Of String) Implements IEditor.FontNames
        Get
            Return _data.Fonts.Keys
        End Get
    End Property

    Public Function CreateFont(name As String, width As Integer, height As Integer) As IEditorFont Implements IEditor.CreateFont
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

    Public Function GetFont(fontName As String) As IEditorFont Implements IEditor.GetFont
        Return New EditorFont(_data, fontName)
    End Function

    Public Sub Save(fileName As String) Implements IEditor.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data))
    End Sub

    Public Sub Load(fileName As String) Implements IEditor.Load
        _data = JsonSerializer.Deserialize(Of EditorData)(File.ReadAllText(fileName))
    End Sub
End Class
