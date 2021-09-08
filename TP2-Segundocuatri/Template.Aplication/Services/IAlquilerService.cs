using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Template.Domain.Base;
using Template.Domain.Commands;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.Aplication.Services
{
    public interface IAlquilerService
    {
        void ActualizarStock(Alquileres alquileres);
        IAlquileres RegistrarAlquileres(AlquileresDTOs alquileres);
        RequestDTOs ActualizarReserva(ActualizarReservaDTO actualizarReservaDTO);
        List<object> GetByEstado(int estado);

        RequestDTOs Alquiler(AlquileresDTOs alquilerDTO);
        List<LibrosPorClienteDTOs> GetLibros(int idCliente);
    }
    public class AlquilerService : GenericService<Alquileres>, IAlquilerService
    {
        protected IAlquileresRepository Repository;


        public AlquilerService(IAlquileresRepository repository, IMapper mapper) : base(repository, mapper)
        {
            Repository = repository;
        }



        public void ActualizarStock(Alquileres alquileres)
        {
            var libro = this.generics.FindBy(x => x.Isbn == alquileres.Isbn, new string[] { "LibrosNavigator" })
                   .Select(x => x.LibrosNavigator).FirstOrDefault();

            libro.Stock -= 1;

            alquileres.LibrosNavigator = libro;

            generics.Update(alquileres);
        }

        public IAlquileres RegistrarAlquileres(AlquileresDTOs alquilerDTO)
        {


            if (alquilerDTO.FechaAlquiler.Equals(DateTime.MinValue))
            {

                var reserva = new Alquileres()
                {
                    ClienteId = alquilerDTO.ClienteId,
                    Isbn = alquilerDTO.Isbn,
                    EstadoId = 1,
                    FechaReserva = alquilerDTO.FechaReserva
                };

                this.generics.Add(reserva);
                //this.ActualizarStock(reserva);
                return new RequestDTOs() { Id = reserva.AlquileresId, Entity = "Reserva", FechaReserva = DateTime.Now };
            }
            else
            {
                var tabla = generics.GetAll().Where(x => x.EstadoId == 2).ToList();

                if (tabla.Count != 0)
                {
                    var alquiler = tabla.Where(x => x.Isbn == alquilerDTO.Isbn).FirstOrDefault();

                    if (alquiler != null)
                    {
            
                        return Alquiler(alquilerDTO);
                    }
                    else
                    {
                        return Alquiler(alquilerDTO);
                    }
                }
                else
                {
                    return Alquiler(alquilerDTO);
                }
            }
        }

        public RequestDTOs Alquiler(AlquileresDTOs alquilerDTO)
        {
            var alquileres = new Alquileres
            {
                ClienteId = alquilerDTO.ClienteId,
                Isbn = alquilerDTO.Isbn,
                EstadoId = 2,
                FechaAlquiler = alquilerDTO.FechaAlquiler,
                FechaDevolucion = alquilerDTO.FechaAlquiler.AddDays(7)
            };

            this.Repository.Add(alquileres);

            this.ActualizarStock(alquileres);

            return new RequestDTOs() { Id = alquileres.AlquileresId, Entity = "Alquiler", FechaAlquiler = DateTime.Now, FechaDevolucion = alquileres.FechaDevolucion };
        }


        public RequestDTOs ActualizarReserva(ActualizarReservaDTO actualizarReservaDTO)
        {

            var reserva = (this.generics.GetAll().Where(x => x.EstadoId == 1))
                .Where(x => x.ClienteId == actualizarReservaDTO.ClienteId && x.Isbn == actualizarReservaDTO.Isbn)
                .FirstOrDefault();

            if (reserva == null)
            {
                throw new Exception("La reserva no se ha encontrado");
            }
        
            reserva.EstadoId = 1;
            reserva.FechaAlquiler = DateTime.Now;
            reserva.FechaDevolucion = reserva.FechaAlquiler.AddDays(7);
            reserva.FechaReserva = DateTime.MinValue;

            this.generics.Update(reserva);

            return new RequestDTOs { Id = reserva.ClienteId, Isbn = reserva.Isbn, FechaAlquiler = reserva.FechaAlquiler, FechaDevolucion = reserva.FechaDevolucion };
        }
        public bool VerificarStock(string isbn)
        {
            var libro = this.generics.FindBy(x => x.Isbn == isbn, new string[]{ "LibrosNavigator" })
                    .Select(x => x.LibrosNavigator).FirstOrDefault();

            if (libro.Stock == 0)
            {
                return false;
            }
            return true;
        }
    

        public List<object> GetByEstado(int estado)
        {
            return Repository.ObtenerPorEstado(estado);
        }

        public List<LibrosPorClienteDTOs> GetLibros(int idCliente)
        {
            return Repository.GetLibros(idCliente);
        }

    }
}
