# Goal Scoring Game - Visual Guide

## Game Layout (ASCII Representation)

```
┌─────────────────────────────────────────────────────────────────┐
│ Score: 3/5                                                      │
│                                                                 │
│                    ┌───────────────────┐                        │
│                    │ ╔═══════════════╗ │  <- White Goal Post   │
│                    │ ║   [RED BOX]   ║ │  <- Red Goalkeeper    │
│                    │ ║               ║ │     (moves left-right)│
│                    │ ╚═══════════════╝ │                        │
│                    └───────────────────┘                        │
│                                                                 │
│                                                                 │
│                         ●                 <- White Ball         │
│                                             (you shoot this)    │
│                                                                 │
│                      [BLUE]               <- Blue Player        │
│                       ▓▓▓                    (you)              │
│                       ▓▓▓                                       │
│                                                                 │
│ Click to shoot | SPACE for random | R to reset                 │
└─────────────────────────────────────────────────────────────────┘
```

## Game Elements

### 1. Goal Post (Top Center)
- **Position**: 200px from left, 50px from top
- **Size**: 400px wide × 100px tall
- **Color**: White with gray net pattern
- **Purpose**: Scoring zone

### 2. Goalkeeper (Inside Goal)
- **Color**: Red
- **Size**: 100px wide × 30px tall
- **Movement**: Automatic, left-to-right at 8 pixels per frame
- **Purpose**: Block your shots

### 3. Player (Bottom Center)
- **Color**: Blue
- **Size**: 40px wide × 60px tall
- **Position**: Bottom center of screen
- **Purpose**: Visual representation of you

### 4. Ball (Near Player)
- **Color**: White
- **Size**: 30px diameter circle
- **Starting Position**: Bottom center, near player
- **Purpose**: What you shoot toward the goal

### 5. Score Display (Top Left)
- **Format**: "Score: X/Y"
- **X**: Goals successfully scored
- **Y**: Total shot attempts
- **Example**: "Score: 3/5" means 3 goals out of 5 attempts

### 6. Result Messages (Center Screen)
Appear after each shot with semi-transparent black background:
- **"GOAL! Score: X/Y"** - Yellow text when you score
- **"BLOCKED! Score: X/Y"** - Yellow text when goalkeeper blocks
- **"MISSED! Score: X/Y"** - Yellow text when ball goes out of bounds

## How to Play - Step by Step

### Starting the Game
1. Launch `GoalGame.exe`
2. Window opens showing the game field
3. Ball is ready at the bottom center

### Taking a Shot
**Method 1: Mouse Click**
1. Move your mouse to where you want to shoot
2. Click left mouse button
3. Ball flies toward that point
4. Result message appears for 2 seconds

**Method 2: Random Shot**
1. Press SPACE key
2. Ball shoots at random location in goal
3. Result message appears for 2 seconds

### Scoring
- **Goal**: Ball enters goal without hitting goalkeeper
  - Score increases (e.g., 3/5 → 4/6)
  - "GOAL!" message displays
  
- **Blocked**: Ball hits goalkeeper
  - Score stays same, attempts increase (e.g., 3/5 → 3/6)
  - "BLOCKED!" message displays
  
- **Missed**: Ball goes outside goal area
  - Score stays same, attempts increase (e.g., 3/5 → 3/6)
  - "MISSED!" message displays

### Resetting the Game
1. Press R key
2. Score resets to 0/0
3. Ball returns to starting position

## Game Physics

### Ball Movement
- Travels in straight line from starting position to target
- Speed: 10 pixels per frame (~500 pixels per second)
- No gravity or curve

### Goalkeeper AI
- Moves at 8 pixels per frame
- Bounces at goal edges
- Direction changes automatically
- Unpredictable timing makes game challenging

### Collision Detection
- Ball and goalkeeper rectangles check for intersection
- If they overlap while ball is in goal zone → BLOCKED
- If ball reaches goal without overlap → GOAL
- If ball exits game area → MISSED

## Tips for Success

1. **Watch the Goalkeeper**: Time your shots when goalkeeper is far from your target
2. **Aim for Corners**: Harder for goalkeeper to reach
3. **Quick Shots**: Take advantage of goalkeeper direction changes
4. **Use Random**: Press SPACE for quick unpredictable shots
5. **Track Stats**: Monitor your score ratio to improve

## Technical Details

### Performance
- 50 FPS (frames per second)
- 20ms frame time
- Smooth animation
- No lag on modern systems

### Window
- Size: 800px × 600px
- Title: "Goal Scoring Game - Try to Score!"
- Centered on screen at startup
- Green grass background

### Controls Summary
| Key/Action | Function |
|------------|----------|
| Left Click | Shoot at cursor location |
| SPACE | Random shot in goal |
| R | Reset score to 0/0 |
| Close Window | Exit game |

## Screenshots Description

The game features:
- **Green grass field** - Forest green background (#228B22)
- **White goal post** - With gray net pattern for realism
- **Red goalkeeper** - Contrasts well against white goal
- **Blue player** - Your character at bottom
- **White ball** - Visible against green background
- **Yellow messages** - Easy to read result notifications
- **White text** - Score and instructions

## Enjoy!

Try to achieve the highest score ratio possible. Can you score 10/10? 20/20? Challenge yourself and have fun!
