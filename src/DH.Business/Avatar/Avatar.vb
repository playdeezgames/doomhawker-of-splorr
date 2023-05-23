Friend Class Avatar
    Inherits AvatarDataClient
    Implements IAvatar
    Public Sub New(data As WorldData)
        MyBase.New(data)
    End Sub
    Public Property MapName As String Implements IAvatar.MapName
        Get
            Return AvatarData.MapName
        End Get
        Set(value As String)
            AvatarData.MapName = value
        End Set
    End Property
    Public Property Column As Integer Implements IAvatar.Column
        Get
            Return AvatarData.Column
        End Get
        Set(value As Integer)
            AvatarData.Column = value
        End Set
    End Property
    Public Property Row As Integer Implements IAvatar.Row
        Get
            Return AvatarData.Row
        End Get
        Set(value As Integer)
            AvatarData.Row = value
        End Set
    End Property
    Public ReadOnly Property Creature As ICreatureInstance Implements IAvatar.Creature
        Get
            Return New CreatureInstance(WorldData, MapName, Column, Row)
        End Get
    End Property
    Public Sub Move(direction As Direction) Implements IAvatar.Move
        Dim c = Creature
        c.Move(direction)
        MapName = c.MapName
        Column = c.Column
        Row = c.Row
    End Sub
End Class
