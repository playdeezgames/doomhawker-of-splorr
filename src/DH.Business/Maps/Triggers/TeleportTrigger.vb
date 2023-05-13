Friend Class TeleportTrigger
    Implements ITeleportTrigger

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _triggerName As String
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property

    Private ReadOnly Property TriggerData As TriggerData
        Get
            Return MapData.Triggers(_triggerName)
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, triggerName As String)
        _data = data
        _mapName = mapName
        _triggerName = triggerName
    End Sub

    Public Property MapName As String Implements ITeleportTrigger.MapName
        Get
            Return TriggerData.Teleport.MapName
        End Get
        Set(value As String)
            TriggerData.Teleport.MapName = value
        End Set
    End Property

    Public Property Column As Integer Implements ITeleportTrigger.Column
        Get
            Return TriggerData.Teleport.Column
        End Get
        Set(value As Integer)
            TriggerData.Teleport.Column = value
        End Set
    End Property

    Public Property Row As Integer Implements ITeleportTrigger.Row
        Get
            Return TriggerData.Teleport.Row
        End Get
        Set(value As Integer)
            TriggerData.Teleport.Row = value
        End Set
    End Property
End Class
