# Build and Run Script for Goal Scoring Game (PowerShell)
# This script builds the project and runs the game

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Goal Scoring Game - Build and Run" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Building the project..." -ForegroundColor Yellow
dotnet build GoalGame\GoalGame.csproj --configuration Release

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "Build successful!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Starting the game..." -ForegroundColor Yellow
    Write-Host ""
    
    Start-Process -FilePath "GoalGame\bin\Release\net10.0-windows\GoalGame.exe"
    
    Write-Host "Game started! Have fun scoring goals!" -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "Build failed. Please check the error messages above." -ForegroundColor Red
    Read-Host "Press Enter to continue"
}
