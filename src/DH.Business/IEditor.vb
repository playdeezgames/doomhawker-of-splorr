Public Interface IEditor
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Property MapCellWidth As Integer
    Property MapCellHeight As Integer

    ReadOnly Property Fonts As IFontEditor
    ReadOnly Property Terrains As ITerrainEditor

    Sub CloneTerrain(fromTerrainName As String, toTerrainName As String)
    Sub DeleteTerrain(terrainName As String)

    Function CreateMap(mapName As String, columns As Integer, rows As Integer, terrainName As String) As IEditorMap
    ReadOnly Property HasMaps As Boolean
    ReadOnly Property MapNames As IEnumerable(Of String)
    ReadOnly Property HasItems As Boolean
    Function GetMap(mapName As String) As IEditorMap
    Sub RenameMap(fromMapName As String, toMapName As String)
    Sub CloneMap(fromMapName As String, toMapName As String)
    Sub DeleteMap(mapName As String)

    Function CreateItem(itemName As String) As IItem
    Function GetItem(itemName As String) As IItem
    Sub RenameItem(fromItemName As String, toItemName As String)
    Sub CloneItem(fromItemName As String, toItemName As String)
    Sub DeleteItem(itemName As String)
    ReadOnly Property ItemNames As IEnumerable(Of String)
End Interface
