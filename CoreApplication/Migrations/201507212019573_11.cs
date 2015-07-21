namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promocions", "Condiciones", c => c.String());
            AddColumn("dbo.Usuarios", "Movil", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Movil");
            DropColumn("dbo.Promocions", "Condiciones");
        }
    }
}
