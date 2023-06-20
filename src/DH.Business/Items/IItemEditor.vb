Public Interface IItemEditor
    Inherits IThingies(Of IItem, String)
    Function Create(name As String, hue As Integer) As IItem
End Interface
