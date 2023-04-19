Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        Initialize()
        SetState(GameState.Title, New TitleState(Me, AddressOf SetCurrentState))
        SetState(GameState.MainMenu, New MainMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.ConfirmQuit, New ConfirmQuitState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditMenu, New EditMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.FontsMenu, New FontsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewFontSize, New NewFontSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickFont, New PickFontState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewFontName, New NewFontNameState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditFont, New EditFontState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditGlyph, New EditGlyphState(Me, AddressOf SetCurrentState))
        SetState(GameState.SaveAs, New SaveAsState(Me, AddressOf SetCurrentState))
        SetState(GameState.LoadFrom, New LoadFromState(Me, AddressOf SetCurrentState))
        SetState(GameState.TerrainsMenu, New TerrainsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewTerrainName, New NewTerrainNameState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditTerrain, New EditTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrainFont, New PickTerrainFontState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrainGlyph, New PickTerrainGlyph(Me, AddressOf SetCurrentState))
    End Sub
End Class
