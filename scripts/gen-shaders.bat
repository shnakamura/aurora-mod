set "CompilerPath=%~dp0..\src\Aurora\Assets\Effects\Compiler\fxc"
set "EffectsPath=%~dp0..\src\Aurora\Assets\Effects\"

if not exist "%CompilerPath%" (
    echo "ERROR: Compiler does not exist: %CompilerPath%"
    pause
    exit /b 1
)

if not exist "%EffectsPath%" (
    echo "ERROR: Effects directory does not exist: %CompilerPath%"
    pause
    exit /b 1
)

pause