using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities.Responses
{
    public class GetGeoPointGoogleResponse
    {
        public List<dynamic> Results { get; set; }
        public string Status { get; set; }
    }
}