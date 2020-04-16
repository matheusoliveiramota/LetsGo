using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Usuario GetUsuario(string nomeDeUsuario);
        Usuario InsertUsuario(Usuario usuario);
        IEnumerable<Estado> GetEstados();
    }
}
