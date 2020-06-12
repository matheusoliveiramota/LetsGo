using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Responses;
using LetsGo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LetsGo.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestauranteController : Controller
    {
        protected readonly IRestauranteService _service;
        protected readonly IRestauranteRepository _repository;

        public RestauranteController(IRestauranteService service, IRestauranteRepository repository)
        {
            this._service = service;
            this._repository = repository;
        }

        [HttpGet]
        public ActionResult GetRestaurantes()
        {
            Restaurante restaurante = new Restaurante(_repository);

            return Ok(restaurante.BuscarRestaurantes());
        }

        [HttpGet("{nomeDeUsuario}")]
        public ActionResult Get(string nomeDeUsuario)
        {
            Restaurante restaurante = new Restaurante(_repository);

            return Ok(restaurante.BuscarRestaurante(nomeDeUsuario));
        }

        [HttpGet("{codRestaurante:int}")]
        public ActionResult Get(int codRestaurante)
        {
            Restaurante restaurante = new Restaurante(_repository);

            return Ok(restaurante.BuscarRestaurante(codRestaurante));
        }

        [HttpGet("GetMesas/{codRestaurante:int}")]
        public ActionResult GetMesas(int codRestaurante)
        {
            Mesa mesa = new Mesa(_repository);

            return Ok(mesa.BuscarMesas(codRestaurante));
        }

        [HttpPost]
        public ActionResult InsertRestaurante(Restaurante restaurante)
        {
            restaurante.Endereco.BuscarLatitudeLongitude(_service);

            restaurante.InserirRestaurante(_repository);
            return Ok(restaurante);
        }

        [HttpGet("Imagem/{nomeImagem}")]
        public IActionResult GetImagem(string nomeImagem)
        {
            var caminhoImagem = "C:\\Users\\mmota\\Pictures\\LetsGo";
            Byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(caminhoImagem,nomeImagem));

            return File(bytes, "image/jpeg");
        }

        [HttpGet("Localizacao")]
        public IActionResult GetPorLocalizacao([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var response = new Response<IEnumerable<Restaurante>>();

            Restaurante restaurante = new Restaurante(_repository);

            var listaRestaurantes = restaurante.BuscarRestaurantes();
            if (listaRestaurantes != null && listaRestaurantes.Count() > 0)
            {
                listaRestaurantes = _service.BuscarRestaurantesProximos(listaRestaurantes.ToList(), latitude, longitude);
                response.Data = listaRestaurantes;
            }
            else
            {
                response.Message = "Nenhum restaurante cadastrado.";
            }

            return Ok(response);
        }

        [HttpGet("Pesquisa")]
        public IActionResult Pesquisa([FromQuery] string busca, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            var response = new Response<IEnumerable<Restaurante>>();

            Restaurante restaurante = new Restaurante(_repository);

            var listaRestaurantes = restaurante.BuscarRestaurantesPorPesquisa(busca);
            if (listaRestaurantes != null && listaRestaurantes.Count() > 0)
            {
                listaRestaurantes = _service.BuscarRestaurantesProximos(listaRestaurantes.ToList(), latitude, longitude);
                response.Data = listaRestaurantes;
            }
            else
            {
                listaRestaurantes = restaurante.BuscarRestaurantes();
                response.Data = listaRestaurantes;
                response.Message = "Não encontramos nenhum restaurante.";
            }

            return Ok(response);
        }
    }
}
