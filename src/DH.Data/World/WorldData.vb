Public Class WorldData
    Public Property Colors As New Dictionary(Of Integer, ColorData) From
        {
            {0, New ColorData With {
                .Red = 0,
                .Green = 0,
                .Blue = 0
            }},
            {1, New ColorData With {
                .Red = 0,
                .Green = 0,
                .Blue = 170
            }},
            {2, New ColorData With {
                .Red = 0,
                .Green = 170,
                .Blue = 0
            }},
            {3, New ColorData With {
                .Red = 0,
                .Green = 170,
                .Blue = 170
            }},
            {4, New ColorData With {
                .Red = 170,
                .Green = 0,
                .Blue = 0
            }},
            {5, New ColorData With {
                .Red = 170,
                .Green = 0,
                .Blue = 170
            }},
            {6, New ColorData With {
                .Red = 170,
                .Green = 85,
                .Blue = 0
            }},
            {7, New ColorData With {
                .Red = 170,
                .Green = 170,
                .Blue = 170
            }},
            {8, New ColorData With {
                .Red = 85,
                .Green = 85,
                .Blue = 85
            }},
            {9, New ColorData With {
                .Red = 85,
                .Green = 85,
                .Blue = 255
            }},
            {10, New ColorData With {
                .Red = 85,
                .Green = 255,
                .Blue = 85
            }},
            {11, New ColorData With {
                .Red = 85,
                .Green = 255,
                .Blue = 255
            }},
            {12, New ColorData With {
                .Red = 255,
                .Green = 85,
                .Blue = 85
            }},
            {13, New ColorData With {
                .Red = 255,
                .Green = 85,
                .Blue = 255
            }},
            {14, New ColorData With {
                .Red = 255,
                .Green = 255,
                .Blue = 85
            }},
            {15, New ColorData With {
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
