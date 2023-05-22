Friend Class PlaceMapTriggerState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Function() MapName,
            Sub(column, row)
                Dim map = World.Maps.Retrieve(MapName)
                map.GetCell(column, row).Trigger = map.Triggers.Retrieve(TriggerName)
            End Sub,
            Sub()
                setState(GameState.EditTrigger, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim trigger = World.Maps.Retrieve(MapName).GetCell(Column, Row).Trigger
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), If(trigger?.Name, ""), White)
    End Sub
End Class
