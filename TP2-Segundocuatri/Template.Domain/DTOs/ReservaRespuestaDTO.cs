using System;
using Template.Domain.Base;
using Template.Domain.Entities;

namespace Template.Domain.DTOs
{
    public class ReservaRespuestaDTO : IAlquileres
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public DateTime FechaReserva { get; set; }
    }
}
