Friend Class PlaceMapItemState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Sub(column, row)
                World.Maps.Retrieve(MapName).GetCell(column, row).Item = World.Items.Retrieve(ItemName).CreateInstance(MapName, column, row)
            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentItem = World.Maps.Retrieve(MapName).GetCell(Column, Row).Item?.Item?.Name
        font.WriteText(displayBuffer, (Zero, Zero), $"({Column},{Row}) {If(currentItem, "(none)")}", White)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Placing: {ItemName}", White)
    End Sub
End Class
