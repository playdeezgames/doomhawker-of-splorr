Friend Class FontsMenuState
    Inherits BaseMenuState
    Const NewFontText = "New Font..."
    Const PickFontText = "Pick Font..."
    Const RenameFontText = "Rename Font..."
    Const DeleteFontText = "Delete Font..."
    Const CloneFontText = "Clone Font..."
    Const ExportFontText = "Export Font..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                   NewFontText,
                   PickFontText,
                   ExportFontText,
                   RenameFontText,
                   CloneFontText,
                   DeleteFontText})
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewFontText
                SetState(GameState.NewFontSize)
            Case PickFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickFont)
                Else
                    SetState(GameState.NewFontSize)
                End If
            Case RenameFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickRenameFont)
                End If
            Case CloneFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickCloneFont)
                End If
            Case DeleteFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickDeleteFont)
                End If
            Case ExportFontText
                If Editor.Fonts.HasAny Then
                    SetState(GameState.PickExportFont)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Font Count: {Editor.Fonts.Names.Count}", Hue.White)
    End Sub
End Class
