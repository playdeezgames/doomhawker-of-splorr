﻿Friend Class MapsMenuState
    Inherits BaseMenuState
    Const NewMapText = "New Map..."
    Const EditMapText = "Pick Map..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                    NewMapText,
                    EditMapText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case NewMapText
                        setState(GameState.NewMapName, False)
                    Case EditMapText
                        If World.Maps.HasAny Then
                            setState(GameState.PickMap, False)
                        Else
                            setState(GameState.NewMapName, False)
                        End If
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, ViewHeight - font.Height), $"Map Count: {World.Maps.Names.Count}", White)
    End Sub
End Class
