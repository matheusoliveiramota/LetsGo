using System;
using System.Collections.Generic;

namespace LetsGo.Domain.Entities
{
    public class DistanciaRestaurante
    {
        public Restaurante Restaurante { get; set; }
        public string Distancia { get; set; }
        public int DistanciaEmMetros { get; set; }
    }
}