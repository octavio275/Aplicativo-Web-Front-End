

namespace Template.Domain.DTOs
{
   public class LibrosDTOs
    {
        public int LibrosId { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public int Stock { get; set; }
        public bool Verificacion { get; set; }
        public string Imagen { get; set; }


    }
    public class ResponseLibrosGetById
    {
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public int Stock { get; set; }
    }
    public class LibrosPorClienteDTOs
    {
        public string Estado { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public string Imagen { get; set; }
    }
}
