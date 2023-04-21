Friend MustInherit Class BasePickState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _index As Integer = 0
    Private ReadOnly _caption As String = ""

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState), caption As String)
        MyBase.New(parent, setState)
        _caption = caption
    End Sub
    Protected MustOverride Function ListItemSource() As IEnumerable(Of String)
    Protected MustOverride Sub HandlePick(picked As String)
    Protected MustOverride Sub HandleCancel()
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _index = (_index + ListItemSource().Count - 1) Mod ListItemSource().Count
            Case Command.DownReleased
                _index = (_index + 1) Mod ListItemSource().Count
            Case Command.LeftReleased
                HandleCancel()
            Case Command.FireReleased
                HandlePick(ListItemSource().ToList(_index))
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim listItems = ListItemSource().ToList
        If _index > listItems.Count - 1 Then
            _index = 0
        End If
        Dim y = ViewHeight \ 2 - font.Height \ 2 - _index * font.Height
        For index = 0 To listItems.Count - 1
            Dim h As Hue = If(index = _index, Hue.LightBlue, Hue.Blue)
            Dim text = listItems(index)
            font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, y), text, h)
            y += font.Height
        Next
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(_caption) \ 2, 0), _caption, Hue.White)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
