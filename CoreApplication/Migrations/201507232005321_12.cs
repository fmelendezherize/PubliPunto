namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosContactoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Comentario = c.String(),
                        Destinatario = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DatosContactoes");
        }
    }
}
