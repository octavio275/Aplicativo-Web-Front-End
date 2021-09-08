using System;


namespace Template.Domain.DTOs
{
  public  class ClienteDTOs
    {
        public string Dni { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

      
    }
    public class ClienteRequestDTOs
    {

        public int ClienteId { get; set; }
        public string Entidad { get; set; }
    }
}
