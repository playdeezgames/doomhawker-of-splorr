﻿Friend Class EditTerrainState
    Inherits BaseMenuState
    Const ChangeFontText = "Change Font..."
    Const ChangeGlyphText = "Change Glyph..."
    Const ChangeHueText = "Change Hue..."
    Const ToggleTenantabilityText = "Toggle Tenantability"
    Const RenameTerrainText = "Rename Terrain..."
    Const CloneTerrainText = "Clone Terrain..."
    Const DeleteTerrainText = "Delete Terrain..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                RenameTerrainText,
                ChangeFontText,
                ChangeGlyphText,
                ChangeHueText,
                ToggleTenantabilityText,
                CloneTerrainText,
                DeleteTerrainText
            },
            Sub(menuItem)
                Dim terrain As ITerrain = Editor.Terrains.Retrieve(TerrainName)
                Select Case menuItem
                    Case ToggleTenantabilityText
                        terrain.Tenantability = Not terrain.Tenantability
                    Case ChangeHueText
                        terrain.HueIndex = (terrain.HueIndex + 1) Mod AllHues.Count
                    Case ChangeGlyphText
                        If terrain.Font IsNot Nothing Then
                            setState(GameState.PickTerrainGlyph, False)
                        End If
                    Case ChangeFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickTerrainFont, False)
                        End If
                    Case RenameTerrainText
                        setState(GameState.RenameTerrain, False)
                    Case CloneTerrainText
                        setState(GameState.CloneTerrain, False)
                    Case DeleteTerrainText
                        setState(GameState.ConfirmDeleteTerrain, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.TerrainsMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim terrain As ITerrain = ShowStatistics(displayBuffer)
        If terrain.Font IsNot Nothing Then
            Dim terrainFont = terrain.Font.Font
            Dim width = terrainFont.TextWidth($"{terrain.GlyphKey}")
            Dim height = terrainFont.Height
            terrainFont.WriteText(displayBuffer, (ViewWidth - width, ViewHeight - height), $"{terrain.GlyphKey}", CType(terrain.HueIndex, Hue))
        End If
    End Sub
    Private Shared Function ShowStatistics(displayBuffer As IPixelSink(Of Hue)) As ITerrain
        Dim font = Fonts(GameFont.Font5x7)
        Dim terrain As ITerrain = Editor.Terrains.Retrieve(TerrainName)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Name: {TerrainName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Font: {terrain.Font?.FontName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 10), $"Glyph: {AscW(terrain.GlyphKey)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 11), $"Hue: {AllHues(terrain.HueIndex)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 12), $"Tenantable: {terrain.Tenantability}", Hue.White)
        Return terrain
    End Function
End Class
