Public Interface IEditorGlyph
    ReadOnly Property Width As Integer
    ReadOnly Property Height As Integer
    Sub Toggle(x As Integer, y As Integer)
    Function IsSet(x As Integer, y As Integer) As Boolean
End Interface
