﻿Imports System.Data.Common

Friend Class NavigateState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
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
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Zero)
        Dim avatar = Editor.Avatar
        BasePlaceMapState.DrawMap(displayBuffer, avatar.MapName, avatar.Column, avatar.Row, Zero)
    End Sub
End Class
