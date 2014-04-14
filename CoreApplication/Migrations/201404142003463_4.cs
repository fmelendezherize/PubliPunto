namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EnteComercials", "Promocion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnteComercials", "Promocion", c => c.String());
        }
    }
}
