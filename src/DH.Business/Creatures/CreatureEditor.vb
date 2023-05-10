﻿Friend Class CreatureEditor
    Inherits ThingieEditor(Of CreatureData, ICreature)
    Implements ICreatureEditor

    Private ReadOnly _data As EditorData

    Public Sub New(data As EditorData)
        MyBase.New(Function() data.Creatures, Function(name) New Creature(data, name))
        Me._data = data
    End Sub
    Public Function Create(name As String) As ICreature Implements ICreatureEditor.Create
        _data.Creatures.Add(name, New CreatureData With
                            {
                                .FontName = String.Empty,
                                .GlyphKey = ChrW(32),
                                .HueIndex = Zero
                            })
        Return New Creature(_data, name)
    End Function
End Class
