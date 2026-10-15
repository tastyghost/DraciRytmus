# Current project state

Last updated: 2026-10-14

## Working

### Core exercise loop

- Word cards load from `Assets/Resources/words.csv` and display their matching images.
- The raspberry syllable exercise supports adding raspberries to and removing them from the bowl.
- Wrong answers do not grant energy.
- Correct answers grant exactly one energy and open the Success panel.
- Input is locked during answer feedback and audio playback to prevent duplicate rewards or overlapping actions.
- Five energies advance the player from the Dragon Nest to the map or complete the current location exactly once.

### Word-card audio

- Word-card audio is loaded from `Assets/Resources/Audio/Words` using the `AudioName` and `SyllablesAudioName` columns in `words.csv`.
- Clicking a word card alternates continuously between:
  1. the complete spoken word;
  2. the syllabified word.
- The former third-click sequence that played one `Pop` per syllable is no longer called by the word card.
- The click sequence resets to the complete word whenever a new word is loaded.
- Rapid clicks are ignored until the current clip finishes.
- The existing syllable-pop coroutine remains in `GameManager` but is currently not used by the word-card click flow.

### Map and progression

- Location-specific backgrounds, introductions and companions work.
- Map locations unlock in pairs and completed locations become unavailable.
- Luna can travel along configured branching paths and leave path dots.
- Map buttons respect the unlocked and completed state.
- Companion unlocks and the Companion Collection are based on completed locations.
- Opening the map from the bottom bar is navigation-only: it does not change energy, `currentLocationIndex`, unlocked locations or saved progress.

### Central UI navigation and bottom bar

- `GameManager` is the single central owner of the main panel state.
- `BottomMenuController` only forwards button clicks to these `GameManager` methods:
  - `OpenMapFromBottomBar()`
  - `OpenCollectionFromBottomBar()`
  - `OpenSettingsFromBottomBar()`
  - `OpenMenuFromBottomBar()`
- `BottomMenuController` does not directly activate or deactivate panels.
- `BottomBar` contains Map, Collection, Settings and Menu buttons.
- The bar is available on the Map, gameplay-opened Collection, Settings and completed egg-finale screens.
- The bar is hidden on the Title screen, during exercises, Success, Companion, Location Intro, map travel and egg-hatching steps before the baby dragon appears.
- Menu opens the existing `TitlePanel`. Continue restores the saved game state.
- `SettingsPanel` exists and is reachable through the central navigation. Its final settings controls are not implemented yet.

### Companion Collection and Back behavior

- The Collection can be opened from the Title screen, Map, Settings or completed egg finale.
- The Collection remembers its source for the current session.
- Its Back button returns to the correct source: Title, Map, Settings or the completed egg-finale screen.
- Opening the Collection from Title before starting a new game returns to Title and cannot skip the Dragon Nest exercise.

### Responsive portrait UI

- The Canvas uses `Scale With Screen Size` with a `1080 x 1920` reference resolution and width matching.
- `ResponsiveCanvas` expands configured full-screen backgrounds to cover taller phone displays while preserving each image's aspect ratio. Excess width is cropped on tall screens.
- `MapBackground` is deliberately excluded from responsive expansion and remains fixed at `1080 x 1920`, so manually positioned map locations, paths, locks, glows, footprints and Luna stay aligned with the map artwork.
- `MapBackdrop` uses `Assets/Art/Backgrounds/MapBackdrop.png`, sits behind `MapBackground`, and is included in the responsive background list to fill space on taller displays.
- `BottomBar` is anchored to the bottom edge and stretches across the screen width.

### Save and Continue

- Save and Continue preserve:
  - energy;
  - current location;
  - unlocked location count;
  - whether the player is currently inside a location;
  - whether the player has reached the map;
  - current map position;
  - completed locations and collected companions;
  - egg-hatching start, current step and completion.
- Recent UI and audio changes did not alter PlayerPrefs keys, so existing saves remain compatible.

### Final Dragon Egg location

- The final Dragon Egg location uses the egg-hatching sequence instead of the standard Companion panel.
- The sequence progresses through the configured egg sprites and ends with the newly hatched dragon.
- Egg-hatching progress and completion are saved and restored.
- The final stage displays the message introducing Ejoume.
- The egg image rocks through `EggRock`.
- `BottomBar` remains hidden during hatching and appears only after the baby dragon is shown.
- Continuing a save on the completed finale restores the final screen with the bottom bar visible.
- Opening Collection from the completed finale and pressing Back returns to the finale.

### Other presentation and audio

- The location companion on the Exercise panel plays `CompanionFlying` automatically and loops while visible.
- The Title screen Exit button opens a confirmation popup. Cancel returns to the game; Exit closes the application or stops Editor Play Mode.
- A persistent automatic `AudioManager` uses one `AudioSource` and `PlayOneShot()` for:
  - `Pop` when a raspberry is added to the bowl;
  - `AntiPop` when a raspberry is manually removed from the bowl;
  - `Success` when the Success panel opens;
  - word-card and location-intro voice clips.
- Location intro narration is loaded from `Assets/Resources/Audio/Location` for Meadow, Bridge, Stone, Forest and Egg intros.
- Word-card and location-intro voice clips currently use an 8x playback multiplier because the source WAV files have low recorded peaks. Raspberry and success effects retain their normal volume.

## Current scene and Inspector wiring

- `GameManager` has references to `SettingsPanel` and `BottomBar`.
- `BottomMenuController` has one reference to `GameManager` and no panel references.
- `eggHatchingText` is assigned in the scene.
- The six egg-hatching sprites should remain ordered from intact egg through the newly hatched dragon.
- `ResponsiveCanvas.Full Screen Backgrounds` contains the regular full-screen backgrounds plus `MapBackdrop`; it must not contain `MapBackground`.
- `MapBackdrop` must remain the first child of `MapPanel`, behind the fixed `MapBackground` and all interactive map elements.
- The Collection Back button calls `GameManager.CloseCompanionCollection()`.

## Known limitations / verification still required

- Footprints/path dots are created during travel but are not saved or restored.
- Rapid map-location selection should still be tested carefully during Luna's movement.
- `SettingsPanel` is currently a navigation destination without completed user-facing settings controls.
- The bottom bar is anchored to the physical bottom of the Canvas; explicit safe-area padding for gesture bars and display cutouts has not yet been implemented.
- Location intro audio names are mapped to the current location order: Meadow, Bridge, Stone, Forest and Egg. Reordering or adding locations requires updating this mapping.
- Newly added voice recordings should have a similar source level to the existing WAV files. Already-normalized loud files may distort with the current 8x voice multiplier.
- Portrait layouts should be visually checked at least at `1080 x 1920`, `1080 x 2340` and `1080 x 2400`.
- The complete flow still needs final device/build verification, including New Game, Continue, wrong/correct answers, five-energy progression, all bottom-bar destinations, Collection Back destinations, map paths and the egg finale.

## Planned later

- User-facing volume settings.
- Safe-area handling for the bottom navigation controls.
- Saving and restoring footprint/path-dot history.
- Comprehension mini-games.
- Oromotor exercises.
- Companion lost-item events.
- Additional audio and music.
