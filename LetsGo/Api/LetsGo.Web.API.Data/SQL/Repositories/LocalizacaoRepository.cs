using System.Collections.Generic;
using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;

namespace LetsGo.Web.API.Data.SQL.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly DataConnection _conn;

        public LocalizacaoRepository(DataConnection connection)
        {
            _conn = connection;
        }

        public IEnumerable<Estado> BuscarEstados()
        {
            return _conn.Query<Estado>("SELECT CodEstado,Nome,Sigla FROM Estado");
        }
    }
}