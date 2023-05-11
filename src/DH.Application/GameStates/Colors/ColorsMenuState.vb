Friend Class ColorsMenuState
    Inherits BaseMenuState
    Const NewColorText = "New Color..."
    Const PickColorText = "Pick Color..."
    Const RenameColorText = "Rename Color..."
    Const DeleteColorText = "Delete Color..."
    Const CloneColorText = "Clone Color..."
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Colors Menu:",
            New List(Of String) From {
                   NewColorText,
                   PickColorText,
                   RenameColorText,
                   CloneColorText,
                   DeleteColorText},
            Sub(menuItem)
                Select Case menuItem
                    Case NewColorText
                        setState(GameState.NewColorName, False)
                    Case PickColorText
                        If Editor.Colors.HasAny Then
                            setState(GameState.PickColor, False)
                        Else
                            setState(GameState.NewColorName, False)
                        End If
                    Case RenameColorText
                        If Editor.Colors.HasAny Then
                            setState(GameState.PickRenameColor, False)
                        End If
                    Case CloneColorText
                        If Editor.Colors.HasAny Then
                            setState(GameState.PickCloneColor, False)
                        End If
                    Case DeleteColorText
                        If Editor.Colors.HasAny Then
                            setState(GameState.PickDeleteColor, False)
                        End If
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Color Count: {Editor.Colors.Names.Count}", White)
    End Sub
End Class
