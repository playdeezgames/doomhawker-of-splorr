Friend Class EditTriggersState
    Inherits BaseMenuState
    Const NewTriggerText = "New Trigger..."
    Const PickTriggerText = "Pick Trigger..."
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                NewTriggerText,
                PickTriggerText
            },
            Sub(menuItem)

            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim map = World.Maps.Retrieve(MapName)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Trigger Count: {map.Triggers.Names.Count}", White)
    End Sub
End Class
