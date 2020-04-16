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
        public Restaurante GetRestaurante(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Endereco InsertEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public void InsertMesas(IEnumerable<Mesa> mesa)
        {
            throw new NotImplementedException();
        }

        public Restaurante InsertRestaurante(Restaurante restaurante)
        {
            throw new NotImplementedException();
        }
    }
}
