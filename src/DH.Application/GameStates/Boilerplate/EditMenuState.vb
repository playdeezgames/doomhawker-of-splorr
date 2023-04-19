Friend Class EditMenuState
    Inherits BaseMenuState
    Const EditFontsText = "Edit Fonts..."
    Const EditTerrainsText = "Edit Terrains..."
    Const EditMapsText = "Edit Maps..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {EditFontsText, EditTerrainsText, EditMapsText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case EditFontsText
                SetState(GameState.FontsMenu)
            Case EditTerrainsText
                SetState(GameState.TerrainsMenu)
            Case EditMapsText
                SetState(GameState.MapsMenu)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub
End Class
