Friend Class BaseInputState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _character As Char = " "c
    Private _buffer As String = ""
    Private ReadOnly _caption As String
    Private ReadOnly _onCancel As Action
    Private ReadOnly _onDone As Action(Of String)
    Public Sub New(
                  parent As IGameController(Of Hue, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  caption As String,
                  onCancel As Action,
                  onDone As Action(Of String))
        MyBase.New(parent, setState)
        _caption = caption
        _onCancel = onCancel
        _onDone = onDone
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                ChangeCharacterBy(-16)
            Case Command.DownReleased
                ChangeCharacterBy(16)
            Case Command.RightReleased
                If _character >= ChrW(128) Then
                    ChangeCharacterBy(4)
                Else
                    ChangeCharacterBy(1)
                End If
            Case Command.LeftReleased
                If _character >= ChrW(128) Then
                    ChangeCharacterBy(-4)
                Else
                    ChangeCharacterBy(-1)
                End If
            Case Command.FireReleased
                Select Case AscW(_character)
                    Case 128, 129, 130, 131
                        If _buffer.Length > 1 Then
                            _buffer = _buffer.Substring(0, _buffer.Length - 1)
                        Else
                            _buffer = ""
                        End If
                    Case 132, 133, 134, 135
                        _buffer = ""
                    Case 136, 137, 138, 139
                        _onDone(_buffer)
                        _buffer = ""
                    Case 140, 141, 142, 143
                        _buffer = ""
                        _onCancel()
                    Case Else
                        _buffer += _character
                End Select
        End Select
    End Sub
    Private Sub ChangeCharacterBy(delta As Integer)
        Dim ascii = AscW(_character) + delta
        Const MinimumAscii = 32
        Const MaximumAscii = 144
        While ascii < MinimumAscii
            ascii += (MaximumAscii - MinimumAscii)
        End While
        While ascii >= MaximumAscii
            ascii -= (MaximumAscii - MinimumAscii)
        End While
        _character = ChrW(ascii)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim CellWidth = font.TextWidth(" ")
        Dim CellHeight = font.Height
        font.WriteText(displayBuffer, (0, 0), _caption, Hue.White)
        font.WriteText(displayBuffer, (0, font.Height), _buffer, Hue.LightBlue)
        For row = 0 To 5
            For column = 0 To 15
                Dim ch As Char = ChrW(row * 16 + column + 32)
                If ch = _character Then
                    displayBuffer.Fill((column * CellWidth, (row + 3) * CellHeight), (CellWidth, CellHeight), Hue.White)
                    font.WriteText(displayBuffer, (column * CellWidth, (row + 3) * CellHeight), $"{ch}", Hue.Black)
                Else
                    font.WriteText(displayBuffer, (column * CellWidth, (row + 3) * CellHeight), $"{ch}", Hue.Gray)
                End If
            Next
        Next
        If _character >= ChrW(128) AndAlso _character < ChrW(132) Then
            displayBuffer.Fill((0, 9 * CellHeight), (CellWidth * 4, CellHeight), Hue.White)
            font.WriteText(displayBuffer, (0, 9 * CellHeight), " <- ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (0, 9 * CellHeight), " <- ", Hue.Gray)
        End If
        If _character >= ChrW(132) AndAlso _character < ChrW(136) Then
            displayBuffer.Fill((CellWidth * 4, 9 * CellHeight), (CellWidth * 4, CellHeight), Hue.White)
            font.WriteText(displayBuffer, (CellWidth * 4, 9 * CellHeight), " << ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (CellWidth * 4, 9 * CellHeight), " << ", Hue.Gray)
        End If
        If _character >= ChrW(136) AndAlso _character < ChrW(140) Then
            displayBuffer.Fill((CellWidth * 8, 9 * CellHeight), (CellWidth * 4, CellHeight), Hue.White)
            font.WriteText(displayBuffer, (CellWidth * 8, 9 * CellHeight), " OK ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (CellWidth * 8, 9 * CellHeight), " OK ", Hue.Gray)
        End If
        If _character >= ChrW(140) AndAlso _character < ChrW(144) Then
            displayBuffer.Fill((CellWidth * 12, 9 * CellHeight), (CellWidth * 4, CellHeight), Hue.White)
            font.WriteText(displayBuffer, (CellWidth * 12, 9 * CellHeight), " XX ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (CellWidth * 12, 9 * CellHeight), " XX ", Hue.Gray)
        End If
    End Sub
    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
