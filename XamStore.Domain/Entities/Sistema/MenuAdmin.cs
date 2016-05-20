using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Enums;

namespace XamStore.Domain.Entities.Sistema
{
    public class MenuAdmin
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public MenuAdminTipoEnum Tipo { get; set; }
    }
}
