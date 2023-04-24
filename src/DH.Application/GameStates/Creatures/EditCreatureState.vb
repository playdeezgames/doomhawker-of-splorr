Friend Class EditCreatureState
    Inherits BaseMenuState
    Const ChangeFontText = "Change Font..."
    Const ChangeGlyphText = "Change Glyph..."
    Const ChangeHueText = "Change Hue..."
    Const RenameCreatureText = "Rename Creature..."
    Const CloneCreatureText = "Clone Creature..."
    Const DeleteCreatureText = "Delete Creature..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            New List(Of String) From
            {
                RenameCreatureText,
                ChangeFontText,
                ChangeGlyphText,
                ChangeHueText,
                CloneCreatureText,
                DeleteCreatureText
            })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Dim creature As ICreature = Editor.Creatures.Retrieve(CreatureName)
        Select Case menuItem
            Case ChangeHueText
                creature.HueIndex = (creature.HueIndex + 1) Mod AllHues.Count
            Case ChangeGlyphText
                If creature.Font IsNot Nothing Then
                    SetState(GameState.PickCreatureGlyph)
                End If
            Case ChangeFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickCreatureFont)
                End If
            Case RenameCreatureText
                SetState(GameState.RenameCreature)
            Case CloneCreatureText
                SetState(GameState.CloneCreature)
            Case DeleteCreatureText
                SetState(GameState.ConfirmDeleteCreature)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim creature As ICreature = ShowStatistics(displayBuffer)
        If creature.Font IsNot Nothing Then
            Dim terrainFont = creature.Font.Font
            Dim width = terrainFont.TextWidth($"{creature.GlyphKey}")
            Dim height = terrainFont.Height
            terrainFont.WriteText(displayBuffer, (ViewWidth - width, ViewHeight - height), $"{creature.GlyphKey}", AllHues(creature.HueIndex))
        End If
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.CreaturesMenu)
    End Sub

    Private Shared Function ShowStatistics(displayBuffer As IPixelSink(Of Hue)) As ICreature
        Dim font = Fonts(GameFont.Font5x7)
        Dim creature As ICreature = Editor.Creatures.Retrieve(CreatureName)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Name: {CreatureName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Font: {creature.Font?.FontName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 10), $"Glyph: {AscW(creature.GlyphKey)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 11), $"Hue: {AllHues(creature.HueIndex)}", Hue.White)
        Return creature
    End Function
End Class
