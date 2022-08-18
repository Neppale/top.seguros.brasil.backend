@echo off
setlocal

IF "%1"=="-s" GOTO DATABASERESTART

:START
cls
echo "   _______ _____ ____    _____      _ _              ______             _                       _____ _____
echo "  |__   __/ ____|  _ \  |  __ \    | (_)            |  ____|           (_)                /\   |  __ \_   _|
echo "     | | | (___ | |_) | | |__) |__ | |_  ___ _   _  | |__   _ __   __ _ _ _ __   ___     /  \  | |__) || |
echo "     | |  \___ \|  _ <  |  ___/ _ \| | |/ __| | | | |  __| | '_ \ / _` | | '_ \ / _ \   / /\ \ |  ___/ | |
echo "     | |  ____) | |_) | | |  | (_) | | | (__| |_| | | |____| | | | (_| | | | | |  __/  / ____ \| |    _| |_
echo "     |_| |_____/|____/  |_|   \___/|_|_|\___|\__, | |______|_| |_|\__, |_|_| |_|\___| /_/    \_\_|   |_____|
echo "                                             __/ |                __/ |
echo "                                            |___/                |___/
echo "                   _______        _   _               _____               
echo "                  |__   __|      | | (_)             |  __ \                          _
echo "                     | | ___  ___| |_ _ _ __   __ _  | |__) | __ ___  _ __ ___  _ __ | |_
echo "                     | |/ _ \/ __| __| | '_ \ / _` | |  ___/ '__/ _ \| '_ ` _ \| '_ \| __|
echo "                     | |  __/\__ \ |_| | | | | (_| | | |   | | | (_) | | | | | | |_) | |_
echo "                     |_|\___||___/\__|_|_| |_|\__, | |_|   |_|  \___/|_| |_| |_| .__/ \__|
echo "                                               __/ |                           | |
echo "                                              |___/                            |_|
echo:

echo Este programa: 
echo:
echo  1. Reinicia o banco de dados local;
echo  2. Executa os testes de integraçao; 
echo  3. Apaga o novo banco de dados.
echo:
echo Antes de continuar, certifique-se de que o servidor local esta [31mdesligado[0m.
echo ----------------------------------------------------------------
SET /P CHOICE=Deseja continuar? (S/[N])
IF /I %CHOICE% NEQ S GOTO END

:DATABASERESTART
echo ----------------------------------------------------------------
echo Reiniciando banco de dados.
echo ----------------------------------------------------------------
taskkill /IM tsb.mininal.policy.engine.exe /F
taskkill /IM Ssms.exe /F
sqlcmd -S localhost -U sa -P Password1234! -i "../Scripts/CreateDatabase.sql"
echo ----------------------------------------------------------------
echo Banco de dados reiniciado.
START dotnet run --project ../../

:TESTING
echo Executando bateria de testes...
echo ----------------------------------------------------------------
timeout 5 /NOBREAK
sleep 5s
call newman run ./Postman/PostmanCollection.json -e ./Postman/PostmanEnvironment.json -k
echo ----------------------------------------------------------------
if errorlevel 1 GOTO ERROREND
if errorlevel 0 GOTO DATABASEKILL


:DATABASEKILL
echo Testes finalizados com sucesso!
echo Apagando banco de dados...
echo ----------------------------------------------------------------
timeout 5 /NOBREAK
sleep 5s
taskkill /IM tsb.mininal.policy.engine.exe /F
sqlcmd -S localhost -U sa -P Password1234! -i "../Scripts/DropDatabase.sql"
sqlcmd -S localhost -U sa -P Password1234! -i "../Scripts/CreateDatabase.sql"
cd Temp
del *.pdf /q
del *.png -/q

:END
echo Finalizando...
endlocal
exit /b 0

:ERROREND
taskkill /IM tsb.mininal.policy.engine.exe /F
sqlcmd -S DESKTOP-ELHKR4F\SQLEXPRESS -i "../Scripts/DropDatabase.sql"
sqlcmd -S localhost -U sa -P Password1234! -i "../Scripts/CreateDatabase.sql"
cd ../Temp
del *.pdf /q
del *.png -/q
echo Testes finalizados com falhas. Por favor, verifique o log e tente novamente.
endlocal
pause
exit /b 1

