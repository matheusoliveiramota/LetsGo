using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.UI.Services
{
    public class RestauranteServiceUI : ServiceUI, IRestauranteServiceUI
    {
        public RestauranteServiceUI(IConfiguration configuration) : base(configuration) { }
        public Restaurante GetByUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
