﻿Public Class GameController
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
        SetState(GameState.PickFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Sub() SetCurrentState(GameState.FontsMenu),
                Sub(picked)
                    FontName = picked
                    SetCurrentState(GameState.EditFont)
                End Sub,
                Function() Editor.FontNames))
        SetState(GameState.NewFontName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Font Name:",
                Sub()
                    FontName = ""
                    SetCurrentState(GameState.FontsMenu)
                End Sub,
                Sub(buffer)
                    FontName = buffer
                    Editor.CreateFont(FontName, FontWidth, FontHeight)
                    SetCurrentState(GameState.EditFont)
                End Sub))
        SetState(GameState.EditFont, New EditFontState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditGlyph, New EditGlyphState(Me, AddressOf SetCurrentState))
        SetState(GameState.SaveAs, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Save Filename:",
                 Sub() SetCurrentState(GameState.MainMenu),
                 Sub(buffer)
                     Editor.Save(buffer)
                     SetCurrentState(GameState.MainMenu)
                 End Sub))
        SetState(GameState.LoadFrom, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Open Filename:",
                 Sub() SetCurrentState(GameState.MainMenu),
                 Sub(buffer)
                     Editor.Load(buffer)
                     SetCurrentState(GameState.EditMenu)
                 End Sub))
        SetState(GameState.TerrainsMenu, New TerrainsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewTerrainName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Terrain Name:",
                 Sub()
                     TerrainName = ""
                     SetCurrentState(GameState.TerrainsMenu)
                 End Sub,
                 Sub(buffer)
                     TerrainName = buffer
                     Editor.CreateTerrain(TerrainName)
                     SetCurrentState(GameState.EditTerrain)
                 End Sub))
        SetState(GameState.EditTerrain, New EditTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrainFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Sub() SetCurrentState(GameState.EditTerrain),
                Sub(picked)
                    Editor.GetTerrain(TerrainName).Font = Editor.GetFont(picked)
                    SetCurrentState(GameState.EditTerrain)
                End Sub,
                Function() Editor.FontNames))
        SetState(GameState.PickTerrainGlyph, New PickTerrainGlyphState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() SetCurrentState(GameState.TerrainsMenu),
                 Sub(picked)
                     TerrainName = picked
                     SetCurrentState(GameState.EditTerrain)
                 End Sub,
                 Function() Editor.TerrainNames))
        SetState(GameState.MapsMenu, New MapsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewMapName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Map Name:",
                 Sub() SetCurrentState(GameState.MapsMenu),
                 Sub(buffer)
                     MapName = buffer
                     SetCurrentState(GameState.NewMapSize)
                 End Sub))
        SetState(GameState.PickMap, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Map",
                 Sub() SetCurrentState(GameState.MapsMenu),
                 Sub(picked)
                     MapName = picked
                     SetCurrentState(GameState.EditMap)
                 End Sub,
                 Function() Editor.MapNames))
        SetState(GameState.NewMapSize, New NewMapSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.Settings, New SettingsState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditMap, New EditMapState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickMapTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() SetCurrentState(GameState.EditMap),
                 Sub(picked)
                     TerrainName = picked
                     SetCurrentState(GameState.PlaceMapTerrain)
                 End Sub,
                 Function() Editor.TerrainNames))
        SetState(GameState.PlaceMapTerrain, New PlaceMapTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickDefaultMapTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() SetCurrentState(GameState.NewMapSize),
                 Sub(picked)
                     TerrainName = picked
                     SetCurrentState(GameState.NewMapSize)
                 End Sub,
                 Function() Editor.TerrainNames))
        SetState(GameState.RenameMap, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "New Map Name:",
                 Sub()
                     SetCurrentState(GameState.EditMap)
                 End Sub,
                 Sub(buffer)
                     Editor.RenameMap(MapName, buffer)
                     MapName = buffer
                     SetCurrentState(GameState.EditMap)
                 End Sub))
        SetState(GameState.PickRenameFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font To Rename",
                 Sub()
                     SetCurrentState(GameState.FontsMenu)
                 End Sub,
                 Sub(picked)
                     FontName = picked
                     SetCurrentState(GameState.RenameFont)
                 End Sub,
                 Function() Editor.FontNames))
        SetState(GameState.RenameFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Rename Font To:",
                 Sub()
                     SetCurrentState(GameState.FontsMenu)
                 End Sub,
                 Sub(buffer)
                     Editor.RenameFont(FontName, buffer)
                     SetCurrentState(GameState.FontsMenu)
                 End Sub))
        SetState(GameState.PickCloneFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font To Clone",
                 Sub()
                     SetCurrentState(GameState.FontsMenu)
                 End Sub,
                 Sub(picked)
                     FontName = picked
                     SetCurrentState(GameState.CloneFont)
                 End Sub,
                 Function() Editor.FontNames))
        SetState(GameState.CloneFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Font Name:",
                 Sub()
                     SetCurrentState(GameState.FontsMenu)
                 End Sub,
                 Sub(buffer)
                     Editor.CloneFont(FontName, buffer)
                     FontName = buffer
                     SetCurrentState(GameState.EditFont)
                 End Sub))
    End Sub
End Class
