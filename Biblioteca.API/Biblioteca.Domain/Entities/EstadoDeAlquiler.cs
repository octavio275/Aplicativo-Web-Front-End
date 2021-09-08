using System.Collections.Generic;

namespace Biblioteca.Domain.Entities
{
    public class EstadoDeAlquiler
    {
        public int EstadoDeAlquilerId { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Alquiler> AlquilerNavigator { get; set; }
    }
}
