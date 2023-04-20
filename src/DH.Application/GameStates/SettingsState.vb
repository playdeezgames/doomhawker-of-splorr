Friend Class SettingsState
    Inherits BaseMenuState
    Const IncreaseWidthText = "Increase Cell Width"
    Const IncreaseHeightText = "Increase Cell Height"
    Const DecreaseWidthText = "Decrease Cell Width"
    Const DecreaseHeightText = "Decrease Cell Height"

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From {
                   IncreaseWidthText,
                   IncreaseHeightText,
                   DecreaseWidthText,
                   DecreaseHeightText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case IncreaseWidthText
                Editor.MapCellWidth += 1
            Case IncreaseHeightText
                Editor.MapCellHeight += 1
            Case DecreaseWidthText
                Editor.MapCellWidth -= 1
            Case DecreaseHeightText
                Editor.MapCellHeight -= 1
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, 5 * font.Height), $"Cell Width: {Editor.MapCellWidth}", Hue.White)
        font.WriteText(displayBuffer, (0, 6 * font.Height), $"Cell Height: {Editor.MapCellHeight}", Hue.White)
    End Sub
End Class
