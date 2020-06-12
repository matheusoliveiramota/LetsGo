using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;

namespace LetsGo.Web.API.Data.SQL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataConnection _conn;
        public UsuarioRepository(DataConnection connection)
        {
            _conn = connection;
        }

        public Usuario BuscarUsuario(string nomeDeUsuario)
        {
            var usuario = _conn.QueryScalar<Usuario>("SELECT CodUsuario, Nome,NomeDeUsuario FROM Usuario WHERE NomeDeUsuario = @NomeDeUsuario"
                                             ,new { nomeDeUsuario });
        
            return usuario;
        }

        public void InserirUsuario(Usuario usuario)
        {
            usuario.CodUsuario = _conn.QueryScalar<int>(@"INSERT INTO Usuario(Nome,NomeDeUsuario) VALUES(@Nome,@NomeDeUsuario)
                            
                                                          SELECT SCOPE_IDENTITY() AS CodUsuario"
                                                        ,new { usuario.Nome, usuario.NomeDeUsuario });

        }
    }
}
