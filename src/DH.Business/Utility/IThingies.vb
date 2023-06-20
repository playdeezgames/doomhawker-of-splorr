Public Interface IThingies(Of TThingie, TName)
    Function Retrieve(name As TName) As TThingie
    ReadOnly Property HasAny As Boolean
    ReadOnly Property Names As IEnumerable(Of TName)
    Sub Rename(fromName As TName, toName As TName)
    Sub Clone(fromName As TName, toName As TName)
    Sub Delete(name As TName)
End Interface
