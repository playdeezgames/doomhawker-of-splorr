Friend Class Creature
    Implements ICreature

    Private data As EditorData
    Private name As String

    Public Sub New(data As EditorData, name As String)
        Me.data = data
        Me.name = name
    End Sub
End Class
