Public Class Editor
    Implements IEditor
    Private _data As EditorData
    Sub New(data As EditorData)
        _data = data
    End Sub
    Public ReadOnly Property TerrainNames As IEnumerable(Of String) Implements IEditor.TerrainNames
        Get
            Return _data.Terrains.Keys
        End Get
    End Property
    Public ReadOnly Property HasTerrains As Boolean Implements IEditor.HasTerrains
        Get
            Return _data.Terrains.Any
        End Get
    End Property
    Public ReadOnly Property HasMaps As Boolean Implements IEditor.HasMaps
        Get
            Return _data.Maps.Any
        End Get
    End Property
    Public Property MapCellWidth As Integer Implements IEditor.MapCellWidth
        Get
            Return _data.MapCellWidth
        End Get
        Set(value As Integer)
            _data.MapCellWidth = value
        End Set
    End Property
    Public Property MapCellHeight As Integer Implements IEditor.MapCellHeight
        Get
            Return _data.MapCellHeight
        End Get
        Set(value As Integer)
            _data.MapCellHeight = value
        End Set
    End Property
    Public ReadOnly Property MapNames As IEnumerable(Of String) Implements IEditor.MapNames
        Get
            Return _data.Maps.Keys
        End Get
    End Property
    Public ReadOnly Property HasItems As Boolean Implements IEditor.HasItems
        Get
            Return _data.Items.Any
        End Get
    End Property
    Public ReadOnly Property ItemNames As IEnumerable(Of String) Implements IEditor.ItemNames
        Get
            Return _data.Items.Keys
        End Get
    End Property

    Public ReadOnly Property Fonts As IFontEditor Implements IEditor.Fonts
        Get
            Return New FontEditor(_data)
        End Get
    End Property

    Public Sub Save(fileName As String) Implements IEditor.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data))
    End Sub
    Public Sub Load(fileName As String) Implements IEditor.Load
        _data = JsonSerializer.Deserialize(Of EditorData)(File.ReadAllText(fileName))
    End Sub
    Public Function CreateTerrain(terrainName As String) As ITerrain Implements IEditor.CreateTerrain
        _data.Terrains(terrainName) = New TerrainData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Tenantability = False,
            .HueIndex = 0}
        Return New Terrain(_data, terrainName)
    End Function
    Public Function GetTerrain(terrainName As String) As ITerrain Implements IEditor.GetTerrain
        Return New Terrain(_data, terrainName)
    End Function
    Public Function CreateMap(mapName As String, columns As Integer, rows As Integer, terrainName As String) As IEditorMap Implements IEditor.CreateMap
        Dim cells = New List(Of MapCellData)
        While cells.Count < columns * rows
            cells.Add(New MapCellData With {.Terrain = terrainName})
        End While
        _data.Maps(mapName) = New MapData With
            {
                .Columns = columns,
                .Rows = rows,
                .Cells = cells
            }
        Return New EditorMap(_data, mapName)
    End Function
    Public Function GetMap(mapName As String) As IEditorMap Implements IEditor.GetMap
        Return New EditorMap(_data, mapName)
    End Function
    Public Sub RenameMap(fromMapName As String, toMapName As String) Implements IEditor.RenameMap
        Dim temp = _data.Maps(fromMapName)
        _data.Maps.Remove(fromMapName)
        _data.Maps.Add(toMapName, temp)
    End Sub
    Public Sub RenameFont(fromFontName As String, toFontName As String) Implements IEditor.RenameFont
        Fonts.Rename(fromFontName, toFontName)
    End Sub
    Public Sub CloneFont(fromFontName As String, toFontName As String) Implements IEditor.CloneFont
        Fonts.Clone(fromFontName, toFontName)
    End Sub
    Public Sub DeleteFont(fontName As String) Implements IEditor.DeleteFont
        Fonts.Delete(fontName)
    End Sub
    Public Sub RenameTerrain(fromTerrainName As String, toTerrainName As String) Implements IEditor.RenameTerrain
        Dim temp = _data.Terrains(fromTerrainName)
        _data.Terrains.Remove(fromTerrainName)
        _data.Terrains.Add(toTerrainName, temp)
    End Sub
    Public Sub CloneTerrain(fromTerrainName As String, toTerrainName As String) Implements IEditor.CloneTerrain
        Dim temp = _data.Terrains(fromTerrainName)
        _data.Terrains.Add(toTerrainName, temp)
    End Sub
    Public Sub DeleteTerrain(terrainName As String) Implements IEditor.DeleteTerrain
        _data.Terrains.Remove(terrainName)
    End Sub

    Public Sub CloneMap(fromMapName As String, toMapName As String) Implements IEditor.CloneMap
        Dim temp = _data.Maps(fromMapName)
        _data.Maps.Add(toMapName, temp)
    End Sub

    Public Sub DeleteMap(mapName As String) Implements IEditor.DeleteMap
        _data.Maps.Remove(mapName)
    End Sub

    Public Function CreateItem(itemName As String) As IItem Implements IEditor.CreateItem
        _data.Items(itemName) = New ItemData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .HueIndex = 0}
        Return New Item(_data, itemName)
    End Function

    Public Function GetItem(itemName As String) As IItem Implements IEditor.GetItem
        Return New Item(_data, itemName)
    End Function

    Public Sub RenameItem(fromItemName As String, toItemName As String) Implements IEditor.RenameItem
        Dim temp = _data.Items(fromItemName)
        _data.Items.Remove(fromItemName)
        _data.Items.Add(toItemName, temp)
    End Sub

    Public Sub CloneItem(fromItemName As String, toItemName As String) Implements IEditor.CloneItem
        Dim temp = _data.Items(fromItemName)
        _data.Items.Add(toItemName, temp)
    End Sub

    Public Sub DeleteItem(itemName As String) Implements IEditor.DeleteItem
        _data.Items.Remove(itemName)
    End Sub
End Class