Imports System.Runtime.CompilerServices

Friend Module DirectionExtensions
    <Extension>
    Function NextColumn(direction As Direction, column As Integer, row As Integer) As Integer
        Select Case direction
            Case Direction.North
                Return column
            Case Direction.East
                Return column + 1
            Case Direction.South
                Return column
            Case Direction.West
                Return column - 1
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function NextRow(direction As Direction, column As Integer, row As Integer) As Integer
        Select Case direction
            Case Direction.North
                Return row - 1
            Case Direction.East
                Return row
            Case Direction.South
                Return row + 1
            Case Direction.West
                Return row
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
