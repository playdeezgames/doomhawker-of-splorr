﻿Friend Class MainMenuState
    Inherits BaseMenuState
    Const EditText = "Edit"
    Const OptionsText = "Options"
    Const QuitText = "Quit"
    Const AboutText = "About"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {EditText, OptionsText, AboutText, QuitText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case EditText
                SetState(GameState.EditMenu)
            Case QuitText
                SetState(GameState.ConfirmQuit)
        End Select
    End Sub
End Class
