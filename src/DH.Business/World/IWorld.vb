Public Interface IWorld
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Sub ClearAvatar()
    Sub SetAvatar(creature As ICreatureInstance)
    Property MapCellWidth As Integer
    Property MapCellHeight As Integer

    ReadOnly Property Fonts As IFontEditor
    ReadOnly Property Terrains As ITerrainEditor
    ReadOnly Property Maps As IMapEditor
    ReadOnly Property Items As IItemEditor
    ReadOnly Property Creatures As ICreatureEditor
    ReadOnly Property Avatar As IAvatar
    ReadOnly Property Colors As IColorEditor
End Interface