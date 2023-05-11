Public Interface ICreatureEditor
    Inherits IThingieEditor(Of ICreature)
    Function Create(name As String, hue As String) As ICreature
End Interface
