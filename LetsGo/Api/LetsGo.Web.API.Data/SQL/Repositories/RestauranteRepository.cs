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
        public Restaurante BuscarRestaurante(string nomeDeUsuario)
        {
            var restaurante = _conn.QueryScalar<Restaurante>(@"SELECT R.CodRestaurante, R.Nome, R.NomeImagem
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

                restaurante.Endereco = BuscarEnderecoPorRestaurante(restaurante.CodRestaurante);

                restaurante.ItemPlaca = BuscarItemPlacaPorRestaurante(restaurante.CodRestaurante);

                restaurante.Mesas = BuscarMesas(restaurante.CodRestaurante);
            }

            return restaurante;
        }
        public IEnumerable<Restaurante> BuscarRestaurantes()
        {
            var restaurantes = _conn.Query<Restaurante>(@"SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                                                            FROM Restaurante  R");
            if (restaurantes != null)
            {
                foreach(var restaurante in restaurantes)
                {
                    restaurante.Usuario = _conn.QueryScalar<Usuario>(@"SELECT U.CodUsuario, U.Nome, U.NomeDeUsuario
                                                                        FROM Restaurante  R
                                                                    INNER JOIN Usuario      U ON R.CodUsuario = U.CodUsuario
                                                                    WHERE R.CodRestaurante = @CodRestaurante"
                                                                , new { restaurante.CodRestaurante });

                    restaurante.Endereco = BuscarEnderecoPorRestaurante(restaurante.CodRestaurante);

                    restaurante.ItemPlaca = BuscarItemPlacaPorRestaurante(restaurante.CodRestaurante);

                    restaurante.Mesas = BuscarMesas(restaurante.CodRestaurante);
                }
            }

            return restaurantes;
        }

        public Restaurante BuscarRestaurante(int codRestaurante)
        {
            var restaurante = _conn.QueryScalar<Restaurante>(@"SELECT R.CodRestaurante, R.Nome, R.NomeImagem
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

                restaurante.Endereco = BuscarEnderecoPorRestaurante(restaurante.CodRestaurante);

                restaurante.ItemPlaca = BuscarItemPlacaPorRestaurante(restaurante.CodRestaurante);

                restaurante.Mesas = BuscarMesas(restaurante.CodRestaurante);
            }

            return restaurante;
        }

        public ItemPlaca BuscarItemPlacaPorRestaurante(int codRestaurante)
        {
            var itemPlaca = _conn.QueryScalar<ItemPlaca>(@"SELECT I.CodItemPlaca, I.CodigoDeBarras 
                                                                 FROM Restaurante  R
                                                           INNER JOIN ItemPlaca    I ON R.CodItemPlaca = I.CodItemPlaca
                                                           WHERE R.CodRestaurante = @CodRestaurante"
                                                          , new { codRestaurante });

            if(itemPlaca != null)
            {
                itemPlaca.Placa = _conn.QueryScalar<Placa>(@"SELECT P.CodPlaca, P.Nome, P.Descricao
                                                                    FROM Placa        P
                                                            INNER JOIN ItemPlaca      I ON I.CodPlaca = P.CodPlaca
                                                            WHERE I.CodItemPlaca = @CodItemPlaca"
                                                            , new { itemPlaca.CodItemPlaca });
            }

            return itemPlaca;
        }

        public void InserirEndereco(Endereco endereco)
        {
            endereco.CodEndereco = _conn.QueryScalar<int>(@"INSERT INTO Endereco(CodEstado,Cidade,Cep,Rua,Bairro,Numero,Complemento,Latitude,Longitude)
                                                            VALUES (@CodEstado,@Cidade,@Cep,@Rua,@Bairro,@Numero,@Complemento,@Latitude,@Longitude)

                                                            SELECT SCOPE_IDENTITY() AS CodEndereco"
                                                           , new { endereco.Estado.CodEstado
                                                                  ,endereco.Cidade
                                                                  ,endereco.Cep
                                                                  ,endereco.Rua
                                                                  ,endereco.Bairro
                                                                  ,endereco.Numero
                                                                  ,endereco.Complemento
                                                                  ,endereco.Latitude
                                                                  ,endereco.Longitude}
                                                           );
        }

        public void InserirItemPlaca(ItemPlaca itemPlaca)
        {
            itemPlaca.CodItemPlaca = _conn.QueryScalar<int>(@"INSERT INTO ItemPlaca(CodPlaca)
                                                            VALUES (@CodPlaca)
                                                        
                                                            SELECT SCOPE_IDENTITY() AS CodItemPlaca"
                                                            , new {
                                                                itemPlaca.Placa.CodPlaca
                                                            });
        }

        public void InserirMesas(Restaurante restaurante)
        {
            foreach(var mesa in restaurante.Mesas)
            {
                InserirCoordenada(mesa.Coordenada);
                _conn.Execute(@"INSERT INTO Mesa(CodRestaurante,CodCoordenada,CodPino,CodEstadoMesa,Numero)
                                VALUES(@CodRestaurante,@CodCoordenada,@CodPino,@CodEstadoMesa,@Numero)"
                                , new
                                {
                                    restaurante.CodRestaurante,
                                    mesa.Coordenada.CodCoordenada,
                                    mesa.Pino.CodPino,
                                    CodEstadoMesa = 3,
                                    mesa.Numero
                                });
            }
        }

        public void InserirRestaurante(Restaurante restaurante)
        {
            restaurante.CodRestaurante = _conn.QueryScalar<int>(@"INSERT INTO Restaurante(CodUsuario,CodEndereco,Nome,CodItemPlaca,NomeImagem)
                                                                  VALUES(@CodUsuario,@CodEndereco,@Nome,@CodItemPlaca,@NomeImagem)
                                                                  
                                                                  SELECT SCOPE_IDENTITY() AS CodRestaurante"
                                                                , new { 
                                                                  restaurante.Usuario.CodUsuario,
                                                                  restaurante.Endereco.CodEndereco,
                                                                  restaurante.Nome,
                                                                  restaurante.ItemPlaca.CodItemPlaca,
                                                                  restaurante.NomeImagem
                                                                });
        }

        public Endereco BuscarEnderecoPorRestaurante(int codRestaurante)
        {
            var endereco = _conn.QueryScalar<Endereco>(@"SELECT ED.CodEndereco,ED.Cep,ED.Cidade,ED.Bairro,ED.Rua,ED.Numero,ED.Complemento,ED.Latitude,ED.Longitude
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
        public IEnumerable<Mesa> BuscarMesas(int codRestaurante)
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
        public void InserirCoordenada(Coordenada coordenada)
        {
            coordenada.CodCoordenada = _conn.QueryScalar<int>(@"DECLARE @CodCoordenada AS INT = 0

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
        }

        public IEnumerable<Restaurante> BuscarRestaurantePorPesquisa(string busca)
        {
            var restaurantes = _conn.Query<Restaurante>(@"
                SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                    FROM Restaurante  R
                WHERE Nome LIKE '%' + @busca + '%'

                UNION

                SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                        FROM Restaurante  R
                    INNER JOIN Endereco		E ON R.CodEndereco = E.CodEndereco
                WHERE E.Rua LIKE '%' + @busca + '%'

                UNION

                SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                        FROM Restaurante  R
                    INNER JOIN Endereco		E ON R.CodEndereco = E.CodEndereco
                WHERE E.Bairro LIKE '%' + @busca + '%'

                UNION

                SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                        FROM Restaurante  R
                    INNER JOIN Endereco		E ON R.CodEndereco = E.CodEndereco
                WHERE E.Cidade LIKE '%' + @busca + '%'

                UNION

                SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                        FROM Restaurante  R
                    INNER JOIN Endereco		E ON R.CodEndereco = E.CodEndereco
                    INNER JOIN Estado		T ON T.CodEstado = E.CodEstado
                WHERE T.Nome LIKE '%' + @busca + '%'

                UNION

                SELECT R.CodRestaurante, R.Nome, R.NomeImagem
                        FROM Restaurante  R
                    INNER JOIN Endereco		E ON R.CodEndereco = E.CodEndereco
                    INNER JOIN Estado		T ON T.CodEstado = E.CodEstado
                WHERE T.Sigla LIKE '%' + @busca + '%'"
            ,new { busca });

            if (restaurantes != null)
            {
                foreach(var restaurante in restaurantes)
                {
                    restaurante.Usuario = _conn.QueryScalar<Usuario>(@"SELECT U.CodUsuario, U.Nome, U.NomeDeUsuario
                                                                        FROM Restaurante  R
                                                                    INNER JOIN Usuario      U ON R.CodUsuario = U.CodUsuario
                                                                    WHERE R.CodRestaurante = @CodRestaurante"
                                                                , new { restaurante.CodRestaurante });

                    restaurante.Endereco = BuscarEnderecoPorRestaurante(restaurante.CodRestaurante);

                    restaurante.ItemPlaca = BuscarItemPlacaPorRestaurante(restaurante.CodRestaurante);

                    restaurante.Mesas = BuscarMesas(restaurante.CodRestaurante);
                }
            }

            return restaurantes;
        }
    }
}
