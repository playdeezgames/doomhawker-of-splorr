Friend Class Creature
    Inherits Sprite
    Implements ICreature
    Private ReadOnly Property CreatureData As CreatureData
        Get
            Return WorldData.Creatures(_spriteName)
        End Get
    End Property

    Public Sub New(data As WorldData, name As String)
        MyBase.New(data, Function(w, n) w.Creatures(n), name)
    End Sub

    Public Function CreateInstance(mapName As String, column As Integer, row As Integer) As ICreatureInstance Implements ICreature.CreateInstance
        Dim mapData = WorldData.Maps(mapName)
        Dim mapCellData = mapData.Cells(column + row * mapData.Columns)
        mapCellData.Creature = New CreatureInstanceData With
            {
                .CreatureName = Name
            }
        Return New CreatureInstance(WorldData, mapName, column, row)
    End Function
End Class
