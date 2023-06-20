Friend Class ChangeHueState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Change Hue:",
            Function() World.Colors.Names.Select(Function(x) x.ToString),
            Sub(picked)
                HueChangeAction(CInt(picked))
                HueChangeAction = Nothing
                setState(Nothing, False)
            End Sub,
            Sub()
                setState(Nothing, False)
            End Sub)
    End Sub
End Class
