Friend Class Terrain
    Inherits Sprite
    Implements ITerrain

    Private ReadOnly _data As EditorData
    Private ReadOnly _terrainName As String
    Private ReadOnly Property TerrainData As TerrainData
        Get
            Return _data.Terrains(_terrainName)
        End Get
    End Property

    Public Sub New(data As EditorData, terrainName As String)
        MyBase.New(data.Terrains(terrainName), terrainName)
        Me._data = data
        Me._terrainName = terrainName
    End Sub

    Public Property Tenantability As Boolean Implements ITerrain.Tenantability
        Get
            Return TerrainData.Tenantability
        End Get
        Set(value As Boolean)
            TerrainData.Tenantability = value
        End Set
    End Property

    Public Overrides Property Font As IEditorFont
        Get
            If _data.Fonts.ContainsKey(TerrainData.FontName) Then
                Return New EditorFont(_data, TerrainData.FontName)
            End If
            Return Nothing
        End Get
        Set(value As IEditorFont)
            If value IsNot Nothing Then
                FontName = value.FontName
            Else
                FontName = Nothing
            End If
        End Set
    End Property
End Class
