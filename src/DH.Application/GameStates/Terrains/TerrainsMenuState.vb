Friend Class TerrainsMenuState
    Inherits BaseMenuState
    Const NewTerrainText = "New Terrain..."
    Const EditTerrainText = "Pick Terrain..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {NewTerrainText, EditTerrainText})
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewTerrainText
                SetState(GameState.NewTerrainName)
            Case EditTerrainText
                If Editor.Terrains.HasAny Then
                    SetState(GameState.PickTerrain)
                Else
                    SetState(GameState.NewTerrainName)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Terrain Count: {Editor.Terrains.Names.Count}", Hue.White)
    End Sub
End Class
