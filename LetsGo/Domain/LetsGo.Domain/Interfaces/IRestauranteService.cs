﻿using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IRestauranteService
    {
        void InsertRestaurante(Restaurante restaurante);
        Restaurante GetRestaurante(string nomeDeUsuario);
    }
}
