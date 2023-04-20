Friend Class EditMapState
    Inherits BaseMenuState
    Const PlaceTerrainText = "Place Terrain..."

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {
                    PlaceTerrainText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case PlaceTerrainText
                SetState(GameState.PickMapTerrain)
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MapsMenu)
    End Sub
End Class
