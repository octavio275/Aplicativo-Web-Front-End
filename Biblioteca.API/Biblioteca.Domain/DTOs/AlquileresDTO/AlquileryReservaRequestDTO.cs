
namespace Biblioteca.Domain.DTOs.AlquileresDTO
{
    public class AlquileryReservaRequestDTO
    {
        public int ClienteId { get; set; }
        public string ISBN { get; set; }
        public string FechaAlquiler { get; set; }
        public string FechaReserva { get; set; }
    }
}
