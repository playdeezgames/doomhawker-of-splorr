Public Interface ICreatureEditor
    Inherits IThingieEditor(Of ICreature)
    Function Create(name As String) As ICreature
End Interface
