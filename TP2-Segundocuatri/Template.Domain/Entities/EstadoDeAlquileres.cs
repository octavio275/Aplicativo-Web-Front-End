using System.Collections.Generic;

namespace Template.Domain.Entities
{
   public class EstadoDeAlquileres
    {
        public int EstadoId { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Alquileres> AlquilerNavigator { get; set; }

    }
}
