Friend Class SettingsState
    Inherits BaseMenuState
    Const IncreaseWidthText = "Increase Cell Width"
    Const IncreaseHeightText = "Increase Cell Height"
    Const DecreaseWidthText = "Decrease Cell Width"
    Const DecreaseHeightText = "Decrease Cell Height"

    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                   IncreaseWidthText,
                   IncreaseHeightText,
                   DecreaseWidthText,
                   DecreaseHeightText
                   },
            Sub(menuItem)
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
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, 5 * font.Height), $"Cell Width: {Editor.MapCellWidth}", White)
        font.WriteText(displayBuffer, (Zero, 6 * font.Height), $"Cell Height: {Editor.MapCellHeight}", White)
    End Sub
End Class
