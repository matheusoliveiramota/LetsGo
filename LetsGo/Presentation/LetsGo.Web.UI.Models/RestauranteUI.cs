using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LetsGo.Web.UI.Models
{
    public class RestauranteUI
    {
        [Required(ErrorMessage = "Digite o nome !")]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o endereço !")]
        [StringLength(200)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Digite a cidade !")]
        [StringLength(200)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Digite o bairro !")]
        [StringLength(200)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o CEP !")]
        [StringLength(9)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Digite o Número !")]
        [StringLength(50)]
        public string Numero { get; set; }

        [StringLength(50)]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Selecione o Estado !")]
        public int CodEstado { get; set; }

        public int CodPlaca { get; set; }

        public IList<int> Pinos { get; set; }
        public string NomeDeUsuario { get; set; } 

        //public FileExtensionsAttribute Imagem { get; set; }
    }
}

