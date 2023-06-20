Public Interface ICreatures
    Inherits IThingies(Of ICreature, String)
    Function Create(name As String, hue As Integer) As ICreature
End Interface
