Friend MustInherit Class ThingieEditor(Of TData, TThingie)
    Implements IThingieEditor(Of TThingie)
    Private ReadOnly _thingieSource As Func(Of Dictionary(Of String, TData))
    Private ReadOnly _thingieRetriever As Func(Of String, TThingie)
    Protected Const Zero = 0

    Sub New(thingieSource As Func(Of Dictionary(Of String, TData)), thingieRetriever As Func(Of String, TThingie))
        _thingieSource = thingieSource
        _thingieRetriever = thingieRetriever
    End Sub

    Public ReadOnly Property HasAny As Boolean Implements IThingieEditor(Of TThingie).HasAny
        Get
            Return _thingieSource().Any
        End Get
    End Property

    Public ReadOnly Property Names As IEnumerable(Of String) Implements IThingieEditor(Of TThingie).Names
        Get
            Return _thingieSource().Keys
        End Get
    End Property

    Public Sub Rename(fromName As String, toName As String) Implements IThingieEditor(Of TThingie).Rename
        Dim temp = _thingieSource()(fromName)
        _thingieSource().Remove(fromName)
        _thingieSource().Add(toName, temp)
    End Sub

    Public Sub Clone(fromName As String, toName As String) Implements IThingieEditor(Of TThingie).Clone
        Dim temp = _thingieSource()(fromName)
        _thingieSource().Add(toName, temp)
    End Sub

    Public Sub Delete(name As String) Implements IThingieEditor(Of TThingie).Delete
        _thingieSource().Remove(name)
    End Sub

    Public Function Retrieve(name As String) As TThingie Implements IThingieEditor(Of TThingie).Retrieve
        Return _thingieRetriever(name)
    End Function
End Class
