# Current project state

Last updated: 2026-07-16

## Working

- Word cards load from CSV and display their matching images.
- The raspberry syllable exercise works, including adding and removing raspberries from the bowl.
- Correct answers grant exactly one energy and open the Success panel.
- Five energies advance the player from the Dragon Nest to the map or complete the current location.
- Location-specific backgrounds, introductions and companions work.
- Map locations unlock in pairs and completed locations become unavailable.
- Luna can travel along configured branching map paths and leave path dots.
- Companion unlocks and the Companion Collection are based on completed locations.
- The Collection can be opened from the Title screen or Map and its Back button returns to the correct source screen.
- Save and Continue preserve energy, location, map position, unlocked locations and completed locations.
- The final Dragon Egg location uses the egg-hatching sequence instead of the standard companion panel.
- Egg-hatching progress and completion are saved and restored.
- The final hatching stage displays the message introducing Ejoume.
- The egg image has a rocking animation through `EggRock`.
- The location companion on the Exercise panel plays `CompanionFlying` automatically and loops continuously while visible.
- The Title screen Exit button opens a modal confirmation popup. Cancel returns to the game and Exit closes the application or stops Editor Play Mode.
- Sound effects are implemented through a persistent automatic `AudioManager` using one `AudioSource` and `PlayOneShot()`:
  - `Pop` when a raspberry is added to the bowl.
  - `AntiPop` when a raspberry is manually removed from the bowl.
  - `Success` when the Success panel opens.
- Word-card audio is loaded from `Assets/Resources/Audio/Words` using the `AudioName` and `SyllablesAudioName` columns in `words.csv`:
  - The first click on a new word card plays the complete word.
  - The second click plays the syllabified word.
  - The third and every later click plays `Pop` once per syllable.
  - The click sequence resets whenever a new word is loaded.
  - Input is locked during playback so rapid clicks cannot overlap word or syllable-pop sequences.
- Location intro narration is loaded from `Assets/Resources/Audio/Location` for Meadow, Bridge, Stone, Forest and Egg intros.
- Word-card and location-intro voice clips use an 8x playback multiplier because the current source WAV files have very low recorded peaks. Raspberry and success effects keep their original volume.

## Inspector/setup still required

- Assign the existing `eggHatchingText` TextMesh Pro component to the GameManager's **Egg Hatching Text** field. The serialized reference is currently empty.
- Confirm that the six egg-hatching sprites are ordered correctly in the GameManager Inspector.
- Check the runtime-generated Exit confirmation popup visually in portrait Play Mode.

## Known limitations / in progress

- Footprints/path dots are created during travel but are not saved or restored.
- Rapid map-location selection should still be tested carefully during Luna's movement.
- The final hatching screen has no separate post-finale navigation action yet.
- Location intro audio names are currently mapped to the existing location order: Meadow, Bridge, Stone, Forest and Egg. Reordering or adding locations requires updating this mapping.
- Newly added voice recordings should have a similar source level to the current WAV files. Already-normalized loud files may distort with the current 8x voice multiplier.
- Unity Editor play testing and a full build verification are still required.

## Planned later

- Comprehension mini-games.
- Oromotor exercises.
- Companion lost-item events.
- Additional audio and music.
- User-facing volume settings.
