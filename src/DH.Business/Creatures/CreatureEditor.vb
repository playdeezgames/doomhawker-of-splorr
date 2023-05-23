Friend Class CreatureEditor
    Inherits Thingies(Of CreatureData, ICreature)
    Implements ICreatureEditor

    Private ReadOnly _data As WorldData

    Public Sub New(data As WorldData)
        MyBase.New(Function() data.Creatures, Function(name) New Creature(data, name))
        Me._data = data
    End Sub
    Public Function Create(name As String, hue As String) As ICreature Implements ICreatureEditor.Create
        _data.Creatures.Add(name, New CreatureData With
                            {
                                .FontName = String.Empty,
                                .GlyphKey = ChrW(32),
                                .Hue = hue
                            })
        Return New Creature(_data, name)
    End Function
End Class
