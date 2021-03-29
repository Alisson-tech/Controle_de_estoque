using Controle_de_estoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_de_estoque.Controllers
{
    public class CadastroController : Controller
    {
        #region Grupo Produtos
        //[Authorize] -> faz com que o metodo possa ser chamado apenas com autorização (Login)
        [Authorize]
        public ActionResult GrupoProduto()
        {
            var lista = GrupoProdutoModel.RecuperarLista();

            ViewBag.QtdMaxLinhas = 5;
            ViewBag.PaginaAtual = 1;

            var difQtdPag = ((lista.Count() % ViewBag.QtdMaxLinhas) > 0 ? 1 : 0);

            ViewBag.QtdPaginas = (lista.Count() / ViewBag.QtdMaxLinhas) + difQtdPag;

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarGrupoProduto(int id)
        {
            //retornar em json o objeto GrupoProduto
            return Json(GrupoProdutoModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarGrupoProduto(GrupoProdutoModel model)
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
        #endregion

        #region Usuarios

        private const string _senha = "{$a7#8%51f{8?";
        //[Authorize] -> faz com que o metodo possa ser chamado apenas com autorização (Login)
        [Authorize]
        public ActionResult usuario()
        {
            ViewBag.SenhaPadrao = _senha;
            return View(UsuarioModel.RecuperarLista());
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
        #endregion

        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }

        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }
        [Authorize]
        public ActionResult pais()
        {
            return View();
        }

        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }


    }
}