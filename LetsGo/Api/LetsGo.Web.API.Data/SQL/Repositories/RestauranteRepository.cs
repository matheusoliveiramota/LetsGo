using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.API.Data.SQL.Repositories
{
    public class RestauranteRepository : IRestauranteRepository
    {
        private readonly DataConnection _conn;

        public RestauranteRepository(DataConnection connection)
        {
            _conn = connection;
        }
        public Restaurante GetRestaurante(string nomeDeUsuario)
        {
            var restaurante = _conn.QueryScalar<Restaurante>(@"SELECT R.CodRestaurante, R.Nome 
                                                                       FROM Restaurante  R
                                                                 INNER JOIN Usuario      U ON R.CodUsuario = U.CodUsuario
                                                                WHERE U.NomeDeUsuario = @NomeDeUsuario"
                                                            , new { nomeDeUsuario });
            if(restaurante != null)
            {
                restaurante.Usuario = _conn.QueryScalar<Usuario>(@"SELECT U.CodUsuario, U.Nome, U.NomeDeUsuario
                                                                       FROM Restaurante  R
                                                                 INNER JOIN Usuario      U ON R.CodUsuario = U.CodUsuario
                                                                WHERE R.CodRestaurante = @CodRestaurante"
                                                            , new { restaurante.CodRestaurante });

                restaurante.Endereco = GetEnderecoPorRestaurante(restaurante.CodRestaurante);

                restaurante.Placa = GetPlacaPorRestaurante(restaurante.CodRestaurante);

                restaurante.Mesas = GetMesasPorRestaurante(restaurante.CodRestaurante);
            }

            return restaurante;
        }

        public Restaurante GetRestaurante(int codRestaurante)
        {
            var restaurante = _conn.QueryScalar<Restaurante>(@"SELECT R.CodRestaurante, R.Nome 
                                                                       FROM Restaurante  R
                                                                WHERE R.CodRestaurante = @CodRestaurante"
                                                            , new { codRestaurante });
            if (restaurante != null)
            {
                restaurante.Usuario = _conn.QueryScalar<Usuario>(@"SELECT U.CodUsuario, U.Nome, U.NomeDeUsuario
                                                                       FROM Restaurante  R
                                                                 INNER JOIN Usuario      U ON R.CodUsuario = U.CodUsuario
                                                                WHERE R.CodRestaurante = @CodRestaurante"
                                                            , new { restaurante.CodRestaurante });

                restaurante.Endereco = GetEnderecoPorRestaurante(restaurante.CodRestaurante);

                restaurante.Placa = GetPlacaPorRestaurante(restaurante.CodRestaurante);

                restaurante.Mesas = GetMesasPorRestaurante(restaurante.CodRestaurante);
            }

            return restaurante;
        }

        private ItemPlaca GetPlacaPorRestaurante(int codRestaurante)
        {
            var itemPlaca = _conn.QueryScalar<ItemPlaca>(@"SELECT I.CodItemPlaca, I.CodigoDeBarras 
                                                                 FROM Restaurante  R
                                                           INNER JOIN ItemPlaca    I ON R.CodRestaurante = I.CodRestaurante 
                                                           WHERE R.CodRestaurante = @CodRestaurante"
                                                          , new { codRestaurante });

            itemPlaca.Placa = _conn.QueryScalar<Placa>(@"SELECT P.CodPlaca, P.Nome, P.Descricao
                                                                 FROM Placa        P
                                                           INNER JOIN ItemPlaca    I ON I.CodPlaca = P.CodPlaca
                                                           WHERE I.CodItemPlaca = @CodItemPlaca"
                                                          , new { itemPlaca.CodItemPlaca });

            return itemPlaca;
        }

        public Endereco InsertEndereco(Endereco endereco)
        {
            endereco.CodEndereco = _conn.QueryScalar<int>(@"INSERT INTO Endereco(CodEstado,Cidade,Cep,Rua,Bairro,Numero,Complemento)
                                                            VALUES (@CodEstado,@Cidade,@Cep,@Rua,@Bairro,@Numero,@Complemento)

                                                            SELECT SCOPE_IDENTITY() AS CodEndereco"
                                                           , new { endereco.Estado.CodEstado
                                                                  ,endereco.Cidade
                                                                  ,endereco.Cep
                                                                  ,endereco.Rua
                                                                  ,endereco.Bairro
                                                                  ,endereco.Numero
                                                                  ,endereco.Complemento}
                                                           );
            return endereco;
        }

        public void InsertItemPlaca(Restaurante restaurante)
        {
            _conn.Execute(@"INSERT INTO ItemPlaca(CodPlaca,CodRestaurante)
                            VALUES (@CodPlaca,@CodRestaurante)"
                            , new {
                                restaurante.Placa.Placa.CodPlaca,
                                restaurante.CodRestaurante
                            });
        }

        public void InsertMesas(Restaurante restaurante)
        {
            foreach(var mesa in restaurante.Mesas)
            {
                var codCoordenada = InsertCoordenada(mesa.Coordenada);
                _conn.Execute(@"INSERT INTO Mesa(CodRestaurante,CodCoordenada,CodPino,CodEstadoMesa,Numero)
                                VALUES(@CodRestaurante,@CodCoordenada,@CodPino,@CodEstadoMesa,@Numero)"
                                , new
                                {
                                    restaurante.CodRestaurante,
                                    codCoordenada,
                                    mesa.Pino.CodPino,
                                    CodEstadoMesa = 3,
                                    mesa.Numero
                                });
            }
                                  
        }

        public Restaurante InsertRestaurante(Restaurante restaurante)
        {
            restaurante.CodRestaurante = _conn.QueryScalar<int>(@"INSERT INTO Restaurante(CodUsuario,CodEndereco,Nome)
                                                                  VALUES(@CodUsuario,@CodEndereco,@Nome)
                                                                  
                                                                  SELECT SCOPE_IDENTITY() AS CodRestaurante"
                                                                , new { 
                                                                  restaurante.Usuario.CodUsuario,
                                                                  restaurante.Endereco.CodEndereco,
                                                                  restaurante.Nome
                                                                });
            return restaurante;
        }

        private Endereco GetEnderecoPorRestaurante(int codRestaurante)
        {
            var endereco = _conn.QueryScalar<Endereco>(@"SELECT ED.CodEndereco,ED.Cep,ED.Cidade,ED.Bairro,ED.Rua,ED.Numero,ED.Complemento
		                                                          FROM Restaurante	RS
	                                                        INNER JOIN Endereco		ED ON ED.CodEndereco = RS.CodEndereco
	                                                        INNER JOIN Estado		ES ON ES.CodEstado = ED.CodEstado
                                                        WHERE RS.CodRestaurante = @CodRestaurante"
                                                       ,new { codRestaurante });

            endereco.Estado = _conn.QueryScalar<Estado>(@"SELECT ES.CodEstado,ES.Nome,ES.Sigla
		                                                          FROM Endereco		ED
	                                                        INNER JOIN Estado		ES ON ES.CodEstado = ED.CodEstado
                                                        WHERE ED.CodEndereco = @CodEndereco"
                                                        ,new { endereco.CodEndereco });
            return endereco;
        }
        private IEnumerable<Mesa> GetMesasPorRestaurante(int codRestaurante)
        {
            var mesas = _conn.Query<Mesa>(@"SELECT M.CodMesa,M.CodEstadoMesa AS Estado,M.DataAlteracaoEstado,M.Numero
		                                                FROM Mesa		   M
	                                            INNER JOIN Restaurante     R ON R.CodRestaurante = M.CodRestaurante
                                            WHERE R.CodRestaurante = @CodRestaurante"
                                          , new { codRestaurante });

            foreach(var mesa in mesas)
            {
                mesa.Coordenada = _conn.QueryScalar<Coordenada>(@"SELECT C.CodCoordenada,C.Altura,C.Largura,C.Esquerda,C.Topo
		                                                                    FROM Mesa		   M
	                                                                INNER JOIN Coordenada  C ON C.CodCoordenada = M.CodCoordenada
                                                                WHERE M.CodMesa = @CodMesa"
                                                                , new { mesa.CodMesa });

                mesa.Pino = _conn.QueryScalar<Pino>(@"SELECT P.CodPino,P.Numero
		                                                        FROM Mesa		   M
	                                                      INNER JOIN Pino		   P ON P.CodPino = M.CodPino
                                                    WHERE M.CodMesa = @CodMesa"
                                                    ,new { mesa.CodMesa });
            }

            return mesas;
        }
        private int InsertCoordenada(Coordenada coordenada)
        {
            var codCoordenada = _conn.QueryScalar<int>(@"DECLARE @CodCoordenada AS INT = 0

                                                          SELECT TOP 1 @CodCoordenada = CodCoordenada
	                                                          FROM Coordenada
                                                          WHERE	Esquerda = @Esquerda
	                                                          AND Topo = @Topo
	                                                          AND Largura = @Largura
	                                                          AND Altura = @Altura

                                                          IF @CodCoordenada > 0
                                                          BEGIN
	                                                          SELECT @CodCoordenada FROM Coordenada
                                                          END
                                                          ELSE
                                                          BEGIN
	                                                          INSERT INTO Coordenada(Esquerda,Topo,Largura,Altura)
	                                                          VALUES (@Esquerda,@Topo,@Largura,@Altura)

                                                              SELECT SCOPE_IDENTITY() AS CodCoordenada
                                                          END"
                                                          , new
                                                          {
                                                              coordenada.Esquerda,
                                                              coordenada.Topo,
                                                              coordenada.Largura,
                                                              coordenada.Altura
                                                          });
            return codCoordenada;
        }
    }
}
