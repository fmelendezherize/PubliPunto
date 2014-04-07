namespace Decktra.PubliPuntoEstacion.CoreApplication.Migrations
{
    using Decktra.PubliPuntoEstacion.CoreApplication.Model;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Decktra.PubliPuntoEstacion.CoreApplication.Context.PubliPuntoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Decktra.PubliPuntoEstacion.CoreApplication.Context.PubliPuntoContext context)
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
                    Promocion = "Pizza 2 x 1 los miercoles y jueves",
                    ImagenUrl = "pizza01.jpg",
                    LogoUrl = "logo_pizzahut.jpg",
                    Tags = "Pizza;Pizzeria;Dulces"
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
                    Tags = "Pizza;Pizzeria;Pasta;Dulces;Cafe;Helados"
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
                    Tags = "Pizza;Conos;Galletas;Dulces;Helados"
                });

            //---------------------------

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
                    Tags = "Seguridad;Alarma;Alambrado;Cercos;Camaras"
                });

            //---------------------------

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
                    Tags = "Discoteca;Baile;Fiestas;Bar;Club"
                });

            //---------------------------

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
                    Tags = "Zapatos;Zapateria;Cholas;Sandalias;Zapatillas;Calzado;Deportivos"
                });

            //---------------------------

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
                    Tags = "Restaurant;Vinos;Mariscos;Carnes;Pescados;Pastas"
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
                    Tags = "Restaurant;Pollos;Carnes;Pescados"
                });

            //---------------------------

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
                    LogoUrl = "grabrielanzola_peq.jpg",
                    Tags = "Publicidad;Dise�o;Animaciones;Campa�as;Logos"
                });

            //---------------------------

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
                    Tags = "Hospedaje;Habitaciones;Turimos;Hoteles;Camas"
                });

            //---------------------------

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
                    Tags = "Taller;Carros;Reparaciones;Automoviles;Repuestos"
                });
        }
    }
}
