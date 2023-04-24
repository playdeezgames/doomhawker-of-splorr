﻿Public Interface IMapEditor
    Inherits IThingieEditor(Of IEditorMap)
    Function Create(name As String, columns As Integer, rows As Integer, terrainName As String) As IEditorMap
End Interface
