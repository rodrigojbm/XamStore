using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Console
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int IdFabricante { get; set; }
        public virtual Fabricante Fabricante { get; set; }
    }
}
