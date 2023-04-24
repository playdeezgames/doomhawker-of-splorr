Friend Class EditItemState
    Inherits BaseMenuState
    Const ChangeFontText = "Change Font..."
    Const ChangeGlyphText = "Change Glyph..."
    Const ChangeHueText = "Change Hue..."
    Const RenameItemText = "Rename Item..."
    Const CloneItemText = "Clone Item..."
    Const DeleteItemText = "Delete Item..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            New List(Of String) From
            {
                RenameItemText,
                ChangeFontText,
                ChangeGlyphText,
                ChangeHueText,
                CloneItemText,
                DeleteItemText
            })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Dim item As IItem = Editor.Items.Retrieve(ItemName)
        Select Case menuItem
            Case ChangeHueText
                item.HueIndex = (item.HueIndex + 1) Mod AllHues.Count
            Case ChangeGlyphText
                If item.Font IsNot Nothing Then
                    SetState(GameState.PickItemGlyph)
                End If
            Case ChangeFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickItemFont)
                End If
            Case RenameItemText
                SetState(GameState.RenameItem)
            Case CloneItemText
                SetState(GameState.CloneItem)
            Case DeleteItemText
                SetState(GameState.ConfirmDeleteItem)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim item As IItem = ShowStatistics(displayBuffer)
        If item.Font IsNot Nothing Then
            Dim terrainFont = item.Font.Font
            Dim width = terrainFont.TextWidth($"{item.GlyphKey}")
            Dim height = terrainFont.Height
            terrainFont.WriteText(displayBuffer, (ViewWidth - width, ViewHeight - height), $"{item.GlyphKey}", AllHues(item.HueIndex))
        End If
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.ItemsMenu)
    End Sub

    Private Shared Function ShowStatistics(displayBuffer As IPixelSink(Of Hue)) As IItem
        Dim font = Fonts(GameFont.Font5x7)
        Dim item As IItem = Editor.Items.Retrieve(ItemName)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Name: {ItemName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Font: {item.Font?.FontName}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 10), $"Glyph: {AscW(item.GlyphKey)}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 11), $"Hue: {AllHues(item.HueIndex)}", Hue.White)
        Return item
    End Function
End Class
