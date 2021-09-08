
namespace Template.Domain.DTOs
{
    public class AlquilerEstadoDTO
    {
        public string ISBNLibro { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public string EditorialLibro { get; set; }
        public string EdicionLibro { get; set; }
        public string ImagenLibro { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string FechaAlquiler { get; set; }
        public string FechaDevolucion { get; set; }

    }
}
