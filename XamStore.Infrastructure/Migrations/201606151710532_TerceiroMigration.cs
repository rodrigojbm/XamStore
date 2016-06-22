namespace XamStore.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TerceiroMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Estado", name: "Descricao", newName: "Nome");
            AddColumn("dbo.Fabricante", "Sobre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fabricante", "Sobre");
            RenameColumn(table: "dbo.Estado", name: "Nome", newName: "Descricao");
        }
    }
}
