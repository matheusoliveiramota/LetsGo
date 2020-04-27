using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Domain.Entities;
using LetsGo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestauranteController : Controller
    {
        protected readonly IRestauranteService _service;
        public RestauranteController(IRestauranteService service)
        {
            this._service = service;
        }

        [HttpGet("{nomeDeUsuario}")]
        public ActionResult Get(string nomeDeUsuario)
        {
            return Ok(_service.GetRestaurante(nomeDeUsuario));
        }

        [HttpGet("GetUltimaAlteracoMesa/{codRestaurante}")]
        public ActionResult GetUltimaAlteracoEstadoMesa(int codRestaurante)
        {
            return Ok(_service.GetUltimaAlteracaoEstadoMesa(codRestaurante));
        }

        [HttpGet("GetMesas/{codRestaurante}")]
        public ActionResult GetMesas(int codRestaurante)
        {
            return Ok(_service.GetMesas(codRestaurante));
        }

        [HttpPost]
        public ActionResult InsertRestaurante(Restaurante restaurante)
        {
            return Ok(_service.InsertRestaurante(restaurante));
        }
    }
}
