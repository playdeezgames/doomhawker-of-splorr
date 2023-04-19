Friend Class MapsMenuState
    Inherits BaseMenuState
    Const NewMapText = "New Map..."
    Const EditMapText = "Edit Map..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {
                    NewMapText,
                    EditMapText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewMapText
                SetState(GameState.NewMapName)
            Case EditMapText
                If Editor.HasMaps Then
                    SetState(GameState.PickMap)
                Else
                    SetState(GameState.NewMapName)
                End If
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.MainMenu)
    End Sub
End Class
