# Simple FPS Unity

## Overview

Simple FPS Unity is a small first-person shooter prototype built with the Unity game engine. The project focuses on core FPS mechanics such as raycast shooting, randomly spawned targets, stage progression, and score tracking.

Targets spawn in a 3D area above a defined spawn center. As the player progresses through stages, the number of targets increases while large targets become less frequent.

The game stores the highest score and highest stage locally using Unity's PlayerPrefs system.

## Features

* First-person shooting using raycasting
* Random target spawning within a configurable 3D area
* Targets with variable size (normal to large targets up to 5× scale)
* Stage progression with increasing difficulty
* Score tracking and stage display using TextMeshPro
* Local high score and highest stage persistence
* Basic FPS player movement and mouse look

## Gameplay

The player controls a first-person camera and shoots targets that appear in the environment.

Each destroyed target increases the player's score. When all targets in the current stage are destroyed, the game automatically advances to the next stage where more targets will spawn.

## Controls

Default controls:

* **W / A / S / D** — Move the player
* **Space** — Jump 
* **Mouse** — Look around / aim
* **Left Mouse Button** — Shoot

## Project Structure

Main scripts and components in the project:

* **GameManager.cs**
  Handles stage progression, target spawning, score tracking, and saving player progress.

* **RaycastGun.cs**
  Implements the shooting system using raycasting, including effects such as muzzle flash, recoil, and sound.

* **TargetObjek.cs**
  Script attached to targets that notifies the GameManager when a target is destroyed.

* **playerMovement.cs**
  Basic first-person movement controller.

* **mouseLook.cs**
  Handles camera rotation and mouse look behavior.

## Running the Project

1. Clone the repository:
   git clone https://github.com/Lukas166/simple-fps-unity.git

2. Open the project in Unity (Unity 2021 or newer recommended).

3. Open the scene located at:
   Assets/Scenes/SampleScene.unity

4. Press **Play** to start the game.

## Data Persistence

The game stores the following values locally using PlayerPrefs:

* Highest score achieved
* Highest stage reached

These values persist between play sessions.

## Possible Improvements

Future extensions for the project could include:

* Moving or AI-controlled targets
* Different target types with different scores
* Improved visual effects and sound design
* Difficulty scaling adjustments
* Menu system and game UI improvements

## License

This project is intended for educational and experimental purposes.
