using System;


namespace Template.Domain.Entities
{

   
   public class Alquileres
    {
        public int AlquileresId { get; set; }
        public int ClienteId { get; set; }
        public string Isbn { get; set; }
        public int EstadoId { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaAlquiler { get; set; }
        public DateTime FechaDevolucion { get; set; }
        //public int LibrosId { get; set; }
        public Cliente ClienteNavigator { get; set; }
        public Libros LibrosNavigator { get; set; }
        public EstadoDeAlquileres EstadoNavigator { get; set; }
     
    }
}
