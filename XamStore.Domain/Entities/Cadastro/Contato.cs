﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Mensagem { get; set; }
    }
}
