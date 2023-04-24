Friend Class Creature
    Implements ICreature

    Private ReadOnly _data As EditorData
    Private ReadOnly _name As String
    Private ReadOnly Property CreatureData As CreatureData
        Get
            Return _data.Creatures(_name)
        End Get
    End Property

    Public Sub New(data As EditorData, name As String)
        Me._data = data
        Me._name = name
    End Sub

    Public Property FontName As String Implements ISprite.FontName
        Get
            Return CreatureData.FontName
        End Get
        Set(value As String)
            CreatureData.FontName = value
        End Set
    End Property

    Public Property GlyphKey As Char Implements ISprite.GlyphKey
        Get
            Return CreatureData.GlyphKey
        End Get
        Set(value As Char)
            CreatureData.GlyphKey = value
        End Set
    End Property

    Public Property HueIndex As Integer Implements ISprite.HueIndex
        Get
            Return CreatureData.HueIndex
        End Get
        Set(value As Integer)
            CreatureData.HueIndex = value
        End Set
    End Property

    Public Property Font As IEditorFont Implements ISprite.Font
        Get
            If _data.Fonts.ContainsKey(CreatureData.FontName) Then
                Return New EditorFont(_data, CreatureData.FontName)
            End If
            Return Nothing
        End Get
        Set(value As IEditorFont)
            If value IsNot Nothing Then
                CreatureData.FontName = value.FontName
            Else
                CreatureData.FontName = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property Name As String Implements ISprite.Name
        Get
            Return _name
        End Get
    End Property
End Class
