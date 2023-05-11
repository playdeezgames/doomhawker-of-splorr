Friend Class NavigateState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.FireReleased
                SetStates(GameState.GameMenu, GameState.Navigate)
            Case Command.UpReleased
                Editor.Avatar.Creature.Move(Direction.North)
            Case Command.DownReleased
                Editor.Avatar.Creature.Move(Direction.South)
            Case Command.LeftReleased
                Editor.Avatar.Creature.Move(Direction.West)
            Case Command.RightReleased
                Editor.Avatar.Creature.Move(Direction.East)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        Dim avatar = Editor.Avatar
        BasePlaceMapState.DrawMap(displayBuffer, avatar.MapName, avatar.Column, avatar.Row, Black)
    End Sub
End Class
