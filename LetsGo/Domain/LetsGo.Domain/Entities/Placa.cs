using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Placa
    {
        private IPlacaRepository placaRepository;

        public int CodPlaca { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<Pino> Pinos { get; set; }

        public Placa(IPlacaRepository placaRepository)
        {
            this.placaRepository = placaRepository;
        }

        public Placa() { }

        public IEnumerable<Placa> BuscarPlacas()
        {
            return placaRepository.BuscarPlacas();
        }
    }
}
