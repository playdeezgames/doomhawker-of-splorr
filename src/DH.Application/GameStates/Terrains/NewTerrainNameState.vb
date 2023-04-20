Friend Class NewTerrainNameState
    Inherits BaseInputState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Terrain Name:", Sub()
                                                          TerrainName = ""
                                                          setState(GameState.TerrainsMenu)
                                                      End Sub,
                                                      Sub(buffer)
                                                          TerrainName = buffer
                                                          Editor.CreateTerrain(TerrainName)
                                                          setState(GameState.EditTerrain)
                                                      End Sub)
    End Sub
End Class
