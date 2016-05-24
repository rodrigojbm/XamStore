using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Enums;

namespace XamStore.Domain.Entities.Sistema
{
    public class SessionAutenticacaoAdmin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public MenuAdminTipoEnum Nivel { get; set; }
    }
}
