Friend Class MainMenuState
    Inherits BaseMenuState
    Const EditText = "Edit"
    Const SaveText = "Save..."
    Const LoadText = "Load..."
    Const OptionsText = "Options"
    Const QuitText = "Quit"
    Const AboutText = "About"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {EditText, SaveText, LoadText, OptionsText, AboutText, QuitText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case SaveText
                SetState(GameState.SaveAs, False)
            Case LoadText
                SetState(GameState.LoadFrom, False)
            Case EditText
                SetState(GameState.EditMenu, False)
            Case QuitText
                SetState(GameState.ConfirmQuit, False)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.ConfirmQuit, False)
    End Sub
End Class
