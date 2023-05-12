Friend Class PickTriggerTypeState
    Inherits BasePickState
    Const TeleportText = "Teleport"
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            Sub()
                setState(GameState.EditTrigger, False)
            End Sub,
            Sub(picked)

            End Sub,
            Function() New List(Of String) From {
                TeleportText
            })
    End Sub
End Class
