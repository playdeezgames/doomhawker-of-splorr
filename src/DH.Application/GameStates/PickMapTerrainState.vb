Friend Class PickMapTerrainState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub
    Protected Overrides Sub HandlePick(picked As String)
        TerrainName = picked
        SetState(GameState.PlaceMapTerrain)
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMap)
    End Sub
    Protected Overrides Function ListItemSource() As IEnumerable(Of String)
        Return Editor.TerrainNames
    End Function
End Class
