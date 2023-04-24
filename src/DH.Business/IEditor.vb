Public Interface IEditor
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Property MapCellWidth As Integer
    Property MapCellHeight As Integer

    ReadOnly Property Fonts As IFontEditor
    ReadOnly Property Terrains As ITerrainEditor
    ReadOnly Property Maps As IMapEditor

    Function CreateItem(itemName As String) As IItem
    ReadOnly Property HasItems As Boolean
    Function GetItem(itemName As String) As IItem
    Sub RenameItem(fromItemName As String, toItemName As String)
    Sub CloneItem(fromItemName As String, toItemName As String)
    Sub DeleteItem(itemName As String)
    ReadOnly Property ItemNames As IEnumerable(Of String)
End Interface
