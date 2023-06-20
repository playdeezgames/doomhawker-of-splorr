Friend Class NewMapSizeState
    Inherits BaseMenuState
    Const CreateText = "Create..."
    Const IncreaseWidthText = "Increase Width"
    Const IncreaseHeightText = "Increase Height"
    Const DecreaseWidthText = "Decrease Width"
    Const DecreaseHeightText = "Decrease Height"
    Const PickTerrainText = "Pick Terrain..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
                   {
                        CreateText,
                        IncreaseWidthText,
                        IncreaseHeightText,
                        DecreaseWidthText,
                        DecreaseHeightText,
                        PickTerrainText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case IncreaseWidthText
                        MapWidth += 1
                    Case IncreaseHeightText
                        MapHeight += 1
                    Case DecreaseWidthText
                        MapWidth = Math.Max(MapWidth - 1, 1)
                    Case DecreaseHeightText
                        MapHeight = Math.Max(MapHeight - 1, 1)
                    Case CreateText
                        World.Maps.Create(MapName, MapWidth, MapHeight, TerrainName)
                        setState(GameState.EditMap, False)
                    Case PickTerrainText
                        setState(GameState.PickDefaultMapTerrain, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.MapsMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, font.Height * 8), $"Width: {MapWidth}", White)
        font.WriteText(displayBuffer, (Zero, font.Height * 9), $"Height: {MapHeight}", White)
        font.WriteText(displayBuffer, (Zero, font.Height * 10), $"Terrain: {TerrainName}", White)
    End Sub
End Class
