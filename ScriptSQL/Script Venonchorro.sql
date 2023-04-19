create database db_venonchorro

go

use db_venonchorro

go

CREATE TABLE Ouvinte (
id_ouvinte INT PRIMARY KEY IDENTITY(1,1),
Nome VARCHAR(50) NOT NULL,
Apelido VARCHAR(20) NOT NULL,
Email VARCHAR(40) UNIQUE NOT NULL,
Senha VARCHAR(20) NOT NULL,
Foto VARBINARY(MAX)
)

go

CREATE TABLE Episodio (
id_episodio INT PRIMARY KEY IDENTITY (1,1),
Titulo VARCHAR(50) NOT NULL,
Descriçao VARCHAR(500) NOT NULL,
Duraçao TIME NOT NULL,
Link VARCHAR(200) NOT NULL,
Arte VARBINARY(MAX),
Data_Public DATETIME NOT NULL,
Qntde_Play DECIMAL(10),
Qntde_Download DECIMAL(10),
Participantes VARCHAR(500) NOT NULL
)

go

CREATE TABLE Comentario (
id_comentario INT PRIMARY KEY IDENTITY(1,1),
Conteudo VARCHAR(1000) NOT NULL,
Hora_Public DATETIME NOT NULL,
fk_id_episodio INT,
fk_id_ouvinte INT
)

go

ALTER TABLE Comentario ADD FOREIGN KEY(fk_id_episodio) REFERENCES Episodio (id_episodio)

go

ALTER TABLE Comentario ADD FOREIGN KEY(fk_id_ouvinte) REFERENCES Ouvinte (id_ouvinte)
