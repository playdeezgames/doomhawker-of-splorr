Friend Class EditMenuState
    Inherits BaseMenuState
    Const ExitEditorText = "Exit Editor"
    Const EditFontsText = "Edit Fonts..."
    Const EditTerrainTypesText = "Edit Terrains..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {ExitEditorText, EditFontsText, EditTerrainTypesText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case ExitEditorText
                SetState(GameState.MainMenu)
            Case EditFontsText
                SetState(GameState.FontsMenu)
            Case EditTerrainTypesText
                SetState(GameState.TerrainsMenu)
        End Select
    End Sub
End Class
