Friend Class PlaceMapCreatureState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Sub(column, row)
                Editor.Maps.Retrieve(MapName).GetCell(column, row).Creature = Editor.Creatures.Retrieve(CreatureName).CreateInstance(MapName, column, row)
            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentCreature = Editor.Maps.Retrieve(MapName).GetCell(Column, Row).Creature?.Creature?.Name
        font.WriteText(displayBuffer, (Zero, Zero), $"({Column},{Row}) {If(currentCreature, "(none)")}", White)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Placing: {CreatureName}", White)
    End Sub
End Class
