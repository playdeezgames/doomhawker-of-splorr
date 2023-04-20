Friend Class NewFontNameState
    Inherits BaseInputState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, "Font Name:", Sub()
                                                       FontName = ""
                                                       setState(GameState.FontsMenu)
                                                   End Sub,
                                                    Sub(buffer)
                                                        FontName = buffer
                                                        Editor.CreateFont(FontName, FontWidth, FontHeight)
                                                        setState(GameState.EditFont)
                                                    End Sub)
    End Sub
End Class
