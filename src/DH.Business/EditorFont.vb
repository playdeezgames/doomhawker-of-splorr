Friend Class EditorFont
    Implements IEditorFont
    Private ReadOnly _data As EditorData
    Private ReadOnly _fontName As String
    Private ReadOnly Property FontData As FontData
        Get
            Return _data.Fonts(_fontName)
        End Get
    End Property

    Public ReadOnly Property Font As Font Implements IEditorFont.Font
        Get
            Return New Font(FontData)
        End Get
    End Property

    Public Sub New(data As EditorData, name As String)
        Me._data = data
        Me._fontName = name
    End Sub
End Class
