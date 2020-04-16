using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.API.Services
{
    public class PlacaService : IPlacaService
    {
        private readonly IPlacaRepository _repository;
        public PlacaService(IPlacaRepository placaRepository)
        {
            _repository = placaRepository;
        }
        public IEnumerable<Placa> GetPlacas()
        {
            return _repository.GetPlacas();
        }
    }
}
