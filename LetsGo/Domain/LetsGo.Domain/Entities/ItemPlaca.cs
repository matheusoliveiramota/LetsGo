using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class ItemPlaca
    {
        public int CodItemPlaca { get; set; }
        public Placa Placa { get; set; }
        public string CodigoDeBarras { get; set; }
    }
}
