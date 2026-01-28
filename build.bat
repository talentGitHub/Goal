@echo off
REM Quick Build Script for Goal Scoring Game

echo Building Goal Scoring Game...
dotnet build GoalGame\GoalGame.csproj --configuration Release

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Build successful!
    echo ========================================
    echo.
    echo To run the game, execute:
    echo   GoalGame\bin\Release\net10.0-windows\GoalGame.exe
    echo.
    echo Or simply run: build-and-run.bat
    echo.
) else (
    echo.
    echo Build failed!
    pause
)
