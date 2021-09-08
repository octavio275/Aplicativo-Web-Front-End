using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;

namespace Template.AcessData
{
   public class Context : DbContext
    {
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<EstadoDeAlquileres> EstadoDeAlquileres { get; set; }
        public DbSet<Alquileres> Alquileres { get; set; }



        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-HCM64F2\\SQLEXPRESS;Database=TP2-municipalidadCarmenDeAreco;Trusted_Connection=True");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(q => q.ClienteId);

                entity.Property(q => q.Dni).HasMaxLength(10).IsRequired();
                entity.Property(q => q.Nombre).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Apellido).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Email).HasMaxLength(45).IsRequired();
                entity.ToTable("Cliente");
                entity.HasData(new Cliente { ClienteId = 2, Dni = "41003963", Nombre = "OCTA", Apellido = "jorge", Email = "octavio@gmail.com" });

            }
            );

            modelBuilder.Entity<Alquileres>(entity =>
            {
                entity.HasKey(q => q.AlquileresId);

                entity.Property(q => q.Isbn).HasMaxLength(50).IsRequired();

                entity.HasOne(q => q.LibrosNavigator)
                .WithMany(q => q.AlquilerNavigator)
                .HasForeignKey(q => q.Isbn);

                entity.HasOne(q => q.ClienteNavigator)
                .WithMany(q => q.AlquilerNavigator)
                .HasForeignKey(q => q.ClienteId);

                entity.HasOne(q => q.EstadoNavigator)
                .WithMany(q => q.AlquilerNavigator)
                .HasForeignKey(q => q.EstadoId);
            }
            );

            modelBuilder.Entity<Libros>(entity =>
            {

                entity.ToTable("Libros");
                entity.Property(e => e.Isbn).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Titulo).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Autor).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Editorial).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Edicion).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Stock).IsRequired();
                entity.HasKey(e => e.Isbn);
                entity.HasData(new Libros { Isbn = "1", Titulo = "fisicaUniversitaria", Autor = "zemansky", Editorial = "Presman", Edicion = "4ta", Stock = 100, Imagen = "fis.jpg" });
                entity.HasData(new Libros { Isbn = "2", Titulo = "curso de programacion c#", Autor = "Varios autores", Editorial = "mp edicion", Edicion = "5ta", Stock = 10, Imagen = "cursoDeC.jpg" });
                entity.HasData(new Libros { Isbn = "3", Titulo = "Git", Autor = "Pablo hijonosa", Editorial = "Desconocido", Edicion = "4ta", Stock = 100, Imagen = "git.jpg" });
                entity.HasData(new Libros { Isbn = "4", Titulo = "Redes ", Autor = "Tanenbaum", Editorial = "Pearson", Edicion = "7ta", Stock = 100, Imagen = "red.jpg" });
                entity.HasData(new Libros { Isbn = "5", Titulo = "Info y comunicacion", Autor = "Tanenbaum", Editorial = "Presman", Edicion = "8ta", Stock = 5, Imagen = "comunicacion.jpg" });


            });
            modelBuilder.Entity<EstadoDeAlquileres>(entity =>
            {
                entity.ToTable("EstadoDeAlquileres");
                entity.Property(x => x.Descripcion).HasMaxLength(45).IsRequired();
                entity.HasKey(x => x.EstadoId);
                entity.HasData(new EstadoDeAlquileres { EstadoId = 1, Descripcion = "Reservado" });
                entity.HasData(new EstadoDeAlquileres { EstadoId = 2, Descripcion = "Alquilado" });
                entity.HasData(new EstadoDeAlquileres { EstadoId = 3, Descripcion = "Cancelado" });


            });

            

            
        }
    }
}
