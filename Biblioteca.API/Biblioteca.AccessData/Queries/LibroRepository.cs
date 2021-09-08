using Biblioteca.AccessData.BibliotecaDBContext;
using Biblioteca.AccessData.Commands;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Queries;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Biblioteca.AccessData.Queries
{
    public class LibroRepository : GenericsRepository, ILibroRepository
    {
        private readonly IDbConnection conexion;
        private readonly Compiler SqlKataCompiler;

        private readonly BibliotecaContext contexto;
        public LibroRepository(BibliotecaContext contexto, IDbConnection conexion, Compiler SqlKataCompiler) : base(contexto)
        {
            this.conexion = conexion;
            this.SqlKataCompiler = SqlKataCompiler;

            this.contexto = contexto;
        }

        public List<Libro> ObtenerLibros(bool stock, string autor, string titulo)
        {
            var db = new QueryFactory(conexion, SqlKataCompiler);
            List<Libro> libros = new List<Libro>();
            if (stock)
            {
                /*
                var libros_todos = contexto.Set<Libro>().ToList();

                if (autor == null)
                {
                    autor = "";
                }
                if (titulo == null)
                {
                    titulo = "";
                }

                autor = autor.ToLower();
                titulo = titulo.ToLower();
                foreach (Libro libro in libros_todos)
                {
                    var autor_bd = libro.Autor.ToLower();
                    var titulo_bd = libro.Titulo.ToLower();
                    if (autor_bd.Contains(autor.ToLower()) || titulo_bd.Contains(titulo.ToLower()))
                    {
                        libros.Add(libro);
                    }

                }


                */
                    libros = db.Query("Libro").
                        Select(
                        "Libro.ISBN AS ISBN",
                        "Libro.Titulo AS Titulo",
                        "Libro.Autor AS Autor",
                        "Libro.Editorial AS Editorial",
                        "Libro.Edicion AS Edicion",
                        "Libro.Stock AS Stock",
                        "Libro.Imagen As Imagen").
                        Where("Libro.Stock", ">", 0).
                        WhereLike("Libro.Autor", $"{autor}%").
                        WhereLike("Libro.Titulo", $"{titulo}%").
                        Get<Libro>().ToList();
            }
            else
            {
                libros = db.Query("Libro").
                    Select(
                    "Libro.ISBN AS ISBN",
                    "Libro.Titulo AS Titulo",
                    "Libro.Autor AS Autor",
                    "Libro.Editorial AS Editorial",
                    "Libro.Edicion AS Edicion",
                    "Libro.Stock AS Stock",
                    "Libro.Imagen As Imagen").
                    Where("Libro.Stock", "=", 0).
                    WhereLike("Libro.Autor", $"{autor}%").
                    WhereLike("Libro.Titulo", $"{titulo}%").
                    Get<Libro>().ToList();
            }
            return libros;
        }
    }
}
