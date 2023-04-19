Friend Class FontsMenuState
    Inherits BaseMenuState
    Const NewFontText = "New Font..."
    Const EditFontText = "Edit Font..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {NewFontText, EditFontText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewFontText
                SetState(GameState.NewFontSize)
            Case EditFontText
                If Editor.HasFonts Then
                    SetState(GameState.PickFont)
                Else
                    SetState(GameState.NewFontSize)
                End If
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub
End Class
