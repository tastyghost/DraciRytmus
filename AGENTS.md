# Dragon Rhythm – Codex Instructions

## Project overview

Dragon Rhythm is a portrait mobile Unity game for children aged approximately
3–6 years. It supports speech-therapy practice through syllable rhythmization.

The main character is a dark-blue dragon named Luna.

The basic exercise loop is:

1. Show a word card.
2. Play or allow playback of the word audio.
3. The child moves or selects the number of raspberries corresponding to the
   number of syllables.
4. The child confirms the answer.
5. A correct answer grants one energy.
6. Five energies complete the current location.
7. Luna travels on the map and unlocks a location or companion.

## Current locations

1. Dragon Nest
2. Flower Meadow
3. Wooden Bridge
4. Mysterious Stone
5. Whispering Forest
6. Glowing Dragon Egg

The final location uses an egg-hatching sequence instead of the standard
companion-unlock flow.

## Important game panels

- TitlePanel
- ExercisePanel
- SuccessPanel
- CompanionPanel
- MapPanel
- LocationIntroPanel
- CollectionPanel
- EggHatchingPanel

## Main systems

- Word-card loading from CSV
- Exercise and syllable-answer logic
- Energy progression
- Location unlocking
- Branching map movement
- Footprint/path history
- Companion collection
- Save and load
- Final egg-hatching sequence

## Development rules

- This is a beginner-maintained Unity project.
- Prefer clear, readable C# over advanced abstractions.
- Do not introduce dependency-injection frameworks or external packages
  unless explicitly requested.
- Do not rename public serialized fields without explaining the required
  Inspector changes.
- Preserve existing Inspector references whenever possible.
- Do not change scene or prefab YAML files unless the task explicitly requires it.
- Never delete existing functionality merely because it appears unused.
- Check for null references when objects may be optional.
- Prevent repeated button presses during animations.
- Keep mobile portrait layout in mind.
- Use Czech text for player-facing game content.
- Code comments and technical identifiers may be in English.
- Avoid changing unrelated scripts.
- Make the smallest reasonable change that completes the task.

## Unity Inspector requirements

When adding a new serialized reference:

1. Use [SerializeField] private rather than public where practical.
2. Explain exactly which GameObject or component must be assigned.
3. State where the field will appear in the Inspector.
4. Explain whether the object should initially be active or inactive.
5. Do not assume that Unity automatically assigns the reference.

## Save-system requirements

Existing player progress must remain compatible whenever reasonably possible.

Saved progress includes:

- current location
- energy count
- unlocked or collected companions
- visited paths and footprints
- location progress

When changing saved data:

- preserve old save fields when possible
- provide safe default values
- explain whether old saves must be reset

## Egg-hatching sequence

The final sequence contains:

1. intact egg
2. egg with one crack
3. egg with multiple cracks
4. breaking egg with shell pieces
5. newly hatched green baby dragon

The sequence should begin after the final required success at the Glowing
Dragon Egg location.

The standard companion panel should not interrupt this finale unless
explicitly required.

## Before editing

Before making a change:

1. Identify the scripts responsible for the feature.
2. Trace the current call flow.
3. Explain briefly what will be changed.
4. Check whether the change affects the Inspector, save system, map progression,
   or other panels.

## After editing

After making a change:

1. Summarize every modified file.
2. Describe the new behaviour.
3. List all required Unity Inspector steps.
4. Mention possible side effects.
5. Provide a short manual test procedure.
6. Report any compiler errors or tests that could not be run.

## Testing checklist

At minimum, verify logically that:

- a new game starts correctly
- Continue restores saved progress
- wrong answers do not grant energy
- correct answers grant exactly one energy
- five energies trigger progression only once
- rapid repeated button presses do not duplicate rewards
- companions unlock only at the correct location
- map buttons respect lock state
- footprints remain correct when arriving from different locations
- the egg finale starts only at the final location