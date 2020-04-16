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
    public class PlacaController : ControllerBase
    {
        protected readonly IPlacaService _service;

        public PlacaController(IPlacaService placaService)
        {
            _service = placaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Placa>> GetPlacas()
        {
            return Ok(_service.GetPlacas());
        }
    }
}