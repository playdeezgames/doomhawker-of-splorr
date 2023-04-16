Public Module GameContext
    Public Const ViewWidth = 160
    Public Const ViewHeight = 90
    Public Const CellWidth = 5
    Public Const CellHeight = 5
    Public Const CellColumns = ViewWidth \ CellWidth
    Public Const CellRows = ViewHeight \ CellHeight
    Public X As Integer
    Public Y As Integer
    Public Sub Initialize()
        X = CellColumns \ 2
        Y = CellRows \ 2
    End Sub
End Module
