using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.UI.Services.Interfaces
{
    public interface IUsuarioServiceUI
    {
        Usuario GetByNomeDeUsuario(string nomeDeUsuario);
        Usuario InsertUsuario(Usuario usuario);
        IEnumerable<Estado> GetEstados();
    }
}
