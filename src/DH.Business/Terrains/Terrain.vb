﻿Friend Class Terrain
    Implements ITerrain

    Private ReadOnly _data As EditorData
    Private ReadOnly _terrainName As String
    Private ReadOnly Property TerrainData As TerrainData
        Get
            Return _data.Terrains(_terrainName)
        End Get
    End Property

    Public Sub New(data As EditorData, terrainName As String)
        Me._data = data
        Me._terrainName = terrainName
    End Sub

    Public Property FontName As String Implements ITerrain.FontName
        Get
            Return TerrainData.FontName
        End Get
        Set(value As String)
            TerrainData.FontName = value
        End Set
    End Property

    Public Property GlyphKey As Char Implements ITerrain.GlyphKey
        Get
            Return TerrainData.GlyphKey
        End Get
        Set(value As Char)
            TerrainData.GlyphKey = value
        End Set
    End Property

    Public Property Tenantability As Boolean Implements ITerrain.Tenantability
        Get
            Return TerrainData.Tenantability
        End Get
        Set(value As Boolean)
            TerrainData.Tenantability = value
        End Set
    End Property

    Public Property HueIndex As Integer Implements ITerrain.HueIndex
        Get
            Return TerrainData.HueIndex
        End Get
        Set(value As Integer)
            TerrainData.HueIndex = value
        End Set
    End Property

    Public Property Font As IEditorFont Implements ITerrain.Font
        Get
            If _data.Fonts.ContainsKey(TerrainData.FontName) Then
                Return New EditorFont(_data, TerrainData.FontName)
            End If
            Return Nothing
        End Get
        Set(value As IEditorFont)
            If value IsNot Nothing Then
                TerrainData.FontName = value.FontName
            Else
                TerrainData.FontName = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property Name As String Implements ITerrain.Name
        Get
            Return _terrainName
        End Get
    End Property
End Class
