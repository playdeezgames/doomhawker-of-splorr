Friend Class EditGlyphState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
    End Sub
    Const CellWidth = 8
    Const CellHeight = 8
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.DarkGray)
        Dim glyph = Editor.GetFont(GameContext.FontName).GetGlyph(GameContext.GlyphKey)
        For x = 0 To glyph.Width - 1
            For y = 0 To glyph.Height - 1
                If glyph.IsSet(x, y) Then
                    displayBuffer.Fill((x * CellWidth, y * CellHeight), (CellWidth, CellHeight), Hue.White)
                Else
                    displayBuffer.Fill((x * CellWidth, y * CellHeight), (CellWidth, CellHeight), Hue.Black)
                End If
            Next
        Next
    End Sub
    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
