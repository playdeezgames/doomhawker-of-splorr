Friend Class BaseInputState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)
    Private _character As Char = " "c
    Private _buffer As String = ""
    Private ReadOnly _caption As String
    Private ReadOnly _onCancel As Action
    Private ReadOnly _onDone As Action(Of String)
    Public Sub New(
                  parent As IGameController(Of Integer, Command, Sfx),
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
                            _buffer = _buffer.Substring(Zero, _buffer.Length - 1)
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
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Zero)
        Dim font = Fonts(GameFont.Font5x7)
        Dim CellWidth = font.TextWidth(" ")
        Dim CellHeight = font.Height
        font.WriteText(displayBuffer, (Zero, Zero), _caption, 15)
        font.WriteText(displayBuffer, (Zero, font.Height), _buffer, 9)
        For row = Zero To 5
            For column = Zero To 15
                Dim ch As Char = ChrW(row * 16 + column + 32)
                If ch = _character Then
                    displayBuffer.Fill((column * CellWidth, (row + 3) * CellHeight), (CellWidth, CellHeight), 15)
                    font.WriteText(displayBuffer, (column * CellWidth, (row + 3) * CellHeight), $"{ch}", Zero)
                Else
                    font.WriteText(displayBuffer, (column * CellWidth, (row + 3) * CellHeight), $"{ch}", 7)
                End If
            Next
        Next
        If _character >= ChrW(128) AndAlso _character < ChrW(132) Then
            displayBuffer.Fill((Zero, 9 * CellHeight), (CellWidth * 4, CellHeight), 15)
            font.WriteText(displayBuffer, (Zero, 9 * CellHeight), " <- ", Zero)
        Else
            font.WriteText(displayBuffer, (Zero, 9 * CellHeight), " <- ", 7)
        End If
        If _character >= ChrW(132) AndAlso _character < ChrW(136) Then
            displayBuffer.Fill((CellWidth * 4, 9 * CellHeight), (CellWidth * 4, CellHeight), 15)
            font.WriteText(displayBuffer, (CellWidth * 4, 9 * CellHeight), " << ", Zero)
        Else
            font.WriteText(displayBuffer, (CellWidth * 4, 9 * CellHeight), " << ", 7)
        End If
        If _character >= ChrW(136) AndAlso _character < ChrW(140) Then
            displayBuffer.Fill((CellWidth * 8, 9 * CellHeight), (CellWidth * 4, CellHeight), 15)
            font.WriteText(displayBuffer, (CellWidth * 8, 9 * CellHeight), " OK ", Zero)
        Else
            font.WriteText(displayBuffer, (CellWidth * 8, 9 * CellHeight), " OK ", 7)
        End If
        If _character >= ChrW(140) AndAlso _character < ChrW(144) Then
            displayBuffer.Fill((CellWidth * 12, 9 * CellHeight), (CellWidth * 4, CellHeight), 15)
            font.WriteText(displayBuffer, (CellWidth * 12, 9 * CellHeight), " XX ", Zero)
        Else
            font.WriteText(displayBuffer, (CellWidth * 12, 9 * CellHeight), " XX ", 7)
        End If
    End Sub
End Class
