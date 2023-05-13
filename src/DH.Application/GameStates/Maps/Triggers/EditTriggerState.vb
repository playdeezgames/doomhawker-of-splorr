Friend Class EditTriggerState
    Inherits BaseMenuState
    Const ChangeTriggerTypeText = "Change Trigger Type..."
    Const TriggerDetailsText = "Trigger Details..."
    Const SetNextTriggerText = "Set Next Trigger..."
    Const ClearNextTriggerText = "Clear Next Trigger"
    Const RenameTriggerText = "Rename Trigger..."
    Const CloneTriggerText = "Clone Trigger..."
    Const DeleteTriggerText = "Delete Trigger"
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                ChangeTriggerTypeText,
                TriggerDetailsText,
                SetNextTriggerText,
                ClearNextTriggerText,
                RenameTriggerText,
                CloneTriggerText,
                DeleteTriggerText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case CloneTriggerText
                        setState(GameState.CloneTrigger, False)
                    Case TriggerDetailsText
                        Select Case World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).TriggerType
                            Case Data.TriggerType.Teleport
                                setState(GameState.PickTriggerTeleportMap, False)
                            Case Else
                                Throw New NotImplementedException
                        End Select
                    Case DeleteTriggerText
                        setState(GameState.ConfirmDeleteTrigger, False)
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
        If trigger.TriggerType = Data.TriggerType.Teleport Then
            y -= font.Height
        End If
        font.WriteText(displayBuffer, (0, y), $"Trigger: {MapName}/{trigger.Name}", White)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Trigger Type: {trigger.TriggerType}", White)
        y += font.Height
        If trigger.TriggerType = Data.TriggerType.Teleport Then
            Dim teleport = trigger.Teleport
            font.WriteText(displayBuffer, (0, y), $"Teleport To: {teleport.MapName}({teleport.Column},{teleport.Row})", White)
            y += font.Height
        End If
        If trigger.NextTrigger IsNot Nothing Then
            font.WriteText(displayBuffer, (0, y), $"Next Trigger: {trigger.NextTrigger.Name}", White)
            y += font.Height
        End If
    End Sub
End Class
