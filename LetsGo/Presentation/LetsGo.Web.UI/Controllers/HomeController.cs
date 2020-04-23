using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using LetsGo.Web.UI.Services.Interfaces;
using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Models;

namespace LetsGo.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestauranteServiceUI _restauranteService;
        private readonly IUsuarioServiceUI _usuarioService;
        private readonly IPlacaServiceUI _placaService;

        public HomeController(IRestauranteServiceUI restauranteService, IUsuarioServiceUI usuarioService, IPlacaServiceUI placaService)
        {
            _restauranteService = restauranteService;
            _usuarioService = usuarioService;
            _placaService = placaService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var nomeDeUsuario = User.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(nomeDeUsuario))
            {
                var usuario = _usuarioService.GetByNomeDeUsuario(User.FindFirst("sub")?.Value);
                if (usuario == null)
                {
                    usuario = _usuarioService.InsertUsuario(new Usuario
                    {
                        Nome = User.FindFirst("name")?.Value,
                        NomeDeUsuario = User.FindFirst("sub")?.Value
                    });

                    return RedirectToAction("AdicionarRestaurante", usuario);
                }

                var restaurante = _restauranteService.GetByUsuario(usuario);
                if (restaurante != null)
                {
                    return View(); // Index e passar parâmetro Usuario por ViewBag
                }
                else
                {
                    return RedirectToAction("AdicionarRestaurante", usuario);
                }
            }

            return View();
        }

        [Authorize]
        public IActionResult AdicionarRestaurante(Usuario usuario)
        {
            var placa = _placaService.Get();
            var estados = _usuarioService.GetEstados();

            ViewBag.NomeDeUsuario = usuario.NomeDeUsuario;
            ViewBag.Placa = placa;
            ViewBag.Estados = estados;

            return View("NovoRestaurante");
        }

        [HttpPost]
        public IActionResult AdicionarRestaurante(RestauranteUI restauranteUI)
        {
            _restauranteService.InsertRestaurante(restauranteUI);
            return View("NovoRestaurante");
        }

        [HttpGet("[controller]/GetEstadoMesa/{idTable}")]
        public ActionResult<int> GetTableState(int idTable)
        {
            return 1;
        }

        [HttpGet("[controller]/GetMesas")]
        public ActionResult<dynamic> GetTables()
        {
            //List<Mesa> mesas = new List<Mesa>();
            //mesas.Add(new Mesa { Id = 1, Coordenadas = new Coordenadas { Esquerda = 30, Topo = 90, Largura = 60, Altura =  90}, Estado = 1 });
            //mesas.Add(new Mesa { Id = 2 ,Coordenadas = new Coordenadas { Esquerda = 210, Topo = 90, Largura = 90, Altura = 60 }, Estado = 0 });

            //var result = new { Mesas = mesas, DataUltimaAtualizacao = DateTime.Now };
            var result = new { DataUltimaAtualizacao = DateTime.Now };
            return Json(result);
        }

        [HttpGet("[controller]/ExisteAlteracao")]
        public ActionResult<bool> ExisteAlteracao(DateTime dataUltimaAtualizacao)
        {
            return true;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new LetsGo.Web.UI.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
