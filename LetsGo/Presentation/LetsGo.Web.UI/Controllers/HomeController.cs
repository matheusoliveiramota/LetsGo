using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LetsGo.Web.UI.Models;
using System.Threading.Tasks;

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
        public ActionResult<int> GetTables()
        {
            return 1;
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
