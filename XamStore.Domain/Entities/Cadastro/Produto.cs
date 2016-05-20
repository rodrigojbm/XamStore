using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Garantia { get; set; }
        public decimal Peso { get; set; }
        public int Estoque { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Imagem> Imagens { get; set; }
        public ICollection<Slide> Slides { get; set; }
    }
}
