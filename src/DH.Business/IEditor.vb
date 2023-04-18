Public Interface IEditor
    Sub Save(fileName As String)
    Function CreateFont(name As String, width As Integer, weight As Integer) As IEditorFont
    Function GetFont(fontName As String) As IEditorFont
End Interface
