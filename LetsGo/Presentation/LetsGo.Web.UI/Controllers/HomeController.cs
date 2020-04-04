using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LetsGo.Web.UI.Models;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace LetsGo.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdicionarRestaurante()
        {
            return View("NovoRestaurante");
        }

        [HttpPost]
        public IActionResult AdicionarRestaurante(Restaurante restaurante)
        {
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
            List<Mesa> mesas = new List<Mesa>();
            mesas.Add(new Mesa { Id = 1, Coordenadas = new Coordenadas { Esquerda = 30, Topo = 90, Largura = 60, Altura =  90}, Estado = 1 });
            mesas.Add(new Mesa { Id = 2 ,Coordenadas = new Coordenadas { Esquerda = 210, Topo = 90, Largura = 90, Altura = 60 }, Estado = 0 });

            var result = new { Mesas = mesas, DataUltimaAtualizacao = DateTime.Now };
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
