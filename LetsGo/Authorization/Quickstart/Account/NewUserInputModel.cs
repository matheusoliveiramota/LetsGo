using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Quickstart.UI
{
    public class NewUserInputModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
