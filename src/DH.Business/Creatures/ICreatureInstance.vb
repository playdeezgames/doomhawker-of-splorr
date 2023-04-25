Public Interface ICreatureInstance
    ReadOnly Property Creature As ICreature
    ReadOnly Property MapName As String
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    Sub Move(direction As Direction)
End Interface
