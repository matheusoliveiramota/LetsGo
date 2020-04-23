using LetsGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Mesa
    {
        public int CodMesa { get; set; }
        public Coordenada Coordenada { get; set; }
        public Pino Pino { get; set; }
        public int Numero { get; set; }
        public EstadoMesa Estado { get; set; }
        public DateTime DataAlteracaoEstado { get; set; }
    }
}
