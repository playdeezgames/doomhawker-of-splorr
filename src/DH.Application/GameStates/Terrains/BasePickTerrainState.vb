Friend Class BasePickTerrainState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState), nextState As GameState, cancelState As GameState)
        MyBase.New(
            parent,
            setState,
            "Choose Terrain",
            Sub() setState(cancelState),
            Sub(picked)
                TerrainName = picked
                setState(nextState)
            End Sub,
            Function() Editor.TerrainNames)
    End Sub
End Class
