using System;

namespace Biblioteca.Domain.Entities
{
    public class Alquiler
    {
        public int Id { get; set; } //PK
        public int ClienteId { get; set; } //FK
        public string ISBN { get; set; } //FK
        public int EstadoDeAlquilerId { get; set; } //FK
        public DateTime? FechaAlquiler { get; set; }
        public DateTime? FechaReserva { get; set; }
        public DateTime? FechaDevolucion { get; set; }


        public Cliente Cliente { get; set; }
        public Libro Libros { get; set; }
        public EstadoDeAlquiler EstadoDeAlquiler { get; set; }
    }
}
