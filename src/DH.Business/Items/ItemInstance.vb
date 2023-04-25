Friend Class ItemInstance
    Implements IItemInstance

    Private ReadOnly _data As EditorData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer

    Private ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Dim map = _data.Maps(_mapName)
            Return map.Cells(_column + _row * map.Columns).Item
        End Get
    End Property


    Public Sub New(data As EditorData, mapName As String, column As Integer, row As Integer)
        Me._data = data
        Me._mapName = mapName
        Me._column = column
        Me._row = row
    End Sub

    Public ReadOnly Property Item As IItem Implements IItemInstance.Item
        Get
            Return New Item(_data, ItemInstanceData.ItemName)
        End Get
    End Property
End Class
