Friend Class ColorEditor
    Inherits ThingieEditor(Of ColorData, IEditorColor)
    Implements IColorEditor
    Private ReadOnly _data As WorldData
    Public Sub New(data As WorldData)
        MyBase.New(Function() data.Colors, Function(name) New EditorColor(data, name))
        _data = data
    End Sub
    Public Function Create(name As String, red As Byte, green As Byte, blue As Byte) As IEditorColor Implements IColorEditor.Create
        _data.Colors(name) = New ColorData With
            {
                .Red = red,
                .Green = green,
                .Blue = blue
            }
        Return New EditorColor(_data, name)
    End Function
End Class
