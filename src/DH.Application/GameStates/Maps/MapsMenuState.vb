﻿Friend Class MapsMenuState
    Inherits BaseMenuState
    Const NewMapText = "New Map..."
    Const EditMapText = "Pick Map..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                    NewMapText,
                    EditMapText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewMapText
                SetState(GameState.NewMapName)
            Case EditMapText
                If Editor.Maps.HasAny Then
                    SetState(GameState.PickMap)
                Else
                    SetState(GameState.NewMapName)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Map Count: {Editor.Maps.Names.Count}", Hue.White)
    End Sub
End Class
