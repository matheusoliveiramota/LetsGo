using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Coordenada
    {
        private IRestauranteRepository restauranteRepository;

        public int CodCoordenada { get; set; }
        public int Esquerda { get; set; }
        public int Topo { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }

        public Coordenada(IRestauranteRepository restauranteRepository)
        {
            this.restauranteRepository = restauranteRepository;
        }

        public Coordenada() { }

        public void InserirCoordenada()
        {
            restauranteRepository.InserirCoordenada(this);
        }
    }
}
