Friend Class NewMapNameState
    Inherits BaseInputState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Map Name:")
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MapsMenu)
    End Sub

    Protected Overrides Sub HandleDone(buffer As String)
        MapName = buffer
        SetState(GameState.NewMapSize)
    End Sub
End Class
