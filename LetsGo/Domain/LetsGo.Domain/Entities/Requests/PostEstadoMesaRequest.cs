namespace LetsGo.Domain.Entities.Requests
{
    public class PostEstadoMesaRequest
    {
        public int CodRestaurante { get; set; }
        public short Porta { get; set; }
        public short CodEstadoMesa { get; set; }
    }
}