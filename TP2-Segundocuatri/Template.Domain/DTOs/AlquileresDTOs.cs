using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Base;

namespace Template.Domain.DTOs
{
     public  class AlquileresDTOs
    {

        public int ClienteId { get; set; }
        public string Isbn { get; set; }
        public DateTime FechaAlquiler { get; set; }
        public DateTime FechaReserva { get; set; }
 


    }
    public class LibroAlqDTOs : IAlquileres
    {
        public string Estado { get; set; }
        public int ClienteId { get; set; }
        public string FechaAlquiler { get; set; }
        public string FechaDevolucion { get; set; }
        public string FechaReserva { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }

    }
}
