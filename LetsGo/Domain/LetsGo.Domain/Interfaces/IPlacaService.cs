using LetsGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsGo.Domain.Interfaces
{
    public interface IPlacaService
    {
        IEnumerable<Placa> GetPlacas();
    }
}
