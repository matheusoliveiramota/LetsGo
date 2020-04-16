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
            return _conn.QueryScalar<Usuario>("SELECT CodUsuario,Nome,NomeDeUsuario FROM Usuario");
        }

        public Usuario InsertUsuario(Usuario usuario)
        {
            return _conn.QueryScalar<Usuario>(@"SET NOCOUNT ON
                                                INSERT INTO Usuario(Nome,NomeDeUsuario) VALUES(@Nome,@NomeDeUsuario)
                                                
                                                SELECT CodUsuario,Nome,NomeDeUsuario FROM Usuario",
                                                new { usuario.Nome, usuario.NomeDeUsuario });
        }
    }
}
