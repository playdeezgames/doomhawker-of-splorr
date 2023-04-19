Friend Class EditMenuState
    Inherits BaseMenuState
    Const EditFontsText = "Edit Fonts..."
    Const EditTerrainTypesText = "Edit Terrains..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {EditFontsText, EditTerrainTypesText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case EditFontsText
                SetState(GameState.FontsMenu)
            Case EditTerrainTypesText
                SetState(GameState.TerrainsMenu)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub
End Class
