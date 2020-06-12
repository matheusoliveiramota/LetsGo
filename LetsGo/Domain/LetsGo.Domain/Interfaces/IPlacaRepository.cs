using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IPlacaRepository
    {
        IEnumerable<Placa> BuscarPlacas();
        short AtualizarEstadoMesa(int codRestaurante, short porta, short codEstadoMesa);
        void InserirLogMesaEstado(int codRestaurante, short porta, short codEstadoMesa);
        GetEstadoMesaResponse BuscarEstadoMesa(int codRestaurante, short porta);
    }
}
