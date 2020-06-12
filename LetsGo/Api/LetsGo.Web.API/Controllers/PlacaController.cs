using System.Linq;
using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Requests;
using LetsGo.Domain.Entities.Responses;
using LetsGo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacaController : ControllerBase
    {
        protected readonly IPlacaService _service;
        protected readonly IPlacaRepository _repository;

        public PlacaController(IPlacaService placaService, IPlacaRepository repository)
        {
            _service = placaService;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<Placa> Get()
        {
            Placa placa = new Placa(_repository);
            return Ok(placa.BuscarPlacas().FirstOrDefault());
        }

        [HttpGet("GetEstadoMesa/{codRestaurante:int}/{porta:int}")]
        public ActionResult<GetEstadoMesaResponse> GetEstadoMesa(int codRestaurante, short porta)
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