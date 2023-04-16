Friend Class EditMenuState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Const ExitEditorText = "Exit Editor"
    Const EditFontsText = "Edit Fonts..."
    ReadOnly _menuItems As IReadOnlyList(Of String) = New List(Of String) From
        {
            ExitEditorText,
            EditFontsText
        }
    Private _currentItem As Integer = 0
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.DownReleased
                _currentItem = (_currentItem + _menuItems.Count - 1) Mod _menuItems.Count
            Case Command.UpReleased
                _currentItem = (_currentItem + 1) Mod _menuItems.Count
            Case Command.FireReleased
                Select Case _menuItems(_currentItem)
                    Case ExitEditorText
                        SetState(GameState.MainMenu)
                End Select
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim index As Integer = 0
        For Each menuItem In _menuItems
            Dim h As Hue = If(index = _currentItem, Hue.LightBlue, Hue.Blue)
            font.WriteText(displayBuffer, (0, index * font.Height), menuItem, h)
            index += 1
        Next
    End Sub
    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
