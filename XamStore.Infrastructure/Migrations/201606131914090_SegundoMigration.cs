namespace XamStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundoMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Estado", name: "Descricao", newName: "Nome");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Estado", name: "Nome", newName: "Descricao");
        }
    }
}
