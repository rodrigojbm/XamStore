﻿using System.Collections.Generic;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Abreviacao { get; set; }

        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}