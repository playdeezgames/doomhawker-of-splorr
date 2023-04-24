Friend Class ItemsMenuState
    Inherits BaseMenuState
    Const NewItemText = "New Item..."
    Const PickItemText = "Pick Item..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                    NewItemText,
                    PickItemText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewItemText
                SetState(GameState.NewItemName)
            Case PickItemText
                If Editor.Items.HasAny Then
                    SetState(GameState.PickItem)
                Else
                    SetState(GameState.NewItemName)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Item Count: {Editor.ItemNames.Count}", Hue.White)
    End Sub
End Class
