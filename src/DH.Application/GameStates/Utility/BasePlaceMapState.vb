Friend MustInherit Class BasePlaceMapState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _column As Integer = 0
    Private _row As Integer = 0
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
                  parent As IGameController(Of Hue, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  onPlace As Action(Of Integer, Integer),
                  onCancel As Action)
        MyBase.New(parent, setState)
        _onCancel = onCancel
        _onPlace = onPlace
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Dim map = Editor.Maps.Retrieve(MapName)
        Select Case command
            Case Command.UpReleased
                If _row = 0 Then
                    HandleCancel()
                Else
                    _row -= 1
                End If
            Case Command.DownReleased
                If _row = map.Rows - 1 Then
                    HandleCancel()
                Else
                    _row += 1
                End If
            Case Command.LeftReleased
                If _column = 0 Then
                    HandleCancel()
                Else
                    _column -= 1
                End If
            Case Command.RightReleased
                If _column = map.Columns - 1 Then
                    HandleCancel()
                Else
                    _column += 1
                End If
            Case Command.FireReleased
                _onPlace(_column, _row)
        End Select
    End Sub

    Private Sub HandleCancel()
        _row = 0
        _column = 0
        _onCancel()
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        DrawMap(displayBuffer)
    End Sub

    Private Sub DrawMap(displayBuffer As IPixelSink(Of Hue))
        Dim cellWidth = Editor.MapCellWidth
        Dim cellHeight = Editor.MapCellHeight
        displayBuffer.Fill((ViewWidth \ 2 - cellWidth \ 2, ViewHeight \ 2 - cellHeight \ 2), (cellWidth, cellHeight), Hue.White)
        Dim offsetX = ViewWidth \ 2 - cellWidth \ 2 - _column * cellWidth
        Dim offsetY = ViewHeight \ 2 - cellHeight \ 2 - _row * cellHeight
        Dim map = Editor.Maps.Retrieve(MapName)
        For c = 0 To map.Columns - 1
            For r = 0 To map.Rows - 1
                Dim plotX = c * cellWidth + offsetX
                Dim plotY = r * cellHeight + offsetY
                Dim cell = map.GetCell(c, r)
                Dim terrain = cell.Terrain
                If terrain IsNot Nothing Then
                    Dim font = terrain.Font.Font
                    font.WriteText(displayBuffer, (plotX, plotY), $"{terrain.GlyphKey}", AllHues(terrain.HueIndex))
                End If
                Dim itemInstance As IItemInstance = cell.Item
                If itemInstance IsNot Nothing Then
                    Dim font = itemInstance.Item.Font.Font
                    font.WriteText(displayBuffer, (plotX, plotY), $"{itemInstance.Item.GlyphKey}", AllHues(itemInstance.Item.HueIndex))
                End If
            Next
        Next
    End Sub
End Class
