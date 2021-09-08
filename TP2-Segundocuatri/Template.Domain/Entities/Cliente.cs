using System;
using System.Collections.Generic;

namespace Template.Domain.Entities
{
   public class Cliente
    {
        public int ClienteId { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public ICollection<Alquileres> AlquilerNavigator { get; set; }

    }
}
