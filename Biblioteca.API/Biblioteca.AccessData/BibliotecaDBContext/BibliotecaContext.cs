using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.AccessData.BibliotecaDBContext
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EstadoDeAlquiler> EstadoDeAlquileres { get; set; }
        public DbSet<Libro> Libros { get; set; }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(q => q.ClienteId);

                entity.Property(q => q.DNI).HasMaxLength(10).IsRequired();
                entity.Property(q => q.Nombre).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Apellido).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Email).HasMaxLength(45).IsRequired();
                entity.ToTable("Cliente");

                entity.HasData(
                    new Cliente { ClienteId = 1, DNI = "12345678", Nombre = "Pepe", Apellido = "Garcia", Email = "pepegarcia@gmail.com" }
                    );
            }
            );

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(q => q.ISBN);
                entity.Property(q => q.ISBN).HasMaxLength(50).IsRequired();
                entity.Property(q => q.Titulo).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Autor).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Editorial).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Edicion).HasMaxLength(45).IsRequired();
                entity.Property(q => q.Stock).HasMaxLength(10);
                entity.Property(q => q.Imagen).HasMaxLength(250).IsRequired();
                entity.ToTable("Libro");

                entity.HasData(
                    new Libro { ISBN = "970-24-0779-6", Titulo = "Crónicas Del Fin - El Cielo Roto", Autor = "Gabriela Campbell", Editorial = "Grupo Editorial Patria", Edicion = "1ra", Stock = 3, Imagen = "http://fantifica.com/wp-content/uploads/2017/02/El-cielo-roto-Portada-340x544.jpg" },
                    new Libro { ISBN = "968-880-205-0", Titulo = "Monsters", Autor = "Thelma Carr", Editorial = "Person Education", Edicion = "2da", Stock = 5, Imagen = "https://s3.amazonaws.com/virginia.webrand.com/virginia/344/LEqKdMKtOj8/c9e74d28a1a2e698f62446b8e5345254.jpg" },
                    new Libro { ISBN = "978-84-205-4462-5", Titulo = "The Arrivals", Autor = "Lucas Lloyd", Editorial = "Prentice Hall", Edicion = "5ta", Stock = 0, Imagen = "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/sci-fi-book-cover-template-a1ec26573b7a71617c38ffc6e356eef9_screen.jpg?ts=1561547637" },
                    new Libro { ISBN = "0-13-24310-9", Titulo = "Los abrazos robados", Autor = "Pilar Mayo", Editorial = "Christianna Lee", Edicion = "8va", Stock = 1, Imagen = "https://i.pinimg.com/236x/9c/ef/63/9cef638f1327bed70d7f15fa71c0b79f.jpg" },
                    new Libro { ISBN = "84-7829-074-5", Titulo = "Harry Potter y el Caliz de Fuego", Autor = "J. K. Rowling", Editorial = "Pearson Education", Edicion = "7ma", Stock = 4, Imagen = "https://juanjelopezponeletras.files.wordpress.com/2017/04/harry-potter-olly-moss-goblet-of-fire.png" },
                    new Libro { ISBN = "0-8053-5340-2", Titulo = "Surviving the Abyss", Autor = "Lola Sutton", Editorial = "ADDISON-WESLEY", Edicion = "2da", Stock = 6, Imagen = "https://s3.amazonaws.com/virginia.webrand.com/virginia/344/KCVmVk4cdl7/202852714dec217e579db202a977be70.jpg" },
                    new Libro { ISBN = "978-0-13-607373-4", Titulo = "Harry Potter y el Prisionero de Azkaban", Autor = "J. K. Rowling", Editorial = "Prentice Hall", Edicion = "8va", Stock = 10, Imagen = "https://juanjelopezponeletras.files.wordpress.com/2017/04/harry-potter-olly-moss-prisoner-of-azkabanefe.png" },
                    new Libro { ISBN = "978-84-9035-528-2", Titulo = "Crónicas del fin - Una grieta en el cielo", Autor = "Gabriela Campbell", Editorial = "Pearson Education", Edicion = "7ma", Stock = 2, Imagen = "http://www.esferalibros.com/uploads/imagenes/libros/principal/201804/principal-portada-cronicas-del-fin-es_med.jpg" },
                    new Libro { ISBN = "978-8432432", Titulo = "Memory", Autor = "Angelina Aludo", Editorial = "Pearson Education", Edicion = "7ma", Stock = 2, Imagen = "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/contemporary-fiction-night-time-book-cover-design-template-1be47835c3058eb42211574e0c4ed8bf_screen.jpg?ts=1594616847" },
                    new Libro { ISBN = "932442342343", Titulo = "The Invasion Of The Zombie Aliens", Autor = "Jimmie Collins", Editorial = "Pearson Education", Edicion = "7ma", Stock = 2, Imagen = "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/sci-fi-kindle-book-cover-design-template-bd12cf83a9f9d72327e372c5db1d2883_screen.jpg?ts=1561443942" }
                    );
            }
            );
            modelBuilder.Entity<EstadoDeAlquiler>(entity =>
            {
                entity.HasKey(q => q.EstadoDeAlquilerId);

                entity.Property(q => q.Descripcion).HasMaxLength(45).IsRequired();

                entity.ToTable("EstadoDeAlquiler");

                entity.HasData(
                    new EstadoDeAlquiler { EstadoDeAlquilerId = 1, Descripcion = "Reservado" },
                    new EstadoDeAlquiler { EstadoDeAlquilerId = 2, Descripcion = "Alquilado" },
                    new EstadoDeAlquiler { EstadoDeAlquilerId = 3, Descripcion = "Cancelado" }
                    );
            }
            );

            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.HasKey(q => q.Id);

                entity.Property(q => q.ISBN).HasMaxLength(50).IsRequired();

                entity.HasOne(q => q.Libros)
                .WithMany(q => q.AlquilerNavigator)
                .HasForeignKey(q => q.ISBN);

                entity.HasOne(q => q.Cliente)
                .WithMany(q => q.AlquilerNavigator)
                .HasForeignKey(q => q.ClienteId);

                entity.HasOne(q => q.EstadoDeAlquiler)
                .WithMany(q => q.AlquilerNavigator)
                .HasForeignKey(q => q.EstadoDeAlquilerId);
            }
            );
        }
    }
}
