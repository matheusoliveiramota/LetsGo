﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Entities
{
    public class Restaurante
    {
        public int CodRestaurante { get; set; }
        public Usuario Usuario { get; set; }
        public Endereco Endereco { get; set; }
        public string Nome { get; set; }
        public List<Mesa> Mesas { get; set; }
    }
}
