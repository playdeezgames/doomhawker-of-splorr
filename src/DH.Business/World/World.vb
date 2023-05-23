Public Class World
    Inherits WorldDataClient
    Implements IWorld
    Sub New(data As WorldData)
        MyBase.New(data)
    End Sub
    Public Property MapCellWidth As Integer Implements IWorld.MapCellWidth
        Get
            Return WorldData.MapCellWidth
        End Get
        Set(value As Integer)
            WorldData.MapCellWidth = value
        End Set
    End Property
    Public Property MapCellHeight As Integer Implements IWorld.MapCellHeight
        Get
            Return WorldData.MapCellHeight
        End Get
        Set(value As Integer)
            WorldData.MapCellHeight = value
        End Set
    End Property
    Public ReadOnly Property Fonts As IFontEditor Implements IWorld.Fonts
        Get
            Return New FontEditor(WorldData)
        End Get
    End Property

    Public ReadOnly Property Terrains As ITerrainEditor Implements IWorld.Terrains
        Get
            Return New TerrainEditor(WorldData)
        End Get
    End Property

    Public ReadOnly Property Maps As IMapEditor Implements IWorld.Maps
        Get
            Return New MapEditor(WorldData)
        End Get
    End Property

    Public ReadOnly Property Items As IItemEditor Implements IWorld.Items
        Get
            Return New ItemEditor(WorldData)
        End Get
    End Property

    Public ReadOnly Property Creatures As ICreatures Implements IWorld.Creatures
        Get
            Return New Creatures(WorldData)
        End Get
    End Property

    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If WorldData.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(WorldData)
        End Get
    End Property

    Public ReadOnly Property Colors As IColors Implements IWorld.Colors
        Get
            Return New Colors(WorldData)
        End Get
    End Property

    Public Sub Save(fileName As String) Implements IWorld.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(WorldData))
    End Sub
    Public Sub Load(fileName As String) Implements IWorld.Load
        WorldData = JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(fileName))
    End Sub

    Public Sub ClearAvatar() Implements IWorld.ClearAvatar
        WorldData.Avatar = Nothing
    End Sub

    Public Sub SetAvatar(creature As ICreatureInstance) Implements IWorld.SetAvatar
        WorldData.Avatar = New AvatarData With
            {
                .MapName = creature.MapName,
                .Column = creature.Column,
                .Row = creature.Row
            }
    End Sub
End Class