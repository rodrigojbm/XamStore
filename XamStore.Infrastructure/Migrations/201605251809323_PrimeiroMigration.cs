namespace XamStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiroMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        IdEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.IdEstado, cascadeDelete: true)
                .Index(t => t.IdEstado);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Abreviacao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Console",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        IdFabricante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fabricante", t => t.IdFabricante, cascadeDelete: true)
                .Index(t => t.IdFabricante);
            
            CreateTable(
                "dbo.Fabricante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contato",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefone = c.String(),
                        Mensagem = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(nullable: false),
                        Complemento = c.String(),
                        Numero = c.String(nullable: false),
                        Cep = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        IdCidade = c.Int(nullable: false),
                        IdPessoa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cidade", t => t.IdCidade, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdCidade)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacebookId = c.String(),
                        GoogleId = c.String(),
                        Nome = c.String(nullable: false),
                        Sobrenome = c.String(nullable: false),
                        Rg = c.String(),
                        Cpf = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Sexo = c.Int(nullable: false),
                        Senha = c.String(nullable: false),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genero",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Imagem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jogo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Multiplayer = c.Boolean(nullable: false),
                        Jogadores = c.Int(nullable: false),
                        Classificacao = c.Int(nullable: false),
                        IdPlataforma = c.Int(nullable: false),
                        IdGenero = c.Int(nullable: false),
                        IdConsole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Console", t => t.IdConsole, cascadeDelete: true)
                .ForeignKey("dbo.Genero", t => t.IdGenero, cascadeDelete: true)
                .ForeignKey("dbo.Plataforma", t => t.IdPlataforma, cascadeDelete: true)
                .Index(t => t.IdPlataforma)
                .Index(t => t.IdGenero)
                .Index(t => t.IdConsole);
            
            CreateTable(
                "dbo.Plataforma",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Controller = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuAdmin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Controller = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Newsletter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        PagseguroId = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Frete = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        IsNovo = c.Boolean(nullable: false),
                        IdPessoa = c.Int(nullable: false),
                        IdEndereco = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.IdEndereco, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa, cascadeDelete: true)
                .Index(t => t.IdPessoa)
                .Index(t => t.IdEndereco);
            
            CreateTable(
                "dbo.PedidoItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Double(nullable: false),
                        IdPedido = c.Int(nullable: false),
                        IdProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedido", t => t.IdPedido, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdPedido)
                .Index(t => t.IdProduto);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        Preco = c.Double(nullable: false),
                        Garantia = c.String(nullable: false),
                        Peso = c.String(),
                        Estoque = c.Int(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.IdCategoria, cascadeDelete: true)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.ProdutoEstoque",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        IdProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produto", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdProduto);
            
            CreateTable(
                "dbo.ProdutoImagem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        IdImagem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imagem", t => t.IdImagem, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdProduto)
                .Index(t => t.IdImagem);
            
            CreateTable(
                "dbo.Slide",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imagem = c.String(nullable: false),
                        IdProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produto", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdProduto);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        IdUsuarioNivel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UsuarioNivel", t => t.IdUsuarioNivel, cascadeDelete: true)
                .Index(t => t.IdUsuarioNivel);
            
            CreateTable(
                "dbo.UsuarioNivel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Venda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        CodigoRastreioCorreios = c.String(),
                        IsNova = c.Boolean(nullable: false),
                        IdPedido = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedido", t => t.IdPedido, cascadeDelete: true)
                .Index(t => t.IdPedido);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venda", "IdPedido", "dbo.Pedido");
            DropForeignKey("dbo.Usuario", "IdUsuarioNivel", "dbo.UsuarioNivel");
            DropForeignKey("dbo.Slide", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.ProdutoImagem", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.ProdutoImagem", "IdImagem", "dbo.Imagem");
            DropForeignKey("dbo.ProdutoEstoque", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.PedidoItem", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.Produto", "IdCategoria", "dbo.Categoria");
            DropForeignKey("dbo.PedidoItem", "IdPedido", "dbo.Pedido");
            DropForeignKey("dbo.Pedido", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Pedido", "IdEndereco", "dbo.Endereco");
            DropForeignKey("dbo.Jogo", "IdPlataforma", "dbo.Plataforma");
            DropForeignKey("dbo.Jogo", "IdGenero", "dbo.Genero");
            DropForeignKey("dbo.Jogo", "IdConsole", "dbo.Console");
            DropForeignKey("dbo.Endereco", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidade");
            DropForeignKey("dbo.Console", "IdFabricante", "dbo.Fabricante");
            DropForeignKey("dbo.Cidade", "IdEstado", "dbo.Estado");
            DropIndex("dbo.Venda", new[] { "IdPedido" });
            DropIndex("dbo.Usuario", new[] { "IdUsuarioNivel" });
            DropIndex("dbo.Slide", new[] { "IdProduto" });
            DropIndex("dbo.ProdutoImagem", new[] { "IdImagem" });
            DropIndex("dbo.ProdutoImagem", new[] { "IdProduto" });
            DropIndex("dbo.ProdutoEstoque", new[] { "IdProduto" });
            DropIndex("dbo.Produto", new[] { "IdCategoria" });
            DropIndex("dbo.PedidoItem", new[] { "IdProduto" });
            DropIndex("dbo.PedidoItem", new[] { "IdPedido" });
            DropIndex("dbo.Pedido", new[] { "IdEndereco" });
            DropIndex("dbo.Pedido", new[] { "IdPessoa" });
            DropIndex("dbo.Jogo", new[] { "IdConsole" });
            DropIndex("dbo.Jogo", new[] { "IdGenero" });
            DropIndex("dbo.Jogo", new[] { "IdPlataforma" });
            DropIndex("dbo.Endereco", new[] { "IdPessoa" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.Console", new[] { "IdFabricante" });
            DropIndex("dbo.Cidade", new[] { "IdEstado" });
            DropTable("dbo.Venda");
            DropTable("dbo.UsuarioNivel");
            DropTable("dbo.Usuario");
            DropTable("dbo.Slide");
            DropTable("dbo.ProdutoImagem");
            DropTable("dbo.ProdutoEstoque");
            DropTable("dbo.Produto");
            DropTable("dbo.PedidoItem");
            DropTable("dbo.Pedido");
            DropTable("dbo.Newsletter");
            DropTable("dbo.MenuAdmin");
            DropTable("dbo.Menu");
            DropTable("dbo.Plataforma");
            DropTable("dbo.Jogo");
            DropTable("dbo.Imagem");
            DropTable("dbo.Genero");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Endereco");
            DropTable("dbo.Contato");
            DropTable("dbo.Fabricante");
            DropTable("dbo.Console");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Categoria");
        }
    }
}
