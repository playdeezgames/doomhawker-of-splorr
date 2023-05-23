Friend Class Item
    Inherits Sprite
    Implements IItem
    Private ReadOnly _data As WorldData
    Private ReadOnly _itemName As String
    Private ReadOnly Property ItemData As ItemData
        Get
            Return _data.Items(_itemName)
        End Get
    End Property
    Public Sub New(data As WorldData, itemName As String)
        MyBase.New(data, Function(w, n) w.Items(n), itemName)
        _data = data
        _itemName = itemName
    End Sub

    Public Function CreateInstance(mapName As String, column As Integer, row As Integer) As IItemInstance Implements IItem.CreateInstance
        Dim mapData = _data.Maps(mapName)
        Dim mapCellData = mapData.Cells(column + row * mapData.Columns)
        mapCellData.Item = New ItemInstanceData With
            {
                .ItemName = Name
            }
        Return New ItemInstance(_data, mapName, column, row)
    End Function
End Class
