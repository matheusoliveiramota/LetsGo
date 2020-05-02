using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Requests;
using LetsGo.Domain.Entities.Responses;
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

        public GetEstadoMesaResponse GetEstadoMesa(int codRestaurante, int porta)
        {
            var response = _repository.GetEstadoMesa(codRestaurante, porta);
            
            if(response.CodEstadoMesa == 3) // Não monitorado
            {
                response.CodEstadoMesa = 1;
            }

            return response;
        }

        public IEnumerable<Placa> GetPlacas()
        {
            return _repository.GetPlacas();
        }

        public short PostEstadoMesa(PostEstadoMesaRequest request)
        {
            var response = _repository.UpdateEstadoMesa(request.CodRestaurante,request.Porta,request.CodEstadoMesa);
            _repository.InsertLogMesaEstado(request.CodRestaurante,request.Porta,request.CodEstadoMesa);

            return response;
        }
    }
}
