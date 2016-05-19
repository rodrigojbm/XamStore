using System.Collections.Generic;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdEstado { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual List<Endereco> PessoaEnderecos { get; set; }
    }
}