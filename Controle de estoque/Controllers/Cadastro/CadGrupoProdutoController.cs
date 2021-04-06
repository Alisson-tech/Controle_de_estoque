using Controle_de_estoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_de_estoque.Controllers
{
    public class CadGrupoProdutoController : Controller
    {
        //Limitar quantidade de registro em uma pagina
        private const int _QtdMaxLinhas=5;

        //[Authorize] -> faz com que o metodo possa ser chamado apenas com autorização (Login)
        [Authorize]

        //quando inicia a pagina
        public ActionResult index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _QtdMaxLinhas, 10, 15, 20 }, _QtdMaxLinhas);
            ViewBag.QtdMaxLinhas = _QtdMaxLinhas;
            ViewBag.PaginaAtual = 1;

            var lista = GrupoProdutoModel.RecuperarLista(ViewBag.PaginaAtual, _QtdMaxLinhas);
            var quant = GrupoProdutoModel.RecuperarQuant();


            var difQtdPag = ((quant % ViewBag.QtdMaxLinhas) > 0 ? 1 : 0);

            ViewBag.QtdPaginas = (quant / ViewBag.QtdMaxLinhas) + difQtdPag;

            return View(lista);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult GrupoProdutoPagina(int pagina, int tamPag)
        {
            var lista = GrupoProdutoModel.RecuperarLista(pagina, tamPag);

            return Json(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarGrupoProduto(int id)
        {
            //retornar em json o objeto GrupoProduto
            return Json(GrupoProdutoModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarGrupoProduto(GrupoProdutoModel model)
        {
            var resultado = "OK";
            var mensagem = new List<string>();
            var idSalvo = string.Empty;

            //erro do Model (required)
            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                //obter mensagem de erro do model 
                mensagem = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = model.Salvar();
                    if (id>0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {

                    resultado = "ERRO";
                }
            }
            //cria um objeto anonimo e retorna em json
            return Json(new { Resultado=resultado, Mensagem= mensagem, IdSalvo = idSalvo});
        }

    }
}