Friend MustInherit Class Thingies(Of TData, TThingie)
    Inherits WorldDataClient
    Implements IThingies(Of TThingie)
    Private ReadOnly _thingieSource As Func(Of Dictionary(Of String, TData))
    Private ReadOnly _thingieRetriever As Func(Of String, TThingie)

    Sub New(data As WorldData, thingieSource As Func(Of Dictionary(Of String, TData)), thingieRetriever As Func(Of String, TThingie))
        MyBase.New(data)
        _thingieSource = thingieSource
        _thingieRetriever = thingieRetriever
    End Sub

    Public ReadOnly Property HasAny As Boolean Implements IThingies(Of TThingie).HasAny
        Get
            Return _thingieSource().Any
        End Get
    End Property

    Public ReadOnly Property Names As IEnumerable(Of String) Implements IThingies(Of TThingie).Names
        Get
            Return _thingieSource().Keys
        End Get
    End Property

    Public Sub Rename(fromName As String, toName As String) Implements IThingies(Of TThingie).Rename
        Dim temp = _thingieSource()(fromName)
        _thingieSource().Remove(fromName)
        _thingieSource().Add(toName, temp)
    End Sub

    Public Sub Clone(fromName As String, toName As String) Implements IThingies(Of TThingie).Clone
        Dim temp = _thingieSource()(fromName)
        _thingieSource().Add(toName, temp)
    End Sub

    Public Sub Delete(name As String) Implements IThingies(Of TThingie).Delete
        _thingieSource().Remove(name)
    End Sub

    Public Function Retrieve(name As String) As TThingie Implements IThingies(Of TThingie).Retrieve
        Return _thingieRetriever(name)
    End Function
End Class
