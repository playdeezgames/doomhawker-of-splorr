Public Interface IFontEditor
    Function Create(name As String, width As Integer, height As Integer) As IEditorFont
    Function Retrieve(name As String) As IEditorFont
    ReadOnly Property HasAny As Boolean
    ReadOnly Property Names As IEnumerable(Of String)
    Sub Rename(fromName As String, toName As String)
    Sub Clone(fromName As String, toName As String)
    Sub Delete(name As String)
End Interface
