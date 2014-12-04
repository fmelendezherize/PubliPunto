namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Promocions", "EnteComercial_Id", "dbo.EnteComercials");
            DropIndex("dbo.Promocions", new[] { "EnteComercial_Id" });
            RenameColumn(table: "dbo.Promocions", name: "EnteComercial_Id", newName: "EnteComercialId");
            CreateTable(
                "dbo.PromocionCupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PromocionId = c.Int(nullable: false),
                        RemoteId = c.String(),
                        CodigoCanjeo = c.String(),
                        UsuarioAsignadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioAsignadoId, cascadeDelete: true)
                .ForeignKey("dbo.Promocions", t => t.PromocionId, cascadeDelete: true)
                .Index(t => t.PromocionId)
                .Index(t => t.UsuarioAsignadoId);
            
            AddColumn("dbo.Promocions", "Detalles", c => c.String());
            AddColumn("dbo.Promocions", "FechaFin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promocions", "FechaInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promocions", "IsActivo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Promocions", "EnteComercialId", c => c.Int(nullable: false));
            CreateIndex("dbo.Promocions", "EnteComercialId");
            AddForeignKey("dbo.Promocions", "EnteComercialId", "dbo.EnteComercials", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promocions", "EnteComercialId", "dbo.EnteComercials");
            DropForeignKey("dbo.PromocionCupons", "PromocionId", "dbo.Promocions");
            DropForeignKey("dbo.PromocionCupons", "UsuarioAsignadoId", "dbo.Usuarios");
            DropIndex("dbo.PromocionCupons", new[] { "UsuarioAsignadoId" });
            DropIndex("dbo.PromocionCupons", new[] { "PromocionId" });
            DropIndex("dbo.Promocions", new[] { "EnteComercialId" });
            AlterColumn("dbo.Promocions", "EnteComercialId", c => c.Int());
            DropColumn("dbo.Promocions", "IsActivo");
            DropColumn("dbo.Promocions", "FechaInicio");
            DropColumn("dbo.Promocions", "FechaFin");
            DropColumn("dbo.Promocions", "Detalles");
            DropTable("dbo.PromocionCupons");
            RenameColumn(table: "dbo.Promocions", name: "EnteComercialId", newName: "EnteComercial_Id");
            CreateIndex("dbo.Promocions", "EnteComercial_Id");
            AddForeignKey("dbo.Promocions", "EnteComercial_Id", "dbo.EnteComercials", "Id");
        }
    }
}
