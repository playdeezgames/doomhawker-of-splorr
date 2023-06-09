﻿Friend Class TerrainEditor
    Inherits Thingies(Of TerrainData, ITerrain, String)
    Implements ITerrainEditor
    Private ReadOnly _data As WorldData
    Public Sub New(data As WorldData)
        MyBase.New(data, Function() data.Terrains, Function(name) New Terrain(data, name))
        _data = data
    End Sub
    Public Function Create(name As String, hue As Integer) As ITerrain Implements ITerrainEditor.Create
        _data.Terrains(name) = New TerrainData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Tenantability = False,
            .Hue = hue}
        Return New Terrain(_data, name)
    End Function
End Class
