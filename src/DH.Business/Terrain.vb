Friend Class Terrain
    Implements ITerrain

    Private ReadOnly _data As EditorData
    Private ReadOnly _terrainName As String

    Public Sub New(data As EditorData, terrainName As String)
        Me._data = data
        Me._terrainName = terrainName
    End Sub
End Class
