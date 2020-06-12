using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IRestauranteRepository
    {
        void InserirEndereco(Endereco endereco); 
        void InserirItemPlaca(ItemPlaca placa);
        void InserirRestaurante(Restaurante restaurante);
        void InserirCoordenada(Coordenada coordenada);
        void InserirMesas(Restaurante restaurante); 
        Restaurante BuscarRestaurante(string nomeDeUsuario);
        Endereco BuscarEnderecoPorRestaurante(int codRestaurante);
        ItemPlaca BuscarItemPlacaPorRestaurante(int codRestaurante);
        Restaurante BuscarRestaurante(int codRestaurante);    
        IEnumerable<Mesa> BuscarMesas(int codRestaurante);
        IEnumerable<Restaurante> BuscarRestaurantes();
        IEnumerable<Restaurante> BuscarRestaurantePorPesquisa(string busca);
    }
}
