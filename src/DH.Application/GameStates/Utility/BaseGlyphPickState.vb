Friend Class BaseGlyphPickState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)
    Private _row As Integer = Zero
    Private _column As Integer = Zero
    Private ReadOnly _onDone As Action(Of Char)
    Private ReadOnly _onCancel As Action
    Private ReadOnly _fontSource As Func(Of String)
    Public Sub New(
                  parent As IGameController(Of String, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  onDone As Action(Of Char),
                  onCancel As Action,
                  fontSource As Func(Of String))
        MyBase.New(parent, setState)
        _onDone = onDone
        _onCancel = onCancel
        _fontSource = fontSource
    End Sub
    Const CellRows = 6
    Const CellColumns = 16
    Const FirstCharacter = 32
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                If _row > Zero Then
                    _row -= 1
                End If
            Case Command.DownReleased
                If _row < CellRows - 1 Then
                    _row += 1
                End If
            Case Command.LeftReleased
                If _column > Zero Then
                    _column -= 1
                End If
            Case Command.RightReleased
                If _column < CellColumns - 1 Then
                    _column += 1
                End If
            Case Command.OkReleased
                _onDone(ChrW(_row * CellColumns + _column + FirstCharacter))
            Case Command.CancelReleased
                _onCancel()
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), DarkGray)
        RenderEditorFont(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim h As String = Gray
        Dim text As String
        Dim ascii = FirstCharacter + _row * CellColumns + _column
        text = $"Character: {ascii}({ChrW(ascii)})"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, Zero), text, h)
    End Sub
    Private Sub RenderEditorFont(displayBuffer As IPixelSink(Of String))
        Dim editorFont As IEditorFont = Editor.Fonts.Retrieve(_fontSource())
        Dim font As Font = editorFont.Font
        Dim cellWidth = font.TextWidth(" ")
        Dim cellHeight = font.Height
        Dim offsetX = ViewWidth \ 2 - cellWidth * CellColumns \ 2
        Dim offsetY = ViewHeight \ 2 - cellHeight * CellRows \ 2
        For row = Zero To CellRows - 1
            For column = Zero To CellColumns - 1
                Dim plotX = offsetX + cellWidth * column
                Dim plotY = offsetY + cellHeight * row
                Dim text = $"{ChrW(FirstCharacter + row * CellColumns + column)}"
                If row = _row And column = _column Then
                    displayBuffer.Fill((plotX, plotY), (cellWidth, cellHeight), White)
                    font.WriteText(displayBuffer, (plotX, plotY), text, Black)
                Else
                    displayBuffer.Fill((plotX, plotY), (cellWidth, cellHeight), Black)
                    font.WriteText(displayBuffer, (plotX, plotY), text, Gray)
                End If
            Next
        Next
    End Sub
End Class
