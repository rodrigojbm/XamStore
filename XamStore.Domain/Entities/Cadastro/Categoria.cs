using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    /// <summary>
    /// Identifica se o produto é um jogo, console, etc...
    /// </summary>
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
