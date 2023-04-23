Friend Class Item
    Implements IItem
    Private ReadOnly _data As EditorData
    Private ReadOnly _itemName As String
    Private ReadOnly Property ItemData As ItemData
        Get
            Return _data.Items(_itemName)
        End Get
    End Property

    Public Property HueIndex As Integer Implements IItem.HueIndex
        Get
            Return ItemData.HueIndex
        End Get
        Set(value As Integer)
            ItemData.HueIndex = value
        End Set
    End Property

    Public Property Font As IEditorFont Implements IItem.Font
        Get
            If _data.Fonts.ContainsKey(ItemData.FontName) Then
                Return New EditorFont(_data, ItemData.FontName)
            End If
            Return Nothing
        End Get
        Set(value As IEditorFont)
            If value IsNot Nothing Then
                ItemData.FontName = value.FontName
            Else
                ItemData.FontName = Nothing
            End If
        End Set
    End Property

    Public Property GlyphKey As Char Implements IItem.GlyphKey
        Get
            Return ItemData.GlyphKey
        End Get
        Set(value As Char)
            ItemData.GlyphKey = value
        End Set
    End Property

    Public ReadOnly Property FontName As String Implements IItem.FontName
        Get
            Return ItemData.FontName
        End Get
    End Property

    Public Sub New(data As EditorData, itemName As String)
        _data = data
        _itemName = itemName
    End Sub
End Class
