[![Run in Insomnia}](https://insomnia.rest/images/run.svg)](https://insomnia.rest/run/?label=TSB%20API%20Minimal%20Policy%20Engine&uri=api.verissimo.dev%2FInsomniaCollection.json) [![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/2fe25d04adce953c2f24?action=collection%2Fimport)

[![Teste de Build .NET](https://github.com/Neppale/tsb.mininal.policy.engine/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Neppale/tsb.mininal.policy.engine/actions/workflows/dotnet.yml)

![enter image description here](https://i.imgur.com/dEYYaYQ.png)

Projeto Integrado Multidisciplinar do segundo semestre da Universidade Paulista, curso Análise e Desenvolvimento de Sistemas. Neste projeto, foi desenvolvido um sistema de gerenciamento de apólices de seguros para uma empresa. O sistema foi desenvolvido em linguagem de programação C#, utilizando o framework ASP.NET Core.

Esta API foi desenvolvida com o objetivo de ser utilizada por funcionários da empresa, para gerenciar as apólices de seguros, e para os clientes que desejem contratar seguros.

## Instalação

Primeiramente, é necessário instalar as dependências do projeto e em seguida, compilar o projeto:

    dotnet restore
    dotnet build

## Execução

Para executar o projeto, basta utilizar o comando:

    dotnet run

É importante lembrar que o arquivo _appsettings.json_ contém as configurações de conexão com o banco de dados. Caso o banco de dados não esteja disponível, o projeto não irá funcionar. O banco de dados utilizado é o Microsoft SQL Server, e o script de criação do banco de dados está disponível no diretório Scripts.

## Testes

É possível realizar um conjunto de testes. Para isso, é necessário ter o NodeJS em sua máquina, e instalar o pacote Newman:

    npm install -g newman

Este comando irá instalar o pacote Newman, e em seguida, é possível executar os testes:

    newman run Tests/Postman/PostmanCollection.json -e Tests/Postman/PostmanEnvironment.json -k --bail

Lembre-se que o servidor deve estar ligado para que os testes funcionem. Os testes incluem a criação, alteração, exlusão e consulta de usuários, coberturas, terceirizados, clientes, veículos, apólices e ocorrências.

## Endpoints

É possível acessar os endpoints da API através dos links abaixo:

[![Run in Insomnia}](https://insomnia.rest/images/run.svg)](https://insomnia.rest/run/?label=TSB%20API%20Minimal%20Policy%20Engine&uri=api.verissimo.dev%2FInsomniaCollection.json) [![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/2fe25d04adce953c2f24?action=collection%2Fimport)
