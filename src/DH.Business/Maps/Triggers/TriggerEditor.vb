Friend Class TriggerEditor
    Inherits Thingies(Of TriggerData, IEditorTrigger, String)
    Implements ITriggerEditor

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String

    Public Sub New(data As WorldData, mapName As String)
        MyBase.New(data, Function() data.Maps(mapName).Triggers, Function(triggerName) New EditorTrigger(data, mapName, triggerName))
        Me._data = data
        Me._mapName = mapName
    End Sub

    Public Function Create(name As String, triggerType As TriggerType) As IEditorTrigger Implements ITriggerEditor.Create
        _data.Maps(_mapName).Triggers(name) = New TriggerData With {.TriggerType = triggerType}
        Return New EditorTrigger(_data, _mapName, name)
    End Function
End Class
