Public Interface IColors
    Inherits IThingies(Of IColor, Integer)
    Function Create(name As Integer, red As Byte, green As Byte, blue As Byte) As IColor
End Interface
