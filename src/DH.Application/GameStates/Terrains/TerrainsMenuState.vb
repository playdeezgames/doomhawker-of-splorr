Friend Class TerrainsMenuState
    Inherits BaseMenuState
    Const GoBackText = "Go Back"
    Const NewTerrainText = "New Terrain..."
    Const EditTerrainText = "Edit Terrain..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {GoBackText, NewTerrainText, EditTerrainText})
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case GoBackText
                SetState(GameState.MainMenu)
            Case NewTerrainText
                SetState(GameState.NewTerrainName)
        End Select
    End Sub
End Class
