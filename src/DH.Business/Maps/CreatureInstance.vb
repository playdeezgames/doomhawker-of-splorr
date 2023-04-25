Friend Class CreatureInstance
    Implements ICreatureInstance

    Private ReadOnly _data As EditorData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer

    Public Sub New(data As EditorData, mapName As String, column As Integer, row As Integer)
        Me._data = data
        Me._mapName = mapName
        Me._column = column
        Me._row = row
    End Sub
    Private ReadOnly Property CreatureInstanceData As CreatureInstanceData
        Get
            Dim map = _data.Maps(_mapName)
            Return map.Cells(_column + _row * map.Columns).Creature
        End Get
    End Property


    Public ReadOnly Property Creature As ICreature Implements ICreatureInstance.Creature
        Get
            Return New Creature(_data, CreatureInstanceData.CreatureName)
        End Get
    End Property

    Public ReadOnly Property MapName As String Implements ICreatureInstance.MapName
        Get
            Return _mapName
        End Get
    End Property

    Public ReadOnly Property Column As Integer Implements ICreatureInstance.Column
        Get
            Return _column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ICreatureInstance.Row
        Get
            Return _row
        End Get
    End Property
End Class
