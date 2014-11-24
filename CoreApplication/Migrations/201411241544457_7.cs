namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RamoComercials", "LogoUrl", c => c.String());
            DropColumn("dbo.RamoComercials", "ImagenUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RamoComercials", "ImagenUrl", c => c.String());
            DropColumn("dbo.RamoComercials", "LogoUrl");
        }
    }
}
