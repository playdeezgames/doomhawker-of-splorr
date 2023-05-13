Friend Class EditTriggerState
    Inherits BaseMenuState
    Const ChangeTriggerTypeText = "Change Trigger Type..."
    Const RenameTriggerText = "Rename Trigger..."
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                ChangeTriggerTypeText,
                RenameTriggerText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case ChangeTriggerTypeText
                        setState(GameState.PickTriggerType, False)
                    Case RenameTriggerText
                        setState(GameState.RenameTrigger, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.EditTriggers, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim trigger = World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName)
        Dim y = ViewHeight - font.Height * 2
        font.WriteText(displayBuffer, (0, y), $"Trigger: {MapName}/{TriggerName}", White)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Trigger Type: {trigger.TriggerType}", White)
    End Sub
End Class
