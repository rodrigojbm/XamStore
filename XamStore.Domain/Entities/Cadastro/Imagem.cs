﻿using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Imagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ProdutoImagem ProdutoImagem { get; set; }
    }
}
