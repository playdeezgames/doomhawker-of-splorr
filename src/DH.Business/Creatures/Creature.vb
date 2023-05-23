Friend Class Creature
    Inherits Sprite
    Implements ICreature

    Private ReadOnly _data As WorldData
    Private ReadOnly _name As String
    Private ReadOnly Property CreatureData As CreatureData
        Get
            Return _data.Creatures(_name)
        End Get
    End Property

    Public Sub New(data As WorldData, name As String)
        MyBase.New(data, Function() data.Creatures(name), name)
        Me._data = data
        Me._name = name
    End Sub

    Public Function CreateInstance(mapName As String, column As Integer, row As Integer) As ICreatureInstance Implements ICreature.CreateInstance
        Dim mapData = _data.Maps(mapName)
        Dim mapCellData = mapData.Cells(column + row * mapData.Columns)
        mapCellData.Creature = New CreatureInstanceData With
            {
                .CreatureName = Name
            }
        Return New CreatureInstance(_data, mapName, column, row)
    End Function
End Class
