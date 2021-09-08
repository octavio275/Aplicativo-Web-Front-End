using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Entities;

namespace Template.Domain.DTOs
{
    public class ReservaDTOs
    {

        public int ReservaId { get; set; }
        public Cliente ClienteId { get; set; }

        public int Cliente { get; set; }
        public Cliente Nombre { get; set; }
        public Cliente Apellido { get; set; }
        public string Isbn { get; set; }
        public int Estado { get; set; }
        public DateTime FechaReserva { get; set; }
        public virtual Cliente ClienteNavigator { get; set; }
        public object Id { get; set; }
        public string Entity { get; set; }
    }
}
