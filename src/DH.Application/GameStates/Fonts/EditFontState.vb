Friend Class EditFontState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _row As Integer = 0
    Private _column As Integer = 0
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub
    Const CellRows = 6
    Const CellColumns = 16
    Const FirstCharacter = 32
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _row = Math.Max(-1, _row - 1)
            Case Command.DownReleased
                _row = Math.Min(CellRows - 1, _row + 1)
            Case Command.LeftReleased
                _column = (_column + CellColumns - 1) Mod CellColumns
            Case Command.RightReleased
                _column = (_column + 1) Mod CellColumns
            Case Command.FireReleased
                If _row < 0 Then
                    SetState(GameState.FontsMenu)
                Else
                    GlyphKey = ChrW(_row * CellColumns + _column + FirstCharacter)
                    SetState(GameState.EditGlyph)
                End If
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.DarkGray)
        RenderEditorFont(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim h As Hue = Hue.Gray
        Dim text As String
        If _row >= 0 Then
            Dim ascii = FirstCharacter + _row * CellColumns + _column
            text = $"Character: {ascii}({ChrW(ascii)})"
        Else
            text = "Done Editing"
            h = Hue.White
        End If
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, 0), text, h)
    End Sub

    Private Sub RenderEditorFont(displayBuffer As IPixelSink(Of Hue))
        Dim editorFont As IEditorFont = Editor.GetFont(FontName)
        Dim font As Font = editorFont.Font
        Dim cellWidth = font.TextWidth(" ")
        Dim cellHeight = font.Height
        Dim offsetX = ViewWidth \ 2 - cellWidth * CellColumns \ 2
        Dim offsetY = ViewHeight \ 2 - cellHeight * CellRows \ 2
        For row = 0 To CellRows - 1
            For column = 0 To CellColumns - 1
                Dim plotX = offsetX + cellWidth * column
                Dim plotY = offsetY + cellHeight * row
                Dim text = $"{ChrW(FirstCharacter + row * CellColumns + column)}"
                If row = _row And column = _column Then
                    displayBuffer.Fill((plotX, plotY), (cellWidth, cellHeight), Hue.White)
                    font.WriteText(displayBuffer, (plotX, plotY), text, Hue.Black)
                Else
                    displayBuffer.Fill((plotX, plotY), (cellWidth, cellHeight), Hue.Black)
                    font.WriteText(displayBuffer, (plotX, plotY), text, Hue.Gray)
                End If
            Next
        Next
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
