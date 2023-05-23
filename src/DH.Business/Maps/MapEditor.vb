Friend Class MapEditor
    Inherits Thingies(Of MapData, IEditorMap)
    Implements IMapEditor
    Private ReadOnly _data As WorldData

    Public Sub New(data As WorldData)
        MyBase.New(Function() data.Maps, Function(name) New EditorMap(data, name))
        _data = data
    End Sub
    Public Function Create(name As String, columns As Integer, rows As Integer, terrainName As String) As IEditorMap Implements IMapEditor.Create
        Dim cells = New List(Of MapCellData)
        While cells.Count < columns * rows
            cells.Add(New MapCellData With {.Terrain = terrainName})
        End While
        _data.Maps(name) = New MapData With
            {
                .Columns = columns,
                .Rows = rows,
                .Cells = cells
            }
        Return New EditorMap(_data, name)
    End Function
End Class
