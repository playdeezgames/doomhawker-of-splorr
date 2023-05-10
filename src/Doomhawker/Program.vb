Module Program
    Const ConfigFileName = "config.json"
    Sub Main(args As String())
        Dim config = LoadConfig()
        Dim gameController As New GameController(
            Function() (config.WindowWidth, config.WindowHeight),
            Function() config.SfxVolume,
            AddressOf SaveConfig)
        Using host As New Host(Of Integer, Command, Sfx)(
            "Doomhawker of SPLORR!!",
            gameController,
            (EditorContext.ViewWidth, EditorContext.ViewHeight),
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
        If newState.DPad.Up = ButtonState.Pressed AndAlso oldState.DPad.Up = ButtonState.Released Then
            results.Add(Command.UpPressed)
        End If
        If newState.DPad.Down = ButtonState.Pressed AndAlso oldState.DPad.Down = ButtonState.Released Then
            results.Add(Command.DownPressed)
        End If
        If newState.DPad.Left = ButtonState.Pressed AndAlso oldState.DPad.Left = ButtonState.Released Then
            results.Add(Command.LeftPressed)
        End If
        If newState.DPad.Right = ButtonState.Pressed AndAlso oldState.DPad.Right = ButtonState.Released Then
            results.Add(Command.RightPressed)
        End If
        If newState.Buttons.X = ButtonState.Pressed AndAlso oldState.Buttons.X = ButtonState.Released Then
            results.Add(Command.FirePressed)
        End If
        If newState.Buttons.Y = ButtonState.Pressed AndAlso oldState.Buttons.Y = ButtonState.Released Then
            results.Add(Command.FirePressed)
        End If
        If newState.Buttons.A = ButtonState.Pressed AndAlso oldState.Buttons.A = ButtonState.Released Then
            results.Add(Command.FirePressed)
        End If
        If newState.Buttons.B = ButtonState.Pressed AndAlso oldState.Buttons.B = ButtonState.Released Then
            results.Add(Command.FirePressed)
        End If

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
        If newState.Buttons.X = ButtonState.Released AndAlso oldState.Buttons.X = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        If newState.Buttons.Y = ButtonState.Released AndAlso oldState.Buttons.Y = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        If newState.Buttons.A = ButtonState.Released AndAlso oldState.Buttons.A = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        If newState.Buttons.B = ButtonState.Released AndAlso oldState.Buttons.B = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        Return results.ToArray
    End Function

    Private Sub SaveConfig(windowSize As (Integer, Integer), volume As Single)
        File.WriteAllText(ConfigFileName, JsonSerializer.Serialize(New DHConfig With {.SfxVolume = volume, .WindowHeight = windowSize.Item2, .WindowWidth = windowSize.Item1}))
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
            {Keys.Space, Command.FireReleased}
        }
    Private Function CommandTransformerator(key As Keys) As Command?
        If keyTable.ContainsKey(key) Then
            Return keyTable(key)
        End If
        Return Nothing
    End Function
    Private Function BufferCreatorator(texture As Texture2D) As IDisplayBuffer(Of Integer)
        Return New DisplayBuffer(Of Integer)(texture, AddressOf TransformHue)
    End Function
    Private ReadOnly hueTable As IReadOnlyDictionary(Of Integer, Color) =
        New Dictionary(Of Integer, Color) From
        {
            {0, New Color(0, 0, 0, 255)},
            {1, New Color(0, 0, 170, 255)},
            {2, New Color(0, 170, 0, 255)},
            {3, New Color(0, 170, 170, 255)},
            {4, New Color(170, 0, 0, 255)},
            {5, New Color(170, 0, 170, 255)},
            {6, New Color(170, 85, 0, 255)},
            {7, New Color(170, 170, 170, 255)},
            {8, New Color(85, 85, 85, 255)},
            {9, New Color(85, 85, 255, 255)},
            {10, New Color(85, 255, 85, 255)},
            {11, New Color(85, 255, 255, 255)},
            {12, New Color(255, 85, 85, 255)},
            {13, New Color(255, 85, 255, 255)},
            {14, New Color(255, 255, 85, 255)},
            {15, New Color(255, 255, 255, 255)}
        }
    Private Function TransformHue(hue As Integer) As Color
        Return hueTable(hue)
    End Function
End Module
