namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
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
            
            CreateTable(
                "dbo.EnteComercials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        Codigo = c.String(nullable: false, maxLength: 200),
                        Rif = c.String(),
                        Telefonos = c.String(),
                        Direccion = c.String(),
                        WebAddress = c.String(),
                        LogoUrl = c.String(),
                        ImagenUrl = c.String(),
                        VideoUrl = c.String(),
                        Tags = c.String(),
                        IsActivo = c.Boolean(nullable: false),
                        RamoComercial_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RamoComercials", t => t.RamoComercial_Id, cascadeDelete: true)
                .Index(t => t.RamoComercial_Id);
            
            CreateTable(
                "dbo.Promocions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnteComercialId = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 200),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        Detalles = c.String(),
                        DetallesBig = c.String(),
                        Condiciones = c.String(),
                        CuponesPorUsuario = c.Int(nullable: false),
                        ImagenUrl = c.String(),
                        ImagenSmallUrl = c.String(),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        Vigencia = c.Int(nullable: false),
                        IsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnteComercials", t => t.EnteComercialId, cascadeDelete: true)
                .Index(t => t.EnteComercialId);
            
            CreateTable(
                "dbo.PromocionCupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PromocionId = c.Int(nullable: false),
                        RemoteId = c.String(),
                        CodigoCanjeo = c.String(),
                        UsuarioAsignadoId = c.Int(),
                        SmsSent = c.Boolean(nullable: false),
                        FechaReclamo = c.DateTime(),
                        FechaVigencia = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioAsignadoId)
                .ForeignKey("dbo.Promocions", t => t.PromocionId, cascadeDelete: true)
                .Index(t => t.PromocionId)
                .Index(t => t.UsuarioAsignadoId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cedula = c.String(maxLength: 10),
                        Codigo = c.String(),
                        Nombre = c.String(),
                        Email = c.String(),
                        Movil = c.String(),
                        Pin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RamoComercials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        Codigo = c.String(nullable: false, maxLength: 200),
                        Descripcion = c.String(),
                        LogoUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnteComercials", "RamoComercial_Id", "dbo.RamoComercials");
            DropForeignKey("dbo.PromocionCupons", "PromocionId", "dbo.Promocions");
            DropForeignKey("dbo.PromocionCupons", "UsuarioAsignadoId", "dbo.Usuarios");
            DropForeignKey("dbo.Promocions", "EnteComercialId", "dbo.EnteComercials");
            DropIndex("dbo.PromocionCupons", new[] { "UsuarioAsignadoId" });
            DropIndex("dbo.PromocionCupons", new[] { "PromocionId" });
            DropIndex("dbo.Promocions", new[] { "EnteComercialId" });
            DropIndex("dbo.EnteComercials", new[] { "RamoComercial_Id" });
            DropTable("dbo.RamoComercials");
            DropTable("dbo.Usuarios");
            DropTable("dbo.PromocionCupons");
            DropTable("dbo.Promocions");
            DropTable("dbo.EnteComercials");
            DropTable("dbo.DatosContactoes");
        }
    }
}
