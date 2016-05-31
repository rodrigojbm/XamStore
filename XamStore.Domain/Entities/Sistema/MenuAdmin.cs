using XamStore.Domain.Enums;

namespace XamStore.Domain.Entities.Sistema
{
    public class MenuAdmin
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public MenuAdminTipoEnum Tipo { get; set; }
    }
}
