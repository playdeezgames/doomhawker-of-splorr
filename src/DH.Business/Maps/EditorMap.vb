Friend Class EditorMap
    Implements IEditorMap
    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property

    Public ReadOnly Property Columns As Integer Implements IEditorMap.Columns
        Get
            Return MapData.Columns
        End Get
    End Property

    Public ReadOnly Property Rows As Integer Implements IEditorMap.Rows
        Get
            Return MapData.Rows
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String)
        _data = data
        _mapName = mapName
    End Sub

    Public Function GetCell(column As Integer, row As Integer) As IEditorMapCell Implements IEditorMap.GetCell
        Return New EditorMapCell(_data, _mapName, column, row)
    End Function
End Class
