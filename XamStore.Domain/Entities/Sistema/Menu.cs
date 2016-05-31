namespace XamStore.Domain.Entities.Sistema
{
    public class Menu
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Ativo { get; set; }
    }
}
