#!/bin/sh
# Path: Source\Tests\Testing.bash
# @echo off
# setlocal

# Atenção: Este script não deve ser executado diretamente.

if [ -S "$1" ]; then
    goto :DATABASERESTART
fi


# :START
# cls

echo "   _______ _____ ____    _____      _ _              ______             _                       _____ _____"
echo "  |__   __/ ____|  _ \  |  __ \    | (_)            |  ____|           (_)                /\   |  __ \_   _|"
echo "     | | | (___ | |_) | | |__) |__ | |_  ___ _   _  | |__   _ __   __ _ _ _ __   ___     /  \  | |__) || |"
echo "     | |  \___ \|  _ <  |  ___/ _ \| | |/ __| | | | |  __| |  _ \ / _  | |  _ \ / _ \   / /\ \ |  ___/ | |"
echo "     | |  ____) | |_) | | |  | (_) | | | (__| |_| | | |____| | | | (_| | | | | |  __/  / ____ \| |    _| |_"
echo "     |_| |_____/|____/  |_|   \___/|_|_|\___|\__, | |______|_| |_|\__, |_|_| |_|\___| /_/    \_\_|   |_____|"
echo "                                             __/ |                __/ |                                     "
echo "                                            |___/                |___/                                      "
echo "                   _______        _   _               _____                                                 "
echo "                  |__   __|      | | (_)             |  __ \                          _"
echo "                     | | ___  ___| |_ _ _ __   __ _  | |__) | __ ___  _ __ ___  _ __ | |_"
echo "                     | |/ _ \/ __| __| |  _ \ / _  | |  ___/  __/ _ \|  _   _ \|  _ \| __|"
echo "                     | |  __/\__ \ |_| | | | | (_| | | |   | | | (_) | | | | | | |_) | |_"
echo "                     |_|\___||___/\__|_|_| |_|\__, | |_|   |_|  \___/|_| |_| |_| .__/ \__|"
echo "                                               __/ |                           | |"
echo "                                              |___/                            |_|                          "
echo

# TODO: Fazer funcionar.
# echo Este programa:
# echo
# echo  1. Reinicia o banco de dados local;
# echo  2. Executa os testes de integraçao;
# echo  3. Apaga o novo banco de dados.
# echo
# echo Antes de continuar, certifique-se de que o servidor local esta [31mdesligado[0m.
# echo ----------------------------------------------------------------
# read -r -p "Deseja continuar? (S/[N]) " CHOICE
# # if [ "$CHOICE" != "S" ]; then
    
# # fi

echo ----------------------------------------------------------------
echo Reiniciando banco de dados.
echo ----------------------------------------------------------------
echo
sqlcmd -S "localhost" -U "sa" -P "Password1234!" -i "Source/Scripts/CreateDatabase.sql"
echo ----------------------------------------------------------------
echo Banco de dados reiniciado.
echo ----------------------------------------------------------------
echo
echo ----------------------------------------------------------------
echo Executando testes de integraçao.
echo ----------------------------------------------------------------
dotnet run --project . >.log 2>&1 &
sleep 20
newman run "Source/Tests/Postman/PostmanCollection.json" -e "Source/Tests/Postman/PostmanEnvironment.json" -k
EXIT_CODE=$?

echo ----------------------------------------------------------------
echo Testes de integraçao executados.
echo ----------------------------------------------------------------
echo
echo ----------------------------------------------------------------

if [ "$EXIT_CODE" -eq 0 ]; then
    echo
    echo [32mTodos os testes foram executados com sucesso.[0m
    echo
else
    echo
    echo [31mHouve falha nos testes.[0m
    echo
fi
echo ----------------------------------------------------------------

exit $EXIT_CODE











