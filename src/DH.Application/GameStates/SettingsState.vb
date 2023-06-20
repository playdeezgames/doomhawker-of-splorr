Friend Class SettingsState
    Inherits BaseMenuState
    Const IncreaseWidthText = "Increase Cell Width"
    Const IncreaseHeightText = "Increase Cell Height"
    Const DecreaseWidthText = "Decrease Cell Width"
    Const DecreaseHeightText = "Decrease Cell Height"

    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
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
                        World.MapCellWidth += 1
                    Case IncreaseHeightText
                        World.MapCellHeight += 1
                    Case DecreaseWidthText
                        World.MapCellWidth -= 1
                    Case DecreaseHeightText
                        World.MapCellHeight -= 1
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, 5 * font.Height), $"Cell Width: {World.MapCellWidth}", White)
        font.WriteText(displayBuffer, (Zero, 6 * font.Height), $"Cell Height: {World.MapCellHeight}", White)
    End Sub
End Class
