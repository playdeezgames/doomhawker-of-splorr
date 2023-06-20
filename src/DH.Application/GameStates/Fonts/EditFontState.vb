Friend Class EditFontState
    Inherits BaseGlyphPickState
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
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
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Font Name: {FontName}", White)
    End Sub
End Class
