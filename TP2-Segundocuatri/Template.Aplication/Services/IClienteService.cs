using AutoMapper;
using System;
using System.Collections.Generic;
using Template.Aplication.Services;
using Template.Domain.Commands;
using Template.Domain.DTOs;
using Template.Domain.Entities;
using Template.Domain.Queries;

namespace PS.template.aplicacion.services
{
    public interface IClienteService
    {
        ClienteRequestDTOs CreateCliente(ClienteDTOs cliente );
        public LoginDTOs GetValidarUsuario(string contraseña);

        List<ClienteDTOs> GetAllListaCliente();
        List<ClienteDTOs> GetClientes(string dni, string nombre, string apellido);
    }
    public class ClienteService : GenericService<Cliente>, IClienteService
    {
        protected IClienteRepository Repository;
        private readonly IClienteQuery _query;

        public ClienteService( IClienteQuery query, IClienteRepository repository, IMapper mapper) : base(repository, mapper)
        {
               _query = query;
            Repository = repository;


        }
    

        public ClienteRequestDTOs CreateCliente(ClienteDTOs clientedto)
        {
            if (clientedto.Nombre == "" || clientedto.Apellido == "" || clientedto.Dni == "" || clientedto.Email == "")
            {
                throw new Exception("Caracteres vacios vuelva a ingresar los datos");
            }
            var cliente = new Cliente()
            {
                Dni = clientedto.Dni,
                Nombre = clientedto.Nombre,
                Apellido = clientedto.Apellido,
                Email = clientedto.Email
            };

           this.Repository.Add(cliente);
            return new ClienteRequestDTOs() { ClienteId = cliente.ClienteId, Entidad = "Cliente" };
        }


        public List<ClienteDTOs> GetAllListaCliente()
        {
            return _query.GetAllListaCliente();
        }
     
        public LoginDTOs GetValidarUsuario(string contraseña)
        {
            return _query.ValidarUsuario(contraseña);
        }

        public List<ClienteDTOs> GetClientes(string dni, string nombre, string apellido)
        {
            var listaClientes = _query.GetClientes(dni, nombre, apellido);
            return Mapper.Map<List<ClienteDTOs>>(listaClientes);
        }


    }
}
