using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
