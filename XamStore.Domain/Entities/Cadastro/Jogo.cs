using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Multiplayer { get; set; }
        public int Jogadores { get; set; }

        public int IdGenero { get; set; }
        public virtual Genero Genero { get; set; }

    }
}
