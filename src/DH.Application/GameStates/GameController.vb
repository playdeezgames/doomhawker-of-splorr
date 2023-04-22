Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        Initialize()
        SetBoilerplateStates()
        SetFileStates()
        SetFontStates()
        SetTerrainStates()
        SetItemStates()
        SetMapStates()
        SetCurrentState(GameState.Title, True)
    End Sub

    Private Sub SetItemStates()
        SetState(GameState.ItemsMenu, New ItemsMenuState(Me, AddressOf SetCurrentState))
    End Sub

    Private Sub SetBoilerplateStates()
        SetState(GameState.Title, New TitleState(Me, AddressOf SetCurrentState))
        SetState(GameState.MainMenu, New MainMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.ConfirmQuit, New ConfirmQuitState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditMenu, New EditMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.Settings, New SettingsState(Me, AddressOf SetCurrentState))
        SetState(GameState.Messages, New MessagesState(Me, AddressOf SetCurrentState))
    End Sub

    Private Sub SetFileStates()
        SetState(GameState.SaveAs, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Save Filename:",
                 Sub() TransitionToState(GameState.MainMenu),
                 Sub(buffer)
                     Editor.Save(buffer)
                     Messages.Enqueue(New EditorMessage With {
                     .Lines = New List(Of (Hue, String)) From
                     {
                        (Hue.Green, "You saved it!")
                     }})
                     SetStates(GameState.Messages, GameState.MainMenu)
                 End Sub))
        SetState(GameState.LoadFrom, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Open Filename:",
                 Sub() TransitionToState(GameState.MainMenu),
                 Sub(buffer)
                     Try
                         Editor.Load(buffer)
                         TransitionToState(GameState.EditMenu)
                     Catch ex As Exception
                         Messages.Enqueue(New EditorMessage With {
                         .Lines = New List(Of (Hue, String)) From
                         {
                            (Hue.Red, "Failed to load!")
                         }})
                         SetStates(GameState.Messages, GameState.MainMenu)
                     End Try
                 End Sub))
    End Sub

    Private Sub SetMapStates()
        SetState(GameState.EditMap, New EditMapState(Me, AddressOf SetCurrentState))
        SetState(GameState.MapsMenu, New MapsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewMapName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Map Name:",
                 Sub() TransitionToState(GameState.MapsMenu),
                 Sub(buffer)
                     MapName = buffer
                     TransitionToState(GameState.NewMapSize)
                 End Sub))
        SetState(GameState.PickMap, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Map",
                 Sub() TransitionToState(GameState.MapsMenu),
                 Sub(picked)
                     MapName = picked
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Function() Editor.MapNames))
        SetState(GameState.NewMapSize, New NewMapSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.CloneMap, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "Cloned Map Name:",
                         Sub()
                             TransitionToState(GameState.EditMap)
                         End Sub,
                         Sub(buffer)
                             Editor.CloneMap(MapName, buffer)
                             MapName = buffer
                             TransitionToState(GameState.EditMap)
                         End Sub))
        SetState(GameState.PickMapTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() TransitionToState(GameState.EditMap),
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.PlaceMapTerrain)
                 End Sub,
                 Function() Editor.TerrainNames))
        SetState(GameState.PlaceMapTerrain, New PlaceMapTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickDefaultMapTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() TransitionToState(GameState.NewMapSize),
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.NewMapSize)
                 End Sub,
                 Function() Editor.TerrainNames))
        SetState(GameState.RenameMap, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "New Map Name:",
                 Sub()
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Sub(buffer)
                     Editor.RenameMap(MapName, buffer)
                     MapName = buffer
                     TransitionToState(GameState.EditMap)
                 End Sub))
        SetState(GameState.ConfirmDeleteMap, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm map deletion?",
                 Hue.Red,
                 Sub(confirmation)
                     If confirmation Then
                         Editor.DeleteMap(MapName)
                         TransitionToState(GameState.MapsMenu)
                         Return
                     End If
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditMap)
                 End Sub))
    End Sub

    Private Sub SetTerrainStates()
        SetState(GameState.RenameTerrain, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "New Terrain Name:",
                         Sub()
                             TransitionToState(GameState.EditTerrain)
                         End Sub,
                         Sub(buffer)
                             Editor.RenameTerrain(TerrainName, buffer)
                             TerrainName = buffer
                             TransitionToState(GameState.EditTerrain)
                         End Sub))
        SetState(GameState.CloneTerrain, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Terrain Name:",
                 Sub()
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Sub(buffer)
                     Editor.CloneTerrain(TerrainName, buffer)
                     TerrainName = buffer
                     TransitionToState(GameState.EditTerrain)
                 End Sub))
        SetState(GameState.ConfirmDeleteTerrain, New BaseConfirmState(
                         Me,
                         AddressOf SetCurrentState,
                         "Confirm terrain deletion?",
                         Hue.Red,
                         Sub(confirmation)
                             If confirmation Then
                                 Editor.DeleteTerrain(TerrainName)
                                 TransitionToState(GameState.TerrainsMenu)
                                 Return
                             End If
                             TransitionToState(GameState.EditTerrain)
                         End Sub,
                         Sub()
                             TransitionToState(GameState.EditTerrain)
                         End Sub))
        SetState(GameState.TerrainsMenu, New TerrainsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewTerrainName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Terrain Name:",
                 Sub()
                     TerrainName = ""
                     TransitionToState(GameState.TerrainsMenu)
                 End Sub,
                 Sub(buffer)
                     TerrainName = buffer
                     Editor.CreateTerrain(TerrainName)
                     TransitionToState(GameState.EditTerrain)
                 End Sub))
        SetState(GameState.EditTerrain, New EditTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrainFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Sub() TransitionToState(GameState.EditTerrain),
                Sub(picked)
                    Editor.GetTerrain(TerrainName).Font = Editor.GetFont(picked)
                    TransitionToState(GameState.EditTerrain)
                End Sub,
                Function() Editor.FontNames))
        SetState(GameState.PickTerrainGlyph, New PickTerrainGlyphState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() TransitionToState(GameState.TerrainsMenu),
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Function() Editor.TerrainNames))
    End Sub

    Private Sub SetFontStates()
        SetState(GameState.FontsMenu, New FontsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewFontSize, New NewFontSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Sub() TransitionToState(GameState.FontsMenu),
                Sub(picked)
                    FontName = picked
                    TransitionToState(GameState.EditFont)
                End Sub,
                Function() Editor.FontNames))
        SetState(GameState.NewFontName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Font Name:",
                Sub()
                    FontName = ""
                    TransitionToState(GameState.FontsMenu)
                End Sub,
                Sub(buffer)
                    FontName = buffer
                    Editor.CreateFont(FontName, FontWidth, FontHeight)
                    TransitionToState(GameState.EditFont)
                End Sub))
        SetState(GameState.EditFont, New EditFontState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditGlyph, New EditGlyphState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickDeleteFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font to Delete",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(picked)
                     FontName = picked
                     TransitionToState(GameState.ConfirmDeleteFont)
                 End Sub,
                 Function() Editor.FontNames))
        SetState(GameState.ConfirmDeleteFont, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm font deletion?",
                 Hue.Red,
                 Sub(confirmation)
                     If confirmation Then
                         Editor.DeleteFont(FontName)
                     End If
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub))
        SetState(GameState.PickRenameFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font To Rename",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(picked)
                     FontName = picked
                     TransitionToState(GameState.RenameFont)
                 End Sub,
                 Function() Editor.FontNames))
        SetState(GameState.RenameFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Rename Font To:",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(buffer)
                     Editor.RenameFont(FontName, buffer)
                     TransitionToState(GameState.FontsMenu)
                 End Sub))
        SetState(GameState.PickCloneFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font To Clone",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(picked)
                     FontName = picked
                     TransitionToState(GameState.CloneFont)
                 End Sub,
                 Function() Editor.FontNames))
        SetState(GameState.CloneFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Font Name:",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(buffer)
                     Editor.CloneFont(FontName, buffer)
                     FontName = buffer
                     TransitionToState(GameState.EditFont)
                 End Sub))
    End Sub

    Private Sub TransitionToState(nextState As GameState)
        SetCurrentState(nextState, False)
    End Sub

    Private Sub SetStates(pushedState As GameState, nextState As GameState)
        TransitionToState(nextState)
        SetCurrentState(pushedState, True)
    End Sub
End Class
