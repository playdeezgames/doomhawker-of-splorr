Friend Class PlaceMapTerrainState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Sub(column, row)
                Editor.Maps.Retrieve(MapName).GetCell(column, row).Terrain = Editor.Terrains.Retrieve(TerrainName)
            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentTerrain = Editor.Maps.Retrieve(MapName).GetCell(Column, Row).Terrain.Name
        font.WriteText(displayBuffer, (0, 0), $"({Column},{Row}) {currentTerrain}", 15)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Placing: {TerrainName}", 15)
    End Sub
End Class
