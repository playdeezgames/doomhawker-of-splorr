Friend Class GameMenuState
    Inherits BaseMenuState
    Const NavigateText = "Navigate"
    Const AbandonGameText = "Abandon Game"

    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                   NavigateText,
                   AbandonGameText},
            Sub(menuItem)
                Select Case menuItem
                    Case NavigateText
                        setState(Nothing, False)
                        setState(GameState.Neutral, False)
                    Case AbandonGameText
                        setState(GameState.ConfirmAbandon, False)
                End Select
            End Sub,
            Sub()
                setState(Nothing, False)
            End Sub)
    End Sub
End Class
