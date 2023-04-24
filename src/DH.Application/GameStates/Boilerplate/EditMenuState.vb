Friend Class EditMenuState
    Inherits BaseMenuState
    Const EditFontsText = "Fonts..."
    Const EditTerrainsText = "Terrains..."
    Const EditItemsText = "Items..."
    Const EditCreaturesText = "Creatures..."
    Const EditMapsText = "Maps..."
    Const SettingsText = "Settings..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                   EditFontsText,
                   EditTerrainsText,
                   EditItemsText,
                   EditCreaturesText,
                   EditMapsText,
                   SettingsText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case EditFontsText
                SetState(GameState.FontsMenu)
            Case EditTerrainsText
                SetState(GameState.TerrainsMenu)
            Case EditMapsText
                SetState(GameState.MapsMenu)
            Case SettingsText
                SetState(GameState.Settings)
            Case EditItemsText
                SetState(GameState.ItemsMenu)
            Case EditCreaturesText
                SetState(GameState.CreaturesMenu)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub
End Class
