using AutoMapper;
using Biblioteca.Domain.DTOs.AlquileresDTO.Response;
using Biblioteca.Domain.DTOs.ClientesDTO;
using Biblioteca.Domain.DTOs.ClientesDTO.Response;
using Biblioteca.Domain.DTOs.LibrosDTO;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.Mappers
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Cliente, ClienteResponseDTO>();

            CreateMap<Cliente, ClienteDTO>();

            CreateMap<Alquiler, AlquilerResponseDTO>();

            CreateMap<AlquilerResponseDTO, Alquiler>();

            CreateMap<Libro, LibroDTO>();
        }
    }
}
