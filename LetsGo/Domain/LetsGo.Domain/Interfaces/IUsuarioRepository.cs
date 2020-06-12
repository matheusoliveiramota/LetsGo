using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario BuscarUsuario(string nomeDeUsuario);
        void InserirUsuario(Usuario usuario);
    }
}
