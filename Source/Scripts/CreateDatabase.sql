USE msdb;
GO
DROP DATABASE IF EXISTS tsb_database;
GO
CREATE DATABASE tsb_database;
GO
USE tsb_database;
GO

CREATE TABLE Veiculos (
    id_veiculo INT PRIMARY KEY IDENTITY NOT NULL,
    marca VARCHAR(50) NOT NULL,
    modelo VARCHAR(100) NOT NULL,
    ano VARCHAR(15) NOT NULL,
    uso VARCHAR(255) NOT NULL,
    placa VARCHAR(8) UNIQUE NOT NULL ,
    renavam VARCHAR(11) UNIQUE NOT NULL ,
    sinistrado BIT NOT NULL,
    id_cliente INT,
    status BIT DEFAULT 1 NOT NULL,
);

CREATE TABLE Clientes (
    id_cliente INT PRIMARY KEY IDENTITY NOT NULL,
    nome_completo VARCHAR(150) NOT NULL,
    senha VARCHAR(max) NOT NULL,
    email VARCHAR(50) UNIQUE NOT NULL,
    cpf VARCHAR(14) UNIQUE NOT NULL,
    cnh VARCHAR(11)UNIQUE NOT NULL,
    cep VARCHAR(9) NOT NULL,
    data_nascimento DATE NOT NULL,
    telefone1 VARCHAR(15) UNIQUE NOT NULL,
    telefone2 VARCHAR(15),
    status BIT DEFAULT 1 NOT NULL
);

CREATE TABLE Usuarios (
    id_usuario INT PRIMARY KEY IDENTITY NOT NULL,
    nome_completo VARCHAR(150) NOT NULL,
    email VARCHAR(50) UNIQUE NOT NULL,
    senha VARCHAR(max) NOT NULL,
    tipo VARCHAR(50) NOT NULL,
    status BIT DEFAULT 1 NOT NULL
);

CREATE TABLE Apolices (
    id_apolice INT PRIMARY KEY IDENTITY NOT NULL,
    data_inicio DATE NOT NULL,
    data_fim DATE NOT NULL,
    premio DECIMAL(9, 2) NOT NULL,
    indenizacao DECIMAL(9, 2) NOT NULL,
    documento VARCHAR(max) NOT NULL,
    id_cobertura INT NOT NULL,
    id_usuario INT NOT NULL,
    id_cliente INT,
    id_veiculo INT,
    status VARCHAR(15) DEFAULT 'Em Analise' NOT NULL
);

CREATE TABLE Coberturas (
    id_cobertura INT PRIMARY KEY IDENTITY NOT NULL,
    nome VARCHAR(50) NOT NULL,
    descricao VARCHAR(255) NOT NULL,
    valor DECIMAL(9, 2) NOT NULL,
    taxa_indenizacao DECIMAL(9, 2) NOT NULL,
    status BIT DEFAULT 1 NOT NULL
);

CREATE TABLE Ocorrencias (
    id_ocorrencia INT PRIMARY KEY IDENTITY NOT NULL,
    data DATE NOT NULL,
    local VARCHAR(100) NOT NULL,
    UF VARCHAR(2) NOT NULL,
    municipio VARCHAR(20) NOT NULL,
    descricao VARCHAR(255) NOT NULL,
    tipo VARCHAR(15) NOT NULL,
    documento VARCHAR(max),
    tipoDocumento VARCHAR(11),
    id_veiculo INT NOT NULL,
    id_cliente INT,
    id_terceirizado INT,
    status VARCHAR(15) DEFAULT 'Andamento' NOT NULL
);

CREATE TABLE Terceirizados (
    id_terceirizado INT PRIMARY KEY IDENTITY NOT NULL,
    nome VARCHAR(150) NOT NULL,
    telefone VARCHAR(15) UNIQUE NOT NULL,
    funcao VARCHAR(30) NOT NULL,
    cnpj VARCHAR(18) UNIQUE NOT NULL,
    valor DECIMAL(9, 2) NOT NULL,
    status BIT DEFAULT 1 NOT NULL
);

CREATE TABLE Notificacoes (
    id_notificacao INT PRIMARY KEY IDENTITY NOT NULL,
    id_usuario INT NOT NULL,
    quantidade INT NOT NULL DEFAULT 0,
);

ALTER TABLE Veiculos ADD CONSTRAINT FK_Veiculos_2
    FOREIGN KEY (id_cliente)
    REFERENCES Clientes (id_cliente) ON DELETE SET NULL;
 
ALTER TABLE Apolices ADD CONSTRAINT FK_Apolices_2
    FOREIGN KEY (id_cobertura)
    REFERENCES Coberturas (id_cobertura);
 
ALTER TABLE Apolices ADD CONSTRAINT FK_Apolices_3
    FOREIGN KEY (id_usuario)
    REFERENCES Usuarios (id_usuario);
 
ALTER TABLE Apolices ADD CONSTRAINT FK_Apolices_4
    FOREIGN KEY (id_cliente)
    REFERENCES Clientes (id_cliente) ON DELETE SET NULL;
 
ALTER TABLE Apolices ADD CONSTRAINT FK_Apolices_5
    FOREIGN KEY (id_veiculo)
    REFERENCES Veiculos (id_veiculo);
 
ALTER TABLE Ocorrencias ADD CONSTRAINT FK_Ocorrencias_2
    FOREIGN KEY (id_veiculo)
    REFERENCES Veiculos (id_veiculo);
 
ALTER TABLE Ocorrencias ADD CONSTRAINT FK_Ocorrencias_3
    FOREIGN KEY (id_cliente)
    REFERENCES Clientes (id_cliente) ON DELETE SET NULL;

ALTER TABLE Notificacoes ADD CONSTRAINT FK_Notificacoes_1
    FOREIGN KEY (id_usuario)
    REFERENCES Usuarios (id_usuario);
 
ALTER TABLE Ocorrencias ADD CONSTRAINT FK_Ocorrencias_4
    FOREIGN KEY (id_terceirizado)
    REFERENCES Terceirizados (id_terceirizado);
GO