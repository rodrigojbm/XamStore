﻿using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Operacao;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Garantia { get; set; }
        public string Peso { get; set; }
        public int Estoque { get; set; }

        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<ProdutoImagem> ProdutoImagens { get; set; }
        //public virtual ICollection<Slide> Slides { get; set; }
        //public virtual ICollection<ProdutoEstoque> ProdutoEstoques { get; set; }
        //public virtual ICollection<PedidoItem> PedidoItens { get; set; }
    }
}
