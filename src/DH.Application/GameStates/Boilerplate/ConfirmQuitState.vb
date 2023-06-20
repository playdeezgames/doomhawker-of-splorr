Friend Class ConfirmQuitState
    Inherits BaseConfirmState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Are you sure you want to quit?",
            Red,
            Sub(confirmation)
                If confirmation Then
                    setState(Nothing, False)
                Else
                    setState(GameState.MainMenu, False)
                End If
            End Sub,
            Sub()
                setState(GameState.MainMenu, False)
            End Sub)
    End Sub
End Class
