Public Interface IItemEditor
    Inherits IThingies(Of IItem)
    Function Create(name As String, hue As String) As IItem
End Interface
