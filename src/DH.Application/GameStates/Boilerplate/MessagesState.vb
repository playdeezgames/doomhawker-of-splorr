Friend Class MessagesState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        If command = Command.FireReleased Then
            Messages.Dequeue()
            If Not Messages.Any Then
                PopState()
            End If
        End If
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = 0
        For Each line In Messages.Peek.Lines
            font.WriteText(displayBuffer, (0, y), line.Item2, line.Item1)
            y += font.Height
        Next
    End Sub
End Class
