Friend Class PlaceMapTerrainState
    Inherits BasePlaceMapState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
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
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentTerrain = Editor.Maps.Retrieve(MapName).GetCell(Column, Row).Terrain.TerrainName
        font.WriteText(displayBuffer, (0, 0), $"({Column},{Row}) {currentTerrain}", Hue.White)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Placing: {TerrainName}", Hue.White)
    End Sub
End Class
