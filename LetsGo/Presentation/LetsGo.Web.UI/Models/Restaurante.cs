using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LetsGo.Web.UI.Models
{
    public class Restaurante
    {
        public int CodRestaurante { get; set; }

        [Required(ErrorMessage = "Digite o nome !")]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o endereço !")]
        [StringLength(200)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Digite o bairro !")]
        [StringLength(200)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o CEP !")]
        [StringLength(9)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Digite o CEP !")]
        [StringLength(50)]
        public string Numero { get; set; }

        public FileExtensionsAttribute Imagem { get; set; }
    }
}

