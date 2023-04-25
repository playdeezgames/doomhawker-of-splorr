Imports System.Data.Common

Friend Class NavigateState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
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

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim avatar = Editor.Avatar
        BasePlaceMapState.DrawMap(displayBuffer, avatar.MapName, avatar.Column, avatar.Row, Hue.Black)
    End Sub
End Class
