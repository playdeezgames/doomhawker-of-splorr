Public Interface ICreature
    Inherits ISprite

    Function CreateInstance(mapName As String, column As Integer, row As Integer) As ICreatureInstance
End Interface
