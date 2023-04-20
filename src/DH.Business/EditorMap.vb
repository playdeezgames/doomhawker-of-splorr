Friend Class EditorMap
    Implements IEditorMap
    Private ReadOnly _data As EditorData
    Private ReadOnly _mapName As String
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property
    Public Sub New(data As EditorData, mapName As String)
        _data = data
        _mapName = mapName
    End Sub
End Class
