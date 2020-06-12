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
    public class LocalizacaoController : ControllerBase
    {
        protected readonly ILocalizacaoRepository _repository;

        public LocalizacaoController(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetEstados")]
        public ActionResult<GetEstadoMesaResponse> GetEstados()
        {
            Estado estado = new Estado(_repository);
            return Ok(estado.BuscarEstados());
        }
    }
}