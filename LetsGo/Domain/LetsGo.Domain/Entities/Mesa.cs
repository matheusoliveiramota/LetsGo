using LetsGo.Domain.Enums;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Mesa
    {
        private readonly IRestauranteRepository restauranteRepository;

        public int CodMesa { get; set; }
        public Coordenada Coordenada { get; set; }
        public Pino Pino { get; set; }
        public int Numero { get; set; }
        public EstadoMesa Estado { get; set; }
        public DateTime DataAlteracaoEstado { get; set; }

        public Mesa(IRestauranteRepository restauranteRepository)
        {
            this.restauranteRepository = restauranteRepository;
        }

        public Mesa() { }

        public IEnumerable<Mesa> BuscarMesas(int codRestaurante)
        {
            return restauranteRepository.BuscarMesas(codRestaurante);
        }
    }
}
