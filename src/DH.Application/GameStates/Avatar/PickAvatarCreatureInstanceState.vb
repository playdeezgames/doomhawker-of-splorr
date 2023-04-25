Friend Class PickAvatarCreatureInstanceState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
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
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentCreature = Editor.Maps.Retrieve(MapName).GetCell(Column, Row).Creature?.Creature?.Name
        font.WriteText(displayBuffer, (0, 0), $"({Column},{Row}) {If(currentCreature, "(none)")}", Hue.White)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Choosing Avatar", Hue.White)
    End Sub
End Class
