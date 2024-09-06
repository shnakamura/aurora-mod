set "SourcePath=%~dp0src\Aurora"

if not exist "%SourcePath%" (
    echo "ERROR: Source directory does not exist: %SourcePath%"
    pause
    exit /b 1
)

set "TargetPath=%USERPROFILE%\Documents\My Games\Terraria\tModLoader\ModSources\Aurora"

mklink /D "%TargetPath%" "%SourcePath%"
