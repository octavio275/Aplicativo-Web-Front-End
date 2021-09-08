using AutoMapper;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.Aplication.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //CreateMap<Cliente, ClienteResponseDTOs>();

            CreateMap<Cliente, ClienteDTOs>();

            CreateMap<Alquileres, AlquilerResponseDTOs>();

            CreateMap<AlquilerResponseDTOs, Alquileres>();

            CreateMap<Libros, LibrosDTOs>();
        }
    }
}
