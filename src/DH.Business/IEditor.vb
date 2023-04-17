Public Interface IEditor
    Function CreateFont(name As String, width As Integer, weight As Integer) As IEditorFont
    Function GetFont(fontName As String) As IEditorFont
End Interface
