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
        //[Authorize] -> faz com que o metodo possa ser chamado apenas com aotorização (Login)
        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(_listaGrupoProduto);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RecuperarGrupoProduto(int id)
        {
            //retornar em json o objeto da lista produto, cujo o Id(id do objeto) seja igual o id(parametro)
            return Json(_listaGrupoProduto.Find(x => x.Id == id));
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