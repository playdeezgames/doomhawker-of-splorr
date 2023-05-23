Friend MustInherit Class AvatarDataClient
    Inherits WorldDataClient
    Protected Sub New(data As WorldData)
        MyBase.New(data)
    End Sub
    Protected ReadOnly Property AvatarData As AvatarData
        Get
            Return WorldData.Avatar
        End Get
    End Property
End Class
