Public Interface ITerrainEditor
    Inherits IThingies(Of ITerrain)
    Function Create(name As String, hue As String) As ITerrain
End Interface
