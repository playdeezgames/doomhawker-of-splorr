Friend Class NewTerrainNameState
    Inherits BaseInputState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Terrain Name:")
    End Sub

    Protected Overrides Sub HandleCancel()
        TerrainName = ""
        SetState(GameState.TerrainsMenu)
    End Sub

    Protected Overrides Sub HandleDone(buffer As String)
        TerrainName = buffer
        Editor.CreateTerrain(TerrainName)
        SetState(GameState.EditTerrain)
    End Sub
End Class
