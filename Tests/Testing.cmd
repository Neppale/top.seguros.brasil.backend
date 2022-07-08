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
echo "                   _______        _   _               _____                           _             
echo "                  |__   __|      | | (_)             |  __ \                         | |                                             
echo "                     | | ___  ___| |_ _ _ __   __ _  | |__) | __ ___  _ __ ___  _ __ | |_                                            
echo "                     | |/ _ \/ __| __| | '_ \ / _` | |  ___/ '__/ _ \| '_ ` _ \| '_ \| __|                                           
echo "                     | |  __/\__ \ |_| | | | | (_| | | |   | | | (_) | | | | | | |_) | |_                                            
echo "                     |_|\___||___/\__|_|_| |_|\__, | |_|   |_|  \___/|_| |_| |_| .__/ \__|                                           
echo "                                               __/ |                           | |                                                   
echo "                                              |___/                            |_|                            
echo:

echo Este programa reiniciara o banco de dados local, executara os testes, e logo em seguida apagara o banco.
echo Antes de continuar, certifique-se de que o servidor local esta [31mdesligado[0m.
echo ----------------------------------------------------------------
SET /P CHOICE=Deseja continuar? (S/[N])
IF /I %CHOICE% NEQ S GOTO END

:DATABASERESTART
echo ----------------------------------------------------------------
echo Reiniciando banco de dados.
echo ----------------------------------------------------------------
taskkill /IM dotnet.exe /F
taskkill /IM Ssms.exe /F
sqlcmd -S DESKTOP-ELHKR4F\SQLEXPRESS -i "../Scripts/CreateDatabase.sql"
echo ----------------------------------------------------------------
echo Banco de dados reiniciado.
START dotnet run --project ../

:TESTING
echo Executando bateria de testes...
echo ----------------------------------------------------------------
timeout 5
sleep 5s
call newman run ./Postman/PostmanCollection.json -e ./Postman/PostmanEnvironment.json -k
echo ----------------------------------------------------------------
if errorlevel 1 GOTO ERROREND
if errorlevel 0 GOTO DATABASEKILL


:DATABASEKILL
echo Testes finalizados com sucesso!
echo Apagando banco de dados...
timeout 5
sleep 5s
echo ----------------------------------------------------------------
taskkill /IM tsb.mininal.policy.engine.exe /F
sqlcmd -S DESKTOP-ELHKR4F\SQLEXPRESS -i "../Scripts/DropDatabase.sql"
cd ../Temp
del *.pdf /q
del *.png -/q

:END
echo Finalizando...
endlocal
exit /b 0

:ERROREND
echo Testes falharam. Por favor, verifique o log e tente novamente.
endlocal
pause
exit /b 1

