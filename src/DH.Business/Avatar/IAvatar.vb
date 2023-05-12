Public Interface IAvatar
    Property MapName As String
    Property Column As Integer
    Property Row As Integer
    ReadOnly Property Creature As ICreatureInstance
    Sub Move(direction As Direction)
End Interface
