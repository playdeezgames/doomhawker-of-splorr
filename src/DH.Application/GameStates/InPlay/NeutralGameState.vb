Friend Class NeutralGameState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
    End Sub
    Public Overrides Sub OnStart()
        MyBase.OnStart()
        SetState(GameState.Navigate)
    End Sub
End Class
