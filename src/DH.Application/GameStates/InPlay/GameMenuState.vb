Imports System.Globalization

Friend Class GameMenuState
    Inherits BaseMenuState
    Const NavigateText = "Navigate"
    Const AbandonGameText = "Abandon Game"

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                   NavigateText,
                   AbandonGameText})
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NavigateText
                PopState()
                SetState(GameState.Navigate)
            Case AbandonGameText
                PopState()
                SetState(GameState.MainMenu)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        PopState()
    End Sub
End Class
