using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class ItemPlaca
    {
        private IRestauranteRepository restauranteRepository;

        public int CodItemPlaca { get; set; }
        public Placa Placa { get; set; }
        public string CodigoDeBarras { get; set; }

        public ItemPlaca(IRestauranteRepository restauranteRepository)
        {
            this.restauranteRepository = restauranteRepository;
        }
        public ItemPlaca() { }

        public void InserirItemPlaca()
        {
            restauranteRepository.InserirItemPlaca(this);
        }

        public void InserirItemPlaca(IRestauranteRepository repository)
        {
            repository.InserirItemPlaca(this);
        }

        public ItemPlaca BuscarItemPlacaPorRestaurante(int codRestaurante)
        {
            return restauranteRepository.BuscarItemPlacaPorRestaurante(codRestaurante);
        }
    }
}