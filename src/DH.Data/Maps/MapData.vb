Public Class MapData
    Property Columns As Integer
    Property Rows As Integer
    Property Cells As List(Of MapCellData)
    Property Triggers As New Dictionary(Of String, TriggerData)
End Class
