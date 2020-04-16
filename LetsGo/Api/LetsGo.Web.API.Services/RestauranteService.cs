using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.API.Services
{
    public class RestauranteService : IRestauranteService
    {
        protected readonly IRestauranteRepository _repository;

        public RestauranteService(IRestauranteRepository repository)
        {
            _repository = repository;
        }

        public Restaurante GetRestaurante(string nomeDeUsuario)
        {
            throw new NotImplementedException();
        }

        public void InsertRestaurante(Restaurante restaurante)
        {
            _repository.InsertRestaurante(new Restaurante());
        }
    }
}
