using Controle_de_estoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Controle_de_estoque.Controllers.Cadastro
{
    public class CadUsuariosController : Controller
    {
        private const int _QtdMaxLinhas = 5;
        private const string _senha = "{$a7#8%51f{8?";

        //[Authorize] -> faz com que o metodo possa ser chamado apenas com autorização (Login)
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.SenhaPadrao = _senha;

            ViewBag.ListaTamPag = new SelectList(new int[] { _QtdMaxLinhas, 10, 15, 20 }, _QtdMaxLinhas);
            ViewBag.QtdMaxLinhas = _QtdMaxLinhas;
            ViewBag.PaginaAtual = 1;

            var lista = UsuarioModel.RecuperarLista(ViewBag.PaginaAtual, _QtdMaxLinhas);
            var quant = UsuarioModel.RecuperarQuant();


            var difQtdPag = ((quant % ViewBag.QtdMaxLinhas) > 0 ? 1 : 0);

            ViewBag.QtdPaginas = (quant / ViewBag.QtdMaxLinhas) + difQtdPag;

            return View(lista);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult UsuarioPagina(int pagina, int tamPag)
        {
            var lista = UsuarioModel.RecuperarLista(pagina, tamPag);

            return Json(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int id)
        {
            //retornar em json o objeto GrupoProduto
            return Json(UsuarioModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel model)
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
                    if (model.Senha == _senha)
                    {
                        model.Senha = "";
                    }
                    var id = model.Salvar();
                    if (id > 0)
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
            return Json(new { Resultado = resultado, Mensagem = mensagem, IdSalvo = idSalvo });
        }
    }
}