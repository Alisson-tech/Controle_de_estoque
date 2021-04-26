using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Controle_de_estoque
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //exibir erro retornado pelo ASP NET (ataque xss )
        void Application_Error(object sender, EventArgs e)
        {
            //pega o objeto do erro
            Exception ex = Server.GetLastError();

            if (ex is HttpRequestValidationException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.ContentType = "application/json";
                //retorna em json
                Response.Write("{ \"Resultado\":\"AVISO\",\"Mensagens\":[\"Somente texto sem caracteres especiais pode ser enviado.\"],\"IdSalvo\":\"\"}");
                Response.End();
            }
            if (ex is HttpAntiForgeryException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.End();
            }
        }

        //evento de requisição da autenticação
        //usa o cookie para preencher o genericPrincipal para gerenciar roles
        protected void Application_AuthenticateRequest(object Sender, EventArgs e)
        {
            //recuperar cookie 
            var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null && cookie.Value != string.Empty)
            {
                //criar ticket de autenticação
                FormsAuthenticationTicket ticket;

                try
                {
                    //descriptografar cookie
                    ticket = FormsAuthentication.Decrypt(cookie.Value);
                }
                catch
                {

                    return;
                }
                //recuperar string tipo do usuario
                var perfis = ticket.UserData.Split(';');

                //se nao existir usuario configurado
                if(Context.User != null)
                {
                    //Configura o usuario Context.user.identity com a role/autorização perfis
                    Context.User = new GenericPrincipal(Context.User.Identity, perfis);
                }
            }
        }
    }
}
