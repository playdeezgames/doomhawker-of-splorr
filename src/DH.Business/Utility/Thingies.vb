Friend MustInherit Class Thingies(Of TData, TThingie, TName)
    Inherits WorldDataClient
    Implements IThingies(Of TThingie, TName)
    Private ReadOnly _thingieSource As Func(Of Dictionary(Of TName, TData))
    Private ReadOnly _thingieRetriever As Func(Of TName, TThingie)

    Sub New(data As WorldData, thingieSource As Func(Of Dictionary(Of TName, TData)), thingieRetriever As Func(Of TName, TThingie))
        MyBase.New(data)
        _thingieSource = thingieSource
        _thingieRetriever = thingieRetriever
    End Sub

    Public ReadOnly Property HasAny As Boolean Implements IThingies(Of TThingie, TName).HasAny
        Get
            Return _thingieSource().Any
        End Get
    End Property

    Public ReadOnly Property Names As IEnumerable(Of TName) Implements IThingies(Of TThingie, TName).Names
        Get
            Return _thingieSource().Keys
        End Get
    End Property

    Public Sub Rename(fromName As TName, toName As TName) Implements IThingies(Of TThingie, TName).Rename
        Dim temp = _thingieSource()(fromName)
        _thingieSource().Remove(fromName)
        _thingieSource().Add(toName, temp)
    End Sub

    Public Sub Clone(fromName As TName, toName As TName) Implements IThingies(Of TThingie, TName).Clone
        Dim temp = _thingieSource()(fromName)
        _thingieSource().Add(toName, temp)
    End Sub

    Public Sub Delete(name As TName) Implements IThingies(Of TThingie, TName).Delete
        _thingieSource().Remove(name)
    End Sub

    Public Function Retrieve(name As TName) As TThingie Implements IThingies(Of TThingie, TName).Retrieve
        Return _thingieRetriever(name)
    End Function
End Class
