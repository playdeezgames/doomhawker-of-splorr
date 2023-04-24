Public Class Editor
    Implements IEditor
    Private _data As EditorData
    Sub New(data As EditorData)
        _data = data
    End Sub
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

    Public ReadOnly Property Terrains As ITerrainEditor Implements IEditor.Terrains
        Get
            Return New TerrainEditor(_data)
        End Get
    End Property

    Public ReadOnly Property Maps As IMapEditor Implements IEditor.Maps
        Get
            Return New MapEditor(_data)
        End Get
    End Property

    Public Sub Save(fileName As String) Implements IEditor.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data))
    End Sub
    Public Sub Load(fileName As String) Implements IEditor.Load
        _data = JsonSerializer.Deserialize(Of EditorData)(File.ReadAllText(fileName))
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