using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Endereco
    {
        private IRestauranteRepository restauranteRepository;

        public int CodEndereco { get; set; }
        public Estado Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Endereco(IRestauranteRepository restauranteRepository)
        {
            this.restauranteRepository = restauranteRepository;
        }

        public Endereco() { }

        public void InserirEndereco()
        {
            restauranteRepository.InserirEndereco(this);
        }

        public void InserirEndereco(IRestauranteRepository repository)
        {
            repository.InserirEndereco(this);
        }

        public void BuscarLatitudeLongitude(IRestauranteService restauranteService)
        {
            restauranteService.BuscarLatitudeLongitude(this);
        }

        public Endereco BuscarEnderecoPorRestaurante(int codRestaurante)
        {
           return restauranteRepository.BuscarEnderecoPorRestaurante(codRestaurante);
        }
    }
}
