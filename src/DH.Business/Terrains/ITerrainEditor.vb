Public Interface ITerrainEditor
    Inherits IThingieEditor(Of ITerrain)
    Function Create(name As String) As ITerrain
End Interface
