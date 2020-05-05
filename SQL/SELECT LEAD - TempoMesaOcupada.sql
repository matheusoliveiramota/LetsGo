SELECT CodMesa
	  ,CodEstadoMesa
	  ,Data
	  ,DataProximoEstado
	  ,DATEDIFF(SECOND,DATA,DataProximoEstado) TempoMesaOcupada 
 FROM (
	SELECT CodMesa
		  ,CodEstadoMesa
		  ,Data
		  ,LEAD(Data) OVER(PARTITION BY CodMesa ORDER BY CodMesa,Data) As DataProximoEstado 
	FROM LogMesaEstado
) LogMesa