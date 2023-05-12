Friend Class TerrainsMenuState
    Inherits BaseMenuState
    Const NewTerrainText = "New Terrain..."
    Const EditTerrainText = "Pick Terrain..."
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {NewTerrainText, EditTerrainText},
            Sub(menuItem)
                Select Case menuItem
                    Case NewTerrainText
                        setState(GameState.NewTerrainName, False)
                    Case EditTerrainText
                        If World.Terrains.HasAny Then
                            setState(GameState.PickTerrain, False)
                        Else
                            setState(GameState.NewTerrainName, False)
                        End If
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Terrain Count: {World.Terrains.Names.Count}", White)
    End Sub
End Class
