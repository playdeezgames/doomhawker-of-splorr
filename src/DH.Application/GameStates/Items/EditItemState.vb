﻿Friend Class EditItemState
    Inherits BaseMenuState
    Const ChangeFontText = "Change Font..."
    Const ChangeGlyphText = "Change Glyph..."
    Const ChangeHueText = "Change Hue..."
    Const RenameItemText = "Rename Item..."
    Const CloneItemText = "Clone Item..."
    Const DeleteItemText = "Delete Item..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                RenameItemText,
                ChangeFontText,
                ChangeGlyphText,
                ChangeHueText,
                CloneItemText,
                DeleteItemText
            },
            Sub(menuItem)
                Dim item As IItem = World.Items.Retrieve(ItemName)
                Select Case menuItem
                    Case ChangeHueText
                        HueChangeAction = Sub(hue)
                                              item.Hue = hue
                                          End Sub
                        setState(GameState.ChangeHue, True)
                    Case ChangeGlyphText
                        If item.Font IsNot Nothing Then
                            setState(GameState.PickItemGlyph, False)
                        End If
                    Case ChangeFontText
                        If World.Fonts.HasAny Then
                            setState(GameState.PickItemFont, False)
                        End If
                    Case RenameItemText
                        setState(GameState.RenameItem, False)
                    Case CloneItemText
                        setState(GameState.CloneItem, False)
                    Case DeleteItemText
                        setState(GameState.ConfirmDeleteItem, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.ItemsMenu, False)
            End Sub)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim item As IItem = ShowStatistics(displayBuffer)
        If item.Font IsNot Nothing Then
            Dim terrainFont = item.Font.Font
            Dim width = terrainFont.TextWidth($"{item.GlyphKey}")
            Dim height = terrainFont.Height
            terrainFont.WriteText(displayBuffer, (ViewWidth - width, ViewHeight - height), $"{item.GlyphKey}", item.Hue)
        End If
    End Sub
    Private Shared Function ShowStatistics(displayBuffer As IPixelSink(Of Integer)) As IItem
        Dim font = Fonts(GameFont.Font5x7)
        Dim item As IItem = World.Items.Retrieve(ItemName)
        font.WriteText(displayBuffer, (Zero, font.Height * 8), $"Name: {ItemName}", White)
        font.WriteText(displayBuffer, (Zero, font.Height * 9), $"Font: {item.Font?.FontName}", White)
        font.WriteText(displayBuffer, (Zero, font.Height * 10), $"Glyph: {AscW(item.GlyphKey)}", White)
        font.WriteText(displayBuffer, (Zero, font.Height * 11), $"Hue: {item.Hue}", White)
        Return item
    End Function
End Class
