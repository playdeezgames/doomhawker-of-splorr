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

    Public ReadOnly Property Items As IItemEditor Implements IEditor.Items
        Get
            Return New ItemEditor(_data)
        End Get
    End Property

    Public Sub Save(fileName As String) Implements IEditor.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data))
    End Sub
    Public Sub Load(fileName As String) Implements IEditor.Load
        _data = JsonSerializer.Deserialize(Of EditorData)(File.ReadAllText(fileName))
    End Sub
End Class