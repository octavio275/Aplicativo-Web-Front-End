using Biblioteca.AccessData.BibliotecaDBContext;
using Biblioteca.AccessData.Commands;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Queries;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Biblioteca.AccessData.Queries
{
    public class ClienteRepository : GenericsRepository, IClienteRepository
    {
        private readonly IDbConnection conexion;
        private readonly Compiler SqlKataCompiler;

        public ClienteRepository(BibliotecaContext contexto, IDbConnection conexion, Compiler SqlKataCompiler) : base(contexto)
        {
            this.conexion = conexion;
            this.SqlKataCompiler = SqlKataCompiler;
        }

        public List<Cliente> GetClientes(string dni, string nombre, string apellido)
        {
            var db = new QueryFactory(conexion, SqlKataCompiler);
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
