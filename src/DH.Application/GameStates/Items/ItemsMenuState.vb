Friend Class ItemsMenuState
    Inherits BaseMenuState
    Const NewItemText = "New Item..."
    Const PickItemText = "Pick Item..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                    NewItemText,
                    PickItemText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case NewItemText
                        setState(GameState.NewItemName, False)
                    Case PickItemText
                        If Editor.Items.HasAny Then
                            setState(GameState.PickItem, False)
                        Else
                            setState(GameState.NewItemName, False)
                        End If
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Item Count: {Editor.Items.Names.Count}", Hue.White)
    End Sub
End Class
