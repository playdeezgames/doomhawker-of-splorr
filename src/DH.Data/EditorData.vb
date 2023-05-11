Public Class EditorData
    Public Property Colors As New Dictionary(Of String, ColorData) From
        {
            {"black", New ColorData With {
                .Red = 0,
                .Green = 0,
                .Blue = 0
            }},
            {"blue", New ColorData With {
                .Red = 0,
                .Green = 0,
                .Blue = 170
            }},
            {"green", New ColorData With {
                .Red = 0,
                .Green = 170,
                .Blue = 0
            }},
            {"cyan", New ColorData With {
                .Red = 0,
                .Green = 170,
                .Blue = 170
            }},
            {"red", New ColorData With {
                .Red = 170,
                .Green = 0,
                .Blue = 0
            }},
            {"magenta", New ColorData With {
                .Red = 170,
                .Green = 0,
                .Blue = 170
            }},
            {"brown", New ColorData With {
                .Red = 170,
                .Green = 85,
                .Blue = 0
            }},
            {"gray", New ColorData With {
                .Red = 170,
                .Green = 170,
                .Blue = 170
            }},
            {"darkgray", New ColorData With {
                .Red = 85,
                .Green = 85,
                .Blue = 85
            }},
            {"lightblue", New ColorData With {
                .Red = 85,
                .Green = 85,
                .Blue = 255
            }},
            {"lightgreen", New ColorData With {
                .Red = 85,
                .Green = 255,
                .Blue = 85
            }},
            {"lightcyan", New ColorData With {
                .Red = 85,
                .Green = 255,
                .Blue = 255
            }},
            {"lightred", New ColorData With {
                .Red = 255,
                .Green = 85,
                .Blue = 85
            }},
            {"lightmagenta", New ColorData With {
                .Red = 255,
                .Green = 85,
                .Blue = 255
            }},
            {"yellow", New ColorData With {
                .Red = 255,
                .Green = 255,
                .Blue = 85
            }},
            {"white", New ColorData With {
                .Red = 255,
                .Green = 255,
                .Blue = 255
            }}
        }
    Public Property Fonts As New Dictionary(Of String, FontData)
    Public Property Terrains As New Dictionary(Of String, TerrainData)
    Public Property Maps As New Dictionary(Of String, MapData)
    Public Property Items As New Dictionary(Of String, ItemData)
    Public Property Creatures As New Dictionary(Of String, CreatureData)
    Public Property Avatar As AvatarData
    Public Property MapCellWidth As Integer = 8
    Public Property MapCellHeight As Integer = 8
End Class
