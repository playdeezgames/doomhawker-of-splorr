Friend Class PickTerrainGlyphState
    Inherits BaseGlyphPickState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Protected Overrides Sub HandleDone(glyph As Char)
        Editor.GetTerrain(TerrainName).GlyphKey = glyph
        SetState(GameState.EditTerrain)
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditTerrain)
    End Sub

    Protected Overrides Function FontNameSource() As String
        Return Editor.GetTerrain(TerrainName).FontName
    End Function
End Class
