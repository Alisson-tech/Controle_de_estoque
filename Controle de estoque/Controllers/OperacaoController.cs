using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_de_estoque.Controllers
{
    public class OperacaoController : Controller
    {
        [Authorize]
        public ActionResult EntradaEstoque()
        {
            return View();
        }

        [Authorize]
        public ActionResult SaidaEstoque()
        {
            return View();
        }

        [Authorize]
        public ActionResult LancPerdaProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult Inventario()
        {
            return View();
        }

    }
}