using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Template.AcessData;
using Template.Domain.DTOs;
using Template.Domain.Entities;
using Template.Domain.Queries;

namespace PS.template.accesodatos.Queries
{
    public class LibroQuery : ILibroQuery
    {
        private readonly IDbConnection connection;
        private readonly Compiler sqlKataCompiler;
        private readonly Context context;


        public LibroQuery(IDbConnection connection, Compiler sqlKataCompiler, Context context)
        {
            this.connection = connection;
            this.sqlKataCompiler = sqlKataCompiler;
            this.context = context;

        }

        public List<LibrosDTOs> GetALL()
        {
            var db = new QueryFactory(connection, sqlKataCompiler);

            var query = db.Query("LIbros");

            var result = query.Get<LibrosDTOs>();

            return result.ToList();
        }

        public List<Libros> ObtenerLibros(bool stock, string autor, string titulo)
        {
            var db = new QueryFactory(connection, sqlKataCompiler);
            List<Libros> libros;
            if (stock)
            {
                libros = db.Query("Libros").
                    Select(
                    "Libros.Isbn AS Isbn",
                    "Libros.Titulo AS Titulo",
                    "Libros.Autor AS Autor",
                    "Libros.Editorial AS Editorial",
                    "Libros.Edicion AS Edicion",
                    "Libros.Stock AS Stock",
                    "Libros.Imagen As Imagen").
                    Where("Libros.Stock", ">", 0).WhereLike("Libros.Autor", $"{autor}%").WhereLike("Libros.Titulo", $"{titulo}%").Get<Libros>().ToList();
            }
            else
            {
                libros = db.Query("Libros").
                    Select(
                    "Libros.Isbn AS Isbn",
                    "Libros.Titulo AS Titulo",
                    "Libros.Autor AS Autor",
                    "Libros.Editorial AS Editorial",
                    "Libros.Edicion AS Edicion",
                    "Libros.Stock AS Stock",
                    "Libros.Imagen As Imagen").
                    Where("Libros.Stock", "=", 0).WhereLike("Libros.Autor", $"{autor}%").WhereLike("Libros.Titulo", $"{titulo}%").Get<Libros>().ToList();
            }
            return libros;
        }
  

      
    }
}
