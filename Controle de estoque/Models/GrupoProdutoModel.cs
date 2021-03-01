using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Controle_de_estoque.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o campo nome.")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}