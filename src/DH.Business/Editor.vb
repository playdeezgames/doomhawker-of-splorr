Public Class Editor
    Implements IEditor
    Private _data As EditorData
    Sub New(data As EditorData)
        _data = data
    End Sub
    Const FirstCharacter = 32
    Const LastCharacter = 127

    Public ReadOnly Property FontNames As IEnumerable(Of String) Implements IEditor.FontNames
        Get
            Return _data.Fonts.Keys
        End Get
    End Property

    Public ReadOnly Property HasFonts As Boolean Implements IEditor.HasFonts
        Get
            Return _data.Fonts.Any
        End Get
    End Property

    Public ReadOnly Property TerrainNames As IEnumerable(Of String) Implements IEditor.TerrainNames
        Get
            Return _data.Terrains.Keys
        End Get
    End Property

    Public ReadOnly Property HasTerrains As Boolean Implements IEditor.HasTerrains
        Get
            Return _data.Terrains.Any
        End Get
    End Property

    Public ReadOnly Property HasMaps As Boolean Implements IEditor.HasMaps
        Get
            Return _data.Maps.Any
        End Get
    End Property

    Public Function CreateFont(name As String, width As Integer, height As Integer) As IEditorFont Implements IEditor.CreateFont
        Dim fontData As New FontData With
        {
            .Height = height,
            .Glyphs = New Dictionary(Of Char, GlyphData)
        }
        For character = FirstCharacter To LastCharacter
            Dim glyphData = New GlyphData With
                {
                    .Width = width,
                    .Lines = New Dictionary(Of Integer, IEnumerable(Of Integer))
                }
            For line = 0 To height - 1
                glyphData.Lines(line) = Array.Empty(Of Integer)
            Next
            fontData.Glyphs(ChrW(character)) = glyphData
        Next
        _data.Fonts(name) = fontData
        Return New EditorFont(_data, name)
    End Function

    Public Function GetFont(fontName As String) As IEditorFont Implements IEditor.GetFont
        Return New EditorFont(_data, fontName)
    End Function

    Public Sub Save(fileName As String) Implements IEditor.Save
        File.WriteAllText(fileName, JsonSerializer.Serialize(_data))
    End Sub

    Public Sub Load(fileName As String) Implements IEditor.Load
        _data = JsonSerializer.Deserialize(Of EditorData)(File.ReadAllText(fileName))
    End Sub

    Public Function CreateTerrain(terrainName As String) As ITerrain Implements IEditor.CreateTerrain
        _data.Terrains(terrainName) = New TerrainData With {
            .FontName = String.Empty,
            .GlyphKey = " "c,
            .Tenantability = False}
        Return New Terrain(_data, terrainName)
    End Function

    Public Function GetTerrain(terrainName As String) As ITerrain Implements IEditor.GetTerrain
        Return New Terrain(_data, terrainName)
    End Function

    Public Function HasFont(fontName As String) As Boolean Implements IEditor.HasFont
        Return _data.Fonts.ContainsKey(fontName)
    End Function
End Class
