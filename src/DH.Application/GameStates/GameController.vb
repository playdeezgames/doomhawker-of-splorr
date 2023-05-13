Imports DH.Data

Public Class GameController
    Inherits BaseGameController(Of String, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Boolean, Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), fullScreenSource As Func(Of Boolean), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Boolean, Single))
        MyBase.New(windowSizeSource(), fullScreenSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, FullScreen, Volume)
        Initialize()
        SetBoilerplateStates()
        SetFileStates()
        SetColorStates()
        SetFontStates()
        SetTerrainStates()
        SetItemStates()
        SetCreatureStates()
        SetMapStates()
        SetAvatarStates()
        SetInPlayStates()
        SetCurrentState(GameState.Title, True)
    End Sub

    Private Sub SetColorStates()
        SetState(GameState.ColorsMenu, New ColorsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewColorName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Color Name:",
                Sub(buffer)
                    ColorName = buffer
                    World.Colors.Create(ColorName, 0, 0, 0)
                    TransitionToState(GameState.EditColor)
                End Sub,
                Sub()
                    ColorName = ""
                    TransitionToState(GameState.ColorsMenu)
                End Sub))
        SetState(GameState.PickColor, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Color",
                Function() World.Colors.Names,
                Sub(picked)
                    ColorName = picked
                    TransitionToState(GameState.EditColor)
                End Sub,
                Sub() TransitionToState(GameState.ColorsMenu)))
        SetState(GameState.EditColor, New EditColorState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickRenameColor, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Color To Rename",
                 Function() World.Colors.Names,
                 Sub(picked)
                     ColorName = picked
                     TransitionToState(GameState.RenameColor)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.ColorsMenu)
                 End Sub))
        SetState(GameState.RenameColor, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Rename Color To:",
                 Sub(buffer)
                     World.Colors.Rename(ColorName, buffer)
                     TransitionToState(GameState.ColorsMenu)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.ColorsMenu)
                 End Sub))
        SetState(GameState.PickDeleteColor, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Color to Delete",
                 Function() World.Colors.Names,
                 Sub(picked)
                     ColorName = picked
                     TransitionToState(GameState.ConfirmDeleteColor)
                 End Sub,
                 Sub() TransitionToState(GameState.ColorsMenu)))
        SetState(GameState.ConfirmDeleteColor, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm Color Deletion?",
                 Red,
                 Sub(confirmation)
                     If confirmation Then
                         World.Colors.Delete(ColorName)
                     End If
                     TransitionToState(GameState.ColorsMenu)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.ColorsMenu)
                 End Sub))
        SetState(GameState.PickCloneColor, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Color To Clone",
                 Function() World.Colors.Names,
                 Sub(picked)
                     ColorName = picked
                     TransitionToState(GameState.CloneColor)
                 End Sub,
                 Sub() TransitionToState(GameState.ColorsMenu)))
        SetState(GameState.CloneColor, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Color Name:",
                 Sub(buffer)
                     World.Colors.Clone(ColorName, buffer)
                     ColorName = buffer
                     TransitionToState(GameState.EditColor)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.ColorsMenu)
                 End Sub))
        SetState(GameState.ChangeHue, New ChangeHueState(Me, AddressOf SetCurrentState))
    End Sub

    Private Sub SetInPlayStates()
        SetState(GameState.Navigate, New NavigateState(Me, AddressOf SetCurrentState))
        SetState(GameState.GameMenu, New GameMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.ConfirmAbandon, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm Abandon?",
                 Red,
                 Sub(confirm)
                     If confirm Then
                         SetCurrentState(Nothing, False)
                         SetCurrentState(GameState.MainMenu, False)
                     Else
                         SetCurrentState(GameState.GameMenu, False)
                     End If
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.GameMenu, False)
                 End Sub))
    End Sub

    Private Sub SetAvatarStates()
        SetState(GameState.AvatarMenu, New AvatarMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickAvatarMap, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Avatar Map",
                 Function() World.Maps.Names,
                 Sub(picked)
                     MapName = picked
                     SetCurrentState(GameState.PickAvatarCreatureInstance, False)
                 End Sub,
                 Sub() SetCurrentState(GameState.AvatarMenu, False)))
        SetState(GameState.PickAvatarCreatureInstance, New PickAvatarCreatureInstanceState(
                 Me,
                 AddressOf SetCurrentState))
    End Sub

    Private Sub SetCreatureStates()
        SetState(GameState.CreaturesMenu, New CreaturesMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditCreature, New EditCreatureState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewCreatureName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Creature Name:",
                 Sub(buffer)
                     CreatureName = buffer
                     World.Creatures.Create(CreatureName, Black)
                     TransitionToState(GameState.EditCreature)
                 End Sub,
                 Sub()
                     CreatureName = ""
                     TransitionToState(GameState.CreaturesMenu)
                 End Sub))
        SetState(GameState.PickCreature, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Creature",
                 Function() World.Creatures.Names,
                 Sub(picked)
                     CreatureName = picked
                     TransitionToState(GameState.EditCreature)
                 End Sub,
                 Sub() TransitionToState(GameState.CreaturesMenu)))
        SetState(GameState.RenameCreature, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "New Creature Name:",
                         Sub(buffer)
                             World.Creatures.Rename(CreatureName, buffer)
                             CreatureName = buffer
                             TransitionToState(GameState.EditCreature)
                         End Sub,
                         Sub()
                             TransitionToState(GameState.EditCreature)
                         End Sub))
        SetState(GameState.CloneCreature, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Creature Name:",
                 Sub(buffer)
                     World.Creatures.Clone(CreatureName, buffer)
                     CreatureName = buffer
                     TransitionToState(GameState.EditCreature)
                 End Sub,
                 Sub() TransitionToState(GameState.EditCreature)))
        SetState(GameState.ConfirmDeleteCreature, New BaseConfirmState(
                         Me,
                         AddressOf SetCurrentState,
                         "Confirm creature deletion?",
                         Red,
                         Sub(confirmation)
                             If confirmation Then
                                 World.Creatures.Delete(CreatureName)
                                 TransitionToState(GameState.CreaturesMenu)
                                 Return
                             End If
                             TransitionToState(GameState.EditCreature)
                         End Sub,
                         Sub()
                             TransitionToState(GameState.EditCreature)
                         End Sub))
        SetState(GameState.PickCreatureFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Function() World.Fonts.Names,
                Sub(picked)
                    World.Creatures.Retrieve(CreatureName).Font = World.Fonts.Retrieve(picked)
                    TransitionToState(GameState.EditCreature)
                End Sub,
                Sub() TransitionToState(GameState.EditCreature)))
        SetState(GameState.PickCreatureGlyph, New BaseGlyphPickState(
                 Me,
                 AddressOf SetCurrentState,
                 Sub(glyph)
                     World.Creatures.Retrieve(CreatureName).GlyphKey = glyph
                     TransitionToState(GameState.EditCreature)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditCreature)
                 End Sub,
                 Function() World.Creatures.Retrieve(CreatureName).FontName))
    End Sub

    Private Sub SetItemStates()
        SetState(GameState.ItemsMenu, New ItemsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditItem, New EditItemState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewItemName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Item Name:",
                 Sub(buffer)
                     ItemName = buffer
                     World.Items.Create(ItemName, Black)
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Sub()
                     ItemName = ""
                     TransitionToState(GameState.ItemsMenu)
                 End Sub))
        SetState(GameState.PickItem, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Item",
                 Function() World.Items.Names,
                 Sub(picked)
                     ItemName = picked
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Sub() TransitionToState(GameState.ItemsMenu)))
        SetState(GameState.RenameItem, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "New Item Name:",
                         Sub(buffer)
                             World.Items.Rename(ItemName, buffer)
                             ItemName = buffer
                             TransitionToState(GameState.EditItem)
                         End Sub,
                         Sub()
                             TransitionToState(GameState.EditItem)
                         End Sub))
        SetState(GameState.CloneItem, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Item Name:",
                 Sub(buffer)
                     World.Items.Clone(ItemName, buffer)
                     ItemName = buffer
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditItem)
                 End Sub))
        SetState(GameState.ConfirmDeleteItem, New BaseConfirmState(
                         Me,
                         AddressOf SetCurrentState,
                         "Confirm item deletion?",
                         Red,
                         Sub(confirmation)
                             If confirmation Then
                                 World.Items.Delete(ItemName)
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
                Function() World.Fonts.Names,
                Sub(picked)
                    World.Items.Retrieve(ItemName).Font = World.Fonts.Retrieve(picked)
                    TransitionToState(GameState.EditItem)
                End Sub,
                Sub() TransitionToState(GameState.EditItem)))
        SetState(GameState.PickItemGlyph, New BaseGlyphPickState(
                 Me,
                 AddressOf SetCurrentState,
                 Sub(glyph)
                     World.Items.Retrieve(ItemName).GlyphKey = glyph
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditItem)
                 End Sub,
                 Function() World.Items.Retrieve(ItemName).FontName))
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
                 Sub(buffer)
                     World.Save(buffer)
                     Messages.Enqueue(New EditorMessage With {
                     .Lines = New List(Of (String, String)) From
                     {
                        (Green, "You saved it!")
                     }})
                     SetStates(GameState.Messages, GameState.MainMenu)
                 End Sub,
                 Sub() TransitionToState(GameState.MainMenu)))
        SetState(GameState.LoadFrom, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Open Filename:",
                 Sub(buffer)
                     Try
                         World.Load(buffer)
                         TransitionToState(GameState.MainMenu)
                     Catch ex As Exception
                         Messages.Enqueue(New EditorMessage With {
                         .Lines = New List(Of (String, String)) From
                         {
                            (Red, "Failed to load!")
                         }})
                         SetStates(GameState.Messages, GameState.MainMenu)
                     End Try
                 End Sub,
                 Sub() TransitionToState(GameState.MainMenu)))
    End Sub

    Private Sub SetMapStates()
        SetState(GameState.EditMap, New EditMapState(Me, AddressOf SetCurrentState))
        SetState(GameState.MapsMenu, New MapsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewMapName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Map Name:",
                 Sub(buffer)
                     MapName = buffer
                     TransitionToState(GameState.NewMapSize)
                 End Sub,
                 Sub() TransitionToState(GameState.MapsMenu)))
        SetState(GameState.PickMap, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Map",
                 Function() World.Maps.Names,
                 Sub(picked)
                     MapName = picked
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Sub() TransitionToState(GameState.MapsMenu)))
        SetState(GameState.NewMapSize, New NewMapSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.CloneMap, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "Cloned Map Name:",
                         Sub(buffer)
                             World.Maps.Clone(MapName, buffer)
                             MapName = buffer
                             TransitionToState(GameState.EditMap)
                         End Sub,
                         Sub() TransitionToState(GameState.EditMap)))
        SetState(GameState.PickMapTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Function() World.Terrains.Names,
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.PlaceMapTerrain)
                 End Sub,
                 Sub() TransitionToState(GameState.EditMap)))
        SetState(GameState.PickMapItem, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Item",
                 Function() World.Items.Names,
                 Sub(picked)
                     ItemName = picked
                     TransitionToState(GameState.PlaceMapItem)
                 End Sub,
                 Sub() TransitionToState(GameState.EditMap)))
        SetState(GameState.PickMapCreature, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Creature",
                 Function() World.Creatures.Names,
                 Sub(picked)
                     CreatureName = picked
                     TransitionToState(GameState.PlaceMapCreature)
                 End Sub,
                 Sub() TransitionToState(GameState.EditMap)))
        SetState(GameState.PlaceMapTerrain, New PlaceMapTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PlaceMapItem, New PlaceMapItemState(Me, AddressOf SetCurrentState))
        SetState(GameState.PlaceMapCreature, New PlaceMapCreatureState(Me, AddressOf SetCurrentState))
        SetState(GameState.RemoveMapItem, New RemoveMapItemState(Me, AddressOf SetCurrentState))
        SetState(GameState.RemoveMapCreature, New RemoveMapCreatureState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickDefaultMapTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Function() World.Terrains.Names,
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.NewMapSize)
                 End Sub,
                 Sub() TransitionToState(GameState.NewMapSize)))
        SetState(GameState.RenameMap, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "New Map Name:",
                 Sub(buffer)
                     World.Maps.Rename(MapName, buffer)
                     MapName = buffer
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Sub() TransitionToState(GameState.EditMap)))
        SetState(GameState.ConfirmDeleteMap, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm map deletion?",
                 Red,
                 Sub(confirmation)
                     If confirmation Then
                         World.Maps.Delete(MapName)
                         TransitionToState(GameState.MapsMenu)
                         Return
                     End If
                     TransitionToState(GameState.EditMap)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditMap)
                 End Sub))
        SetTriggerStates()
    End Sub

    Private Sub SetTriggerStates()
        SetState(GameState.EditTriggers, New EditTriggersState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewTriggerName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "New Trigger Name:",
                 Sub(name)
                     World.Maps.Retrieve(MapName).Triggers.Create(name, TriggerType.Teleport)
                     TriggerName = name
                     TransitionToState(GameState.EditTrigger)
                 End Sub,
                 Sub() TransitionToState(GameState.EditTriggers)))
        SetState(GameState.EditTrigger, New EditTriggerState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTriggerType, New PickTriggerTypeState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTrigger, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Trigger",
                 Function() World.Maps.Retrieve(MapName).Triggers.Names,
                 Sub(picked)
                     TriggerName = picked
                     TransitionToState(GameState.EditTrigger)
                 End Sub,
                 Sub() TransitionToState(GameState.EditTriggers)))
        SetState(GameState.RenameTrigger, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "New Trigger Name:",
                         Sub(buffer)
                             World.Maps.Retrieve(MapName).Triggers.Rename(TriggerName, buffer)
                             TriggerName = buffer
                             TransitionToState(GameState.EditTrigger)
                         End Sub,
                         Sub()
                             TransitionToState(GameState.EditTrigger)
                         End Sub))
    End Sub

    Private Sub SetTerrainStates()
        SetState(GameState.RenameTerrain, New BaseInputState(
                         Me,
                         AddressOf SetCurrentState,
                         "New Terrain Name:",
                         Sub(buffer)
                             World.Terrains.Rename(TerrainName, buffer)
                             TerrainName = buffer
                             TransitionToState(GameState.EditTerrain)
                         End Sub,
                         Sub() TransitionToState(GameState.EditTerrain)))
        SetState(GameState.CloneTerrain, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Terrain Name:",
                 Sub(buffer)
                     World.Terrains.Clone(TerrainName, buffer)
                     TerrainName = buffer
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Sub() TransitionToState(GameState.EditTerrain)))
        SetState(GameState.ConfirmDeleteTerrain, New BaseConfirmState(
                         Me,
                         AddressOf SetCurrentState,
                         "Confirm terrain deletion?",
                         Red,
                         Sub(confirmation)
                             If confirmation Then
                                 World.Terrains.Delete(TerrainName)
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
                 Sub(buffer)
                     TerrainName = buffer
                     World.Terrains.Create(TerrainName, Black)
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Sub()
                     TerrainName = ""
                     TransitionToState(GameState.TerrainsMenu)
                 End Sub))
        SetState(GameState.EditTerrain, New EditTerrainState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickTerrainFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Function() World.Fonts.Names,
                Sub(picked)
                    World.Terrains.Retrieve(TerrainName).Font = World.Fonts.Retrieve(picked)
                    TransitionToState(GameState.EditTerrain)
                End Sub,
                Sub() TransitionToState(GameState.EditTerrain)))
        SetState(GameState.PickTerrainGlyph, New BaseGlyphPickState(
                 Me,
                 AddressOf SetCurrentState,
                 Sub(glyph)
                     World.Terrains.Retrieve(TerrainName).GlyphKey = glyph
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Sub()
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Function() World.Terrains.Retrieve(TerrainName).FontName))
        SetState(GameState.PickTerrain, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Terrain",
                 Function() World.Terrains.Names,
                 Sub(picked)
                     TerrainName = picked
                     TransitionToState(GameState.EditTerrain)
                 End Sub,
                 Sub() TransitionToState(GameState.TerrainsMenu)))
    End Sub

    Private Sub SetFontStates()
        SetState(GameState.FontsMenu, New FontsMenuState(Me, AddressOf SetCurrentState))
        SetState(GameState.NewFontSize, New NewFontSizeState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Font",
                Function() World.Fonts.Names,
                Sub(picked)
                    FontName = picked
                    TransitionToState(GameState.EditFont)
                End Sub,
                Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.PickExportFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                "Choose Export Font",
                Function() World.Fonts.Names,
                Sub(picked)
                    FontName = picked
                    TransitionToState(GameState.ExportFontAs)
                End Sub,
                Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.NewFontName, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Font Name:",
                Sub(buffer)
                    FontName = buffer
                    World.Fonts.Create(FontName, FontWidth, FontHeight)
                    TransitionToState(GameState.EditFont)
                End Sub,
                Sub()
                    FontName = ""
                    TransitionToState(GameState.FontsMenu)
                End Sub))
        SetState(GameState.EditFont, New EditFontState(Me, AddressOf SetCurrentState))
        SetState(GameState.EditGlyph, New EditGlyphState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickDeleteFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font to Delete",
                 Function() World.Fonts.Names,
                 Sub(picked)
                     FontName = picked
                     TransitionToState(GameState.ConfirmDeleteFont)
                 End Sub,
                 Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.ConfirmDeleteFont, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm font deletion?",
                 Red,
                 Sub(confirmation)
                     If confirmation Then
                         World.Fonts.Delete(FontName)
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
                 Function() World.Fonts.Names,
                 Sub(picked)
                     FontName = picked
                     TransitionToState(GameState.RenameFont)
                 End Sub,
                 Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.RenameFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Rename Font To:",
                 Sub(buffer)
                     World.Fonts.Rename(FontName, buffer)
                     TransitionToState(GameState.FontsMenu)
                 End Sub,
                 Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.PickCloneFont, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Pick Font To Clone",
                 Function() World.Fonts.Names,
                 Sub(picked)
                     FontName = picked
                     TransitionToState(GameState.CloneFont)
                 End Sub,
                 Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.CloneFont, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Cloned Font Name:",
                 Sub(buffer)
                     World.Fonts.Clone(FontName, buffer)
                     FontName = buffer
                     TransitionToState(GameState.EditFont)
                 End Sub,
                 Sub() TransitionToState(GameState.FontsMenu)))
        SetState(GameState.ExportFontAs, New BaseInputState(
                 Me,
                 AddressOf SetCurrentState,
                 "Export Filename:",
                 Sub(buffer)
                     World.Fonts.Export(FontName, buffer)
                     Messages.Enqueue(New EditorMessage With {
                     .Lines = New List(Of (String, String)) From
                     {
                        (Green, "You exported it!")
                     }})
                     SetStates(GameState.Messages, GameState.FontsMenu)
                 End Sub,
                 Sub() TransitionToState(GameState.FontsMenu)))
    End Sub

    Private Sub TransitionToState(nextState As GameState)
        SetCurrentState(nextState, False)
    End Sub

    Private Sub SetStates(pushedState As GameState, nextState As GameState)
        TransitionToState(nextState)
        SetCurrentState(pushedState, True)
    End Sub
End Class
