﻿Friend Class EditTriggersState
    Inherits BaseMenuState
    Const NewTriggerText = "New Trigger..."
    Const PickTriggerText = "Pick Trigger..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                NewTriggerText,
                PickTriggerText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case NewTriggerText
                        setState(GameState.NewTriggerName, False)
                    Case PickTriggerText
                        If World.Maps.Retrieve(MapName).Triggers.HasAny Then
                            setState(GameState.PickTrigger, False)
                        Else
                            setState(GameState.NewTriggerName, False)
                        End If
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim map = World.Maps.Retrieve(MapName)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Trigger Count: {map.Triggers.Names.Count}", White)
    End Sub
End Class
