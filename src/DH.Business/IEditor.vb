Public Interface IEditor
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Property MapCellWidth As Integer
    Property MapCellHeight As Integer

    ReadOnly Property Fonts As IFontEditor
    ReadOnly Property Terrains As ITerrainEditor
    ReadOnly Property Maps As IMapEditor
    ReadOnly Property Items As IItemEditor
    ReadOnly Property ItemNames As IEnumerable(Of String)
End Interface
