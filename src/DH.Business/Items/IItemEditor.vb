Public Interface IItemEditor
    Inherits IThingieEditor(Of IItem)
    Function Create(name As String) As IItem
End Interface
