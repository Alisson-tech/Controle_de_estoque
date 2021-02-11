using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_de_estoque.Controllers
{
    public class RelatorioController : Controller
    {
        [Authorize]
        public ActionResult PosicaoEstoque()
        {
            return View();
        }

        [Authorize]
        public ActionResult Ressuprimento()
        {
            return View();
        }
    }
}