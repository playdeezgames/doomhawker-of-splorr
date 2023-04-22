Friend Class EditGlyphState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Private _x As Integer = 0
    Private _y As Integer = 0
    Public Overrides Sub HandleCommand(command As Command)
        Dim glyph = Editor.GetFont(EditorContext.FontName).GetGlyph(EditorContext.GlyphKey)
        Select Case command
            Case Command.LeftReleased
                If _x = 0 Then
                    HandleDone()
                Else
                    _x -= 1
                End If
            Case Command.RightReleased
                If _x = glyph.Width - 1 Then
                    HandleDone()
                Else
                    _x += 1
                End If
            Case Command.UpReleased
                If _y = 0 Then
                    HandleDone()
                Else
                    _y -= 1
                End If
            Case Command.DownReleased
                If _y = glyph.Height - 1 Then
                    HandleDone()
                Else
                    _y = _y + 1
                End If
            Case Command.FireReleased
                glyph.Toggle(_x, _y)
        End Select
    End Sub
    Private Sub HandleDone()
        _x = 0
        _y = 0
        SetState(GameState.EditFont, False)
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
