﻿Friend Class EditorFont
    Implements IEditorFont
    Private ReadOnly _data As WorldData
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

    Public ReadOnly Property FontName As String Implements IEditorFont.FontName
        Get
            Return _fontName
        End Get
    End Property

    Public Sub New(data As WorldData, name As String)
        Me._data = data
        Me._fontName = name
    End Sub

    Public Function GetGlyph(glyphKey As Char) As IEditorGlyph Implements IEditorFont.GetGlyph
        Return New EditorGlyph(_data, _fontName, glyphKey)
    End Function
End Class
