Public Interface IThingieEditor(Of TThingie)
    Function Retrieve(name As String) As TThingie
    ReadOnly Property HasAny As Boolean
    ReadOnly Property Names As IEnumerable(Of String)
    Sub Rename(fromName As String, toName As String)
    Sub Clone(fromName As String, toName As String)
    Sub Delete(name As String)
End Interface
