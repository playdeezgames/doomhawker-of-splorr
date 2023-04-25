Friend Class MainMenuState
    Inherits BaseMenuState
    Const EmbarkText = "Embark!"
    Const EditText = "Edit..."
    Const SaveText = "Save..."
    Const LoadText = "Load..."
    Const OptionsText = "Options"
    Const QuitText = "Quit"
    Const AboutText = "About"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                   EmbarkText,
                   EditText,
                   SaveText,
                   LoadText,
                   OptionsText,
                   AboutText,
                   QuitText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case EmbarkText
                If Editor.Avatar IsNot Nothing Then
                    SetState(GameState.Navigate)
                End If
            Case SaveText
                SetState(GameState.SaveAs)
            Case LoadText
                SetState(GameState.LoadFrom)
            Case EditText
                SetState(GameState.EditMenu)
            Case QuitText
                SetState(GameState.ConfirmQuit)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.ConfirmQuit)
    End Sub
End Class
