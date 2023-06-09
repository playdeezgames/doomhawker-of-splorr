﻿Friend Class PickAvatarCreatureInstanceState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Function() MapName,
            Sub(column, row)
                Dim creature = World.Maps.Retrieve(MapName).GetCell(column, row).Creature
                If creature IsNot Nothing Then
                    World.SetAvatar(creature)
                    setState(GameState.AvatarMenu, False)
                End If
            End Sub,
            Sub()
                setState(GameState.AvatarMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentCreature = World.Maps.Retrieve(MapName).GetCell(Column, Row).Creature?.Creature?.Name
        font.WriteText(displayBuffer, (Zero, Zero), $"({Column},{Row}) {If(currentCreature, "(none)")}", White)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Choosing Avatar", White)
    End Sub
End Class
