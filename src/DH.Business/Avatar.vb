Friend Class Avatar
    Implements IAvatar
    Private ReadOnly _data As EditorData
    Public Sub New(data As EditorData)
        _data = data
    End Sub
    Public Property MapName As String Implements IAvatar.MapName
        Get
            Return _data.Avatar.MapName
        End Get
        Set(value As String)
            _data.Avatar.MapName = value
        End Set
    End Property
    Public Property Column As Integer Implements IAvatar.Column
        Get
            Return _data.Avatar.Column
        End Get
        Set(value As Integer)
            _data.Avatar.Column = value
        End Set
    End Property
    Public Property Row As Integer Implements IAvatar.Row
        Get
            Return _data.Avatar.Row
        End Get
        Set(value As Integer)
            _data.Avatar.Row = value
        End Set
    End Property
End Class
