Public Class World
    Implements IWorld
    Private _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public Property MapCellWidth As Integer Implements IWorld.MapCellWidth
        Get
            Return _data.MapCellWidth
        End Get
        Set(value As Integer)
            _data.MapCellWidth = value
        End Set
    End Property
    Public Property MapCellHeight As Integer Implements IWorld.MapCellHeight
        Get
            Return _data.MapCellHeight
        End Get
        Set(value As Integer)
            _data.MapCellHeight = value
        End Set
    End Property
    Public ReadOnly Property Fonts As IFontEditor Implements IWorld.Fonts
        Get
            Return New FontEditor(_data)
        End Get
    End Property

    Public ReadOnly Property Terrains As ITerrainEditor Implements IWorld.Terrains
        Get
            Return New TerrainEditor(_data)
        End Get
    End Property

    Public ReadOnly Property Maps As IMapEditor Implements IWorld.Maps
        Get
            Return New MapEditor(_data)
        End Get
    End Property

    Public ReadOnly Property Items As IItemEditor Implements IWorld.Items
        Get
            Return New ItemEditor(_data)
        End Get
    End Property

    Public ReadOnly Property Creatures As ICreatureEditor Implements IWorld.Creatures
        Get
            Return New CreatureEditor(_data)
        End Get
    End Property

    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If _data.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(_data)
        End Get
    End Property

    Public ReadOnly Property Colors As IColorEditor Implements IWorld.Colors
        Get
            Return New ColorEditor(_data)
        End Get
    End Property

    Public Sub Save(fileName As String) Implements IWorld.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data))
    End Sub
    Public Sub Load(fileName As String) Implements IWorld.Load
        _data = JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(fileName))
    End Sub

    Public Sub ClearAvatar() Implements IWorld.ClearAvatar
        _data.Avatar = Nothing
    End Sub

    Public Sub SetAvatar(creature As ICreatureInstance) Implements IWorld.SetAvatar
        _data.Avatar = New AvatarData With
            {
                .MapName = creature.MapName,
                .Column = creature.Column,
                .Row = creature.Row
            }
    End Sub
End Class