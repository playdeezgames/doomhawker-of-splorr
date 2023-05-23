Friend Class Color
    Inherits ColorDataClient
    Implements IColor

    Public Sub New(data As WorldData, name As String)
        MyBase.New(data, name)
    End Sub

    Public Property Red As Byte Implements IColor.Red
        Get
            Return ColorData.Red
        End Get
        Set(value As Byte)
            ColorData.Red = value
        End Set
    End Property

    Public Property Green As Byte Implements IColor.Green
        Get
            Return ColorData.Green
        End Get
        Set(value As Byte)
            ColorData.Green = value
        End Set
    End Property

    Public Property Blue As Byte Implements IColor.Blue
        Get
            Return ColorData.Blue
        End Get
        Set(value As Byte)
            ColorData.Blue = value
        End Set
    End Property
End Class
