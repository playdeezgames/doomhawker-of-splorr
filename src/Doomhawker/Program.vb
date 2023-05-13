Module Program
    Const ConfigFileName = "config.json"
    Sub Main(args As String())
        Dim config = LoadConfig()
        Dim gameController As New GameController(
            Function() (config.WindowWidth, config.WindowHeight),
            Function() config.FullScreen,
            Function() config.SfxVolume,
            AddressOf SaveConfig)
        Using host As New Host(Of String, Command, Sfx)(
            "Doomhawker of SPLORR!!",
            gameController,
            (WorldContext.ViewWidth, WorldContext.ViewHeight),
            AddressOf BufferCreatorator,
            AddressOf CommandTransformerator,
            AddressOf GamePadTransformer,
            New Dictionary(Of Sfx, String) From
            {
            })
            host.Run()
        End Using
    End Sub

    Private Function GamePadTransformer(newState As GamePadState, oldState As GamePadState) As Command()
        Dim results As New HashSet(Of Command)
        If newState.DPad.Up = ButtonState.Released AndAlso oldState.DPad.Up = ButtonState.Pressed Then
            results.Add(Command.UpReleased)
        End If
        If newState.DPad.Down = ButtonState.Released AndAlso oldState.DPad.Down = ButtonState.Pressed Then
            results.Add(Command.DownReleased)
        End If
        If newState.DPad.Left = ButtonState.Released AndAlso oldState.DPad.Left = ButtonState.Pressed Then
            results.Add(Command.LeftReleased)
        End If
        If newState.DPad.Right = ButtonState.Released AndAlso oldState.DPad.Right = ButtonState.Pressed Then
            results.Add(Command.RightReleased)
        End If
        If newState.Buttons.A = ButtonState.Released AndAlso oldState.Buttons.A = ButtonState.Pressed Then
            results.Add(Command.OkReleased)
        End If
        If newState.Buttons.B = ButtonState.Released AndAlso oldState.Buttons.B = ButtonState.Pressed Then
            results.Add(Command.CancelReleased)
        End If
        Return results.ToArray
    End Function

    Private Sub SaveConfig(windowSize As (Integer, Integer), fullScreen As Boolean, volume As Single)
        File.WriteAllText(ConfigFileName, JsonSerializer.Serialize(New DHConfig With {.SfxVolume = volume, .WindowHeight = windowSize.Item2, .WindowWidth = windowSize.Item1, .FullScreen = fullScreen}))
    End Sub
    Const DefaultWindowScale = 6
    Const DefaultWindowWidth = ViewWidth * DefaultWindowScale
    Const DefaultWindowHeight = ViewHeight * DefaultWindowScale
    Const DefaultSfxVolume = 1.0F
    Private Function LoadConfig() As DHConfig
        Try
            Return JsonSerializer.Deserialize(Of DHConfig)(File.ReadAllText(ConfigFileName))
        Catch ex As Exception
            Return New DHConfig With
            {
                .WindowWidth = DefaultWindowWidth,
                .WindowHeight = DefaultWindowHeight,
                .SfxVolume = DefaultSfxVolume
            }
        End Try
    End Function

    Private ReadOnly keyTable As IReadOnlyDictionary(Of Keys, Command) =
        New Dictionary(Of Keys, Command) From
        {
            {Keys.Up, Command.UpReleased},
            {Keys.Right, Command.RightReleased},
            {Keys.Left, Command.LeftReleased},
            {Keys.Down, Command.DownReleased},
            {Keys.Space, Command.OkReleased},
            {Keys.Escape, Command.CancelReleased}
        }
    Private Function CommandTransformerator(key As Keys) As Command?
        If keyTable.ContainsKey(key) Then
            Return keyTable(key)
        End If
        Return Nothing
    End Function
    Private Function BufferCreatorator(texture As Texture2D) As IDisplayBuffer(Of String)
        Return New DisplayBuffer(Of String)(texture, AddressOf TransformHue)
    End Function
    Private ReadOnly hueTable As IReadOnlyDictionary(Of String, Color) =
        New Dictionary(Of String, Color) From
        {
            {Black, New Color(0, 0, 0, 255)},
            {Blue, New Color(0, 0, 170, 255)},
            {Green, New Color(0, 170, 0, 255)},
            {Cyan, New Color(0, 170, 170, 255)},
            {Red, New Color(170, 0, 0, 255)},
            {Magenta, New Color(170, 0, 170, 255)},
            {Brown, New Color(170, 85, 0, 255)},
            {Gray, New Color(170, 170, 170, 255)},
            {DarkGray, New Color(85, 85, 85, 255)},
            {LightBlue, New Color(85, 85, 255, 255)},
            {LightGreen, New Color(85, 255, 85, 255)},
            {LightCyan, New Color(85, 255, 255, 255)},
            {LightRed, New Color(255, 85, 85, 255)},
            {LightMagenta, New Color(255, 85, 255, 255)},
            {Yellow, New Color(255, 255, 85, 255)},
            {White, New Color(255, 255, 255, 255)}
        }
    Private Function TransformHue(hue As String) As Color
        If World.Colors.Names.Contains(hue) Then
            Dim editorColor = World.Colors.Retrieve(hue)
            Return New Color(editorColor.Red, editorColor.Green, editorColor.Blue)
        End If
        Return hueTable(hue)
    End Function
End Module
