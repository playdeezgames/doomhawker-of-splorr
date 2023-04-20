Friend Class PickMapTerrainState
    Inherits BasePickTerrainState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, GameState.PlaceMapTerrain, GameState.EditMap)
    End Sub
End Class
