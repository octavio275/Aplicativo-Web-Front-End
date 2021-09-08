
using Template.Domain.Entities;

namespace Template.Domain.DTOs
{
    public class ResponsiveGetAllAlquileres
    {
        public Alquileres AlquileresId { get; set; }
        public Cliente ClienteId { get; set; }
        public Libros LibrosId { get; set; }

        public int Cliente { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public int Stock { get; set; }
    }
}
