Friend Class ConfirmQuitState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _confirmation As Boolean = False

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased, Command.DownReleased
                _confirmation = Not _confirmation
            Case Command.LeftReleased
                SetState(GameState.MainMenu)
            Case Command.FireReleased
                If _confirmation Then
                    QuitRequested = True
                Else
                    SetState(GameState.MainMenu)
                End If
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, 0), "Are you sure you want to quit?", Hue.Red)
        font.WriteText(displayBuffer, (0, font.Height), "No", If(_confirmation, Hue.Blue, Hue.LightBlue))
        font.WriteText(displayBuffer, (0, font.Height * 2), "Yes", If(_confirmation, Hue.LightBlue, Hue.Blue))
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
