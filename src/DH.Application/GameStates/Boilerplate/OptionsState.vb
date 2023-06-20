Friend Class OptionsState
    Inherits BaseMenuState
    Const WindowSizeText = "Window Size..."
    Const FullScreenText = "Toggle Full Screen"
    Const VolumeText = "Volume..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Options:",
            New List(Of String) From {
                WindowSizeText,
                FullScreenText,
                VolumeText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case FullScreenText
                        parent.FullScreen = Not parent.FullScreen
                End Select
            End Sub,
            Sub()
                setState(GameState.MainMenu, False)
            End Sub)
    End Sub
End Class
