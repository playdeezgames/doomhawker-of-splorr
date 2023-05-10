Friend Class BaseGlyphPickState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)
    Private _row As Integer = 0
    Private _column As Integer = 0
    Private ReadOnly _onDone As Action(Of Char)
    Private ReadOnly _onCancel As Action
    Private ReadOnly _fontSource As Func(Of String)
    Public Sub New(
                  parent As IGameController(Of Integer, Command, Sfx),
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
                If _row = 0 Then
                    _onCancel()
                Else
                    _row -= 1
                End If
            Case Command.DownReleased
                If _row = CellRows - 1 Then
                    _onCancel()
                Else
                    _row += 1
                End If
            Case Command.LeftReleased
                If _column = 0 Then
                    _onCancel()
                Else
                    _column -= 1
                End If
            Case Command.RightReleased
                If _column = CellColumns - 1 Then
                    _onCancel()
                Else
                    _column += 1
                End If
            Case Command.FireReleased
                _onDone(ChrW(_row * CellColumns + _column + FirstCharacter))
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), 8)
        RenderEditorFont(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim h As Integer = 7
        Dim text As String
        Dim ascii = FirstCharacter + _row * CellColumns + _column
        text = $"Character: {ascii}({ChrW(ascii)})"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, 0), text, h)
    End Sub
    Private Sub RenderEditorFont(displayBuffer As IPixelSink(Of Integer))
        Dim editorFont As IEditorFont = Editor.Fonts.Retrieve(_fontSource())
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
                    displayBuffer.Fill((plotX, plotY), (cellWidth, cellHeight), 15)
                    font.WriteText(displayBuffer, (plotX, plotY), text, 0)
                Else
                    displayBuffer.Fill((plotX, plotY), (cellWidth, cellHeight), 0)
                    font.WriteText(displayBuffer, (plotX, plotY), text, 7)
                End If
            Next
        Next
    End Sub
End Class
