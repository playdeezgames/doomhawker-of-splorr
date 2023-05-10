Friend Class EditCreatureState
    Inherits BaseMenuState
    Const ChangeFontText = "Change Font..."
    Const ChangeGlyphText = "Change Glyph..."
    Const ChangeHueText = "Change Hue..."
    Const RenameCreatureText = "Rename Creature..."
    Const CloneCreatureText = "Clone Creature..."
    Const DeleteCreatureText = "Delete Creature..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                RenameCreatureText,
                ChangeFontText,
                ChangeGlyphText,
                ChangeHueText,
                CloneCreatureText,
                DeleteCreatureText
            },
            Sub(menuItem)
                Dim creature As ICreature = Editor.Creatures.Retrieve(CreatureName)
                Select Case menuItem
                    Case ChangeHueText
                        creature.HueIndex = (creature.HueIndex + 1) Mod AllHues.Count
                    Case ChangeGlyphText
                        If creature.Font IsNot Nothing Then
                            setState(GameState.PickCreatureGlyph, False)
                        End If
                    Case ChangeFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickCreatureFont, False)
                        End If
                    Case RenameCreatureText
                        setState(GameState.RenameCreature, False)
                    Case CloneCreatureText
                        setState(GameState.CloneCreature, False)
                    Case DeleteCreatureText
                        setState(GameState.ConfirmDeleteCreature, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.CreaturesMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim creature As ICreature = ShowStatistics(displayBuffer)
        If creature.Font IsNot Nothing Then
            Dim terrainFont = creature.Font.Font
            Dim width = terrainFont.TextWidth($"{creature.GlyphKey}")
            Dim height = terrainFont.Height
            terrainFont.WriteText(displayBuffer, (ViewWidth - width, ViewHeight - height), $"{creature.GlyphKey}", AllHues(creature.HueIndex))
        End If
    End Sub
    Private Shared Function ShowStatistics(displayBuffer As IPixelSink(Of Integer)) As ICreature
        Dim font = Fonts(GameFont.Font5x7)
        Dim creature As ICreature = Editor.Creatures.Retrieve(CreatureName)
        font.WriteText(displayBuffer, (Zero, font.Height * 8), $"Name: {CreatureName}", 15)
        font.WriteText(displayBuffer, (Zero, font.Height * 9), $"Font: {creature.Font?.FontName}", 15)
        font.WriteText(displayBuffer, (Zero, font.Height * 10), $"Glyph: {AscW(creature.GlyphKey)}", 15)
        font.WriteText(displayBuffer, (Zero, font.Height * 11), $"Hue: {AllHues(creature.HueIndex)}", 15)
        Return creature
    End Function
End Class
