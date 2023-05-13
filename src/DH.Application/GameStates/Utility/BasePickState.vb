Friend Class BasePickState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)
    Private _index As Integer = Zero
    Private ReadOnly _caption As String = ""
    Private ReadOnly _onPick As Action(Of String)
    Private ReadOnly _onCancel As Action
    Private ReadOnly _listSource As Func(Of IEnumerable(Of String))

    Public Sub New(
                  parent As IGameController(Of String, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  caption As String,
                  listSource As Func(Of IEnumerable(Of String)),
                  onPick As Action(Of String),
                  onCancel As Action)
        MyBase.New(parent, setState)
        _caption = caption
        _onCancel = onCancel
        _onPick = onPick
        _listSource = listSource
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _index = (_index + _listSource().Count - 1) Mod _listSource().Count
            Case Command.DownReleased
                _index = (_index + 1) Mod _listSource().Count
            Case Command.CancelReleased
                _onCancel()
            Case Command.OkReleased
                _onPick(_listSource().ToList(_index))
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim listItems = _listSource().ToList
        If _index > listItems.Count - 1 Then
            _index = Zero
        End If
        Dim y = ViewHeight \ 2 - font.Height \ 2 - _index * font.Height
        For index = Zero To listItems.Count - 1
            Dim h As String = If(index = _index, LightBlue, Blue)
            Dim text = listItems(index)
            font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, y), text, h)
            y += font.Height
        Next
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(_caption) \ 2, Zero), _caption, White)
    End Sub
End Class
