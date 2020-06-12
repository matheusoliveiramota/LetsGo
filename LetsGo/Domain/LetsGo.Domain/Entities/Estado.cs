using LetsGo.Domain.Interfaces;
using System.Collections.Generic;

namespace LetsGo.Domain.Entities
{
    public class Estado
    {
        private ILocalizacaoRepository localizacaoRepository;

        public int CodEstado { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        public Estado(ILocalizacaoRepository localizacaoRepository)
        {
            this.localizacaoRepository = localizacaoRepository;
        }

        public Estado() { }

        public IEnumerable<Estado> BuscarEstados()
        {
            return localizacaoRepository.BuscarEstados();
        }
    }
}