using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.DTOs
{
    public class AlquileresReservasDTOs
    {
        public object FechaReserva;

        public object FechaAlquiler { get; set; }
        public int ClienteId { get; set; }
        public string Isbn { get; set; }
    }
}
