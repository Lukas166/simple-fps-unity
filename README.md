# Shooting Target Game (Unity)

## Overview

This project is a simple FPS-style shooting game built with Unity. The player shoots randomly spawned targets while progressing through stages. Each stage increases the difficulty by spawning more targets and reducing the probability of larger targets appearing.

The game tracks score and stage progression, and stores the highest score and stage locally using Unity PlayerPrefs.

## Features

* FPS-style shooting using raycast
* Random target spawning in a defined 3D area
* Targets spawn above a defined spawn center with a maximum height limit
* Randomized target scale (normal to large targets)
* Increasing difficulty across stages
* Score and stage tracking
* High score and highest stage saved locally
* UI built with TextMeshPro

## Gameplay Mechanics

### Target Spawning

Targets spawn within a defined area around a spawn center.

* Horizontal position (X, Z) is randomized within a spawn radius
* Vertical position (Y) spawns between the spawn center and a defined maximum height above it
* Example: if the max height is 15, targets spawn between `spawnCenter.y` and `spawnCenter.y + 15`

### Target Size

Targets have two possible sizes:

* Normal size (scale = 1)
* Large targets (random scale up to 5x)

The chance of spawning large targets decreases as the stage increases.

### Stage Progression

* Stage 1 begins with 3 targets
* Each new stage increases the number of targets by 2
* When all targets in a stage are destroyed, the next stage begins automatically

### Scoring

* Each destroyed target grants 10 points
* Score is displayed on screen
* High score and highest stage are saved locally

## Project Structure

Typical scene structure:

```
Scene
├── Player
│   └── Main Camera
├── Gun
│   └── RaycastGun Script
├── GameManager
├── SpawnCenter
└── Canvas
    ├── ScoreText
    └── StageText
```

## Setup

### Requirements

* Unity (recommended: Unity 2021+ or newer)
* TextMeshPro package installed

### Scene Setup

1. Create an empty GameObject called `GameManager`

2. Attach the `GameManager` script

3. Assign the following in the Inspector:

   * `Score Text`
   * `Stage Text`
   * `Target Prefab`
   * `Spawn Center`

4. Create an empty GameObject called `SpawnCenter` and place it in the center of the spawn area.

5. Ensure the target prefab contains:

   * Collider
   * Tag (e.g. `target`)
   * Script that calls `GameManager.instance.TargetDestroyed()` when destroyed.

## How to Play

1. Start the game.
2. Aim using the camera.
3. Shoot targets using the fire button.
4. Destroy all targets in the current stage to advance to the next stage.
5. Try to achieve the highest score and stage possible.

## Data Storage

The game stores progress locally using Unity PlayerPrefs:

* `SavedHighScore`
* `SavedHighStage`

These values persist between game sessions.

## Future Improvements

Possible improvements include:

* Moving targets
* Different target types
* Difficulty scaling adjustments
* Visual effects for impacts
* Sound and lighting improvements
* Score scaling based on target size

## License

This project is for educational and experimentation purposes.
