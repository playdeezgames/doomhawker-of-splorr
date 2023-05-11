Friend Class MessagesState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
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
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = Zero
        For Each line In Messages.Peek.Lines
            font.WriteText(displayBuffer, (Zero, y), line.Item2, line.Item1)
            y += font.Height
        Next
    End Sub
End Class
