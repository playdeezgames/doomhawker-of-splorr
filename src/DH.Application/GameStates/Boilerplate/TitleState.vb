Friend Class TitleState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        If command = Command.FireReleased Then
            SetState(GameState.MainMenu)
        End If
    End Sub

    ReadOnly hues As IReadOnlyList(Of String) = New List(Of String) From
        {
            White,
            Yellow,
            LightMagenta,
            LightRed,
            LightCyan,
            LightGreen,
            LightBlue
        }
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        RenderTitle(displayBuffer)
        RenderFooter(displayBuffer)
    End Sub

    Private Sub RenderFooter(displayBuffer As IPixelSink(Of String))
        Dim font = Fonts(GameFont.Font3x5)
        Const text = "Press FIRE"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, ViewHeight - font.Height), text, Gray)
    End Sub

    Private Sub RenderTitle(displayBuffer As IPixelSink(Of String))
        Dim font = Fonts(GameFont.Font8x8)
        Dim h = RNG.FromEnumerable(hues)
        font.WriteText(displayBuffer, (Zero, ViewHeight \ 2 - font.Height * 3 - font.Height \ 2), "Doomhawker", h)
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth("of") \ 2, ViewHeight \ 2 - font.Height \ 2), "of", h)
        font.WriteText(displayBuffer, (ViewWidth - font.TextWidth("SPLORR!!"), ViewHeight \ 2 + font.Height * 3 - font.Height \ 2), "SPLORR!!", h)
    End Sub
End Class
