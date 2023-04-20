Public Interface IEditorFont
    ReadOnly Property Font As Font
    Function GetGlyph(glyphKey As Char) As IEditorGlyph
End Interface
