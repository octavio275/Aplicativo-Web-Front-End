using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Base;
using Template.Domain.Entities;

namespace Template.Domain.DTOs
{
   public class RequestDTOs : IAlquileres
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Isbn { get; set; }

        public DateTime FechaReserva { get; set; }
        public DateTime FechaAlquiler { get; set; }
        
        public DateTime FechaDevolucion { get; set; }

    }
    public class ActualizarReservaDTO
    {
        public int ClienteId { get; set; }
        public string Isbn { get; set; }
    }
}
