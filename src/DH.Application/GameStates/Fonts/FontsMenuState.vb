Friend Class FontsMenuState
    Inherits BaseMenuState
    Const NewFontText = "New Font..."
    Const PickFontText = "Pick Font..."
    Const RenameFontText = "Rename Font..."
    Const DeleteFontText = "Delete Font..."
    Const CloneFontText = "Clone Font..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {
                   NewFontText,
                   PickFontText,
                   RenameFontText,
                   CloneFontText,
                   DeleteFontText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewFontText
                SetState(GameState.NewFontSize)
            Case PickFontText
                If Editor.HasFonts Then
                    SetState(GameState.PickFont)
                Else
                    SetState(GameState.NewFontSize)
                End If
            Case RenameFontText
                If Editor.HasFonts Then
                    SetState(GameState.PickRenameFont)
                End If
            Case CloneFontText
                If Editor.HasFonts Then
                    SetState(GameState.PickCloneFont)
                End If
            Case DeleteFontText
                If Editor.HasFonts Then
                    SetState(GameState.PickDeleteFont)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Font Count: {Editor.FontNames.Count}", Hue.White)
    End Sub
End Class
