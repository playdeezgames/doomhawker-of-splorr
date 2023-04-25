Friend MustInherit Class Sprite
    Implements ISprite
    Private ReadOnly _data As SpriteData
    Private ReadOnly _name As String
    Sub New(data As SpriteData, name As String)
        _data = data
        _name = name
    End Sub

    Public Property FontName As String Implements ISprite.FontName
        Get
            Return _data.FontName
        End Get
        Set(value As String)
            _data.FontName = value
        End Set
    End Property

    Public Property GlyphKey As Char Implements ISprite.GlyphKey
        Get
            Return _data.GlyphKey
        End Get
        Set(value As Char)
            _data.GlyphKey = value
        End Set
    End Property

    Public Property HueIndex As Integer Implements ISprite.HueIndex
        Get
            Return _data.HueIndex
        End Get
        Set(value As Integer)
            _data.HueIndex = value
        End Set
    End Property

    Public MustOverride Property Font As IEditorFont Implements ISprite.Font

    Public ReadOnly Property Name As String Implements ISprite.Name
        Get
            Return _name
        End Get
    End Property
End Class
