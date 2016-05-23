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
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Garantia = c.Int(nullable: false),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                "dbo.Imagem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa, cascadeDelete: true)
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
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Abreviacao = c.String(nullable: false),
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
                "dbo.Genero",
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
                        IdGenero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genero", t => t.IdGenero, cascadeDelete: true)
                .Index(t => t.IdGenero);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "IdUsuarioNivel", "dbo.UsuarioNivel");
            DropForeignKey("dbo.Jogo", "IdGenero", "dbo.Genero");
            DropForeignKey("dbo.Cidade", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidade");
            DropForeignKey("dbo.Slide", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.ProdutoImagem", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.ProdutoImagem", "IdImagem", "dbo.Imagem");
            DropForeignKey("dbo.ProdutoEstoque", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.Produto", "IdCategoria", "dbo.Categoria");
            DropIndex("dbo.Usuario", new[] { "IdUsuarioNivel" });
            DropIndex("dbo.Jogo", new[] { "IdGenero" });
            DropIndex("dbo.Endereco", new[] { "IdPessoa" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.Cidade", new[] { "IdEstado" });
            DropIndex("dbo.Slide", new[] { "IdProduto" });
            DropIndex("dbo.ProdutoImagem", new[] { "IdImagem" });
            DropIndex("dbo.ProdutoImagem", new[] { "IdProduto" });
            DropIndex("dbo.ProdutoEstoque", new[] { "IdProduto" });
            DropIndex("dbo.Produto", new[] { "IdCategoria" });
            DropTable("dbo.UsuarioNivel");
            DropTable("dbo.Usuario");
            DropTable("dbo.Newsletter");
            DropTable("dbo.Jogo");
            DropTable("dbo.Genero");
            DropTable("dbo.Contato");
            DropTable("dbo.Estado");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Endereco");
            DropTable("dbo.Cidade");
            DropTable("dbo.Slide");
            DropTable("dbo.Imagem");
            DropTable("dbo.ProdutoImagem");
            DropTable("dbo.ProdutoEstoque");
            DropTable("dbo.Produto");
            DropTable("dbo.Categoria");
        }
    }
}
