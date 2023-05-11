Public Interface IItemEditor
    Inherits IThingieEditor(Of IItem)
    Function Create(name As String, hue As String) As IItem
End Interface
