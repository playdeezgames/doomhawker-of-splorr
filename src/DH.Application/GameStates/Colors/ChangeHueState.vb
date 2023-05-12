Friend Class ChangeHueState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Change Hue:",
            Sub()
                setState(Nothing, False)
            End Sub,
            Sub(picked)
                HueChangeAction(picked)
                HueChangeAction = Nothing
                setState(Nothing, False)
            End Sub,
            Function() World.Colors.Names)
    End Sub
End Class
