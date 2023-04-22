Public Interface IEditor
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Function CreateFont(name As String, width As Integer, weight As Integer) As IEditorFont
    Function GetFont(fontName As String) As IEditorFont
    Function CreateTerrain(terrainName As String) As ITerrain
    Function GetTerrain(terrainName As String) As ITerrain
    Function HasFont(fontName As String) As Boolean
    Function CreateMap(mapName As String, columns As Integer, rows As Integer, terrainName As String) As IEditorMap
    ReadOnly Property HasMaps As Boolean
    ReadOnly Property HasFonts As Boolean
    ReadOnly Property FontNames As IEnumerable(Of String)
    ReadOnly Property TerrainNames As IEnumerable(Of String)
    ReadOnly Property HasTerrains As Boolean
    Property MapCellWidth As Integer
    Property MapCellHeight As Integer
    ReadOnly Property MapNames As IEnumerable(Of String)
    Function GetMap(mapName As String) As IEditorMap
    Sub RenameMap(fromMapName As String, toMapName As String)
    Sub RenameFont(fromFontName As String, toFontName As String)
    Sub CloneFont(fromFontName As String, toFontName As String)
    Sub DeleteFont(fontName As String)
    Sub RenameTerrain(fromTerrainName As String, toTerrainName As String)
    Sub CloneTerrain(fromTerrainName As String, toTerrainName As String)
    Sub DeleteTerrain(terrainName As String)
    Sub CloneMap(fromMapName As String, toMapName As String)
    Sub DeleteMap(mapName As String)
End Interface
