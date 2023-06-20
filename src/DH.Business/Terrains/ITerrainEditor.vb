Public Interface ITerrainEditor
    Inherits IThingies(Of ITerrain, String)
    Function Create(name As String, hue As Integer) As ITerrain
End Interface
