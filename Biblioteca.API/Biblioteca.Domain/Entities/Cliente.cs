using System.Collections.Generic;

namespace Biblioteca.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public ICollection<Alquiler> AlquilerNavigator { get; set; }
    }
}
