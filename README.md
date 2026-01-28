# Goal - Windows Goal Scoring Game

A fun Windows Forms application where you play as a player trying to score goals against a moving goalkeeper!

## Game Description

This is an interactive goal-scoring game where:
- **Goalkeeper**: Automatically moves left and right in the goal, trying to block your shots
- **Player**: You control the shots, trying to score as many goals as possible
- **Objective**: Score as much as possible by shooting the ball past the goalkeeper

## Features

- Interactive gameplay with mouse and keyboard controls
- Animated goalkeeper that moves to block shots
- Score tracking system (goals scored / total attempts)
- Visual feedback for goals, blocks, and misses
- Simple and intuitive controls

## How to Play

1. **Shoot the Ball**: Click anywhere in the goal area to shoot the ball in that direction
2. **Random Shot**: Press SPACE to take a random shot
3. **Reset Score**: Press R to reset your score and start over

## Controls

- **Left Mouse Click**: Aim and shoot at the clicked location
- **SPACE**: Take a random shot at the goal
- **R**: Reset the game score

## Building the Application

### Requirements
- .NET 10.0 SDK or later
- Windows operating system (for running the application)

### Build Instructions

1. Clone the repository
2. Navigate to the project directory
3. Build the solution:
```bash
dotnet build Goal.sln
```

### Run Instructions

Run the application:
```bash
dotnet run --project GoalGame/GoalGame.csproj
```

Or build and run the executable:
```bash
cd GoalGame/bin/Debug/net10.0-windows
./GoalGame.exe
```

## Project Structure

```
Goal/
├── GoalGame/
│   ├── GoalGame.csproj    # Project file
│   ├── Program.cs         # Application entry point
│   └── GameForm.cs        # Main game logic and UI
├── Goal.sln               # Solution file
└── README.md              # This file
```

## Game Mechanics

- The goalkeeper automatically moves back and forth in the goal
- Click to shoot the ball towards your target
- If the ball hits the goalkeeper, it's blocked
- If the ball reaches the goal without hitting the goalkeeper, you score!
- The score shows your successful goals vs total attempts

## Screenshots

The game features:
- Green grass field
- White goal post with net pattern
- Red moving goalkeeper
- Blue player at the bottom
- White ball for shooting

Enjoy playing and try to score as many goals as possible!