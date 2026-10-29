@echo off
setlocal EnableExtensions
cd /d "%~dp0"
set "ASPNETCORE_ENVIRONMENT=Development"
set "DATABASE_URL="
title WT-12 - start project

echo ================================================
echo  WT-12 - Bank Accounts / Razor Pages / PostgreSQL
echo ================================================
echo.
echo [1/5] Checking .NET SDK...
where dotnet >nul 2>&1
if errorlevel 1 goto no_dotnet
dotnet --list-sdks
if errorlevel 1 goto no_dotnet

echo.
echo [2/5] Checking Docker Desktop...
where docker >nul 2>&1
if errorlevel 1 goto no_docker
docker info >nul 2>&1
if errorlevel 1 goto docker_stopped

echo.
echo [3/5] Starting local PostgreSQL...
docker container inspect wt-lab8-postgres >nul 2>&1
if errorlevel 1 (
    echo Container missing. Creating WT-12 local PostgreSQL on port 5433...
    docker run -d --name wt-lab8-postgres -e POSTGRES_USER=wt8_user -e POSTGRES_PASSWORD=wt8_pass -e POSTGRES_DB=wt_lab8 -p 5433:5432 -v wt12_postgres_data:/var/lib/postgresql/data postgres:16-alpine
    if errorlevel 1 goto postgres_failed
) else (
    echo Existing WT-8 PostgreSQL container detected.
    docker start wt-lab8-postgres >nul 2>&1
)

echo Waiting for the database to accept connections...
set "TRIES=0"
:wait_postgres
docker exec wt-lab8-postgres pg_isready -U wt8_user -d wt_lab8 >nul 2>&1
if not errorlevel 1 goto postgres_ready
set /a TRIES+=1
if %TRIES% GEQ 40 goto postgres_not_ready
timeout /t 2 /nobreak >nul
goto wait_postgres
:postgres_ready
echo PostgreSQL is ready.

echo.
echo [4/5] Building project (NuGet restore is automatic)...
dotnet build "wt-lab12-12auth-validation-shilets.csproj"
if errorlevel 1 goto build_failed

echo.
echo [5/5] Starting the web application...
echo The database tables and demo user are created automatically on first start.
echo URL: http://localhost:5290/Login
echo Login: student  Password: 1234
echo Keep this window open while you use the website.
echo.
dotnet run --no-build --launch-profile http --project "wt-lab12-12auth-validation-shilets.csproj"
if errorlevel 1 goto run_failed
goto end

:no_dotnet
echo.
echo ERROR: .NET SDK is missing. Install the .NET 8 SDK from https://dotnet.microsoft.com/download/dotnet/8.0
exit /b 1
:no_docker
echo.
echo ERROR: Docker Desktop is missing. Install it from https://www.docker.com/products/docker-desktop/
exit /b 1
:docker_stopped
echo.
echo ERROR: Docker Desktop is not running. Start Docker Desktop, wait for Engine running, and try again.
pause
exit /b 1
:postgres_failed
echo.
echo ERROR: Could not create PostgreSQL. Check Docker Desktop and whether port 5433 is free.
pause
exit /b 1
:postgres_not_ready
echo.
echo ERROR: PostgreSQL container did not become ready. Check Docker Desktop.
pause
exit /b 1
:build_failed
echo.
echo ERROR: The .NET build failed. Keep this screen and send the error output.
pause
exit /b 1
:run_failed
echo.
echo ERROR: The application stopped with an error. Keep this screen and send the error output.
pause
exit /b 1
:end
echo.
echo The application has stopped. Press any key to close this window.
pause >nul
endlocal
