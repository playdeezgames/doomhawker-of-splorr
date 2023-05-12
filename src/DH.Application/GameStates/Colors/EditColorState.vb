Friend Class EditColorState
    Inherits BaseMenuState
    Const RedPlus1Text = "Red + 1"
    Const RedPlus16Text = "Red + 10h"
    Const GreenPlus1Text = "Green + 1"
    Const GreenPlus16Text = "Green + 10h"
    Const BluePlus1Text = "Blue + 1"
    Const BluePlus16Text = "Blue + 10h"

    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                RedPlus1Text,
                RedPlus16Text,
                GreenPlus1Text,
                GreenPlus16Text,
                BluePlus1Text,
                BluePlus16Text
            },
            Sub(menuItem)
                Dim color = World.Colors.Retrieve(ColorName)
                Select Case menuItem
                    Case RedPlus1Text
                        color.Red = CByte((color.Red + 1) And 255)
                    Case RedPlus16Text
                        color.Red = CByte((color.Red + 16) And 255)
                    Case GreenPlus1Text
                        color.Green = CByte((color.Green + 1) And 255)
                    Case GreenPlus16Text
                        color.Green = CByte((color.Green + 16) And 255)
                    Case BluePlus1Text
                        color.Blue = CByte((color.Blue + 1) And 255)
                    Case BluePlus16Text
                        color.Blue = CByte((color.Blue + 16) And 255)
                End Select
            End Sub,
            Sub()
                setState(GameState.ColorsMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = ViewHeight - font.Height * 4
        Dim color = World.Colors.Retrieve(ColorName)
        font.WriteText(displayBuffer, (0, y), $"Color: {ColorName}", White)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Red: {color.Red:X}h", White)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Green: {color.Green:X}h", White)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Blue: {color.Blue:X}h", White)
        displayBuffer.Fill((ViewWidth - 16, ViewHeight - 16), (16, 16), ColorName)
    End Sub
End Class
