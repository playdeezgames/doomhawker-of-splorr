Friend Class PickFontState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub
    Protected Overrides Function ListItemSource() As IEnumerable(Of String)
        Return Editor.FontNames
    End Function
    Protected Overrides Sub HandlePick(picked As String)
        FontName = picked
        SetState(GameState.EditFont)
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.FontsMenu)
    End Sub
End Class
