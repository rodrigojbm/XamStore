﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamStore.Domain.Entities.Sistema
{
    public class Menu
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Ativo { get; set; }
    }
}
