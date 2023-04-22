Friend Class EditMapState
    Inherits BaseMenuState
    Const PlaceTerrainText = "Place Terrain..."
    Const RenameMapText = "Rename Map..."
    Const CloneMapText = "Clone Map..."
    Const DeleteMapText = "Delete Map..."

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                    PlaceTerrainText,
                    RenameMapText,
                    CloneMapText,
                    DeleteMapText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case PlaceTerrainText
                SetState(GameState.PickMapTerrain)
            Case RenameMapText
                SetState(GameState.RenameMap)
            Case CloneMapText
                SetState(GameState.CloneMap)
            Case DeleteMapText
                SetState(GameState.ConfirmDeleteMap)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MapsMenu)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Map Name: {MapName}", Hue.White)
    End Sub
End Class
