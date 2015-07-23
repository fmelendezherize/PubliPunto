using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Context
{
    class PubliPuntoContext : DbContext
    {
        public DbSet<RamoComercial> RamoComercials { get; set; }
        public DbSet<EnteComercial> EnteComercials { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<DatosContacto> DatosContatos { get; set; }

        public PubliPuntoContext()
        {
            Database.SetInitializer<PubliPuntoContext>(new PubliPuntoContextInitializer());
            //Database.SetInitializer<PubliPuntoContext>(null);
        }
    }

    class PubliPuntoContextInitializer : CreateDatabaseIfNotExists<PubliPuntoContext>
    {
        public PubliPuntoContextInitializer()
        {

        }

        protected override void Seed(PubliPuntoContext context)
        {
            base.Seed(context);
            //TestSampleSeed(context);
        }

        private static void TestSampleSeed(PubliPuntoContext context)
        {
            var ramoComercial = context.RamoComercials.Add(
            new RamoComercial
            {
                Codigo = "CR",
                Nombre = "Comida Rapida"
            });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "CR-001",
                    Nombre = "Pizza Hut",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    WebAddress = "http://www.pizzahut.net.ve/",
                    ImagenUrl = "pizza01.jpg",
                    LogoUrl = "logo_pizzahut.jpg",
                    Tags = "Pizza;Pizzeria;Dulces",
                    IsActivo = true,
                    ListOfPromocions = new List<Promocion>() { 
                        new Promocion { 
                            Codigo = "001", Descripcion = "Promocion Lunes 2 x 1.", IsActivo = true 
                        } ,
                        new Promocion {
                            Codigo = "002", Descripcion = "Promocion Martes por una Grande una pequeña gratis."
                        },
                        new Promocion {
                            Codigo = "003", Descripcion = "Promocion Miercoles un ingrediente adicional gratis."
                        }
                    }
                });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "CR-002",
                    Nombre = "Spigolo Pizza",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    WebAddress = "http://www.spigolopizza.com.ve/",
                    LogoUrl = "ejemp_empre2.jpg",
                    Tags = "Pizza;Pizzeria;Pasta;Dulces;Cafe;Helados",
                    IsActivo = true
                });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "CR-002",
                    Nombre = "Pizza Cono",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    WebAddress = "http://www.pizzacono.com.ve/",
                    LogoUrl = "ejemp_empre1.jpg",
                    Tags = "Pizza;Conos;Galletas;Dulces;Helados",
                    IsActivo = true
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "CMP", Nombre = "Computacion" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "CMP-001",
                    Nombre = "Microsoft",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    WebAddress = "http://www.microsoft.com/",
                    Tags = "Computacion;Software;Moviles;Celulares;Windows",
                    IsActivo = true
                });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "CMP-002",
                    Nombre = "Google",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    WebAddress = "http://www.google.com/",
                    Tags = "Computacion;Software;Moviles;Celulares;Android",
                    IsActivo = true
                });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "CMP-003",
                    Nombre = "Decktra",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    WebAddress = "http://www.decktra.com.ve/",
                    Tags = "Computacion;Software;Redes",
                    IsActivo = true
                });

            ////--------------------------------------------------------------------------------------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "SCT", Nombre = "Seguridad" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "SCT-001",
                    Nombre = "SIESCOM C.A.",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.siescom.com.ve/",
                    VideoUrl = "siescom_final.wmv",
                    ImagenUrl = "siescom_gra.jpg",
                    LogoUrl = "siescom_peq.jpg",
                    Tags = "Seguridad;Alarma;Alambrado;Cercos;Camaras",
                    IsActivo = true,
                    ListOfPromocions = new List<Promocion>() { 
                        new Promocion { 
                            Codigo = "001", Descripcion = "Promocion 2 puntos de camaras por el precio de 1." 
                        } ,
                        new Promocion {
                            Codigo = "002", Descripcion = "Promocion 15 mts de cerco electrico gratis."
                        }
                    }
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "DCT", Nombre = "Discotecas" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "DCT-001",
                    Nombre = "Dance Club",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.danceclub.com.ve/",
                    ImagenUrl = "danceclub_gra.jpg",
                    LogoUrl = "danceclub_peq.jpg",
                    Tags = "Discoteca;Baile;Fiestas;Bar;Club",
                    IsActivo = true,
                    ListOfPromocions = new List<Promocion>() { 
                        new Promocion { 
                            Codigo = "001", Descripcion = "Jueves Ladies Night 3 tragos de tequila gratis." 
                        } ,
                        new Promocion {
                            Codigo = "002", Descripcion = "Miercoles de parejas, mitad de precio entrada."
                        }
                    }
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "ZPT", Nombre = "Zapaterias" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "ZPT-001",
                    Nombre = "DC Shoes",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.dcshoes.com.ve/",
                    ImagenUrl = "dcshoes_gra.jpg",
                    LogoUrl = "dcshoes_peq.jpg",
                    Tags = "Zapatos;Zapateria;Cholas;Sandalias;Zapatillas;Calzado;Deportivos",
                    IsActivo = true
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "RST", Nombre = "Restaurant" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "RST-001",
                    Nombre = "Bellavino",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.bellavino.com.ve/",
                    LogoUrl = "ejemp_empre3.jpg",
                    Tags = "Restaurant;Vinos;Mariscos;Carnes;Pescados;Pastas",
                    IsActivo = true
                });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "RST-002",
                    Nombre = "Restaurant",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.restaurant.com.ve/",
                    ImagenUrl = "restaurant_gra.jpg",
                    LogoUrl = "restaurant_peq.jpg",
                    Tags = "Restaurant;Pollos;Carnes;Pescados",
                    IsActivo = true,
                    ListOfPromocions = new List<Promocion>() { 
                        new Promocion { 
                            Codigo = "001", Descripcion = "Lunes Mexicano. Descuento del 10%" 
                        } ,
                        new Promocion {
                            Codigo = "002", Descripcion = "Martes Italiano. Descuento del 10%"
                        }
                    }
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "PBC", Nombre = "Publicidad" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "PBC-001",
                    Nombre = "Gabriel Anzola",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "gabrielanzola@gmail.com",
                    ImagenUrl = "gabrielanzola_gra.jpg",
                    LogoUrl = "gabrielanzola_peq.jpg",
                    Tags = "Publicidad;Diseño;Animaciones;Campañas;Logos",
                    IsActivo = true
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "HTL", Nombre = "Hoteles" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "HTL-001",
                    Nombre = "Hotel",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.hotel.com.ve/",
                    ImagenUrl = "hotel_gra.jpg",
                    LogoUrl = "hotel_peq.jpg",
                    Tags = "Hospedaje;Habitaciones;Turimos;Hoteles;Camas",
                    IsActivo = true
                });

            ////---------------------------

            ramoComercial = ramoComercial = context.RamoComercials.Add(new RamoComercial { Codigo = "TLL", Nombre = "Talleres" });

            context.EnteComercials.AddOrUpdate(q => q.Codigo,
                new EnteComercial
                {
                    Codigo = "TLL-001",
                    Nombre = "Todo Taller",
                    RamoComercial = ramoComercial,
                    Rif = "J-123-456-789",
                    Direccion = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Telefonos = "0251.251.11.11 - 0416.111.11.11",
                    WebAddress = "http://www.taller.com.ve/",
                    ImagenUrl = "taler_gra.jpg",
                    LogoUrl = "taler_peq.jpg",
                    Tags = "Taller;Carros;Reparaciones;Automoviles;Repuestos",
                    IsActivo = true,
                    ListOfPromocions = new List<Promocion>() { 
                        new Promocion { 
                            Codigo = "001", Descripcion = "Revision Gratis la primera visita." 
                        } ,
                    }
                });
        }
    }
}
