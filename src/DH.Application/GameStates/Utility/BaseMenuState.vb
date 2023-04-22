Friend MustInherit Class BaseMenuState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _menuItems As IReadOnlyList(Of String)
    Private _currentItem As Integer = 0

    Protected Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean), menuItems As IReadOnlyList(Of String))
        MyBase.New(parent, setState)
        _menuItems = menuItems
    End Sub

    MustOverride Sub HandleMenuItem(menuItem As String)

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _currentItem = (_currentItem + _menuItems.Count - 1) Mod _menuItems.Count
            Case Command.DownReleased
                _currentItem = (_currentItem + 1) Mod _menuItems.Count
            Case Command.FireReleased
                HandleMenuItem(_menuItems(_currentItem))
            Case Command.LeftReleased
                HandleCancel()
        End Select
    End Sub
    Protected MustOverride Sub HandleCancel()
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim row = 0
        For Each menuItem In _menuItems
            Dim h As Hue = If(row = _currentItem, Hue.LightBlue, Hue.Blue)
            font.WriteText(displayBuffer, (0, row * font.Height), menuItem, h)
            row += 1
        Next
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
