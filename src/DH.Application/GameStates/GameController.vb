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
        SetState(GameState.EditItem, New EditItemState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewItemName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Item Name:",
                 Sub()
                     ItemName = ""
                     TransitionToState(GameState.ItemsMenu)
                 End Sub,
                 Sub(buffer)
                     ItemName = buffer
                     Editor.Items.Create(ItemName)
                     TransitionToState(GameState.EditItem)
                 End Sub))
        SetState(GameState.PickItem, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Item",
                 Sub() TransitionToState(GameState.ItemsMenu),
                 Sub(picked)
                     ItemName = picked
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Function() Editor.ItemNames))
        SetState(GameState.RenameItem, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "New Item Name:",
                         Sub()
                             TransitionToState(GameState.EditItem)
                         End Sub,
                         Sub(buffer)
                             Editor.RenameItem(ItemName, buffer)
                             ItemName = buffer
                             TransitionToState(GameState.EditItem)
                         End Sub))
        SetState(GameState.CloneItem, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Item Name:",
                 Sub()
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Sub(buffer)
                     Editor.CloneItem(ItemName, buffer)
                     ItemName = buffer
                     TransitionToState(GameState.EditItem)
                 End Sub))
        SetState(GameState.ConfirmDeleteItem, New BaseConfirmState(
                         Me,
                         AddressOf SetCurrentState,
                         "Confirm item deletion?",
                         Hue.Red,
                         Sub(confirmation)
                             If confirmation Then
                                 Editor.DeleteItem(ItemName)
                                 TransitionToState(GameState.ItemsMenu)
                                 Return
                             End If
                             TransitionToState(GameState.EditItem)
                         End Sub,
                         Sub()
                             TransitionToState(GameState.EditItem)
                         End Sub))
        SetState(GameState.PickItemFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Sub() TransitionToState(GameState.EditItem),
                Sub(picked)
                    Editor.GetItem(ItemName).Font = Editor.Fonts.Retrieve(picked)
                    TransitionToState(GameState.EditItem)
                End Sub,
                Function() Editor.Fonts.Names))
        SetState(GameState.PickItemGlyph, New BaseGlyphPickState(
                 Me,
                 AddressOf SetCurrentState,
                 Sub(glyph)
                     Editor.GetItem(ItemName).GlyphKey = glyph
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Function() Editor.GetItem(ItemName).FontName))
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
                 Function() Editor.Maps.Names))
        SetState(GameState.NewMapSize, New NewMapSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.CloneMap, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "Cloned Map Name:",
                         Sub()
                             TransitionToState(GameState.EditMap)
                         End Sub,
                         Sub(buffer)
                             Editor.Maps.Clone(MapName, buffer)
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
                 Function() Editor.Terrains.Names))
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
                 Function() Editor.Terrains.Names))
        SetState(GameState.RenameMap, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "New Map Name:",
                 Sub()
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Sub(buffer)
                     Editor.Maps.Rename(MapName, buffer)
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
                         Editor.Maps.Delete(MapName)
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
                             Editor.Terrains.Rename(TerrainName, buffer)
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
                     Editor.Terrains.Clone(TerrainName, buffer)
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
                                 Editor.Terrains.Delete(TerrainName)
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
                     Editor.Terrains.Create(TerrainName)
                     TransitionToState(GameState.EditTerrain)
                 End Sub))
        SetState(GameState.EditTerrain, New EditTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrainFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Sub() TransitionToState(GameState.EditTerrain),
                Sub(picked)
                    Editor.Terrains.Retrieve(TerrainName).Font = Editor.Fonts.Retrieve(picked)
                    TransitionToState(GameState.EditTerrain)
                End Sub,
                Function() Editor.Fonts.Names))
        SetState(GameState.PickTerrainGlyph, New BaseGlyphPickState(
                 Me,
                 AddressOf SetCurrentState,
                 Sub(glyph)
                     Editor.Terrains.Retrieve(TerrainName).GlyphKey = glyph
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Function() Editor.Terrains.Retrieve(TerrainName).FontName))
        SetState(GameState.PickTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Sub() TransitionToState(GameState.TerrainsMenu),
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Function() Editor.Terrains.Names))
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
                Function() Editor.Fonts.Names))
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
                    Editor.Fonts.Create(FontName, FontWidth, FontHeight)
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
                 Function() Editor.Fonts.Names))
        SetState(GameState.ConfirmDeleteFont, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm font deletion?",
                 Hue.Red,
                 Sub(confirmation)
                     If confirmation Then
                         Editor.Fonts.Delete(FontName)
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
                 Function() Editor.Fonts.Names))
        SetState(GameState.RenameFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Rename Font To:",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(buffer)
                     Editor.Fonts.Rename(FontName, buffer)
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
                 Function() Editor.Fonts.Names))
        SetState(GameState.CloneFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Font Name:",
                 Sub()
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub(buffer)
                     Editor.Fonts.Clone(FontName, buffer)
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
