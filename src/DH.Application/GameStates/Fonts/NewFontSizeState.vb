Friend Class NewFontSizeState
    Inherits BaseMenuState
    Const NeverMindText = "Never Mind"
    Const CreateText = "Create..."
    Const IncreaseWidthText = "Increase Width"
    Const IncreaseHeightText = "Increase Height"
    Const DecreaseWidthText = "Decrease Width"
    Const DecreaseHeightText = "Decrease Height"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState, New List(Of String) From
                   {
                        NeverMindText,
                        CreateText,
                        IncreaseWidthText,
                        IncreaseHeightText,
                        DecreaseWidthText,
                        DecreaseHeightText
                   })
    End Sub
    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NeverMindText
                SetState(GameState.FontsMenu)
            Case IncreaseWidthText
                NewFontWidth += 1
            Case IncreaseHeightText
                NewFontHeight += 1
            Case DecreaseWidthText
                NewFontWidth = Math.Max(NewFontWidth - 1, 1)
            Case DecreaseHeightText
                NewFontHeight = Math.Max(NewFontHeight - 1, 1)
            Case CreateText
                SetState(GameState.NewFontName)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Width: {NewFontWidth}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Height: {NewFontHeight}", Hue.White)
    End Sub
End Class
