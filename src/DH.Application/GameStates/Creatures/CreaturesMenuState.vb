Friend Class CreaturesMenuState
    Inherits BaseMenuState
    Const NewCreatureText = "New Creature..."
    Const PickCreatureText = "Pick Creature..."
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState, New List(Of String) From {
                    NewCreatureText,
                    PickCreatureText
                   })
    End Sub

    Public Overrides Sub HandleMenuItem(menuItem As String)
        Select Case menuItem
            Case NewCreatureText
                SetState(GameState.NewCreatureName)
            Case PickCreatureText
                If Editor.Items.HasAny Then
                    SetState(GameState.PickCreature)
                Else
                    SetState(GameState.NewCreatureName)
                End If
        End Select
    End Sub
    Protected Overrides Sub HandleCancel()
        SetState(GameState.EditMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Creature Count: {Editor.Creatures.Names.Count}", Hue.White)
    End Sub
End Class
