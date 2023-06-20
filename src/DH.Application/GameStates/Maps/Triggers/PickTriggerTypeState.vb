Imports DH.Data

Friend Class PickTriggerTypeState
    Inherits BasePickState
    Const TeleportText = "Teleport"
    Private Shared ReadOnly table As New Dictionary(Of String, TriggerType) From
        {
            {TeleportText, TriggerType.Teleport}
        }
    Public Sub New(parent As IGameController(Of Integer, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            Function() table.Keys,
            Sub(picked)
                World.Maps.Retrieve(MapName).Triggers.Retrieve(TriggerName).TriggerType = table(picked)
                setState(GameState.EditTrigger, False)
            End Sub,
            Sub() setState(GameState.EditTrigger, False))
    End Sub
End Class
