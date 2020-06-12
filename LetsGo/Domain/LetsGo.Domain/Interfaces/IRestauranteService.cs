using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IRestauranteService
    {
        List<Restaurante> BuscarRestaurantesProximos(List<Restaurante> restaurantes, double latitude, double longitude);
        void BuscarLatitudeLongitude(Endereco endereco);
    }
}
