Friend Class EditMapState
    Inherits BaseMenuState
    Const PlaceTerrainText = "Place Terrain..."
    Const PlaceItemText = "Place Items..."
    Const RemoveItemsText = "Remove Items..."
    Const PlaceCreaturesText = "Place Creatures..."
    Const RemoveCreaturesText = "Remove Creatures..."
    Const EditTriggersText = "Triggers..."
    Const PlaceTriggersText = "Place Triggers..."
    Const RemoveTriggersText = "Remove Triggers..."
    Const RenameMapText = "Rename Map..."
    Const CloneMapText = "Clone Map..."
    Const DeleteMapText = "Delete Map..."

    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                    PlaceTerrainText,
                    PlaceItemText,
                    RemoveItemsText,
                    PlaceCreaturesText,
                    RemoveCreaturesText,
                    EditTriggersText,
                    PlaceTriggersText,
                    RemoveTriggersText,
                    RenameMapText,
                    CloneMapText,
                    DeleteMapText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case PlaceTerrainText
                        setState(GameState.PickMapTerrain, False)
                    Case PlaceItemText
                        setState(GameState.PickMapItem, False)
                    Case PlaceCreaturesText
                        setState(GameState.PickMapCreature, False)
                    Case RemoveItemsText
                        setState(GameState.RemoveMapItem, False)
                    Case RemoveCreaturesText
                        setState(GameState.RemoveMapCreature, False)
                    Case RenameMapText
                        setState(GameState.RenameMap, False)
                    Case CloneMapText
                        setState(GameState.CloneMap, False)
                    Case DeleteMapText
                        setState(GameState.ConfirmDeleteMap, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.MapsMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Map Name: {MapName}", White)
    End Sub
End Class
