using System;

namespace Biblioteca.Domain.DTOs.AlquileresDTO
{
    public class AlquilerRequestConLibrosDTO
    {
        public string ISBNLibro { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public string EditorialLibro { get; set; }
        public string EdicionLibro { get; set; }
        public string ImagenLibro { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public DateTime? FechaAlquiler { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public DateTime? FechaReserva { get; set; }
        
    }
}
