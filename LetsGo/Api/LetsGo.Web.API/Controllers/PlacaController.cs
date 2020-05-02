using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Requests;
using LetsGo.Domain.Entities.Responses;
using LetsGo.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacaController : ControllerBase
    {
        protected readonly IPlacaService _service;

        public PlacaController(IPlacaService placaService)
        {
            _service = placaService;
        }

        [HttpGet]
        public ActionResult<Placa> Get()
        {
            return Ok(_service.GetPlacas().FirstOrDefault());
        }

        [HttpGet("GetEstadoMesa/{codRestaurante:int}/{porta:int}")]
        public ActionResult<GetEstadoMesaResponse> GetEstadoMesa(int codRestaurante, int porta)
        {
            return Ok(_service.GetEstadoMesa(codRestaurante,porta));
        }

        [HttpPost("PostEstadoMesa")]
        public ActionResult<short> PostEstadoMesa(PostEstadoMesaRequest request)
        {
            return Ok(_service.PostEstadoMesa(request));
        }
    }
}