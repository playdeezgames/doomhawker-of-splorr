Friend Class FontsMenuState
    Inherits BaseMenuState
    Const NewFontText = "New Font..."
    Const PickFontText = "Pick Font..."
    Const RenameFontText = "Rename Font..."
    Const DeleteFontText = "Delete Font..."
    Const CloneFontText = "Clone Font..."
    Const ExportFontText = "Export Font..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                   NewFontText,
                   PickFontText,
                   ExportFontText,
                   RenameFontText,
                   CloneFontText,
                   DeleteFontText},
            Sub(menuItem)
                Select Case menuItem
                    Case NewFontText
                        setState(GameState.NewFontSize, False)
                    Case PickFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickFont, False)
                        Else
                            setState(GameState.NewFontSize, False)
                        End If
                    Case RenameFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickRenameFont, False)
                        End If
                    Case CloneFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickCloneFont, False)
                        End If
                    Case DeleteFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickDeleteFont, False)
                        End If
                    Case ExportFontText
                        If Editor.Fonts.HasAny Then
                            setState(GameState.PickExportFont, False)
                        End If
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Font Count: {Editor.Fonts.Names.Count}", 15)
    End Sub
End Class
