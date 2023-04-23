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
            New List(Of String) From
            {
                RenameTerrainText,
                ChangeFontText,
                ChangeGlyphText,
                ChangeHueText,
                ToggleTenantabilityText,
                CloneTerrainText,
                DeleteTerrainText
            })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Dim terrain As ITerrain = Editor.GetTerrain(TerrainName)
        Select Case menuItem
            Case ToggleTenantabilityText
                terrain.Tenantability = Not terrain.Tenantability
            Case ChangeHueText
                terrain.HueIndex = (terrain.HueIndex + 1) Mod AllHues.Count
            Case ChangeGlyphText
                If terrain.Font IsNot Nothing Then
                    SetState(GameState.PickTerrainGlyph)
                End If
            Case ChangeFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickTerrainFont)
                End If
            Case RenameTerrainText
                SetState(GameState.RenameTerrain)
            Case CloneTerrainText
                SetState(GameState.CloneTerrain)
            Case DeleteTerrainText
                SetState(GameState.ConfirmDeleteTerrain)
        End Select
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

    Protected Overrides Sub HandleCancel()
        SetState(GameState.TerrainsMenu)
    End Sub

    Private Shared Function ShowStatistics(displayBuffer As IPixelSink(Of Hue)) As ITerrain
        Dim font = Fonts(GameFont.Font5x7)
        Dim terrain As ITerrain = Editor.GetTerrain(TerrainName)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Name: {TerrainName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Font: {terrain.Font?.FontName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 10), $"Glyph: {AscW(terrain.GlyphKey)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 11), $"Hue: {AllHues(terrain.HueIndex)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 12), $"Tenantable: {terrain.Tenantability}", Hue.White)
        Return terrain
    End Function
End Class
