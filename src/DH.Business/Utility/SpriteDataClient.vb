Friend MustInherit Class SpriteDataClient
    Inherits WorldDataClient
    Private ReadOnly _spriteDataSource As Func(Of WorldData, String, SpriteData)
    Protected ReadOnly _spriteName As String
    Protected Sub New(data As WorldData, spriteData As Func(Of WorldData, String, SpriteData), name As String)
        MyBase.New(data)
        _spriteDataSource = spriteData
        _spriteName = name
    End Sub
    Protected ReadOnly Property SpriteData As SpriteData
        Get
            Return _spriteDataSource(WorldData, _spriteName)
        End Get
    End Property
End Class
