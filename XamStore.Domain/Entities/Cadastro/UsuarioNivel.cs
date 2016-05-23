using System.Collections.Generic;

namespace XamStore.Domain.Entities.Cadastro
{
    public class UsuarioNivel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}