Friend Class PlaceMapTerrainState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Function() MapName,
            Sub(column, row)
                World.Maps.Retrieve(MapName).GetCell(column, row).Terrain = World.Terrains.Retrieve(TerrainName)
            End Sub,
            Sub()
                setState(GameState.EditMap, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentTerrain = World.Maps.Retrieve(MapName).GetCell(Column, Row).Terrain.Name
        font.WriteText(displayBuffer, (Zero, Zero), $"({Column},{Row}) {currentTerrain}", White)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Placing: {TerrainName}", White)
    End Sub
End Class
