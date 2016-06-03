using System.Collections.Generic;
using XamStore.Domain.Entities.Sistema;
using XamStore.Domain.Enums;

namespace XamStore.Resource.Menu
{
    public static class MenuResource
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
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "ProdutoAdmin",
                    Nome = "Produtos",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "JogoAdmin",
                    Nome = "Jogos",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Plataforma",
                    Nome = "Plataformas",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Genero",
                    Nome = "Generos",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Console",
                    Nome = "Consoles",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Fabricante",
                    Nome = "Fabricantes",
                    Tipo = MenuAdminTipoEnum.Cadastro
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Usuario",
                    Nome = "Usuários",
                    Tipo = MenuAdminTipoEnum.Operacao
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Venda",
                    Nome = "Vendas",
                    Tipo = MenuAdminTipoEnum.Operacao
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "ProdutoEstoque",
                    Nome = "Estoque",
                    Tipo = MenuAdminTipoEnum.Operacao
                },
                new MenuAdmin
                {
                    Action = "Index",
                    Controller = "Relatorio",
                    Nome = "Relatórios",
                    Tipo = MenuAdminTipoEnum.Operacao
                }
            };

            return menuAdminList;
        }
    }
}
