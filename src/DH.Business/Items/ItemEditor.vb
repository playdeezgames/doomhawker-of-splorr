Friend Class ItemEditor
    Inherits Thingies(Of ItemData, IItem, String)
    Implements IItemEditor
    Private _data As WorldData
    Public Sub New(data As WorldData)
        MyBase.New(data, Function() data.Items, Function(name) New Item(data, name))
        Me._data = data
    End Sub
    Public Function Create(name As String, hue As Integer) As IItem Implements IItemEditor.Create
        _data.Items(name) = New ItemData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Hue = hue}
        Return New Item(_data, name)
    End Function
End Class
