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
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace LetsGo.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestauranteServiceUI _restauranteService;
        private readonly IUsuarioServiceUI _usuarioService;
        private readonly IPlacaServiceUI _placaService;
        private readonly IConfiguration _configuration;

        public HomeController(IRestauranteServiceUI restauranteService, IUsuarioServiceUI usuarioService, IPlacaServiceUI placaService, IConfiguration configuration)
        {
            _restauranteService = restauranteService;
            _usuarioService = usuarioService;
            _placaService = placaService;
            _configuration = configuration;
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
                    return View(restaurante);
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

        [Authorize]
        [HttpPost]
        public IActionResult AdicionarRestaurante(RestauranteUI restauranteUI)
        {
            _restauranteService.InsertRestaurante(restauranteUI);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("[controller]/GetMesas/{codRestaurante}")]
        public ActionResult<dynamic> GetMesas(int codRestaurante)
        {
            var result = _restauranteService.GetMesas(codRestaurante);
            return Json(result);
        }

        [Authorize]
        public async Task LogOut()
        {
            // Atualizar os Coookies (Atuenticação Local)
            await HttpContext.SignOutAsync("Cookies");

            // Desconcecta no Identity Server
            await HttpContext.SignOutAsync("OpenIdConnect");
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
