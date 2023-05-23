Friend MustInherit Class Sprite
    Inherits SpriteDataClient
    Implements ISprite
    Sub New(data As WorldData, spriteData As Func(Of WorldData, String, SpriteData), name As String)
        MyBase.New(data, spriteData, name)
    End Sub

    Public Property FontName As String Implements ISprite.FontName
        Get
            Return SpriteData.FontName
        End Get
        Set(value As String)
            SpriteData.FontName = value
        End Set
    End Property

    Public Property GlyphKey As Char Implements ISprite.GlyphKey
        Get
            Return SpriteData.GlyphKey
        End Get
        Set(value As Char)
            SpriteData.GlyphKey = value
        End Set
    End Property

    Public Property Hue As String Implements ISprite.Hue
        Get
            Return SpriteData.Hue
        End Get
        Set(value As String)
            SpriteData.Hue = value
        End Set
    End Property

    Public Property Font As IEditorFont Implements ISprite.Font
        Get
            If WorldData.Fonts.ContainsKey(SpriteData.FontName) Then
                Return New EditorFont(WorldData, SpriteData.FontName)
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
            Return _spriteName
        End Get
    End Property
End Class
