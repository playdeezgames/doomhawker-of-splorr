Friend Class NavigateState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.OkReleased, Command.CancelReleased
                SetStates(GameState.GameMenu, GameState.Neutral)
            Case Command.UpReleased
                World.Avatar.Move(Direction.North)
            Case Command.DownReleased
                World.Avatar.Move(Direction.South)
            Case Command.LeftReleased
                World.Avatar.Move(Direction.West)
            Case Command.RightReleased
                World.Avatar.Move(Direction.East)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        Dim avatar = World.Avatar
        BasePlaceMapState.DrawMap(displayBuffer, avatar.MapName, avatar.Column, avatar.Row, Black)
    End Sub
End Class
