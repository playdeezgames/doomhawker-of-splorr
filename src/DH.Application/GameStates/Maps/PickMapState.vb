Friend Class PickMapState
    Inherits BasePickState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(
            parent,
            setState,
            "Choose Map",
            Sub() setState(GameState.MapsMenu),
            Sub(picked)
                MapName = picked
                setState(GameState.EditMap)
            End Sub,
            Function() Editor.MapNames)
    End Sub
End Class
