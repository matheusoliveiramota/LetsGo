using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IRestauranteService
    {
        Restaurante InsertRestaurante(Restaurante restaurante);
        Restaurante GetRestaurante(string nomeDeUsuario);
        DateTime GetUltimaAlteracaoEstadoMesa(int codRestaurante);
        IEnumerable<Mesa> GetMesas(int codRestaurante);
    }
}
