Public Interface ITerrainEditor
    Function Create(name As String) As ITerrain
    Function Retrieve(name As String) As ITerrain
    ReadOnly Property Names As IEnumerable(Of String)
    ReadOnly Property HasAny As Boolean
    Sub Rename(fromName As String, toName As String)
    Sub Clone(fromName As String, toName As String)
    Sub Delete(name As String)
End Interface
