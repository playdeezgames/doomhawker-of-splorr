Friend Class EditGlyphState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Private _x As Integer = Zero
    Private _y As Integer = Zero
    Public Overrides Sub HandleCommand(command As Command)
        Dim glyph = World.Fonts.Retrieve(WorldContext.FontName).GetGlyph(WorldContext.GlyphKey)
        Select Case command
            Case Command.LeftReleased
                If _x > Zero Then
                    _x -= 1
                End If
            Case Command.RightReleased
                If _x < glyph.Width - 1 Then
                    _x += 1
                End If
            Case Command.UpReleased
                If _y > Zero Then
                    _y -= 1
                End If
            Case Command.DownReleased
                If _y < glyph.Height - 1 Then
                    _y += 1
                End If
            Case Command.OkReleased
                glyph.Toggle(_x, _y)
            Case Command.CancelReleased
                HandleDone()
        End Select
    End Sub
    Private Sub HandleDone()
        _x = Zero
        _y = Zero
        SetState(GameState.EditFont)
    End Sub
    Const CellWidth = 8
    Const CellHeight = 8
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), DarkGray)
        Dim glyph = World.Fonts.Retrieve(WorldContext.FontName).GetGlyph(WorldContext.GlyphKey)
        For x = Zero To glyph.Width - 1
            For y = Zero To glyph.Height - 1
                Dim plot = (ViewWidth - glyph.Width * CellWidth + x * CellWidth, y * CellHeight)
                If glyph.IsSet(x, y) Then
                    displayBuffer.Fill(plot, (CellWidth, CellHeight), White)
                Else
                    displayBuffer.Fill(plot, (CellWidth, CellHeight), Black)
                End If
                If x = _x AndAlso y = _y Then
                    displayBuffer.Frame(plot, (CellWidth, CellHeight), LightBlue)
                End If
            Next
        Next
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, Zero), $"X:{_x}", Gray)
        font.WriteText(displayBuffer, (Zero, font.Height), $"Y:{_y}", Gray)
    End Sub
End Class
