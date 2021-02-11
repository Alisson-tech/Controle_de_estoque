using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Controle_de_estoque.Models
{
    public class LoginViewModel
    {
        //Requer que o usuário informe usuario
        [Required(ErrorMessage = "Informe o Usuário")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        //Requer que o usuário informe senha
        [Required(ErrorMessage = "Informe a Senha")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }


        [Display(Name = "Lembrar Me")]
        public bool LembrarMe { get; set; }
    }
}