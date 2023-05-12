Public Interface ITriggerEditor
    Inherits IThingieEditor(Of IEditorTrigger)
    Function Create(name As String, triggerType As TriggerType) As IEditorTrigger
End Interface
