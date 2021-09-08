using System;
using System.Collections.Generic;

namespace Template.Domain.Entities
{
    public class Libros
    {
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }


        public ICollection<Alquileres> AlquilerNavigator { get; set; }

    }
}
