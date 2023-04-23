Friend Class EditFontState
    Inherits BaseGlyphPickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            Sub(glyph)
                GlyphKey = glyph
                setState(GameState.EditGlyph, False)
            End Sub,
            Sub()
                setState(GameState.FontsMenu, False)
            End Sub,
            Function() FontName)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Font Name: {FontName}", Hue.White)
    End Sub
End Class
