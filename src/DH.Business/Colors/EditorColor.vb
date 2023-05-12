Friend Class EditorColor
    Implements IEditorColor

    Private ReadOnly _data As EditorData
    Private ReadOnly _name As String
    Private ReadOnly Property ColorData As ColorData
        Get
            Return _data.Colors(_name)
        End Get
    End Property

    Public Sub New(data As EditorData, name As String)
        Me._data = data
        Me._name = name
    End Sub

    Public Property Red As Byte Implements IEditorColor.Red
        Get
            Return ColorData.Red
        End Get
        Set(value As Byte)
            ColorData.Red = value
        End Set
    End Property

    Public Property Green As Byte Implements IEditorColor.Green
        Get
            Return ColorData.Green
        End Get
        Set(value As Byte)
            ColorData.Green = value
        End Set
    End Property

    Public Property Blue As Byte Implements IEditorColor.Blue
        Get
            Return ColorData.Blue
        End Get
        Set(value As Byte)
            ColorData.Blue = value
        End Set
    End Property
End Class
