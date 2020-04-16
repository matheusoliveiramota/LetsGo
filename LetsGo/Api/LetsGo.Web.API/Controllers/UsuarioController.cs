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
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService placaService)
        {
            _service = placaService;
        }

        [HttpGet("GetEstados")]
        public ActionResult<IEnumerable<Estado>> GetEstados()
        {
            return Ok(_service.GetEstados());
        }

        [HttpPost]
        public ActionResult<Usuario> InsertUsuario(Usuario usuario)
        {
            return Ok(_service.InsertUsuario(usuario));
        }
    }
}