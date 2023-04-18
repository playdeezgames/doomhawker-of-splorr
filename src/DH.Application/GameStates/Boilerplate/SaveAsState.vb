Friend Class SaveAsState
    Inherits BaseInputState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Filename:")
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub

    Protected Overrides Sub HandleDone(buffer As String)
        Editor.Save(buffer)
        SetState(GameState.MainMenu)
    End Sub
End Class
