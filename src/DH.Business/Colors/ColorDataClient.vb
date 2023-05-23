﻿Friend MustInherit Class ColorDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property ColorName As String
    Protected Sub New(data As WorldData, colorName As String)
        MyBase.New(data)
        Me.ColorName = colorName
    End Sub
    Protected ReadOnly Property ColorData As ColorData
        Get
            Return WorldData.Colors(ColorName)
        End Get
    End Property
End Class
