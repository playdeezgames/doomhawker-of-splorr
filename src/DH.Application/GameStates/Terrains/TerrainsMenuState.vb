Friend Class TerrainsMenuState
    Inherits BaseMenuState
    Const NewTerrainText = "New Terrain..."
    Const EditTerrainText = "Edit Terrain..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {NewTerrainText, EditTerrainText})
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewTerrainText
                SetState(GameState.NewTerrainName)
            Case EditTerrainText
                If Editor.HasTerrains Then
                    SetState(GameState.PickTerrain)
                Else
                    SetState(GameState.NewTerrainName)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub
End Class
