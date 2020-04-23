using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.API.Services
{
    public class RestauranteService : IRestauranteService
    {
        protected readonly IRestauranteRepository _restauranteRepository;
        protected readonly IUsuarioRepository _usuarioRepository;

        public RestauranteService(IRestauranteRepository repository, IUsuarioRepository usuarioRepository)
        {
            _restauranteRepository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public Restaurante GetRestaurante(string nomeDeUsuario)
        {
            return _restauranteRepository.GetRestaurante(nomeDeUsuario);
        }

        public Restaurante InsertRestaurante(Restaurante restaurante)
        {
            restaurante.Usuario = _usuarioRepository.GetUsuario(restaurante.Usuario.NomeDeUsuario);
            restaurante.Endereco = _restauranteRepository.InsertEndereco(restaurante.Endereco);
            restaurante = _restauranteRepository.InsertRestaurante(restaurante);
            _restauranteRepository.InsertItemPlaca(restaurante);
            _restauranteRepository.InsertMesas(restaurante);

            return _restauranteRepository.GetRestaurante(restaurante.CodRestaurante);
        }
    }
}
