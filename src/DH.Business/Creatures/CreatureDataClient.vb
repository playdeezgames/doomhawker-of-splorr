Friend MustInherit Class CreatureDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property CreatureName As String
    Protected Sub New(data As WorldData, creatureName As String)
        MyBase.New(data)
        Me.CreatureName = creatureName
    End Sub
    Protected ReadOnly Property CreatureData As CreatureData
        Get
            Return WorldData.Creatures(CreatureName)
        End Get
    End Property
End Class
