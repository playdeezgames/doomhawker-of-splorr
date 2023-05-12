Friend Class EditorTrigger
    Implements IEditorTrigger
    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _triggerName As String
    Public Sub New(data As WorldData, mapName As String, triggerName As String)
        _data = data
        _mapName = mapName
        _triggerName = triggerName
    End Sub
End Class
