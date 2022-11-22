[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/2fe25d04adce953c2f24?action=collection%2Fimport#?env%5BTSB%20API%20Policy%20Engine%20DEV%5D=W3sia2V5IjoiIF8uVVJMICIsInZhbHVlIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA5MyIsImVuYWJsZWQiOnRydWUsInNlc3Npb25WYWx1ZSI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTMiLCJzZXNzaW9uSW5kZXgiOjB9LHsia2V5IjoiQkVBUkVSIiwidmFsdWUiOiJleUpoYkdjaU9pSklVekkxTmlJc0luUjVjQ0k2SWtwWFZDSjkuZXlKcGMzTWlPaUowYjNCelpXZDFjbTl6TG1KeUlpd2lZWFZrSWpvaWRHOXdjMlZuZFhKdmN5NWljaUo5LkJsZ2RYZFlfd3YwNkFiR3RsQlBScGVYcy1FeUdyeXAtMjBpSzNsTjBIRzgiLCJlbmFibGVkIjp0cnVlLCJ0eXBlIjoic2VjcmV0Iiwic2Vzc2lvblZhbHVlIjoiZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SnBjM01pT2lKMGIzQnpaV2QxY205ekxtSnlJaXdpWVhWa0lqb2lkRzl3YzJWbmRYSnZjeTVpY2lKOS5CbGdkWGRZX3d2MDZBYkd0bEJQUnBlWHMtRXlHcnlwLTIwaUszbE4uLi4iLCJzZXNzaW9uSW5kZXgiOjF9XQ==)

[![Build](https://github.com/Neppale/top.seguros.brasil.backend/actions/workflows/build.yml/badge.svg)](https://github.com/Neppale/top.seguros.brasil.backend/actions/workflows/build.yml)

[![Integration tests](https://github.com/Neppale/top.seguros.brasil.backend/actions/workflows/test.yml/badge.svg)](https://github.com/Neppale/top.seguros.brasil.backend/actions/workflows/test.yml)

[![Lint](https://github.com/Neppale/top.seguros.brasil.backend/actions/workflows/lint.yml/badge.svg)](https://github.com/Neppale/top.seguros.brasil.backend/actions/workflows/lint.yml)

É possível que o Lint falhe por conta de um bug no Super Linter, e por isso, não é obrigatório para o merge.

![Logotipo da Top Seguros Brasil](https://i.imgur.com/dEYYaYQ.png)

Projeto Integrado Multidisciplinar do segundo semestre da Universidade Paulista, curso Análise e Desenvolvimento de Sistemas. Neste projeto, foi desenvolvido um sistema de gerenciamento de apólices de seguros para uma empresa. O sistema foi desenvolvido em linguagem de programação C#, utilizando o framework ASP.NET Core.

Esta API foi desenvolvida com o objetivo de ser utilizada por funcionários da empresa, para gerenciar as apólices de seguros, e para os clientes que desejem contratar seguros.

## Instalação

Primeiramente, é necessário instalar as dependências do projeto e em seguida, compilar o projeto:

    dotnet restore
    dotnet build

## Execução

Para executar o projeto, basta utilizar o comando:

    dotnet run

É importante lembrar que há variáveis de ambiente que devem ser configuradas para que o projeto funcione corretamente. Para isso, basta criar um arquivo `.env` na raiz do projeto, com as seguintes variáveis:

    CONNECTION_STRING=string de conexão com o banco de dados

É recomendado que a instalação do banco de dados ocorra na plataforma Docker para evitar problemas de compatibilidade. Para isso, basta executar o seguinte comando:

    docker run --name sqlserver -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password1234!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

## Testes

É possível realizar um conjunto de testes. Para isso, é necessário ter o NodeJS em sua máquina, e instalar o pacote Newman:

    npm install -g newman

Este comando irá instalar o pacote Newman, e em seguida, é possível executar os testes através do arquivo Testing.cmd na pasta Tests. O assistente que abrirá automatiza os testes de integração, criando um novo banco de dados e o apagando ao fim dos testes. É necessária a utilização de um banco de dados com o servidor em "localhost" e a senha "Password1234!". Os testes incluem a criação, alteração, exclusão e consulta de usuários, coberturas, terceirizados, clientes, veículos, apólices e ocorrências.

## Endpoints

É possível acessar os endpoints da API através dos links abaixo:

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/2fe25d04adce953c2f24?action=collection%2Fimport#?env%5BTSB%20API%20Policy%20Engine%20DEV%5D=W3sia2V5IjoiIF8uVVJMICIsInZhbHVlIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA5MyIsImVuYWJsZWQiOnRydWUsInNlc3Npb25WYWx1ZSI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTMiLCJzZXNzaW9uSW5kZXgiOjB9LHsia2V5IjoiQkVBUkVSIiwidmFsdWUiOiJleUpoYkdjaU9pSklVekkxTmlJc0luUjVjQ0k2SWtwWFZDSjkuZXlKcGMzTWlPaUowYjNCelpXZDFjbTl6TG1KeUlpd2lZWFZrSWpvaWRHOXdjMlZuZFhKdmN5NWljaUo5LkJsZ2RYZFlfd3YwNkFiR3RsQlBScGVYcy1FeUdyeXAtMjBpSzNsTjBIRzgiLCJlbmFibGVkIjp0cnVlLCJ0eXBlIjoic2VjcmV0Iiwic2Vzc2lvblZhbHVlIjoiZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SnBjM01pT2lKMGIzQnpaV2QxY205ekxtSnlJaXdpWVhWa0lqb2lkRzl3YzJWbmRYSnZjeTVpY2lKOS5CbGdkWGRZX3d2MDZBYkd0bEJQUnBlWHMtRXlHcnlwLTIwaUszbE4uLi4iLCJzZXNzaW9uSW5kZXgiOjF9XQ==)
