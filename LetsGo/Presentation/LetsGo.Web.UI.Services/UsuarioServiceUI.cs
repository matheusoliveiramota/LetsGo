using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.UI.Services
{
    public class UsuarioServiceUI : ServiceUI, IUsuarioServiceUI
    {
        public UsuarioServiceUI(IConfiguration configuration) : base(configuration) { }
        public Usuario GetByNomeDeUsuario(string nomeDeUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario InsertUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
