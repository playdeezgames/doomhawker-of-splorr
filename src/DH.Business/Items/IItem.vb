Public Interface IItem
    Property HueIndex As Integer
    Property Font As IEditorFont
    Property GlyphKey As Char
    ReadOnly Property FontName As String
    ReadOnly Property Name As String
    Function CreateInstance(mapName As String, column As Integer, row As Integer) As IItemInstance
End Interface
