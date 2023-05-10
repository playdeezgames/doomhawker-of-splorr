Friend Class EditGlyphState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Private _x As Integer = 0
    Private _y As Integer = 0
    Public Overrides Sub HandleCommand(command As Command)
        Dim glyph = Editor.Fonts.Retrieve(EditorContext.FontName).GetGlyph(EditorContext.GlyphKey)
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
        SetState(GameState.EditFont)
    End Sub
    Const CellWidth = 8
    Const CellHeight = 8
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), 8)
        Dim glyph = Editor.Fonts.Retrieve(EditorContext.FontName).GetGlyph(EditorContext.GlyphKey)
        For x = 0 To glyph.Width - 1
            For y = 0 To glyph.Height - 1
                Dim plot = (ViewWidth - glyph.Width * CellWidth + x * CellWidth, y * CellHeight)
                If glyph.IsSet(x, y) Then
                    displayBuffer.Fill(plot, (CellWidth, CellHeight), 15)
                Else
                    displayBuffer.Fill(plot, (CellWidth, CellHeight), 0)
                End If
                If x = _x AndAlso y = _y Then
                    displayBuffer.Frame(plot, (CellWidth, CellHeight), 9)
                End If
            Next
        Next
        Dim font = Fonts(GameFont.Font5x7)
        If _x = -1 Then
            font.WriteText(displayBuffer, (0, 0), "Done", 15)
        Else
            font.WriteText(displayBuffer, (0, 0), $"X:{_x}", 7)
            font.WriteText(displayBuffer, (0, font.Height), $"Y:{_y}", 7)
        End If
    End Sub
End Class
