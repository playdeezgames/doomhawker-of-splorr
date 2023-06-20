Friend Class BaseConfirmState
    Inherits BaseGameState(Of Integer, Command, Sfx, GameState)
    Private _confirmation As Boolean = False
    Private ReadOnly _caption As String
    Private ReadOnly _hue As Integer
    Private ReadOnly _onDone As Action(Of Boolean)
    Private ReadOnly _onCancel As Action
    Public Sub New(
                  parent As IGameController(Of Integer, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  caption As String,
                  hue As Integer,
                  onDone As Action(Of Boolean),
                  onCancel As Action)
        MyBase.New(parent, setState)
        _onDone = onDone
        _onCancel = onCancel
        _caption = caption
        _hue = hue
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased, Command.DownReleased
                _confirmation = Not _confirmation
            Case Command.CancelReleased
                _onCancel()
            Case Command.OkReleased
                _onDone(_confirmation)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Integer))
        displayBuffer.Fill((Zero, Zero), (ViewWidth, ViewHeight), Black)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (Zero, Zero), _caption, _hue)
        font.WriteText(displayBuffer, (Zero, font.Height), "No", If(_confirmation, Blue, LightBlue))
        font.WriteText(displayBuffer, (Zero, font.Height * 2), "Yes", If(_confirmation, LightBlue, Blue))
    End Sub
End Class
