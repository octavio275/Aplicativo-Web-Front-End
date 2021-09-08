using AutoMapper;
using Biblioteca.Application.Services.Base;
using Biblioteca.Domain.DTOs;
using Biblioteca.Domain.DTOs.AlquileresDTO;
using Biblioteca.Domain.DTOs.AlquileresDTO.Response;
using Biblioteca.Domain.DTOs.LibrosDTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Queries;
using Biblioteca.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Application.Services
{
    public class AlquilerService : GenericsService, IAlquilerService
    {
        protected IAlquilerRepository Repository;
        public AlquilerService(IAlquilerRepository _repository, IMapper mapper) : base(_repository,mapper)
        {
            Repository = _repository;
        }

        public AlquilerResponseDTO AddAlquiler(AlquileryReservaRequestDTO alquilerRequestDTO)
        {
            /*if (!EsValido(alquilerRequestDTO))
            {
                throw new Exception("Datos erroneos");
            }*/
            if (!ExisteCliente(alquilerRequestDTO.ClienteId))
            {
                throw new Exception("No existe el cliente ingresado");
            }
            
            if (!ExisteLibro(alquilerRequestDTO.ISBN))
            {
                throw new Exception("No existe el libro ingresado");
            }
            if (!HayStock(alquilerRequestDTO.ISBN))
            {
                throw new Exception("Ese libro no tiene stock");
            }

            var alquiler = new Alquiler();
            if (alquilerRequestDTO.FechaAlquiler.Length > 1)
            {
                DateTime fecha = FechaValida(alquilerRequestDTO.FechaAlquiler);
                alquiler.EstadoDeAlquilerId = 2;
                alquiler.FechaAlquiler = fecha;
                alquiler.FechaDevolucion = fecha.AddDays(7);
            }
            else
            {
                DateTime fecha2 = FechaValida(alquilerRequestDTO.FechaReserva);
                alquiler.EstadoDeAlquilerId = 1;
                alquiler.FechaReserva = fecha2;
            }
            alquiler.ClienteId = alquilerRequestDTO.ClienteId;
            alquiler.ISBN = alquilerRequestDTO.ISBN;

            repository.Add(alquiler);

            Libro libro = this.repository.GetAll<Libro>().FirstOrDefault(item => item.ISBN == alquilerRequestDTO.ISBN);
            libro.Stock--;
            repository.Update<Libro>(libro);

            repository.SaveChanges();

            return Mapper.Map<AlquilerResponseDTO>(alquiler);
        }

        public bool EsValido(AlquileryReservaRequestDTO alquilerRequestDTO)
        {
            if (ExisteCliente(alquilerRequestDTO.ClienteId) && ExisteLibro(alquilerRequestDTO.ISBN))
            {
                return true;
            }
            return false;
        }

        public bool ExisteCliente(int id)
        {
            var cliente = repository.GetAll<Cliente>().FirstOrDefault(x => x.ClienteId == id);
            if (cliente != null)
            {
                return true;
            }
            return false;
        }
        public bool ExisteLibro(string isbn)
        {
            var libro = repository.GetAll<Libro>().FirstOrDefault(x => x.ISBN == isbn);
            if (libro != null)
            {
                return true;
            }
            return false;
        }

        public bool HayStock(string isbn)
        {
            var libro = repository.GetAll<Libro>().FirstOrDefault(x => x.ISBN == isbn);
            if (libro.Stock > 0)
            {
                return true;
            }
            return false;
        }

        public DateTime FechaValida(string cadenaFecha)
        {
            DateTime fecha;
            try
            {
                fecha = Convert.ToDateTime(cadenaFecha);
            }
            catch (Exception)
            {
                throw new Exception("La fecha ingresada es invalida");
            }
            return fecha;
        }

        public void AlquilarReserva(RequestAlquilarReserva paraAlquilarDTO)
        {
            

            if (!ExisteClienteYLibro(paraAlquilarDTO.cliente, paraAlquilarDTO.ISBN))
            {
                throw new Exception();
            }

            var alquileres = this.repository.GetAll<Alquiler>();

            //Con While
            
            bool reservado = false;
            int contador =  0;
            while (!reservado && contador < alquileres.Count)
            {
                if (alquileres[contador].ClienteId == paraAlquilarDTO.cliente && alquileres[contador].ISBN == paraAlquilarDTO.ISBN && alquileres[contador].EstadoDeAlquilerId == 1)
                {
                    reservado = true;
                    alquileres[contador].EstadoDeAlquilerId = 2;
                    alquileres[contador].FechaAlquiler = DateTime.Today;
                    //alquileres[contador].FechaReserva = null;
                    alquileres[contador].FechaDevolucion = DateTime.Today.AddDays(7);
                    this.repository.Update(alquileres[contador]);
                }
                contador++;
            }
            repository.SaveChanges();

            if (!reservado)
            {
                throw new Exception("El Cliente no tiene reservado ese libro");
            }
        }

        public bool ExisteClienteYLibro(int idCliente, string isbn)
        {
            if(ExisteCliente(idCliente) && ExisteLibro(isbn))
            {
                return true;
            }
            return false;
        }

        public List<object> GetPorEstado(int estado)
        {
            return Repository.ObtenerPorEstado(estado);
        }

        public List<LibroDeClienteDTO> GetLibrosPorCliente(int idCliente)
        {
            return Repository.GetLibrosPorCliente(idCliente);
        }
    }
}
