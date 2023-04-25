Friend Class Creature
    Inherits Sprite
    Implements ICreature

    Private ReadOnly _data As EditorData
    Private ReadOnly _name As String
    Private ReadOnly Property CreatureData As CreatureData
        Get
            Return _data.Creatures(_name)
        End Get
    End Property

    Public Sub New(data As EditorData, name As String)
        MyBase.New(data, data.Creatures(name), name)
        Me._data = data
        Me._name = name
    End Sub
End Class
