Friend Class ItemEditor
    Inherits ThingieEditor(Of ItemData, IItem)
    Implements IItemEditor
    Private _data As EditorData
    Public Sub New(data As EditorData)
        MyBase.New(Function() data.Items, Function(name) New Item(data, name))
        Me._data = data
    End Sub
    Public Function Create(name As String, hue As String) As IItem Implements IItemEditor.Create
        _data.Items(name) = New ItemData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Hue = hue}
        Return New Item(_data, name)
    End Function
End Class
