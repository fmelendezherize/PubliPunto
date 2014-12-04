namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PromocionCupons", "UsuarioAsignadoId", "dbo.Usuarios");
            DropIndex("dbo.PromocionCupons", new[] { "UsuarioAsignadoId" });
            AlterColumn("dbo.PromocionCupons", "UsuarioAsignadoId", c => c.Int());
            CreateIndex("dbo.PromocionCupons", "UsuarioAsignadoId");
            AddForeignKey("dbo.PromocionCupons", "UsuarioAsignadoId", "dbo.Usuarios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PromocionCupons", "UsuarioAsignadoId", "dbo.Usuarios");
            DropIndex("dbo.PromocionCupons", new[] { "UsuarioAsignadoId" });
            AlterColumn("dbo.PromocionCupons", "UsuarioAsignadoId", c => c.Int(nullable: false));
            CreateIndex("dbo.PromocionCupons", "UsuarioAsignadoId");
            AddForeignKey("dbo.PromocionCupons", "UsuarioAsignadoId", "dbo.Usuarios", "Id", cascadeDelete: true);
        }
    }
}
