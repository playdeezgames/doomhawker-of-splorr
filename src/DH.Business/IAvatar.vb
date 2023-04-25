Public Interface IAvatar
    Property MapName As String
    Property Column As Integer
    Property Row As Integer
    ReadOnly Property Creature As ICreatureInstance
End Interface
