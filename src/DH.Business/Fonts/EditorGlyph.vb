Friend Class EditorGlyph
    Implements IEditorGlyph

    Private ReadOnly _data As EditorData
    Private ReadOnly _fontName As String
    Private ReadOnly _glyphKey As Char
    Private ReadOnly Property FontData As FontData
        Get
            Return _data.Fonts(_fontName)
        End Get
    End Property
    Private ReadOnly Property GlyphData As GlyphData
        Get
            Return FontData.Glyphs(_glyphKey)
        End Get
    End Property

    Public Sub New(data As EditorData, fontName As String, glyphKey As Char)
        _data = data
        _fontName = fontName
        _glyphKey = glyphKey
    End Sub

    Public ReadOnly Property Width As Integer Implements IEditorGlyph.Width
        Get
            Return GlyphData.Width
        End Get
    End Property

    Public ReadOnly Property Height As Integer Implements IEditorGlyph.Height
        Get
            Return FontData.Height
        End Get
    End Property

    Public Function IsSet(x As Integer, y As Integer) As Boolean Implements IEditorGlyph.IsSet
        Return GlyphData.Lines(y).Contains(x)
    End Function

    Public Sub Toggle(x As Integer, y As Integer) Implements IEditorGlyph.Toggle
        If IsSet(x, y) Then
            GlyphData.Lines(y) = GlyphData.Lines(y).Where(Function(v) v <> x)
        Else
            GlyphData.Lines(y) = (New HashSet(Of Integer)(GlyphData.Lines(y)) From {x})
        End If
    End Sub
End Class
