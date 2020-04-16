using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace LetsGo.Web.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _repository = usuarioRepository;
        }
        public IEnumerable<Estado> GetEstados()
        {
            return _repository.GetEstados();
        }

        public Usuario GetUsuario(string nomeDeUsuario)
        {
            return _repository.GetUsuario(nomeDeUsuario);
        }

        public Usuario InsertUsuario(Usuario usuario)
        {
            return _repository.InsertUsuario(usuario);
        }
    }
}
