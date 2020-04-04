using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.Web.UI.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public Coordenadas Coordenadas { get; set; }
        public short Estado { get; set; }
    }
}
