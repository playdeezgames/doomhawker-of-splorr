Friend Class EditorMapCell
    Implements IEditorMapCell

    Private ReadOnly _data As EditorData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly Property MapCellData As MapCellData
        Get
            Dim map = _data.Maps(_mapName)
            Return map.Cells(_column + _row * map.Columns)
        End Get
    End Property

    Public Property Terrain As ITerrain Implements IEditorMapCell.Terrain
        Get
            Dim terrainName = MapCellData.Terrain
            If terrainName IsNot Nothing AndAlso _data.Terrains.ContainsKey(terrainName) Then
                Return New Terrain(_data, terrainName)
            End If
            Return Nothing
        End Get
        Set(value As ITerrain)
            If value Is Nothing Then
                MapCellData.Terrain = Nothing
                Return
            End If
            MapCellData.Terrain = value.TerrainName
        End Set
    End Property

    Public Property Item As IItemInstance Implements IEditorMapCell.Item
        Get
            If MapCellData.Item Is Nothing Then
                Return Nothing
            End If
            Return New ItemInstance(_data, _mapName, _column, _row)
        End Get
        Set(value As IItemInstance)
            If value Is Nothing Then
                MapCellData.Item = Nothing
                Return
            End If
            MapCellData.Item = New ItemInstanceData With
                {
                    .ItemName = value.Item.Name
                }
        End Set
    End Property

    Public Sub New(data As EditorData, mapName As String, column As Integer, row As Integer)
        Me._data = data
        Me._mapName = mapName
        Me._column = column
        Me._row = row
    End Sub
End Class
