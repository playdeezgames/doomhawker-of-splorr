Friend Class LoadFromState
    Inherits BaseInputState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Filename:", Sub() setState(GameState.MainMenu), Sub(buffer)
                                                                                          Editor.Load(buffer)
                                                                                          setState(GameState.MainMenu)
                                                                                      End Sub)
    End Sub
End Class
