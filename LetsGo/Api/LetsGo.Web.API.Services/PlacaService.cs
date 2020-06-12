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

        public GetEstadoMesaResponse GetEstadoMesa(int codRestaurante, short porta)
        {
            var response = _repository.BuscarEstadoMesa(codRestaurante, porta);
            
            if(response != null && response.CodEstadoMesa == 3) // Não monitorado
            {
                response.CodEstadoMesa = 1;
            }

            return response;
        }

        public short PostEstadoMesa(PostEstadoMesaRequest request)
        {
            var response = _repository.AtualizarEstadoMesa(request.CodRestaurante,request.Porta,request.CodEstadoMesa);
            _repository.InserirLogMesaEstado(request.CodRestaurante,request.Porta,request.CodEstadoMesa);

            return response;
        }
    }
}
