using Biblioteca.Domain.DTOs;
using Biblioteca.Domain.DTOs.AlquileresDTO;
using Biblioteca.Domain.DTOs.AlquileresDTO.Response;
using Biblioteca.Domain.DTOs.LibrosDTO;
using System.Collections.Generic;

namespace Biblioteca.Domain.Services
{
    public interface IAlquilerService
    {
        AlquilerResponseDTO AddAlquiler(AlquileryReservaRequestDTO alquilerRequestDTO);
        public void AlquilarReserva(RequestAlquilarReserva paraAlquilarDTO);

        public List<object> GetPorEstado(int estado);
        public List<LibroDeClienteDTO> GetLibrosPorCliente(int idCliente);
    }
}
