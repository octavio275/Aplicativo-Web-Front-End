using AutoMapper;
using Biblioteca.Application.Services.Base;
using Biblioteca.Domain.DTOs.ClientesDTO;
using Biblioteca.Domain.DTOs.ClientesDTO.Response;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Queries;
using Biblioteca.Domain.Services;
using System;
using System.Collections.Generic;

namespace Biblioteca.Application.Services
{
    public class ClienteService : GenericsService, IClienteService
    {
        protected IClienteRepository Repository;

        public ClienteService(IClienteRepository repository, IMapper mapper) : base(repository,mapper)
        {
            Repository = repository;
        }

        public ClienteResponseDTO CreateCliente(ClienteDTO clientedto)
        {
            if(!EsValido(clientedto))
            {
                throw new Exception("Datos erroneos.");
            }

            var cliente = new Cliente()
            {
                DNI = clientedto.DNI,
                Nombre = clientedto.Nombre,
                Apellido = clientedto.Apellido,
                Email = clientedto.Email
            };

            repository.Add(cliente);
            repository.SaveChanges();

            return Mapper.Map<ClienteResponseDTO>(cliente);
        }

        public static bool EsValido(ClienteDTO clientedto)
        {
            if (DNIValido(clientedto.DNI) && DatoValido(clientedto.Nombre) && DatoValido(clientedto.Apellido) && DatoValido(clientedto.Email))
            {
                return true;
            }
            return false;
        }
        public static bool DNIValido(string dni)
        {
            if (EsNumero(dni) && dni.Length == 8)
            {
                return true;
            }
            return false;
        }

        public static bool EsNumero(string dni)
        {
            int entero;
            return int.TryParse(dni, out entero);
        }

        public static bool DatoValido(string cadena)
        {
            if (cadena.Length > 3 && !EsNumero(cadena))
            {
                return true;
            }
            return false;
        }

        public List<Cliente> GetAll()
        {
            var cuestionarios = this.repository.GetAll<Cliente>();
            return cuestionarios;
        }

        public List<ClienteDTO> GetClientes(string dni, string nombre, string apellido)
        {
            var listaClientes = Repository.GetClientes(dni, nombre, apellido);
            return Mapper.Map<List<ClienteDTO>>(listaClientes);
        }
    }
}
