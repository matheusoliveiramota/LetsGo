using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.UI.Services
{
    public class UsuarioServiceUI : ServiceUI, IUsuarioServiceUI
    {
        public UsuarioServiceUI(IConfiguration configuration) : base(configuration) { }
        public Usuario GetByNomeDeUsuario(string nomeDeUsuario)
        {
            Usuario usuario = null;
            
            var response = ChamadaApiGET("/api/Usuario/" + nomeDeUsuario);
            
            if(response != null)
            {
                usuario = JsonConvert.DeserializeObject<Usuario>(response);
            }

            return usuario;
        }

        public IEnumerable<Estado> GetEstados()
        {
            IEnumerable<Estado> estados = null;

            var response = ChamadaApiGET("/api/Usuario/GetEstados");

            if (response != null)
            {
                estados = JsonConvert.DeserializeObject<IEnumerable<Estado>>(response);
            }

            return estados;
        }

        public Usuario InsertUsuario(Usuario usuario)
        {
            string serializedUsuario = JsonConvert.SerializeObject(usuario);

            var response = ChamadaApiPost(serializedUsuario, "/api/Usuario");

            if (response != null)
            {
                usuario = JsonConvert.DeserializeObject<Usuario>(response);
            }

            return usuario;
        }
    }
}
