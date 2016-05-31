using XamStore.Domain.Enums;

namespace XamStore.Domain.Entities.Sistema
{
    public class SessionAutenticacaoAdmin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public MenuAdminTipoEnum Nivel { get; set; }
    }
}
