﻿Friend Class TerrainEditor
    Implements ITerrainEditor

    Private ReadOnly _data As EditorData

    Public Sub New(data As EditorData)
        Me._data = data
    End Sub

    Public ReadOnly Property Names As IEnumerable(Of String) Implements ITerrainEditor.Names
        Get
            Return _data.Terrains.Keys
        End Get
    End Property

    Public ReadOnly Property HasAny As Boolean Implements ITerrainEditor.HasAny
        Get
            Return _data.Terrains.Any
        End Get
    End Property

    Public Sub Rename(fromName As String, toName As String) Implements ITerrainEditor.Rename
        Dim temp = _data.Terrains(fromName)
        _data.Terrains.Remove(fromName)
        _data.Terrains.Add(toName, temp)
    End Sub

    Public Sub Clone(fromName As String, toName As String) Implements ITerrainEditor.Clone
        Dim temp = _data.Terrains(fromName)
        _data.Terrains.Add(toName, temp)
    End Sub

    Public Sub Delete(name As String) Implements ITerrainEditor.Delete
        _data.Terrains.Remove(name)
    End Sub

    Public Function Create(name As String) As ITerrain Implements ITerrainEditor.Create
        _data.Terrains(name) = New TerrainData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Tenantability = False,
            .HueIndex = 0}
        Return New Terrain(_data, name)
    End Function

    Public Function Retrieve(name As String) As ITerrain Implements ITerrainEditor.Retrieve
        Return New Terrain(_data, name)
    End Function
End Class