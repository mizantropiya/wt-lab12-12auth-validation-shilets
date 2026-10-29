@echo off
setlocal EnableExtensions
cd /d "%~dp0"
set "ASPNETCORE_ENVIRONMENT=Development"
set "DATABASE_URL="
title WT-12 - build and migrations
echo =======================================
echo   WT-12 - build and migrations check
echo =======================================
call dotnet tool restore
if errorlevel 1 goto failed
call dotnet build
if errorlevel 1 goto failed
call dotnet tool run dotnet-ef database update
if errorlevel 1 goto failed
call dotnet tool run dotnet-ef migrations list
if errorlevel 1 goto failed
echo.
echo SUCCESS. Take a screenshot of AddUsers in the migrations list for Figure 4.
pause
exit /b 0
:failed
echo.
echo ERROR: The check stopped. Keep this window open and copy the last error message.
pause
exit /b 1
