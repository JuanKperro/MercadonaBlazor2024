
using MercadonaBlazor2024.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MercadonaBlazor2024.Server.Models
{
    public class AaplicacionDBContext : IdentityDbContext
    {

        /*
          esta clase va a servir para q Identity genere atraves de EF el DBContext para generar tablas a partir de clases modelo
          usando DbSets....como estamos creando un DBContext Personalizado pq vamos a añadir tablas propias ademas de las de Identity
          EF te obliga a sobrecargar el constructor (sino lo pones te salta un error indicandote que sobrecargues el constructor)
         */

        public AaplicacionDBContext(DbContextOptions<AaplicacionDBContext> options) : base(options)
        {
        }


        #region ....propiedades de la clase AplicacionDBContext ......

        //nos definimos un DbSet por cada clase modelo a mapear en el DbContext como propiedad....
        public DbSet<Direccion> Direcciones { get; set; }

        public DbSet<Provincia> Provincias { get; set; }

        public DbSet<Municipio> Municipios { get; set; }

        #endregion


        #region ....metodos clase AplicacionDBContext .....
        //metodo que se ejecuta para crar las tablas a partir de las clases en el momento que se lanza migracion....
        //para lanzar migracion y q se vuelquen cambios sobre la BD fisica:
        //  - 1º abres consola de administracion de paquetes powershell de Nuget: Herramientas ---> administracion paquetes NuGet ----> consola adminstracion....
        //              Add-Migration nº_nombre_migracion 
        //
        // OJO!!!!  la migracion no se genera si el proyecto no compila (hay errores...)

        //  - 2º paso, si todo esta ok para ejecutar la migracion:  
        //          Update-Database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region //// modificacion tabla IdentityUser ///
            builder.Entity<MiClienteIdentity>();
            //builder.Entity<MiClienteIdentity>().HasMany<Direccion>(p => p.).WithOne().HasForeignKey<Direccion>(s => s.IdDireccion);
            #endregion

            #region ////// creacion tabla Direcciones a partir de clase modelo Direccion /////
            builder.Entity<Direccion>().ToTable("Direcciones");
            builder.Entity<Direccion>().HasKey((Direccion dir) => dir.IdDireccion);

            builder.Entity<Direccion>().Property((Direccion dir) => dir.NombreVia).IsRequired().HasMaxLength(250);
            builder.Entity<Direccion>().Property((Direccion dir) => dir.CP).IsRequired().HasMaxLength(5);

            //cuando la clase modelo tiene como prop. un objeto de otra clase, EF no puede mapearlo contra un tipo de dato de SqlServer
            //¿solucion? o no almacenas esa prop como columna de tabla o serializas esa prop a un string usando metodo:
            // .HasConversion( 1ºparam_lambda_serializacion, 2ºparam_lambda_deserializacion)
            builder.Entity<Direccion>().Property((Direccion dir) => dir.Provincia)
                                       .HasConversion(
                                            prov => JsonSerializer.Serialize<Provincia>(prov, (JsonSerializerOptions)null),
                                            prov => JsonSerializer.Deserialize<Provincia>(prov, (JsonSerializerOptions)null)
                                        ).HasColumnName("Provincia");

            builder.Entity<Direccion>().Property((Direccion dir) => dir.Municipio)
                           .HasConversion(
                                muni => JsonSerializer.Serialize<Municipio>(muni, (JsonSerializerOptions)null),
                                muni => JsonSerializer.Deserialize<Municipio>(muni, (JsonSerializerOptions)null)
                            ).HasColumnName("Municipio");



            #endregion




            builder.Entity<Provincia>().HasNoKey();
            builder.Entity<Municipio>().HasNoKey();
        }
        #endregion



    }
}
