@echo off
rem ===========================================================================
rem  BH3 7.0 server v2 (.NET 8 + SQLite) - build script
rem
rem  KEEP THIS FILE ASCII-ONLY. cmd.exe parses batch files by byte offset,
rem  so any non-ASCII byte (even with chcp 65001) breaks line splitting.
rem
rem  Targets:
rem    build.bat            publish self-contained -> ..\run  (default)
rem    build.bat quick      dotnet build (needs ASP.NET Core 8 runtime installed)
rem    build.bat clean      remove bin/obj and ..\run\*.dll
rem ===========================================================================
setlocal
cd /d "%~dp0"

set "TARGET=%~1"
if "%TARGET%"=="" set "TARGET=publish"

if /i "%TARGET%"=="clean"  goto CLEAN
if /i "%TARGET%"=="quick"  goto QUICK
if /i "%TARGET%"=="publish" goto PUBLISH

echo [ERROR] unknown target "%TARGET%"
echo         usage: build.bat [publish^|quick^|clean]
exit /b 1

:PUBLISH
echo === publish: self-contained win-x64 net8.0 -^> ..\run ===
dotnet publish "Server\Server.csproj" -c Release -r win-x64 --self-contained true --nologo -v q -o "run"
if errorlevel 1 goto FAIL
echo.
echo [OK] built. run\Server.exe is self-contained (no .NET install needed).
goto DONE

:QUICK
echo === build: compile check only (output goes to Server\bin, NOT run\) ===
dotnet build "Server\Server.csproj" -c Release --nologo -v q
if errorlevel 1 goto FAIL
echo.
echo [OK] compiled with 0 errors.
echo      This does NOT update run\. To produce a runnable build: build.bat
goto DONE

:CLEAN
echo === clean ===
if exist "Server\bin"     rmdir /s /q "Server\bin"
if exist "Server\obj"     rmdir /s /q "Server\obj"
if exist "Common\bin"     rmdir /s /q "Common\bin"
if exist "Common\obj"     rmdir /s /q "Common\obj"
echo [OK] bin/obj removed. run\ and run\data\ (your save) are untouched.
goto DONE

:FAIL
echo.
echo [ERROR] build failed, scroll up for the first error.
exit /b 1

:DONE
endlocal
