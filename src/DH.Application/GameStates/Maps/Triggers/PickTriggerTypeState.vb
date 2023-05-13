Imports DH.Data

Friend Class PickTriggerTypeState
    Inherits BasePickState
    Const TeleportText = "Teleport"
    Private Shared ReadOnly table As New Dictionary(Of String, TriggerType) From
        {
            {TeleportText, TriggerType.Teleport}
        }
    Public Sub New(parent As IGameController(Of String, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            Sub()
                setState(GameState.EditTrigger, False)
            End Sub,
            Sub(picked)
                World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).TriggerType = table(picked)
                setState(GameState.EditTrigger, False)
            End Sub,
            Function() table.Keys)
    End Sub
End Class
