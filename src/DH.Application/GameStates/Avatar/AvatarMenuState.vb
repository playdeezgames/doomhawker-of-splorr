Friend Class AvatarMenuState
    Inherits BaseMenuState
    Const ChooseAvatarText = "Choose Avatar..."
    Const ClearAvatarText = "Clear Avatar"
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
                   {
                    ChooseAvatarText,
                    ClearAvatarText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case ChooseAvatarText
                        setState(GameState.PickAvatarMap, False)
                    Case ClearAvatarText
                        Editor.ClearAvatar()
                End Select
            End Sub,
            Sub()
                setState(GameState.EditMenu, False)
            End Sub)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim avatar = Editor.Avatar
        If avatar IsNot Nothing Then
            Dim font = Fonts(GameFont.Font5x7)
            font.WriteText(displayBuffer, (0, font.Height * 5), $"Map Name: {avatar.MapName}", Hue.White)
            font.WriteText(displayBuffer, (0, font.Height * 6), $"Column: {avatar.Column}", Hue.White)
            font.WriteText(displayBuffer, (0, font.Height * 7), $"Row: {avatar.Row}", Hue.White)
            Dim creature As ICreatureInstance = avatar.Creature
            font.WriteText(displayBuffer, (0, font.Height * 8), $"Creature: {creature.Creature.Name}", Hue.White)
        End If
    End Sub
End Class
