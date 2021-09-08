using AutoMapper;
using System;
using System.Collections.Generic;
using Template.Domain.DTOs;
using Template.Domain.Entities;
using Template.Domain.Queries;

namespace Template.Aplication.Services
{
    public interface ILibroService
    {
        List<LibrosDTOs> GetALL();
        List<LibrosDTOs> BuscarLibro(bool stock, string autor, string titulo);
  
    }


    public class LibroService : GenericService<Libros>, ILibroService
    {
        public LibroService(ILibroQuery query, IMapper mapper) : base(mapper) => _query = query;
        private readonly ILibroQuery _query;


     

        public List<LibrosDTOs> GetALL()
        {
            return _query.GetALL();
        }


        public List<LibrosDTOs> BuscarLibro(bool stock, string autor, string titulo)
        {
            var listaLibros =_query.ObtenerLibros(stock, autor, titulo);

            return Mapper.Map<List<LibrosDTOs>>(listaLibros);
        }
    }
}
