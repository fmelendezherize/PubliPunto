namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PromocionCupons", "SmsSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PromocionCupons", "SmsSent");
        }
    }
}
