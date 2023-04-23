Public Interface IFontEditor
    Inherits IThingieEditor(Of IEditorFont)
    Function Create(name As String, width As Integer, height As Integer) As IEditorFont
End Interface
