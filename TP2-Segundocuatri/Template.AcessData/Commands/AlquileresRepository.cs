using Microsoft.EntityFrameworkCore;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Template.AcessData;
using Template.Domain.Commands;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.AccessData.Commands
{
    public class AlquileresRepository : GenericRepository<Alquileres>, IAlquileresRepository
    {
        private readonly IDbConnection connection;
        private readonly Compiler sqlKataCompiler;
        private readonly Context context;
        public AlquileresRepository(IDbConnection connection, Compiler sqlKataCompiler, Context contexto) : base(contexto)
        {
            this.connection = connection;
            this.sqlKataCompiler = sqlKataCompiler;
            this.context = contexto;
        }

        //lista de alquileres
        //public List<ClienteDTOs> GetAllLista()
        //{
        //    var db = new QueryFactory(connection, sqlKataCompiler);

        //    var query = db.Query("Alquileres");

        //    var result = query.Get<ClienteDTOs>();

        //    return result.ToList();
        //}

        public List<LibrosPorClienteDTOs> GetLibros(int IdCliente)
            {
                var db = new QueryFactory(connection, sqlKataCompiler);
                var libros = db.Query("Alquileres").
                    Select("EstadoDeAlquileres.Descripcion As Estado","Cliente.Nombre As NombreCliente","Cliente.Apellido As ApellidoCliente","Libros.Isbn AS Isbn",
                    "Libros.Titulo AS Titulo",
                    "Libros.Autor AS Autor",
                    "Libros.Editorial AS Editorial",
                    "Libros.Edicion AS Edicion",
                    "Libros.Imagen As Imagen").Join("Libros", "Libros.Isbn", "Alquileres.Isbn").Join("Cliente", "Cliente.ClienteId", "Alquileres.ClienteId").Join("EstadoDeAlquileres", "Alquileres.EstadoId", "EstadoDeAlquileres.EstadoId").
                    Where("Cliente.ClienteId", "=", IdCliente).
                    Get<LibrosPorClienteDTOs>().ToList();
                return libros;
            }


        public List<object> ObtenerPorEstado(int estado)
        {
            var lista = context.Alquileres
                //.Include(x => x.LibrosId)
                .Include(x => x.EstadoId)
                .Include(x => x.ClienteId)
                .ToList();

            var listaReservas = new List<object>();
            foreach (Alquileres alquileres in lista)
            {
                if (alquileres.EstadoId == estado && estado == 1)
                {
                    var reserva = new ReservaDeEstadoDTO()
                    {
                        ISBNLibro = alquileres.LibrosNavigator.Isbn,
                        TituloLibro = alquileres.LibrosNavigator.Titulo,
                        AutorLibro = alquileres.LibrosNavigator.Autor,
                        EditorialLibro = alquileres.LibrosNavigator.Editorial,
                        EdicionLibro = alquileres.LibrosNavigator.Edicion,
                        ImagenLibro = alquileres.LibrosNavigator.Imagen,
                        ClienteId = alquileres.ClienteId,
                        NombreCliente = alquileres.ClienteNavigator.Nombre,
                        ApellidoCliente = alquileres.ClienteNavigator.Apellido,
                        FechaReserva = alquileres.FechaReserva.ToString().Split(" ")[0]
                    };
                    listaReservas.Add(reserva);
                }
                else if (alquileres.EstadoId == estado && estado == 2)
                {
                    var reserva = new AlquilerEstadoDTO()
                    {
                        ISBNLibro = alquileres.LibrosNavigator.Isbn,
                        TituloLibro = alquileres.LibrosNavigator.Titulo,
                        AutorLibro = alquileres.LibrosNavigator.Autor,
                        EditorialLibro = alquileres.LibrosNavigator.Editorial,
                        EdicionLibro = alquileres.LibrosNavigator.Edicion,
                        ImagenLibro = alquileres.LibrosNavigator.Imagen,
                        ClienteId = alquileres.ClienteId,
                        NombreCliente = alquileres.ClienteNavigator.Nombre,
                        ApellidoCliente = alquileres.ClienteNavigator.Apellido,
                        FechaAlquiler = alquileres.FechaAlquiler.ToString().Split(" ")[0],
                        FechaDevolucion = alquileres.FechaDevolucion.ToString().Split(" ")[0]
                    };
                    listaReservas.Add(reserva);
                }
            }
            return listaReservas;
        }
    }
}
