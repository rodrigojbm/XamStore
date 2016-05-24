using System.Collections.Generic;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int IdEstado { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }
    }
}