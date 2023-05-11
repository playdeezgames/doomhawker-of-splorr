Friend Class PickAvatarCreatureInstanceState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Sub(column, row)
                Dim creature = Editor.Maps.Retrieve(MapName).GetCell(column, row).Creature
                If creature IsNot Nothing Then
                    Editor.SetAvatar(creature)
                    setState(GameState.AvatarMenu, False)
                End If
            End Sub,
            Sub()
                setState(GameState.AvatarMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentCreature = Editor.Maps.Retrieve(MapName).GetCell(Column, Row).Creature?.Creature?.Name
        font.WriteText(displayBuffer, (Zero, Zero), $"({Column},{Row}) {If(currentCreature, "(none)")}", White)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Choosing Avatar", White)
    End Sub
End Class
