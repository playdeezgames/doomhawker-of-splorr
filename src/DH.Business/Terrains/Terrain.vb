Friend Class Terrain
    Inherits Sprite
    Implements ITerrain

    Private ReadOnly _data As WorldData
    Private ReadOnly _terrainName As String
    Private ReadOnly Property TerrainData As TerrainData
        Get
            Return _data.Terrains(_terrainName)
        End Get
    End Property

    Public Sub New(data As WorldData, terrainName As String)
        MyBase.New(data, Function(w, n) w.Terrains(n), terrainName)
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
End Class
