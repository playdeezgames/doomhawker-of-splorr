Public Interface IEditor
    Sub Save(fileName As String)
    Sub Load(fileName As String)
    Function CreateFont(name As String, width As Integer, weight As Integer) As IEditorFont
    Function GetFont(fontName As String) As IEditorFont
    ReadOnly Property FontNames As IEnumerable(Of String)
End Interface
