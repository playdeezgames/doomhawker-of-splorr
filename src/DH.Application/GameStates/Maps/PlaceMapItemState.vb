Friend Class PlaceMapItemState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Sub(column, row)
                Editor.Maps.Retrieve(MapName).GetCell(column, row).Item = Editor.Items.Retrieve(ItemName).CreateInstance(MapName, column, row)
            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentItem = Editor.Maps.Retrieve(MapName).GetCell(Column, Row).Item?.Item?.Name
        font.WriteText(displayBuffer, (Zero, Zero), $"({Column},{Row}) {If(currentItem, "(none)")}", 15)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Placing: {ItemName}", 15)
    End Sub
End Class
