﻿using System.Collections.Generic;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Domain.Entities.Sistema
{
    public class SessionCarrinho
    {
        public ICollection<ProdutoCarrinho> ProdutosCarrinho = new List<ProdutoCarrinho>(); 
        public Endereco Endereco { get; set; }
        public decimal Frete { get; set; }
        public decimal TotalGeral { get; set; }
        public bool IsSedex { get; set; }
    }
}
