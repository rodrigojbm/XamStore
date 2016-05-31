using XamStore.Domain.Entities.Enums;

namespace XamStore.Domain.Entities.Sistema
{
    public class SessionAutenticacaoClient
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public AutenticacaoTipoEnum AutenticacaoTipo { get; set; }
    }
}
