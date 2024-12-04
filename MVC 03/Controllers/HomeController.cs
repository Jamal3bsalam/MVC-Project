using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_03.Company.G05.PL.Services;
using MVC_03.Models;
using MVC_03.Services;
using System.Diagnostics;
using System.Text;

namespace MVC_03.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedServices _scope01;
        private readonly IScopedServices _scope02;
        private readonly ITransientServices _transient01;
        private readonly ITransientServices _transient02;
        private readonly ISingeltonServices _singelton01;
        private readonly ISingeltonServices _singelton02;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedServices scope01,
            IScopedServices scope02,
            ITransientServices transient01,
            ITransientServices transient02,
            ISingeltonServices singelton01,
            ISingeltonServices singelton02
 
            )
        {
            _logger = logger;
            this._scope01 = scope01;
            this._scope02 = scope02;
            this._transient01 = transient01;
            this._transient02 = transient02;
            this._singelton01 = singelton01;
            this._singelton02 = singelton02;
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scope01 :: {_scope01.GetGuid()}\n");
            builder.Append($"scope02 :: {_scope02.GetGuid()}\n");

            builder.Append($"transient01 :: {_transient01.GetGuid()}\n");
            builder.Append($"transient02 :: {_transient02.GetGuid()}\n");

            builder.Append($"singelton01 :: {_singelton01.GetGuid()}\n");
            builder.Append($"singelton02 :: {_singelton02.GetGuid()}\n");

            return builder.ToString();
        }
        public IActionResult Index()
        {
            return View();
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
