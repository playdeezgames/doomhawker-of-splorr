Friend Class TitleState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font8x8)
        font.WriteText(displayBuffer, (0, ViewHeight \ 2 - font.Height * 2 - font.Height \ 2), "Doomhawker", Hue.Yellow)
        font.WriteText(displayBuffer, (0, ViewHeight \ 2 - font.Height \ 2), "of", Hue.Yellow)
        font.WriteText(displayBuffer, (0, ViewHeight \ 2 + font.Height * 2 - font.Height \ 2), "SPLORR!!", Hue.Yellow)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
