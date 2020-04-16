using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Placa
    {
        public int CodPlaca { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Pino> Pinos { get; set; }
    }
}
