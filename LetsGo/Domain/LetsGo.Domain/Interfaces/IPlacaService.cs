using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Requests;
using LetsGo.Domain.Entities.Responses;
using System.Collections.Generic;

namespace LetsGo.Domain.Interfaces
{
    public interface IPlacaService
    {
        GetEstadoMesaResponse GetEstadoMesa(int codRestaurante, short porta);
        short PostEstadoMesa(PostEstadoMesaRequest request);
    }
}
