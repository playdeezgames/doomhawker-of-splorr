Friend Class PickTerrainFontState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(
            parent,
            setState,
            "Choose Font",
            Sub() setState(GameState.EditTerrain),
            Sub(picked)
                Editor.GetTerrain(TerrainName).Font = Editor.GetFont(picked)
                setState(GameState.EditTerrain)
            End Sub,
            Function() Editor.FontNames)
    End Sub
End Class
