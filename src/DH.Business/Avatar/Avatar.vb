Friend Class Avatar
    Implements IAvatar
    Private ReadOnly _data As WorldData
    Public Sub New(data As WorldData)
        _data = data
    End Sub
    Public Property MapName As String Implements IAvatar.MapName
        Get
            Return _data.Avatar.MapName
        End Get
        Set(value As String)
            _data.Avatar.MapName = value
        End Set
    End Property
    Public Property Column As Integer Implements IAvatar.Column
        Get
            Return _data.Avatar.Column
        End Get
        Set(value As Integer)
            _data.Avatar.Column = value
        End Set
    End Property
    Public Property Row As Integer Implements IAvatar.Row
        Get
            Return _data.Avatar.Row
        End Get
        Set(value As Integer)
            _data.Avatar.Row = value
        End Set
    End Property

    Public ReadOnly Property Creature As ICreatureInstance Implements IAvatar.Creature
        Get
            Return New CreatureInstance(_data, MapName, Column, Row)
        End Get
    End Property

    Public Sub Move(direction As Direction) Implements IAvatar.Move
        Dim c = Creature
        c.Move(direction)
        Column = c.Column
        Row = c.Row
    End Sub
End Class
