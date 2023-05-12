Imports DH.Data

Public Module WorldContext
    Public Const ViewWidth = 192
    Public Const ViewHeight = 108
    Public World As IWorld
    Public FontWidth As Integer = 8
    Public FontHeight As Integer = 8
    Public FontName As String = ""
    Public ColorName As String = ""
    Public GlyphKey As Char = ChrW(32)
    Public TerrainName As String = ""
    Public ItemName As String = ""
    Public CreatureName As String = ""
    Public MapName As String = ""
    Public MapWidth As Integer
    Public MapHeight As Integer
    Public Messages As New Queue(Of EditorMessage)
    Public HueChangeAction As Action(Of String)
    Friend Sub Initialize()
        World = New DH.Business.World(New WorldData)
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
