Friend MustInherit Class Sprite
    Implements ISprite
    Private ReadOnly _data As WorldData
    Private ReadOnly _spriteData As Func(Of WorldData, String, SpriteData)
    Private ReadOnly _name As String
    Sub New(data As WorldData, spriteData As Func(Of WorldData, String, SpriteData), name As String)
        _data = data
        _spriteData = spriteData
        _name = name
    End Sub

    Public Property FontName As String Implements ISprite.FontName
        Get
            Return _spriteData(_data, _name).FontName
        End Get
        Set(value As String)
            _spriteData(_data, _name).FontName = value
        End Set
    End Property

    Public Property GlyphKey As Char Implements ISprite.GlyphKey
        Get
            Return _spriteData(_data, _name).GlyphKey
        End Get
        Set(value As Char)
            _spriteData(_data, _name).GlyphKey = value
        End Set
    End Property

    Public Property Hue As String Implements ISprite.Hue
        Get
            Return _spriteData(_data, _name).Hue
        End Get
        Set(value As String)
            _spriteData(_data, _name).Hue = value
        End Set
    End Property

    Public Property Font As IEditorFont Implements ISprite.Font
        Get
            If _data.Fonts.ContainsKey(_spriteData(_data, _name).FontName) Then
                Return New EditorFont(_data, _spriteData(_data, _name).FontName)
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
