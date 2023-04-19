﻿Friend Class PickFontState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _index As Integer = 0

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Private Function ListItemSource() As IEnumerable(Of String)
        Return Editor.FontNames
    End Function

    Protected Sub HandlePick(picked As String)
        FontName = picked
        SetState(GameState.EditFont)
    End Sub

    Protected Sub HandleCancel()
        SetState(GameState.FontsMenu)
    End Sub

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
        Dim fontList = ListItemSource().ToList
        If _index > fontList.Count - 1 Then
            _index = 0
        End If
        Dim y = ViewHeight \ 2 - font.Height \ 2 - _index * font.Height
        For index = 0 To fontList.Count - 1
            Dim h As Hue = If(index = _index, Hue.LightBlue, Hue.Blue)
            Dim text = fontList(index)
            font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, y), text, h)
            y += font.Height
        Next
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
