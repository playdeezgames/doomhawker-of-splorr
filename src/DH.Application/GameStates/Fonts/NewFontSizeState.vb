Friend Class NewFontSizeState
    Inherits BaseMenuState
    Const CreateText = "Create..."
    Const IncreaseWidthText = "Increase Width"
    Const IncreaseHeightText = "Increase Height"
    Const DecreaseWidthText = "Decrease Width"
    Const DecreaseHeightText = "Decrease Height"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
                   {
                        CreateText,
                        IncreaseWidthText,
                        IncreaseHeightText,
                        DecreaseWidthText,
                        DecreaseHeightText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case IncreaseWidthText
                        FontWidth += 1
                    Case IncreaseHeightText
                        FontHeight += 1
                    Case DecreaseWidthText
                        FontWidth = Math.Max(FontWidth - 1, 1)
                    Case DecreaseHeightText
                        FontHeight = Math.Max(FontHeight - 1, 1)
                    Case CreateText
                        setState(GameState.NewFontName, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.FontsMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Font Width: {FontWidth}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Font Height: {FontHeight}", Hue.White)
    End Sub
End Class
