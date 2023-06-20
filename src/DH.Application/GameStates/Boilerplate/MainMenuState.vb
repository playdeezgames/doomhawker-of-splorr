Friend Class MainMenuState
    Inherits BaseMenuState
    Const EmbarkText = "Embark!"
    Const EditText = "Edit..."
    Const SaveText = "Save..."
    Const LoadText = "Load..."
    Const OptionsText = "Options"
    Const QuitText = "Quit"
    Const AboutText = "About"
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent,
                   setState,
                   "Main Menu:",
                   New List(Of String) From {
                       EmbarkText,
                       EditText,
                       SaveText,
                       LoadText,
                       OptionsText,
                       AboutText,
                       QuitText},
                   Sub(menuItem)
                       Select Case menuItem
                           Case EmbarkText
                               If World.Avatar IsNot Nothing Then
                                   setState(GameState.Neutral, False)
                               End If
                           Case SaveText
                               setState(GameState.SaveAs, False)
                           Case LoadText
                               setState(GameState.LoadFrom, False)
                           Case EditText
                               setState(GameState.EditMenu, False)
                           Case QuitText
                               setState(GameState.ConfirmQuit, False)
                           Case OptionsText
                               setState(GameState.Options, False)
                       End Select
                   End Sub,
                   Sub()
                       setState(GameState.ConfirmQuit, False)
                   End Sub)
    End Sub
End Class
