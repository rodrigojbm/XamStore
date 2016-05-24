using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Enums;

namespace XamStore.Domain.Entities.Sistema
{
    public class SessionAutenticacaoClient
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public AutenticacaoTipoEnum AutenticacaoTipo { get; set; }
    }
}
