Public Interface IEditor
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Function CreateFont(name As String, width As Integer, weight As Integer) As IEditorFont
    Function GetFont(fontName As String) As IEditorFont
    Function CreateTerrain(terrainName As String) As ITerrain
    Function GetTerrain(terrainName As String) As ITerrain
    Function HasFont(fontName As String) As Boolean
    Function CreateMap(mapName As String, columns As Integer, rows As Integer) As IEditorMap
    ReadOnly Property HasMaps As Boolean
    ReadOnly Property HasFonts As Boolean
    ReadOnly Property FontNames As IEnumerable(Of String)
    ReadOnly Property TerrainNames As IEnumerable(Of String)
    ReadOnly Property HasTerrains As Boolean
    Property MapCellWidth As Integer
    Property MapCellHeight As Integer
    ReadOnly Property MapNames As IEnumerable(Of String)
End Interface
