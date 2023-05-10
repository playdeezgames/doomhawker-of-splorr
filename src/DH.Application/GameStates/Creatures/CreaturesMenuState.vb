Friend Class CreaturesMenuState
    Inherits BaseMenuState
    Const NewCreatureText = "New Creature..."
    Const PickCreatureText = "Pick Creature..."
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From {
                    NewCreatureText,
                    PickCreatureText
                   },
            Sub(menuItem)
                Select Case menuItem
                    Case NewCreatureText
                        setState(GameState.NewCreatureName, False)
                    Case PickCreatureText
                        If Editor.Items.HasAny Then
                            setState(GameState.PickCreature, False)
                        Else
                            setState(GameState.NewCreatureName, False)
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
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"Creature Count: {Editor.Creatures.Names.Count}", 15)
    End Sub
End Class
