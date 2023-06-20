Friend Class EditMenuState
    Inherits BaseMenuState
    Const EditFontsText = "Fonts..."
    Const EditTerrainsText = "Terrains..."
    Const EditItemsText = "Items..."
    Const EditCreaturesText = "Creatures..."
    Const EditMapsText = "Maps..."
    Const AvatarText = "Avatar..."
    Const SettingsText = "Settings..."
    Const EditColorsText = "Colors..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Edit Menu:",
            New List(Of String) From {
                   EditColorsText,
                   EditFontsText,
                   EditTerrainsText,
                   EditItemsText,
                   EditCreaturesText,
                   EditMapsText,
                   AvatarText,
                   SettingsText},
            Sub(menuItem)
                Select Case menuItem
                    Case EditColorsText
                        setState(GameState.ColorsMenu, False)
                    Case EditFontsText
                        setState(GameState.FontsMenu, False)
                    Case EditTerrainsText
                        setState(GameState.TerrainsMenu, False)
                    Case EditMapsText
                        setState(GameState.MapsMenu, False)
                    Case SettingsText
                        setState(GameState.Settings, False)
                    Case EditItemsText
                        setState(GameState.ItemsMenu, False)
                    Case EditCreaturesText
                        setState(GameState.CreaturesMenu, False)
                    Case AvatarText
                        setState(GameState.AvatarMenu, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.MainMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Const text = "(B)/Esc to go back"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, ViewHeight - font.Height), text, Gray)
    End Sub
End Class
