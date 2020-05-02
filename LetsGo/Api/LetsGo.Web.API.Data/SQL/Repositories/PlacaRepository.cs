using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Responses;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.API.Data.SQL.Repositories
{
    public class PlacaRepository : IPlacaRepository
    {
        private readonly DataConnection _conn;

        public PlacaRepository(DataConnection connection)
        {
            _conn = connection;
        }

        public GetEstadoMesaResponse GetEstadoMesa(int codRestaurante, int porta)
        {
           var response = _conn.QueryScalar<GetEstadoMesaResponse>(@"SELECT P.Porta,M.CodestadoMesa
                                                                            FROM Mesa	M
                                                                        INNER JOIN Pino P ON M.CodPino = P.CodPino
                                                                    WHERE   M.CodRestaurante = @CodRestaurante
                                                                        AND P.Porta = @Porta"
                                            ,new {   codRestaurante
                                                    ,porta 
                                            });

            return response;
        }

        public IEnumerable<Placa> GetPlacas()
        {
            IEnumerable<Placa> placas = _conn.Query<Placa>("SELECT CodPlaca,Nome,Descricao FROM Placa");
            foreach(var placa in placas)
            {
                placa.Pinos = _conn.Query<Pino>("SELECT CodPino,Numero FROM Pino WHERE CodPlaca = @CodPlaca", new { placa.CodPlaca });
            }
            return placas;
        }

        public void InsertLogMesaEstado(int codRestaurante, short porta, short codEstadoMesa)
        {
            _conn.Execute(@"INSERT INTO LogMesaEstado(CodMesa,CodEstadoMesa,Data)
                            SELECT M.CodMesa,@CodEstadoMesa,GETDATE()
                                    FROM Mesa	M
                                INNER JOIN Pino P ON P.CodPino = M.CodPino
                            WHERE   P.Porta = @Porta
                                AND M.CodRestaurante = @CodRestaurante"
            , new {
                codRestaurante,
                porta,
                codEstadoMesa
            });
        }

        public short UpdateEstadoMesa(int codRestaurante, short porta, short codEstadoMesa)
        {
            _conn.Execute(@"UPDATE M
                                SET M.CodEstadoMesa = @CodEstadoMesa,
                                    M.DataAlteracaoEstado = GETDATE()
                                      FROM Mesa M
                                INNER JOIN Pino	P ON M.CodPino = P.CodPino 
                            WHERE    M.CodRestaurante = @CodRestaurante
                                AND P.Porta = @Porta"
                            , new {
                                codRestaurante,
                                porta,
                                codEstadoMesa
                            });

            return codEstadoMesa;
        }
    }
}
