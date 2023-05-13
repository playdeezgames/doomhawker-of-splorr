Friend Class EditTriggerState
    Inherits BaseMenuState
    Const ChangeTriggerTypeText = "Change Trigger Type..."
    Const SetNextTriggerText = "Set Next Trigger..."
    Const ClearNextTriggerText = "Clear Next Trigger"
    Const RenameTriggerText = "Rename Trigger..."
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                ChangeTriggerTypeText,
                SetNextTriggerText,
                ClearNextTriggerText,
                RenameTriggerText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case ChangeTriggerTypeText
                        setState(GameState.PickTriggerType, False)
                    Case RenameTriggerText
                        setState(GameState.RenameTrigger, False)
                    Case SetNextTriggerText
                        TriggerChangeAction = Sub(newTriggerName)
                                                  World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).NextTrigger = World.Maps.Retrieve(MapName).Triggers.Retrieve(newTriggerName)
                                              End Sub
                        setState(GameState.ChangeTrigger, True)
                    Case ClearNextTriggerText
                        World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).NextTrigger = Nothing
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
        If trigger.NextTrigger IsNot Nothing Then
            y -= font.Height
        End If
        font.WriteText(displayBuffer, (0, y), $"Trigger: {MapName}/{trigger.Name}", White)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Trigger Type: {trigger.TriggerType}", White)
        y += font.Height
        If trigger.NextTrigger IsNot Nothing Then
            font.WriteText(displayBuffer, (0, y), $"Next Trigger: {trigger.NextTrigger.Name}", White)
            y += font.Height
        End If
    End Sub
End Class
