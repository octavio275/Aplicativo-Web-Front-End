using Template.AcessData;
using Template.Domain.Commands;
using Template.Domain.Entities;

namespace Template.AccessData.Commands
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(Context context) : base(context)
        {

        }
    }
}
