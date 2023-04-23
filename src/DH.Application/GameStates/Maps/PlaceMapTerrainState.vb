Friend Class PlaceMapTerrainState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _column As Integer = 0
    Private _row As Integer = 0

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Dim map = Editor.GetMap(MapName)
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
                Dim mapCell = map.GetCell(_column, _row)
                mapCell.Terrain = Editor.Terrains.Retrieve(TerrainName)
        End Select
    End Sub

    Private Sub HandleCancel()
        _row = 0
        _column = 0
        SetState(GameState.EditMap)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        DrawMap(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Dim currentTerrain = Editor.GetMap(MapName).GetCell(_column, _row).Terrain.TerrainName
        font.WriteText(displayBuffer, (0, 0), $"({_column},{_row}) {currentTerrain}", Hue.White)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Placing: {TerrainName}", Hue.White)
    End Sub

    Private Sub DrawMap(displayBuffer As IPixelSink(Of Hue))
        Dim cellWidth = Editor.MapCellWidth
        Dim cellHeight = Editor.MapCellHeight
        displayBuffer.Fill((ViewWidth \ 2 - cellWidth \ 2, ViewHeight \ 2 - cellHeight \ 2), (cellWidth, cellHeight), Hue.White)
        Dim offsetX = ViewWidth \ 2 - cellWidth \ 2 - _column * cellWidth
        Dim offsetY = ViewHeight \ 2 - cellHeight \ 2 - _row * cellHeight
        Dim map = Editor.GetMap(MapName)
        For column = 0 To map.Columns - 1
            For row = 0 To map.Rows - 1
                Dim plotX = column * cellWidth + offsetX
                Dim plotY = row * cellHeight + offsetY
                Dim cell = map.GetCell(column, row)
                Dim terrain = cell.Terrain
                If terrain IsNot Nothing Then
                    Dim font = terrain.Font.Font
                    font.WriteText(displayBuffer, (plotX, plotY), $"{terrain.GlyphKey}", AllHues(terrain.HueIndex))
                End If
            Next
        Next
    End Sub
End Class
