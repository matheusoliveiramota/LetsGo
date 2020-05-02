IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Usuarios')
BEGIN
	CREATE TABLE Usuario
	(
		CodUsuario INT PRIMARY KEY IDENTITY,
		Nome VARCHAR(200),
		NomeDeUsuario VARCHAR (100)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Estado')
BEGIN
	CREATE TABLE Estado
	(
		CodEstado INT PRIMARY KEY IDENTITY,
		Nome VARCHAR(100),
		Sigla VARCHAR(2)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Endereco')
BEGIN
	CREATE TABLE Endereco
	(
		CodEndereco INT PRIMARY KEY IDENTITY,
		CodEstado INT,
		Cidade VARCHAR(200),
		Cep VARCHAR(10),
		Rua VARCHAR(300),
		Bairro VARCHAR(200),
		Numero VARCHAR(50),
		Complemento VARCHAR(200),
		CONSTRAINT FK_Estado FOREIGN KEY (CodEstado) REFERENCES Estado(CodEstado)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Restaurante')
BEGIN
	CREATE TABLE Restaurante
	(
		CodRestaurante INT PRIMARY KEY IDENTITY,
		CodUsuario INT,
		CodEndereco INT,
		Nome VARCHAR(200),
		CONSTRAINT FK_Usuario FOREIGN KEY (CodUsuario) REFERENCES Usuario(CodUsuario),
		CONSTRAINT FK_Endereco FOREIGN KEY (CodEndereco) REFERENCES Endereco(CodEndereco)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Coordenada')
BEGIN
	CREATE TABLE Coordenada
	(
		CodCoordenada INT PRIMARY KEY IDENTITY,
		Esquerda INT,
		Topo INT,
		Largura INT,
		Altura INT
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Placa')
BEGIN
	CREATE TABLE Placa
	(
		CodPlaca INT PRIMARY KEY IDENTITY,
		Nome VARCHAR(200),
		Descricao VARCHAR(200)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'ItemPlaca')
BEGIN
	CREATE TABLE ItemPlaca
	(
		CodItemPlaca INT PRIMARY KEY IDENTITY,
		CodPlaca INT,
		CodRestaurante INT,
		CodigoDeBarras VARCHAR(200),
		CONSTRAINT FK_ItemPlaca_Placa FOREIGN KEY (CodPlaca) REFERENCES Placa(CodPlaca),
		CONSTRAINT FK_ItemaPlaca_Restaurante FOREIGN KEY (CodRestaurante) REFERENCES Restaurante(CodRestaurante)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Pino')
BEGIN
	CREATE TABLE Pino
	(
		CodPino INT PRIMARY KEY IDENTITY,
		CodPlaca INT,
		Numero VARCHAR(100),
		Porta INT
		CONSTRAINT FK_Placa FOREIGN KEY (CodPlaca) REFERENCES Placa(CodPlaca)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'EstadoMesa')
BEGIN
	CREATE TABLE EstadoMesa
	(
		CodEstadoMesa INT PRIMARY KEY IDENTITY,
		Estado VARCHAR(100)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'Mesa')
BEGIN
	CREATE TABLE Mesa
	(
		CodMesa INT PRIMARY KEY IDENTITY,
		CodRestaurante INT,
		CodCoordenada INT,
		CodPino INT,
		CodEstadoMesa INT,
		Numero INT,
		DataAlteracaoEstado DATETIME,
		CONSTRAINT FK_Restaurante FOREIGN KEY (CodRestaurante) REFERENCES Restaurante(CodRestaurante),
		CONSTRAINT FK_Coordenada FOREIGN KEY (CodCoordenada) REFERENCES Coordenada(CodCoordenada),
		CONSTRAINT FK_Pino FOREIGN KEY (CodPino) REFERENCES Pino(CodPino),
		CONSTRAINT FK_EstadoMesa FOREIGN KEY (CodEstadoMesa) REFERENCES EstadoMesa(CodEstadoMesa)
	)
END

IF NOT EXISTS(SELECT TOP 1 1 FROM sys.tables WHERE name = 'LogMesaEstado')
BEGIN
	CREATE TABLE LogMesaEstado
	(
		CodLogEstadoMesa INT PRIMARY KEY IDENTITY,
		CodMesa INT,
		CodEstadoMesa INT,
		[Data] DATETIME,
		CONSTRAINT FK_LogMesaEstado_EstadoMesa FOREIGN KEY (CodEstadoMesa) REFERENCES EstadoMesa(CodEstadoMesa),
		CONSTRAINT FK_LogMesaEstado_Mesa FOREIGN KEY (CodMesa) REFERENCES Mesa(CodMesa)
	)
END