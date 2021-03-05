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
        private static List<GrupoProdutoModel> _listaGrupoProduto = new List<GrupoProdutoModel>()
        {
            new GrupoProdutoModel() {Id=1, Nome= "Livros", Ativo=true},
            new GrupoProdutoModel() {Id=2, Nome= "Mouse", Ativo=false},
            new GrupoProdutoModel() {Id=3, Nome= "Museu", Ativo=true}
        };
        //[Authorize] -> faz com que o metodo possa ser chamado apenas com autorização (Login)
        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(GrupoProdutoModel.RecuperarLista());
        }

        [HttpPost]
        [Authorize]
        public ActionResult RecuperarGrupoProduto(int id)
        {
            //retornar em json o objeto da lista produto, cujo o Id(id do objeto) seja igual o id(parametro)
            return Json(GrupoProdutoModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public ActionResult Usuario()
        {
            return View();
        }

    }
}