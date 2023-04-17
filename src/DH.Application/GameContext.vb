Imports DH.Data

Public Module GameContext
    Public Const ViewWidth = 192
    Public Const ViewHeight = 108
    Public Editor As IEditor
    Public FontWidth As Integer = 8
    Public FontHeight As Integer = 8
    Public FontName As String = ""
    Public GlyphKey As Char = ChrW(32)
    Friend Sub Initialize()
        Editor = New DH.Business.Editor(New EditorData)
        InitializeFonts()
    End Sub
    Friend ReadOnly Fonts As New Dictionary(Of GameFont, Font)
    Private Sub InitializeFonts()
        Fonts.Clear()
        Fonts.Add(GameFont.Font3x5, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont3x5.json"))))
        Fonts.Add(GameFont.Font4x6, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont4x6.json"))))
        Fonts.Add(GameFont.Font5x7, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont5x7.json"))))
        Fonts.Add(GameFont.Font8x8, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont8x8.json"))))
    End Sub
End Module
