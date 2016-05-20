using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Enums;
using XamStore.Domain.Entities.Sistema;

namespace XamStore.Resource.Menu
{
    public class MenuResource
    {
        public static IEnumerable<Domain.Entities.Sistema.Menu> ListMenu()
        {
            var menuList = new List<Domain.Entities.Sistema.Menu>
            {
                new Domain.Entities.Sistema.Menu
                {
                    Action = "Index",
                    Controller = "Home",
                    Nome = "Início",
                    Ativo = true
                },
                new Domain.Entities.Sistema.Menu
                {
                    Action = "Index",
                    Controller = "Produto",
                    Nome = "Produtos",
                    Ativo = true
                },
                new Domain.Entities.Sistema.Menu
                {
                    Action = "Index",
                    Controller = "Sobre",
                    Nome = "Quem Somos",
                    Ativo = true
                },
                new Domain.Entities.Sistema.Menu
                {
                    Action = "Index",
                    Controller = "Contato",
                    Nome = "Contato",
                    Ativo = true
                }
            };

            return menuList;
        }

        public static IEnumerable<MenuAdmin> ListMenuAdmin()
        {
            var menuAdminList = new List<MenuAdmin>
            {
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Slide",
                    Nome = "Slides",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Categoria",
                    Nome = "Categorias",
                    Tipo = MenuAdminTipoEnum.Cadastro
                }
            };

            return menuAdminList;
        }
    }
}
