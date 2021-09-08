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
    public class ClienteQuery : IClienteQuery
    {
        private readonly IDbConnection connection;
        private readonly Compiler sqlKataCompiler;
        private readonly Context context;

        public ClienteQuery(IDbConnection connection, Compiler sqlKataCompiler, Context context)
        {
            this.connection = connection;
            this.sqlKataCompiler = sqlKataCompiler;
            this.context = context;
        }

        public List<ClienteDTOs> GetAllListaCliente()
        {
            var db = new QueryFactory(connection, sqlKataCompiler);

            var query = db.Query("Cliente");

            var result = query.Get<ClienteDTOs>();

            return result.ToList();
        }

        public LoginDTOs ValidarUsuario(string contraseña)
        {
            LoginDTOs cliente = new LoginDTOs();
            var query1 = (from x in context.Cliente where x.Dni == contraseña select x).FirstOrDefault();
            if (query1 != null)
            {
                cliente.Id = query1.ClienteId;
                cliente.Verificacion = true;
            }
            else
            {
                cliente.Id = 0;
                cliente.Verificacion = false;
            }
            return cliente;


        }

        public List<Cliente> GetClientes(string dni, string nombre, string apellido)
        {
            var db = new QueryFactory(connection, sqlKataCompiler);
            var Clientes = db.Query("Cliente").
                Select("ClienteId", "DNI", "Nombre", "Apellido", "Email").
                WhereLike("Cliente.DNI", $"{dni}%").
                WhereLike("Cliente.Nombre", $"{nombre}%").
                WhereLike("Cliente.Apellido", $"{apellido}%").
                Get<Cliente>().ToList();
            return Clientes;
        }
   







    }
}
