SELECT ISNULL(MAX(M.DataAlteracaoEstado),0) AS DataAlteracaoEstado
		    FROM Mesa		   M
	INNER JOIN Restaurante     R ON R.CodRestaurante = M.CodRestaurante
WHERE R.CodRestaurante = 1

SELECT * FROM Mesa

UPDATE Mesa
	SET CodEstadoMesa = 3,DataAlteracaoEstado = NULL
WHERE CodMesa = 1


UPDATE Mesa
	SET CodEstadoMesa = 2,DataAlteracaoEstado = GETDATE()
WHERE CodMesa = 1