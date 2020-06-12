DELETE FROM LogMesaEstado
DBCC CHECKIDENT ('dbo.LogMesaEstado', RESEED, 0)

DELETE FROM Mesa 
DBCC CHECKIDENT ('dbo.Mesa', RESEED, 0)

DELETE FROM Restaurante 
DBCC CHECKIDENT ('dbo.Restaurante', RESEED, 0)

DELETE FROM ItemPlaca 
DBCC CHECKIDENT ('dbo.ItemPlaca', RESEED, 0)

DELETE FROM Endereco 
DBCC CHECKIDENT ('dbo.Endereco', RESEED, 0)

DELETE FROM Usuario 
DBCC CHECKIDENT ('dbo.Usuario', RESEED, 0)

DELETE FROM Coordenada 
DBCC CHECKIDENT ('dbo.Coordenada', RESEED, 0)