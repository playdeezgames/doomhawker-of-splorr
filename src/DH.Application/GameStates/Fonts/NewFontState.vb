Friend Class NewFontState
    Inherits BaseMenuState
    Const NeverMindText = "Never Mind"
    Const CreateText = "Create..."
    Const IncreaseWidthText = "Increase Width"
    Const IncreaseHeightText = "Increase Height"
    Const DecreaseWidthText = "Decrease Width"
    Const DecreaseHeightText = "Decrease Height"
    Private _fontWidth As Integer = 8
    Private _fontHeight As Integer = 8
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
                _fontWidth += 1
            Case IncreaseHeightText
                _fontHeight += 1
            Case DecreaseWidthText
                _fontWidth = Math.Max(_fontWidth - 1, 1)
            Case DecreaseHeightText
                _fontHeight = Math.Max(_fontHeight - 1, 1)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, font.Height * 8), $"Width: {_fontWidth}", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height * 9), $"Height: {_fontHeight}", Hue.White)
    End Sub
End Class
