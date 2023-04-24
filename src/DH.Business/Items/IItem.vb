Public Interface IItem
    Inherits ISprite
    Function CreateInstance(mapName As String, column As Integer, row As Integer) As IItemInstance
End Interface
