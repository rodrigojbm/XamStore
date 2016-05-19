using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class PessoaEndereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public int IdCidade { get; set; }
        public virtual Cidade Cidade { get; set; }
        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
