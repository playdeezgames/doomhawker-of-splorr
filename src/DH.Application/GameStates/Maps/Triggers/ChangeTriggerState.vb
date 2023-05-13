Friend Class ChangeTriggerState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Change Trigger:",
            Function() World.Maps.Retrieve(MapName).Triggers.Names,
            Sub(picked)
                TriggerChangeAction(picked)
                TriggerChangeAction = Nothing
                setState(Nothing, False)
            End Sub,
            Sub()
                setState(Nothing, False)
            End Sub)
    End Sub
End Class
