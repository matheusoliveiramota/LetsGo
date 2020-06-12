using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Restaurante
    {
        private IRestauranteRepository restauranteRepository;

        public int CodRestaurante { get; set; }
        public ItemPlaca ItemPlaca { get; set; }
        public string NomeImagem { get; set; }
        public Usuario Usuario { get; set; }
        public Endereco Endereco { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Mesa> Mesas { get; set; }
        public DistanciaRestaurante DistanciaRestaurante { get; set; }

        public Restaurante(IRestauranteRepository restauranteRepository)
        {
            this.restauranteRepository = restauranteRepository;
        }

        public Restaurante() { }

        public void InserirRestaurante()
        {
            restauranteRepository.InserirRestaurante(this);
        }

        public void InserirRestaurante(IRestauranteRepository repository)
        {
            this.restauranteRepository = repository;

            Endereco.InserirEndereco(repository);
            ItemPlaca.InserirItemPlaca(repository);
            repository.InserirRestaurante(this);
            InserirMesas();
        }

        public void InserirMesas()
        {
            restauranteRepository.InserirMesas(this);
        }

        public Restaurante BuscarRestaurante(string nomeDeUsuario)
        {
            return restauranteRepository.BuscarRestaurante(nomeDeUsuario);
        }
        public Restaurante BuscarRestaurante(int codRestaurante)
        {
            return restauranteRepository.BuscarRestaurante(codRestaurante);
        }

        public IEnumerable<Restaurante> BuscarRestaurantes()
        {
            return restauranteRepository.BuscarRestaurantes();
        }

        public IEnumerable<Restaurante> BuscarRestaurantesPorPesquisa(string busca)
        {
            return restauranteRepository.BuscarRestaurantePorPesquisa(busca);
        }
    }
}
