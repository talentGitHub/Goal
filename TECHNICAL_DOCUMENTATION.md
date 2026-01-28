# Goal Scoring Game - Technical Documentation

## Application Overview

This is a Windows Forms application built with .NET 10.0 that implements a goal-scoring game.

## Game Components

### 1. **Goalkeeper (Red Rectangle)**
- Position: Inside the goal post at the top
- Behavior: Automatically moves left-to-right continuously
- Size: 100x30 pixels
- Movement: Bounces between goal post edges at 8 pixels per frame

### 2. **Goal Post (White Rectangle with Net Pattern)**
- Position: Top of screen (200, 50)
- Size: 400x100 pixels
- Visual: White background with gray grid pattern simulating a net

### 3. **Player (Blue Rectangle)**
- Position: Bottom center of screen
- Size: 40x60 pixels
- Role: Visual representation of the shooter

### 4. **Ball (White Circle)**
- Starting Position: Bottom center (next to player)
- Size: 30x30 pixels diameter
- Behavior: Moves when player shoots

## Game Mechanics

### Shooting System
1. **Mouse Click**: Click anywhere on screen to shoot ball toward that point
2. **Keyboard (SPACE)**: Shoots ball at a random point in the goal
3. **Ball Physics**: Ball travels in straight line from start position to target

### Scoring Logic
1. **Goal**: Ball reaches goal area without hitting goalkeeper
   - Score increments
   - Attempts increment
   - Shows "GOAL!" message

2. **Blocked**: Ball hits goalkeeper while in goal area
   - Only attempts increment
   - Shows "Blocked by goalkeeper!" message

3. **Missed**: Ball goes outside goal boundaries
   - Only attempts increment
   - Shows "Missed!" message

### Score Tracking
- Format: `Score: X/Y` where X = goals scored, Y = total attempts
- Displayed in top-left corner
- Can be reset with 'R' key

## Technical Implementation

### Key Classes and Methods

#### GameForm.cs
- `InitializeGame()`: Sets up initial positions of all game objects
- `GameTimer_Tick()`: Game loop - runs ~50 times per second
  - Updates goalkeeper position
  - Updates ball position if moving
  - Checks for collisions and scoring
- `ShootBall()`: Calculates ball velocity toward target
- `ResetBall()`: Returns ball to starting position

### Collision Detection
- Uses Rectangle.IntersectsWith() for goalkeeper-ball collision
- Uses boundary checks for goal scoring and misses

### Animation
- System.Windows.Forms.Timer with 20ms interval (~50 FPS)
- Double-buffered rendering to prevent flicker

## Game Layout

```
┌────────────────────────────────────────┐
│ Score: 3/5                             │
│                                        │
│        ┌────────────────┐              │
│        │ [===========] │ <- Goal Post │
│        │    [RED]      │ <- Goalkeeper│
│        └────────────────┘              │
│                                        │
│                                        │
│           (O) <- Ball                  │
│                                        │
│            [BLUE]  <- Player           │
│                                        │
│ Controls: Click to shoot | SPACE...    │
└────────────────────────────────────────┘
```

## Controls Summary

| Control | Action |
|---------|--------|
| Left Mouse Click | Aim and shoot at clicked location |
| SPACE | Random shot at goal |
| R | Reset score to 0/0 |

## Build Requirements

- .NET 10.0 SDK
- Windows OS (uses Windows Forms)
- EnableWindowsTargeting property enabled for cross-platform development

## Project Structure

```
GoalGame/
├── GoalGame.csproj   - Project configuration
├── Program.cs        - Application entry point
└── GameForm.cs       - Main game implementation (200+ lines)
```

## Features Implemented

✅ Animated goalkeeper with automatic movement
✅ Player shooting mechanics (mouse + keyboard)
✅ Ball physics and trajectory
✅ Collision detection
✅ Score tracking system
✅ Visual feedback (message boxes)
✅ Game reset functionality
✅ Professional UI with colored elements
✅ Smooth 50 FPS animation

## Future Enhancement Ideas

- Difficulty levels (faster goalkeeper)
- Sound effects
- Multiple rounds/time limits
- High score tracking
- Different ball speeds based on distance
- Power-ups or special shots
- Save/load game statistics
