@echo off
cls
echo Este programa apagara o banco de dados local, criara outro, executara a batida de testes, e logo em seguida o apagara.
echo ----------------------------------------------------------------
echo Se nao deseja continuar, feche este programa imediatamente.
echo ----------------------------------------------------------------
echo Para continuar, certifique-se de que o servidor local esta *desligado*, e pressione qualquer tecla.
pause

echo ----------------------------------------------------------------
echo Reiniciando banco de dados. Caso o banco de dados esteja sendo utilizado, o programa congelara.
echo ----------------------------------------------------------------
sqlcmd -S DESKTOP-ELHKR4F\SQLEXPRESS -i "../Scripts/CreateDatabase.sql"
echo ----------------------------------------------------------------
echo Banco de dados reiniciado.
START dotnet run --project ../

echo Executando bateria de testes. Aguarde...
timeout 3
call newman run ./Postman/PostmanCollection.json -e ./Postman/PostmanEnvironment.json -k
echo ----------------------------------------------------------------
echo Apagando banco de dados...
taskkill /IM tsb.mininal.policy.engine.exe /F
sqlcmd -S DESKTOP-ELHKR4F\SQLEXPRESS -i "../Scripts/DropDatabase.sql"
echo ----------------------------------------------------------------
echo Testes finalizados. Algum teste falhou? Corrija antes de fazer commit, por favor!
pause