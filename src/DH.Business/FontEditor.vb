Friend Class FontEditor
    Implements IFontEditor

    Private _data As EditorData

    Public Sub New(data As EditorData)
        Me._data = data
    End Sub

    Public ReadOnly Property HasAny As Boolean Implements IFontEditor.HasAny
        Get
            Return _data.Fonts.Any
        End Get
    End Property

    Public ReadOnly Property Names As IEnumerable(Of String) Implements IFontEditor.Names
        Get
            Return _data.Fonts.Keys
        End Get
    End Property

    Public Sub Rename(fromName As String, toName As String) Implements IFontEditor.Rename
        Dim temp = _data.Fonts(fromName)
        _data.Fonts.Remove(fromName)
        _data.Fonts.Add(toName, temp)
    End Sub

    Public Sub Clone(fromName As String, toName As String) Implements IFontEditor.Clone
        Dim temp = _data.Fonts(fromName)
        _data.Fonts.Add(toName, temp)
    End Sub

    Public Sub Delete(name As String) Implements IFontEditor.Delete
        _data.Fonts.Remove(name)
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

    Public Function Retrieve(name As String) As IEditorFont Implements IFontEditor.Retrieve
        Return New EditorFont(_data, name)
    End Function

    Public Function Has(name As String) As Boolean Implements IFontEditor.Has
        Return _data.Fonts.ContainsKey(name)
    End Function
End Class
