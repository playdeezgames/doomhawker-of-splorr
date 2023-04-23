Friend Class Item
    Implements IItem
    Private ReadOnly _data As EditorData
    Private ReadOnly _itemName As String
    Private ReadOnly Property ItemData As ItemData
        Get
            Return _data.Items(_itemName)
        End Get
    End Property
    Public Sub New(data As EditorData, itemName As String)
        _data = data
        _itemName = itemName
    End Sub
End Class
