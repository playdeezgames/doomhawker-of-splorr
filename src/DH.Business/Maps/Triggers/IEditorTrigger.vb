Public Interface IEditorTrigger
    Property TriggerType As TriggerType
    Property NextTrigger As IEditorTrigger
    ReadOnly Property Name As String
    ReadOnly Property Teleport As ITeleportTrigger
End Interface
