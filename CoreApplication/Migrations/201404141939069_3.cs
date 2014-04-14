namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promocions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 200),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        EnteComercial_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnteComercials", t => t.EnteComercial_Id)
                .Index(t => t.EnteComercial_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promocions", "EnteComercial_Id", "dbo.EnteComercials");
            DropIndex("dbo.Promocions", new[] { "EnteComercial_Id" });
            DropTable("dbo.Promocions");
        }
    }
}
