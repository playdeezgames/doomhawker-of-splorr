Friend Class ColorEditor
    Inherits ThingieEditor(Of ColorData, IEditorColor)
    Implements IColorEditor
    Private _data As EditorData

    Public Sub New(data As EditorData)
        MyBase.New(Function() data.Colors, Function(name) New EditorColor(data, name))
        Me._data = data
    End Sub

    Public Function Create(name As String, red As Byte, green As Byte, blue As Byte) As IEditorColor Implements IColorEditor.Create
        Throw New NotImplementedException()
    End Function
End Class
