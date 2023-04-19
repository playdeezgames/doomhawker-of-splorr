Friend Class EditGlyphState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub
    Private _x As Integer = 0
    Private _y As Integer = 0
    Public Overrides Sub HandleCommand(command As Command)
        Dim glyph = Editor.GetFont(EditorContext.FontName).GetGlyph(EditorContext.GlyphKey)
        Select Case command
            Case Command.LeftReleased
                _x = Math.Max(_x - 1, -1)
            Case Command.RightReleased
                _x = Math.Min(_x + 1, glyph.Width - 1)
            Case Command.UpReleased
                _y = Math.Max(_y - 1, 0)
            Case Command.DownReleased
                _y = Math.Min(_y + 1, glyph.Height - 1)
            Case Command.FireReleased
                If _x = -1 Then
                    SetState(GameState.EditFont)
                Else
                    glyph.Toggle(_x, _y)
                End If
        End Select
    End Sub
    Const CellWidth = 8
    Const CellHeight = 8
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.DarkGray)
        Dim glyph = Editor.GetFont(EditorContext.FontName).GetGlyph(EditorContext.GlyphKey)
        For x = 0 To glyph.Width - 1
            For y = 0 To glyph.Height - 1
                Dim plot = (ViewWidth - glyph.Width * CellWidth + x * CellWidth, y * CellHeight)
                If glyph.IsSet(x, y) Then
                    displayBuffer.Fill(plot, (CellWidth, CellHeight), Hue.White)
                Else
                    displayBuffer.Fill(plot, (CellWidth, CellHeight), Hue.Black)
                End If
                If x = _x AndAlso y = _y Then
                    displayBuffer.Frame(plot, (CellWidth, CellHeight), Hue.LightBlue)
                End If
            Next
        Next
        Dim font = Fonts(GameFont.Font5x7)
        If _x = -1 Then
            font.WriteText(displayBuffer, (0, 0), "Done", Hue.White)
        Else
            font.WriteText(displayBuffer, (0, 0), $"X:{_x}", Hue.Gray)
            font.WriteText(displayBuffer, (0, font.Height), $"Y:{_y}", Hue.Gray)
        End If
    End Sub
    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
