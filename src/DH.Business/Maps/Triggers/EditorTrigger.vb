Friend Class EditorTrigger
    Implements IEditorTrigger
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

    Public Property TriggerType As TriggerType Implements IEditorTrigger.TriggerType
        Get
            Return TriggerData.TriggerType
        End Get
        Set(value As TriggerType)
            TriggerData.TriggerType = value
        End Set
    End Property
End Class
