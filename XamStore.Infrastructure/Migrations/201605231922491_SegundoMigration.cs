namespace XamStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundoMigration : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        PagseguroId = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Frete = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        IdPessoa = c.Int(nullable: false),
                        IdEndereco = c.Int(nullable: false),
                        IsNovo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.IdEndereco, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa)
                .Index(t => t.IdEndereco);
            
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
            DropForeignKey("dbo.PedidoItem", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.PedidoItem", "IdPedido", "dbo.Pedido");
            DropForeignKey("dbo.Venda", "IdPedido", "dbo.Pedido");
            DropForeignKey("dbo.Pedido", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Pedido", "IdEndereco", "dbo.Endereco");
            DropIndex("dbo.Venda", new[] { "IdPedido" });
            DropIndex("dbo.Pedido", new[] { "IdEndereco" });
            DropIndex("dbo.Pedido", new[] { "IdPessoa" });
            DropIndex("dbo.PedidoItem", new[] { "IdProduto" });
            DropIndex("dbo.PedidoItem", new[] { "IdPedido" });
            DropTable("dbo.Venda");
            DropTable("dbo.Pedido");
            DropTable("dbo.PedidoItem");
        }
    }
}
