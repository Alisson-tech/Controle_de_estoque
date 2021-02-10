using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_de_estoque.Controllers
{
    public class GraficoController : Controller
    {
        // GET: Grafoco
        public ActionResult PerdaMes()
        {
            return View();
        }
        public ActionResult EntradaSaidaMes()
        {
            return View();
        }
    }
}