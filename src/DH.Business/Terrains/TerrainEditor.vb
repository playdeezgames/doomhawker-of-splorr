Friend Class TerrainEditor
    Inherits ThingieEditor(Of TerrainData, ITerrain)
    Implements ITerrainEditor
    Private ReadOnly _data As EditorData
    Public Sub New(data As EditorData)
        MyBase.New(Function() data.Terrains, Function(name) New Terrain(data, name))
        _data = data
    End Sub
    Public Function Create(name As String, hue As String) As ITerrain Implements ITerrainEditor.Create
        _data.Terrains(name) = New TerrainData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Tenantability = False,
            .Hue = hue}
        Return New Terrain(_data, name)
    End Function
End Class
