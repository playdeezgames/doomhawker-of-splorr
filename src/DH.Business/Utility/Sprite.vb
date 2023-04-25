﻿Friend MustInherit Class Sprite
    Implements ISprite
    Private ReadOnly _data As EditorData
    Private ReadOnly _spriteData As SpriteData
    Private ReadOnly _name As String
    Sub New(data As EditorData, spriteData As SpriteData, name As String)
        _data = data
        _spriteData = spriteData
        _name = name
    End Sub

    Public Property FontName As String Implements ISprite.FontName
        Get
            Return _spriteData.FontName
        End Get
        Set(value As String)
            _spriteData.FontName = value
        End Set
    End Property

    Public Property GlyphKey As Char Implements ISprite.GlyphKey
        Get
            Return _spriteData.GlyphKey
        End Get
        Set(value As Char)
            _spriteData.GlyphKey = value
        End Set
    End Property

    Public Property HueIndex As Integer Implements ISprite.HueIndex
        Get
            Return _spriteData.HueIndex
        End Get
        Set(value As Integer)
            _spriteData.HueIndex = value
        End Set
    End Property

    Public Property Font As IEditorFont Implements ISprite.Font
        Get
            If _data.Fonts.ContainsKey(_spriteData.FontName) Then
                Return New EditorFont(_data, _spriteData.FontName)
            End If
            Return Nothing
        End Get
        Set(value As IEditorFont)
            If value IsNot Nothing Then
                FontName = value.FontName
            Else
                FontName = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property Name As String Implements ISprite.Name
        Get
            Return _name
        End Get
    End Property
End Class