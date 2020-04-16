using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("GetByNomeDeUsuario/{nomeDeUsuario}")]
        public ActionResult Get(string nomeDeUsuario)
        {
            _service.GetRestaurante(nomeDeUsuario);
            return Ok();
        }
    }
}
