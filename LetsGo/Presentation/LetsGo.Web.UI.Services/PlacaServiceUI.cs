using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Web.UI.Services
{
    public class PlacaServiceUI : ServiceUI, IPlacaServiceUI
    {
        public PlacaServiceUI(IConfiguration configuration) : base(configuration) { }
        public Placa Get()
        {
            Placa placa = null;

            var response = ChamadaApiGET("/api/Placa");

            if (response != null)
            {
                placa = JsonConvert.DeserializeObject<Placa>(response);
            }

            return placa;
        }
    }
}
