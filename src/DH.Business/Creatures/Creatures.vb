Friend Class Creatures
    Inherits Thingies(Of CreatureData, ICreature, String)
    Implements ICreatures
    Public Sub New(data As WorldData)
        MyBase.New(data, Function() data.Creatures, Function(name) New Creature(data, name))
    End Sub
    Public Function Create(name As String, hue As Integer) As ICreature Implements ICreatures.Create
        WorldData.Creatures.Add(name, New CreatureData With
                            {
                                .FontName = String.Empty,
                                .GlyphKey = ChrW(32),
                                .Hue = hue
                            })
        Return New Creature(WorldData, name)
    End Function
End Class
