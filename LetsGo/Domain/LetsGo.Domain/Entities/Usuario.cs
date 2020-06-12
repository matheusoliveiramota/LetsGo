using LetsGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Usuario
    {
        private IUsuarioRepository usuarioRepository;

        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string NomeDeUsuario { get; set; }

        public Usuario(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario() { }

        public void InserirUsuario()
        {
            usuarioRepository.InserirUsuario(this);
        }

        public void InserirUsuario(IUsuarioRepository repository)
        {
            repository.InserirUsuario(this);
        }

        public Usuario BuscarUsuario(string nomeDeUsuario)
        {
            return usuarioRepository.BuscarUsuario(nomeDeUsuario);
        }
    }
}
