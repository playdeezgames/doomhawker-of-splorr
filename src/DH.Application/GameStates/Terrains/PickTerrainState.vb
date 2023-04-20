﻿Friend Class PickTerrainState
    Inherits BasePickTerrainState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, GameState.EditTerrain, GameState.TerrainsMenu)
    End Sub
End Class
