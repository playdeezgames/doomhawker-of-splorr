Public Interface ICreatureEditor
    Inherits IThingies(Of ICreature)
    Function Create(name As String, hue As String) As ICreature
End Interface
