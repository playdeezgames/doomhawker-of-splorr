Friend Class NewFontNameState
    Inherits BaseInputState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Font Name:")
    End Sub
    Protected Overrides Sub HandleCancel()
        FontName = ""
        SetState(GameState.FontsMenu)
    End Sub
    Protected Overrides Sub HandleDone(buffer As String)
        FontName = buffer
        Editor.CreateFont(FontName, FontWidth, FontHeight)
        SetState(GameState.EditFont)
    End Sub
End Class
