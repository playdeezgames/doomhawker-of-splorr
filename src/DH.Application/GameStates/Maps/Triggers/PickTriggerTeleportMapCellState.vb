Friend Class PickTriggerTeleportMapCellState
    Inherits BasePlaceMapState

    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Function() World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).Teleport.MapName,
            Sub(column, row)
                Dim teleport = World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).Teleport
                teleport.Column = column
                teleport.Row = row
                setState(GameState.EditTrigger, False)
            End Sub,
            Sub()
                'no cancelling!
            End Sub)
    End Sub
End Class
