Friend Class FontsMenuState
    Inherits BaseMenuState
    Const GoBackText = "Go Back"
    Const NewFontText = "New Font..."
    Const EditFontText = "Edit Font..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {GoBackText, NewFontText, EditFontText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case GoBackText
                SetState(GameState.EditMenu)
            Case NewFontText
                SetState(GameState.NewFontSize)
            Case EditFontText
                If Editor.FontNames.Any Then
                    SetState(GameState.PickFont)
                Else
                    SetState(GameState.NewFontSize)
                End If
        End Select
    End Sub
End Class
