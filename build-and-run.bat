@echo off
REM Build and Run Script for Goal Scoring Game
REM This script builds the project and runs the game

echo ========================================
echo Goal Scoring Game - Build and Run
echo ========================================
echo.

echo Building the project...
dotnet build GoalGame\GoalGame.csproj --configuration Release

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Build successful!
    echo.
    echo Starting the game...
    echo.
    cd GoalGame\bin\Release\net10.0-windows
    start GoalGame.exe
    cd ..\..\..\..
    echo.
    echo Game started! Have fun scoring goals!
) else (
    echo.
    echo Build failed. Please check the error messages above.
    pause
)
