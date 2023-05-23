Public Interface IColors
    Inherits IThingies(Of IColor)
    Function Create(name As String, red As Byte, green As Byte, blue As Byte) As IColor
End Interface
