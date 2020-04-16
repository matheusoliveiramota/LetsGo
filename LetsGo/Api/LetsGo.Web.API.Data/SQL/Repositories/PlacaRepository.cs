using LetsGo.Domain.Entities;
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
        public IEnumerable<Placa> GetPlacas()
        {
            IEnumerable<Placa> placas = _conn.Query<Placa>("SELECT CodPlaca,Nome FROM Placa");
            foreach(var placa in placas)
            {
                placa.Pinos = _conn.Query<Pino>("SELECT CodPino,Numero FROM Pino");
            }
            return placas;
        }
    }
}
