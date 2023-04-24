Public Class EditorData
    Public Property Fonts As New Dictionary(Of String, FontData)
    Public Property Terrains As New Dictionary(Of String, TerrainData)
    Public Property Maps As New Dictionary(Of String, MapData)
    Public Property Items As New Dictionary(Of String, ItemData)
    Public Property Creatures As New Dictionary(Of String, CreatureData)
    Public Property MapCellWidth As Integer = 8
    Public Property MapCellHeight As Integer = 8
End Class
