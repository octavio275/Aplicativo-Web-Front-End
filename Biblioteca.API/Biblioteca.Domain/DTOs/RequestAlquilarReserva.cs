using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Domain.DTOs
{
    public class RequestAlquilarReserva
    {
        public int cliente { get; set; }
        public string ISBN { get; set; }
    }
}
