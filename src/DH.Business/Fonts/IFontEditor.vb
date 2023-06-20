Public Interface IFontEditor
    Inherits IThingies(Of IEditorFont, String)

    Sub Export(name As String, fileName As String)
    Function Create(name As String, width As Integer, height As Integer) As IEditorFont
End Interface
