Friend Class EditFontState
    Inherits BaseGlyphPickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Protected Overrides Sub HandleDone(glyph As Char)
        GlyphKey = glyph
        SetState(GameState.EditGlyph)
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.FontsMenu)
    End Sub

    Protected Overrides Function FontNameSource() As String
        Return FontName
    End Function

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Font Name: {FontName}", Hue.White)
    End Sub
End Class
