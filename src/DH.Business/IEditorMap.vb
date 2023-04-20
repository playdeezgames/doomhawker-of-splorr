Public Interface IEditorMap
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function GetCell(column As Integer, row As Integer) As IEditorMapCell
End Interface
