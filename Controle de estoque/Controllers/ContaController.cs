﻿using Controle_de_estoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Controle_de_estoque.Controllers
{
    public class ContaController : Controller
    {
        //[AllowAnonymous] -- Torna o metodo Publico
        [AllowAnonymous]
        
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        //Metodo Post
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string ReturnUrl)
        {
            //Se o login não for valido retorna pra Login
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var achou = UsuarioModel.ValidarUsuario(login.Usuario, login.Senha);

            //se for valido Redireciona para Funções do site
            if (achou!=null)
            {
                ///FormsAuthentication.SetAuthCookie(achou.Nome, login.LembrarMe);
                
                ///criar ticket de validação do usuario
               var ticket = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(
                    1, achou.Nome, DateTime.Now, DateTime.Now.AddHours(12),
                    login.LembrarMe, "Operador"
                    ));

                //criar o cookie de validação com as informações do ticket
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);

                //adicionar cookie ao navegador
                Response.Cookies.Add(cookie);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Invalido");
            }

            return View(login);
        }
        //Deslogar do Site Usanto FormsAuthentication e retorna para pagina Home
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}