Friend Class EditFontState
    Inherits BaseGlyphPickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
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
End Class
