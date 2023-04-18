Friend Class EditTerrainState
    Inherits BaseMenuState
    Const GoBackText = "Go Back"
    Const ChangeFontText = "Change Font..."
    Const ChangeGlyphText = "Change Glyph..."
    Const ChangeHueText = "Change Hue..."
    Const ToggleTenantabilityText = "Toggle Tenantability"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {GoBackText, ChangeFontText, ChangeGlyphText, ChangeHueText, ToggleTenantabilityText})
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case GoBackText
                SetState(GameState.TerrainsMenu)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim terrain As ITerrain = Editor.GetTerrain(TerrainName)
        font.WriteText(displayBuffer, (0, font.Height * 6), $"Name: {TerrainName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 7), $"Font: {terrain.FontName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Glyph: {AscW(terrain.GlyphKey)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Hue: {CType(terrain.HueIndex, Hue)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 10), $"Tenantable: {terrain.Tenantability}", Hue.White)
    End Sub
End Class
