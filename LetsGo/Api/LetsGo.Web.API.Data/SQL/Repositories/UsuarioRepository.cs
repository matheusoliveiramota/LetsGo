using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.API.Data.SQL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataConnection _conn;
        public UsuarioRepository(DataConnection connection)
        {
            _conn = connection;
        }
        public IEnumerable<Estado> GetEstados()
        {
            return _conn.Query<Estado>("SELECT CodEstado,Nome,Sigla FROM Estado");
        }

        public Usuario GetUsuario(string nomeDeUsuario)
        {
            var usuario = _conn.QueryScalar<Usuario>("SELECT CodUsuario, Nome,NomeDeUsuario FROM Usuario WHERE NomeDeUsuario = @NomeDeUsuario"
                                             ,new { nomeDeUsuario });
        
            return usuario;
        }

        public Usuario InsertUsuario(Usuario usuario)
        {
            usuario.CodUsuario = _conn.QueryScalar<int>(@"INSERT INTO Usuario(Nome,NomeDeUsuario) VALUES(@Nome,@NomeDeUsuario)
                            
                                                          SELECT SCOPE_IDENTITY() AS CodUsuario"
                                                        ,new { usuario.Nome, usuario.NomeDeUsuario });

            return usuario;
        }
    }
}
