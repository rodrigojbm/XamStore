namespace XamStore.Domain.Entities.Cadastro
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public int IdUsuarioNivel { get; set; }
        public virtual UsuarioNivel UsuarioNivel { get; set; }
    }
}
