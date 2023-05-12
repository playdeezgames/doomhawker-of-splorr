Friend Class CreatureInstance
    Implements ICreatureInstance

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private _column As Integer
    Private _row As Integer

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Public Sub Move(direction As Direction) Implements ICreatureInstance.Move
        Dim nextColumn = direction.NextColumn(_column, _row)
        Dim nextRow = direction.NextRow(_column, _row)
        Dim map = New EditorMap(_data, _mapName)
        Dim mapCell = map.GetCell(nextColumn, nextRow)
        If mapCell Is Nothing Then
            Return
        End If
        Dim terrain = mapCell.Terrain
        If terrain.Tenantability Then
            '-check for creature
            '-when creature found:
            '--cancel move
            '--result: creature
            '-check for item
            '-when item found:
            '--cancel move
            '--result: item
            '-move creature
            Dim instanceData = CreatureInstanceData
            MapCellData.Creature = Nothing
            _column = nextColumn
            _row = nextRow
            MapCellData.Creature = instanceData
            '-trigger any move event there might be
            '-result: moved
        Else
            '-trigger any bump event
            '-cancel move
            '-result: bump
        End If
    End Sub

    Private ReadOnly Property CreatureInstanceData As CreatureInstanceData
        Get
            Return MapCellData.Creature
        End Get
    End Property
    Private ReadOnly Property MapCellData As MapCellData
        Get
            Dim map = _data.Maps(_mapName)
            Return map.Cells(_column + _row * map.Columns)
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
