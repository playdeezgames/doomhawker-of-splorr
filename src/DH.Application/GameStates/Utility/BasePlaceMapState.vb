Friend MustInherit Class BasePlaceMapState
    Inherits BaseGameState(Of String, Command, Sfx, GameState)
    Private ReadOnly _mapNameSource As Func(Of String)
    Private _column As Integer = Zero
    Private _row As Integer = Zero
    Private ReadOnly _onCancel As Action
    Private ReadOnly _onPlace As Action(Of Integer, Integer)
    Protected ReadOnly Property Column As Integer
        Get
            Return _column
        End Get
    End Property
    Protected ReadOnly Property Row As Integer
        Get
            Return _row
        End Get
    End Property

    Public Sub New(
                  parent As IGameController(Of String, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  mapNameSource As Func(Of String),
                  onPlace As Action(Of Integer, Integer),
                  onCancel As Action)
        MyBase.New(parent, setState)
        _onCancel = onCancel
        _onPlace = onPlace
        _mapNameSource = mapNameSource
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Dim map = World.Maps.Retrieve(_mapNameSource())
        Select Case command
            Case Command.UpReleased
                If _row > Zero Then
                    _row -= 1
                End If
            Case Command.DownReleased
                If _row < map.Rows - 1 Then
                    _row += 1
                End If
            Case Command.LeftReleased
                If _column > Zero Then
                    _column -= 1
                End If
            Case Command.RightReleased
                If _column < map.Columns - 1 Then
                    _column += 1
                End If
            Case Command.OkReleased
                _onPlace(_column, _row)
            Case Command.CancelReleased
                _row = Zero
                _column = Zero
                _onCancel()
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of String))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        DrawMap(displayBuffer, _mapNameSource(), _column, _row, White)
    End Sub

    Friend Shared Sub DrawMap(displayBuffer As IPixelSink(Of String), mapName As String, column As Integer, row As Integer, cursorHue As String)
        Dim cellWidth = World.MapCellWidth
        Dim cellHeight = World.MapCellHeight
        displayBuffer.Fill((ViewWidth \ 2 - cellWidth \ 2, ViewHeight \ 2 - cellHeight \ 2), (cellWidth, cellHeight), cursorHue)
        Dim offsetX = ViewWidth \ 2 - cellWidth \ 2 - column * cellWidth
        Dim offsetY = ViewHeight \ 2 - cellHeight \ 2 - row * cellHeight
        Dim map = World.Maps.Retrieve(mapName)
        For c = Zero To map.Columns - 1
            For r = Zero To map.Rows - 1
                Dim plotX = c * cellWidth + offsetX
                Dim plotY = r * cellHeight + offsetY
                Dim cell = map.GetCell(c, r)
                Dim terrain = cell.Terrain
                If terrain IsNot Nothing Then
                    Dim font = terrain.Font.Font
                    font.WriteText(displayBuffer, (plotX, plotY), $"{terrain.GlyphKey}", terrain.Hue)
                End If
                Dim itemInstance As IItemInstance = cell.Item
                If itemInstance IsNot Nothing Then
                    Dim font = itemInstance.Item.Font.Font
                    font.WriteText(displayBuffer, (plotX, plotY), $"{itemInstance.Item.GlyphKey}", itemInstance.Item.Hue)
                End If
                Dim creatureInstance As ICreatureInstance = cell.Creature
                If creatureInstance IsNot Nothing Then
                    Dim font = creatureInstance.Creature.Font.Font
                    font.WriteText(displayBuffer, (plotX, plotY), $"{creatureInstance.Creature.GlyphKey}", creatureInstance.Creature.Hue)
                End If
            Next
        Next
    End Sub
End Class
