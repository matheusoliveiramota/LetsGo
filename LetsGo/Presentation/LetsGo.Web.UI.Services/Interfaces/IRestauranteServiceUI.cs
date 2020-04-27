using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.UI.Services.Interfaces
{
    public interface IRestauranteServiceUI
    {
        Restaurante GetByUsuario(Usuario usuario);
        Restaurante InsertRestaurante(RestauranteUI usuario);
        bool ExisteAlteracao(int codRestaurante, DateTime dataUltimaAtualizacao);
        IEnumerable<Mesa> GetMesas(int codRestaurante);
    }
}
