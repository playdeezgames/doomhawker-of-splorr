Friend Class FontsMenuState
    Inherits BaseMenuState
    Const GoBackText = "Go Back"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {GoBackText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case GoBackText
                SetState(GameState.EditMenu)
        End Select
    End Sub
End Class
