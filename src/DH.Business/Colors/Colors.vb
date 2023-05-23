Friend Class Colors
    Inherits Thingies(Of ColorData, IEditorColor)
    Implements IColors
    Public Sub New(data As WorldData)
        MyBase.New(data, Function() data.Colors, Function(name) New EditorColor(data, name))
    End Sub
    Public Function Create(name As String, red As Byte, green As Byte, blue As Byte) As IEditorColor Implements IColors.Create
        WorldData.Colors(name) = New ColorData With
            {
                .Red = red,
                .Green = green,
                .Blue = blue
            }
        Return New EditorColor(WorldData, name)
    End Function
End Class
