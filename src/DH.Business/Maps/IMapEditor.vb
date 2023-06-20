Public Interface IMapEditor
    Inherits IThingies(Of IEditorMap, String)
    Function Create(name As String, columns As Integer, rows As Integer, terrainName As String) As IEditorMap
End Interface
