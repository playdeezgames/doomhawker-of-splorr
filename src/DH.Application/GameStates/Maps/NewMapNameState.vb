Friend Class NewMapNameState
    Inherits BaseInputState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Map Name:", Sub() setState(GameState.MapsMenu), Sub(buffer)
                                                                                          MapName = buffer
                                                                                          setState(GameState.NewMapSize)
                                                                                      End Sub)
    End Sub
End Class
