namespace XamStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primeiro : DbMigration
    {
        public override void Up()
        {
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
                        EmailAutenticacao = c.String(),
                        Sexo = c.Int(nullable: false),
                        Senha = c.String(nullable: false),
                        ConfirmaSenha = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cidade", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidade");
            DropIndex("dbo.Endereco", new[] { "IdPessoa" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.Cidade", new[] { "IdEstado" });
            DropTable("dbo.Estado");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Endereco");
            DropTable("dbo.Cidade");
        }
    }
}