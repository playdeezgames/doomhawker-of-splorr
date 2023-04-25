﻿Friend Class AvatarMenuState
    Inherits BaseMenuState
    Const ChooseAvatarText = "Choose Avatar..."
    Const ClearAvatarText = "Clear Avatar"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From
                   {
                    ChooseAvatarText,
                    ClearAvatarText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case ChooseAvatarText
                SetState(GameState.PickAvatarMap)
            Case ClearAvatarText
                Editor.ClearAvatar()
        End Select
    End Sub

    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim avatar = Editor.Avatar
        If avatar IsNot Nothing Then
            Dim font = Fonts(GameFont.Font5x7)
            font.WriteText(displayBuffer, (0, font.Height * 5), $"Map Name: {avatar.MapName}", Hue.White)
            font.WriteText(displayBuffer, (0, font.Height * 6), $"Column: {avatar.Column}", Hue.White)
            font.WriteText(displayBuffer, (0, font.Height * 7), $"Row: {avatar.Row}", Hue.White)
        End If
    End Sub
End Class
