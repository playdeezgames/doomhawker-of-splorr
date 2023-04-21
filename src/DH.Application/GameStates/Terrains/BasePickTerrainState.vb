Friend Class BasePickTerrainState
    Inherits BasePickState
    Private ReadOnly _cancelState As GameState
    Private ReadOnly _nextState As GameState
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
        _nextState = nextState
        _cancelState = cancelState
    End Sub
    Protected Overrides Sub HandlePick(picked As String)
        TerrainName = picked
        SetState(_nextState)
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(_cancelState)
    End Sub
    Protected Overrides Function ListItemSource() As IEnumerable(Of String)
        Return Editor.TerrainNames
    End Function
End Class
