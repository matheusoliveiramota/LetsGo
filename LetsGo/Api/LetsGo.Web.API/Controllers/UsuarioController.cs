using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{nomeDeUsuario}")]
        public ActionResult<Usuario> Get(string nomeDeUsuario)
        {
            Usuario usuario = new Usuario(_repository);

            return Ok(usuario.BuscarUsuario(nomeDeUsuario));
        }

        [HttpPost]
        public ActionResult<Usuario> InsertUsuario(Usuario usuario)
        {
            usuario.InserirUsuario(_repository);
            return Ok(usuario);
        }
    }
}