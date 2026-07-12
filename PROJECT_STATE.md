# Current project state

Last updated: 2026-07-12

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
- The Title screen Exit button opens a modal confirmation popup. Cancel returns to the game and Exit closes the application or stops Editor Play Mode.
- Sound effects are implemented through a persistent automatic `AudioManager` using one `AudioSource` and `PlayOneShot()`:
  - `Pop` when a raspberry is added to the bowl.
  - `AntiPop` when a raspberry is manually removed from the bowl.
  - `Success` when the Success panel opens.

## Inspector/setup still required

- Assign the existing `eggHatchingText` TextMesh Pro component to the GameManager's **Egg Hatching Text** field. The serialized reference is currently empty.
- Confirm that the six egg-hatching sprites are ordered correctly in the GameManager Inspector.
- Check the runtime-generated Exit confirmation popup visually in portrait Play Mode.

## Known limitations / in progress

- Footprints/path dots are created during travel but are not saved or restored.
- Rapid map-location selection should still be tested carefully during Luna's movement.
- The final hatching screen has no separate post-finale navigation action yet.
- Unity Editor play testing and a full build verification are still required.

## Planned later

- Comprehension mini-games.
- Oromotor exercises.
- Companion lost-item events.
- Additional audio and music.
- User-facing volume settings.
