using System.Collections.Generic;
using System.Security.AccessControl;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Estado
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Abreviacao { get; set; }

        public virtual List<Cidade> Cidades { get; set; }
    }
}