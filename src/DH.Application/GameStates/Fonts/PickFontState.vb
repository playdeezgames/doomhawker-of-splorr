Friend Class PickFontState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(
            parent,
            setState,
            "Choose Font",
            Sub() setState(GameState.FontsMenu),
            Sub(picked)
                FontName = picked
                setState(GameState.EditFont)
            End Sub,
            Function() Editor.FontNames)
    End Sub
End Class
