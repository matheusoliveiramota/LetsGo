using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Requests;
using LetsGo.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IPlacaService
    {
        IEnumerable<Placa> GetPlacas();
        GetEstadoMesaResponse GetEstadoMesa(int codRestaurante, int porta);
        short PostEstadoMesa(PostEstadoMesaRequest request);
    }
}
