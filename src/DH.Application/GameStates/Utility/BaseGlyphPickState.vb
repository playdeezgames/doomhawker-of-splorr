Friend MustInherit Class BaseGlyphPickState
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
                If _row = 0 Then
                    HandleCancel()
                Else
                    _row -= 1
                End If
            Case Command.DownReleased
                If _row = CellRows - 1 Then
                    HandleCancel()
                Else
                    _row += 1
                End If
            Case Command.LeftReleased
                If _column = 0 Then
                    HandleCancel()
                Else
                    _column -= 1
                End If
            Case Command.RightReleased
                If _column = CellColumns - 1 Then
                    HandleCancel()
                Else
                    _column += 1
                End If
            Case Command.FireReleased
                HandleDone(ChrW(_row * CellColumns + _column + FirstCharacter))
        End Select
    End Sub
    Protected MustOverride Sub HandleDone(glyph As Char)
    Protected MustOverride Sub HandleCancel()
    Protected MustOverride Function FontNameSource() As String
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.DarkGray)
        RenderEditorFont(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim h As Hue = Hue.Gray
        Dim text As String
        Dim ascii = FirstCharacter + _row * CellColumns + _column
        text = $"Character: {ascii}({ChrW(ascii)})"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, 0), text, h)
    End Sub
    Private Sub RenderEditorFont(displayBuffer As IPixelSink(Of Hue))
        Dim editorFont As IEditorFont = Editor.GetFont(FontNameSource)
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
