Friend Class MainMenuState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Const EmbarkText = "Embark!"
    Const OptionsText = "Options"
    Const QuitText = "Quit"
    Const AboutText = "About"
    Private ReadOnly _menuItems As IReadOnlyList(Of String) =
        New List(Of String) From
        {
            EmbarkText,
            OptionsText,
            AboutText,
            QuitText
        }
    Private _currentMenuItem As Integer = 0

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _currentMenuItem = (_currentMenuItem + _menuItems.Count - 1) Mod _menuItems.Count
            Case Command.DownReleased
                _currentMenuItem = (_currentMenuItem + 1) Mod _menuItems.Count
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim row = 0
        For Each menuItem In _menuItems
            Dim h = If(row = _currentMenuItem, Hue.LightBlue, Hue.Blue)
            font.WriteText(displayBuffer, (0, font.Height * row), menuItem, h)
            row += 1
        Next
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
