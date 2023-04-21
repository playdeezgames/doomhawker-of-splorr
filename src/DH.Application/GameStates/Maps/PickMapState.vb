﻿Friend Class PickMapState
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

    Protected Overrides Sub HandlePick(picked As String)
        MapName = picked
        SetState(GameState.EditMap)
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MapsMenu)
    End Sub

    Protected Overrides Function ListItemSource() As IEnumerable(Of String)
        Return Editor.MapNames
    End Function
End Class
