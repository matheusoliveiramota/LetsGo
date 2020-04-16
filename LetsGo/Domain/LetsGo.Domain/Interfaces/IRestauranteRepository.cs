﻿using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IRestauranteRepository
    {
        Endereco InsertEndereco(Endereco endereco); 
        Restaurante InsertRestaurante(Restaurante restaurante);
        void InsertMesas(IEnumerable<Mesa> mesa); 
        Restaurante GetRestaurante(Usuario usuario);
    }
}
