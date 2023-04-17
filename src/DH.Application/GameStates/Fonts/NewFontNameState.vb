Imports System.Data.Common

Friend Class NewFontNameState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _character As Char = " "c

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
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
                        If FontName.Length > 1 Then
                            FontName = FontName.Substring(0, FontName.Length - 1)
                        Else
                            FontName = ""
                        End If
                    Case 132, 133, 134, 135
                        FontName = ""
                    Case 136, 137, 138, 139
                        Editor.CreateFont(FontName, FontWidth, FontHeight)
                    Case 140, 141, 142, 143
                        FontName = ""
                        SetState(GameState.FontsMenu)
                    Case Else
                        FontName += _character
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
        font.WriteText(displayBuffer, (0, 0), "Font Name:", Hue.White)
        font.WriteText(displayBuffer, (0, font.Height), FontName, Hue.LightBlue)
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
            font.WriteText(displayBuffer, (CellWidth * 4, 9 * CellHeight), " <x ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (CellWidth * 4, 9 * CellHeight), " <x ", Hue.Gray)
        End If
        If _character >= ChrW(136) AndAlso _character < ChrW(140) Then
            displayBuffer.Fill((CellWidth * 8, 9 * CellHeight), (CellWidth * 4, CellHeight), Hue.White)
            font.WriteText(displayBuffer, (CellWidth * 8, 9 * CellHeight), " => ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (CellWidth * 8, 9 * CellHeight), " => ", Hue.Gray)
        End If
        If _character >= ChrW(140) AndAlso _character < ChrW(144) Then
            displayBuffer.Fill((CellWidth * 12, 9 * CellHeight), (CellWidth * 4, CellHeight), Hue.White)
            font.WriteText(displayBuffer, (CellWidth * 12, 9 * CellHeight), " xx ", Hue.Black)
        Else
            font.WriteText(displayBuffer, (CellWidth * 12, 9 * CellHeight), " xx ", Hue.Gray)
        End If
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
