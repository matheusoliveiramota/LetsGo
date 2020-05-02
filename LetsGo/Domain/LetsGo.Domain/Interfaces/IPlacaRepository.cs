using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IPlacaRepository
    {
        IEnumerable<Placa> GetPlacas();
        short UpdateEstadoMesa(int codRestaurante, short porta, short codEstadoMesa);
        void InsertLogMesaEstado(int codRestaurante, short porta, short codEstadoMesa);
        GetEstadoMesaResponse GetEstadoMesa(int codRestaurante, int porta);
    }
}
