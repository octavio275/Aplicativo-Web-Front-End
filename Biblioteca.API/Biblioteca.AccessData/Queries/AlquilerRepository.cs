using Biblioteca.AccessData.BibliotecaDBContext;
using Biblioteca.AccessData.Commands;
using Biblioteca.Domain.DTOs.AlquileresDTO;
using Biblioteca.Domain.DTOs.LibrosDTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Biblioteca.AccessData.Queries
{
    public class AlquilerRepository : GenericsRepository, IAlquilerRepository
    {
        private readonly BibliotecaContext context;
        private readonly IDbConnection conexion;
        private readonly Compiler SqlKataCompiler;

        public AlquilerRepository(BibliotecaContext contexto, IDbConnection conexion, Compiler SqlKataCompiler) : base(contexto)
        {
            this.context = contexto;
            this.conexion = conexion;
            this.SqlKataCompiler = SqlKataCompiler;
        }
        
        public List<object> ObtenerPorEstado(int estado)
        {
            var lista = context.Alquileres
                .Include(x => x.Libros)
                .Include(x => x.EstadoDeAlquiler)
                .Include(x => x.Cliente)
                .ToList();

            var listaReservas = new List<object>();
            foreach (Alquiler alquileres in lista)
            {
                if (alquileres.EstadoDeAlquilerId == estado && estado ==1)
                {
                    var reserva = new ReservaDeAbstractDTO()
                    {
                        ISBNLibro = alquileres.Libros.ISBN,
                        TituloLibro = alquileres.Libros.Titulo,
                        AutorLibro = alquileres.Libros.Autor,
                        EditorialLibro = alquileres.Libros.Editorial,
                        EdicionLibro = alquileres.Libros.Edicion,
                        ImagenLibro = alquileres.Libros.Imagen,
                        ClienteId = alquileres.ClienteId,
                        NombreCliente = alquileres.Cliente.Nombre,
                        ApellidoCliente = alquileres.Cliente.Apellido,
                        FechaReserva = alquileres.FechaReserva.ToString().Split(" ")[0]
                    };
                    listaReservas.Add(reserva);
                }
                else if (alquileres.EstadoDeAlquilerId == estado && estado == 2)
                {
                    var reserva = new AlquilerDeAbstractDTO()
                    {
                        ISBNLibro = alquileres.Libros.ISBN,
                        TituloLibro = alquileres.Libros.Titulo,
                        AutorLibro = alquileres.Libros.Autor,
                        EditorialLibro = alquileres.Libros.Editorial,
                        EdicionLibro = alquileres.Libros.Edicion,
                        ImagenLibro = alquileres.Libros.Imagen,
                        ClienteId = alquileres.ClienteId,
                        NombreCliente = alquileres.Cliente.Nombre,
                        ApellidoCliente = alquileres.Cliente.Apellido,
                        FechaAlquiler = alquileres.FechaAlquiler.ToString().Split(" ")[0],
                        FechaDevolucion = alquileres.FechaDevolucion.ToString().Split(" ")[0]
                    };
                    listaReservas.Add(reserva);
                }
            }
            return listaReservas;
        }


        public List<LibroDeClienteDTO> GetLibrosPorCliente(int idCliente)
        {
            var db = new QueryFactory(conexion, SqlKataCompiler);
            var libros = db.Query("Alquileres").
                Select(
                "EstadoDeAlquiler.Descripcion As Estado",
                "Cliente.Nombre As NombreCliente",
                "Cliente.Apellido As ApellidoCliente",
                "Libro.ISBN AS ISBN",
                "Libro.Titulo AS Titulo",
                "Libro.Autor AS Autor",
                "Libro.Editorial AS Editorial",
                "Libro.Edicion AS Edicion",
                "Libro.Imagen As Imagen").
                Join("Libro", "Libro.ISBN", "Alquileres.ISBN").
                Join("Cliente", "Cliente.ClienteId", "Alquileres.ClienteId").
                Join("EstadoDeAlquiler", "Alquileres.EstadoDeAlquilerId", "EstadoDeAlquiler.EstadoDeAlquilerId").
                Where("Cliente.ClienteId", "=", idCliente).
                Get<LibroDeClienteDTO>().ToList();
            return libros;
        }
    }
}
