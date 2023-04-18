Friend Class PickFontState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _fontIndex As Integer = 0

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _fontIndex = (_fontIndex + Editor.FontNames.Count - 1) Mod Editor.FontNames.Count
            Case Command.DownReleased
                _fontIndex = (_fontIndex + 1) Mod Editor.FontNames.Count
            Case Command.LeftReleased
                SetState(GameState.FontsMenu)
            Case Command.FireReleased
                FontName = Editor.FontNames.ToList(_fontIndex)
                SetState(GameState.EditFont)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim fontList = Editor.FontNames.ToList
        If _fontIndex > fontList.Count - 1 Then
            _fontIndex = 0
        End If
        Dim y = ViewHeight \ 2 - font.Height \ 2 - _fontIndex * font.Height
        For index = 0 To fontList.Count - 1
            Dim h As Hue = If(index = _fontIndex, Hue.LightBlue, Hue.Blue)
            Dim text = fontList(index)
            font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, y), text, h)
            y += font.Height
        Next
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
