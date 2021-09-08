using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.AccessData.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(maxLength: 45, nullable: false),
                    Apellido = table.Column<string>(maxLength: 45, nullable: false),
                    Email = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "EstadoDeAlquiler",
                columns: table => new
                {
                    EstadoDeAlquilerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoDeAlquiler", x => x.EstadoDeAlquilerId);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    ISBN = table.Column<string>(maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(maxLength: 45, nullable: false),
                    Autor = table.Column<string>(maxLength: 45, nullable: false),
                    Editorial = table.Column<string>(maxLength: 45, nullable: false),
                    Edicion = table.Column<string>(maxLength: 45, nullable: false),
                    Stock = table.Column<int>(maxLength: 10, nullable: false),
                    Imagen = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.ISBN);
                });

            migrationBuilder.CreateTable(
                name: "Alquileres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(nullable: false),
                    ISBN = table.Column<string>(maxLength: 50, nullable: false),
                    EstadoDeAlquilerId = table.Column<int>(nullable: false),
                    FechaAlquiler = table.Column<DateTime>(nullable: true),
                    FechaReserva = table.Column<DateTime>(nullable: true),
                    FechaDevolucion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquileres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alquileres_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquileres_EstadoDeAlquiler_EstadoDeAlquilerId",
                        column: x => x.EstadoDeAlquilerId,
                        principalTable: "EstadoDeAlquiler",
                        principalColumn: "EstadoDeAlquilerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquileres_Libro_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Libro",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "ClienteId", "Apellido", "DNI", "Email", "Nombre" },
                values: new object[] { 1, "Garcia", "12345678", "pepegarcia@gmail.com", "Pepe" });

            migrationBuilder.InsertData(
                table: "EstadoDeAlquiler",
                columns: new[] { "EstadoDeAlquilerId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Reservado" },
                    { 2, "Alquilado" },
                    { 3, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "Libro",
                columns: new[] { "ISBN", "Autor", "Edicion", "Editorial", "Imagen", "Stock", "Titulo" },
                values: new object[,]
                {
                    { "970-24-0779-6", "Gabriela Campbell", "1ra", "Grupo Editorial Patria", "http://fantifica.com/wp-content/uploads/2017/02/El-cielo-roto-Portada-340x544.jpg", 3, "Crónicas Del Fin - El Cielo Roto" },
                    { "968-880-205-0", "Thelma Carr", "2da", "Person Education", "https://s3.amazonaws.com/virginia.webrand.com/virginia/344/LEqKdMKtOj8/c9e74d28a1a2e698f62446b8e5345254.jpg", 5, "Monsters" },
                    { "978-84-205-4462-5", "Lucas Lloyd", "5ta", "Prentice Hall", "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/sci-fi-book-cover-template-a1ec26573b7a71617c38ffc6e356eef9_screen.jpg?ts=1561547637", 0, "The Arrivals" },
                    { "0-13-24310-9", "Pilar Mayo", "8va", "Christianna Lee", "https://i.pinimg.com/236x/9c/ef/63/9cef638f1327bed70d7f15fa71c0b79f.jpg", 1, "Los abrazos robados" },
                    { "84-7829-074-5", "J. K. Rowling", "7ma", "Pearson Education", "https://juanjelopezponeletras.files.wordpress.com/2017/04/harry-potter-olly-moss-goblet-of-fire.png", 4, "Harry Potter y el Caliz de Fuego" },
                    { "0-8053-5340-2", "Lola Sutton", "2da", "ADDISON-WESLEY", "https://s3.amazonaws.com/virginia.webrand.com/virginia/344/KCVmVk4cdl7/202852714dec217e579db202a977be70.jpg", 6, "Surviving the Abyss" },
                    { "978-0-13-607373-4", "J. K. Rowling", "8va", "Prentice Hall", "https://juanjelopezponeletras.files.wordpress.com/2017/04/harry-potter-olly-moss-prisoner-of-azkabanefe.png", 10, "Harry Potter y el Prisionero de Azkaban" },
                    { "978-84-9035-528-2", "Gabriela Campbell", "7ma", "Pearson Education", "http://www.esferalibros.com/uploads/imagenes/libros/principal/201804/principal-portada-cronicas-del-fin-es_med.jpg", 2, "Crónicas del fin - Una grieta en el cielo" },
                    { "978-8432432", "Angelina Aludo", "7ma", "Pearson Education", "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/contemporary-fiction-night-time-book-cover-design-template-1be47835c3058eb42211574e0c4ed8bf_screen.jpg?ts=1594616847", 2, "Memory" },
                    { "932442342343", "Jimmie Collins", "7ma", "Pearson Education", "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/sci-fi-kindle-book-cover-design-template-bd12cf83a9f9d72327e372c5db1d2883_screen.jpg?ts=1561443942", 2, "The Invasion Of The Zombie Aliens" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_ClienteId",
                table: "Alquileres",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_EstadoDeAlquilerId",
                table: "Alquileres",
                column: "EstadoDeAlquilerId");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_ISBN",
                table: "Alquileres",
                column: "ISBN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alquileres");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "EstadoDeAlquiler");

            migrationBuilder.DropTable(
                name: "Libro");
        }
    }
}
