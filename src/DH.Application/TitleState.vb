Friend Class TitleState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        If command = Command.FireReleased Then
            SetState(GameState.MainMenu)
        End If
    End Sub

    ReadOnly hues As IReadOnlyList(Of Hue) = New List(Of Hue) From
        {
            Hue.White,
            Hue.Yellow,
            Hue.LightGreen,
            Hue.LightMagenta,
            Hue.LightRed,
            Hue.LightCyan,
            Hue.LightBlue
        }
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        RenderTitle(displayBuffer)
        RenderFooter(displayBuffer)
    End Sub

    Private Sub RenderFooter(displayBuffer As IPixelSink(Of Hue))
        Dim font = Fonts(GameFont.Font3x5)
        Const text = "Press FIRE"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, ViewHeight - font.Height), text, Hue.Gray)
    End Sub

    Private Sub RenderTitle(displayBuffer As IPixelSink(Of Hue))
        Dim font = Fonts(GameFont.Font8x8)
        Dim h = RNG.FromEnumerable(hues)
        font.WriteText(displayBuffer, (0, ViewHeight \ 2 - font.Height * 3 - font.Height \ 2), "Doomhawker", h)
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth("of") \ 2, ViewHeight \ 2 - font.Height \ 2), "of", h)
        font.WriteText(displayBuffer, (ViewWidth - font.TextWidth("SPLORR!!"), ViewHeight \ 2 + font.Height * 3 - font.Height \ 2), "SPLORR!!", h)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
