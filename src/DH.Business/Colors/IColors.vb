Public Interface IColors
    Inherits IThingies(Of IEditorColor)
    Function Create(name As String, red As Byte, green As Byte, blue As Byte) As IEditorColor
End Interface
