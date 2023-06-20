Public Interface ITriggerEditor
    Inherits IThingies(Of IEditorTrigger, String)
    Function Create(name As String, triggerType As TriggerType) As IEditorTrigger
End Interface
